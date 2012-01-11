using System;

namespace SharpBlog.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? PublicationDate { get; set; }
        public virtual Author Author { get; set; }
        public PublicationState State { get; set; }
        public DateTime? ModificationDate { get; set; }
        public virtual DateTime? LastModificationAuthor { get; set; }
    }
}
