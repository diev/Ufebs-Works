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

using CorrLib.UFEBS;

using System.Collections;

namespace CorrLib;

public class SourceFiles : IReadOnlyList<string[]>
{
    private readonly string[][] _items;

    public string[] this[int index]
        => _items[index];

    public int Count
        => _items.Length;

    public SourceFiles(string directory, string mask)
        : this(Directory.GetFiles(directory == string.Empty
            ? "."
            : directory,
            mask))
    { }

    public SourceFiles(string[] files)
    {
        _items = new string[files.Length][];

        for (int i = 0; i < files.Length; i++)
        {
            string file = files[i];
            var packet = new PacketEPD(file);

            _items[i] = new string[]
            {
                files[i],
                packet.EDType,
                packet.EDQuantity,
                packet.Sum.DisplaySum(),
                string.Empty //TODO File.Exists?
            };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public IEnumerator<string[]> GetEnumerator()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            yield return _items[i];
        }
    }
}
