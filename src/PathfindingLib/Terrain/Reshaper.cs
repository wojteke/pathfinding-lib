using PathfindingLib.API.Terrain;
using PathfindingLib.Helpers;

namespace PathfindingLib.Terrain
{
	public class Reshaper
	{
		public static T[,] Terrain3DBilinearInterpolation<T>(T[,] entryArray, int outputWidth, int outputHeight)
			where T : IPoint3D, new()
		{
			T Lerp(T a, T b, double value)
			{
				var val = b.SubtractOther(a).MultiplyValue(value).AddOther(a);
				return new T() { X = val.X, Y = val.Y, Z = val.Z };
			}

			var output = new T[outputWidth, outputHeight];
			var inputWidth = entryArray.GetLength(0);
			var inputHeight = entryArray.GetLength(1);

			for (int x = 0; x < outputWidth; x++)
			{
				var fractionX = (double)x * inputWidth / outputWidth;
				int integerX = (int)fractionX;
				fractionX -= integerX;

				for (int z = 0; z < outputHeight; z++)
				{
					var fractionZ = (double)z * inputHeight / outputHeight;
					int integerZ = (int)fractionZ;
					fractionZ -= integerZ;

					var x1 = entryArray.GetValidArrayElement(integerX, integerZ);
					var x2 = entryArray.GetValidArrayElement(integerX + 1, integerZ);
					var x3 = entryArray.GetValidArrayElement(integerX, integerZ + 1);
					var x4 = entryArray.GetValidArrayElement(integerX + 1, integerZ + 1);

					var z1 = Lerp(x1, x2, fractionX);
					var z2 = Lerp(x3, x4, fractionX);
					var value = Lerp(z1, z2, fractionZ);

					output[x, z] = value;
				}
			}
			return output;
		}
	}
}
