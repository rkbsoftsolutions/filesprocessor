using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FilesProcessorUtility.FileProcessorFacotry
{
    public class FileCopyProcessor : FileProcessorBase
    {
        public override async Task<FileProcessorResponse> Execute(InputCommand inputCommand)
        {
            if(await Validate(inputCommand).ConfigureAwait(false))
            {
                File.Copy(Path.Combine(BaseFolderPath, inputCommand.Params[0].ParamValue), 
                    Path.Combine(inputCommand.Params[1].ParamValue),true);
               
            }
            return new FileProcessorResponse { isSuccess = true };
        }

        protected async override Task<bool> Validate(InputCommand inputCommand)
        {
            if(inputCommand.Params.Any(f => string.IsNullOrEmpty(f.ParamValue)))
            {
                throw new Exception("Not valid file inputs");
                
            }
            return await Task.Run(() => true);
        }
    }
}
