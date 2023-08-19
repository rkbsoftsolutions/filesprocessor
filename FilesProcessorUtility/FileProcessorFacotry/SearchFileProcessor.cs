using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FilesProcessorUtility.FileProcessorFacotry
{
    public class SearchFileProcessor : FileProcessorBase
    {
        public override async Task<FileProcessorResponse> Execute(InputCommand inputCommand)
        {
            var filePath = inputCommand.Params[0].ParamValue;
            var searchText = inputCommand.Params[1].ParamValue;
            var  fileContent =await System.IO.File.ReadAllLinesAsync(filePath);
            var countSearchText = 0;
            foreach (var line in fileContent)
            {
                if (line.Contains(searchText))
                {
                    countSearchText += 1;
                }
                
            }
            return new FileProcessorResponse { isSuccess = true, result = countSearchText };
        }
    }
}