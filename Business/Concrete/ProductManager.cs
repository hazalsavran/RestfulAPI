using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transactions;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Result;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _ProductDal;
        ICategoryService _categoryService;
        

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _ProductDal = productDal;
            _categoryService = categoryService;
           
        }
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))] //Attribute
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {
            IResult result =BusinessRules.Run(CheckIfProductNameExists(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryID),CheckIfCategoryLimitExceded());
            // yukarıdaki result kurala uymayan resulttur (iş kuralı) , IResult tipindedir
            if (result!=null)
            {
                return result;
            }
            _ProductDal.Add(product);
            return new SuccesResult(Messages.ProductAdded);
            
           
        }
        [CacheAspect]//daha önce her hangi bir kullanıcı ürünleri listelediyse onları cache'leyip performans yönetimini artırırız (tekrar tekrar veritabanına gitmeyi engeller)
                     // key, value  şeklinde tutulur, key => Business.Concrete.ProductManager.GetAll() 'dur
        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları
            return new SuccesDataResult<List<Product>>(_ProductDal.GetAll(),Messages.ProductListed);
                      // dataresult döndürülüyor çalışılan tip liste tipinde ürünler , döndürülen data=> _product.getAll()
                      //işlem sonucu => true 
                      //mesaj => Ürünler listelendi
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccesDataResult<List<Product>>(_ProductDal.GetAll(p => p.CategoryID == id));
        }
        [CacheAspect]
        public IDataResult<Product> GetById(int productID)
        {
            return new SuccesDataResult<Product>(_ProductDal.Get(p=>p.ProductID==productID));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccesDataResult<List<Product>>(_ProductDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccesDataResult<List<ProductDetailDto>>(_ProductDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")] //Iproductservicedeki tüm get cachelerini siler
       
        public IResult Update(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryID));
            if (result !=null)
            {
                return result;   
            }
            _ProductDal.Update(product);
            return new SuccesResult(Messages.ProductUpdated);
            
            

        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = _ProductDal.GetAll(p => p.CategoryID == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfError);
            }

            return new SuccesResult();
        }

        private IResult CheckIfProductNameExists(string productname)
        {
            var result = _ProductDal.GetAll(p => p.ProductName == productname).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }


            return new SuccesResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }


            return new SuccesResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);
            if (product.UnitPrice<10)
            {
                throw new Exception("");
            }
            Add(product);
            return null;
        }
    }
}
