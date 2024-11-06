namespace CutOutWiz.Data.DTOs.Security
{
	public class ModulePermissionDto
	{
		public string ModuleObjectId { get; set; }
		public string ModuleName { get; set; }
		public string PermissionObjectId { get; set; }
		public string DisplayName { get; set; }
		public string PermissionValue { get; set; }
		public decimal DisplayOrder { get; set; }
	}
}
