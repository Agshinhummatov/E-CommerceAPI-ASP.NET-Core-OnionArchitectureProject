using E_CommerceAPI.Application.Abstractions.Storage;
using E_CommerceAPI.Application.Features.Commands.Product.CreateProduct;
using E_CommerceAPI.Application.Features.Commands.Product.RemoveProduct;
using E_CommerceAPI.Application.Features.Commands.Product.UpdateProduct;
using E_CommerceAPI.Application.Features.Commands.ProductImageFile.RemoveProdcutImage;
using E_CommerceAPI.Application.Features.Commands.ProductImageFile.UploadProdcutImage;
using E_CommerceAPI.Application.Features.Queries.Product.GetAllProduct;
using E_CommerceAPI.Application.Features.Queries.Product.GetByIdProduct;
using E_CommerceAPI.Application.Features.Queries.ProductImageFile.GetProductImages;
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
       
        readonly IConfiguration _configuration;
        readonly IMediator _mediator;


        public ProductsController(
            IMediator mediator)
        {
            
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest )
        {
            GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);

            return Ok(response);
        }
        

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute]GetByIdProductQureyRequset getByIdProductQureyRequset)
        {
            GetByIdProductQureyResponse responce = await _mediator.Send(getByIdProductQureyRequset);
            return Ok(responce);
        }



        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommandRequset createProductCommandRequset)
        {
            CreateProductCommandResponse responce = await _mediator.Send(createProductCommandRequset);

            return StatusCode((int)HttpStatusCode.Created);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody]UpdateProductCommandRequset updateProductCommandRequset)
        {
           UpdateProductCommandResponce responce = await _mediator.Send(updateProductCommandRequset);
            return Ok();

        }

        
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] RemoveProductCommandRequset removeProductCommandRequset)
        {
            RemoveProductCommandResponce responce = await _mediator.Send(removeProductCommandRequset);
            return Ok();
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Upload([FromQuery] UploadProductImageCommandRequest uploadProductImageCommandRequest )
        {

            uploadProductImageCommandRequest.Files = Request.Form.Files;
            UploadProductImageCommandResponse uploadProductImageCommandResponce = await _mediator.Send(uploadProductImageCommandRequest);

            return Ok();



        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetProductImages([FromRoute] GetProductImagesQueryRequest getProductImagesQueryRequest)
        {
            List<GetProductImagesQueryResponse> response = await _mediator.Send(getProductImagesQueryRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult> DeleteProductImage( [FromRoute] RemoveProductImageCommandRequest removeProductImageCommandRequest, [FromQuery] string imageId)
        {
            removeProductImageCommandRequest.ImageId = imageId;
            RemoveProductImageCommandResponse responce = await _mediator.Send(removeProductImageCommandRequest);
            return Ok();
        }

    }
}
