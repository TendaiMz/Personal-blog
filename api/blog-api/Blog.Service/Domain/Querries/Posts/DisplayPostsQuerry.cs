using Blog.Service.Domain.Entities;
using Blog.Service.Domain.Model;
using Blog.Service.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Service.Domain.Querries.Posts
{
    public class DisplayPostsQuery : IRequest<List<BlogPostModel>>
    {

    }
}
