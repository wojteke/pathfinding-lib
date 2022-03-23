using System;
using System.Collections.Generic;
using System.Text;

namespace PathfindingLib.Helpers
{
	public static class ArrayHelper
	{
		public static T GetValidArrayElement<T>(this T[,] array, int x, int z)
		{
			var sizeX = array.GetLength(0);
			var sizeZ = array.GetLength(1);
			if (z < 0)
				z = 0;
			if (x < 0)
				x = 0;
			if (z >= sizeZ)
				z = sizeZ - 1;
			if (x >= sizeX)
				x = sizeX - 1;
			return array[x, z];
		}
	}
}
