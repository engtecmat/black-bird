using System;
using System.Collections.Generic;

namespace BlackBird
{
    /// <summary>
    /// a util class
    /// </summary>
    public sealed class Utils
    {
        /// <summary>
        /// a random intance
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// generate a set of indices
        /// </summary>
        /// <param name="count">the number of indcies</param>
        /// <param name="maxValue">the maximum value</param>
        /// <returns></returns>
        public static List<int> GenerateIndices(int count, int maxValue)
        {
            return GenerateIndices(count, 0, maxValue);
        }

        /// <summary>
        /// generate a set of indices
        /// </summary>
        /// <param name="count">the number of indcies</param>
        /// <param name="minValue">the minimum value</param>
        /// <param name="maxValue">the maximum value</param>
        /// <returns></returns>
        public static List<int> GenerateIndices(int count, int minValue, int maxValue)
        {
            List<int> indices = new List<int>();

            while (indices.Count < Math.Min(count, maxValue))
            {
                int index = random.Next(minValue, maxValue);
                if (indices.Contains(index))
                {
                    continue;
                }
                indices.Add(index);
            }
            return indices;
        }
    }
}
