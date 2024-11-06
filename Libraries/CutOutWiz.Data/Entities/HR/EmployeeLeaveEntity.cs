namespace  CutOutWiz.Data.Entities.HR
{
    public class EmployeeLeaveEntity
    {
        public int Id { get; set; }
        public int LeaveForContactId { get; set; }
        public int LeaveTypeId { get; set; }
        public int? LeaveSubTypeId { get; set; }
        public DateTime LeaveStartRequestDate { get; set; } 
        public DateTime LeaveEndRequestDate { get; set; }
        public DateTime? LeaveApprovedStartDate { get; set; }
        public DateTime? LeaveApprovedEndDate { get; set; }
        public string EmployeeNote { get; set; }
        public string HRNote { get; set; }
        public string LeaveStatus { get; set; }
        public int LeaveRequestByContactId { get; set; }
        public int? LeaveApprovalByContactId { get; set; }
    }
}
