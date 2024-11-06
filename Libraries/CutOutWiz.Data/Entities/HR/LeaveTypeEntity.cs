namespace  CutOutWiz.Data.Entities.HR
{
    public class LeaveTypeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; }
        public int? CreatedByContactId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedByContactId { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
