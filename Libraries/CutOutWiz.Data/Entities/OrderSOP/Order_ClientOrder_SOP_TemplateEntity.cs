
namespace CutOutWiz.Data.Entities.OrderSOP
{
	public class Order_ClientOrder_SOP_TemplateEntity
	{
		public int Id { get; set; }
		public int Order_ClientOrder_Id { get; set; }
		public int SOP_Template_Id { get; set; }
		public int SortOrder { get; set; }
		public bool IsDeleted { get; set; }
		public int OrderSOP_Template_Id { get; set; }
	}
}
