using Application.Abstractions;
using Application.Commands;
using Application;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Presentation.Endpoints;
using Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Queries;
using Presentation.ViewModels.Products;

namespace Presentation.UnitTests
{
    public class ProductsControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Create_WithInvalidInput_ShouldReturnStatus500AndError()
        {
            Mock<ICommandBus> commandBusMock = new Mock<ICommandBus>();
            commandBusMock.Setup(s => s.Send<CreateProductCommand, Product>(It.IsAny<CreateProductCommand>())).Returns(Task.FromResult(OperationResult<Product>.FailureResult("")));
            Mock<IMapper> mapperMock = new Mock<IMapper>();

            var sut = new ProductsController(commandBusMock.Object, mapperMock.Object, null);
            var sutInput = new CreateProductRequestModel() { Name = "", Description = ""};
            var result = (await sut.Create(sutInput)) as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().BeOfType(typeof(ErrorResult));
            ((ErrorResult)result.Value).Messages.Should().NotBeEmpty();
            commandBusMock.Verify(p => p.Send<CreateProductCommand, Product>(It.IsAny<CreateProductCommand>()), Times.Once());
        }

        [Test]
        public async Task Create_WithValidInput_ShouldReturnStatus200AndProduct()
        {
            var returnsFromMock = new Product();
            Mock<ICommandBus> commandBusMock = new Mock<ICommandBus>();
            commandBusMock.Setup(s => s.Send<CreateProductCommand, Product>(It.IsAny<CreateProductCommand>())).Returns(Task.FromResult(OperationResult<Product>.SuccessResult(returnsFromMock)));

            Mock<IMapper> mapperMock = new Mock<IMapper>();
            var returnsFromMapperMock = new ProductResultModel() { };
            mapperMock.Setup(m => m.Map<Product, ProductResultModel>(returnsFromMock)).Returns(returnsFromMapperMock);

            var sut = new ProductsController(commandBusMock.Object, mapperMock.Object, null);
            var sutInput = new CreateProductRequestModel() { Name = "Name", Stock = 1, Description = "Description", Status = 1 };
            var result = (await sut.Create(sutInput)) as OkObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType(typeof(ProductResultModel));
            result.Value.Should().Be(returnsFromMapperMock);
            commandBusMock.Verify(p => p.Send<CreateProductCommand, Product>(It.IsAny<CreateProductCommand>()), Times.Once());
            mapperMock.Verify(p => p.Map<Product, ProductResultModel>(returnsFromMock), Times.Once());
        }

       
        [Test]
        public async Task GetById_WithContent_ShouldReturn200AndProducts()
        {
            Guid productId = Guid.NewGuid();
            Mock<IProductQueryService> productQueryServiceMock = new Mock<IProductQueryService>();
            var returnsFromQueryMock = new Product();
            productQueryServiceMock.Setup(m => m.FindById(productId)).Returns(Task.FromResult(returnsFromQueryMock));
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            var returnsFromMapperMock = new ProductResultModel() { };
            mapperMock.Setup(m => m.Map<Product, ProductResultModel>(returnsFromQueryMock)).Returns(returnsFromMapperMock);

            var sut = new ProductsController(null, mapperMock.Object, productQueryServiceMock.Object);

            var result = (await sut.GetById(productId)) as OkObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeOfType(typeof(ProductResultModel));
            result.Value.Should().Be(returnsFromMapperMock);
            productQueryServiceMock.Verify(p => p.FindById(productId), Times.Once());
            mapperMock.Verify(p => p.Map<Product, ProductResultModel>(returnsFromQueryMock), Times.Once());
        }

        [Test]
        public async Task GetById_WithNoContent_ShouldReturn400()
        {
            Guid productId = Guid.NewGuid();
            Mock<IProductQueryService> productQueryServiceMock = new Mock<IProductQueryService>();                        
            Mock<IMapper> mapperMock = new Mock<IMapper>();            

            var sut = new ProductsController(null, mapperMock.Object, productQueryServiceMock.Object);

            var result = (await sut.GetById(productId)) as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(404);            
            productQueryServiceMock.Verify(p => p.FindById(productId), Times.Once());
            mapperMock.Verify(p => p.Map<Product, ProductResultModel>(It.IsAny<Product>()), Times.Never());
        }
    }
}
