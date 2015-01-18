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

public partial class admin_UC05_0512 : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private PostFactory m_PostFactory;
    private IPostService m_PostService;
    private WebLogService m_WebLogService;
    
    private int m_NodeId = 2;

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
            btnSold.Visible = false;
            btnDelete.Visible = false;
        }
        else
        {
            btnAdd.Visible = false;
            btnSold.Visible = true;
            btnDelete.Visible = true;
        }
    }

    private void fillGridView()
    {
        //搜尋條件
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("Flag", "1");
        conditions.Add("KeyWord", txtSearchKeyword.Text.Trim());
        conditions.Add("Type", ddlSearchType.SelectedValue);
        if (!string.IsNullOrEmpty(txtSearchShowDateStart.Text.Trim()))
        {
            conditions.Add("ShowDateStart", txtSearchShowDateStart.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtSearchShowDateEnd.Text.Trim()))
        {
            conditions.Add("ShowDateEnd", txtSearchShowDateEnd.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtSearchCloseDateStart.Text.Trim()))
        {
            conditions.Add("CloseDateStart", txtSearchCloseDateStart.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtSearchCloseDateEnd.Text.Trim()))
        {
            conditions.Add("CloseDateEnd", txtSearchCloseDateEnd.Text.Trim());
        }
        

        //分頁
        AspNetPager1.RecordCount = m_PostService.GetPostCount(conditions);
        lblTotalCount.Text = string.Format("共查出 {0} 筆資料", AspNetPager1.RecordCount.ToString());
        int pageIndex = (AspNetPager1.CurrentPageIndex - 1);
        int pageSize = AspNetPager1.PageSize;
        conditions.Add("PageIndex", pageIndex.ToString());
        conditions.Add("PageSize", pageSize.ToString());
        conditions.Add("Order", "order by p.CloseDate desc, p.ShowDate desc, p.Title");

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
        postVO.Node = m_PostService.GetNodeById(m_NodeId);
        //postVO.PicFileName = m_PicFileName;
        postVO.Flag = 1;
        //if (!string.IsNullOrEmpty(txtShowDate.Text.Trim()))
        //{
        //    postVO.ShowDate = DateTime.Parse(txtShowDate.Text.Trim());
        //}
        //if (!string.IsNullOrEmpty(txtCloseDate.Text.Trim()))
        //{
        //    postVO.ShowDate = DateTime.Parse(txtCloseDate.Text.Trim());
        //}
        m_PostService.CreatePost(postVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.新增, postVO);
        ClearUI();
        fillGridView();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        PostVO postVO = m_PostService.GetPostById(m_Mode);
        postVO.Flag = 0;
        m_PostService.UpdatePost(postVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.刪除, postVO, "", string.Format("單號:{0}", postVO.PostId));
        ClearUI();
        fillGridView();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGridView();
    }

    private void ClearUI()
    {
        m_Mode = 0;
        //m_PicFileName = string.Empty;
        //ltlImg.Text = string.Empty;
        UIHelper.ClearUI(pnlContent);
        pnlContent.Visible = false;
        btnShowAdd.Enabled = true;
        rfClodeDate.Visible = false;
        rfCustomField2.Visible = false;
        rfSellPrice.Visible = false;        
    }

    //private string GetPic(string fileName)
    //{
    //    return "<img src='../../upload/" + fileName + "' width='145' height='108' border='0'>";
    //}

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
                UIHelper.FillUI(pnlContent, postVO);
                ShowMode();
                if (postVO.Type == 1)
                {
                    btnSold.Visible = false;
                }
                pnlContent.Visible = true;
                rfClodeDate.Visible = true;
                rfCustomField2.Visible = true;
                rfSellPrice.Visible = true;
                txtTitle.Enabled = false;
                break;
            //case "myDel":
            //    postVO.Flag = 0;
            //    m_PostService.UpdatePost(postVO);
            //    m_WebLogService.AddSystemLog(MsgVO.Action.刪除, postVO, "", string.Format("單號:{0}", postVO.PostId));
            //    break;

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

    protected void btnSold_Click(object sender, EventArgs e)
    {
        try
        {
            PostVO postVO = m_PostService.GetPostById(m_Mode);
            UIHelper.FillVO(pnlContent, postVO);
            postVO.Type = 1;
            m_PostService.UpdatePost(postVO);
            m_WebLogService.AddSystemLog(MsgVO.Action.售出, postVO, "", string.Format("單號:{0}", postVO.PostId));
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

    //protected void btnUpliad_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        HttpFileCollection hfc = Request.Files;
    //        for (int i = 0; i < hfc.Count; i++)
    //        {
    //            HttpPostedFile hpf = hfc[i];
    //            if (hpf.ContentLength > 0)
    //            {
    //                string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + System.IO.Path.GetFileName(hpf.FileName);
    //                hpf.SaveAs(Server.MapPath("~\\") + "\\upload\\" + fileName);
    //                ltlImg.Text = GetPic(fileName);
    //                m_PicFileName = fileName;
    //            }
    //            else
    //            {
    //                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS("請點選您要上傳的照片!"), false);
    //                return;
    //            }
    //        }
    //    }
    //    catch
    //    {
    //        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS("檔案傳輸錯誤!"), false);
    //        return;
    //    }
    //}
}