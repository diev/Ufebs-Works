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
}
