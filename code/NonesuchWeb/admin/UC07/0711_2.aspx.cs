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
using System.Drawing;

public partial class admin_UC07_0711_2 : System.Web.UI.Page
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
            ////先更新到今天之前的結帳
            m_AccountingService.UpdateCash(); 
            txtDate.Text = DateTime.Today.ToString("yyyy/MM/dd");

            ShowMode();                       
            LoadDataToUI();
        }
    }

    private void ShowMode()
    {
        string empty = "無資料";

        lblCashYesterday.Text = empty;
        SetLabelColor(lblCashYesterday);

        lblTotalToday.Text = empty;
        SetLabelColor(lblTotalToday);

        lblCashToday.Text = empty;
        SetLabelColor(lblCashToday);

        lblBuyToday.Text = empty;
        SetLabelColor(lblBuyToday);

        lblSellToday.Text = empty;
        SetLabelColor(lblSellToday);

        lblMobileToday.Text = empty;
        SetLabelColor(lblMobileToday);

        lblSpecialToday.Text = empty;
        SetLabelColor(lblSpecialToday);

        gvBuyToday.DataSource = null;
        gvBuyToday.DataBind();

        gvSellToday.DataSource = null;
        gvSellToday.DataBind();

        gvMobileToday.DataSource = null;
        gvMobileToday.DataBind();

        gvSpecialToday.DataSource = null;
        gvSpecialToday.DataBind();
    }

    private void SetLabelColor(Label lbl)
    {
        double number;

        if (double.TryParse(lbl.Text,out number))
        {
            if (number > 0)
            {
                lbl.ForeColor = Color.Blue;
            }
            else if (number < 0)
            {
                lbl.ForeColor = Color.Red;
            }
            else
            {
                lbl.ForeColor = Color.Black;
            }
        }
        else
        {
            lbl.ForeColor = Color.Black;
        }
    }

    private void LoadDataToUI()
    {
        DateTime date;

        if (DateTime.TryParse(txtDate.Text.Trim(), out date))
        {
            ////今日目前的結餘
            CashStatisticsVO cashStatisticsVO = m_AccountingService.GetCashStatisticsVO(date);
            if (cashStatisticsVO != null)
            {
                lblCashYesterday.Text = cashStatisticsVO.CashYesterday.ToString();
                SetLabelColor(lblCashYesterday);

                lblCashToday.Text = cashStatisticsVO.CashToday.ToString();
                SetLabelColor(lblCashToday);

                lblTotalToday.Text = cashStatisticsVO.TotalToday.ToString();
                SetLabelColor(lblTotalToday);

                lblBuyToday.Text = cashStatisticsVO.BuyToday.ToString();
                SetLabelColor(lblBuyToday);

                lblSellToday.Text = cashStatisticsVO.SellToday.ToString();
                SetLabelColor(lblSellToday);

                lblMobileToday.Text = cashStatisticsVO.MobileToday.ToString();
                SetLabelColor(lblMobileToday);

                lblSpecialToday.Text = cashStatisticsVO.SpecialToday.ToString();
                SetLabelColor(lblSpecialToday);


                ////今日進貨
                Dictionary<string, string> conditionsBuyToday = new Dictionary<string, string>();
                conditionsBuyToday.Add("Flag", "1");
                conditionsBuyToday.Add("NodeId", "2");
                conditionsBuyToday.Add("ShowDate", date.ToString("yyyy/MM/dd"));
                gvBuyToday.DataSource = m_PostService.GetPostList(conditionsBuyToday);
                gvBuyToday.DataBind();

                ////今日銷貨
                Dictionary<string, string> conditionsSellToday = new Dictionary<string, string>();
                conditionsSellToday.Add("Flag", "1");
                conditionsSellToday.Add("NodeId", "2");
                //conditionsSellToday.Add("WithOutMemberId", "1");
                conditionsSellToday.Add("CloseDate", date.ToString("yyyy/MM/dd"));
                gvSellToday.DataSource = m_PostService.GetPostList(conditionsSellToday);
                gvSellToday.DataBind();

                ////今日門號
                IList<NodeVO> storeList = m_PostService.GetNodeListByParentName("店家");
                Dictionary<string, string> conditionsMember = new Dictionary<string, string>();
                conditionsMember.Add("Status", "1");
                conditionsMember.Add("ApplyDate2", date.ToString("yyyy/MM/dd"));
                conditionsMember.Add("Store", storeList[0].Name);
                gvMobileToday.DataSource = m_MemberService.GetMemberList(conditionsMember);
                gvMobileToday.DataBind();

                ////特別收支
                NodeVO nodeSpecial = m_PostService.GetNodeByName("#特別現金收支");

                Dictionary<string, string> conditionsSpecial = new Dictionary<string, string>();
                conditionsSpecial.Add("Flag", "1");
                conditionsSpecial.Add("NodeId", nodeSpecial.NodeId.ToString());
                conditionsSpecial.Add("CloseDate", date.ToString("yyyy/MM/dd"));
                gvSpecialToday.DataSource = m_PostService.GetPostList(conditionsSpecial);
                gvSpecialToday.DataBind();
            }
            else
            {
                ShowMode();
            }            
        }
        else
        {
            ShowMode();
        }
    }    
        
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LoadDataToUI();
    }

    protected void gvSpecialToday_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        int postId = int.Parse(e.CommandArgument.ToString());
        PostVO postVO = m_PostService.GetPostById(postId);
        switch (cmdName)
        {
            case "myDel":
                postVO.Flag = 0;
                m_PostService.UpdatePost(postVO);
                m_WebLogService.AddSystemLog(MsgVO.Action.刪除, postVO, "", string.Format("單號:{0}", postVO.PostId));
                break;

            default:
                break;
        }
        LoadDataToUI();
    }

    protected void gvSpecialToday_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //GridView gv = (GridView)sender;
        //if (e.Row.RowIndex != -1)
        //{
        //    Control ctrl = e.Row;
        //    int postId = int.Parse(UIHelper.FindHiddenValue(ref ctrl, "hdnPostId"));
        //    PostVO postVO = m_PostService.GetPostById(postId);
        //}
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        DateTime date;

        if (DateTime.TryParse(txtDate.Text.Trim(), out date))
        {
            NodeVO nodeSpecial = m_PostService.GetNodeByName("#特別現金收支");

            PostVO postVO = new PostVO();
            postVO.Node = nodeSpecial;
            postVO.CloseDate = date;
            postVO.Title = txtTitle.Text.Trim();
            postVO.Type = int.Parse(ddlType.SelectedValue);
            if (postVO.Type == 1)
            {
                postVO.Price = -1 * (int.Parse(txtPrice.Text.Trim()));
            }
            else
            {
                postVO.Price = (int.Parse(txtPrice.Text.Trim()));
            }
            m_PostService.CreatePost(postVO);

            UIHelper.ClearUI(pnlSpecialToday);
            ddlType.SelectedValue = "";
            LoadDataToUI();
        }

        else
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS("日期格式錯誤!"), false);
            return;
        }
    }
}