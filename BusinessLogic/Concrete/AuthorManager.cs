using AutoMapper;
using BusinessLogic.Abstract;
using BusinessLogic.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Concrete
{
    public class AuthorManager : IAuthorService
    {
        IAuthorDal _authorDal;
        IMapper _mapper;

        public AuthorManager(IAuthorDal authorDal, IMapper mapper)
        {
            _authorDal = authorDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(AuthorValidator))]
        public IDataResult<AuthorDto> Add(AuthorDto author)
        {
            Author entity = _mapper.Map<AuthorDto, Author>(author);

            _authorDal.Add(entity);
            return new SuccessDataResult<AuthorDto>(_mapper.Map<AuthorDto>(entity));
        }
        public IDataResult<List<Author>> GetAll()
        {
            return new SuccessDataResult<List<Author>>(_authorDal.GetAll());
        }

        public IResult GetById(int id)
        {
            Author author = _authorDal.GetById(id);
            var result = _mapper.Map<AuthorDto>(author);
            if (result==null)
            {
                return new ErrorResult("Author Not Found");
            }
            return new SuccessDataResult<AuthorDto>(result);
        }
    }
}
