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
using System.Data;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Service;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Common;
using WuDada.Core.Common.Service;

public partial class admin_UC06_0611 : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private PostFactory m_PostFactory;
    private IPostFileService m_PostFileService;
    private IPostService m_PostService;
    private AuthFactory m_AuthFactory;
    private IAuthService m_AuthService;
    private CommonFactory m_CommonFactory;
    private ICommonService m_CommonService;
    private WebLogService m_WebLogService;
    private SessionHelper m_SessionHelper;
    private ConfigHelper m_ConfigHelper;

    private int m_Mode
    {
        get { if (ViewState["mode"] == null) { ViewState["mode"] = 0; } return int.Parse(ViewState["mode"].ToString()); }
        set { ViewState["mode"] = value; }
    }
    private string m_FileName
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
        m_CommonFactory = new CommonFactory();
        m_SessionHelper = new SessionHelper();
        m_AuthService = m_AuthFactory.GetAuthService();
        m_PostFileService = m_PostFactory.GetPostFileService();
        m_CommonService = m_CommonFactory.GetCommonService();
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
        trContent1.Visible = false;
        trContent2.Visible = false;

        if (m_Mode == 0)
        {            
            btnAdd.Visible = true;            
            btnDelete.Visible = false;                                    
        }
        else
        {
            btnAdd.Visible = false;            
            btnDelete.Visible = true;
        }

        if (!m_SessionHelper.IsAdmin)
        {
            btnSearchExport.Visible = false;
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

        //分頁
        AspNetPager1.RecordCount = m_PostFileService.GetFileCount(conditions);
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
        //conditions.Add("Order", string.Format("order by {0}", ddlSearchOrder.SelectedValue));
        //conditions.Add("Order", "order by p.CloseDate desc, p.ShowDate desc, p.Title");

        gvList.DataSource = m_PostFileService.GetFileList(conditions);
        gvList.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        FileVO fileVO = new FileVO();
        UIHelper.FillVO(pnlContent, fileVO);
        fileVO.FileName = m_FileName;
        fileVO.Flag = 1;
        if (!string.IsNullOrEmpty(txtShowDate.Text.Trim()))
        {
            fileVO.ShowDate = DateTime.Parse(txtShowDate.Text.Trim());
        }
        fileVO.FileNo = GetFileNo(fileVO);
        fileVO.CreatedBy = m_SessionHelper.LoginUser.FullNameInChinese;
        fileVO.UpdatedBy = m_SessionHelper.LoginUser.FullNameInChinese;
        m_PostFileService.CreateFile(fileVO);                
        m_WebLogService.AddSystemLog(MsgVO.Action.新增, fileVO);
        ClearUI();
        fillGridView();
    }

    private string GetFileNo(FileVO fileVO)
    {
        IList<NodeVO> storeList = m_PostService.GetNodeListByParentName("店家");
        string fileNo = "";
        string code = "";

        switch (fileVO.Type)
        {
            case "1":
                code = string.Format("{0}C{1}{2}{3}", storeList[0].Name, fileVO.ShowDate.Value.ToString("yyyyMMdd"), fileVO.Content2, fileVO.Content1);
                fileNo = code;
                break;
            case "2":
                code = string.Format("{0}I{1}", storeList[0].Name, fileVO.ShowDate.Value.ToString("yyyyMMdd"));
                fileNo = m_CommonService.CreateSer_Code_And_Num(code, 3);
                break;
            case "3":
                code = string.Format("{0}B{1}{2}", storeList[0].Name, fileVO.ShowDate.Value.ToString("yyyyMMdd"), fileVO.Content1);
                fileNo = code;
                break;
        }

        return fileNo;
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        FileVO fileVO = m_PostFileService.GetFileById(m_Mode);
        fileVO.Flag = 0;
        fileVO.UpdatedBy = m_SessionHelper.LoginUser.FullNameInChinese;
        m_PostFileService.UpdateFile(fileVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.刪除, fileVO, "", string.Format("單號:{0}", fileVO.FileId));
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
        //conditions.Add("Order", string.Format("order by {0}", ddlSearchOrder.SelectedValue));
        //conditions.Add("Order", "order by p.CloseDate desc, p.ShowDate desc, p.Title");

        IList<FileVO> fileList = m_PostFileService.GetFileList(conditions);
        DataTable table = new DataTable();
        table.Columns.Add("編號", typeof(string));
        table.Columns.Add("類型", typeof(string));
        table.Columns.Add("日期", typeof(string));
        table.Columns.Add("身分證", typeof(string));
        table.Columns.Add("門號", typeof(string));

        if (fileList != null && fileList.Count > 0)
        {
            foreach (FileVO fileVO in fileList)
            {
                DataRow dr = table.NewRow();

                string showDate = fileVO.ShowDate.HasValue ? fileVO.ShowDate.Value.ToString("yyyy/MM/dd") : "";

                dr[0] = fileVO.FileNo;
                dr[1] = fileVO.GetStr_Type;
                dr[2] = showDate;
                dr[3] = fileVO.Content1;
                dr[4] = fileVO.Content2;                
                table.Rows.Add(dr);                
            }
        }

        string uploadRootPath = string.IsNullOrEmpty(m_ConfigHelper.ApiUrl) ? Server.MapPath("~\\") + "\\App_Data\\temp.xls" : "";
        NPOIHelper.ExportByWeb(table, "類別", string.Format("{0}檔案上傳.xls", DateTime.Today.ToString("yyyyMMdd")), true, uploadRootPath);
    }

    private void ClearUI()
    {
        m_Mode = 0;
        m_FileName = string.Empty;
        //ltlImg.Text = string.Empty;
        trContent1.Visible = false;
        trContent2.Visible = false;
        ddlType.Enabled = true;
        ddlType.SelectedValue = "";
        ddlType_SelectedIndexChanged(null, null);
        UIHelper.ClearUI(pnlContent);
        pnlContent.Visible = false;
        btnShowAdd.Enabled = true;        
    }

    //private string GetPic(string fileName)
    //{
    //    return "<img src='../../upload/" + fileName + "' width='145' height='108' border='0'>";
    //}

    protected void gvList_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        int fileId = int.Parse(e.CommandArgument.ToString());
        FileVO fileVO = m_PostFileService.GetFileById(fileId);
        switch (cmdName)
        {
            case "myModify":
                ClearUI();
                ddlType.Enabled = false;
                m_Mode = fileId;		        
                UIHelper.FillUI(pnlContent, fileVO);
                ShowMode();
                ddlType_SelectedIndexChanged(null, null);
                pnlContent.Visible = true;
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

    protected void btnEdit_Click(object sender, EventArgs e)
    {
        try
        {
            FileVO fileVO = m_PostFileService.GetFileById(m_Mode);
            UIHelper.FillVO(pnlContent, fileVO);
            fileVO.UpdatedBy = m_SessionHelper.LoginUser.FullNameInChinese;
            fileVO = m_PostFileService.UpdateFile(fileVO);
            m_WebLogService.AddSystemLog(MsgVO.Action.修改, fileVO, "", string.Format("單號:{0}", fileVO.FileId));                        
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
        
    }

    protected void btnShowAdd_Click(object sender, EventArgs e)
    {
        ClearUI();
        m_FileName = string.Empty;
        ShowMode();
        pnlContent.Visible = true;
        btnShowAdd.Enabled = false;
    }    

    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(ddlType.SelectedValue))
        {
            trContent1.Visible = false;
            trContent2.Visible = false;
        }
        else
        {
            switch (ddlType.SelectedValue)
            {
                case "1":
                    trContent1.Visible = true;
                    trContent2.Visible = true;
                    break;
                case "2":
                    trContent1.Visible = false;
                    trContent2.Visible = false;
                    break;
                case "3":
                    trContent1.Visible = false;
                    trContent2.Visible = true;
                    break;
            }
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