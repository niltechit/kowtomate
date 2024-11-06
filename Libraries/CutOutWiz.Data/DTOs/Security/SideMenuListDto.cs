namespace CutOutWiz.Data.DTOs.Security
{
	public class SideMenuListDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Icon { get; set; }
		public string MenuUrl { get; set; }
		public int? ParentId { get; set; }
		public decimal DisplayOrder { get; set; }
	}
}
