using Blog.Service.Domain.Commands.Posts;
using Blog.Service.Domain.Entities;
using Blog.Service.Domain.Repository;
using Blog.Service.Domain.Value_Objects;
using FakeItEasy;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Blog.Test
{
    public class CreatePostCommandHandlerTest
    {
        private readonly CreatePostCommand command;
        private IRepository<Post> fakeRepo;       
        private CancellationToken cancellationToken;
        private readonly Post post;

        public CreatePostCommandHandlerTest()
        {
            command = new CreatePostCommand { Title = "This is a very long post title", CreationDate = new DateTime(2020, 01, 01), Content = "Post content" }; ;
            fakeRepo = A.Fake<Repository<Post>>();
            cancellationToken = new CancellationToken();
            post = new Post(new PostTitle("This is a very long post title"), new DateTime(2020, 01, 01), "Post content");

        }

        [Fact]
        public void shouldThrowExceptionIfRepositoryIsNull()
        {
            //Arrange          
            IRequestHandler<CreatePostCommand, Post> createPostCommandHandler;         
            fakeRepo = null;

            //Act
            void action() => createPostCommandHandler = new CreatePostCommandHandler(fakeRepo);


            //Assert

            Assert.Throws<ArgumentNullException>(action);

        }

        [Fact]
        public async Task shouldReturnTheSavedPost()
        {
            //Arrange                
            var commandHandler = new CreatePostCommandHandler(fakeRepo);
           
            //Act
            var result = await commandHandler.Handle(command, cancellationToken);
          
            //Assert          
            Assert.Equal(post.Title.ToString(), result.Title.ToString());
            Assert.Equal(post.CreationDate, result.CreationDate);
            Assert.Equal(post.Content, result.Content);
        }

        [Fact]
        public async Task shouldCallSavePost()
        {
            //Arrange

            fakeRepo = A.Fake<IRepository<Post>>();
            var commandHandler = new CreatePostCommandHandler(fakeRepo);
           
            //Act
            var result = await commandHandler.Handle(command, cancellationToken);

            //Assert          
            A.CallTo(() => fakeRepo.Save(A<Post>._)).MustHaveHappened();

        }

    }
}
