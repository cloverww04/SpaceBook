namespace SpaceBook.Models
{
    public class SpaceObject
    {
        public int SpaceObjectId { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        // Add other space object properties as needed
        public List<SpaceObjectContent>? AssociatedSpaceContent { get; set; } // Many -> Many relationship
    }
}
