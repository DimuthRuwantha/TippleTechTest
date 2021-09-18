using System;
using System.Collections.Generic;
using api.Controllers;
using Common.Models.Response;
using Interface;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Test.api.Helpers;
using Xunit;

namespace Test.api.EndPointTests
{
    public class SearchByIngredientTests
    {
        private readonly Mock<ICockTailService> _cockTailService;

        public SearchByIngredientTests()
        {
            _cockTailService = new Mock<ICockTailService>();
        }

        [Fact]
        public async void GetCockTailByIngredient()
        {
            _cockTailService.Setup(s => s.SearchCockTail("Orange juice"))
                .ReturnsAsync(TestHelper.CreateACocktailList());
            var boozeController = new BoozeController(_cockTailService.Object);
            var objectResult = await boozeController.GetIngredientSearch("Orange juice") as OkObjectResult;
            Assert.NotNull(objectResult);
            
            var cocktailList = objectResult?.Value as CocktailList;
            Assert.NotNull(cocktailList);
        }
        
        [Fact]
        public async void GetCockTailByIngredientReturnNull()
        {
            _cockTailService.Setup(s => s.SearchCockTail("Orange juice"))
                .ReturnsAsync((CocktailList)null);
            var boozeController = new BoozeController(_cockTailService.Object);
            var noContentResult = await boozeController.GetIngredientSearch("Orange juice") as NoContentResult;
            Assert.NotNull(noContentResult);
            
            var noContentStatusCode = noContentResult.StatusCode;
            Assert.True(noContentStatusCode == 204);
        }
        
        [Fact]
        public async void GetCockTailByIngredientReturnNullList()
        {
            _cockTailService.Setup(s => s.SearchCockTail("Orange juice"))
                .ReturnsAsync(new CocktailList());
            var boozeController = new BoozeController(_cockTailService.Object);
            var noContentResult = await boozeController.GetIngredientSearch("Orange juice") as NoContentResult;
            Assert.NotNull(noContentResult);
            
            var noContentStatusCode = noContentResult.StatusCode;
            Assert.True(noContentStatusCode == 204);
        }
        
        [Fact]
        public async void GetCockTailByIngredientReturnEmptyList()
        {
            _cockTailService.Setup(s => s.SearchCockTail("Orange juice"))
                .ReturnsAsync(new CocktailList()
                {
                    Cocktails = new List<Cocktail>()
                });
            var boozeController = new BoozeController(_cockTailService.Object);
            var noContentResult = await boozeController.GetIngredientSearch("Orange juice") as NoContentResult;
            Assert.NotNull(noContentResult);
            
            var noContentStatusCode = noContentResult.StatusCode;
            Assert.True(noContentStatusCode == 204);
        }
        
        [Fact]
        public async void GetCockTailByIngredientTrowingErrorReturnInternalServerError()
        {
            _cockTailService.Setup(s => s.SearchCockTail("Orange juice"))
                .Throws(new Exception());
            var boozeController = new BoozeController(_cockTailService.Object);
            var statusCodeResult = await boozeController.GetIngredientSearch("Orange juice") as StatusCodeResult;
            
            Assert.NotNull(statusCodeResult);
            Assert.True(statusCodeResult.StatusCode == 500);
        }
    }
}