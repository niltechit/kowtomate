using CutOutWiz.Data.Models.Log;
using CutOutWiz.Services.DataService;
using CutOutWiz.Services.LogService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CutOutWiz.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class ActivityController : BaseController
    {
        private readonly ILogService _logger;
        private readonly IUserService _userService;

        public ActivityController(ILogService logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var logModel = _logger.GetLogsByDate(DateTime.Now);

            logModel.DateRange = $"{DateTime.Now.ToString("dd MMM yyyy")} - {DateTime.Now.ToString("dd MMM yyyy")}"; 

            ViewData["users"] = GetUserSelectListItems().Result;

            return View(logModel);
        }

        private async Task<List<SelectListItem>> GetUserSelectListItems()
        {
            var users = await _userService.GetUsers();
            var selectClientList = users.Select(m => new SelectListItem
                                        {
                                            Text = m.Username,
                                            Value = m.Username
                                        })
                                        .ToList();


            selectClientList.Add(new SelectListItem
            {
                Text = "All",
                Value = null,
                Selected = true
            });

            selectClientList = selectClientList.OrderBy(m => m.Text).ToList();

            return selectClientList;
        }

        [HttpPost]
        public IActionResult Index(LogModel logModel)
        {
            var dates = logModel.DateRange.Split(" - ");

            var startDate = DateTime.ParseExact(dates[0], "dd MMM yyyy", null);
            var endDate = DateTime.ParseExact(dates[1], "dd MMM yyyy", null);

            List<string> logs = new List<string>();

            while (startDate <= endDate)
            {
                var temp = _logger.GetLogsByDate(startDate);
                logs.AddRange(temp.Logs);

                startDate = startDate.AddDays(1);
            }

            

            if(logModel.Username != "All")
            {
                logs = logs.Where(m => m.Contains(logModel.Username)).ToList();
            }
            
            ViewData["users"] = GetUserSelectListItems().Result;

            logModel.Logs = logs;

            return View(logModel);
        }
    }
}
