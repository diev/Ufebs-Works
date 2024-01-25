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
using System.Text.Json;

namespace CorrSWIFT;

internal static class Program
{
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    private static void Main()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance); //enable Windows-1251

        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        ApplicationConfiguration.Initialize();

        //Application.SetDefaultFont(new Font(new FontFamily("Microsoft Sans Serif"), 8.25f));
        string path = Path.GetDirectoryName(Application.ExecutablePath) ?? ".";
        string file = Path.Combine(path, "_font.json");

        if (File.Exists(file))
        {

            /*
{
  "Size": 9,
  "Style": 0,
  "Bold": false,
  "Italic": false,
  "Strikeout": false,
  "Underline": false,
  "FontFamily": {
    "Name": "Segoe UI"
  },
  "Name": "Segoe UI",
  "Unit": 3,
  "GdiCharSet": 204, //1
  "GdiVerticalFont": false,
  "OriginalFontName": null,
  "SystemFontName": "MessageBoxFont", //""
  "IsSystemFont": true, //false
  "Height": 16, //24
  "SizeInPoints": 9
}
            */

            var font = LoadFont(file);

            if (font != null)
            {
                Application.SetDefaultFont(font);
            }
        }
        
        Application.Run(new MainForm());
    }

    private static Font? LoadFont(string file)
    {
        var json = File.ReadAllBytes(file);

        using var doc = JsonDocument.Parse(json);
        var font = doc.RootElement;
        var name = font.GetProperty("FontFamily").GetProperty("Name").GetString();
        var size = font.GetProperty("Size").GetDouble();

        if (name == null) return null;

        return new Font(new FontFamily(name), (float)size);
    }
}
