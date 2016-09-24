using System;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Post;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Service;
using WuDada.Core.SystemApplications.Domain;
using System.Collections.Generic;
using System.Data;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Service;
using WuDada.Core.Auth.Domain;

public partial class admin_UC05_0512 : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private PostFactory m_PostFactory;
    private IPostService m_PostService;
    private AuthFactory m_AuthFactory;
    private IAuthService m_AuthService;
    private WebLogService m_WebLogService;
    private SessionHelper m_SessionHelper;
    private ConfigHelper m_ConfigHelper;

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
        m_ConfigHelper = new ConfigHelper();
        m_WebLogService = new WebLogService();
        m_PostFactory = new PostFactory();
        m_AuthFactory = new AuthFactory();
        m_SessionHelper = new SessionHelper();
        m_AuthService = m_AuthFactory.GetAuthService();
        m_PostService = m_PostFactory.GetPostService();

        if (!IsPostBack)
        {
            InitDDL();
            pnlContent.Visible = false;
            fillGridView();
            ShowMode();         
        }
    }

    private void ShowMode()
    {
        if (m_Mode == 0)
        {
            txtTitle.Enabled = true;
            btnAdd.Visible = true;
            btnSold.Visible = false;
            btnDelete.Visible = false;
            txtQuantity.Enabled = true;
            txtCloseDate.Enabled = true;
            calendar2.Visible = true;
        }
        else
        {
            btnAdd.Visible = false;
            btnSold.Visible = true;
            btnDelete.Visible = true;
            txtQuantity.Enabled = false;
            txtCloseDate.Enabled = false;
            calendar2.Visible = false;
        }

        if (!m_SessionHelper.IsAdmin)
        {
            btnSearchExport.Visible = false;
        }

        if (m_ConfigHelper.OnlyAdminCreate && !m_SessionHelper.IsAdmin)
        {
            btnSearchExport.Visible = false;
            btnSold.Visible = false;
            btnAdd.Visible = false;
            btnShowAdd.Visible = false;
            btnDelete.Visible = false;
        }
    }

    private void fillGridView()
    {
        //搜尋條件
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("Flag", "1");
        conditions.Add("NodeId", m_NodeId.ToString());
        conditions.Add("KeyWord", txtSearchKeyword.Text.Trim());
        conditions.Add("Type", ddlSearchType.SelectedValue);
        conditions.Add("CustomField1", ddlSearchCustomField1.SelectedValue);
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

        if (!string.IsNullOrEmpty(ddlSearchStore.SelectedValue))
        {
            conditions.Add("Store", ddlSearchStore.SelectedValue);
        }

        //分頁
        AspNetPager1.RecordCount = m_PostService.GetPostCount(conditions);
        lblTotalCount.Text = string.Format("共查出 {0} 筆資料", AspNetPager1.RecordCount.ToString());
        if (AspNetPager1.RecordCount > 0)
        {
            btnSearchExport.Visible = true;
        }
        else
        {
            btnSearchExport.Visible = false;
        }
        int pageIndex = (AspNetPager1.CurrentPageIndex - 1);
        int pageSize = AspNetPager1.PageSize;
        conditions.Add("PageIndex", pageIndex.ToString());
        conditions.Add("PageSize", pageSize.ToString());
        conditions.Add("Order", string.Format("order by {0}", ddlSearchOrder.SelectedValue));
        //conditions.Add("Order", "order by p.CloseDate desc, p.ShowDate desc, p.Title");

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
        postVO.NeedUpdate = true;
        postVO.CreatedBy = m_SessionHelper.LoginUser.FullNameInChinese;
        postVO.UpdatedBy = m_SessionHelper.LoginUser.FullNameInChinese;
        postVO.CreatedDate = DateTime.Now;
        postVO.UpdatedDate = DateTime.Now;
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
        new Thread(new ThreadStart(() => ApiUtil.UpdatePostToServer(2))).Start();
        ClearUI();
        fillGridView();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        PostVO postVO = m_PostService.GetPostById(m_Mode);
        postVO.Flag = 0;
        postVO.NeedUpdate = true;
        postVO.UpdatedBy = m_SessionHelper.LoginUser.FullNameInChinese;
        postVO.UpdatedDate = DateTime.Now;
        m_PostService.UpdatePost(postVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.刪除, postVO, "", string.Format("單號:{0}", postVO.PostId));
        new Thread(new ThreadStart(() => ApiUtil.UpdatePostToServer(2))).Start();
        ClearUI();
        fillGridView();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void btnSearchExport_Click(object sender, EventArgs e)
    {
        //搜尋條件
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("Flag", "1");
        conditions.Add("NodeId", m_NodeId.ToString());
        conditions.Add("KeyWord", txtSearchKeyword.Text.Trim());
        conditions.Add("Type", ddlSearchType.SelectedValue);
        conditions.Add("CustomField1", ddlSearchCustomField1.SelectedValue);
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

        if (!string.IsNullOrEmpty(ddlSearchStore.SelectedValue))
        {
            conditions.Add("Store", ddlSearchStore.SelectedValue);
        }

        conditions.Add("Order", string.Format("order by {0}", ddlSearchOrder.SelectedValue));
        //conditions.Add("Order", "order by p.CloseDate desc, p.ShowDate desc, p.Title");

        IList<PostVO> postList = m_PostService.GetPostList(conditions);
        DataTable table = new DataTable();
        table.Columns.Add("類別", typeof(string));
        table.Columns.Add("狀態", typeof(string));
        table.Columns.Add("進貨日", typeof(string));
        table.Columns.Add("銷貨日", typeof(string));
        table.Columns.Add("品名", typeof(string));
        table.Columns.Add("進貨價", typeof(double));
        table.Columns.Add("售價", typeof(double));
        table.Columns.Add("數量", typeof(int));
        table.Columns.Add("客戶姓名", typeof(string));
        table.Columns.Add("客戶電話", typeof(string));
        table.Columns.Add("商品序號", typeof(string));
        table.Columns.Add("保固商", typeof(string));
        table.Columns.Add("進貨盤商", typeof(string));
        table.Columns.Add("銷售員", typeof(string));
        table.Columns.Add("銷售店點", typeof(string));
        table.Columns.Add("從建立客戶時銷售", typeof(string));

        if (postList != null && postList.Count > 0)
        {
            foreach (PostVO postVO in postList)
            {
                DataRow dr = table.NewRow();

                string showDate = postVO.ShowDate.HasValue ? postVO.ShowDate.Value.ToString("yyyy/MM/dd") : "";
                string closeDate = postVO.CloseDate.HasValue ? postVO.CloseDate.Value.ToString("yyyy/MM/dd") : "";

                dr[0] = postVO.CustomField1;
                dr[1] = postVO.GetStr_Type;
                dr[2] = showDate;
                dr[3] = closeDate;
                dr[4] = postVO.Title;
                dr[5] = postVO.Price == null ? 0 : postVO.Price;
                dr[6] = postVO.SellPrice == null ? 0 : postVO.SellPrice;
                dr[7] = postVO.Quantity;
                dr[8] = postVO.MemberName;
                dr[9] = postVO.MemberPhone;
                dr[10] = postVO.ProductSer;
                dr[11] = postVO.WarrantySuppliers;
                dr[12] = postVO.Wholesalers;
                dr[13] = postVO.CustomField2;
                dr[14] = postVO.Store;
                dr[15] = string.IsNullOrEmpty(postVO.MemberId) ? "" : "是";
                table.Rows.Add(dr);                
            }
        }

        string uploadRootPath = string.IsNullOrEmpty(m_ConfigHelper.ApiUrl) ? Server.MapPath("~\\") + "\\App_Data\\temp.xls" : "";
        NPOIHelper.ExportByWeb(table, "類別", string.Format("{0}庫存.xls", DateTime.Today.ToString("yyyyMMdd")), true, uploadRootPath);
    }

    private void ClearUI()
    {
        m_Mode = 0;
        //m_PicFileName = string.Empty;
        //ltlImg.Text = string.Empty;
        UIHelper.ClearUI(pnlContent);
        pnlContent.Visible = false;
        btnShowAdd.Enabled = true;
        rfCloseDate.Visible = false;
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
		InitDDL();
                UIHelper.FillUI(pnlContent, postVO);
                ShowMode();
                if (postVO.Type == 1)
                {
                    btnSold.Visible = false;
                }
                else
                {
                    txtCloseDate.Text = DateTime.Today.ToString("yyyy/MM/dd");
                }
                pnlContent.Visible = true;
                rfCloseDate.Visible = true;
                rfCustomField2.Visible = true;
                rfSellPrice.Visible = true;
                txtTitle.Enabled = false;
                ddlProductList.Visible = false;
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

            //判斷數量大於1就另外增加一筆新的
            if (postVO.Quantity > 1)
            {
                PostVO newPostVO = new PostVO();
                UIHelper.FillVO(pnlContent, newPostVO);                
                newPostVO.Node = postVO.Node;
                newPostVO.Quantity = 1;
                newPostVO.Type = 1;
                newPostVO.NeedUpdate = true;
                newPostVO.CreatedBy = m_SessionHelper.LoginUser.FullNameInChinese;
                newPostVO.UpdatedBy = m_SessionHelper.LoginUser.FullNameInChinese;
                newPostVO.CreatedDate = DateTime.Now;
                newPostVO.UpdatedDate = DateTime.Now;
                m_PostService.CreatePost(newPostVO);
                m_WebLogService.AddSystemLog(MsgVO.Action.售出, postVO, "", string.Format("單號:{0}", postVO.PostId));

                postVO.Quantity -= 1;                
                postVO.NeedUpdate = true;
                postVO.UpdatedBy = m_SessionHelper.LoginUser.FullNameInChinese;
                postVO.UpdatedDate = DateTime.Now;
                m_PostService.UpdatePost(postVO);
                new Thread(new ThreadStart(() => ApiUtil.UpdatePostToServer(2))).Start();
                fillGridView();
                ClearUI();
                ShowMode();
            }
            else
            {
                UIHelper.FillVO(pnlContent, postVO);
                postVO.Type = 1;                
                postVO.NeedUpdate = true;
                postVO.UpdatedBy = m_SessionHelper.LoginUser.FullNameInChinese;
                postVO.UpdatedDate = DateTime.Now;
                m_PostService.UpdatePost(postVO);
                m_WebLogService.AddSystemLog(MsgVO.Action.售出, postVO, "", string.Format("單號:{0}", postVO.PostId));
                new Thread(new ThreadStart(() => ApiUtil.UpdatePostToServer(2))).Start();
                fillGridView();
                ClearUI();
                ShowMode();
            }
        }
        catch (Exception ex)
        {
            m_Log.Error(ex);
            lblMsg.Text = ex.ToString();
        }
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        if (e.Row.RowIndex != -1)
        {
            Control ctrl = e.Row;
            int postId = int.Parse(UIHelper.FindHiddenValue(ref ctrl, "hdnPostId"));
            PostVO postVO = m_PostService.GetPostById(postId);

            Dictionary<string, string> conditions = new Dictionary<string, string>();
            conditions.Add("Flag", "1");
            conditions.Add("Type", "0");
            conditions.Add("EqualTitle", postVO.Title);
            ////在庫總庫存
            int totalQuantity = m_PostService.GetTotalQuantity(conditions);
            UIHelper.SetLabelText(ref ctrl, "lblTotalQuantity", totalQuantity.ToString());
        }
    }

    protected void btnShowAdd_Click(object sender, EventArgs e)
    {
        ClearUI();
        m_PicFileName = string.Empty;
        ShowMode();
        pnlContent.Visible = true;
        btnShowAdd.Enabled = false;
        InitDDL();
    }

    private void InitDDL()
    {
        ////帶入類別
        IList<NodeVO> typeList = m_PostService.GetNodeListByParentId(3);
        ddlTypeList.Items.Clear();
        if (typeList != null && typeList.Count > 0)
        {
            ddlTypeList.Items.Add(new ListItem("自訂", ""));            
            txtCustomField1.Visible = true;
            foreach (NodeVO node in typeList)
            {
                ddlTypeList.Items.Add(node.Name);                
            }
        }

        if (string.IsNullOrEmpty(ddlSearchCustomField1.SelectedValue))
        {
            ddlSearchCustomField1.Items.Clear();
            if (typeList != null && typeList.Count > 0)
            {
                ddlSearchCustomField1.Items.Add(new ListItem("全部", ""));
                foreach (NodeVO node in typeList)
                {
                    ddlSearchCustomField1.Items.Add(node.Name);
                }
            }
        }

        ////帶入品名
        IList<NodeVO> productList = m_PostService.GetNodeListByParentId(6);
        ddlProductList.Visible = true;
        ddlProductList.Items.Clear();
        if (productList != null && productList.Count > 0)
        {
            ddlProductList.Items.Add(new ListItem("自訂", ""));
            txtTitle.Visible = true;
            foreach (NodeVO node in productList)
            {
                ddlProductList.Items.Add(node.Name);
            }
        }

        //帶入銷售員
        IList<LoginUserVO> userList = m_AuthService.GetLoginUserList("1=1 ORDER BY ArrivedDate");
        ddlCustomField2.Items.Clear();
        if (userList != null && userList.Count > 0)
        {
            ddlCustomField2.Items.Add(new ListItem("請選擇銷售員", ""));
            foreach (LoginUserVO loginUserVO in userList)
            {
                ddlCustomField2.Items.Add(loginUserVO.FullNameInChinese);
            }
        }

        ////帶入店家
        IList<NodeVO> storetList = m_PostService.GetNodeListByParentName("店家");
        ddlStore.Items.Clear();
        ddlSearchStore.Items.Clear();
        if (storetList != null && storetList.Count > 0)
        {
            ddlStore.Items.Add(new ListItem("請選擇店家", ""));
            ddlSearchStore.Items.Add(new ListItem("全部", ""));
            foreach (NodeVO node in storetList)
            {
                ddlStore.Items.Add(node.Name);
                ddlSearchStore.Items.Add(node.Name);
            }
        }

        ddlStore.SelectedValue = storetList[0].Name;
    }

    protected void ddlProductList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ddlProductList.SelectedValue))
        {
            txtTitle.Text = string.Empty;
            txtTitle.Visible = true;
        }
        else
        {
            txtTitle.Text = ddlProductList.SelectedValue;
            txtTitle.Visible = false;
        }
    }

    protected void ddlTypeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ddlTypeList.SelectedValue))
        {
            txtCustomField1.Text = string.Empty;
            txtCustomField1.Visible = true;
        }
        else
        {
            txtCustomField1.Text = ddlTypeList.SelectedValue;
            txtCustomField1.Visible = false;
        }
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