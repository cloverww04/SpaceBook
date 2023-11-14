namespace SpaceBook.Models
{
    public class ContentType
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public UserGeneratedSpaceContent? UserGeneratedSpaceContent { get; set; }
    }
}
