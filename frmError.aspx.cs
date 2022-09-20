using System;
using System.Web;
using System.Web.Security;

public partial class frmError : SetCulture
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetLastModified(DateTime.Now);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
    
        if (HttpContext.Current.Session != null)
            Session.Abandon();
        Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-30);
        Response.Cookies["AuthCookie"].Expires = DateTime.Now.AddDays(-30);
        FormsAuthentication.SignOut();
    }

    protected void Page_Init(object sender, System.EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddDays(-2));
    }
}