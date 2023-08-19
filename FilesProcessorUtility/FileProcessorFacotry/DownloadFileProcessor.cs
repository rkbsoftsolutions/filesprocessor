using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FilesProcessorUtility.FileProcessorFacotry
{
    public class DownloadFileProcessor : FileProcessorBase
    {
        public override async  Task<FileProcessorResponse> Execute(InputCommand inputCommand)
        {
            using (var client = new WebClient())
            {
                var filePathUrl = inputCommand.Params[0].ParamValue;
                var downloadFileName =Path.Combine(BaseFolderPath, inputCommand.Params[1].ParamValue);
                var fileContent =client.DownloadString(new Uri(filePathUrl));
                await System.IO.File.WriteAllTextAsync(downloadFileName, fileContent);
                return new FileProcessorResponse { isSuccess = true , result = downloadFileName };
            }
        }

       
    }
}
