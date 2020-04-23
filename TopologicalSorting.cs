using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TopologicalSorting
{
    public class TopologicalSorting
    {
        public int[] SortFromFile(string filePath)
        {
            string fullInput;
            using (StreamReader stream = new StreamReader(filePath))
            {
                fullInput = stream.ReadToEnd();
            }

            string[] inputLines = fullInput.Trim().Split('\n');

            int[,] matrix = new int[inputLines.Length, inputLines.Length];
            for (var i = 0; i < inputLines.Length; i++)
            {
                var line = inputLines[i];
                string[] words = line.Trim().Split(' ');

                for (var j = 0; j < words.Length; j++)
                {
                    matrix[i, j] = int.Parse(words[j].Trim());
                }
            }

            return Sort(matrix);
        }

        public int[] Sort(int[,] matrix)
        {
            if (matrix.GetLength(0) != matrix.GetLength(1))
            {
                throw new ArgumentException("Incorrect Input");
            }

            if (IsCycled(matrix))
            {
                throw new ArgumentException("Topological Sorting is impossible, graph is cycled");
            }

            Stack<int> answer = new Stack<int>();

            int size = matrix.GetLength(0);
            bool[] visited = new bool[size];

            for (int i = 0; i < size; i++)
            {
                if (!visited[i])
                {
                    SortDFS(i, matrix, visited, answer);
                }
            }

            return answer.ToArray();
        }

        private void SortDFS(int index, int[,] matrix, bool[] visited, Stack<int> answer)
        {
            visited[index] = true;

            int size = matrix.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                if (!visited[i] && matrix[index, i] != 0)
                {
                    SortDFS(i, matrix, visited, answer);
                }
            }

            answer.Push(index);
        }

        private bool IsCycled(int[,] matrix)
        {
            int size = matrix.GetLength(0);

            for (int i = 0; i < size; i++)
            {
                if (IsCycledDFS(i, i, matrix, new bool[size]))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsCycledDFS(int index, int start, int[,] matrix, bool[] visited)
        {
            visited[index] = true;

            int size = matrix.GetLength(0);
            for (int i = 0; i < size; i++)
            {
                if (matrix[index, i] != 0)
                {
                    if (i == start)
                    {
                        return true;
                    }

                    if (!visited[i] && IsCycledDFS(i, start, matrix, visited))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}