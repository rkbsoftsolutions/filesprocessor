using FilesProcessorUtility.FileProcessorFacotry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

/* Developer : Rajinder Kumar
/* Used Factory Pattern */

namespace FilesProcessorUtility
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var userSelectedInput = PopulateCommands(FileFactoryCommands.InitiliseInputCommands());
            if (AcceptCommandInputs(userSelectedInput, userSelectedInput.Params.Count, 0))
            {
                var fileProcessorBase = FileFactoryAccessor.GetFileProcessor(userSelectedInput.Code);
                var reponse = await fileProcessorBase.Execute(userSelectedInput);
                DisplayReponse(userSelectedInput, reponse);
                Console.WriteLine("Do you want close app Y/N ?");
                var action = Console.ReadLine();
                if (action.ToUpper() == "Y")
                {
                    Environment.Exit(0);
                }
            }
        }

        private static void DisplayReponse(InputCommand userSelectedInput, FileProcessorResponse reponse)
        {
            switch (userSelectedInput.Code)
            {
                case "FILECOPY":
                    Console.WriteLine($"Successfully file created");
                    break;
                case "FILEDELETE":
                    Console.WriteLine($"Successfully file deleted");
                    break;
                case "QUERYFOLDERFILES":
                    foreach (var f in (IList)reponse.result)
                    {
                        Console.WriteLine($"{f}");
                    }
                    break;

                case "WAIT":
                    Console.WriteLine($"Processor will wait {Convert.ToInt32(reponse.result)} ");
                    break;
                case "DOWNLOADFILE":
                    if (reponse.isSuccess)
                    {
                        Console.WriteLine($"Download file is completed and Path is {reponse.result}");
                    }
                    break;
                case "CONDITINALCOUNTROW":
                    Console.WriteLine($"Totle Search keyword count is: {Convert.ToInt32(reponse.result)} ");
                    break;
                case "CREATEFOLDER":
                    Console.WriteLine($"Folder is created under: {(reponse.result as DirectoryInfo).FullName}");
                    break;
                default:
                    break;
            }
        }

        private static bool AcceptCommandInputs(InputCommand inputCommand,int paramCount,int currentCount)
        {
            if (inputCommand != null && paramCount >= 0 && paramCount != currentCount)
            {
                Console.WriteLine(inputCommand.Params[currentCount].ParamName);
                var inputvalue = Console.ReadLine();
                if (!string.IsNullOrEmpty(inputvalue))
                {
                    inputCommand.Params[currentCount].ParamValue = inputvalue;
                    currentCount = currentCount + 1;
                    AcceptCommandInputs(inputCommand, inputCommand.Params.Count, currentCount++);
                }
                else
                {
                    AcceptCommandInputs(inputCommand, inputCommand.Params.Count, currentCount);
                }

            }
            return true;
        }
        private static InputCommand PopulateCommands(Dictionary<string,InputCommand> InputCommands)
        {
            Console.Clear();
            foreach (var command in InputCommands)
            {
                Console.WriteLine($"Press {command.Key} for {command.Value.Name}");
            }
            var selectedCmd = Console.ReadLine();
            if (InputCommands.TryGetValue(selectedCmd, out InputCommand inputTest))
            {
                Console.WriteLine($"You choosed {inputTest.Name} Process");
                return inputTest;
            }
            else
            {
                Console.Clear();
                Console.WriteLine($"You choose wrong input ! Please select again");
                PopulateCommands(InputCommands);
            }
            return null;
        }
    }
}
