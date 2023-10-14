using FilesProcessorUtility.FileProcessorFacotry;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

/* Developer : Rajinder Kumar
/* Used Factory Pattern */

namespace FilesProcessorUtility
{
    public class BackTracking
    {
        public void StringPermutation(string word, int start, int end)
        {
            if (start == end)
            {
                Console.WriteLine(word);
            }
            else
            {
                for (int i = start; i <= end; i++)
                {
                    Swap(ref word, start, i);
                    StringPermutation(word, start + 1, end);
                    Swap(ref word, start, i);
                }
            }
        }
        private void Swap(ref string word, int start, int end)
        {
            char[] arr = word.ToCharArray();
            char temp = arr[start];
            arr[start] = arr[end];
            arr[end] = temp;

            word = new string(arr);
        }

        public void TwodArray()
        {
            
           var fileData= File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "TextFile1.txt"));
            List<List<int>> arr = new List<List<int>>();

                var lst = fileData.TrimEnd().Split("\r\n").ToList();

                lst.ForEach(arrTemp =>
                {
                   arr.Add(arrTemp.Split(" ").Select(x => Convert.ToInt32(x)).ToList());
                });

            var LtoR = DiagonalLtoR(arr, 0, arr.Count);
            var RtoL = DiagonalRtoL(arr, 0, arr.Count);
            var resutl = (LtoR - RtoL) >0 ? (LtoR - RtoL) : -(LtoR - RtoL);

        }

        private static int DiagonalLtoR(List<List<int>> number,int start,int end)
        {
            var diagonal = 0;
            for (int i = start; i < end; i++)
            {
                diagonal = ProcessDiagonal(number, diagonal, i);

            }

            return diagonal;
        }
        private static int DiagonalRtoL(List<List<int>> number, int start, int end)
        {
            var diagonal = 0;
            var innerArrayStart = end-1;
            for (int i = start; i < end; i++)
            { 
                diagonal = ProcessDiagonal(number, diagonal, i, innerArrayStart);
                innerArrayStart--;

            }

            return diagonal;
        }

        private static int ProcessDiagonal(List<List<int>> number, int diagonal, int i, int innerArrayStart=-1)
        {
            var innerArray = number[i].ToArray();
            var cl = innerArray.Length;
            if (i < cl)
            {
                var v = (innerArrayStart == -1 ? innerArray[i] : innerArray[innerArrayStart]);
                diagonal = diagonal + v;
               
            }

            return diagonal;
        }

        private static int GetMaxVal(int[] array, int size)
        {
            var maxVal = array[0];
            for (int i = 1; i < size; i++)
                if (array[i] > maxVal)
                    maxVal = array[i];
            return maxVal;
        }


        public static List<int> countingSort(List<int> array)
        {

            var size = array.Count;
            var maxElement = GetMaxVal(array.ToArray(), size);
            var occurrences = new int[maxElement+1];
            for (int i = 0; i < maxElement; i++)
            {
                var v = array[i];
                occurrences[v]++;
            }
            //for (int i = 0, j = 0; i <= maxElement; i++)
            //{
            //    while (occurrences[i] > 0)
            //    {
            //        array[j] = i;
            //        j++;
            //        occurrences[i]--;
            //    }
            //}

            return occurrences.ToList();
        }

    }


    class Program
    {
        static async Task Main(string[] args)
        {
        var fileData = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "TextFile1.txt"));
            List<int> arr = new List<int>();

            var lst = fileData.Split(" ").ToList();

        lst.ForEach(arrTemp =>
        {
            arr.Add(Convert.ToInt32(arrTemp));
        });

            BackTracking.countingSort(arr);

        //new BackTracking().TwodArray();
        //var userSelectedInput = PopulateCommands(FileFactoryCommands.InitiliseInputCommands());
        //if (AcceptCommandInputs(userSelectedInput, userSelectedInput.Params.Count, 0))
        //{
        //    var fileProcessorBase = FileFactoryAccessor.GetFileProcessor(userSelectedInput.Code);
        //    var reponse = await fileProcessorBase.Execute(userSelectedInput);
        //    DisplayReponse(userSelectedInput, reponse);
        //    Console.WriteLine("Do you want close app Y/N ?");
        //    var action = Console.ReadLine();
        //    if (action.ToUpper() == "Y")
        //    {
        //        Environment.Exit(0);
        //    }
        //}

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
