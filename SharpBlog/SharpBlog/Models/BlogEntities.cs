using System;
using System.Data.Entity;
using System.Linq;

namespace SharpBlog.Models
{
    public class BlogEntities : DbContext
    {
        public BlogEntities()
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Properties>();
        }

        public DbSet<Article> Articles { get; set; }

        public Properties Properties
        {
            get
            {
                var set = Set<Properties>();
                var properties = set.SingleOrDefault();
                if (properties == null)
                {
                    properties = new Properties
                                 {
                                     BlogName = "Unnamed blog"
                                 };
                    set.Add(properties);
                }
                return properties;
            }
        }
    }
}
