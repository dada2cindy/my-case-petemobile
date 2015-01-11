using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class admin_UserControl_CKEditor : System.Web.UI.UserControl
{
    public string value
    {

        set { mckeditor.Text = value; }

        get { return mckeditor.Text; }

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}
