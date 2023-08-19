using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FilesProcessorUtility.FileProcessorFacotry
{
    public class FileWaitProcessor : FileProcessorBase
    {
        public override  async Task<FileProcessorResponse> Execute(InputCommand inputCommand)
        {
            await Validate(inputCommand).ConfigureAwait(false);
            Thread.Sleep(int.Parse(inputCommand.Params[0].ParamValue));
            return new FileProcessorResponse { isSuccess = true, result = int.Parse(inputCommand.Params[0].ParamValue) };
        }

        protected override async Task<bool> Validate(InputCommand inputCommand)
        {
            await base.Validate(inputCommand);
            if (inputCommand.Params.Count > 0)
            {
                if (!int.TryParse(inputCommand.Params[0].ParamValue, out int parsedValue) && parsedValue >= 0)
                    throw new ArgumentOutOfRangeException();
            }
            return true;
        }
    }
}
