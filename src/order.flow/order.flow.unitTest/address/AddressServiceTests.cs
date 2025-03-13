using Moq;
using order.flow.domain.entity.address;
using order.flow.domain.repository.Interface.address;
using order.flow.domain.service.address;

namespace order.flow.unitTest.address;

public class AddressServiceTests
{
    private readonly Mock<IAddressRepository> _repositoryMock;
    private readonly AddressService _service;

    public AddressServiceTests()
    {
        _repositoryMock = new Mock<IAddressRepository>();
        _service = new AddressService(_repositoryMock.Object);
    }

    [Fact]
    public async Task ShouldReturnAddressList_WhenDataExists()
    {
        var resaleId = Guid.NewGuid().ToString();
        var addressEntities = new List<AddressEntity>
        {
            new()  { Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440000") },
            new()  { Id = Guid.Parse("550e8400-e29b-41d4-a716-446655440001") }
        };

        _repositoryMock.Setup(r => r.GetByResaleId(resaleId)).ReturnsAsync(addressEntities);

        var result = await _service.GetByResaleId(resaleId);

        Assert.NotNull(result);
        _repositoryMock.Verify(r => r.GetByResaleId(resaleId), Times.Once);
    }

    [Fact]
    public async Task ShouldReturnEmptyList_WhenNoDataExists()
    {
        var resaleId = "550e8400-e29b-41d4-a716-446655440000";
        _repositoryMock.Setup(r => r.GetByResaleId(resaleId)).ReturnsAsync(new List<AddressEntity>());

        var result = await _service.GetByResaleId(resaleId);

        Assert.NotNull(result);
        _repositoryMock.Verify(r => r.GetByResaleId(resaleId), Times.Once);
    }

    [Fact]
    public async Task ShouldThrowException_WhenRepositoryFails()
    {
        var resaleId = Guid.NewGuid().ToString();
        _repositoryMock.Setup(r => r.GetByResaleId(resaleId)).ThrowsAsync(new Exception("Database Error"));

        Assert.ThrowsAsync<Exception>(() => _service.GetByResaleId(resaleId));
        _repositoryMock.Verify(r => r.GetByResaleId(resaleId), Times.Once);
    }
}
