using PathfindingLib.API.Terrain;
using PathfindingLib.Terrain;
using System;

namespace PathfindingLib.Helpers
{
	public static class GeometryHelper
	{
		public static Point3D Add(IPoint3D a, IPoint3D b)
		{
			return new Point3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static Point3D Add(IPoint3D a, double value)
		{
			return new Point3D(a.X + value, a.Y + value, a.Z + value);
		}

		public static Point3D AddOther(this IPoint3D p, IPoint3D other)
		{
			return Add(p, other);
		}

		public static Point3D AddValue(this IPoint3D p, double value)
		{
			return Add(p, value);
		}

		public static Point3D Subtract(IPoint3D a, IPoint3D b)
		{
			return new Point3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		public static Point3D Subtract(IPoint3D a, double value)
		{
			return new Point3D(a.X - value, a.Y - value, a.Z - value);
		}

		public static Point3D SubtractOther(this IPoint3D p, IPoint3D other)
		{
			return Subtract(p, other);
		}

		public static Point3D SubtractValue(this IPoint3D p, double value)
		{
			return Subtract(p, value);
		}

		public static Point3D Multiply(IPoint3D a, IPoint3D b)
		{
			return new Point3D(a.X * b.X, a.Y * b.Y, a.Z * b.Z);
		}

		public static Point3D Multiply(IPoint3D a, double value)
		{
			return new Point3D(a.X * value, a.Y * value, a.Z * value);
		}

		public static Point3D MultiplyOther(this IPoint3D p, IPoint3D other)
		{
			return Multiply(p, other);
		}

		public static Point3D MultiplyValue(this IPoint3D p, double value)
		{
			return Multiply(p, value);
		}

		public static Point3D Divide(IPoint3D a, IPoint3D b)
		{
			return new Point3D(a.X / b.X, a.Y / b.Y, a.Z / b.Z);
		}

		public static Point3D Divide(IPoint3D p, double value)
		{
			return new Point3D(p.X / value, p.Y / value, p.Z / value);
		}

		public static Point3D DivideOther(this IPoint3D p, IPoint3D other)
		{
			return Divide(p, other);
		}

		public static Point3D DivideValue(this IPoint3D p, double value)
		{
			return Divide(p, value);
		}

		public static double Distance(IPoint3D a, IPoint3D b)
		{
			return Math.Sqrt(
				(a.X - b.X) * (a.X - b.X) +
				(a.Y - b.Y) * (a.Y - b.Y) +
				(a.Z - b.Z) * (a.Z - b.Z)
				);
		}

		public static double DistanceTo(this IPoint3D a, IPoint3D other)
		{
			return Distance(a, other);
		}

		public static double Distance2D(IPoint3D a, IPoint3D b)
		{
			return Math.Sqrt(
				(a.X - b.X) * (a.X - b.X) +
				(a.Z - b.Z) * (a.Z - b.Z)
				);
		}

		public static double DistanceTo2D(this IPoint3D a, IPoint3D other)
		{
			return Distance2D(a, other);
		}

		public static double DetXZ(IPoint3D a, IPoint3D b, IPoint3D c)
		{
			return (b.X - a.X) * (c.Z - a.Z) - (c.X - a.X) * (b.Z - a.Z);
		}
	}
}
