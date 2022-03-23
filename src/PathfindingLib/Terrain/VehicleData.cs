using PathfindingLib.API.Terrain;
using System;

namespace PathfindingLib.Terrain
{
	public class VehicleData : IVehicleData
	{
		public double Width { get; set; }
		public double Length { get; set; }
		public double ApproachAngle { get; set; }
		public double ExitAngle { get; set; }
		public double LeftSwingAngle { get; set; }
		public double RightSwingAngle { get; set; }
	}
}
