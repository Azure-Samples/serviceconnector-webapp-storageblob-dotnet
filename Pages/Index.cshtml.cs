using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WebStorageSample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string DisplayWords { get; private set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            string content = string.Format("Hello Service Connector! UTC Now: {0}.", DateTimeOffset.UtcNow.ToString());

            StorageHelper.UploadBlob(Environment.GetEnvironmentVariable(Const.ENDPOINT_ENV_KEY), Const.CONTAINER_NAME, Const.BLOB_NAME, content).Wait();
            DisplayWords = StorageHelper.GetBlob(Environment.GetEnvironmentVariable(Const.ENDPOINT_ENV_KEY), Const.CONTAINER_NAME, Const.BLOB_NAME).Result;
        }
    }
}
