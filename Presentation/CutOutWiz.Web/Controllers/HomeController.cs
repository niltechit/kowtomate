using CutOutWiz.Data;
using CutOutWiz.Data.Models.Dashboard;
using CutOutWiz.Services.DataService;
using CutOutWiz.Services.LogService;
using CutOutWiz.Services.ReportService;
using CutOutWiz.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CutOutWiz.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogService _logger;
        private readonly IReportService _reportService;
        private readonly ICompanyService _companyService;

        public HomeController(ILogService logger, IReportService reportService, ICompanyService companyService)
        {
            _logger = logger;
            _reportService = reportService;
            _companyService = companyService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var companyId = (int) CurrentLoggedInUser.CompanyId;

            var brands = await _reportService.GetCompanyBrands(companyId);
            var selectListItems = brands.Select(m => new SelectListItem
                                        {
                                            Value = m,
                                            Text = m
                                        })
                                        .ToList();

            selectListItems.Add(new SelectListItem { Text = "Select Brand", Value = string.Empty });

            ViewData["Brands"] = selectListItems.OrderBy(x => x.Value).ToList();
            var Role = CurrentLoggedInUser.RoleName;
            if(Role != "Client")
            {
               return  RedirectToAction("Index", "Activity");
            }
            else 
           
            return View();

           
        }

        public async Task<IActionResult> GetReportData(string dateRange, string brand)
        {
            try
            {
                var companyId = (int)CurrentLoggedInUser.CompanyId;

                _logger.Log($"User {CurrentLoggedInUser.Username} landed on dashboard page");

                var dates = dateRange.Split(" - ");

                var startDate = DateTime.ParseExact(dates[0], "dd MMM yyyy", null);
                var endDate = DateTime.ParseExact(dates[1], "dd MMM yyyy", null);

                endDate = endDate.AddHours(23);
                endDate = endDate.AddMinutes(59);
                endDate = endDate.AddSeconds(59);

                TimeZoneInfo BdZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
                startDate = TimeZoneInfo.ConvertTimeToUtc(startDate, BdZone);
                endDate = TimeZoneInfo.ConvertTimeToUtc(endDate, BdZone);

                var res = await _reportService.GetDataForDashboard(startDate, endDate, brand, companyId);

                return Json(new Response<ImageCount>
                {
                    IsSuccess = true,
                    Message = "Dashboard data",
                    Result = res
                });
            }
            catch (Exception ex)
            {
                _logger.Log($"Exception occured {ex.Message}");

                return Json(new Response<dynamic>
                {
                    IsSuccess = false,
                    Message = "Failed to load data"
                });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Clear()
        {
            var companies = await _companyService.GetAllCompany();
            var selectListItems = companies.Select(m => new SelectListItem
                                            {
                                                Value = m.Id.ToString(),
                                                Text = m.FileRootFolderPath
                                            })
                                            .ToList();

            selectListItems.Add(new SelectListItem { Text = "Select Client", Value = string.Empty });

            ViewData["Companies"] = selectListItems.OrderBy(x => x.Value).ToList();


            return View();
        }

        public async Task<IActionResult> RemoveHistory(int companyId, string brand, string date, string state, string article)
        {
            try
            {



                return Json(new Response<dynamic>
                {
                    IsSuccess = true,
                    Message = "Successfully removed history",
                    Result = null
                });
            }
            catch(Exception ex)
            {
                return Json(new Response<dynamic>
                {
                    IsSuccess = false,
                    Message = "Error occured on removing histories",
                    Result = null
                });
            }
        }

        public async Task<IActionResult> GetCompanyBrands(int id)
        {
            try
            {
                var brands = await _reportService.GetCompanyBrands(id);

                return Json(new Response<List<string>>
                {
                    IsSuccess = true,
                    Message = "Successfully get company brand list",
                    Result = brands
                });
            }
            catch(Exception ex)
            {
                return Json(new Response<List<string>>
                {
                    IsSuccess = false,
                    Message = "Failed to get company brand list",
                    Result = null
                });
            }
        }

        public async Task<IActionResult> GetCompanyArticles(int id)
        {
            try
            {
                var articles = await _reportService.GetCompanyArticles(id);

                return Json(new Response<List<string>>
                {
                    IsSuccess = true,
                    Message = "Successfully get company articles list",
                    Result = articles
                });
            }
            catch (Exception ex)
            {
                return Json(new Response<List<string>>
                {
                    IsSuccess = false,
                    Message = "Failed to get company articles list",
                    Result = null
                });
            }
        }

    }
}