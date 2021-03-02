using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Concrete.EntitiyFramework;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryServices
    {
        ICategoryDal _category;

        public CategoryManager(ICategoryDal category)
        {
            _category = category;
        }

        public IDataResult<List<Category>> GetAll()
        {
           return  new SuccessDataResult<List<Category>>( _category.GetAll(),true,"hello"); 
        }

        public IDataResult<Category> GetElementByID(int categoryId)
        {
            return new SuccessDataResult<Category>(_category.Get(p => p.CategoryId == categoryId),true,"hello");
        }
    }
}
