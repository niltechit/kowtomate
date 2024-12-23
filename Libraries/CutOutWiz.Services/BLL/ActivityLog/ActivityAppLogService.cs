﻿using CutOutWiz.Core.Utilities;
using CutOutWiz.Core;
using CutOutWiz.Services.Models.ClientCategoryServices;
using CutOutWiz.Core.Models.ViewModel;
using CutOutWiz.Services.DbAccess;
using CutOutWiz.Services.Logs;
using Microsoft.Extensions.Configuration;
using static CutOutWiz.Core.Utilities.Enums;

namespace CutOutWiz.Services.BLL
{
    public class ActivityAppLogService: IActivityAppLogService
	{
        private readonly IConfiguration _configuration;
		private readonly ILogServices _activityLogService;
		public ActivityAppLogService(ISqlDataAccess db, ILogServices activityLogService, IConfiguration configuration)
		{
			_activityLogService = activityLogService;
            _configuration = configuration;
        }

		private async Task<Response<int>> Insert(ActivityLogModel activityLog)
		{
			return await _activityLogService.Insert(activityLog);
		}

	    public async Task InsertAppErrorActivityLog(CommonActivityLogViewModel model)
		{
			try
			{
				ActivityLogModel activityLog = new ActivityLogModel();
				activityLog.ActivityLogFor = model.ActivityLogFor;
				activityLog.PrimaryId = model.PrimaryId;
				activityLog.Description = $"Page: '{model.RazorPage}', Method: '{model.MethodName}', Error: {model.ErrorMessage}";
				activityLog.CreatedDate = DateTime.Now;
				activityLog.ObjectId = Guid.NewGuid().ToString();
				activityLog.Category = model.Category;

				if (model.Type > 0)
				{
					activityLog.Type = model.Type;
				}
				else
				{
					activityLog.Type = (byte)ActivityLogType.Error;
				}

                //Set CreatedByContactId
                if (model.CreatedByContactId > 0)
				{
					activityLog.CreatedByContactId = model.CreatedByContactId;
				}
				else if (model.loginUser != null && model.loginUser.ContactId > 0)
				{
					activityLog.CreatedByContactId = model.loginUser.ContactId;
				}
				else
				{
					activityLog.CreatedByContactId = 0;
                }

                //Set ContactObjectId
                if (!string.IsNullOrEmpty(model.ContactObjectId))
                {
                    activityLog.ContactObjectId = model.ContactObjectId;
                }

                //Set CompanyObjectId
                if (!string.IsNullOrEmpty(model.CompanyObjectId))
                {
                    activityLog.CompanyObjectId = model.CompanyObjectId;
                }
                else if (model.loginUser != null && !string.IsNullOrEmpty(model.CompanyObjectId))
                {
                    activityLog.CompanyObjectId = model.loginUser.CompanyObjectId;
                }

                await _activityLogService.Insert(activityLog);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error on Insert App Error Log Acitivity");
			}

		}

        public async Task AppStartLog(string consoleAppName)
        {
            try
            {
                // Read the "APP" value from appsettings.json
                string appId = _configuration["GeneralSettings:APP"];

                ActivityLogModel activityLog = new ActivityLogModel();

                activityLog.Description = $"ConsoleAppName: {consoleAppName}";

                if (appId != null)
                {
                    activityLog.ActivityLogFor = ActivityLogForConstants.ConsoleAppId;
                    activityLog.PrimaryId = Convert.ToInt32(appId);
                    activityLog.Description = $"{activityLog.Description}, AppId: {appId}.";
                }
                else
                {
                    activityLog.ActivityLogFor = ActivityLogForConstants.GeneralLog;
                }

                activityLog.CreatedDate = DateTime.Now;
                activityLog.ObjectId = Guid.NewGuid().ToString();
                activityLog.Category = (int)ActivityLogCategory.ConsoleAppStart;
                activityLog.Type = (byte)ActivityLogType.Info;
                activityLog.CreatedByContactId = 0;

                await _activityLogService.Insert(activityLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Insert App Error Log Acitivity");
            }
        }

        public async Task AppEndLog(string consoleAppName)
        {
            try
            {
                // Read the "APP" value from appsettings.json
                string appId = _configuration["GeneralSettings:APP"];

                ActivityLogModel activityLog = new ActivityLogModel();
                activityLog.Description = $"ConsoleAppName: {consoleAppName}";

                if (appId != null)
                {
                    activityLog.ActivityLogFor = ActivityLogForConstants.ConsoleAppId;
                    activityLog.PrimaryId = Convert.ToInt32(appId);
                    activityLog.Description = $"{activityLog.Description}, AppId: {appId}.";
                }
                else
                {
                    activityLog.ActivityLogFor = ActivityLogForConstants.GeneralLog;
                }

                activityLog.CreatedDate = DateTime.Now;
                activityLog.ObjectId = Guid.NewGuid().ToString();
                activityLog.Category = (int)ActivityLogCategory.ConsoleAppAppEnd;
                activityLog.Type = (byte)ActivityLogType.Info;
                activityLog.CreatedByContactId = 0;

                await _activityLogService.Insert(activityLog);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error on Insert App Error Log Acitivity");
            }
        }


        public async Task InsertAppDownloadToEditorPcActivityLog(int clientOrderItem, string message,int contactId = AutomatedAppConstant.ContactId)
		{
			try
			{
				ActivityLogModel activityLog = new ActivityLogModel();
				activityLog.ActivityLogFor = ActivityLogForConstants.OrderItem;
				activityLog.PrimaryId = clientOrderItem;
				activityLog.Description = message;
				activityLog.CreatedDate = DateTime.Now;
				activityLog.CreatedByContactId = AutomatedAppConstant.ContactId;
				activityLog.ObjectId = Guid.NewGuid().ToString();
				

				await _activityLogService.Insert(activityLog);
			}
			catch (Exception ex)
			{
				
			}
		}

        #region CategoryChangeLog
        public async Task<Response<int>> ClientCategoryChangeLogInsert(ClientCategoryChangeLogModel clientCategoryChangeLog)
        {
			return await _activityLogService.ClientCategoryChangeLogInsert(clientCategoryChangeLog);
		}

        #endregion
    }
}
