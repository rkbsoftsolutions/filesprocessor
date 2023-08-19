using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilesProcessorUtility.FileProcessorFacotry
{
   public abstract class FileProcessorBase
    {

        /// <summary>
        /// Set defautl File Path as base root.
        /// </summary>
        protected virtual string BaseFolderPath { get; set; } = "D:\\";

        public abstract Task<FileProcessorResponse> Execute(InputCommand inputCommand);

        protected virtual async Task<bool> Validate(InputCommand inputCommand)
        {
            if (inputCommand.Params.Any(f => string.IsNullOrEmpty(f.ParamValue)))
            {
                throw new Exception("Not valid file inputs");
            }
            return await Task.Run(() => true);
        }
    }
}
