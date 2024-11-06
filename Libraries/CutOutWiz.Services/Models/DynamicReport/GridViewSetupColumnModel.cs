
namespace CutOutWiz.Services.Models.UI
{
    public class GridViewSetupColumnModel
    {
        public int Id { get; set; }
        public int ColoumnId { get; set; }
        public int GridViewSetupId { get; set; }
        public int DisplayOrder { get; set; }
        public double Width { get; set; }
    }

    public class GridViewSetupColumnSlimModel
    {
        public int ColoumnId { get; set; }
        public int DisplayOrder { get; set; }
        public double Width { get; set; }

        public string WidthPx => $"{Width}px";
    }
}
