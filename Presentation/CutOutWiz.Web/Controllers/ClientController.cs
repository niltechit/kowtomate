using CutOutWiz.Core.Utilities;
using CutOutWiz.Data;
using CutOutWiz.Services.DataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CutOutWiz.Web.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class ClientController : BaseController
    {

        private readonly ICompanyService _companyService;
        public ClientController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async Task<ActionResult> Index()
        {
            var company = await _companyService.GetAllCompany();
            return View(company);
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // POST: CompaniesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Company model)
        {
            try
            {
                //Validate Folder Name
                var folder = await _companyService.GetFolderByFolderName(model.FileRootFolderPath);

                if(folder.Count() == 0)
                {
                    model.CreatedDateUtc = DateTime.Now;
                    model.Status = StatusConstants.Active;
                    model.CreatedByContactId = CurrentLoggedInUser.UserId;

                    await _companyService.InsertCompany(model);
                    ViewBag.Message = "New client is created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = model.FileRootFolderPath+" is already exists!";
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: CompaniesController/Edit/5
        public ActionResult Edit(int id)
        {
            var companyInfo = _companyService.GetCompanyById(id);
            var company = companyInfo.Result;
            return View(company);
        }

        // POST: CompaniesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Company model)
        {
            try
            {
                var updateInfo = _companyService.UpdateCompany(model);
                if (updateInfo != null)
                {
                    ViewBag.Message = "Update Successfully Done";
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Message = "Update Failed";
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: CompaniesController/Delete/5
        public ActionResult Delete(int id)
        {
            _companyService.DeleteCompany(id);
            return RedirectToAction(nameof(Index));
        }


        // POST: CompaniesController/Delete/5
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
