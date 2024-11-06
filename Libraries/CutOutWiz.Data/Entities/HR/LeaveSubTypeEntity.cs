namespace  CutOutWiz.Data.Entities.HR
{
    public class LeaveSubTypeEntity
    {
        public int Id { get; set; }
        public int LeaveTypeId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
        public int? CreatedByContactId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? UpdatedByContactId { get; set; }
        public DateTime? UpdatedDate { get; set; }

        //Additional Value
        public string LeaveTypeName { get;set; }
        public bool IsDeleted { get; set; }
    }
}
