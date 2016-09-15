using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Logging;
using WuDada.Core.Post;
using WuDada.Core.Post.Domain;
using WuDada.Core.Post.Service;
using WuDada.Core.SystemApplications.Domain;

public partial class admin_UC04_0431 : System.Web.UI.Page
{
    private ILog m_Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    private PostFactory m_PostFactory;
    private IPostService m_PostService;
    private WebLogService m_WebLogService;

    private int m_NodeId = 9;

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
        nodeVO.PicFileName = m_PicFileName;
        nodeVO.ParentNode = m_PostService.GetNodeById(m_NodeId); ;
        nodeVO = m_PostService.CreateNode(nodeVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.新增, nodeVO);
        ClearUI();
        GetList();
    }

    private void ClearUI()
    {
        m_Mode = 0;
        m_PicFileName = string.Empty;
        txtNodeName.Text = string.Empty;
        ltlImg.Text = string.Empty;
    }

    private string GetPic(string fileName)
    {
        return "<img src='../../upload/" + fileName + "' width='94' height='42' border='0'>";
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
                m_PicFileName = nodeVO.PicFileName;
                ltlImg.Text = GetPic(m_PicFileName);
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
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS("無法刪除類別，品牌底下尚有關聯資料。"), false);
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

    protected void btnUpliad_Click(object sender, EventArgs e)
    {
        try
        {
            HttpFileCollection hfc = Request.Files;
            for (int i = 0; i < hfc.Count; i++)
            {
                HttpPostedFile hpf = hfc[i];
                if (hpf.ContentLength > 0)
                {
                    string fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + System.IO.Path.GetFileName(hpf.FileName);
                    hpf.SaveAs(Server.MapPath("~\\") + "\\upload\\" + fileName);
                    ltlImg.Text = GetPic(fileName);
                    m_PicFileName = fileName;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS("請點選您要上傳的照片!"), false);
                    return;
                }
            }
        }
        catch
        {
            ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "js", JavascriptUtil.AlertJS("檔案傳輸錯誤!"), false);
            return;
        }
    }        

    protected void btnSave_Click(object sender, EventArgs e)
    {
        NodeVO nodeVO = m_PostService.GetNodeById(m_Mode);
        nodeVO.Name = txtNodeName.Text.Trim();
        nodeVO.PicFileName = m_PicFileName;
        m_PostService.UpdateNode(nodeVO);
        m_WebLogService.AddSystemLog(MsgVO.Action.修改, nodeVO);
        GetList();
        ClearUI();
        ShowMode();
    }
}


