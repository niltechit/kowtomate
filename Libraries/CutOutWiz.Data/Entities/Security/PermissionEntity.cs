namespace CutOutWiz.Data.Entities.Security
{
	public class PermissionEntity
	{
		public short Id { get; set; }
		public string DisplayName { get; set; }
		public string PermissionValue { get; set; }  //Permission Note
		public byte Status { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatedByContactId { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public int? UpdatedByContactId { get; set; }
		public string ObjectId { get; set; }

		//TODO: Move it to Dto Later
		public decimal DisplayOrder { get; set; }
		//For 
		//public IEnumerable<string> SelectedModules { get; set; } = new List<string>();
		//public IEnumerable<string> SelectedCompanyTypes { get; set; }
	}
}
