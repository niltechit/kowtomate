using CutOutWiz.Data.Repositories.Accounting;
using CutOutWiz.Data.Repositories.Common;
using CutOutWiz.Data.Repositories.DynamicReport;
using CutOutWiz.Data.Repositories.HR;
using CutOutWiz.Data.Repositories.OrderSOP;
using CutOutWiz.Data.Repositories.Security;
using CutOutWiz.Data.Repositories.SOP;
using Microsoft.Extensions.DependencyInjection;

namespace CutOutWiz.Services
{
	// In your Service Layer, create an extension method class
	public static class RegisterServicesScoped
	{
		public static void AddRepositoryServices(this IServiceCollection services)
		{
			services.AddScoped<CutOutWiz.Data.DbAccess.ISqlDataAccess, CutOutWiz.Data.DbAccess.SqlDataAccess>();

			// Add repository services
			services.AddScoped<IRoleRepository, RoleRepository>();
			services.AddScoped<IContactRepository, ContactRepository>();
			services.AddScoped<ICompanyRepository, CompanyRepository>();
			services.AddScoped<ICompanyTeamRepository, CompanyTeamRepository>();
			services.AddScoped<ICountryRepository, CountryRepository>();
			services.AddScoped<IFileServerRepository, FileServerRepository>();
			services.AddScoped<IShiftRepository, ShiftRepository>();
			services.AddScoped<ICompanyGeneralSettingRepository, CompanyGeneralSettingRepository>();
			services.AddScoped<IOverheadCostRepository, OverheadCostRepository>();
			services.AddScoped<IGridViewSetupRepository, GridViewSetupRepository>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<ISecurityLoginHistoryRepository, SecurityLoginHistoryRepository>();
			services.AddScoped<IMenuRepository, MenuRepository>();
			services.AddScoped<IModuleRepository, ModuleRepository>();
			services.AddScoped<IPermissionRepository, PermissionRepository>();
			services.AddScoped<IDesignationRepository, DesignationRepository>();
			services.AddScoped<IEmployeeLeaveRepository, EmployeeLeaveRepository>();
			services.AddScoped<IEmployeeProfileRepository, EmployeeProfileRepository>();
			services.AddScoped<ILeaveSubTypeRepository, LeaveSubTypeRepository>();
			services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();

			//SOP
			services.AddScoped<ISOPStandardServiceRepository, SOPStandardServiceRepository>();
			services.AddScoped<ISOPTemplateRepositoty, SOPTemplateRepository>();
			services.AddScoped<ISOPTemplateServiceRepositoty, SOPTemplateServiceRepositoty>();
			services.AddScoped<ISOPTempleateFileRepository, SOPTempleateFileRepository>();
			services.AddScoped<IOrderSOPStandardServiceRepository, OrderSOPStandardServiceRepository>();
			services.AddScoped<IOrderSOPTemplateFileRepository, OrderSOPTemplateFileRepository>();
			services.AddScoped<IOrderSOPTemplateJoiningRepository, OrderSOPTemplateJoiningRepository>();
			services.AddScoped<IOrderSOPTemplateServiceRepositoty, OrderSOPTemplateServiceRepositoty>();
			services.AddScoped<IOrderTemplateSOPRepository, OrderTemplateSOPRepository>();
		}
	}

}
