using PathfindingLib.API.Terrain;
using System;

namespace PathfindingLib.Terrain
{
	public class Point3D : IPoint3D
	{
		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public Point3D()
		{

		}

		public Point3D(double x, double y, double z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public static Point3D operator +(Point3D a, Point3D b)
		{
			return new Point3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		public static Point3D operator +(Point3D a, double val)
		{
			return new Point3D(a.X + val, a.Y + val, a.Z + val);
		}

		public static Point3D operator *(Point3D a, double val)
		{
			return new Point3D(a.X * val, a.Y * val, a.Z * val);
		}

		public static Point3D operator /(Point3D a, double val)
		{
			return new Point3D(a.X / val, a.Y / val, a.Z / val);
		}
	}
}
