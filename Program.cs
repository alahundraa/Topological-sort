using System;
using System.Linq;

namespace TopologicalSorting
{
    class Program
    {
        public static void Main(string[] args)
        {
            RunSort(new int[,]
            {
                {0, 1, 0},
                {0, 0, 1},
                {1, 0, 0},
            });

            var answer1 = RunSort(new int[,]
            {
                {0, 1, 1, 0, 0},
                {0, 0, 0, 1, 0},
                {0, 0, 0, 1, 0},
                {0, 0, 0, 0, 1},
                {0, 0, 0, 0, 0},
            });

            var answer2 = new TopologicalSorting().SortFromFile("input.txt");
            Console.WriteLine($"Совпали ли ответы: {answer1.SequenceEqual(answer2)}");
        }

        public static int[] RunSort(int[,] matrix)
        {
            int size1 = matrix.GetLength(0);
            int size2 = matrix.GetLength(0);
            Console.WriteLine("Sorting graph:");
            for (int i = 0; i < size1; i++)
            {
                for (int j = 0; j < size2; j++)
                {
                    Console.Write($"{matrix[i, j]} ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Result is:");
            try
            {
                int[] sorted = new TopologicalSorting().Sort(matrix);
                foreach (var value in sorted)
                {
                    Console.Write($"{value} ");
                }

                Console.WriteLine();
                Console.WriteLine(new string('-', 50));
                return sorted;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}