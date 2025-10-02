using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fzzzt_game
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
            List<int> indices = new List<int>();

            while (indices.Count < Math.Min(count, maxValue))
            {
                int index = random.Next(0, maxValue);
                if (indices.Contains(index)) {
                    continue; 
                }
                indices.Add(index);
            }
            return indices;
        }
    }
}
