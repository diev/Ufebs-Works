#region License
/*
Copyright 2022-2023 Dmitrii Evdokimov
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

namespace ReturnReplace;

public static class Program
{
    public static int Main(string[] args)
    {
        string path, outPath;

        if (args.Length == 0 || args[0] == "/?" || args[0] == "-?") // nothing
        {
            Console.WriteLine("Usage: Input|* [Output_]");
            return 1;
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
        }

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); //enable Windows-1251

        if (Directory.Exists(path))
        {
            foreach (string file in Directory.GetFiles(path))
            {
                Worker.ProcessFile(file, outPath);
            }
        }
        else if (path.Contains('*') || path.Contains('?'))
        {
            string dir = Path.GetDirectoryName(path) ?? Directory.GetCurrentDirectory();
            string mask = Path.GetFileName(path) ?? "*";

            foreach (string file in Directory.GetFiles(dir, mask))
            {
                Worker.ProcessFile(file, outPath);
            }
        }
        else if (File.Exists(path))
        {
            Worker.ProcessFile(path, outPath);
        }
        else
        {
            Console.WriteLine($"Input \"{path}\" not found");
        }

        #region finish
        while (true)
        {
            if (Console.ReadKey().Key == ConsoleKey.Spacebar)
            {
                break;
            }
        }

        return 0;
        #endregion finish
    }
}
