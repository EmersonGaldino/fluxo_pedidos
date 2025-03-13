using Moq;
using order.flow.domain.entity.address;
using order.flow.domain.entity.phone;
using order.flow.domain.entity.resale;
using order.flow.domain.Interface.address;
using order.flow.domain.Interface.phone;
using order.flow.domain.repository.Interface.resale;
using order.flow.domain.service.resale;
using Xunit;
using Assert = Xunit.Assert;

namespace order.flow.unitTest.resale;

public class ResaleServiceTests
{
    private readonly Mock<IResaleRepository> _repositoryMock;
    private readonly Mock<IPhoneService> _phoneServiceMock;
    private readonly Mock<IAddressService> _addressServiceMock;
    private readonly ResaleService _service;

    public ResaleServiceTests()
    {
        _repositoryMock = new Mock<IResaleRepository>();
        _phoneServiceMock = new Mock<IPhoneService>();
        _addressServiceMock = new Mock<IAddressService>();

        _service = new ResaleService(
            _repositoryMock.Object,
            _phoneServiceMock.Object,
            _addressServiceMock.Object
        );
    }

    [Fact]
    public async Task ShouldReturnResaleList_WhenDataExists()
    {
        var resaleId = Guid.NewGuid();
        var resaleEntities = new List<ResaleEntity>
        {
            new ResaleEntity { Id = resaleId }
        };

        _repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(resaleEntities);
        _phoneServiceMock.Setup(p => p.GetByResaleId(resaleId.ToString()))
            .ReturnsAsync(new List<PhoneEntity> { new PhoneEntity { Phone = "(11) 988624571" } });
        _addressServiceMock.Setup(a => a.GetByResaleId(resaleId.ToString()))
            .ReturnsAsync(new List<AddressEntity> { new AddressEntity { Address = "Rua Treze de Marco" } });

        var result = await _service.GetAll();

        Assert.NotNull(result);
        Assert.Single(result);
        Assert.NotEmpty(result[0].Phones);
        Assert.NotEmpty(result[0].AddressDelevery);
    }

    [Fact]
    public async Task ShouldReturnEmptyList_WhenNoDataExists()
    {
        _repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(new List<ResaleEntity>());

        var result = await _service.GetAll();

        Assert.NotNull(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task ShouldThrowException_WhenRepositoryFails()
    {
        _repositoryMock.Setup(r => r.GetAll()).ThrowsAsync(new Exception("Database Error"));

        await Assert.ThrowsAsync<Exception>(() => _service.GetAll());
    }

    [Fact]
    public async Task ShouldThrowException_WhenPhoneServiceFails()
    {
        var resaleId = Guid.NewGuid();
        var resaleEntities = new List<ResaleEntity> { new ResaleEntity { Id = resaleId } };

        _repositoryMock.Setup(r => r.GetAll()).ReturnsAsync(resaleEntities);
        _phoneServiceMock.Setup(p => p.GetByResaleId(resaleId.ToString()))
            .ThrowsAsync(new Exception("Phone Service Error"));

        await Assert.ThrowsAsync<Exception>(() => _service.GetAll());
    }

    [Fact]
    public async Task ShouldReturnEntity_WhenPostIsSuccessful()
    {
        var resaleEntity = new ResaleEntity { Id = Guid.NewGuid() };
        _repositoryMock.Setup(r => r.Post(resaleEntity)).ReturnsAsync(resaleEntity);

        var result = await _service.Post(resaleEntity);

        Assert.NotNull(result);
        Assert.Equal(resaleEntity.Id, result.Id);
    }

    [Fact]
    public async Task ShouldThrowException_WhenPostFails()
    {
        var resaleEntity = new ResaleEntity { Id = Guid.NewGuid() };
        _repositoryMock.Setup(r => r.Post(resaleEntity)).ThrowsAsync(new Exception("Insert Error"));

        await Assert.ThrowsAsync<Exception>(() => _service.Post(resaleEntity));
    }
}
