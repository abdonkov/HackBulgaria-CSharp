using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeometryFigures
{
    class Vector
    {
        private double[] coords;
        public double this[int index]
        {
            get
            {
                return coords[index];
            }
            set
            {
                coords[index] = value;
            }
        }
        public int Dimensionality { get { return coords.Length; } }

        public Vector(params double[] coords)
        {
            this.coords = coords;
        }

        public Vector(Vector v)
        {
            coords = new double[v.coords.Length];
            for (int i = 0; i < v.coords.Length; i++)
            {
                coords[i] = v.coords[i];
            }
        }

        public double GetLength()
        {
            double squareSum = 0;
            foreach (var coord in coords)
            {
                squareSum += Math.Pow(coord, 2);
            }
            return Math.Sqrt(squareSum);
        }

        public override string ToString()
        {
            return string.Format("Vector[{0}]", string.Join(", ", coords));
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector)
            {
                Vector v = obj as Vector;

                if (coords.Length != v.coords.Length) return false;

                for (int i = 0; i < coords.Length; i++)
                {
                    if (coords[i] != v.coords[i]) return false;
                }

                return true;
            }
            else return false;
        }

        public static bool operator ==(Vector v1, Vector v2)
        {
            return object.Equals(v1, v2);
        }

        public static bool operator !=(Vector v1, Vector v2)
        {
            return !object.Equals(v1, v2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 7;
                foreach (var coord in coords)
                {
                    hash = hash * 3 + coord.GetHashCode();
                }
                return hash;
            }
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            if (v1.coords.Length != v2.coords.Length) throw new ArgumentException("Vectors must have same number of dimensions!");

            double[] newCoords = new double[v1.coords.Length];
            for (int i = 0; i < newCoords.Length; i++)
            {
                newCoords[i] = v1.coords[i] + v2.coords[i];
            }
            return new Vector(newCoords);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            if (v1.coords.Length != v2.coords.Length) throw new ArgumentException("Vectors must have same number of dimensions!");

            double[] newCoords = new double[v1.coords.Length];
            for (int i = 0; i < newCoords.Length; i++)
            {
                newCoords[i] = v1.coords[i] - v2.coords[i];
            }
            return new Vector(newCoords);
        }

        public static Vector operator *(Vector v1, Vector v2)
        {
            if (v1.coords.Length != v2.coords.Length) throw new ArgumentException("Vectors must have same number of dimensions!");

            double[] newCoords = new double[v1.coords.Length];
            for (int i = 0; i < newCoords.Length; i++)
            {
                newCoords[i] = v1.coords[i] * v2.coords[i];
            }
            return new Vector(newCoords);
        }

        public static Vector operator +(Vector v1, double scalar)
        {
            double[] newCoords = new double[v1.coords.Length];
            for (int i = 0; i < newCoords.Length; i++)
            {
                newCoords[i] = v1.coords[i] + scalar;
            }
            return new Vector(newCoords);
        }

        public static Vector operator +(double scalar, Vector v1)
        {
            double[] newCoords = new double[v1.coords.Length];
            for (int i = 0; i < newCoords.Length; i++)
            {
                newCoords[i] = scalar + v1.coords[i];
            }
            return new Vector(newCoords);
        }

        public static Vector operator -(Vector v1, double scalar)
        {
            double[] newCoords = new double[v1.coords.Length];
            for (int i = 0; i < newCoords.Length; i++)
            {
                newCoords[i] = v1.coords[i] - scalar;
            }
            return new Vector(newCoords);
        }

        public static Vector operator -(double scalar, Vector v1)
        {
            double[] newCoords = new double[v1.coords.Length];
            for (int i = 0; i < newCoords.Length; i++)
            {
                newCoords[i] = scalar - v1.coords[i];
            }
            return new Vector(newCoords);
        }

        public static Vector operator *(Vector v1, double scalar)
        {
            double[] newCoords = new double[v1.coords.Length];
            for (int i = 0; i < newCoords.Length; i++)
            {
                newCoords[i] = v1.coords[i] * scalar;
            }
            return new Vector(newCoords);
        }

        public static Vector operator *(double scalar, Vector v1)
        {
            double[] newCoords = new double[v1.coords.Length];
            for (int i = 0; i < newCoords.Length; i++)
            {
                newCoords[i] = scalar * v1.coords[i];
            }
            return new Vector(newCoords);
        }

        public static Vector operator /(Vector v1, double scalar)
        {
            double[] newCoords = new double[v1.coords.Length];
            for (int i = 0; i < newCoords.Length; i++)
            {
                newCoords[i] = v1.coords[i] / scalar;
            }
            return new Vector(newCoords);
        }
    }
}
