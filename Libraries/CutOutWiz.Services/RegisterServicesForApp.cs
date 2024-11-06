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
	public static class RegisterServicesForApp
	{
		public static void AddRepositoryServicesForApp(this IServiceCollection services)
		{
			services.AddSingleton<CutOutWiz.Data.DbAccess.ISqlDataAccess, CutOutWiz.Data.DbAccess.SqlDataAccess>();

			// Add repository services
			services.AddSingleton<IRoleRepository, RoleRepository>();
			services.AddSingleton<IContactRepository, ContactRepository>();
			services.AddSingleton<ICompanyRepository, CompanyRepository>();
			services.AddSingleton<ICompanyTeamRepository, CompanyTeamRepository>();
			services.AddSingleton<ICountryRepository, CountryRepository>();
			services.AddSingleton<IFileServerRepository, FileServerRepository>();
			services.AddSingleton<IShiftRepository, ShiftRepository>();
			services.AddSingleton<ICompanyGeneralSettingRepository, CompanyGeneralSettingRepository>();
			services.AddSingleton<IOverheadCostRepository, OverheadCostRepository>();
			services.AddSingleton<IGridViewSetupRepository, GridViewSetupRepository>();
			services.AddSingleton<IUserRepository, UserRepository>();
			services.AddSingleton<ISecurityLoginHistoryRepository, SecurityLoginHistoryRepository>();
			services.AddSingleton<IMenuRepository, MenuRepository>();
			services.AddSingleton<IModuleRepository, ModuleRepository>();
			services.AddSingleton<IPermissionRepository, PermissionRepository>();
			services.AddSingleton<IDesignationRepository, DesignationRepository>();
			services.AddSingleton<IEmployeeLeaveRepository, EmployeeLeaveRepository>();
			services.AddSingleton<IEmployeeProfileRepository, EmployeeProfileRepository>();
			services.AddSingleton<ILeaveSubTypeRepository, LeaveSubTypeRepository>();
			services.AddSingleton<ILeaveTypeRepository, LeaveTypeRepository>();

			//SOP
			services.AddSingleton<ISOPStandardServiceRepository, SOPStandardServiceRepository>();
			services.AddSingleton<ISOPTemplateRepositoty, SOPTemplateRepository>();
			services.AddSingleton<ISOPTemplateServiceRepositoty, SOPTemplateServiceRepositoty>();
			services.AddSingleton<ISOPTempleateFileRepository, SOPTempleateFileRepository>();
			services.AddSingleton<IOrderSOPStandardServiceRepository, OrderSOPStandardServiceRepository>();
			services.AddSingleton<IOrderSOPTemplateFileRepository, OrderSOPTemplateFileRepository>();
			services.AddSingleton<IOrderSOPTemplateJoiningRepository, OrderSOPTemplateJoiningRepository>();
			services.AddSingleton<IOrderSOPTemplateServiceRepositoty, OrderSOPTemplateServiceRepositoty>();
			services.AddSingleton<IOrderTemplateSOPRepository, OrderTemplateSOPRepository>();
		}
	}

}
