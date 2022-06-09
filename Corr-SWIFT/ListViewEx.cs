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

using CorrLib;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorrSWIFT;

internal static class ListViewEx
{
    internal static void ClearSelection(this ListView listView)
    {
        //if (listView.SelectedItems.Count > 0)
        //{
        //    listView.SelectedItems[0].Selected = false;
        //}

        foreach (ListViewItem item in listView.SelectedItems)
        {
            item.Selected = false;
        }
    }

    internal static ListViewItem? SelectedItem(this ListView listView)
    {
        return listView.SelectedItems.Count > 0
            ? listView.SelectedItems[0]
            : null;
    }

    internal static int SelectedIndex(this ListView listView)
    {
        return listView.SelectedItems.Count > 0
            ? listView.SelectedItems[0].Index
            : -1;
    }

    internal static void SelectIndex(this ListView listView, int value)
    {
        listView.ClearSelection();
        listView.Items[value].Selected = true;
    }

    internal static bool SelectFirst(this ListView listView)
    {
        if (listView.Items.Count > 0)
        {
            int index = 0;
            listView.ClearSelection();
            listView.Items[index].Selected = true;

            return true;
        }

        return false;
    }

    internal static bool PrevEnabled(this ListView listView)
    {
        int count = listView.Items.Count;
        int index = listView.SelectedIndex();

        return count > 0 && --index >= 0;
    }

    internal static bool SelectPrev(this ListView listView)
    {
        int count = listView.Items.Count;
        int index = listView.SelectedIndex();

        if (count > 0 && --index >= 0)
        {
            listView.ClearSelection();
            listView.Items[index].Selected = true;

            return true;
        }

        return false;
    }

    internal static bool NextEnabled(this ListView listView)
    {
        int count = listView.Items.Count;
        int index = listView.SelectedIndex();

        return count > 0 && ++index < count - 1;
    }

    internal static bool SelectNext(this ListView listView)
    {
        int count = listView.Items.Count;
        int index = listView.SelectedIndex();

        if (count > 0 && ++index < count - 1)
        {
            listView.ClearSelection();
            listView.Items[index].Selected = true;

            return true;
        }

        return false;
    }

    internal static bool SelectLast(this ListView listView)
    {
        int count = listView.Items.Count;
        int index = listView.SelectedIndex();

        if (count > 0 && ++index == count - 1)
        {
            listView.ClearSelection();
            listView.Items[index].Selected = true;

            return true;
        }

        return false;
    }

    internal static void AddItem(this ListView listView, ED100 ed)
    {
        var item = new ListViewItem(ed.EDNo);

        var itemType = item.SubItems.Add(ed.EDType);
        var itemSum = item.SubItems.Add(ed.Sum.ESum());
        var itemName = item.SubItems.Add(ed.PayerName);
        var itemPayee = item.SubItems.Add(ed.PayeeName);
        var itemPurpose = item.SubItems.Add(ed.Purpose);

        if (ed.PayerName.Length > 160)
        {
            item.UseItemStyleForSubItems = false;
            itemName.ForeColor = Color.Red;
        }

        if (ed.Purpose.Length > 210)
        {
            item.UseItemStyleForSubItems = false;
            itemPurpose.ForeColor = Color.Red;
        }

        listView.Items.Add(item);
    }
}
