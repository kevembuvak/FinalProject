﻿using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Core.Utilities.Business;
using Business.BusinessAspects.Autofac;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        //public ProductManager(IProductDal productDal, ICategoryDal categoryDal)
        //{
        //    _productDal = productDal;
        //    _categoryDal = categoryDal;
        //}
        // bir EntityManager, kendi entity'si dışında başka Dal injection yapamaz

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }


        // Validation kodu ile business kodu ayrıdır
        // Validation ---> verilen objenin, parametrenin uygunluğunu kontrol eder, mesela şifre ile şifre onay aynı 
        // değil ise burda hata verir

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            // İş Kodları - Business Codes -----> Koşullar Döngüler vs.



            //var result = _productDal.GetAll().Where(p => p.CategoryId == product.CategoryId).ToList();

            //if (result.Count >= 10)
            //{
            //    return new ErrorResult(Messages.ExcessiveProduct);
            //}
            //
            //İş kodu böyle yazılmaz, aynı kod update'te de var, DRY - do not repeat yourself

            IResult result = BusinessRules.Run(
                               CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                               CheckIfProductNameExists(product.ProductName),
                               CheckIfCategoryLimitIsReached()
                             );

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(), Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<ProductDetailDto>> GetProuctDetailDtos()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProuctDetailDtos());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            //var result = _productDal.GetAll().Where(p => p.CategoryId == product.CategoryId).ToList();

            //if (result.Count >= 10)
            //{
            //    return new ErrorResult(Messages.ExcessiveProduct);
            //}
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId) // Product product da verilebilirdi ama iş parçacığı olduğundan bu kadarı yeterli
        {
            var result = _productDal.GetAll().Where(p => p.CategoryId == categoryId).ToList().Count;

            if (result >= 10)
            {
                return new ErrorResult(Messages.ExcessiveProduct);
            }

            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll().Where(p => p.ProductName == productName).Any();

            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitIsReached()
        {
            var result = _categoryService.GetAll().Data.Count;

            if (result >= 15)
            {
                return new ErrorResult(Messages.CategoryLimitIsReached);
            }

            return new SuccessResult();
        }
    }
}
