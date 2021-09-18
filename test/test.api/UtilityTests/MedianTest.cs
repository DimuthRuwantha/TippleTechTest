using System.Collections.Generic;
using Services.utils;
using Xunit;

namespace Test.api.UtilityTests
{
    public class MedianTest
    {
        [Fact]
        public void GetMedianForEvenNumbers()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6 };

            var median = Median.GetMedian(list);
            
            Assert.True(median.Equals(3.5));
        }

        [Fact]
        public void GetMedianForOddNumberOfItems()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };

            var median = Median.GetMedian(list);
            
            Assert.True(median.Equals(4));
        }
    }
}