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
using WuDada.Core.Accounting.Service;
using WuDada.Core.Accounting.Domain;
using System.Threading;
using WuDada.Core.Generic.Util;

public partial class admin_UC07_0711 : System.Web.UI.Page
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
    private AccountingFactory m_AccountingFactory;
    private IAccountingService m_AccountingService;
    private ConfigHelper m_ConfigHelper;

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
        m_MemberFactory = new MemberFactory();
        m_AuthFactory = new AuthFactory();
        m_HttpHelper = new HttpHelper();
        m_SessionHelper = new SessionHelper();
        m_AccountingFactory = new AccountingFactory();
        m_AuthService = m_AuthFactory.GetAuthService();
        m_MemberService = m_MemberFactory.GetMemberService();
        m_PostService = m_PostFactory.GetPostService();
        m_AccountingService = m_AccountingFactory.GetAccountingService();

        if (!IsPostBack)
        {
            InitDDL();
            ShowMode();
                       
            fillGridView();

            new Thread(new ThreadStart(ApiUtil.UpdateMemberToServer)).Start();
            new Thread(new ThreadStart(() => ApiUtil.UpdateFileToServer(Server.MapPath("../../App_Data/upload/")))).Start();
            new Thread(new ThreadStart(() => ApiUtil.UpdatePostToServer(2))).Start();
            //LoadTotalCommission();
        }
    }

    //private void LoadTotalCommission()
    //{
    //    IList<NodeVO> storeList = m_PostService.GetNodeListByParentName("店家");

    //    Dictionary<string, string> conditions = new Dictionary<string, string>();
    //    conditions.Add("Status", "1");
    //    conditions.Add("GetCommission", "否");
    //    conditions.Add("Store", storeList[0].Name);

    //    lblNotGetCommission.Text = m_MemberService.GetTotalCommission(conditions).ToString();
    //}

    private void ShowMode()
    {
        if (!m_SessionHelper.IsAdmin)
        {
            btnSearchExport.Visible = false;
            btnSave.Visible = false;
        }
    }

    private void fillGridView()
    {
        IList<NodeVO> storeList = m_PostService.GetNodeListByParentName("店家");
        string ym = string.Format("{0}{1}", ddlSearchYear.SelectedValue, ddlSearchMonth.SelectedValue);
        
        if (string.IsNullOrEmpty(m_ConfigHelper.ApiUrl))
        {
            //空白的表示是Server的
            IList<SalesStatisticsVO> list = m_AccountingService.GetSalesStatisticsByLoginUser(ym);
            gvList.DataSource = list;
        }
        else
        {
            //各店點的
            gvList.DataSource = m_AccountingService.GetSalesStatistics(ym, storeList[0].Name);
        }
        gvList.DataBind();

        if (storeList.Count > 0)
        {
            gvListStore.DataSource = m_AccountingService.GetSalesStatisticsByStore(ym);
            gvListStore.DataBind();
        }
    }    

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGridView();
        //LoadTotalCommission();
    }

    protected void btnSearchExport_Click(object sender, EventArgs e)
    {
        IList<NodeVO> storeList = m_PostService.GetNodeListByParentName("店家");
        string ym = string.Format("{0}{1}", ddlSearchYear.SelectedValue, ddlSearchMonth.SelectedValue);
        IList<SalesStatisticsVO> salesStatisticsList = null;
        if (string.IsNullOrEmpty(m_ConfigHelper.ApiUrl))
        {
            //空白的表示是Server的
            salesStatisticsList = m_AccountingService.GetSalesStatisticsByLoginUser(ym);
        }
        else
        {
            //各店點的
            salesStatisticsList = m_AccountingService.GetSalesStatistics(ym, storeList[0].Name);
        }
        IList<SalesStatisticsVO> salesStatisticsList2 = m_AccountingService.GetSalesStatisticsByStore(ym);        

        DataTable table = new DataTable();
        table.Columns.Add("姓名", typeof(string));
        table.Columns.Add("本月目標", typeof(double));
        table.Columns.Add("太電", typeof(int));
        table.Columns.Add("遠傳", typeof(int));
        table.Columns.Add("中華", typeof(int));
        table.Columns.Add("亞太", typeof(int));
        table.Columns.Add("星星", typeof(int));
        table.Columns.Add("上線件數", typeof(int));
        table.Columns.Add("門號營收", typeof(double));
        table.Columns.Add("門號毛利", typeof(double));
        table.Columns.Add("配件件數", typeof(int));
        table.Columns.Add("配件營收", typeof(double));
        table.Columns.Add("配件毛利", typeof(double));
        table.Columns.Add("總毛利", typeof(double));
        table.Columns.Add("達成率", typeof(double));
        table.Columns.Add("未核發佣金總額", typeof(int));

        if (salesStatisticsList != null && salesStatisticsList.Count > 0)
        {
            foreach (SalesStatisticsVO salesStatistics in salesStatisticsList)
            {
                DataRow dr = table.NewRow();

                dr[0] = salesStatistics.Name;
                dr[1] = salesStatistics.Target;
                dr[2] = salesStatistics.ApplyTelCom1Count;
                dr[3] = salesStatistics.ApplyTelCom2Count;
                dr[4] = salesStatistics.ApplyTelCom3Count;
                dr[5] = salesStatistics.ApplyTelCom4Count;
                dr[6] = salesStatistics.ApplyTelCom5Count;
                dr[7] = salesStatistics.ApplyCount;
                dr[8] = salesStatistics.ApplyRevenue;
                dr[9] = salesStatistics.ApplyProfit;
                dr[10] = salesStatistics.FittingCount;
                dr[11] = salesStatistics.FittingRevenue;
                dr[12] = salesStatistics.FittingProfit;
                dr[13] = salesStatistics.TotalProfit;
                dr[14] = salesStatistics.TargetAchievementRates;
                dr[15] = salesStatistics.NotGetTotalCommission;  

                table.Rows.Add(dr);
            }
        }

        if (storeList.Count > 1 && salesStatisticsList2 != null && salesStatisticsList2.Count > 0)
        {
            foreach (SalesStatisticsVO salesStatistics in salesStatisticsList2)
            {
                DataRow dr = table.NewRow();

                dr[0] = salesStatistics.Name;
                dr[1] = salesStatistics.Target;
                dr[2] = salesStatistics.ApplyTelCom1Count;
                dr[3] = salesStatistics.ApplyTelCom2Count;
                dr[4] = salesStatistics.ApplyTelCom3Count;
                dr[5] = salesStatistics.ApplyTelCom4Count;
                dr[6] = salesStatistics.ApplyTelCom5Count;
                dr[7] = salesStatistics.ApplyCount;
                dr[8] = salesStatistics.ApplyRevenue;
                dr[9] = salesStatistics.ApplyProfit;
                dr[10] = salesStatistics.FittingCount;
                dr[11] = salesStatistics.FittingRevenue;
                dr[12] = salesStatistics.FittingProfit;
                dr[13] = salesStatistics.TotalProfit;
                dr[14] = salesStatistics.TargetAchievementRates;
                dr[15] = salesStatistics.NotGetTotalCommission;

                table.Rows.Add(dr);
            }
        }

        string uploadRootPath = string.IsNullOrEmpty(m_ConfigHelper.ApiUrl) ? Server.MapPath("~\\") + "\\App_Data\\temp.xls" : "";
        NPOIHelper.ExportByWeb(table, "業績", string.Format("{0}業績.xls", ym), true, uploadRootPath);
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        if (e.Row.RowIndex != -1)
        {
            Control ctrl = e.Row;
            Label lblName = (Label)ctrl.FindControl("lblName");

            if (m_SessionHelper.IsAdmin && !"總合".Equals(lblName.Text))
            {
                UIHelper.SetContrlVisible(ref ctrl, "lblTarget", false);
                UIHelper.SetContrlVisible(ref ctrl, "txtTarget", true);
            }
            else
            {
                UIHelper.SetContrlVisible(ref ctrl, "lblTarget", true);
                UIHelper.SetContrlVisible(ref ctrl, "txtTarget", false);
            }


            Label lblTargetAchievementRates = (Label)ctrl.FindControl("lblTargetAchievementRates");
            double rates = double.Parse(lblTargetAchievementRates.Text.Trim());
            if (rates < 60)
            {
                lblTargetAchievementRates.ForeColor = System.Drawing.Color.Red;
            }            
        }
    }

    protected void gvListStore_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        if (e.Row.RowIndex != -1)
        {
            Control ctrl = e.Row;
            Label lblName = (Label)ctrl.FindControl("lblName");

            if (m_SessionHelper.IsAdmin && !"總合".Equals(lblName.Text))
            {
                UIHelper.SetContrlVisible(ref ctrl, "lblTarget", false);
                UIHelper.SetContrlVisible(ref ctrl, "txtTarget", true);
            }
            else
            {
                UIHelper.SetContrlVisible(ref ctrl, "lblTarget", true);
                UIHelper.SetContrlVisible(ref ctrl, "txtTarget", false);
            }


            Label lblTargetAchievementRates = (Label)ctrl.FindControl("lblTargetAchievementRates");
            double rates = double.Parse(lblTargetAchievementRates.Text.Trim());
            if (rates < 60)
            {
                lblTargetAchievementRates.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    
    private void InitDDL()
    {
        ddlSearchYear.Items.Clear();
        for (int i = ConvertUtil.UtcDateTimeToTaiwanDateTime(DateTime.UtcNow).Year; i >= 2014; i--)
        {
            ddlSearchYear.Items.Add(new ListItem(i.ToString()));
        }

        ddlSearchMonth.Items.Clear();
        for (int i = 1; i <= 12; i++)
        {
            ddlSearchMonth.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
        }

        ddlSearchMonth.SelectedValue = ConvertUtil.UtcDateTimeToTaiwanDateTime(DateTime.UtcNow).Month.ToString().PadLeft(2, '0');
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        IList<TargetVO> targetList = new List<TargetVO>();

        string ym = string.Format("{0}{1}", ddlSearchYear.SelectedValue, ddlSearchMonth.SelectedValue);

        for (int i = 0; i < gvList.Rows.Count; i++)
        {
            Label lblName = (Label)gvList.Rows[i].FindControl("lblName");
            TextBox txtTarget = (TextBox)gvList.Rows[i].FindControl("txtTarget");

            double amount = 0;

            double.TryParse(txtTarget.Text.Trim(), out amount);

            TargetVO targetVO = new TargetVO();
            targetVO.Id = string.Format("{0}{1}", ym, lblName.Text);
            targetVO.Name = lblName.Text;
            targetVO.Amount = amount;            
            targetList.Add(targetVO);
        }

        for (int i = 0; i < gvListStore.Rows.Count; i++)
        {
            Label lblName = (Label)gvListStore.Rows[i].FindControl("lblName");
            TextBox txtTarget = (TextBox)gvListStore.Rows[i].FindControl("txtTarget");

            double amount = 0;

            double.TryParse(txtTarget.Text.Trim(), out amount);

            TargetVO targetVO = new TargetVO();
            targetVO.Id = string.Format("{0}{1}", ym, lblName.Text);
            targetVO.Name = lblName.Text;
            targetVO.Amount = amount;
            targetList.Add(targetVO);
        }

        m_AccountingService.UpdateTargetList(targetList);

        fillGridView();
    }
}