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

using System.Collections;
using System.Xml.Linq;

namespace CorrLib;

public class SourceEDCollection : IReadOnlyList<string[]>
{
    private readonly string[][] _items;

    public string[] this[int index] => _items[index];

    public int Count => _items.Length;

    public SourceEDCollection(string path)
    {
        var xdoc = XDocument.Load(path);
        var root = xdoc.Root;

        var packet = new PacketEPD(root);
        int count = int.Parse(packet.EDQuantity);

        _items = new string[count][];
        var nodes = root.Elements();

        for (int i = 0; i < count; i++)
        {
            var ed = new ED100(nodes.ElementAt(i));
            var corr = ed.CorrClone();

            _items[i] = new string[]
            {
                corr.EDNo,
                corr.EDType,
                corr.Sum.ESum(),
                corr.PayerName,
                corr.PayeeName,
                corr.Purpose,
                string.Empty
            };
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<string[]> GetEnumerator()
    {
        for (int i = 0; i < _items.Length; i++)
        {
            yield return _items[i];
        }
    }
}
