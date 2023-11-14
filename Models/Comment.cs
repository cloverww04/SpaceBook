namespace SpaceBook.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public UserGeneratedSpaceContent? UserGeneratedSpaceContent { get; set; }
        public Comment()
        {
            this.CreatedDate = DateTime.Now;
        }

    }
}
