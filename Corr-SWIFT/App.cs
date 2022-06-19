#region License
/*
Copyright 2022 Dmitrii Evdokimov
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

namespace CorrSWIFT;

public static class App
{
    private const string VersionDate = "2022-06-20"; //TODO Versioning in .NET6+

    public static void About()
    {
        string config = Path.ChangeExtension(Application.ExecutablePath, "runtimeconfig.json");
        string text =
            $@"Программа дооформления документов из УФЭБС в SWIFT.

Версия {Application.ProductVersion} ({VersionDate})

Задайте параметры в меню Файл\Параметры...
Сохраняются они в файле
{config}

Можно сохранить с именем Профиля, и тогда
его можно указать в командной строке.";
        //Также пути можно переопределить в командной строке:
        //    Input\[*.xml] [Output\[*_.txt]]";

        MessageBox.Show(text, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
