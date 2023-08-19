using System;
using System.Collections.Generic;
using System.Text;

namespace FilesProcessorUtility
{
     public class InputCommand
        {
            public InputCommand()
            {
                this.Params = new List<Params>();
            }
            public string Code { get; set; }

            public string Name { get; set; }

            public List<Params> Params { get; set; }
        }
        public class Params
        {
            public string ParamName { get; set; }

            public string ParamValue { get; set; }
        }
}
