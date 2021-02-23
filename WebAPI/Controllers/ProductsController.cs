using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // -----> attribute - class ile ilgili bilgi verme, imzalama
    public class ProductsController : ControllerBase
    {
        // Loosely coupled - gevşek bağlılık
        // Inversion of Control --> IoC Container
        // AOP --> Aspect Oriented Programming --> Example: [LogAspect]
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        // Sadece IProductService ile çalışmaz, manager new'layamıyor, productService ile ilgili somut bir şey yok

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            // Swagger
            // Dependency chain
            // IProductService productService = new ProductManager(new EfProductDal());
            var result = _productService.GetAll();

            if(result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);

            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);

            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

        // Güncelleme ve silme için post da kullanılabilir, update ve delete de kullanılabilir
    }
}
