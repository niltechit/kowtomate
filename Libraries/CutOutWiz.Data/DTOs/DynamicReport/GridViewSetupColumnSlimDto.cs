namespace CutOutWiz.Data.DTOs.DynamicReport
{
	public class GridViewSetupColumnSlimDto
	{
		public int ColoumnId { get; set; }
		public int DisplayOrder { get; set; }
		public double Width { get; set; }
		//public string WidthPx => $"{Width}px";
	}
}
