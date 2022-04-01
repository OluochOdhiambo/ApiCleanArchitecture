using System;
using Xunit;
using Api.ApplicationCore.DTOs;

namespace Api.UnitTests.DTOs
{
    public class BookResquestTests : BaseTest
    {
        [Theory]
        [InlineData("Test", 2, "Summary", true, 0.02, 0)]

        public void ValidateModel_CreateBookRequest_ReturnsCorrectNumberOfErrors(string title, int pages, string summary, bool inStock, double price, int numberExpectedErrors)
        {
            var request = new CreateBookRequest
            {
                Title = title,
                Pages = pages,
                Summary = summary,
                InStock = inStock,
                Price = price
            };

        var errorList = ValidateModel(request);

        Assert.Equal(numberExpectedErrors, errorList.Count);
        }

        [Theory]
        [InlineData("Test", 2, "Summary", true, 0.02, 0)]

        public void ValidateModel_UpdateBookRequest_ReturnsCorrectNumberOfErrors(string title, int pages, string summary, bool inStock, double price, int numberExpectedErrors)
        {
            var request = new UpdateBookRequest
            {
                Title = title,
                Pages = pages,
                Summary = summary,
                InStock = inStock,
                Price = price
            };

            var errorList = ValidateModel(request);

            Assert.Equal(numberExpectedErrors, errorList.Count);
        }
    }
}
