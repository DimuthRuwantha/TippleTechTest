using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.utils
{
    public class Median
    {
        public static double GetMedian(List<int> list)
        {
            if (list == null || !list.Any()) return 0;
            
            list.Sort();
            var bitShiftedValue = list.Count >> 1;

            if (list.Count % 2 != 0) return list.ElementAt(bitShiftedValue);
            
            var total = list.ElementAt(bitShiftedValue - 1) + list.ElementAt(bitShiftedValue);
            return (double)total / 2;

        }
    }
}