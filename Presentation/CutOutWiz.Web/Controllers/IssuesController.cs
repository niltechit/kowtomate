using CutOutWiz.Core.Utilities;
using CutOutWiz.Services.DataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CutOutWiz.Web.Controllers
{
    [Authorize(Roles ="SuperAdmin")]
    public class IssuesController : BaseController
    {
        private readonly IFileTrackingService _fileTracking;

        public IssuesController(IFileTrackingService fileTracking)
        {
            _fileTracking=fileTracking;
        }

        // GET: IssuesController
        public async Task<ActionResult> Index()
        {
            var issues = await _fileTracking.GetFileTrackingByCompanyId(CurrentLoggedInUser.CompanyId ?? 0, ApprovalToolActionTypeConstants.Rejected);
            return View(issues);
        }

        // GET: IssuesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: IssuesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IssuesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IssuesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: IssuesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: IssuesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: IssuesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
