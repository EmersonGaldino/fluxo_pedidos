using Moq;
using order.flow.domain.entity.order;
using order.flow.domain.Interface.order;
using order.flow.domain.repository.Interface.order;
using order.flow.domain.service.order;
using Xunit;
using Assert = Xunit.Assert;

namespace order.flow.unitTest.order;

public class OrderServiceTest
{
    private readonly Mock<IOrderRepository> _repositoryMock;
    private readonly Mock<IOrderItemService> _orderItemServiceMock;
    private readonly OrderService _service;

    public OrderServiceTest()
    {
        _repositoryMock = new Mock<IOrderRepository>();
        _orderItemServiceMock = new Mock<IOrderItemService>();
        _service = new OrderService(_repositoryMock.Object, _orderItemServiceMock.Object);
    }

    [Fact]
    public async Task Post_ShouldReturnOrder_WhenSuccessful()
    {
        var order = new OrderEntity
        {
            Items = new List<OrderItemEntity>
            {
                new OrderItemEntity { Description = "Item 1", Quantity = 10 },
                new OrderItemEntity { Description = "Item 2", Quantity = 20 }
            }
        };

        _repositoryMock.Setup(r => r.Post(It.IsAny<OrderEntity>())).ReturnsAsync((OrderEntity o) => o);
        _orderItemServiceMock.Setup(s => s.Post(It.IsAny<OrderItemEntity>())).ReturnsAsync((OrderItemEntity i) => i);

        var result = await _service.Post(order);

        Assert.NotNull(result);
        Assert.Equal(2, result.Items.Count);
        Assert.Equal('R', result.Status);

        _repositoryMock.Verify(r => r.Post(It.IsAny<OrderEntity>()), Times.Once);
        _orderItemServiceMock.Verify(s => s.Post(It.IsAny<OrderItemEntity>()), Times.Exactly(2));
    }

    [Fact]
    public async Task Post_ShouldThrowException_WhenRepositoryFails()
    {
        var order = new OrderEntity { Items = new List<OrderItemEntity>() };
        _repositoryMock.Setup(r => r.Post(It.IsAny<OrderEntity>())).ThrowsAsync(new Exception("Database Error"));

         await Assert.ThrowsAsync<Exception>(() => _service.Post(order));

        _repositoryMock.Verify(r => r.Post(It.IsAny<OrderEntity>()), Times.Once);
    }

    [Fact]
    public async Task Post_ShouldThrowException_WhenOrderItemServiceFails()
    {
        var order = new OrderEntity
        {
            Items = new List<OrderItemEntity> { new OrderItemEntity { Description = "Item 1", Quantity = 10 } }
        };

        _repositoryMock.Setup(r => r.Post(It.IsAny<OrderEntity>())).ReturnsAsync(order);
        _orderItemServiceMock.Setup(s => s.Post(It.IsAny<OrderItemEntity>())).ThrowsAsync(new Exception("Item Service Error"));

        await Assert.ThrowsAsync<Exception>(() => _service.Post(order));

        _repositoryMock.Verify(r => r.Post(It.IsAny<OrderEntity>()), Times.Once);
        _orderItemServiceMock.Verify(s => s.Post(It.IsAny<OrderItemEntity>()), Times.Once);
    }

    [Fact]
    public async Task UpdateStatusAsync_ShouldCallRepository_WhenValidOrderId()
    {
        var orderId = Guid.Parse("550e8400-e29b-41d4-a716-446655440009");

        _repositoryMock.Setup(r => r.UpdateSatatusAsync(orderId)).Returns(Task.CompletedTask);

        await _service.UpdateStatusAsync(orderId);

        _repositoryMock.Verify(r => r.UpdateSatatusAsync(orderId), Times.Once);
    }

    [Fact]
    public async Task UpdateStatusAsync_ShouldThrowException_WhenRepositoryFails()
    {
        var orderId = Guid.Parse("550e8400-e29b-41d4-a716-446655440009");
        _repositoryMock.Setup(r => r.UpdateSatatusAsync(orderId)).ThrowsAsync(new Exception("Update Failed"));

         await Assert.ThrowsAsync<Exception>(() => _service.UpdateStatusAsync(orderId));

        _repositoryMock.Verify(r => r.UpdateSatatusAsync(orderId), Times.Once);
    }
}