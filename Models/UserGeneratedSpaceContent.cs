﻿using System.ComponentModel.DataAnnotations;

namespace SpaceBook.Models
{
    public class UserGeneratedSpaceContent
    {
        [Key]
        public int ContentId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int TypeId { get; set; }
        public ContentType? Type { get; set; } // e.g., SpaceFact, SpaceMission, EventDescription
        public int UserId { get; set; }
        public User? User { get; set; }
        public DateTime? CreatedOn { get; set; }
        public SpaceObject? SpaceObject { get; set; }
        // Add other properties, such as date created, comments, etc.
        public List<SpaceObjectContent>? SpaceObjectContents { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
