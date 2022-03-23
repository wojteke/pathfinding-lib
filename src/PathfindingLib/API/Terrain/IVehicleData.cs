namespace PathfindingLib.API.Terrain
{
	public interface IVehicleData
	{
		double Width { get; set; }

		double Length { get; set; }

		double ApproachAngle { get; set; }

		double ExitAngle { get; set; }

		double LeftSwingAngle { get; set; }

		double RightSwingAngle { get; set; }
	}
}
