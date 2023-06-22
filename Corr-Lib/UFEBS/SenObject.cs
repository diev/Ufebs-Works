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

using System.Xml;

namespace CorrLib.UFEBS;

public static class SenObject
{
    /// <summary>
    /// Извлекает sen:Object в отдельный файл XML.
    /// </summary>
    /// <param name="src">Исходный файл с sen:Object.</param>
    /// <param name="dst">Результирующий файл XML.</param>
    /// <returns>Создан ли результирующий файл.</returns>
    public static bool Extract(string src, string dst, string node = "sen:Object")
        => Extract(new FileInfo(src), new FileInfo(dst), node);

    /// <summary>
    /// Извлекает sen:Object в отдельный файл XML.
    /// Извлекает SWIFTDocument в отдельный файл XML.
    /// </summary>
    /// <param name="src">Исходный файл с sen:Object или SWIFTDocument (ED503).</param>
    /// <param name="dst">Результирующий файл XML.</param>
    /// <returns>Создан ли результирующий файл.</returns>
    public static bool Extract(FileInfo src, FileInfo dst, string node = "sen:Object")
    {
        const int bufferSize = 4096;

        try
        {
            using FileStream fs = new(src.FullName, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, FileOptions.SequentialScan);

            XmlReaderSettings settings = new()
            {
                IgnoreWhitespace = true
            };

            XmlReader senreader = XmlReader.Create(fs, settings);
            senreader.MoveToContent();

            //Контейнер для объекта
            senreader.ReadToFollowing(node);

            //Бинарные данные контейнера
            byte[] buffer = new byte[bufferSize];
            int readBytes = senreader.ReadElementContentAsBase64(buffer, 0, bufferSize);

            using FileStream writer = new(dst.FullName, FileMode.Create, FileAccess.Write, FileShare.Write);

            while (readBytes > 0)
            {
                writer.Write(buffer, 0, readBytes);
                readBytes = senreader.ReadElementContentAsBase64(buffer, 0, bufferSize);
            }

            writer.Flush(true);
            writer.Close();
        }

        catch (XmlException ex)
        {
            //AppTrace.Warning("{0} не XML файл: {1}", src.FullName, ex.Message);
        }
        catch (Exception ex)
        {
            //AppTrace.Error("{0} ошибка чтения: {1}", src.FullName, ex.Message);
        }

        //Обновить состояние!
        dst.Refresh();

        bool ok = false;
        try
        {
            ok = dst.Length > 0;
        }
        catch (Exception ex)
        {
            //TODO: UnauthorizedAccessException
            //AppTrace.Error("{0} ошибка доступа: {1}", src.FullName, ex.Message);
        }
        return ok;
    }
}
