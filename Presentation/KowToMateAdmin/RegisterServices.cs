#region Using
using CutOutWiz.Services.DbAccess;
using CutOutWiz.Services.HR;
using CutOutWiz.Services.Email;
using CutOutWiz.Services.Management;
using CutOutWiz.Services.Security;
using CutOutWiz.Services.SOP;
using CutOutWiz.Services.EmailMessage;
using CutOutWiz.Services.EmailSender;
using CutOutWiz.Services.LogServices;
using Radzen;
using CutOutWiz.Services.Logs;
using CutOutWiz.Services.Log;
using CutOutWiz.Services.StorageService;
using CutOutWiz.Services.ClientOrders;
using CutOutWiz.Services.InternalMessage;
using CutOutWiz.Services.FolderServices;
using CutOutWiz.Services.OrderTeamServices;
using CutOutWiz.Services.MessageService;
using CutOutWiz.Services.OrderAndOrderItemStatusChangeLogServices;
using CutOutWiz.Services.OrderItemStatusChangeLogService;
using CutOutWiz.Services.OrderSOP;
using CutOutWiz.Services.Dashboards;
using KowToMateAdmin.Services;
using CutOutWiz.Services.BLL.OrderStatusAndOrderItemStatus;
using CutOutWiz.Services.BLL.UpdateOrderItem;
using CutOutWiz.Services.BLL.StatusChangeLog;
using CutOutWiz.Services.BLL;
using CutOutWiz.Services.ReportServices;
using CutOutWiz.Services.DynamicReports;
using CutOutWiz.Services.PathReplacementServices;
using CutOutWiz.Services.BLL.OrderAttachment;
using CutOutWiz.Services.CpanelStorageInfoServices;
using CutOutWiz.Services.FeedbackReworkServices;
using CutOutWiz.Services.ClientCommonCategoryService.CommonCategories;
using CutOutWiz.Services.EncryptedMethodServices;
using CutOutWiz.Services.Managers.Accounting;
using CutOutWiz.Services.AutomationAppServices.FtpOrderProcess;
using CutOutWiz.Services.AutomationAppServices.MakeOrderPlacingToPlaced;
using CutOutWiz.Services.ErrorLogServices;
using CutOutWiz.Services.AutomationAppServices.UploadFromEditorPcAutomation;
using CutOutWiz.Services.AutomationAppServices.UploadFromQcPcAutomation;
using CutOutWiz.Services.AutomationAppServices.OrderPlaceAutomation;
using CutOutWiz.Services.AutomationAppServices.OrderDeliveryAutomation;
using CutOutWiz.Services.AutomationAppServices.DownloadToEditorAutomation;
using CutOutWiz.Services.AutomationAppServices.OrderWorkFlowAutomationServices;
using CutOutWiz.Services.AutomationAppServices.EmailSendAutomation;
using CutOutWiz.Services.FtpFileDeleteServices;
using CutOutWiz.Services.IbrApiServices;
using CutOutWiz.Services.BLL.AssignOrderAndItem;
using CutOutWiz.Services.AutomationAppServices.DeleteFilesFromNasAutomation;
using CutOutWiz.Services.ClaimManagementService;
using CutOutWiz.Services.WinSCPFtpService;
using CutOutWiz.Services.MapperHelper;
using CutOutWiz.Services.AutomationAppServices.OrderItemCategorySetByAutomation;
using CutOutWiz.Services.UI;
using CutOutWiz.Services.ImportExport;
using CutOutWiz.Services.AutomationAppServices.ConvertOrderAttachmentFiles;
using CutOutWiz.Services.Helper;
using CutOutWiz.Services.OperationSwitchService;
using CutOutWiz.Services.SftpServices;
using CutOutWiz.Services.WebApiService;
using CutOutWiz.Services.ClientCommonCategoryService.ClientCategorys;
using CutOutWiz.Services.ClientCommonCategoryService.CommonCategoryServices;
using CutOutWiz.Services.ClientCommonCategoryService.ClientCategoryServices;
using CutOutWiz.Services.Managers.Common;
using CutOutWiz.Services;
using Blazored.Toast;

#endregion end of Using

namespace KowToMateAdmin
{
    public static class RegisterServices
    {
        public static void ConfigureServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddMemoryCache();
            builder.Services.AddScoped<DialogService>();
			builder.Services.AddBlazoredToast();

			builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IWorkContext, WorkContext>();
            builder.Services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
            builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>();

			//TODO: Move all dependency to the respective layer later
			//Repositories
			//Remove this after all convertion
			builder.Services.AddRepositoryServices();
			//End of Repositories

