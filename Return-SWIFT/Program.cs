#region License
/*
Copyright 2022-2024 Dmitrii Evdokimov
Open source software

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
#endregion

using System.Text;

using CorrLib.UFEBS;

namespace ReturnSWIFT;

internal class Program
{
    public static List<string> O900in = [];
    public static List<string> O900out = [];

    public static List<string> O950in = [];
    public static List<string> O950out = [];

    static void Main(string[] args)
    {
        string path, outPath;

        if (args.Length == 0 || args[0] == "/?" || args[0] == "-?") // nothing
        {
            Console.WriteLine("Usage: Input|* [Output_.xml]");
            Console.WriteLine("(Use input mask: 4030702000ED503[dd]*.txt)");
            return;
        }
        else // Input specified
        {
            path = Path.GetFullPath(args[0]);
        }

        if (args.Length == 1) // Output default
        {
            string p = Path.GetDirectoryName(path) ?? Directory.GetCurrentDirectory();
            outPath = Path.EndsInDirectorySeparator(p) ? p : p + Path.DirectorySeparatorChar;
        }
        else if (Directory.Exists(args[1]))
        {
            string p = args[1];
            outPath = Path.EndsInDirectorySeparator(p) ? p : p + Path.DirectorySeparatorChar;
        }
        else
        {
            outPath = args[1];

            if (Path.EndsInDirectorySeparator(outPath))
            {
                Directory.CreateDirectory(outPath);
            }
        }

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); //enable Windows-1251

        var ed807 = AppContext.GetData("ED807")?.ToString();

        if (ed807 is null || !File.Exists(ed807))
        {
            Console.WriteLine("Укажите в конфиге файл справочника ED807.xml для лучшего заполнения полей.");
        }
        else
        {
            ED807Finder.ED807File = ed807;
        }

        Console.WriteLine($"--- Документы --- {path}");

        if (Directory.Exists(path))
        {
            foreach (string file in Directory.GetFiles(path))
            {
                Worker.PreprocessFile(file, outPath);
            }

            foreach (string file in Directory.GetFiles(path))
            {
                Worker.ProcessFile(file, outPath);
            }
        }
        else if (path.Contains('*') || path.Contains('?'))
        {
            string dir = Path.GetDirectoryName(path) ?? Directory.GetCurrentDirectory();
            string mask = Path.GetFileName(path) ?? "4030702000ED503*.txt";

            if (Directory.Exists(dir))
            {
                foreach (string file in Directory.GetFiles(dir, "*"))
                {
                    Worker.PreprocessFile(file, outPath);
                }

                foreach (string file in Directory.GetFiles(dir, mask))
                {
                    Worker.ProcessFile(file, outPath);
                }
            }
            else
            {
                Console.WriteLine(@$"Input dir ""{dir}"" not found");
            }
        }
        else if (File.Exists(path))
        {
            Worker.PreprocessFile(path, outPath);
            Worker.ProcessFile(path, outPath);
        }
        else
        {
            Console.WriteLine(@$"Input file ""{path}"" not found");
        }

        for (int i = 0; i < O900in.Count; i++)
        {
            string inFile = O900in[i];
            string outFile = O900out[i];

            try
            {
                Worker.Process900(inFile, outFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@$"Ошибка в файле авизо ""{inFile}"". {ex.Message}");
            }
        }

        for (int i = 0; i < O950in.Count; i++)
        {
            string inFile = O950in[i];
            string outFile = O950out[i];

            try
            {
                Worker.Process950(inFile, outFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(@$"Ошибка в файле выписки ""{inFile}"". {ex.Message}");
            }
        }

        #region finish
        Console.WriteLine("\nJob done. Press Spacebar.");

        while (true)
        {
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                break;
            }
        }
        #endregion finish
    }
}
