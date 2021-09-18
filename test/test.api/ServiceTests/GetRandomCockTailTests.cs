using Common.Models.DTO;
using Common.Models.Response;
using Moq;
using Services;
using Services.Context;
using Test.api.Helpers;
using Xunit;

namespace Test.api.ServiceTests
{
    public class GetRandomCockTailTests
    {
        private readonly Mock<ICocktailDataService> _cocktailDataServiceMock;
        
        public GetRandomCockTailTests()
        {
            _cocktailDataServiceMock = new Mock<ICocktailDataService>();
        }

        [Fact]
        public async void GetRandomCockTailSuccess()
        {
            var dummyDrink = TestHelper.CreateDrink(1);
            _cocktailDataServiceMock.Setup(s => s.GetRandomCockTail())
                .ReturnsAsync(dummyDrink);
            var cocktailService = new CocktailService(_cocktailDataServiceMock.Object);

            var randomCockTail = await cocktailService.GetRandomCockTail();
            
            Assert.True(int.Parse(dummyDrink.idDrink) == randomCockTail.Id);
            Assert.Equal(dummyDrink.strDrink, randomCockTail.Name );
            Assert.Equal(2, randomCockTail.Ingredients.Count);
        }
        
        [Fact]
        public async void GetRandomCockTailReturnsNull()
        {
            var dummyDrink = TestHelper.CreateDrink(1);
            _cocktailDataServiceMock.Setup(s => s.GetRandomCockTail())
                .ReturnsAsync((Drink)null);
            var cocktailService = new CocktailService(_cocktailDataServiceMock.Object);

            var randomCockTail = await cocktailService.GetRandomCockTail();
            
            Assert.Null(randomCockTail);
        }

    }
}