namespace CutOutWiz.Data.Entities.Security
{
	public class MenuEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int? ParentId { get; set; }
		public string Icon { get; set; }
		public bool IsLeftMenu { get; set; }
		public bool IsTopMenu { get; set; }
		public bool IsExternalMenu { get; set; }
		public string MenuUrl { get; set; }
		public int? Status { get; set; }
		public DateTime CreatedDate { get; set; }
		public int? CreatedByContactId { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public int UpdatedByContactId { get; set; }
		public string ObjectId { get; set; }
		public decimal DisplayOrder { get; set; }
		//public IEnumerable<string> SelectedMenuPermissons { get; set; }
	}
}
