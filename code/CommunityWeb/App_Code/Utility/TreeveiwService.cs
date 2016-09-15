using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Service;
using System.Web.UI.WebControls;
using WuDada.Core.Auth.Domain;

/// <summary>
/// TreeveiwService 的摘要描述
/// </summary>
public class TreeveiwService
{
    private AuthFactory m_AuthFactory;
    private IAuthService m_AuthService;

    public TreeveiwService()
    {
        m_AuthFactory = new AuthFactory();
        m_AuthService = m_AuthFactory.GetAuthService();
    }

    public TreeView Populate(string userId)
    {
        TreeView treeView = new TreeView();

        IList<MenuFuncVO> menuFuncList = m_AuthService.GetTopMenuFuncNoLazy();

        //var result = from data in menuFuncList
        //             where data.ParentMenu == null
        //             select data;

        foreach (MenuFuncVO menu in menuFuncList)
        {
            TreeNode treeNode = new TreeNode(menu.MenuFuncName, "");

            if (menu.SubFuncs.Count > 0)
            {
                foreach (MenuFuncVO subMenu in menu.SubFuncs)
                {
                    //TreeNode subTreeNode = new TreeNode(subMenu.MenuFuncName, subMenu.Id.ToString());
                    TreeNode subTreeNode = new TreeNode(subMenu.MenuFuncName, subMenu.MainPath);
                    //subTreeNode.NavigateUrl = "~/" + subMenu.MainPath;
                    treeNode.ChildNodes.Add(subTreeNode);
                }
            }

            treeView.Nodes.Add(treeNode);
        }
        return treeView;
    }
}