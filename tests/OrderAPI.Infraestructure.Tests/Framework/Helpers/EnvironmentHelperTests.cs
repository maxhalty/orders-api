using OrderAPI.Infraestructure.Framework.Exceptions;
using OrderAPI.Infraestructure.Framework.Helpers;

namespace OrderAPI.Infraestructure.Tests.Framework.Helpers;

public class EnvironmentHelperTests
{
    private readonly MockRepository _mockRepository;

    public EnvironmentHelperTests()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
    }

    [Fact]
    public void GetEnvironmentVariable_ValidVariable_GetNotNullOrEmptyValue()
    {
        string expectedValue = "mockVariableValue";
        Environment.SetEnvironmentVariable("MOCK_VARIABLE", expectedValue);
        string result = EnvironmentHelper.GetEnvironmentVariable("MOCK_VARIABLE");

        Assert.NotNull(result);
        Assert.Equal(expectedValue, result);
        _mockRepository.VerifyAll();
    }

    [Fact]
    public void GetEnvironmentVariable_NullData_ThrowEnvironmentVariableNotFoundException()
    {
        _ = Assert.Throws<EnvironmentVariableNotFoundException>(() => EnvironmentHelper.GetEnvironmentVariable("NOT_DEFINED_VARIABLE"));
        _mockRepository.VerifyAll();
    }
}
