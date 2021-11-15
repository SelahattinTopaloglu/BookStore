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
   public interface IAuthorService
    {
        IDataResult<List<Author>> GetAll();
        IResult GetById(int id);
        IDataResult<AuthorDto> Add(AuthorDto author);
    }
}
