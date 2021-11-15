using AutoMapper;
using BusinessLogic.Abstract;
using BusinessLogic.BusinessAspect.Autofac;
using BusinessLogic.CCS;
using BusinessLogic.Constants;
using BusinessLogic.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Entities;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Concrete
{
    public class BookManager : IBookService
    {
        IMapper _mapper;
        IBookDal _bookDal;
        IAuthorService _authorService;
        // ILogger _logger;
        public BookManager(IBookDal bookDal, IAuthorService authorService, IMapper mapper)
        {
            _bookDal = bookDal;
            _authorService = authorService;
            _mapper = mapper;
        }

        [SecuredOperation("book.add, admin")]
        [ValidationAspect(typeof(BookValidator))]
        [CacheRemoveAspect("IBookService.Get")]
        public IResult Add(BookAddDto book)
        {
            //business code
            //

            IResult result = BusinessRules.Run(CheckBookNameExists(book.BookName), CheckIfAuthorLimit());

            if (result != null)
            {
                return result;
            }
            Book entity = _mapper.Map<BookAddDto, Book>(book);
            CheckAuthor(book, entity);

            _bookDal.Add(entity);
            return new SuccessResult(Messages.BookAdded);

        }

        private void CheckAuthor(BookAddDto book, Book entity)
        {
            if (book.AuthorId > 0)
            {
                IResult dataResult = _authorService.GetById(book.AuthorId);
                if (dataResult.Success)
                {
                    SuccessDataResult<AuthorDto> successDataResult = dataResult as SuccessDataResult<AuthorDto>;
                    entity.AuthorId = successDataResult.Data.Id;
                }
            }
            else if (book.Author != null)
            {
                IDataResult<AuthorDto> authorResult = _authorService.Add(book.Author);
                if (authorResult.Success)
                {
                    entity.AuthorId = authorResult.Data.Id;
                }
            }
        }
        [CacheAspect]
        public IDataResult<List<BookListDto>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<BookListDto>>(Messages.MaintenanceTime);
            }

            List<Book> data = _bookDal.GetAll(null, new Expression<Func<Book, object>>[] { x => x.Author  });
            List<BookListDto> bookListDtos = _mapper.Map<List<BookListDto>>(data);
            return new SuccessDataResult<List<BookListDto>>(bookListDtos, Messages.BooksListed);
        }

        public IDataResult<List<BookListDto>> GetAll2()
        {
            List<BookListDto> data = _bookDal.GetAll2();
            List<BookListDto> bookLists = _mapper.Map<List<BookListDto>>(data);
            return new SuccessDataResult<List<BookListDto>>(bookLists, Messages.BooksListed);
        }

        public IDataResult<List<BookDetailDto>> GetBookDetails()
        {
            return new SuccessDataResult<List<BookDetailDto>>(_bookDal.GetBookDetails());
        }

        public IDataResult<List<BookListDto>> GetByAuthorId(int id)
        {
            List<Book> books = _bookDal.GetAll(b => b.AuthorId == id);
            List<BookListDto> bookListDtos = _mapper.Map<List<BookListDto>>(books);
            return new SuccessDataResult<List<BookListDto>>(bookListDtos);
        }

        [CacheAspect]
        public IDataResult<BookListDto> GetById(int bookId)
        {
            Book book = _bookDal.Get(b => b.Id == bookId);
            BookListDto bookListDto = _mapper.Map<BookListDto>(book);
            return new SuccessDataResult<BookListDto>(bookListDto);
        }
        [CacheAspect]
        public IDataResult<BookListDto> GetById2(int id)
        {
            BookListDto book = _bookDal.GetById2(id);
            BookListDto bookListDto = _mapper.Map<BookListDto>(book);
            return new SuccessDataResult<BookListDto>(bookListDto);
        }


        public IDataResult<List<BookListDto>> GetByUnitPrice(double max, double min)
        {
            List<Book> data = _bookDal.GetAll(b => b.UnitPrice >= min && b.UnitPrice <= max);
            List<BookListDto> bookListDto = _mapper.Map<List<BookListDto>>(data);
            return new SuccessDataResult<List<BookListDto>>(bookListDto);
        }

        [ValidationAspect(typeof(BookValidator))]
        [CacheRemoveAspect("IBookService.Get")]
        public IResult Update(BookUpdateDto book)
        {
            //business code
            //b => b.Author == book.Author

            //var result = _bookDal.GetAll(b=> b.Author.Id == book.AuthorId).Count;
            //if (result >= 10)
            //{
            //    return new ErrorResult();
            //}
            _bookDal.Update(_mapper.Map<BookUpdateDto, Book>(book));
            return new SuccessResult(Messages.Updated);
        }

        public IResult Delete(int id)
        {
            Book deletedBook = _bookDal.GetById(id);

            _bookDal.Delete(deletedBook);
            return new SuccessResult(Messages.BookDeleted);
        }
        //private IResult CheckIfAuthorCountOfBook(Author author)
        //{
        //    var result = _bookDal.GetAll(b => b.Author == author).Count;
        //    if (result >= 10)
        //    {
        //        return new ErrorResult();
        //    }
        //    return new SuccessResult();
        //}

        private IResult CheckBookNameExists(string bookName)
        {
            var result = _bookDal.GetAll(b => b.BookName == bookName).Any();
            if (result)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        private IResult CheckIfAuthorLimit()
        {
            var result = _authorService.GetAll();
            if (result.Data.Count > 45)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(BookAddDto book)
        {
            Add(book);
            if (book.UnitPrice < 10)
            {
                throw new Exception("");
            }
            Add(book);
            return null;

        }
    }
}
