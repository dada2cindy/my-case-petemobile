﻿using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.SystemApplications.Domain;
using System.Collections.Generic;
using System.Data;
using WuDada.Core.Auth;
using WuDada.Core.Auth.Service;
using WuDada.Core.Auth.Domain;
using WuDada.Core.Member;
using WuDada.Core.Member.Service;
using WuDada.Core.Member.Domain;

public partial class admin_UC05_0511 : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private MemberFactory m_MemberFactory;
    private IMemberService m_MemberService;
    private AuthFactory m_AuthFactory;
    private IAuthService m_AuthService;
    private WebLogService m_WebLogService;
    private HttpHelper m_HttpHelper;

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
        m_MemberFactory = new MemberFactory();
        m_AuthFactory = new AuthFactory();
        m_HttpHelper = new HttpHelper();
        m_AuthService = m_AuthFactory.GetAuthService();
        m_MemberService = m_MemberFactory.GetMemberService();

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
            btnEdit.Visible = false;
            btnDelete.Visible = false;
        }
        else
        {
            btnAdd.Visible = false;
            btnEdit.Visible = true;
            btnDelete.Visible = true;
        }
    }

    private void fillGridView()
    {
        //搜尋條件
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("Status", "1");
        conditions.Add("KeyWord", txtSearchKeyword.Text.Trim());

        if (!string.IsNullOrEmpty(txtSearchApplyDateStart.Text.Trim()))
        {
            conditions.Add("ApplyDateStart", txtSearchApplyDateStart.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtSearchApplyDateEnd.Text.Trim()))
        {
            conditions.Add("ApplyDateEnd", txtSearchApplyDateEnd.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtSearchDueDateStart.Text.Trim()))
        {
            conditions.Add("DueDateStart", txtSearchDueDateStart.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtSearchDueDateEnd.Text.Trim()))
        {
            conditions.Add("DueDateEnd", txtSearchDueDateEnd.Text.Trim());
        }
        

        //分頁
        AspNetPager1.RecordCount = m_MemberService.GetMemberCount(conditions);
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
        conditions.Add("Order", "order by m.ApplyDate desc, m.Name");

        gvList.DataSource = m_MemberService.GetMemberList(conditions);
        gvList.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        MemberVO memberVO = new MemberVO();
        UIHelper.FillVO(pnlContent, memberVO);
        //postVO.PicFileName = m_PicFileName;
        memberVO.Status = "1";
        memberVO.CreateIP = m_HttpHelper.GetUserIp(Context);
        m_MemberService.CreateMember(memberVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.新增, memberVO);
        ClearUI();
        fillGridView();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        MemberVO memberVO = m_MemberService.GetMemberById(m_Mode);
        memberVO.Status = "0";
        m_MemberService.UpdateMember(memberVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.刪除, memberVO, "", string.Format("單號:{0}", memberVO.MemberId));
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
        conditions.Add("Status", "1");
        conditions.Add("KeyWord", txtSearchKeyword.Text.Trim());

        if (!string.IsNullOrEmpty(txtSearchApplyDateStart.Text.Trim()))
        {
            conditions.Add("ApplyDateStart", txtSearchApplyDateStart.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtSearchApplyDateEnd.Text.Trim()))
        {
            conditions.Add("ApplyDateEnd", txtSearchApplyDateEnd.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtSearchDueDateStart.Text.Trim()))
        {
            conditions.Add("DueDateStart", txtSearchDueDateStart.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtSearchDueDateEnd.Text.Trim()))
        {
            conditions.Add("DueDateEnd", txtSearchDueDateEnd.Text.Trim());
        }
        conditions.Add("Order", "order by m.ApplyDate desc, m.Name");

        IList<MemberVO> memberList = m_MemberService.GetMemberList(conditions);
        DataTable table = new DataTable();
        table.Columns.Add("申辦日期", typeof(string));
        table.Columns.Add("客戶大名", typeof(string));
        table.Columns.Add("聯絡電話", typeof(string));        
        table.Columns.Add("客戶生日", typeof(string));
        table.Columns.Add("申辦專案", typeof(string));
        table.Columns.Add("搭配手機", typeof(string));
        table.Columns.Add("手機序號", typeof(string));
        table.Columns.Add("申辦號碼", typeof(string));
        table.Columns.Add("手機進價", typeof(string));
        table.Columns.Add("銷售金額", typeof(string));
        table.Columns.Add("門號佣金", typeof(string));
        table.Columns.Add("違約金", typeof(string));
        table.Columns.Add("補償金", typeof(string));
        table.Columns.Add("綁約月數", typeof(string));
        table.Columns.Add("門號到期日", typeof(string));
        table.Columns.Add("備註", typeof(string));
        table.Columns.Add("銷售員", typeof(string));        

        if (memberList != null && memberList.Count > 0)
        {
            foreach (MemberVO memberVO in memberList)
            {
                DataRow dr = table.NewRow();

                string applyDate = memberVO.ApplyDate.HasValue ? memberVO.ApplyDate.Value.ToString("yyyy/MM/dd") : "";
                string dueDate = memberVO.DueDate.HasValue ? memberVO.DueDate.Value.ToString("yyyy/MM/dd") : "";
                string birthday = memberVO.Birthday.HasValue ? memberVO.Birthday.Value.ToString("yyyy/MM/dd") : "";

                dr[0] = applyDate;
                dr[1] = memberVO.Name;
                dr[2] = memberVO.Phone;
                dr[3] = birthday;
                dr[4] = memberVO.Project;
                dr[5] = memberVO.Product;
                dr[6] = memberVO.PhoneSer;
                dr[7] = memberVO.Mobile;
                dr[8] = memberVO.PhonePrice;
                dr[9] = memberVO.PhoneSellPrice;
                dr[10] = memberVO.Commission;
                dr[11] = memberVO.BreakMoney;
                dr[12] = memberVO.Compensation;
                dr[13] = memberVO.ContractMonths;
                dr[14] = dueDate;
                dr[15] = memberVO.Sales;
                dr[16] = memberVO.Note;

                table.Rows.Add(dr);                
            }
        }

        NPOIHelper.ExportByWeb(table, "類別", string.Format("{0}客戶.xls",DateTime.Today.ToString("yyyyMMdd")));
    }

    private void ClearUI()
    {
        m_Mode = 0;
        //m_PicFileName = string.Empty;
        //ltlImg.Text = string.Empty;
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
        int memberId = int.Parse(e.CommandArgument.ToString());
        MemberVO memberVO = m_MemberService.GetMemberById(memberId);
        switch (cmdName)
        {
            case "myModify":
                ClearUI();
                m_Mode = memberId;
		InitDDL();
                UIHelper.FillUI(pnlContent, memberVO);
                ShowMode();
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
            MemberVO memberVO = m_MemberService.GetMemberById(m_Mode);
            UIHelper.FillVO(pnlContent, memberVO);
            m_MemberService.UpdateMember(memberVO);
            m_WebLogService.AddSystemLog(MsgVO.Action.修改, memberVO, "", string.Format("單號:{0}", memberVO.MemberId));
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

        InitDDL();
    }
    
    private void InitDDL()
    {
        //帶入銷售員
        IList<LoginUserVO> userList = m_AuthService.GetLoginUserList("1=1 ORDER BY ArrivedDate");
        ddlSales.Items.Clear();
        if (userList != null && userList.Count > 0)
        {
            ddlSales.Items.Add(new ListItem("請選擇銷售員", ""));
            foreach (LoginUserVO loginUserVO in userList)
            {
                ddlSales.Items.Add(loginUserVO.FullNameInChinese);
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