			builder.Services.AddScoped<IDesignationService, DesignationService>();
            builder.Services.AddScoped<ICompanyManager, CompanyManager>();
            builder.Services.AddScoped<ICountryManager, CountryManager>();
            builder.Services.AddScoped<IContactManager, ContactManager>();
			//builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();
			builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IModuleService, ModuleService>();
            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<IPermissionService, PermissionService>();
            builder.Services.AddScoped<IRoleManager, RoleManager>();
            builder.Services.AddScoped<IFileServerManager, FileServerManager>();
            builder.Services.AddScoped<IEmailSenderAccountService, EmailSenderAccountService>();
            builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
            builder.Services.AddScoped<ITeamService, TeamService>();
            builder.Services.AddScoped<ITeamRoleService, TeamRoleService>();
            builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
            builder.Services.AddScoped<IOrderSOPStandardServiceService, OrderSOPStandardServiceService>();
            builder.Services.AddScoped<IOrderSOPTemplateService, OrderSOPTemplateService>();

            builder.Services.AddScoped<ILogService, LogService>();
            builder.Services.AddScoped<IWorkflowEmailService, WorkflowEmailService>();
            builder.Services.AddScoped<IEmailTokenProvider, EmailTokenProvider>();
            builder.Services.AddScoped<IEmailTokenizer, EmailTokenizer>();
            builder.Services.AddScoped<IMailjetEmailService, MailjetEmailService>();
            builder.Services.AddScoped<ILogServices,LogServices>();
            builder.Services.AddScoped<IGCPService,GCPService>();
            builder.Services.AddScoped<IFtpService,FtpService>();
            builder.Services.AddScoped<IOrderTemplateService, OrderTemplateService>();
            builder.Services.AddScoped<IOrderSOPTempleateFileService, OrderSOPTempleateFileService>();

            builder.Services.AddScoped<IClientOrderItemService, ClientOrderItemService>();
            builder.Services.AddScoped<IClientOrderService, ClientOrderService>();
            builder.Services.AddScoped<IOrderFileAttachmentService, OrderFileAttachmentService>();
            builder.Services.AddScoped<IOperationEmailService, OperationEmailService>();

            builder.Services.AddScoped<IArchiveQueueEmailService, ArchiveQueueEmailService>();
            builder.Services.AddScoped<IInternalMessageTemplateService, InternalMessageTemplateService>();
            builder.Services.AddScoped<IInternalMessageService, InternalMessageService>();
            builder.Services.AddScoped<ILocalFileService, LocalFileService>();
            builder.Services.AddScoped<IFolderService, FolderService>();
            builder.Services.AddScoped<IInternalMessageTokenProvider, InternalMessageTokenProvider>();
            builder.Services.AddScoped<IInernalMessageTokenizer, InernalMessageTokenizer>();
            builder.Services.AddScoped<IOrderTeamService, OrderTeamService>();
            builder.Services.AddScoped<IOrderAssignedImageEditorService, OrderAssignedImageEditorService>();
            builder.Services.AddScoped<ICompanyTeamManager, CompanyTeamManager>();
            builder.Services.AddScoped<IOrderFileAssignService, OrderFileAssignService>();
            builder.Services.AddScoped<IOrderItemStatusChangeLogService, OrderItemStatusChangeLogService>();
            builder.Services.AddScoped<IOrderStatusChangeLogService, OrderStatusChangeLogService>();
            builder.Services.AddScoped<NotificationService>();
            //builder.Services.AddScoped<IPageImage, PageImage>();

            builder.Services.AddScoped<IDownloadService,DownloadService>();
            builder.Services.AddScoped<IFluentFtpService, FluentFtpService>();

            builder.Services.AddScoped<IOrderTemplateSOPService, OrderTemplateSOPService>();
            builder.Services.AddScoped<IOrderSOPTemplateOrderSOPStandardService, OrderSOPTemplateOrderSOPStandardService>();
            builder.Services.AddScoped<IOrderSOPStandardService, OrderSOPStandardService>();
            builder.Services.AddScoped<IOrderTempleateSOPFileService, OrderTempleateSOPFileService>();
            builder.Services.AddScoped<IFtpFilePathService, FtpFilePathService>();
            builder.Services.AddScoped<IOrderSOPTemplateJoiningService, OrderSOPTemplateJoiningService>();
            builder.Services.AddScoped<IDashboardService, DashboardService>();
            builder.Services.AddScoped<IClientOrderFtpService, ClientOrderFtpService>();
            builder.Services.AddScoped<ICompanyGeneralSettingManager, CompanyGeneralSettingManager>();
            
            builder.Services.AddServerSideBlazor().AddCircuitOptions(option => { option.DetailedErrors = true; });

            builder.Services.AddScoped<IOrderStatusService, OrderStatusService>();
			builder.Services.AddScoped<IUpdateOrderItemBLLService, UpdateOrderItemBLLService>();
			builder.Services.AddScoped<IPathReplacementService, PathReplacementService>();

			//builder.Services.AddServerSideBlazor().AddCircuitOptions(option => { option.DetailedErrors = true; });

			builder.Services.AddScoped<IAcitivityLogCommonMethodService, AcitivityLogCommonMethodService>();
			builder.Services.AddScoped<IStatusChangeLogBLLService, StatusChangeLogBLLService>();
			builder.Services.AddScoped<IActivityAppLogService, ActivityAppLogService>();
            builder.Services.AddScoped<IShiftManager, ShiftManager>();

