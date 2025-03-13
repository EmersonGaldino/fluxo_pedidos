using Moq;
using order.flow.domain.entity.order;
using order.flow.domain.repository.Interface.order;
using order.flow.domain.service.order;
using Xunit;
using Assert = Xunit.Assert;

namespace order.flow.unitTest.order;

public class OrderItemServiceTests
{
    private readonly Mock<IOrderItemRepository> _repositoryMock;
    private readonly OrderItemService _service;

    public OrderItemServiceTests()
    {
        _repositoryMock = new Mock<IOrderItemRepository>();
        _service = new OrderItemService(_repositoryMock.Object);
    }

    [Fact]
    public async Task Post_ShouldReturnOrderItem_WhenSuccessful()
    {
        var orderItem = new OrderItemEntity { Id = Guid.NewGuid(), Description = "Breja Test", Quantity = 5 };

        _repositoryMock.Setup(r => r.Post(It.IsAny<OrderItemEntity>())).ReturnsAsync(orderItem);

        var result = await _service.Post(orderItem);

        Assert.NotNull(result);
        Assert.Equal(orderItem.Id, result.Id);
        Assert.Equal(orderItem.Description, result.Description);
        Assert.Equal(orderItem.Quantity, result.Quantity);

        _repositoryMock.Verify(r => r.Post(It.IsAny<OrderItemEntity>()), Times.Once);
    }

    [Fact]
    public async Task Post_ShouldThrowException_WhenRepositoryFails()
    {
        var orderItem = new OrderItemEntity { Id = Guid.NewGuid(), Description = "Mais uma breja test", Quantity = 5 };

        _repositoryMock.Setup(r => r.Post(It.IsAny<OrderItemEntity>())).ThrowsAsync(new Exception("Database error"));

        await Assert.ThrowsAsync<Exception>(() => _service.Post(orderItem));

        _repositoryMock.Verify(r => r.Post(It.IsAny<OrderItemEntity>()), Times.Once);
    }

    [Fact]
    public async Task GetAllAsyncCount_ShouldReturnList_WhenSuccessful()
    {
        var orderCounts = new List<OrderCountEntity>
        {
            new() { OrderId = Guid.NewGuid(), TotalQuantity = 10 },
            new() { OrderId = Guid.NewGuid(), TotalQuantity = 15 }
        };

        _repositoryMock.Setup(r => r.GetAllCountAsync()).ReturnsAsync(orderCounts);

        var result = await _service.GetAllAsyncCount();

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(orderCounts[0].OrderId, result[0].OrderId);
        Assert.Equal(orderCounts[1].TotalQuantity, result[1].TotalQuantity);

        _repositoryMock.Verify(r => r.GetAllCountAsync(), Times.Once);
    }

    [Fact]
    public async Task GetAllAsyncCount_ShouldThrowException_WhenRepositoryFails()
    {
        _repositoryMock.Setup(r => r.GetAllCountAsync()).ThrowsAsync(new Exception("Database error"));

        await Assert.ThrowsAsync<Exception>(() => _service.GetAllAsyncCount());

        _repositoryMock.Verify(r => r.GetAllCountAsync(), Times.Once);
    }
}