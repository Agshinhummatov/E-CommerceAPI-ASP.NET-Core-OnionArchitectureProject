using E_CommerceAPI.Application.Abstractions.Storage;
using E_CommerceAPI.Application.Features.Commands.Product.CreateProduct;
using E_CommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using E_CommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using E_CommerceAPI.Application.Repositories;
using E_CommerceAPI.Application.RequestParameters;

using E_CommerceAPI.Application.ViewModels.Products;
using E_CommerceAPI.Domain.Entities;
using E_CommerceAPI.Infrastructure.Services.Storage;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        readonly IConfiguration _configuration;
        readonly IMediator _mediator;


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
,
            IConfiguration configuration,
            IMediator mediator)
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
            _configuration = configuration;
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest )
        {
            GetAllProductQueryResponce response = await _mediator.Send(getAllProductQueryRequest);

            return Ok(response);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(GetByIdProductQureyRequset getByIdProductQureyRequset)
        {
            GetByIdProductQureyResponse responce = await _mediator.Send(getByIdProductQureyRequset);
            return Ok();
        }



        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequset createProductCommandRequset)
        {
            CreateProductCommandResponce responce = await _mediator.Send(createProductCommandRequset);

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
        public async Task<IActionResult> Upload(string id)
        {

            List<(string fileName, string pathOrContainerName)> result =  await _storageService.UploadAsync("photo-images", Request.Form.Files);

            Product product = await _productReadRepository.GetByIdAsync(id);


            //foreach (var r in result)
            //{
            //    product.ProductImagesFile.Add(new()
            //    {
            //        FileName = r.fileName,
            //        Path = r.pathOrContainerName,
            //        Storage  = _storageService.StorageName,
            //        Products = new List<Product>() { product }
            //    });

            //}

            //await _productImageFileWriteRepository.AddRangeAsync(result.Select(d => new ProductImageFile()
            //{
            //    FileName = d.fileName,
            //    Path = d.pathOrContainerName,
            //    Storage = _storageService.StorageName,
            //    Products = new() { }


            //}).ToList());
            await _productImageFileWriteRepository.AddRangeAsync(result.Select(r => new ProductImageFile
            {
                FileName = r.fileName,
                Path = r.pathOrContainerName,
                Storage = _storageService.StorageName,
                Products = new List<Product>() { product }
            }).ToList());

            await _productImageFileWriteRepository.SaveAsync();

            return Ok();


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


           

          

        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages(string id)
        {
          Product? product = await _productReadRepository.Table.Include(p => p.ProductImagesFile).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

            return Ok(product.ProductImagesFile.Select(p => new
            {
                
                p.FileName,
                p.Id
            }));
        }

        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> DeleteProductImage( string id, string imageId)
        {
            Product? product = await _productReadRepository.Table.Include(p => p.ProductImagesFile).FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));

           ProductImageFile productImageFile  =  product.ProductImagesFile.FirstOrDefault(p => p.Id == Guid.Parse(imageId));

            product.ProductImagesFile.Remove(productImageFile);

            _productWriteRepository.SaveAsync();

            return Ok();
        }

    }
}
