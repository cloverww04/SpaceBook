using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using SpaceBook;
using SpaceBook.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//ADD CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:7095")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<SpaceBookDbContext>(builder.Configuration["SpaceBookDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// endpoints for users
app.MapGet("/api/users", async (SpaceBookDbContext db) =>
{
    var users = await db.Users.ToListAsync();

    if (users == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(users);
});

app.MapGet("/api/user/{id}", async (SpaceBookDbContext db, int id) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.UserId == id);

    if (user == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(user);
});

// Check if user exists
app.MapGet("/api/checkuser/{uid}", (SpaceBookDbContext db, string uid) =>
{
    var userExists = db.Users.Where(x => x.UID == uid).FirstOrDefault();
    if (userExists == null)
    {
        return Results.NotFound("User not found");
    }
    return Results.Ok(userExists);
});


app.MapPost("/api/user/create", async (SpaceBookDbContext db, User user) =>
{
    try
    {
        db.Users.Add(user);
        await db.SaveChangesAsync();

        return Results.Ok("User created successfully.");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
});

app.MapPut("/api/user/update/{id}", async (SpaceBookDbContext db, int id, User user) =>
{
    var userToUpdate = await db.Users.FirstOrDefaultAsync(u => u.UserId == id);

    if (userToUpdate == null)
    {
        return Results.NotFound();
    }

    userToUpdate.FirstName = user.FirstName;
    userToUpdate.LastName = user.LastName;

    await db.SaveChangesAsync();
    return Results.Ok(userToUpdate);

});

app.MapDelete("/api/user/delete/{id}", async (SpaceBookDbContext db, int id) =>
{
    var userToDelete = await db.Users.FirstOrDefaultAsync(x => x.UserId == id);

    if (userToDelete == null)
    {
        return Results.NotFound();
    }

    db.Remove(userToDelete);
    await db.SaveChangesAsync();
    return Results.Ok("Removed User");
});


// endpoints for SpaceObjects
app.MapGet("/api/spaceobjects", async (SpaceBookDbContext db) =>
{
    var spaceObjects = await db.SpaceObjects
    .Include(so => so.AssociatedSpaceContent)
    .ToListAsync();
    if (spaceObjects == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(spaceObjects);
});

app.MapGet("/api/spaceobject/{id}", async (SpaceBookDbContext db, int id) =>
{
    var spaceObject = await db.SpaceObjects
    .Include(so => so.AssociatedSpaceContent)
    .FirstOrDefaultAsync(so => so.SpaceObjectId == id);

    if (spaceObject == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(spaceObject);
});

// endpoints for UserGeneratedSpaceContent
app.MapGet("/api/spacecontent", async (SpaceBookDbContext db) =>
{
    var spaceContent = await db.UsersGeneratedSpaceContent
    .Include(sc => sc.SpaceObjectContents)
        .ThenInclude(sc => sc.SpaceObject)
    .Include(sc => sc.User)
    .Include(sc => sc.Type)
    .ToListAsync();
    if (spaceContent == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(spaceContent);
});

app.MapGet("/api/spacecontent/{id}", async (SpaceBookDbContext db, int id) =>
{
    var spaceContent = await db.UsersGeneratedSpaceContent
    .Include(sc => sc.SpaceObjectContents)
        .ThenInclude(sc => sc.SpaceObject)
    .Include(sc => sc.User)
    .Include(sc => sc.Type)
    .Include(sc => sc.Comments)
    .FirstOrDefaultAsync(sc => sc.ContentId == id);

    if (spaceContent == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(spaceContent);
});


app.MapPost("/api/spacecontent/create", async (SpaceBookDbContext db, UserGeneratedSpaceContent userGeneratedSpaceContent) =>
{
    try
    {

        db.UsersGeneratedSpaceContent.Add(userGeneratedSpaceContent);
        await db.SaveChangesAsync();

        return Results.Created($"/api/spaceconten/create/{userGeneratedSpaceContent.ContentId}", userGeneratedSpaceContent);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
});


app.MapPut("/api/spacecontent/update/{id}", async (SpaceBookDbContext db, int id, UserGeneratedSpaceContent spaceContent) =>
{
    var contentToUpdate = await db.UsersGeneratedSpaceContent.FirstOrDefaultAsync(sc => sc.ContentId == id);

    if (contentToUpdate == null)
    {
        return Results.NotFound();
    }

    contentToUpdate.Title = spaceContent.Title;
    contentToUpdate.Description = spaceContent.Description;
    contentToUpdate.Type = spaceContent.Type;
    contentToUpdate.SpaceObjectContents = spaceContent.SpaceObjectContents;

    await db.SaveChangesAsync();
    return Results.Ok(contentToUpdate);

});

app.MapDelete("/api/spacecontent/delete/{id}", async (SpaceBookDbContext db, int id) =>
{
    var deleteContent = await db.UsersGeneratedSpaceContent.SingleOrDefaultAsync(sc => sc.ContentId == id);

    if (deleteContent == null)
    {
        return Results.BadRequest();
    }

    db.Remove(deleteContent);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// endpoints for comments
app.MapPost("/api/comment/create", async (SpaceBookDbContext db, Comment comment) =>
{
    try
    {
        db.Comments.Add(comment);
        await db.SaveChangesAsync();

        return Results.Ok("comment created successfully.");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }

});

app.MapPut("/api/comment/{id}", async (SpaceBookDbContext db, int id, Comment comment) =>
{
    var commentToUpdate = await db.Comments.FirstOrDefaultAsync(c => c.Id == id);

    if (commentToUpdate == null)
    {
        return Results.NotFound();
    }

    commentToUpdate.Content = comment.Content;

    await db.SaveChangesAsync();
    return Results.Ok(commentToUpdate);
});

app.MapDelete("/api/comment/remove/{id}", async (SpaceBookDbContext db, int id) =>
{
    var commentToRemove = await db.Comments.FirstOrDefaultAsync(c => c.Id == id);

    if (commentToRemove == null)
    {
        return Results.NotFound();
    }

    db.Remove(commentToRemove);
    await db.SaveChangesAsync();
    return Results.Ok("Comment deleted");
});

// endpoints for content type
app.MapGet("/api/type", async (SpaceBookDbContext db) =>
{
    var contentType = await db.ContentTypes.ToListAsync();

    if (contentType == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(contentType);
});


// endpoints for spaceObjectContent

app.MapPost("/api/spaceobjectcontent/create/{contentId}/{soId}", async (SpaceBookDbContext db, SpaceObjectContent soc, int contentId, int soId) =>
{
    try
    {
        soc.ContentId = contentId;
        soc.SpaceObjectId = soId;


        db.SpaceObjectsContent.Add(soc);
        await db.SaveChangesAsync();

        return Results.Created($"/api/spaceobjectcontent/create/{soc.SpaceObjectContentId}", soc);

    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});


app.MapPut("/api/spaceobjectcontent/update/{contentId}/{soId}", async (SpaceBookDbContext db, int contentId, int soId, SpaceObjectContent updatedSpaceObjectContent) =>
{
    try
    {

        var spaceObjectContent = await db.SpaceObjectsContent
        .SingleOrDefaultAsync(soc => soc.ContentId == contentId && soc.SpaceObjectId == soId);
        if (spaceObjectContent == null)
        {
            return Results.NotFound();
        }

        spaceObjectContent.SpaceObjectId = updatedSpaceObjectContent.SpaceObjectId;

        await db.SaveChangesAsync();

        return Results.Ok("SpaceObjectContent updated successfully.");
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});





app.Run();
