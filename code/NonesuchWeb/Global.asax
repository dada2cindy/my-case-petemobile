<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>

<script RunAt="server">
  
    protected void Application_Start(Object sender, EventArgs e)
    {
        ////計數器
        //string path = Server.MapPath("~/") + "counter.db";
        //if (!File.Exists(path))
        //{
        //    StreamWriter sw = File.CreateText(path);
        //    sw.WriteLine("0");
        //    sw.Close();
        //}
        //StreamReader sr = File.OpenText(path);
        //string str = sr.ReadToEnd();
        //long count = Int32.Parse(str);
        //Application["count"] = count;
        //sr.Close();
    }

    protected void Session_Start(Object sender, EventArgs e)
    {
        ////計數器
        //Application.Lock();
        //Application["count"] = (long)Application["count"] + 1;
        //long count = (long)Application["count"];
        //string path = Server.MapPath("~/") + "counter.db";

        //StreamWriter sw = new StreamWriter(path, false);
        //sw.WriteLine(count);
        //sw.Close();

    }

    protected void Application_BeginRequest(Object sender, EventArgs e)
    {

    }

    protected void Application_EndRequest(Object sender, EventArgs e)
    {

    }

    protected void Application_AuthenticateRequest(Object sender, EventArgs e)
    {

    }

    protected void Application_Error(Object sender, EventArgs e)
    {

    }

    protected void Session_End(Object sender, EventArgs e)
    {

    } 
       
</script>

