using Blog.Service.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service.Domain.Commands.Posts
{
    public class CreatePostCommand:IRequest<Post>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        public bool IsArchived { get; set; }
        public DateTime CreationDate { get; set; }

    }
}
