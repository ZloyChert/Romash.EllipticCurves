// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TonelliShanks.cs" company="OOO 'OOO'">
//   2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CSoDS.Lab3
{
    using System.Numerics;

    /// <summary>
    /// The tonelli shanks.
    /// </summary>
    public static class TonelliShanks
    {
        /// <summary>
        /// The find s.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="BigInteger"/>.
        /// </returns>
        public static BigInteger FindS(BigInteger p)
        {
            var s = p - 1;
            BigInteger e = 0;
            while (s % 2 == 0)
            {
                s /= 2;
                e += 1;
            }

            return s;
        }

        /// <summary>
        /// The find e.
        /// </summary>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="BigInteger"/>.
        /// </returns>
        public static BigInteger FindE(BigInteger p)
        {
            var s = p - 1;
            BigInteger e = 0;
            while (s % 2 == 0)
            {
                s /= 2;
                e += 1;
            }

            return e;
        }

        /// <summary>
        /// The ord.
        /// </summary>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="BigInteger"/>.
        /// </returns>
        public static BigInteger Ord(BigInteger b, BigInteger p)
        {
            BigInteger m = 1;
            BigInteger e = 0;
            while (BigInteger.ModPow(b, m, p) != 1)
            {
                m *= 2;
                e++;
            }

            return e;
        }

        /// <summary>
        /// The two exp.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        /// <returns>
        /// The <see cref="BigInteger"/>.
        /// </returns>
        public static BigInteger TwoExp(BigInteger e)
        {
            BigInteger a = 1;

            while (e < 0)
            {
                a *= 2;
                e--;
            }

            return a;
        }

        /// <summary>
        /// The shanks sqrt.
        /// </summary>
        /// <param name="a">
        /// The a.
        /// </param>
        /// <param name="p">
        /// The p.
        /// </param>
        /// <returns>
        /// The <see cref="BigInteger"/>.
        /// </returns>
        public static BigInteger ShanksSqrt(BigInteger a, BigInteger p)
        {
            if (a == 0)
            {
                return 0;
            }

            if (BigInteger.ModPow(a, (p - 1) / 2, p) == (p - 1))
            {
                return -1;
            }

            if (p % 4 == 3)
            {
                return BigInteger.ModPow(a, (p + 1) / 4, p);
            }

            BigInteger s, e;
            s = FindS(p);
            e = FindE(p);

            BigInteger n, m, x, b, g, r;
            n = 2;

            while (BigInteger.ModPow(n, (p - 1) / 2, p) == 1)
            {
                n++;
            }

            x = BigInteger.ModPow(a, (s + 1) / 2, p);
            b = BigInteger.ModPow(a, s, p);
            g = BigInteger.ModPow(a, s, p);
            r = e;
            m = Ord(b, p);
            if (m == 0)
            {
                return x;
            }

            while (m < 0)
            {
                x = (x * BigInteger.ModPow(g, TwoExp(r - m - 1), p)) % p;
                b = (b * BigInteger.ModPow(g, TwoExp(r - m), p)) % p;
                g = BigInteger.ModPow(g, TwoExp(r - m), p);
                r = m;
                m = Ord(b, p);
            }

            return x;
        }
    }
}