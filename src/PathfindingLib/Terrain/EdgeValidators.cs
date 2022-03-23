using PathfindingLib.API.DataStructures.Graph;
using PathfindingLib.API.Terrain;
using PathfindingLib.Helpers;
using System;

namespace PathfindingLib.Terrain.EdgeValidators
{
	public static class EdgeValidators
	{
		public static bool AlwaysPass<T>(ITerrainData<T> terrainData, IVehicleData vehicleData, int startX, int startZ, int endX, int endZ) 
			where T : IPoint3D
		{
			return true;
		}

		public static bool ApproachAndExitAngle<T>(ITerrainData<T> terrainData, IVehicleData vehicleData, int startX, int startZ, int endX, int endZ) 
			where T : IPoint3D
		{
			var start = terrainData.Data[startX, startZ];
			var end = terrainData.Data[endX, endZ];
			var tanT = Math.Abs(start.Y - end.Y) / start.DistanceTo2D(end);
			if (start.Y <= end.Y)
			{
				return tanT <= Math.Tan(vehicleData.ApproachAngle);
			}
			else
			{
				return tanT <= Math.Tan(vehicleData.ExitAngle);
			}
		}
		public static bool LeftAndRightSwingAngle<T>(ITerrainData<T> terrainData, IVehicleData vehicleData, int startX, int startZ, int endX, int endZ) 
			where T : IPoint3D
		{
			var start = terrainData.Data[startX, startZ];
			var end = terrainData.Data[endX, endZ];

			// straight line
			if(startX == endX || startZ == endZ)
			{
				IPoint3D vSE12, vSE34, vSE = start.AddOther(end).DivideValue(2); ;
				if(startX == endX)
				{
					var v1 = terrainData.Data.GetValidArrayElement(startX - 1, startZ);
					var v2 = terrainData.Data.GetValidArrayElement(endX - 1, endZ);
					var v12 = v1.AddOther(v2).DivideValue(2);

					var v3 = terrainData.Data.GetValidArrayElement(startX + 1, startZ);
					var v4 = terrainData.Data.GetValidArrayElement(endX + 1, endZ);
					var v34 = v3.AddOther(v4).DivideValue(2);

					vSE12 = vSE.AddOther(v12).DivideValue(2);
					vSE34 = vSE.AddOther(v34).DivideValue(2);
				}
				else
				{
					var v1 = terrainData.Data.GetValidArrayElement(startX, startZ - 1);
					var v2 = terrainData.Data.GetValidArrayElement(endX, endZ - 1);
					var v12 = v1.AddOther(v2).DivideValue(2);

					var v3 = terrainData.Data.GetValidArrayElement(startX, startZ + 1);
					var v4 = terrainData.Data.GetValidArrayElement(endX, endZ + 1);
					var v34 = v3.AddOther(v4).DivideValue(2);

					vSE12 = vSE.AddOther(v12).DivideValue(2);
					vSE34 = vSE.AddOther(v34).DivideValue(2);
				}

				var tanT = Math.Abs(vSE12.Y - vSE34.Y) / vSE12.DistanceTo2D(vSE34);

				// v1 should be on the left
				if (GeometryHelper.DetXZ(start, end, vSE12) < 0)
				{
					var temp = vSE12;
					vSE12 = vSE34;
					vSE34 = temp;
				}

				if (vSE12.Y <= vSE34.Y)
				{
					return tanT <= Math.Tan(vehicleData.LeftSwingAngle);
				}
				else
				{
					return tanT <= Math.Tan(vehicleData.RightSwingAngle);
				}
			}
			// diagonal
			else
			{
				var v1 = terrainData.Data[startX, endZ];
				var v2 = terrainData.Data[endX, startZ];
				var tanT = Math.Abs(v1.Y - v2.Y) / v1.DistanceTo2D(v2);

				// v1 should be on the left
				if (GeometryHelper.DetXZ(start, end, v1) < 0)
				{
					var temp = v2;
					v2 = v1;
					v1 = temp;
				}

				if (v1.Y <= v2.Y)
				{
					return tanT <= Math.Tan(vehicleData.LeftSwingAngle);
				}
				else
				{
					return tanT <= Math.Tan(vehicleData.RightSwingAngle);
				}
			}

		}
	}
}
