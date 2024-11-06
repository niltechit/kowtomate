namespace CutOutWiz.Services.Models.Security
{
	public class SecurityLoginHistoryModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Username { get; set; }
		public DateTime ActionTime { get; set; }
		public bool ActionType { get; set; }
		public string IPAddress { get; set; }
		public string DeviceInfo { get; set; }
		public bool Status { get; set; }
	}

	/// <summary>
	/// TODO: Limilar Object
	/// </summary>
	public class SecurityLoginHistoryViewModel
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public string Username { get; set; }
		public DateTime ActionTime { get; set; }
		public string ActionType { get; set; }
		public string IPAddress { get; set; }
		public string DeviceInfo { get; set; }
		public bool Status { get; set; }
	}
}
