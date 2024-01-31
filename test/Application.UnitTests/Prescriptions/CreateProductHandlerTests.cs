using Application.Handlers;
using Application.Queries;
using Domain.Entities;
using Domain.Repositories;
using Application.Commands;
using Application.Abstractions;

namespace Application.UnitTests
{
    public class CreateProductHandlerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Handle_WithInvalidCommand_ShouldDoNothing()
        {
            Mock<IProductRepository> productRepoMock = new Mock<IProductRepository>();
            Mock<IContextProvider> contextProvider = new Mock<IContextProvider>();
            var cmd = new CreateProductCommand("", 1, 2, "", 0, "");

            var sut = new CreateProductHandler(productRepoMock.Object, contextProvider.Object);            
            var result = await sut.Handle(cmd);

            result.Success.Should().BeFalse();
            result.FailureMessages.Should().HaveCount(1);

            contextProvider.Verify(p => p.GetCurrentUserId(), Times.Never);
            productRepoMock.Verify(p => p.Create(It.IsAny<Product>()), Times.Never);

        }     

        [Test]
        public async Task Handle_WithValidCommand_ShouldCallCreate()
        {
            Mock<IProductRepository> productRepoMock = new Mock<IProductRepository>();
            var returnFromMock = new Product();
            productRepoMock.Setup(m => m.Create(It.IsAny<Product>())).Returns(Task.FromResult(returnFromMock));
            Mock<IContextProvider> contextProvider = new Mock<IContextProvider>();
            contextProvider.Setup(m => m.GetCurrentUserId()).Returns(1);

            var cmd = new CreateProductCommand("name", 1, 2, "Description", 10, "USD"); ;

            var sut = new CreateProductHandler(productRepoMock.Object, contextProvider.Object);
            var result = await sut.Handle(cmd);            

            result.Success.Should().BeTrue();
            result.Result.Should().Be(returnFromMock);
            result.FailureMessages.Should().BeEmpty();

            contextProvider.Verify(p => p.GetCurrentUserId(), Times.Once);
            productRepoMock.Verify(p => p.Create(It.IsAny<Product>()), Times.Once);

        }
    }
}
