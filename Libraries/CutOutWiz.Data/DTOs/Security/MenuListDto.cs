
namespace CutOutWiz.Data.DTOs.Security
{
	public class MenuListDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string ParentMenuName { get; set; }
		public string Icon { get; set; }
		public string MenuUrl { get; set; }
		public int? Status { get; set; }
		public DateTime CreatedDate { get; set; }
		public string ObjectId { get; set; }
		public decimal DisplayOrder { get; set; }
		public string PermissionNames { get; set; }
	}
}
