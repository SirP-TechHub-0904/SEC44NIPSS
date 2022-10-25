using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SEC44NIPSS.Data.Model;
using ServiceStack.Html;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace SEC44NIPSS.Areas.Root.Pages.Main
{
    public class Downloadv2Model : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHostingEnvironment _hostingEnv;

        public Downloadv2Model(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnv)
        {
            _context = context;
            _userManager = userManager;
            _hostingEnv = hostingEnv;
        }
        [BindProperty]
        public string FileNameD { get; set; }
        

        public async Task<IActionResult> OnGetAsync(string filename)
        {
            FileNameD = filename;
           

            return Page();
            
        }
    }

    


        // Class to demonstrate use-case of drive's download file.
        //public class DownloadFile
        //{
        //    /// <summary>
        //    /// Download a Document file in PDF format.
        //    /// </summary>
        //    /// <param name="fileId">file ID of any workspace document format file.</param>
        //    /// <returns>byte array stream if successful, null otherwise.</returns>
        //    public static MemoryStream DriveDownloadFile(string fileId)
        //    {
        //        try
        //        {
        //            /* Load pre-authorized user credentials from the environment.
        //             TODO(developer) - See https://developers.google.com/identity for 
        //             guides on implementing OAuth2 for your application. */
        //            GoogleCredential credential = GoogleCredential
        //                .GetApplicationDefault()
        //                .CreateScoped(DriveService.Scope.Drive);

        //            // Create Drive API service.
        //            var service = new DriveService(new BaseClientService.Initializer
        //            {
        //                HttpClientInitializer = credential,
        //                ApplicationName = "Drive API Snippets"
        //            });

        //            var request = service.Files.Get(fileId);
        //            var stream = new MemoryStream();

        //            // Add a handler which will be notified on progress changes.
        //            // It will notify on each chunk download and when the
        //            // download is completed or failed.
        //            request.MediaDownloader.ProgressChanged +=
        //                progress =>
        //                {
        //                    switch (progress.Status)
        //                    {
        //                        case DownloadStatus.Downloading:
        //                            {
        //                                Console.WriteLine(progress.BytesDownloaded);
        //                                break;
        //                            }
        //                        case DownloadStatus.Completed:
        //                            {
        //                                Console.WriteLine("Download complete.");
        //                                break;
        //                            }
        //                        case DownloadStatus.Failed:
        //                            {
        //                                Console.WriteLine("Download failed.");
        //                                break;
        //                            }
        //                    }
        //                };
        //            request.Download(stream);

        //            return stream;
        //        }
        //        catch (Exception e)
        //        {
        //            // TODO(developer) - handle error appropriately
        //            if (e is AggregateException)
        //            {
        //                Console.WriteLine("Credential Not found");
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return null;
        //    }
        //}
    
}
