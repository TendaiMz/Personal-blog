using Blog.Service.Domain.Value_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service.Domain.Entities
{
    public class Post:Entity
    {

        public Post(PostTitle posttitle, DateTime? dateOfCreation, string content)
        {
            Title = posttitle ?? throw new ArgumentNullException(nameof(posttitle));
            CreationDate = dateOfCreation ?? throw new ArgumentNullException(nameof(content));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public PostTitle Title { get; }
        public string Content { get; }
        public DateTime? ArchiveDate { get; private set; }
        public bool IsArchived { get; set; } = false;

        //public void ArchivePost()
        //{
        //    if (this.IsArchived)
        //    {
        //        throw new BusinessRuleException($"{nameof(Title)} Has been archived already");
        //    }

        //    this.IsArchived = true;
        //    this.ArchiveDate = DateTime.Now;
        //}
    }
}

