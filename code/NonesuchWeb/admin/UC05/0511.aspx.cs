using System;
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
using WuDada.Core.Post.Service;
using WuDada.Core.Post;
using WuDada.Core.Post.Domain;
using System.Linq;
using System.Threading;

public partial class admin_UC05_0511 : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private PostFactory m_PostFactory;
    private MemberFactory m_MemberFactory;
    private IMemberService m_MemberService;
    private AuthFactory m_AuthFactory;
    private IAuthService m_AuthService;
    private WebLogService m_WebLogService;
    private HttpHelper m_HttpHelper;
    private IPostService m_PostService;
    private SessionHelper m_SessionHelper;

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
        m_MemberFactory = new MemberFactory();
        m_AuthFactory = new AuthFactory();
        m_HttpHelper = new HttpHelper();
        m_SessionHelper = new SessionHelper();
        m_AuthService = m_AuthFactory.GetAuthService();
        m_MemberService = m_MemberFactory.GetMemberService();
        m_PostService = m_PostFactory.GetPostService();

        if (!IsPostBack)
        {
            pnlContent.Visible = false;
            fillGridView();
            ShowMode();
            InitDDL();
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

        if (!m_SessionHelper.IsAdmin)
        {
            btnSearchExport.Visible = false;
            btnEdit.Visible = false;
        }

        ddlProject1_SelectedIndexChanged(null, null);
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

        if (!string.IsNullOrEmpty(txtSearchApplyDate2Start.Text.Trim()))
        {
            conditions.Add("ApplyDate2Start", txtSearchApplyDate2Start.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtSearchApplyDate2End.Text.Trim()))
        {
            conditions.Add("ApplyDate2End", txtSearchApplyDate2End.Text.Trim());
        }

        if (!string.IsNullOrEmpty(ddlSearchBirthDay.SelectedValue))
        {
            switch (ddlSearchBirthDay.SelectedValue)
            {
                case "今天生日":
                    conditions.Add("BirthdayMonth", DateTime.Today.Month.ToString());
                    conditions.Add("BirthdayDay", DateTime.Today.Day.ToString());
                    break;
                case "本月生日":
                    conditions.Add("BirthdayMonth", DateTime.Today.Month.ToString());
                    break;
                case "下個月生日":
                    conditions.Add("BirthdayMonth", DateTime.Today.AddMonths(1).Month.ToString());
                    break;
            }
        }

        if (!string.IsNullOrEmpty(ddlSearchStore.SelectedValue))
        {
            conditions.Add("Store", ddlSearchStore.SelectedValue);
        }

        if (!string.IsNullOrEmpty(ddlSearchGetCommission.SelectedValue))
        {
            conditions.Add("GetCommission", ddlSearchGetCommission.SelectedValue);
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
        memberVO.NeedUpdate = true;
        memberVO = m_MemberService.CreateMember(memberVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.新增, memberVO);
        UpdateProductByPhoneSer(memberVO.MemberId);
        new Thread(new ThreadStart(ApiUtil.UpdateMemberToServer)).Start();
        ClearUI();
        fillGridView();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        MemberVO memberVO = m_MemberService.GetMemberById(m_Mode);
        memberVO.Status = "0";
        memberVO.NeedUpdate = true;
        m_MemberService.UpdateMember(memberVO);
        UpdateProductByPhoneSerWithDelete(memberVO.PhoneSer);
        m_WebLogService.AddSystemLog(MsgVO.Action.刪除, memberVO, "", string.Format("單號:{0}", memberVO.MemberId));
        new Thread(new ThreadStart(ApiUtil.UpdateMemberToServer)).Start();
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

        if (!string.IsNullOrEmpty(txtSearchApplyDate2Start.Text.Trim()))
        {
            conditions.Add("ApplyDate2Start", txtSearchApplyDate2Start.Text.Trim());
        }

        if (!string.IsNullOrEmpty(txtSearchApplyDate2End.Text.Trim()))
        {
            conditions.Add("ApplyDate2End", txtSearchApplyDate2End.Text.Trim());
        }

        if (!string.IsNullOrEmpty(ddlSearchBirthDay.SelectedValue))
        {
            switch (ddlSearchBirthDay.SelectedValue)
            {
                case "今天生日":
                    conditions.Add("BirthdayMonth", DateTime.Today.Month.ToString());
                    conditions.Add("BirthdayDay", DateTime.Today.Day.ToString());
                    break;
                case "本月生日":
                    conditions.Add("BirthdayMonth", DateTime.Today.Month.ToString());
                    break;
                case "下個月生日":
                    conditions.Add("BirthdayMonth", DateTime.Today.AddMonths(1).Month.ToString());
                    break;
            }
        }

        if (!string.IsNullOrEmpty(ddlSearchStore.SelectedValue))
        {
            conditions.Add("Store", ddlSearchStore.SelectedValue);
        }

        if (!string.IsNullOrEmpty(ddlSearchGetCommission.SelectedValue))
        {
            conditions.Add("GetCommission", ddlSearchGetCommission.SelectedValue);
        }   

        conditions.Add("Order", "order by m.ApplyDate desc, m.Name");

        IList<MemberVO> memberList = m_MemberService.GetMemberList(conditions);
        DataTable table = new DataTable();
        table.Columns.Add("申請日期", typeof(string));
        table.Columns.Add("開通日期", typeof(string));
        table.Columns.Add("客戶大名", typeof(string));
        table.Columns.Add("身分證字號", typeof(string));
        table.Columns.Add("聯絡電話", typeof(string));        
        table.Columns.Add("客戶生日", typeof(string));
        table.Columns.Add("上線盤商", typeof(string));
        table.Columns.Add("Sim卡卡號", typeof(string));
        table.Columns.Add("申辦專案", typeof(string));
        table.Columns.Add("搭配手機", typeof(string));
        table.Columns.Add("手機序號", typeof(string));
        table.Columns.Add("保固商", typeof(string));
        table.Columns.Add("手機盤商", typeof(string));
        table.Columns.Add("申辦號碼", typeof(string));
        table.Columns.Add("手機進價", typeof(double));
        table.Columns.Add("銷售金額", typeof(double));
        table.Columns.Add("是否幫客戶預繳", typeof(string));
        table.Columns.Add("預繳金額", typeof(double));
        table.Columns.Add("門號佣金", typeof(double));
        table.Columns.Add("佣金是否核發", typeof(string));
        table.Columns.Add("後退佣金", typeof(double));
        table.Columns.Add("吸收違約金", typeof(double));        
        table.Columns.Add("綁約月數", typeof(double));
        table.Columns.Add("門號到期日", typeof(string));
        table.Columns.Add("銷售員", typeof(string));
        table.Columns.Add("銷售店點", typeof(string));
        table.Columns.Add("備註", typeof(string));        

        if (memberList != null && memberList.Count > 0)
        {
            foreach (MemberVO memberVO in memberList)
            {
                DataRow dr = table.NewRow();

                string applyDate = memberVO.ApplyDate.HasValue ? memberVO.ApplyDate.Value.ToString("yyyy/MM/dd") : "";
                string applyDate2 = memberVO.ApplyDate2.HasValue ? memberVO.ApplyDate2.Value.ToString("yyyy/MM/dd") : "";
                string dueDate = memberVO.DueDate.HasValue ? memberVO.DueDate.Value.ToString("yyyy/MM/dd") : "";
                string birthday = !string.IsNullOrEmpty(memberVO.BirthdayYear) ? memberVO.BirthdayYear + "/" : "";
                birthday += !string.IsNullOrEmpty(memberVO.BirthdayMonth) ? memberVO.BirthdayMonth + "/" : "";
                birthday += (!string.IsNullOrEmpty(memberVO.BirthdayMonth) && !string.IsNullOrEmpty(memberVO.BirthdayDay)) ? memberVO.BirthdayDay : "";

                dr[0] = applyDate2;
                dr[1] = applyDate;
                dr[2] = memberVO.Name;
                dr[3] = memberVO.PID;
                dr[4] = memberVO.Phone;
                dr[5] = birthday;
                dr[6] = memberVO.OnlineWholesalers;
                dr[7] = memberVO.SimNo;
                dr[8] = memberVO.GetStr_Project;
                dr[9] = memberVO.Product;
                dr[10] = memberVO.PhoneSer;
                dr[11] = memberVO.WarrantySuppliers;
                dr[12] = memberVO.MobileWholesalers;
                dr[13] = memberVO.Mobile;
                dr[14] = memberVO.PhonePrice == null ? 0 : memberVO.PhonePrice;
                dr[15] = memberVO.PhoneSellPrice == null ? 0 : memberVO.PhoneSellPrice;
                dr[16] = memberVO.SelfPrepayment;
                dr[17] = memberVO.Prepayment == null ? 0 : memberVO.Prepayment;
                dr[18] = memberVO.Commission == null ? 0 : memberVO.Commission;
                dr[19] = memberVO.GetCommission;
                dr[20] = memberVO.ReturnCommission == null ? 0 : memberVO.ReturnCommission;
                dr[21] = memberVO.BreakMoney == null ? 0 : memberVO.BreakMoney;                
                dr[22] = memberVO.ContractMonths == null ? 0 : memberVO.ContractMonths;
                dr[23] = dueDate;
                dr[24] = memberVO.Sales;
                dr[25] = memberVO.Store;
                dr[26] = memberVO.Note;

                table.Rows.Add(dr);                
            }
        }

        NPOIHelper.ExportByWeb(table, "類別", string.Format("{0}客戶.xls", DateTime.Today.ToString("yyyyMMdd")), true);
    }

    private void ClearUI()
    {
        m_Mode = 0;
        //m_PicFileName = string.Empty;
        //ltlImg.Text = string.Empty;
        UIHelper.ClearUI(pnlContent);
        pnlContent.Visible = false;
        btnShowAdd.Enabled = true;
        ddlContractMonths.SelectedValue = "";
        ddlProject1.SelectedValue = "";
        ddlOnlineWholesalers.SelectedValue = "";
        ddlGetCommission.SelectedValue = "否";
        ddlSelfPrepayment.SelectedValue = "無";
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
                ddlProject1.SelectedValue = memberVO.Project1;
                ddlProject2.SelectedValue = memberVO.Project2;
                ddlProject3.SelectedValue = memberVO.Project3;
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
            memberVO.NeedUpdate = true;
            memberVO = m_MemberService.UpdateMember(memberVO);
            m_WebLogService.AddSystemLog(MsgVO.Action.修改, memberVO, "", string.Format("單號:{0}", memberVO.MemberId));
            UpdateProductByPhoneSer(memberVO.MemberId);
            new Thread(new ThreadStart(ApiUtil.UpdateMemberToServer)).Start();
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

    private void UpdateProductByPhoneSer(int memberId)
    {
        if (!string.IsNullOrEmpty(hdnPhoneSerId.Value))
        {
            PostVO postVO = m_PostService.GetPostById(int.Parse(hdnPhoneSerId.Value));

            if (postVO.Flag == 1 && postVO.Type == 0)
            {
                postVO.MemberId = memberId.ToString();
                postVO.Type = 1;
                postVO.MemberName = txtName.Text.Trim();
                postVO.MemberPhone = txtMobile.Text.Trim();
                postVO.SellPrice = 0;
                postVO.CloseDate = DateTime.Parse(txtApplyDate2.Text.Trim());
                postVO.CustomField2 = ddlSales.SelectedValue;
                m_PostService.UpdatePost(postVO);
                m_WebLogService.AddSystemLog(MsgVO.Action.售出, postVO, "", string.Format("單號:{0}", postVO.PostId));
            }
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

        ddlContractMonths.Items.Clear();
        ddlContractMonths.Items.Add(new ListItem("請選擇綁約月數 ", ""));
        ddlContractMonths.Items.Add(new ListItem("12 ", "12"));
        ddlContractMonths.Items.Add(new ListItem("24 ", "24"));
        ddlContractMonths.Items.Add(new ListItem("30 ", "30"));
        ddlContractMonths.Items.Add(new ListItem("36 ", "36"));
     
        ////生日年月日
        ddlBirthDayYear.Items.Clear();
        ddlBirthDayYear.Items.Add(new ListItem("請選擇年", ""));
        for (int i = 1900; i <= DateTime.Today.Year; i++)
        {
            ddlBirthDayYear.Items.Add(i.ToString());
        }


        ddlBirthDayMonth.Items.Clear();
        ddlBirthDayMonth.Items.Add(new ListItem("請選擇月", ""));
        for (int i = 1; i <= 12; i++)
        {
            ddlBirthDayMonth.Items.Add(i.ToString());
        }

        ddlBirthDayDay.Items.Clear();
        ddlBirthDayDay.Items.Add(new ListItem("請選擇日", ""));
        for (int i = 1; i <= 31; i++)
        {
            ddlBirthDayDay.Items.Add(i.ToString());
        }

        if (string.IsNullOrEmpty(ddlSearchBirthDay.SelectedValue))
        {
            ddlSearchBirthDay.Items.Clear();
            ddlSearchBirthDay.Items.Add("");
            ddlSearchBirthDay.Items.Add("今天生日");
            ddlSearchBirthDay.Items.Add("本月生日");
            ddlSearchBirthDay.Items.Add("下個月生日");
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
    protected void ddlContractMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(!string.IsNullOrEmpty(txtApplyDate.Text.Trim()) && !string.IsNullOrEmpty(ddlContractMonths.SelectedValue))
        {
            try
            {
                DateTime applyDate = DateTime.Parse(txtApplyDate.Text.Trim());
                DateTime dueDate;
                switch (ddlContractMonths.SelectedValue)
                {
                    case "12":
                    case "24":
                    case "36":
                        dueDate = applyDate.AddDays((365 * (int.Parse(ddlContractMonths.SelectedValue) / 12)));
                        txtDueDate.Text = dueDate.ToString("yyyy/MM/dd");
                        break;
                    case "30":
                        dueDate = applyDate.AddDays(910);
                        txtDueDate.Text = dueDate.ToString("yyyy/MM/dd");
                        break;
                }
                //DateTime dueDate = applyDate.AddDays
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void ddlProject1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ///先全部清除與鎖定
        ddlProject2.SelectedValue = "";
        ddlProject3.SelectedValue = "";

        ddlProject2.Enabled = false;
        ddlProject3.Enabled = false;
        
        //判斷現在選哪個開放選項
        switch (ddlProject1.SelectedValue)
        {
            case "續約":
                ddlProject2.Enabled = true;
                break;
            case "新辦":
                ddlProject3.Enabled = true;
                break;
            case "攜碼":
                ddlProject2.Enabled = true;
                ddlProject3.Enabled = true;
                break;
        }
    }

    private void UpdateProductByPhoneSerWithDelete(string phoneSer)
    {
        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("Flag", "1");
        conditions.Add("ProductSer", phoneSer);        
        conditions.Add("PageIndex", "0");
        conditions.Add("PageSize", "1");
        IList<PostVO> list = m_PostService.GetPostList(conditions);
        if (list != null && list.Count > 0)
        {
            PostVO postVO = m_PostService.GetPostById(list[0].PostId);
            postVO.MemberId = null;
            postVO.Type = 0;
            postVO.SellPrice = 0;
            postVO.CloseDate = null;
            postVO.MemberName = string.Empty;
            postVO.MemberPhone = string.Empty;
            postVO.CustomField2 = string.Empty;
            m_PostService.UpdatePost(postVO);
            m_WebLogService.AddSystemLog(MsgVO.Action.修改, postVO, "", string.Format("單號:{0}", postVO.PostId));
        }
    }

    protected void txtPhoneSer_TextChanged(object sender, EventArgs e)
    {
        lblPhoneSerMsg.Text = string.Empty;
        hdnPhoneSerId.Value = string.Empty;

        Dictionary<string, string> conditions = new Dictionary<string, string>();
        conditions.Add("Flag", "1");
        conditions.Add("NodeId", "2");
        //conditions.Add("Type", "0");
        //conditions.Add("ProductSer", txtPhoneSer.Text.Trim());
        conditions.Add("KeyWord", txtPhoneSer.Text.Trim());
        conditions.Add("PageIndex", "0");
        conditions.Add("PageSize", "10");
        IList<PostVO> list = m_PostService.GetPostList(conditions);

        if (list != null)
        {
            list = list.Where(l => !string.IsNullOrEmpty(l.ProductSer)).ToList();
        }

        if (list == null || list.Count == 0)
        {
            lblPhoneSerMsg.Text = "查無此手機序號";
        }
        else
        {
            PostVO product = list[0];
            if (product.Type == 1)
            {
                lblPhoneSerMsg.Text = "此序號手機已售出";
            }            
            else
            {                                
                if (!string.IsNullOrEmpty(product.ProductSer))
                {
                    hdnPhoneSerId.Value = product.PostId.ToString();
                    txtPhoneSer.Text = product.ProductSer;
                    txtProduct.Text = product.Title;
                    txtPhonePrice.Text = product.Price.ToString();
                    txtWarrantySuppliers.Text = product.WarrantySuppliers;
                    txtMobileWholesalers.Text = product.Wholesalers;
                }                                
            }
        }
    }
}