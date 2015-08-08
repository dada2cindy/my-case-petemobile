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
        m_AccountingFactory = new AccountingFactory();
        m_AuthService = m_AuthFactory.GetAuthService();
        m_MemberService = m_MemberFactory.GetMemberService();
        m_PostService = m_PostFactory.GetPostService();
        m_AccountingService = m_AccountingFactory.GetAccountingService();

        if (!IsPostBack)
        {
            if (!IsPostBack)
            {
                InitDDL();
                ShowMode(); 
            }
                       
            fillGridView();                    
        }
    }

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
        string ym = string.Format("{0}{1}", ddlSearchYear.SelectedValue, ddlSearchMonth.SelectedValue);

        gvList.DataSource = m_AccountingService.GetSalesStatistics(ym, "興安總店");
        gvList.DataBind();
    }    

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        fillGridView();
    }

    protected void btnSearchExport_Click(object sender, EventArgs e)
    {
        string ym = string.Format("{0}{1}", ddlSearchYear.SelectedValue, ddlSearchMonth.SelectedValue);
        IList<SalesStatisticsVO> salesStatisticsList = m_AccountingService.GetSalesStatistics(ym, "興安總店");
        DataTable table = new DataTable();
        table.Columns.Add("姓名", typeof(string));
        table.Columns.Add("本月目標", typeof(double));
        table.Columns.Add("上線件數", typeof(int));
        table.Columns.Add("門號營收", typeof(double));
        table.Columns.Add("門號毛利", typeof(double));
        table.Columns.Add("配件件數", typeof(int));
        table.Columns.Add("配件營收", typeof(double));
        table.Columns.Add("配件毛利", typeof(double));
        table.Columns.Add("總毛利", typeof(double));
        table.Columns.Add("達成率", typeof(double));

        if (salesStatisticsList != null && salesStatisticsList.Count > 0)
        {
            foreach (SalesStatisticsVO salesStatistics in salesStatisticsList)
            {
                DataRow dr = table.NewRow();

                dr[0] = salesStatistics.Name;
                dr[1] = salesStatistics.Target;
                dr[2] = salesStatistics.ApplyCount;
                dr[3] = salesStatistics.ApplyRevenue;
                dr[4] = salesStatistics.ApplyProfit;
                dr[5] = salesStatistics.FittingCount;
                dr[6] = salesStatistics.FittingRevenue;
                dr[7] = salesStatistics.FittingProfit;
                dr[8] = salesStatistics.TotalProfit;
                dr[9] = salesStatistics.TargetAchievementRates;                

                table.Rows.Add(dr);
            }
        }

        NPOIHelper.ExportByWeb(table, "業績", string.Format("{0}業績.xls", ym), true);
    }

    protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView gv = (GridView)sender;
        if (e.Row.RowIndex != -1)
        {
            Control ctrl = e.Row;
            if (m_SessionHelper.IsAdmin)
            {
                UIHelper.SetContrlVisible(ref ctrl, "lblTarget", false);
                UIHelper.SetContrlVisible(ref ctrl, "txtTarget", true);
            }
            else
            {
                UIHelper.SetContrlVisible(ref ctrl, "lblTarget", true);
                UIHelper.SetContrlVisible(ref ctrl, "txtTarget", false);
            }
        }
    }
    
    private void InitDDL()
    {
        ddlSearchYear.Items.Clear();
        for (int i = DateTime.Today.Year; i >= 2014; i--)
        {
            ddlSearchYear.Items.Add(new ListItem(i.ToString()));
        }

        ddlSearchMonth.Items.Clear();
        for (int i = 1; i <= 12; i++)
        {
            ddlSearchMonth.Items.Add(new ListItem(i.ToString().PadLeft(2, '0')));
        }

        ddlSearchMonth.SelectedValue = DateTime.Today.Month.ToString().PadLeft(2, '0');
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

        m_AccountingService.UpdateTargetList(targetList);

        fillGridView();
    }
}