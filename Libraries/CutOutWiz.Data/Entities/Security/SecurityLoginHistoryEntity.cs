namespace CutOutWiz.Data.Entities.Security
{
	public class SecurityLoginHistoryEntity
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
}
