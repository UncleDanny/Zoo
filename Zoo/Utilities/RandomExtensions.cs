using System;

namespace Zoo.Utilities
{
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a value indicating if the randomly generated integer equals zero.
        /// </summary>
        /// <param name="maxValue">The exclusive upper bound of the random number that will be evaluated.</param>
        public static bool NextBool(this Random random, int maxValue) => random.Next(maxValue) == 0;
    }
}