            //Dynamic reports
            builder.Services.AddScoped<IDynamicReportInfoService, DynamicReportInfoService>();
            builder.Services.AddScoped<IGridViewSetupService, GridViewSetupService>();
            //builder.Services.AddScoped<IDataImportService, DataImportService>();
            builder.Services.AddScoped<IDataImportExportService, DataImportExportService>();
            builder.Services.AddScoped<IGridFilterService, GridFilterService>();
            builder.Services.AddScoped<IDynamicReportPageViewFilterService, DynamicReportPageViewFilterService>();

            builder.Services.AddScoped<IOperationReportService, OperationReportService>();
			builder.Services.AddScoped<IAutoOrderDeliveryService,AutoOrderDeliveryService>();
			builder.Services.AddScoped<ISecurityLoginHistoryService, SecurityLoginHistoryService>();
			builder.Services.AddScoped<ISshNetService, SshNetService>();
			builder.Services.AddScoped<IOrderAttachmentBLLService, OrderAttachmentBLLService>();
			builder.Services.AddScoped<IFluentFtpService, FluentFtpService>();
			builder.Services.AddScoped<ICpanelStorageInfoService, CpanelStorageInfoService>();
			builder.Services.AddScoped<IFeedbackReworkService, FeedbackReworkService>();
            // Client Category Created Services
			builder.Services.AddScoped<IClientCategoryService, ClientCategoryService>();
			builder.Services.AddScoped<ICommonServiceService, CommonServiceService>();
			builder.Services.AddScoped<ICommonCategoryServiceService, CommonCategoryServiceService>();
			builder.Services.AddScoped<ICommonCategoryService, CommonCategoryService>();
			builder.Services.AddScoped<IClientCategoryServiceService, ClientCategoryServiceService>();
			builder.Services.AddScoped<IEncryptedMethodService, EncryptedMethodService>();
            builder.Services.AddScoped<IManageTeamMemberChangelogService,ManageTeamMemberChangelogService>();
            // Overhead cost
            builder.Services.AddScoped<IOverheadCostManager, OverheadCostManager>();
            builder.Services.AddScoped<IFileConvertionService, FileConvertionService>();

            builder.Services.AddScoped<IOrderPlacingToPlacedService, OrderPlacingToPlacedService>();
            builder.Services.AddScoped<IOrderPlaceService, OrderPlaceService>();
            builder.Services.AddScoped<IErrorLogService, ErrorLogService>();
            builder.Services.AddScoped<IUploadFromEditorPcService, UploadFromEditorPcService>();
            builder.Services.AddScoped<IUploadFromQcPcService, UploadFromQcPcService>();
            builder.Services.AddScoped<IOrderDeliveryService, OrderDeliveryService>();
            builder.Services.AddScoped<IDownloadToEditorService, DownloadToEditorService>();
            builder.Services.AddScoped<IOrderWorkFlowAutomationService, OrderWorkFlowAutomationService>();
            builder.Services.AddScoped<IAutoEmailSendService, AutoEmailSendService>();

            builder.Services.AddScoped<IFtpFileDeleteService, FtpFileDeleteService>();
            builder.Services.AddScoped<IClientExternalOrderFTPSetupService, ClientExternalOrderFTPSetupService>();
            builder.Services.AddScoped<IIbrApiService, IbrApiService>();
            builder.Services.AddScoped<IAssingOrderItemService, AssingOrderItemService>();
            builder.Services.AddScoped<IDeleteFilesFromNasService, DeleteFilesFromNasService>();
            builder.Services.AddScoped<IClaimsService,ClaimsService>();

            //Reporting            
            builder.Services.AddScoped<ICompletedFilesComparisionReportService, CompletedFilesComparisionReportService>();
            builder.Services.AddScoped<IWinSCPFtpLibraryService,WinSCPFtpLibraryService>();
            builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();
            builder.Services.AddScoped<IEmployeeProfileService, EmployeeProfileService>();
            builder.Services.AddScoped<IEmployeeLeaveService, EmployeeLeaveService>();
            builder.Services.AddScoped<ILeaveSubTypeService, LeaveSubTypeService>();
            builder.Services.AddScoped<IMapperHelperService, MapperHelperService>();
            builder.Services.AddScoped<IClientCategoryBaseRuleService, ClientCategoryBaseRuleService>();
            builder.Services.AddScoped<ICategorySetService,CategorySetService>();
            builder.Services.AddScoped<IConvertOrderAttachmentFile, ConvertOrderAttachmentFile>();
            builder.Services.AddScoped<IPDFConverstionService, PDFConverstionService>();
            builder.Services.AddScoped<ISwitchOperation, SwitchOperation>();
            builder.Services.AddScoped<ISftpService, SftpService>();
            builder.Services.AddScoped<IFtpSharpLibraryService, FtpSharpLibraryService>();
            builder.Services.AddScoped<IFileViewApiService, FileViewApiService>();
            builder.Services.AddTransient<CloudStorageUsesLimitAlertBackgroundService>();
            builder.Services.AddHostedService(provider => provider.GetRequiredService<CloudStorageUsesLimitAlertBackgroundService>());
        }
    }

}
