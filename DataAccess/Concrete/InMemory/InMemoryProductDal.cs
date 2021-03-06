﻿using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products; // alt çizgi global olmasından


        //veri simülesi
        public InMemoryProductDal()
        {
            _products = new List<Product>
            {
                new Product{
                    ProductId = 1,
                    CategoryId = 1,
                    ProductName = "Bardak",
                    UnitPrice = 15,
                    UnitsInStock = 15
                },

                new Product{
                    ProductId = 2,
                    CategoryId = 1,
                    ProductName = "Kamera",
                    UnitPrice = 500,
                    UnitsInStock = 3
                },

                new Product{
                    ProductId = 3,
                    CategoryId = 2,
                    ProductName = "Telefon",
                    UnitPrice = 1500,
                    UnitsInStock = 2
                },

                new Product{
                    ProductId = 4,
                    CategoryId = 2,
                    ProductName = "Klavye",
                    UnitPrice = 150,
                    UnitsInStock = 65
                },

                new Product{
                    ProductId = 5,
                    CategoryId = 2,
                    ProductName = "Fare",
                    UnitPrice = 85,
                    UnitsInStock = 1
                }
            };
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            // _products.Remove(product); yazamayız çünkü arayüzden gönderdiğin product referans numarası ile listteki referans farklı

            Product productToDelete = null;  // new Product() demek gereksiz çünkü zaten p'nin referansını veriyorsun gereksiz bellek harcanması
            foreach (var p in _products)
            {
                if (product.ProductId == p.ProductId)
                {
                    productToDelete = p;
                }
            }
            _products.Remove(productToDelete);

            // Daha kısa yolu ----> LINQ = Language Integrated Query
            productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId); // yukarıda yapılan foreach dönmesini bu kısaca yapar ve return olarak eşit olan objeyi verir
        }


        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList(); // uyan her nesneyle yeni bir liste haline getirir ve döndürür
        }

        public List<ProductDetailDto> GetProuctDetailDtos()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {
            // gönderdiğim ürünün idsine sahip olan listedeki ürünü bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
