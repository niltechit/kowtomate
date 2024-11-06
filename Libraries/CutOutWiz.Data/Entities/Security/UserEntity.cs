namespace CutOutWiz.Data.Entities.Security
{
	public class UserEntity
	{
		public int Id { get; set; }
		public int CompanyId { get; set; }
		public int ContactId { get; set; }
		public string Username { get; set; }
		public string ProfileImageUrl { get; set; }
		public string PasswordHash { get; set; }
		public string PasswordSalt { get; set; }
		public string RegistrationToken { get; set; }
		public string PasswordResetToken { get; set; }
		public DateTime? LastLoginDateUtc { get; set; }
		public DateTime? LastLogoutDateUtc { get; set; }
		public DateTime? LastPasswordChangeUtc { get; set; }
		public int Status { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatedByContactId { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public int? UpdatedByContactId { get; set; }
		public string ObjectId { get; set; }
		public IEnumerable<string> SelectedUserRoles { get; set; } = new List<string>();
		public string DownloadFolderPath { get; set; }
	}
}
