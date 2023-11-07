namespace SpaceBook.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UID { get; set; }

        public List<UserGeneratedSpaceContent>? CreatedSpaceContent { get; set; } // 1 -> Many relationship
    }
}
