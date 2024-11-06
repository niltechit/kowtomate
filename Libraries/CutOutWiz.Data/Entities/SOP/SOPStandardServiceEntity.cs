namespace CutOutWiz.Data.Entities.SOP
{
	public class SOPStandardServiceEntity
	{
		public short Id { get; set; }
		public string Name { get; set; }
		public decimal? SortOrder { get; set; }
		public byte Status { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatedByContactId { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public int? UpdatedByContactId { get; set; }
		public string ObjectId { get; set; }
		public int? ParentSopServiceId { get; set; }
	}
}
