using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace FilesProcessorUtility.FileProcessorFacotry
{
    public class QueryFolderFilesProcessor : FileProcessorBase
    {
       
        public override async Task<FileProcessorResponse> Execute(InputCommand inputCommand)
        {
            if (await Validate(inputCommand))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(inputCommand.Params[0].ParamValue);
                var fileNames = directoryInfo.GetFiles().Select(f => f.FullName);
                return new FileProcessorResponse {
                    isSuccess = true,
                    result = fileNames.ToArray()
                };
            }
            return new FileProcessorResponse { isSuccess = false };
        }

        protected override Task<bool> Validate(InputCommand inputCommand)
        {
            return base.Validate(inputCommand);
        }

    }
}
