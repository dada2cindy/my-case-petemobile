using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Post;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Service;
using WuDada.Core.SystemApplications.Domain;

public partial class admin_UC05_0513 : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private PostFactory m_PostFactory;
    private IPostService m_PostService;
    private WebLogService m_WebLogService;

    //療程分享NodeId=6
    private int m_NodeId = 6;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        m_WebLogService = new WebLogService();
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();

        if (!IsPostBack)
        {
            pnlContent.Visible = false;
            fillGridView();
            ShowMode();

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
        //DateTime? startDate = ConvertUtil.ToDateTimeMin(DateTime.Now);

        DateTime? startDate = null;
        string sortField = "ShowDate";
        bool sortDesc = true;

        //分頁
        AspNetPager1.RecordCount = m_PostService.CountPostListByNodeId(m_NodeId, false, startDate);
        int pageIndex = (AspNetPager1.CurrentPageIndex - 1);
        int pageSize = AspNetPager1.PageSize;

        gvList.DataSource = m_PostService.GetPostListByNodeId(m_NodeId, false, startDate, pageIndex, pageSize, sortField, sortDesc);
        gvList.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        PostVO postVO = new PostVO();
        postVO.Title = txtTitle.Text.Trim();
        postVO.SortNo = int.Parse(txtSortNo.Text.Trim());
        postVO.Node = m_PostService.GetNodeById(m_NodeId);
        postVO.HtmlContent = ckeContent.value;
        postVO.PicFileName = m_PicFileName;
        postVO.Flag = int.Parse(ddlFlag.SelectedValue);
        if (!string.IsNullOrEmpty(txtShowDate.Text.Trim()))
        {
            postVO.ShowDate = DateTime.Parse(txtShowDate.Text.Trim());
        }
        //postVO.LinkUrl = txtLinkUrl.Text.Trim();
        //if (!string.IsNullOrEmpty(txtLinkUrl.Text.Trim()))
        //{
        //    postVO.Type = 1;
        //}
        //else
        //{
        //    postVO.Type = 0;
        //}
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
        txtTitle.Text = string.Empty;
        txtSortNo.Text = string.Empty;
        ckeContent.value = string.Empty;
        ddlFlag.SelectedValue = string.Empty;
        txtShowDate.Text = string.Empty;
        //txtLinkUrl.Text = string.Empty;
        pnlContent.Visible = false;
        btnShowAdd.Enabled = true;
    }

    private string GetPic(string fileName)
    {
        return "<img src='../../upload/" + fileName + "' width='145' height='108' border='0'>";
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
                txtTitle.Text = postVO.Title;
                txtSortNo.Text = postVO.SortNo.ToString();
                ckeContent.value = postVO.HtmlContent;
                ddlFlag.SelectedValue = postVO.Flag.ToString();
                if (postVO.ShowDate != null)
                {
                    txtShowDate.Text = postVO.ShowDate.Value.ToShortDateString();
                }
                //if (postVO.Type == 1)
                //{
                //    txtLinkUrl.Text = postVO.LinkUrl;
                //}
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
        try
        {
            PostVO postVO = m_PostService.GetPostById(m_Mode);
            postVO.Title = txtTitle.Text.Trim();
            postVO.SortNo = int.Parse(txtSortNo.Text.Trim());
            postVO.Node = m_PostService.GetNodeById(m_NodeId);
            postVO.HtmlContent = ckeContent.value;
            postVO.PicFileName = m_PicFileName;
            postVO.Flag = int.Parse(ddlFlag.SelectedValue);
            if (!string.IsNullOrEmpty(txtShowDate.Text.Trim()))
            {
                postVO.ShowDate = DateTime.Parse(txtShowDate.Text.Trim());
            }
            //postVO.LinkUrl = txtLinkUrl.Text.Trim();
            //if (!string.IsNullOrEmpty(txtLinkUrl.Text.Trim()))
            //{
            //    postVO.Type = 1;
            //}
            //else
            //{
            //    postVO.Type = 0;
            //}
            m_PostService.UpdatePost(postVO);
            m_WebLogService.AddSystemLog(MsgVO.Action.修改, postVO);
            fillGridView();
            ClearUI();
            ShowMode();
        }
        catch (Exception ex)
        {
            m_Log.Error(ex);
            lblMsg.Text = ex.ToString();
        }
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //GridView gv = (GridView)sender;
        //if (e.Row.RowIndex != -1)
        //{
        //    Control ctrl = e.Row;
        //    int postId = int.Parse(UIHelper.FindHiddenValue(ref ctrl, "hdnPostId"));
        //    PostVO postVO = m_PostService.GetPostById(postId);
        //}
    }

    protected void btnShowAdd_Click(object sender, EventArgs e)
    {
        ClearUI();
        m_PicFileName = string.Empty;
        ShowMode();
        pnlContent.Visible = true;
        btnShowAdd.Enabled = false;
    }

    protected void ddlSelect_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillGridView();
    }

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