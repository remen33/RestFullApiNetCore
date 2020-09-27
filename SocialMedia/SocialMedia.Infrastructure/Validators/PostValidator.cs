namespace SocialMedia.Infrastructure.Validators
{
    using FluentValidation;
    using SocialMedia.Core.DTOs;
    using System;

    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .Length(10, 1000);

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now);     
        }
    }
}
