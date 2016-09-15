using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WuDada.Core.Post;
using WuDada.Core.Post.Service;
using WuDada.Core.Post.Domain;
using WuDada.Core.Generic.Util;
using WuDada.Core.Post.DTO;
using WuDada.Core.Post.DTOConverter;

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Response.Redirect("~/admin/login/Login.aspx");
    }
}