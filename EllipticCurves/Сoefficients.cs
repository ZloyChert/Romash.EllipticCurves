using System;

namespace EllipticCurves
{
    public class Coefficients : IEquatable<Coefficients>
    {
        public Coefficients(int a, int b)
        {
            A = a;
            B = b;
        }

        public int A { get; }
        public int B { get; }


        public bool Equals(Coefficients other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return A.Equals(other.A) && B.Equals(other.B);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Coefficients) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (A.GetHashCode() * 397) ^ B.GetHashCode();
            }
        }
    }
}
