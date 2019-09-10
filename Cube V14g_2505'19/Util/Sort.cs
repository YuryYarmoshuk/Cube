using Cube_V11.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cube_V11.Utilit
{
    class Sort
    {
        private static void Swap(TotalStrainModel x,TotalStrainModel y)
        {
            var t = x;
            x = y;
            y = t;
        }

        private static void Swap(TotalStressModel x, TotalStressModel y)
        {
            var t = x;
            x = y;
            y = t;
        }

        static int Partition(List<TotalStrainModel> array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i].ENERGY < array[maxIndex].ENERGY)
                {
                    pivot++;
                    Swap(array[pivot], array[i]);
                }
            }

            pivot++;
            Swap(array[pivot], array[maxIndex]);
            return pivot;
        }

        static List<TotalStrainModel> QuickSort(List<TotalStrainModel> array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        static int Partition(List<TotalStressModel> array, int minIndex, int maxIndex)
        {
            var pivot = minIndex - 1;
            for (var i = minIndex; i < maxIndex; i++)
            {
                if (array[i].VON < array[maxIndex].VON)
                {
                    pivot++;
                    Swap(array[pivot], array[i]);
                }
            }

            pivot++;
            Swap(array[pivot], array[maxIndex]);
            return pivot;
        }

        static List<TotalStressModel> QuickSort(List<TotalStressModel> array, int minIndex, int maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return array;
            }

            var pivotIndex = Partition(array, minIndex, maxIndex);
            QuickSort(array, minIndex, pivotIndex - 1);
            QuickSort(array, pivotIndex + 1, maxIndex);

            return array;
        }

        public static List<TotalStrainModel> QuickSort(List<TotalStrainModel> nodeList)
        {
            return QuickSort(nodeList, 0, nodeList.Count - 1);
        }

        public static List<TotalStressModel> QuickSort(List<TotalStressModel> nodeList)
        {
            return QuickSort(nodeList, 0, nodeList.Count - 1);
        }
    }
}
