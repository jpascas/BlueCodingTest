using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Domain.Entities;
using Application;
using Presentation.ViewModels;
using Application.Commands;
using Application.Queries;
using System.Reflection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Attributes;
using Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Presentation.ViewModels.Products;
using System.Collections;
using System.Xml.Linq;

namespace Presentation.Endpoints
{
    [Authorize]
    [Route("api/[controller]")]
    public class ProductsController : ApiController
    {
        private readonly IMapper mapper;
        private readonly ICommandBus commandBus;
        private readonly IProductQueryService productQueryService;

        public ProductsController(ICommandBus commandBus, IMapper mapper, IProductQueryService productQueryService)
        {
            this.commandBus = commandBus;
            this.mapper = mapper;
            this.productQueryService = productQueryService;
        }

        [HttpPost("")]
        [FluentValidationAutoValidationAttribute]
        public async Task<ActionResult> Create(CreateProductRequestModel createRequestModel)
        {
            var command = new CreateProductCommand(createRequestModel.Name, createRequestModel.Status, createRequestModel.Stock, createRequestModel.Description, createRequestModel.Price, createRequestModel.Currency);

            var productResult = await this.commandBus.Send<CreateProductCommand, Product>(command);

            if (productResult.Success)
            {
                ProductResultModel productModel = this.mapper.Map<Product, ProductResultModel>(productResult.Result);
                return Ok(productModel);
            }
            else
            {
                return ToFailureResult(productResult);
            }
        }   

        [HttpGet("{id}")]
        [FluentValidationAutoValidationAttribute]
        public async Task<ActionResult> GetById(Guid id)
        {
            var product = await this.productQueryService.FindById(id);
            if (product != null)
            {
                var productModel = this.mapper.Map<Product, ProductResultModel>(product);
                return Ok(productModel);
            }
            else
                return NotFound(null);
        }
    }
}
