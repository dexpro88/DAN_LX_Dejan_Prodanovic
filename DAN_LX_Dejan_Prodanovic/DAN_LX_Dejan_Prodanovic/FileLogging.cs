﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_LX_Dejan_Prodanovic
{
    class FileLogging
    {
        public static string texToFile = "";

        public static void LogToFile()
        {
            using (StreamWriter sw = File.AppendText("../../Log.txt"))
            {
                sw.WriteLine(texToFile);
            }
            texToFile = "";
        }
    }
}
