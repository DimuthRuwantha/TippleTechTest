using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using api.Controllers;
using Common.Models.Response;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services.Context;
using Test.api.Helpers;
using Xunit;

namespace Test.api
{
    public class RandomCocktailTests
    {
        private readonly Mock<ICocktailDataService> _cocktailDataServiceMock;
        private readonly Mock<ICockTailService> _cockTailService;

        public RandomCocktailTests()
        {
            _cocktailDataServiceMock = new Mock<ICocktailDataService>();
            _cockTailService = new Mock<ICockTailService>();
        }

        [Fact]
        public async void GetRandomCocktailSuccess()
        {
            _cocktailDataServiceMock.Setup(s => s.GetRandomCockTail())
                .ReturnsAsync(new Drink());

            _cockTailService.Setup(s => s.GetRandomCockTail())
                .ReturnsAsync(TestHelper.CreatACockTail());

            var boozeController = new BoozeController(_cockTailService.Object);
            var objectResult = await boozeController.GetRandom() as OkObjectResult;
            Assert.NotNull(objectResult);
            
            var drink = objectResult?.Value as Cocktail;
            Assert.NotNull(drink);
        }

        [Fact]
        public async void GetRandomCocktailEmpty()
        {
            _cockTailService.Setup(s => s.GetRandomCockTail())
                .ReturnsAsync((Cocktail)null);

            var boozeController = new BoozeController(_cockTailService.Object);
            var noContentResult = await boozeController.GetRandom() as NoContentResult;
            Assert.NotNull(noContentResult);
            
            var noContentStatusCode = noContentResult.StatusCode;
            Assert.True(noContentStatusCode == 204);
        }
       
        [Fact]
        public async void GetRandomThrowingErrorReturnInternalServerError()
        {
            _cockTailService.Setup(s => s.GetRandomCockTail())
                .Throws(new Exception());
            var boozeController = new BoozeController(_cockTailService.Object);
            var statusCodeResult = await boozeController.GetRandom() as StatusCodeResult;
            
            Assert.NotNull(statusCodeResult);
            Assert.True(statusCodeResult.StatusCode == 500);
        }
        
        [Fact]
        public async void GetRandomThrowingHttpErrorReturnStatusCode()
        {
            _cockTailService.Setup(s => s.GetRandomCockTail())
                .Throws(new HttpRequestException()
                {
                    HResult = 404
                });
            var boozeController = new BoozeController(_cockTailService.Object);
            var statusCodeResult = await boozeController.GetRandom() as StatusCodeResult;
            
            Assert.NotNull(statusCodeResult);
            Assert.True(statusCodeResult.StatusCode == 404);
        }
    }
}