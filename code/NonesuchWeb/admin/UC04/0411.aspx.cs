using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Post;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Service;
using WuDada.Core.SystemApplications.Domain;

public partial class admin_UC04_0411 : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private PostFactory m_PostFactory;
    private IPostService m_PostService;
    private WebLogService m_WebLogService;

    private int m_NodeId = 3;

    private int m_Mode
    {
        get { if (ViewState["mode"] == null) { ViewState["mode"] = 0; } return int.Parse(ViewState["mode"].ToString()); }
        set { ViewState["mode"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        m_WebLogService = new WebLogService();
        m_PostFactory = new PostFactory();
        m_PostService = m_PostFactory.GetPostService();

        if (!IsPostBack)
        {
            GetList();
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

    private void GetList()
    {
        gvList.DataSource = m_PostService.GetNodeListByParentId(m_NodeId);
        gvList.DataBind();
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        NodeVO nodeVO = new NodeVO();
        nodeVO.Name = txtNodeName.Text.Trim();
        nodeVO.SortNo = int.Parse(txtSortNo.Text.Trim());
        nodeVO.ParentNode = m_PostService.GetNodeById(m_NodeId); ;
        m_PostService.CreateNode(nodeVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.新增, nodeVO);
        ClearUI();
        GetList();
    }

    private void ClearUI()
    {
        m_Mode = 0;
        txtNodeName.Text = string.Empty;
        txtSortNo.Text = string.Empty;
    }
    protected void gvList_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        int nodeId = int.Parse(e.CommandArgument.ToString());
        NodeVO nodeVO = m_PostService.GetNodeById(nodeId);
        switch (cmdName)
        {
            case "myModify":
                m_Mode = nodeId;
                txtNodeName.Text = nodeVO.Name;
                txtSortNo.Text = nodeVO.SortNo.ToString();
                ShowMode();
                break;
            case "myDel":
                try
                {
                    m_PostService.DeleteNode(nodeVO);
                    m_WebLogService.AddSystemLog(MsgVO.Action.刪除, nodeVO);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS("無法刪除類別，類別底下尚有關聯資料。"), false);
                    m_Log.Error(ex);
                }
                break;

            default:
                break;
        }
        GetList();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearUI();
        ShowMode();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        NodeVO nodeVO = m_PostService.GetNodeById(m_Mode);
        nodeVO.Name = txtNodeName.Text.Trim();
        nodeVO.SortNo = int.Parse(txtSortNo.Text.Trim());
        m_PostService.UpdateNode(nodeVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.修改, nodeVO);
        GetList();
        ClearUI();
        ShowMode();
    }
}


