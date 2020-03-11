using Blog.Service.Domain.Commands.Posts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Service.Domain.Validation
{
        public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
        {
            public CreatePostCommandValidator()
            {
                RuleFor(x => x.Title).NotEmpty().Length(10, 300).WithMessage("Title is empty or too short");
                RuleFor(x => x.Content).NotEmpty().WithMessage("Content is empty");
            }
        }
    }

