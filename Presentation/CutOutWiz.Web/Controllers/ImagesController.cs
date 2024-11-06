using CutOutWiz.Core.Utilities;
using CutOutWiz.Data;
using CutOutWiz.Data.Models.ApprovalTool;
using CutOutWiz.Services.ApprovalTool;
using CutOutWiz.Services.DataService;
using CutOutWiz.Services.LogService;
using CutOutWiz.Services.MessageService;
using CutOutWiz.Services.StorageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CutOutWiz.Web.Controllers
{
    [Authorize(Roles = "Client")]
    public class ImagesController : BaseController
    {
        public readonly IAwsService _awsService;
        private readonly IConfiguration _configuration;
        private readonly IFileTrackingService _fileFracking;
        private readonly IApprovalToolCommonService _approvalToolCommonService;
        private readonly IEmailSenderService _emailSenderSerice;
        private readonly ILogService _logger;

        public ImagesController(IAwsService awsService,
            IConfiguration configuration,
            IFileTrackingService fileFracking,
            IApprovalToolCommonService approvalToolCommonService,
            IEmailSenderService emailSenderSerice,
            ILogService logger)
        {
            _awsService = awsService;
            _configuration = configuration;
            _fileFracking = fileFracking;
            _emailSenderSerice = emailSenderSerice;
            _approvalToolCommonService = approvalToolCommonService;
            _logger = logger;
        }

        public IActionResult Test()
        {
            return View();
        }

        public IActionResult Index()
        {
            _logger.Log($"User {CurrentLoggedInUser.Username} landed on directory browsing page");

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTreeData(string path)
        {
            var folderPath = path;
            //if (!string.IsNullOrWhiteSpace(path))
            //{
            //    path = path.TrimStart('/');
            //}

            //FTP Server
            //FtpService ftpReader = new FtpService();
            //var result = ftpReader.ReadDirectoriesAsync(path);

            //Demo data
            //var result = GetDemoFolders();

            //S3 Server            
            var clientFolderPrefix = CurrentLoggedInUser.ClientRootFolderPath;
            var addApprovalPath = false;

            string delimiter = String.Empty;

            if (string.IsNullOrWhiteSpace(folderPath))
            {
                delimiter = "/";
                folderPath = $"{clientFolderPrefix}/";
                addApprovalPath = true;
            }
            else
            {
                delimiter = "/";
                folderPath = $"{folderPath}/";
            }

            var awsCredentials = _approvalToolCommonService.GetAwsCredentials();
            var response = await _awsService.GetDirectories(awsCredentials, folderPath, delimiter);

            if (response.Result == null)
            {
                response.Result = new List<Data.Node>();
            }

            //Add For Approval Path
            if (path != null && path.ToCharArray().Count(c => c == '/') == 1)
            {
                foreach (var folder in response.Result)
                {
                    folder.key = $"{folder.key}/For Approval";
                }
            }

            return Json(response.Result);
        }

        [HttpGet]
        public async Task<IActionResult> GetImagesByPath(string path, int imgWidth = 120, int imgHeight = 0)
        {
            _logger.Log($"User {CurrentLoggedInUser.Username} viewing images");

            if (!string.IsNullOrWhiteSpace(path))
            {
                //path= Uri.EscapeDataString(path);
                //path = WebUtility.UrlEncode(path);
                path = path.TrimStart('/');
            }

            //Ftp Files
            //FtpService ftpReader = new FtpService();
            //var response = ftpReader.ReadFilesAsync(path);

            //Demo files
            //var response = GetDemoImages();

            //S3 Files
            var awsCredentials = _approvalToolCommonService.GetAwsCredentials();
            var response = await _awsService.GetFilesByPath(awsCredentials, path);

            if (response.Result == null || !response.Result.Any())
            {
                response.Result = new List<Data.DriveImage>();
                return Json(response);
            }

            //Set thomnail image links if result found
            string imageHandlerBaseUrl = _configuration["AwsSettings:ImageHandlerBaseUrl"];

            var fullBasePath = $"{imageHandlerBaseUrl}filters:background_color(gainsboro)/fit-in/{imgWidth}x{imgHeight}/";

            List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".JPE", ".BMP", ".GIF", ".PNG", ".PSD", ".TIF", ".TIFF", ".WEBP", ".SVG", ".CR2", ".NEF" };
            var newImageList = new List<DriveImage>();

        //https://dprp12nc61odv.cloudfront.net/fit-in/200x200/Client%201/Brand%202/To%20Process/A163614-ADI-Catalog-FTW/image_2608801_b41bafab.png
            foreach (var img in response.Result)
            {
                var fileName = _approvalToolCommonService.GetFileNameFromPath(img.RawImagePath ?? "");

                //get images only
                if (!ImageExtensions.Contains(Path.GetExtension(fileName).ToUpperInvariant()))
                {
                    continue;
                }
                img.FullPath = $"{fullBasePath}{img.Path}";               
               
                var directoryName = _approvalToolCommonService.GetParentDirectory(img.RawImagePath ?? "");
                var fileNameWithoutIssueFolder = _approvalToolCommonService.GetActualDirectoryPathWithoutIssueFolder(directoryName);
                img.RawImagePath = $"{fullBasePath}{fileNameWithoutIssueFolder}/{fileName}";

                newImageList.Add(img);
            }

            response.Result = newImageList;
            return Json(response);
        }

        [HttpPost]
        public async Task<ActionResult> MoveFiles(ImageMoveRequestModel model)
        {            
            var moveResponse = new Response<List<ImageMoveResponseModel>>();
            var imagesMoveResponse = new List<ImageMoveResponseModel>();
            //validation
            if (model == null || model.SelectedImages == null)
            {
                moveResponse.Message = "Please select atleast 1 image.";
                return Json(moveResponse);
            }

            //Move Files
            var awsCredentials = _approvalToolCommonService.GetAwsCredentials();
            var selectedImages = model.SelectedImages.Split('|');
            
            foreach (var imgPath in selectedImages)
            {
                var fileMoveResponse = new Response<bool>();

                if (model.ActionType == ApprovalToolActionTypeConstants.Accepted)
                {
                    var destination = _approvalToolCommonService.GetDestinationPath(model.ActionType, imgPath);
                    fileMoveResponse = await _awsService.MoveFile(awsCredentials, imgPath, destination);
                }
                else if (model.ActionType == ApprovalToolActionTypeConstants.Rejected)
                {
                    fileMoveResponse = await _awsService.DeleteFile(awsCredentials, imgPath);
                }

                var fileName = imgPath.Split('/').Last();

                if (fileMoveResponse.IsSuccess)
                {
                    imagesMoveResponse.Add(new ImageMoveResponseModel { FileName = fileName, IsSuccess = true });
                }
                else
                {
                    imagesMoveResponse.Add(new ImageMoveResponseModel { FileName = fileName, IsSuccess = false, Message = fileMoveResponse.Message });
                }
            }
                        
            //Add Comments
            if (model.ActionType == ApprovalToolActionTypeConstants.Rejected && !string.IsNullOrWhiteSpace(model.Comments))
            {                
                var destination = _approvalToolCommonService.GetDestinationIssueFolderPath(model.SelectedImages);

                var comments = _approvalToolCommonService.GetFormattedComments(model);
                var postFixDatePart = DateTime.Now.ToString("dd MMMM yyyy HH mm ss");
                var commentFilePath = $"{destination}comment_{postFixDatePart}.txt";
                await _awsService.AddText(awsCredentials, commentFilePath, comments);                
            }

            moveResponse.IsSuccess = true;
            moveResponse.Result = imagesMoveResponse;

            //Message for client
            moveResponse.Message = _approvalToolCommonService.GetResponseMessage(imagesMoveResponse,model.ActionType);

            //Add accepted or rejected images in database
            var fileTracking = _approvalToolCommonService.AddFileTrackingRecord(model, CurrentLoggedInUser, awsCredentials);
            await _fileFracking.InsertFileTracking(fileTracking);

            //Send Email to Admin
            var emailMessage = _emailSenderSerice.GetEmailMessage(fileTracking);
            var emailSendResponse = await _emailSenderSerice.SendEmail(emailMessage);

            return Json(moveResponse);
        }

        #region Markup Image
        [HttpGet]
        public async Task<IActionResult> GetBase64ByPath(string path, int imgWidth = 800, int imgHeight = 0)
        {
            try
            {
                _logger.Log($"User {CurrentLoggedInUser.Username} opening markup");

                string imageHandlerBaseUrl = _configuration["AwsSettings:ImageHandlerBaseUrl"];
                var fullBasePath = $"{imageHandlerBaseUrl}fit-in/{imgWidth}x{imgHeight}/";

                if (path.EndsWith(".TIFF"))
                {
                    fullBasePath = fullBasePath.Replace(imageHandlerBaseUrl, imageHandlerBaseUrl + "filters:autojpg()/");
                }
                
                var fullPath = $"{fullBasePath}{path}";

                using (var client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync(fullPath).Result)
                    {
                        byte[] fileContents = await response.Content.ReadAsByteArrayAsync();

                        //var base64 = "data:image/png;base64," + Convert.ToBase64String(fileContents);
                        var base64 = "data:image/png;base64," + Convert.ToBase64String(fileContents);
                        return Json(new Response<string> { IsSuccess = true, Message = "base64 conversion successful", Result = base64 });
                    }
                }
            }
            catch (Exception e)
            {
                return Json(new Response<bool> { IsSuccess = false, Message = e.Message, Result = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveMarkup(string drivePath, string base64, string comment = "")
        {
            _logger.Log($"User {CurrentLoggedInUser.Username} saving markup");

            var response = new Response<string>();
            ImageMoveRequestModel model = new ImageMoveRequestModel
            {
                ActionType = ApprovalToolActionTypeConstants.Rejected,
                Comments = comment,
                SelectedImages = drivePath
            };

            try
            {
                var awsCredentials = _approvalToolCommonService.GetAwsCredentials();

                var destination = _approvalToolCommonService.GetDestinationIssueFolderPath(drivePath);
                var postFixDatePart = DateTime.Now.ToString("HH mm ss");
                var fileName = _approvalToolCommonService.GetFileNameFromPath(drivePath);

                var markupFileName = $"{fileName}_markup_{postFixDatePart}.png";

                if (string.IsNullOrWhiteSpace(comment))
                {
                    comment = $"Markup Reference: {markupFileName}";
                }
                else
                {
                    comment = $"{comment}{Environment.NewLine}{Environment.NewLine}Markup Reference: {markupFileName}";
                }
                
                var comments = _approvalToolCommonService.GetFormattedCommentForSingleImage(drivePath, comment);
                                        
                string commentDestinationPath = $"{destination}{fileName}_Comments_{postFixDatePart}.txt";
                var commentAddResponse = await _awsService.AddText(awsCredentials, commentDestinationPath, comments);

                if (!commentAddResponse.IsSuccess)
                {
                    response.Message = commentAddResponse.Message;
                    return Json(response);
                }                

                if (!string.IsNullOrWhiteSpace(base64))
                {
                    var newBase64 = base64.Replace("data:image/png;base64,", "");
                    string commentDestinationPath1 = $"{destination}{markupFileName}";
                    await _awsService.AddFile(awsCredentials, newBase64, commentDestinationPath1);
                }

                //Delete File from QC Folder
                var fileDeleteResponse = await _awsService.DeleteFile(awsCredentials, drivePath);

                if (!fileDeleteResponse.IsSuccess)
                {
                    response.Message = fileDeleteResponse.Message;
                    return Json(response);
                }

                //Add rejected images in database
                var fileTracking = _approvalToolCommonService.AddFileTrackingRecord(model, CurrentLoggedInUser, awsCredentials);
                await _fileFracking.InsertFileTracking(fileTracking);

                //Send Email to Admin
                var emailMessage = _emailSenderSerice.GetEmailMessage(fileTracking);
                var emailSendResponse = await _emailSenderSerice.SendEmail(emailMessage);

                response.Result = fileName;
                response.Message = "Successfull Rejected.";
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message =ex.Message;               
            }

            return Json(response);
        }
        #endregion
    }
}
