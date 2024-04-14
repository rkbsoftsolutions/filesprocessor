using System;
using System.Collections.Generic;
using System.Text;

namespace FileProcessorUnitTest.ZigZagArray
{
   internal class ZigZagArray
    {
       public static int[] ZigZagArrayV1()
        {
            int[] arr = new int[] { 2, 6, 1, 4, 7, 3, 5,8,9,12,5,78 };
            
            Array.Sort(arr);
            var middleIndex = (arr.Length / 2);
            var maxIndex = arr.Length - 1;
            var middleIndexValue = arr[middleIndex];
            arr[middleIndex] = arr[maxIndex];
            arr[maxIndex] = middleIndexValue;
            var x = middleIndex;
            while (x <= maxIndex)
            {
                for (int i = middleIndex; i < maxIndex; i++)
                {
                    if (arr[i] < arr[i + 1])
                    {
                        var temp= arr[i + 1];
                        arr[i + 1]= arr[i];
                        arr[i] = temp;
                    }
                }
                x++;
            }

            Console.Write(String.Join(",", arr));

            return arr;
        }
    }
}
