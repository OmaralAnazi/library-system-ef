using FluentValidation;
using LibrarySystem.Data.Dtos;
using LibrarySystem.Data.Repositories;

namespace LibrarySystem.Data.Validators;

public sealed class CreateBookRequestDtoValidator : AbstractValidator<CreateBookRequestDto>
{
    public CreateBookRequestDtoValidator(IBooksRepository repo)
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(256);
        RuleFor(x => x.Author).NotEmpty().MaximumLength(256);

        RuleFor(x => x.Isbn)
            .NotEmpty().MaximumLength(32)
            .Matches(@"^[0-9\-Xx]+$").WithMessage("Invalid ISBN format.")
            .MustAsync(async (isbn, ct) =>
                (await repo.FindOneAsync(b => b.Isbn == isbn, cancellationToken: ct)) == null)
            .WithMessage("ISBN must be unique.");

        RuleFor(x => x.PublishedAt)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("PublishedAt cannot be in the future.");
    }
}