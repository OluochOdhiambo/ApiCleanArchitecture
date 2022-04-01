using Api.ApplicationCore.Utils;
using Xunit;

namespace Api.UnitTests.Utils
{
    public class DateUtilTests
    {
        [Fact]
        public void GetCurrentDate_ReturnsCorrectDate()
        {
            var currentDate = DateUtil.GetCurrentDate();

            Assert.True(currentDate.Year >= 2022);
        }
    }
}

