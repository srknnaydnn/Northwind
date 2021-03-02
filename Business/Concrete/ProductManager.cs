using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntitiyFramework;
using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductServices
    {
        IProductDal _productDal;
        ICategoryServices _categoryServices;

        public ProductManager(IProductDal productDal, ICategoryServices categoryServices)
        {
            _productDal = productDal;
            _categoryServices = categoryServices;

        }
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ValidationTool))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckProductName(product.ProductName), CheckIfCategoryCount());
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.Added);

        }



        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 12)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), true, Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new DataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), true);
        }

        public IDataResult<List<Product>> GetById(int id)
        {
            return new DataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id), true);
        }



        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new DataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max), true);
        }

        IDataResult<Product> IProductServices.GetById(int id)
        {
            return new DataResult<Product>(_productDal.Get(p => p.ProductId == id), true, "ürün Detayı");
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {

            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count();

            if (result >= 10)
            {
                return new ErrorResults("bir categoride en fazla 10 ürün olabilir...");
            }
            return new SuccessResult();
        }

        private IResult CheckProductName(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResults("AYNI İSİMDE ÜRÜN BULUNMAKTADIR");
            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryCount()
        {
            var result = _categoryServices.GetAll();

            if (result.Data.Count>=8)
            {
                return new ErrorResults("Category SAyısı 15'i geçemez");
            }
            return new SuccessResult();
        }
    }
}
