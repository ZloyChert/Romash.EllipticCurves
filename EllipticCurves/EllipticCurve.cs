using System;
using System.Collections.Generic;
using System.Linq;
using CSoDS.Lab3;

namespace EllipticCurves
{
    public class EllipticCurve
    {
        public int Module { get; }
        public Coefficients Coefficients { get; set; }

        public EllipticCurve(Coefficients coefficients, int module)
        {
            Module = module;
            var coef = GetAllPossibleCoefficients(Module);
            if (!coef.Contains(coefficients))
            {
                throw new ArgumentNullException("Wrong coefficients, they are not possible for elliptic curves");
            }
            Coefficients = coefficients;
        }

        public EllipticCurve(int module)
        {
            Module = module;
            var coeficients = GetAllPossibleCoefficients(Module);
            Coefficients = coeficients[coeficients.Count / 2];
        }

        public static List<Coefficients> GetAllPossibleCoefficients(int module)
        {
            List<Coefficients> results = new List<Coefficients>();
            for (int a = 1; a < module; a++)
            {
                for (int b = 1; b < module; b++)
                {
                    var coef = new Coefficients(a, b);
                    if (IsPossibleCoefficients(coef, module))
                    {
                        results.Add(coef);
                    }
                }
            }
            return results;
        }

        public List<EllipticPoint> GetElementsOfEllipticGroup()
        {
            List<EllipticPoint> results = new List<EllipticPoint>();
            foreach (var x in Enumerable.Range(0, Module))
            {
                int ySquare = ((int)Math.Pow(x, 3) + Coefficients.A * x + Coefficients.B) % Module;
                bool isSquare = Legendre(ySquare, Module) != -1;
                if (isSquare)
                {
                    var y = (int) TonelliShanks.ShanksSqrt(ySquare, Module);
                    results.Add(new EllipticPoint(x, y, Module, Coefficients));
                    if (y != 0)
                    {
                        results.Add(new EllipticPoint(x, Module - y, Module, Coefficients));

                    }
                }
            }
            return results;
        }

        private static bool IsPossibleCoefficients(Coefficients coefficients, int module)
        {
            return (4 * Math.Pow(coefficients.A, 3) + 27 * Math.Pow(coefficients.B, 2)) % module != 0;
        }

        private int Legendre(int a, int p)
        {
            if (a == 0)
            {
                return 0;
            }
            if (a == 1)
            {
                return 1;
            }
            int result;
            if (a % 2 == 0)
            {
                result = Legendre(a / 2, p);
                if (((p * p - 1) & 8) != 0)
                {
                    result = -result;
                }
            }
            else
            {
                result = Legendre(p % a, a);
                if (((a - 1) * (p - 1) & 4) != 0)
                {
                    result = -result;
                }
            }
            return result;
        }
    }
}
