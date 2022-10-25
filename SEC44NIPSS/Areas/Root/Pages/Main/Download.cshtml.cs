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

namespace SEC44NIPSS.Areas.Root.Pages.Main
{
    public class DownloadModel : PageModel
    {
        private readonly SEC44NIPSS.Data.NIPSSDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHostingEnvironment _hostingEnv;

        public DownloadModel(SEC44NIPSS.Data.NIPSSDbContext context, UserManager<IdentityUser> userManager, IHostingEnvironment hostingEnv)
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
           // //eu-west-3
           // RegionEndpoint bucketRegion = RegionEndpoint.EUWest3;
           // ////IAmazonS3 client = new AmazonS3Client(bucketRegion);

            // string accessKey = "AKIA4Y267QYKVUNR3IBW";
            // string secretKey = "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";
            // AmazonS3Client s3Client = new AmazonS3Client(new BasicAWSCredentials(accessKey, secretKey), Amazon.RegionEndpoint.EUWest3);
            //// string objectKey = "EMR" + "/" + filename;
            // string objectKey = filename;
            // //EMR is folder name of the image inside the bucket 
            // GetObjectRequest request = new GetObjectRequest();
            // request.BucketName = "nipss-bucket";
            // request.Key = objectKey;

            // var response = s3Client.GetObjectAsync(request);
            // response.Wait();
            // string pathx = System.Environment.ExpandEnvironmentVariables("%userprofile%/Downloads/" + filename);
            // //string pathx = @"C:\Users\Jinmcever\Downloads";
            // await response.Result.WriteResponseStreamToFileAsync(pathx, true, CancellationToken.None);
            //var strresp = response.Result.ResponseStream;
            ////await  response.WriteResponseStreamToFile("D:\\Test\\" + filename);
            ////return File(response.Result.ResponseStream);
            //using (var fileStream = new FileStream(pathx, FileMode.Create, FileAccess.Write))
            //{
            //    strresp.CopyTo(fileStream);
            //}
            return Page();
            //using (var webResponse = webRequest.GetResponse())
            //using (var webStream = webResponse.GetResponseStream())
            //{
            //    if (webStream != null)
            //    {
            //        Response.Clear();
            //        Response.Buffer = true;
            //        Response.ContentType = "application/pdf";
            //        response.Result.Headers("Content-Disposition",, "attachment; filename=\"test.pdf\"");
            //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //        webStream.CopyTo(Response.OutputStream);
            //        Response.Flush();
            //        Response.End();
            //    }
            //}
        }
    }
}
