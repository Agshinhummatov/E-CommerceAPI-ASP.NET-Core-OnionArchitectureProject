using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Application.RequestParameters;
using E_CommerceAPI.Application.Services;
using E_CommerceAPI.Application.ViewModels.Products;
using E_CommerceAPI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;

namespace E_CommerceAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductReadRepository _productReadRepository;
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IWebHostEnvironment _webHostEnvironment;
        readonly IFileService _fileService;



        public ProductsController(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            this._webHostEnvironment = webHostEnvironment;
            _fileService = fileService;

        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] [FromServices]Pagination pagination)
        {
            
            var totalCount = _productReadRepository.GetAll(false).Count();

            var products = _productReadRepository.GetAll(false).Skip(pagination.Page * pagination.Size).Take(pagination.Size).Select(p => new
            {
                p.Id,
                p.Name,
                p.Price,
                p.Stock,
                p.CreatedDate,
                p.UpdatedDate
            }).ToList();

            return Ok(new
            {
                totalCount,
                products
            });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _productReadRepository.GetByIdAsync(id, false));
        }



        [HttpPost]
        public async Task<IActionResult> Post(VM_Create_Product model)
        {
            await _productWriteRepository.AddAsync(new() {

                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });

            await _productWriteRepository.SaveAsync();
            return StatusCode((int)HttpStatusCode.Created);
        }


        [HttpPut]
        public async Task<IActionResult> Put(VM_Updated_Product model)
        {
            Product product = await _productReadRepository.GetByIdAsync(model.Id);

            product.Name = model.Name;
            product.Price = model.Price;
            product.Stock = model.Stock;


            await _productWriteRepository.SaveAsync();

            return Ok();

        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productWriteRepository.RemoveAsync(id);
            await _productWriteRepository.SaveAsync();
            return Ok();
        }


        [HttpPost("action")]
        public async Task<IActionResult> Upload()
        {
           await _fileService.UploadAsync("resource/product-images", Request.Form.Files);

            return Ok();

        }



    }
}
