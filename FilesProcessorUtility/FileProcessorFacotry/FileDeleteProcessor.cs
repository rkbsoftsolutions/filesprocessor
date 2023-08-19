using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace FilesProcessorUtility.FileProcessorFacotry
{
    public class FileDeleteProcessor : FileProcessorBase
    {
        public override async Task<FileProcessorResponse> Execute(InputCommand inputCommand)
        {
            if(await Validate(inputCommand))
            {
                File.Delete(Path.Combine(BaseFolderPath, inputCommand.Params[0].ParamValue));
            }
            return new FileProcessorResponse { isSuccess = true };
        }
    }
}
