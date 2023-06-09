using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.CrossCottingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccsess.Abstract;
using DataAccsess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    //[logAspect]-->AOP
    //[validate]-->AOP
    //[RemoveCache]-->AOP
    //[Transaction]-->AOP
    //[Performance]-->AOP
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;//İNJECTİON UYGULADIK(enjekte ettik)product manager içinde
                                  //ikisini birden yapabilirdik
        public ProductManager(ICategoryService categoryService, IProductDal productDal)
        {
            _categoryService= categoryService;
            _productDal = productDal;

        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult add(Product product)
        {
            //doğrulamanın yapıldığı en kötü kod yöntemi buradaydı.Validation tool'a taşıdık

            //ValidationTool.Validate(new ProductValidator(), product);


            IResult result= BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrent(product.CategoryId),
                CheckIfCategoryLimitExceded());//iş kuralları ekleme
            if (result!=null)
            {
                return result;
            }
            _productDal.Add(product);

            return new SuccessResult(Masseges.ProductAdded);

        }
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 16)
            {
                return new ErrorDataResult<List<Product>>(Masseges.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Masseges.ProductsListed);

        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));//succes data içinde product gönderiyoruz
                                                                                                  //sonra conctructor gönderiyoruz
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && max >= p.UnitPrice));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 18)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Masseges.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult update(Product product)
        {
            var result=_productDal.GetAll(p=>p.CategoryId==product.CategoryId).Count;
            if(result >= 10)
            {
                return new ErrorResult(Masseges.ProductCountOfCategoryError);
            }
            throw new NotImplementedException();
        }

        //bir kategoride en fazla 10 ürün olabilir diye bir istek gelirse ne yapmalıyız

        private IResult CheckIfProductCountOfCategoryCorrent(int categoryId)
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Masseges.ProductCountOfCategoryError);

            }
            return new SuccessResult();
        }
        //aynı isimde ürün eklenemez.
        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Masseges.ProductNameAlreadyExists);

            }
            return new SuccessResult();
        }
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if(result.Data.Count > 15) 
            {
                return new ErrorResult(Masseges.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
