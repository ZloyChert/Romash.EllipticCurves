using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EllipticCurves
{
    public class EllipticPoint : IEquatable<EllipticPoint>
    {
        public int X { get; }
        public int Y { get; }
        public int Module { get; }
        Coefficients Coefficients { get; }

        public EllipticPoint(int x, int y, int module, Coefficients coefficients)
        {
            Coefficients = coefficients;
            Module = module;
            X = x;
            Y = y;
        }

        public EllipticPoint Minus()
        {
            return new EllipticPoint(X, AddOnValue(- Y, Module), Module, Coefficients);
        }

        public EllipticPoint Multiply(int mult)
        {
            EllipticPoint result = new EllipticPoint(X, Y, Module, Coefficients);
            EllipticPoint point = new EllipticPoint(X, Y, Module, Coefficients);
            for(int i = 1; i < mult; i++)
            {
                result = Add(result, point);
            }
            return result;
        }

        public int GetPointOrder()
        {
            var m = (int) Math.Pow(Module + 1 + 2 * Math.Pow(Module, 0.5), 0.5) + 1;
            var table = new List<EllipticPoint> {new EllipticPoint(X, Y, Module + 1, Coefficients)};
            for (int i = 1; i <= m; i++)
            {
                table.Add(Multiply(i));
            }
            var alpha = Multiply(m).Minus();
            for (int i = 1; i <= m; i++)
            {
                var elPoint = alpha.Multiply(i);
                var indexInTable = table.IndexOf(elPoint);
                if (indexInTable > 0)
                {
                    return m * i + indexInTable;
                }
            }
            return 0;
        }

        private static int AddOnValue(int value, int module)
        {
            while (value < 0)
            {
                value += module;
            }
            return value % module;
        }

        private static int OnModules(int valueUp, int valueDown, int module)
        {
            if (valueDown < 0)
            {
                valueUp = -valueUp;
                valueDown = -valueDown;
            }
            valueUp = AddOnValue(valueUp, module);
            for (int i = 0; i < module; i++)
            {
                if (valueDown * i % module == valueUp)
                {
                    return i;
                }
            }
            return 0;
        }

        public static EllipticPoint Add(EllipticPoint a, EllipticPoint b)
        {
            int slope;
            if (!a.Equals(b))
            {
                slope = OnModules(b.Y - a.Y, b.X - a.X, a.Module);
            }
            else
            {
                slope = OnModules(3 * (int)Math.Pow(a.X, 2) + a.Coefficients.A, 2 * a.Y, a.Module);
            }
            int x = (int)Math.Pow(slope, 2) - a.X - b.X;
            x = AddOnValue(x, a.Module);
            int y = slope * (a.X - x) - a.Y;
            y = AddOnValue(y, a.Module);
            return new EllipticPoint(x, y, a.Module, a.Coefficients);
        }

        public bool Equals(EllipticPoint other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Module == other.Module;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((EllipticPoint) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = (hashCode * 397) ^ Y;
                hashCode = (hashCode * 397) ^ Module;
                return hashCode;
            }
        }
    }
}
