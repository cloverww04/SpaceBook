namespace SpaceBook.Models
{
    public class UserGeneratedSpaceContent
    {
        public int ContentId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ContentType? Type { get; set; } // e.g., SpaceFact, SpaceMission, EventDescription
        public int UserId { get; set; }
        public User? User { get; set; }
        // Add other properties, such as date created, comments, etc.
        public List<SpaceObjectContent>? AssociatedSpaceObjects { get; set; } // Many -> Many relationship
    }
}
