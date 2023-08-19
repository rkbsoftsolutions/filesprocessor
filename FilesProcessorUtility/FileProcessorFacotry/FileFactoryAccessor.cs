using System;
using System.Collections.Generic;
using System.Text;

namespace FilesProcessorUtility.FileProcessorFacotry
{
    public static class FileFactoryAccessor
    {
        public static FileProcessorBase GetFileProcessor(string code)
        {
            switch (code)
            { 
                case "FILECOPY":
                return new FileCopyProcessor();
                case "FILEDELETE":
                    return new FileDeleteProcessor();
                case "QUERYFOLDERFILES":
                    return new QueryFolderFilesProcessor();
                case "WAIT":
                    return new FileWaitProcessor();
                case "DOWNLOADFILE":
                    return new DownloadFileProcessor();
                case "CONDITINALCOUNTROW":
                    return new SearchFileProcessor();
                case "CREATEFOLDER":
                    return new CreateFolderProcess();
                default:
                throw new KeyNotFoundException();
            }
        }
    }
}
