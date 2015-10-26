using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Genetic
{
    public static class Random
    {
        #region Fields
        private static readonly System.Random random;
        #endregion

        static Random()
        {
            random = new System.Random(Guid.NewGuid().GetHashCode());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool NextBool()
        {
            int value = random.Next();
            int bit = random.Next(0, 32);

            return (value & (1 << bit)) != 0;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Next(int min, int max)
        {
            return random.Next(min, max);
        }
    }
}
