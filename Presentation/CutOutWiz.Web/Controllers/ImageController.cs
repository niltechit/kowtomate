using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CutOutWiz.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class ImageController : Controller
    {
        public IActionResult Index()
        {
            /*
            
            var path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\img"}";

            string img1Path = Path.Combine(path, "Image (1).jpg");
            string img2Path = Path.Combine(path, "Image (2).jpg");

            byte[] image1 = System.IO.File.ReadAllBytes(img1Path);
            byte[] image2= System.IO.File.ReadAllBytes(img2Path);


            var base64Image1 = "data:image/png;base64," + Convert.ToBase64String(image1);
            var base64Image2 = "data:image/png;base64," + Convert.ToBase64String(image2);


            ViewBag.Img1 = base64Image1;
            ViewBag.Img2 = base64Image2;

            */

            return View();
        }

        public IActionResult Paint()
        {

            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult SaveImage(string imageData)
        {

            Random rnd = new Random();
            String Filename = Directory.GetCurrentDirectory(); 
            Filename = Path.Combine(Filename, "wwwroot");
            Filename = Path.Combine(Filename, "img");
            Filename = Path.Combine(Filename, "2db32f4030938e97fc6e52378089c95d.jpg");

            string Pic_Path = Filename;//HttpContext.Current.Server.MapPath("ShanuHTML5DRAWimg.png");    
            using (FileStream fs = new FileStream(Pic_Path, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    byte[] data = Convert.FromBase64String(imageData);
                    bw.Write(data);
                    bw.Close();
                }
            }

            return null;
        }
    }
}
