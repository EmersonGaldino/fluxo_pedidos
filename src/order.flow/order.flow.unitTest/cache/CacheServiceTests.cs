using Microsoft.Extensions.Caching.Memory;
using Moq;
using order.flow.utils.cache.memory;
using Xunit;
using Assert = Xunit.Assert;

namespace order.flow.unitTest.cache;

public class LocalCacheServiceTest
{
    private readonly Mock<IMemoryCache> _cacheMock;
    private readonly LocalCacheService _cacheService;

    public LocalCacheServiceTest()
    {
        _cacheMock = new Mock<IMemoryCache>();
        _cacheService = new LocalCacheService(new MemoryCache(new MemoryCacheOptions()));
    }

    [Fact]
    public void Set_ShouldStoreValueInCache()
    {
        var key = "orderSendToServiceKey";
        var value = "{orderid:'12345678900',itens:[{'description':'breja boa', quantity:100}]}";

        _cacheService.Set(key, value);

        var cachedValue = _cacheService.Get<string>(key);
        Assert.Equal(value, cachedValue);
    }

    [Fact]
    public void Get_ShouldReturnStoredValue()
    {
        var key = "orderSendToServiceKey";
        var expectedValue = 100;

        _cacheService.Set(key, expectedValue);

        var actualValue = _cacheService.Get<int>(key);

        Assert.Equal(expectedValue, actualValue);
    }

    [Fact]
    public void Get_ShouldReturnDefault_WhenKeyNotFound()
    {
        var key = "nonExistingKey";

        var result = _cacheService.Get<string>(key);

        Assert.Null(result);
    }

    [Fact]
    public void Remove_ShouldDeleteValueFromCache()
    {
        var key = "orderSendToServiceKey";
        var value = "{}";

        _cacheService.Set(key, value);

        _cacheService.Remove(key);
        var result = _cacheService.Get<string>(key);

        Assert.Null(result);
    }
}