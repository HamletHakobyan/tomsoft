using System;

namespace SharpBlog.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PublicationDate { get; set; }
        public virtual Article Article { get; set; }
        public virtual Author Author { get; set; }
        public virtual Comment Parent { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public virtual Author LastModificationAuthor { get; set; }
    }
}
