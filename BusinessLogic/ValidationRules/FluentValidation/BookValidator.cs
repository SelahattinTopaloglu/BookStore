using Entity.DTOs;
using FluentValidation;

namespace BusinessLogic.ValidationRules.FluentValidation
{
    public class BookValidator: AbstractValidator<BookAddDto>
    {
        public BookValidator()
        {
            RuleFor(b=> b.BookName).MinimumLength(2);
            RuleFor(b=> b.PageNumber).GreaterThan(20);
            RuleFor(b => b.UnitPrice).GreaterThan(0);
            RuleFor(b => b.UnitPrice).GreaterThanOrEqualTo(10).When(b => b.AuthorId == 1);
          //  RuleFor(b => b.BookName).Must(StartWithA).WithMessage("Kitap ismi A ile başlamalı");
        }

        //private bool StartWithA(string arg)
        //{
        //    return arg.StartsWith("A");
        //}
    }
}
