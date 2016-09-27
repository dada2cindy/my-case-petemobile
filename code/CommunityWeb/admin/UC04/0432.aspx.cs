using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Post;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Service;
using WuDada.Core.SystemApplications.Domain;
using System.Collections.Generic;

public partial class admin_UC04_0432 : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private PostFactory m_PostFactory;
    private IPostService m_PostService;
    private WebLogService m_WebLogService;

    //手機NodeId=3
    private int m_NodeId = 3;

    private int m_Mode
    {
        get { if (ViewState["mode"] == null) { ViewState["mode"] = 0; } return int.Parse(ViewState["mode"].ToString()); }
        set { ViewState["mode"] = value; }
    }
    private string m_PicFileName
    {
        get { if (ViewState["picfilename"] == null) { ViewState["picfilename"] = string.Empty; } return ViewState["picfilename"].ToString(); }
        set { ViewState["picfilename"] = value; }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        m_WebLogService = new WebLogService();
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();

        if (!IsPostBack)
        {
            initDDL();
            pnlContent.Visible = false;
            fillGridView();
            ShowMode();
        }
    }

    private void initDDL()
    {
        IList<NodeVO> nodeList = m_PostService.GetNodeListByParentId(9);
        ddlWarrantySuppliers.Items.Clear();
        ddlWarrantySuppliers.Items.Add(new ListItem("請選擇品牌", ""));
        foreach (NodeVO vo in nodeList)
        {
            ddlWarrantySuppliers.Items.Add(new ListItem(vo.Name, vo.Name));
        }
    }

    private void ShowMode()
    {
        if (m_Mode == 0)
        {
            btnAdd.Visible = true;
            btnSave.Visible = false;
        }
        else
        {
            btnAdd.Visible = false;
            btnSave.Visible = true;
        }
    }

    private void fillGridView()
    {
        //搜尋條件
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("NodeId", m_NodeId.ToString());
        conditions.Add("ProductKeyWord", txtSearchKeyword.Text.Trim());

        //分頁
        AspNetPager1.RecordCount = m_PostService.GetPostCount(conditions);
        lblTotalCount.Text = string.Format("共查出 {0} 筆資料", AspNetPager1.RecordCount.ToString());

        int pageIndex = (AspNetPager1.CurrentPageIndex - 1);
        int pageSize = AspNetPager1.PageSize;
        conditions.Add("PageIndex", pageIndex.ToString());
        conditions.Add("PageSize", pageSize.ToString());
        conditions.Add("Order", string.Format("order by {0}", "p.WarrantySuppliers, p.SortNo, p.Title"));

        gvList.DataSource = m_PostService.GetPostList(conditions);
        gvList.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        PostVO postVO = new PostVO();
        UIHelper.FillVO(pnlContent, postVO);
        postVO.PicFileName = m_PicFileName;
        postVO.HtmlContent = fckHtmlContent.value;
        postVO.Node = m_PostService.GetNodeById(m_NodeId);
        m_PostService.CreatePost(postVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.新增, postVO);
        ClearUI();
        fillGridView();
    }

    private void ClearUI()
    {
        m_Mode = 0;
        m_PicFileName = string.Empty;
        ltlImg.Text = string.Empty;
        UIHelper.ClearUI(pnlContent);
        fckHtmlContent.value = string.Empty;
        pnlContent.Visible = false;
        btnShowAdd.Enabled = true;
        ddlIsHot.SelectedValue = "False";
        ddlIsNew.SelectedValue = "False";
    }

    private string GetPic(string fileName)
    {
        return "<img src='../../upload/" + fileName + "' width='340' height='340' border='0'>";
    }

    protected void gvList_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        int postId = int.Parse(e.CommandArgument.ToString());
        PostVO postVO = m_PostService.GetPostById(postId);
        switch (cmdName)
        {
            case "myModify":
                ClearUI();
                m_Mode = postId;
                m_PicFileName = postVO.PicFileName;
                ltlImg.Text = GetPic(m_PicFileName);
                UIHelper.FillUI(pnlContent, postVO);
                fckHtmlContent.value = postVO.HtmlContent;
                ShowMode();
                pnlContent.Visible = true;
                break;
            case "myDel":
                m_PostService.DeletePost(postVO);
                m_WebLogService.AddSystemLog(MsgVO.Action.刪除, postVO);
                break;
            default:
                break;
        }
        fillGridView();
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearUI();
        ShowMode();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PostVO postVO = m_PostService.GetPostById(m_Mode);
        UIHelper.FillVO(pnlContent, postVO);
        postVO.PicFileName = m_PicFileName;
        postVO.HtmlContent = fckHtmlContent.value;
        m_PostService.UpdatePost(postVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.修改, postVO);
        fillGridView();
        ClearUI();
        ShowMode();
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        if (e.Row.RowIndex != -1)
        {
            Control ctrl = e.Row;
            int postId = int.Parse(UIHelper.FindHiddenValue(ref ctrl, "hdnPostId"));
            PostVO postVO = m_PostService.GetPostById(postId);
            NodeVO node = m_PostService.GetNodeById(postVO.Node.NodeId);
        }
    }

    protected void btnShowAdd_Click(object sender, EventArgs e)
    {
        ClearUI();
        m_PicFileName = string.Empty;
        ShowMode();
        pnlContent.Visible = true;
        btnShowAdd.Enabled = false;
    }

    //protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    fillGridView();
    //}

    protected void btnUpliad_Click(object sender, EventArgs e)
    {
        try
        {
            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + System.IO.Path.GetFileName(hpf.FileName);
                    hpf.SaveAs(Server.MapPath("~\\") + "\\upload\\" + fileName);
                    ltlImg.Text = GetPic(fileName);
                    m_PicFileName = fileName;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS("請點選您要上傳的照片!"), false);
                    return;
                }
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS("檔案傳輸錯誤!"), false);
            return;
        }
    }
}