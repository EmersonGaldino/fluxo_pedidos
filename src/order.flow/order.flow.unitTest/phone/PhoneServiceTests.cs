using Moq;
using order.flow.domain.entity.phone;
using order.flow.domain.repository.Interface.phone;
using order.flow.domain.service.phone;
using Xunit;
using Assert = Xunit.Assert;

namespace order.flow.unitTest.phone;

public class PhoneServiceTests
{
    private readonly Mock<IPhoneRepository> _repositoryMock;
    private readonly PhoneService _service;

    public PhoneServiceTests()
    {
        _repositoryMock = new Mock<IPhoneRepository>();
        _service = new PhoneService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetByResaleId_ShouldReturnPhones_WhenSuccessful()
    {
        var resaleId = "550e8400-e29b-41d4-a716-446655440000";
        var phones = new List<PhoneEntity>
        {
            new() { Id = Guid.NewGuid(), Phone = "(15)988763733"},
            new() { Id = Guid.NewGuid(), Phone = "(52)877631266"}
        };

        _repositoryMock.Setup(r => r.GetByResaleId(resaleId)).ReturnsAsync(phones);

        var result = await _service.GetByResaleId(resaleId);

        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal(phones[0].Phone, result[0].Phone);
        Assert.Equal(phones[1].Phone, result[1].Phone);

        _repositoryMock.Verify(r => r.GetByResaleId(resaleId), Times.Once);
    }

    [Fact]
    public async Task GetByResaleId_ShouldReturnEmptyList_WhenNoPhonesFound()
    {
        var resaleId = "550e8400-e29b-41d4-a716-446655440000";
        _repositoryMock.Setup(r => r.GetByResaleId(resaleId)).ReturnsAsync(new List<PhoneEntity>());

        var result = await _service.GetByResaleId(resaleId);

        Assert.NotNull(result);
        Assert.Empty(result);

        _repositoryMock.Verify(r => r.GetByResaleId(resaleId), Times.Once);
    }

    [Fact]
    public async Task GetByResaleId_ShouldThrowException_WhenRepositoryFails()
    {
        var resaleId = "550e8400-e29b-41d4-a716-446655440000";
        _repositoryMock.Setup(r => r.GetByResaleId(resaleId)).ThrowsAsync(new Exception("Database error"));

        await Assert.ThrowsAsync<Exception>(() => _service.GetByResaleId(resaleId));

        _repositoryMock.Verify(r => r.GetByResaleId(resaleId), Times.Once);
    }
}