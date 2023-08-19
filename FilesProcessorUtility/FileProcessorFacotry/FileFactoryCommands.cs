using System;
using System.Collections.Generic;
using System.Text;

namespace FilesProcessorUtility.FileProcessorFacotry
{
   public  class FileFactoryCommands
    {
        public static Dictionary<string, InputCommand> InitiliseInputCommands()
        {
            
            Dictionary<string, InputCommand> InputCommands = new Dictionary<string, InputCommand>();
        InputCommands.Add("1", new InputCommand
            {
                Code = "FILECOPY",
                Name = "File Copy",
                Params = new List<Params> { new Params { ParamName = "Source File" }, new Params { ParamName = "Destination File" } }
            });
            InputCommands.Add("2", new InputCommand
            {
                Code = "FILEDELETE",
                Name = "File Delete",
                Params = new List<Params> { new Params { ParamName = "File Path" }
            }
            });
            InputCommands.Add("3", new InputCommand
            {
                Code = "QUERYFOLDERFILES",
                Name = "Query Folder Files",
                Params = new List<Params> { new Params { ParamName = "Folder Path" }
            }
            });
            InputCommands.Add("4", new InputCommand
            {
                Code = "DOWNLOADFILE",
                Name = "Download File",
                Params = new List<Params> { new Params { ParamName = "Source File" }, new Params { ParamName ="Output File" }
            }
            });
            InputCommands.Add("5", new InputCommand
            {
                Code = "WAIT",
                Name = "Wait",
                Params = new List<Params> { new Params { ParamName ="Please enter wait time:" }
            }
            });
            InputCommands.Add("6", new InputCommand
            {
                Code = "CONDITINALCOUNTROW",
                Name = "Conditinal Count Row",
                Params = new List<Params> { new Params { ParamName ="Source File" },new Params { ParamName = "string to search in rows" }
            }
            });
            InputCommands.Add("7", new InputCommand
            {
                Code = "CREATEFOLDER",
                Name = "Create Folder",
                Params = new List<Params> { new Params { ParamName = "New Folder Name" }
            }
            });

            return InputCommands;
        }
    }
}
