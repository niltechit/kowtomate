namespace CutOutWiz.Data.Entities.SOP
{
	public class SOPTemplateEntity
	{
		public int Id { get; set; }
		public int CompanyId { get; set; }
		//[Required(ErrorMessage = "File Server is required.")]
		public short? FileServerId { get; set; }
		public string Name { get; set; }

		public decimal Version { get; set; }
		public int? ParentTemplateId { get; set; }
        public string Instruction { get; set; }
		public decimal? UnitPrice { get; set; }
		public byte? Status { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatedByContactId { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public int? UpdatedByContactId { get; set; }
		public int? InstructionModifiedByContactId { get; set; }
		public string ObjectId { get; set; }

		//TODO: add dto later for extra fields
		public int? ParentSopServiceId { get; set; }
		public int OrderTemplateId { get; set; }
	}
}
