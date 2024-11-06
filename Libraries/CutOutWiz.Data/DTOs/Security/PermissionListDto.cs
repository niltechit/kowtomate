namespace CutOutWiz.Data.DTOs.Security
{
	public class PermissionListDto
	{
		public short Id { get; set; }
		public string DisplayName { get; set; }
		public string PermissionValue { get; set; }
		public decimal DisplayOrder { get; set; }
		public byte Status { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatedByContactId { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public int? UpdatedByContactId { get; set; }
		public string ObjectId { get; set; }
		public string ModuleNames { get; set; }
		public string CompanyTypes { get; set; }
		public string MenuNames { get; set; }
	}
}
