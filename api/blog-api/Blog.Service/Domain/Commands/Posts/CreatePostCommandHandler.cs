using Blog.Service.Domain.Entities;
using Blog.Service.Domain.Repository;
using Blog.Service.Domain.Value_Objects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Blog.Service.Domain.Commands.Posts
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Post>
    {
        private readonly IRepository<Post> repo;

        public CreatePostCommandHandler(IRepository<Post> repo)
        {
            this.repo = repo ?? throw new ArgumentNullException($"{nameof(repo)} cannot be null");
        }
        public  async Task<Post> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var post = new Post(new PostTitle(request.Title), request.CreationDate, request.Content);
            await repo.Save(post);
            var pst = await repo.GetAll();
            return pst.LastOrDefault();
        }
    }
}
