using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using SpaceBook;
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
        return Results.StatusCode(204);
    }
    return Results.Ok(userExists);
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
    .Include(sc => sc.AssociatedSpaceObjects)
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
    .Include(sc => sc.AssociatedSpaceObjects)
    .FirstOrDefaultAsync(sc => sc.ContentId == id);

    if (spaceContent == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(spaceContent);
});


app.Run();
