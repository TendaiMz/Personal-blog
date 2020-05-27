using Blog.Service.Domain.Commands.Posts;
using Blog.Service.Domain.DTO;
using Blog.Service.Domain.Querries.Posts;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Blog.Service.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    public class BlogPostsController : BaseController
    {

        private readonly IMediator mediator;

        public BlogPostsController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new DisplayPostsQuery();
            var result = await mediator.Send(query);
            return Ok(result);

        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        //Q [MapToApiVersion("1.0")]
        public async Task<IActionResult> Post([FromBody] BlogPostDto blogPostDto)
        {
            var command = new CreatePostCommand()
            {
                Title = blogPostDto.Title, // "",
                Content = blogPostDto.Content,  //"Great Blog Content",
                CreationDate = DateTime.Now,
                IsArchived = blogPostDto.IsArchived
            };

            var result = await mediator.Send(command);
            return result is null ? BadRequest(result) : (IActionResult)Created("New post created", result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
