<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Imaging;

public class Handler : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        using (Bitmap b = new Bitmap(50, 20))
        {
            Font f = new Font("Arial", 12, FontStyle.Bold);
            Graphics g = Graphics.FromImage(b);
            SolidBrush whiteBrush = new SolidBrush(Color.Black);
            SolidBrush blackBrush = new SolidBrush(Color.White);
            RectangleF canvas = new RectangleF(0, 3, 250, 50);
            g.FillRectangle(whiteBrush, canvas);
            int x1 = 0;
            int y1 = 0;
            Random rdn = new Random();
            for (int i = 0; i < 80; i++)
            {
                x1 = rdn.Next(0, b.Width);
                y1 = rdn.Next(0, b.Height);
                b.SetPixel(x1, y1, Color.Brown);
            }
            context.Session["Captcha"] = null;
            context.Session["Captcha"] = GetRandomString();
            g.DrawString(context.Session["Captcha"].ToString(), f, blackBrush, canvas);
        
            context.Response.ContentType = "image/gif";
            b.Save(context.Response.OutputStream, ImageFormat.Gif);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    private string GetRandomString()
    {
        string[] arrStr = "A,B,C,D,1,2,3,4,5,6,7,8,9,0".Split(",".ToCharArray());
        string strDraw = string.Empty;
        Random r = new Random();
        for (int i = 0; i < 4; i++)
        {
            strDraw += arrStr[r.Next(0, arrStr.Length - 1)];
        }
        return strDraw;
    }
}