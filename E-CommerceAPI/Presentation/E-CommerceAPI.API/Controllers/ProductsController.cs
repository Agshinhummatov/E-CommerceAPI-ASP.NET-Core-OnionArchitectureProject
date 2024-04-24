using E_CommerceAPI.Application.Abstractions.Storage;
using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Application.RequestParameters;

using E_CommerceAPI.Application.ViewModels.Products;
using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Infrastructure.Services.Storage;
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
        //readonly IFileService _fileService;
        readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        readonly IProductImageFileReadRepository _productImageFileReadRepository;
        readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        readonly IFileReadRepository _fileReadRepository;
        readonly IFileWriteRepository _fileWriteRepository;
        readonly IStorageService _storageService;

       

        public ProductsController(IProductReadRepository productReadRepository, 
            IProductWriteRepository productWriteRepository, 
            IWebHostEnvironment webHostEnvironment, 
            IProductImageFileWriteRepository productImageFileWriteRepository,
            IProductImageFileReadRepository productImageFileReadRepository, 
            IInvoiceFileReadRepository invoiceFileReadRepository,
            IInvoiceFileWriteRepository invoiceFileWriteRepository,
            IFileReadRepository fileReadRepository,
            IFileWriteRepository fileWriteRepository,
            IStorageService storageService
            )
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            this._webHostEnvironment = webHostEnvironment;
            _fileReadRepository = fileReadRepository;
            _fileWriteRepository = fileWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _productImageFileReadRepository = productImageFileReadRepository;
            _productImageFileWriteRepository = productImageFileWriteRepository;
            _storageService = storageService;



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


        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {

           var datas =  await _storageService.UploadAsync("resource/files", Request.Form.Files);
            //var datas = await _fileService.UploadAsync("resource/file", Request.Form.Files);

            await _productImageFileWriteRepository.AddRangeAsync(datas.Select(d => new ProductImageFile()
            {
                FileName = d.fileName,
                Path = d.pathOrContainerName,
                 Storage = _storageService.StorageName


            }).ToList());
            await _productImageFileWriteRepository.SaveAsync();


            ////await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(d => new InvoiceFile()
            ////{
            ////    FileName = d.fileName,
            ////    Path = d.path,
            ////    Price = new Random().Next()


            ////}).ToList());

            ////await _invoiceFileWriteRepository.SaveAsync();


            //await _fileWriteRepository.AddRangeAsync(datas.Select(d => new E_CommerceAPI.Domain.Entities.File()
            //{
            //    FileName = d.fileName,
            //    Path = d.path,



            //}).ToList());

            //await _fileWriteRepository.SaveAsync();
            return Ok();

          

        }



    }
}
