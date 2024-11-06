namespace CutOutWiz.Data.DTOs.HR
{
	public class EmployeeLeaveDto
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
		public string EmployeeId { get; set; }
		public string Name { get; set; }
		//public List<LeaveTypeModel> LeaveTypes { get; set; } = new List<LeaveTypeModel>();
		//public List<LeaveSubTypeModel> SubLeaveTyoes { get; set; } = new List<LeaveSubTypeModel>();
	}
}
