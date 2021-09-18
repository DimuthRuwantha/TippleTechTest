using System;
using System.Collections.Generic;
using Common.Models.Response;
using Microsoft.VisualBasic;
using Moq;
using Services;
using Services.Context;
using Test.api.Helpers;
using Xunit;

namespace Test.api.ServiceTests
{
    public class CocktailsByIngredientsServiceTest
    {
        
        private readonly Mock<ICocktailDataService> _cocktailDataServiceMock;
        
        public CocktailsByIngredientsServiceTest()
        {
            _cocktailDataServiceMock = new Mock<ICocktailDataService>();
        }

        [Fact]
        public async void SearchCockTailReturnSuccess()
        {
            var dummyDrink = TestHelper.CreateDrink(1);
            var dummyDrinkList = TestHelper.CreateDrinksList(new List<Drink>()
            {
                dummyDrink
            });
            
            _cocktailDataServiceMock.Setup(s => s.GetCocktailsByIngredient("Gin"))
                .ReturnsAsync(dummyDrinkList);
            _cocktailDataServiceMock.Setup(s => s.GetCocktailDrinkById(1))
                .ReturnsAsync(dummyDrink);

            var cocktailService = new CocktailService(_cocktailDataServiceMock.Object);

            var cockTails = await cocktailService.SearchCockTail("Gin");
            
            Assert.NotNull(cockTails);
            Assert.True(cockTails.Cocktails.Count == dummyDrinkList.Drinks.Count);
            Assert.True(cockTails.meta.count == dummyDrinkList.Drinks.Count);
            Assert.Equal(2,cockTails.meta.medianIngredientCount);
        }

        [Fact]
        public async void SearchCockTailReturnNullFromDataService()
        {
            _cocktailDataServiceMock.Setup(s => s.GetCocktailsByIngredient("Gin"))
                .ReturnsAsync((DrinksList)null);
            
            var cocktailService = new CocktailService(_cocktailDataServiceMock.Object);
            var cockTails = await cocktailService.SearchCockTail("Gin");

            Assert.Null(cockTails);
        }
        
        [Fact]
        public async void SearchCockTailReturnEmptyDrinkListFromDataService()
        {
            _cocktailDataServiceMock.Setup(s => s.GetCocktailsByIngredient("Gin"))
                .ReturnsAsync(new DrinksList());
            
            var cocktailService = new CocktailService(_cocktailDataServiceMock.Object);
            var cockTails = await cocktailService.SearchCockTail("Gin");

            Assert.Null(cockTails);
        }
        
        [Fact]
        public async void SearchCockTailReturnInitializedEmptyDrinkListFromDataService()
        {
            _cocktailDataServiceMock.Setup(s => s.GetCocktailsByIngredient("Gin"))
                .ReturnsAsync(new DrinksList()
                {
                    Drinks = new List<Drink>()
                });
            var cocktailService = new CocktailService(_cocktailDataServiceMock.Object);

            var cockTails = await cocktailService.SearchCockTail("Gin");
            Assert.Null(cockTails);
        }

        [Fact]
        public async void SearchCocktailThrowsException()
        {
            _cocktailDataServiceMock.Setup(s => s.GetCocktailsByIngredient("Gin"))
                .Throws(new Exception());

            var cocktailService = new CocktailService(_cocktailDataServiceMock.Object);

            await Assert.ThrowsAsync<Exception>( () => cocktailService.SearchCockTail("Gin"));
        }
    }
}