using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_UserControl_CKEditorForMulti : System.Web.UI.UserControl
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