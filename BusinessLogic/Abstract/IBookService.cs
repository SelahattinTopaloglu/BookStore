using Core.Utilities.Results;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Abstract
{
    public interface IBookService
    {
        IDataResult<List<BookListDto>> GetAll();
        IDataResult<List<BookListDto>> GetAll2();
        IDataResult<List<BookListDto>> GetByAuthorId(int id);
        IDataResult <List<BookDetailDto>> GetBookDetails();
        IDataResult<List<BookListDto>> GetByUnitPrice(double  max, double min);
        IDataResult<BookListDto> GetById(int id);
        IDataResult<BookListDto> GetById2(int id);
        IResult Add(BookAddDto book);
        IResult Update(BookUpdateDto book);
        IResult Delete(int id);
        IResult AddTransactionalTest(BookAddDto book);

    }
}
