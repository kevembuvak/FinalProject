using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());

            foreach (var category in categoryManager.GetAll().Data)
            {
                Console.WriteLine(category.CategoryName);
            }

            ProductManager productManager = new ProductManager(new EfProductDal(), new CategoryManager(new EfCategoryDal()));

            var result = productManager.GetProuctDetailDtos();

            if (result.Success)
            {
                foreach (var item in productManager.GetProuctDetailDtos().Data)
                {
                    Console.WriteLine(item.ProductName + " / " + item.CategoryName);
                }
            } 
            else
            {
                Console.WriteLine(result.Message);
            }
        }

    }
}
