using System.ComponentModel.DataAnnotations;

namespace  CutOutWiz.Data.Entities.HR
{
	public class DesignationEntity
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public byte? Status { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatedByContactId { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public int? UpdatedByContactId { get; set; }
		public string ObjectId { get; set; }
        public bool DayOffMonday { get; set; }
        public bool DayOffTuesday { get; set; }
        public bool DayOffWednesday { get; set; }
        public bool DayOffThursday { get; set; }
        public bool DayOffFriday { get; set; }
        public bool DayOffSaturday { get; set; }
        public bool DayOffSunday { get; set; }

    }
}
