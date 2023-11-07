namespace SpaceBook.Models
{
    public class SpaceObjectContent
    {
        public int SpaceObjectContentId { get; set; }
        public int SpaceObjectId { get; set; }
        public SpaceObject? SpaceObject { get; set; }
        public int ContentId { get; set; }
        public UserGeneratedSpaceContent? Content { get; set; }
    }
}
