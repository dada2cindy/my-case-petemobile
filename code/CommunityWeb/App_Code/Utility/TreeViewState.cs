using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// TreeViewState 的摘要描述
/// </summary>
public class TreeViewState
{
    public void SaveTreeView(TreeView treeView, string key)
    {
        List<bool?> list = new List<bool?>();
        SaveTreeViewExpandedState(treeView.Nodes, list);
        HttpContext.Current.Session[key + treeView.ID] = list;
    }

    private void SaveTreeViewExpandedState(TreeNodeCollection nodes, List<bool?> list)
    {
        foreach (TreeNode node in nodes)
        {
            list.Add(node.Expanded);
            if (node.ChildNodes.Count > 0)
            {
                SaveTreeViewExpandedState(node.ChildNodes, list);
            }
        }
    }

    private int RestoreTreeViewIndex;

    public void RestoreTreeView(TreeView treeView, string key)
    {
        RestoreTreeViewIndex = 0;
        RestoreTreeViewExpandedState(treeView.Nodes,
            (List<bool?>)HttpContext.Current.Session[key + treeView.ID] ?? new List<bool?>());
    }

    private void RestoreTreeViewExpandedState(TreeNodeCollection nodes, List<bool?> list)
    {
        foreach (TreeNode node in nodes)
        {
            if (RestoreTreeViewIndex >= list.Count) return;

            node.Expanded = list[RestoreTreeViewIndex++];
            if (node.ChildNodes.Count > 0)
            { }
            RestoreTreeViewExpandedState(node.ChildNodes, list);
        }
    }
}