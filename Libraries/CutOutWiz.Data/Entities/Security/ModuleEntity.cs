﻿
namespace CutOutWiz.Data.Entities.Security
{
	public class ModuleEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? Status { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatedByContactId { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public int? UpdatedByContactId { get; set; }
		public string ObjectId { get; set; }
		public string Icon { get; set; }
		public decimal DisplayOrder { get; set; }

		public string PermissionNames { get; set; }
		//public IEnumerable<string> SelectedModulePermissons { get; set; } = new List<string>();
	}
}
