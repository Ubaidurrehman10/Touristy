using Moq;
using Shouldly;
using Touristy.Api.Controllers;

namespace Touristy.Api.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void ShouldReturnForcastResults()
        {
            var controller = new WeatherForecastController();
            var result = controller.Get();

            result.Count().ShouldBeGreaterThan(1);
            result.ShouldNotBeNull();
        }
    }
}
