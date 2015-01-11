using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.SystemApplications;
using WuDada.Core.SystemApplications.Service;
using WuDada.Core.SystemApplications.Domain;

public partial class admin_UC06_0602 : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private SystemFactory m_SystemFactory;
    private ISystemService m_SystemService;
    private WebLogService m_WebLogService;
    private string m_Classify = "聯絡我們收件者";

    private int m_Mode
    {
        get { if (ViewState["mode"] == null) { ViewState["mode"] = 0; } return int.Parse(ViewState["mode"].ToString()); }
        set { ViewState["mode"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        m_WebLogService = new WebLogService();
        m_SystemFactory = new SystemFactory();
        m_SystemService = m_SystemFactory.GetSystemService();

        if (!IsPostBack)
        {
            pnlContent.Visible = false;
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
        gvList.DataSource = m_SystemService.GetAllItemParamByNoDel(m_Classify);
        gvList.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        ItemParamVO iemParamVO = new ItemParamVO();
        iemParamVO.Classify = m_Classify;
        iemParamVO.Name = txtName.Text.Trim();
        iemParamVO.Value = txtValue.Text.Trim();
        m_SystemService.CreateItemParam(iemParamVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.新增, iemParamVO);
        ClearUI();
        GetList();
    }

    private void ClearUI()
    {
        m_Mode = 0;
        txtName.Text = string.Empty;
        txtValue.Text = string.Empty;
        pnlContent.Visible = false;
        btnShowAdd.Enabled = true;
    }

    protected void gvList_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        string cmdName = e.CommandName;
        int itemParamId = int.Parse(e.CommandArgument.ToString());
        ItemParamVO itemParamVO = m_SystemService.GetItemParamById(itemParamId);
        switch (cmdName)
        {
            case "myModify":
                ClearUI();
                m_Mode = itemParamId;
                txtName.Text = itemParamVO.Name;
                txtValue.Text = itemParamVO.Value;
                ShowMode();
                pnlContent.Visible = true;
                break;
            case "myDel":
                m_SystemService.DeleteItemParam(itemParamVO);
                m_WebLogService.AddSystemLog(MsgVO.Action.刪除, itemParamVO);
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
        ItemParamVO iemParamVO = m_SystemService.GetItemParamById(m_Mode);
        iemParamVO.Classify = m_Classify;
        iemParamVO.Name = txtName.Text.Trim();
        iemParamVO.Value = txtValue.Text.Trim();
        m_SystemService.UpdateItemParam(iemParamVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.修改, iemParamVO);
        GetList();
        ClearUI();
        ShowMode();
    }
    protected void btnShowAdd_Click(object sender, EventArgs e)
    {
        ClearUI();
        ShowMode();
        pnlContent.Visible = true;
        btnShowAdd.Enabled = false;
    }
}