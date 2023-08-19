using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FilesProcessorUtility.FileProcessorFacotry
{
    public class CreateFolderProcess : FileProcessorBase
    {
        public override Task<FileProcessorResponse> Execute(InputCommand inputCommand)
        {
            FileProcessorResponse fileProcessorResponse = new FileProcessorResponse { isSuccess = false };
           DirectoryInfo directory= Directory.CreateDirectory(Path.Combine(BaseFolderPath,inputCommand.Params[0].ParamValue));
             fileProcessorResponse.isSuccess = true;
            fileProcessorResponse.result = directory;
            return Task.Run(() => fileProcessorResponse);
        }
    }
}
