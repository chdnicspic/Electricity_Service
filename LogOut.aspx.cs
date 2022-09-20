using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using BLL;

public partial class LogOut : SetCulture
{
    public LogOut()
    {
        this.Load += Page_Load;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        string userid = "";
        if (Session["UserID"] == null)
        {
            userid = "No User Id";
        }
        else
        {
            userid = Session["UserID"].ToString();
        }
        updateLogDetailsGO(userid);
        Response.Cache.SetLastModified(DateTime.Now);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Session.Clear();
        Session.Abandon();
        Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddDays(-30);
        Response.Cookies["AuthCookieNew"].Expires = DateTime.Now.AddDays(-30);
        // 'If Not Response.Cookies["AuthCookieGlb"] Is Nothing Then
        // '    Response.Cookies["AuthCookieGlb"].Expires = DateTime.Now.AddDays(-30)
        // 'End If
        Session.RemoveAll();
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
        var objActivityLog = new BLL.ActivityLog();
        objActivityLog.InsertUserActivityLog(userid, Request.ServerVariables["REMOTE_ADDR"].ToString(), "LogOut", "LogOut", "Logout|");
        Response.Redirect("~/Home.aspx");
    }
    private void updateLogDetailsGO(string userid)
    {
        try
        {
            InterfaceCLS.InterfaceCLS MyInterface;
            var MyImplementer = new BLL.AppProcessing();
            var mycommand = new SqlCommand();
            MyInterface = MyImplementer;
            mycommand.Parameters.Clear();
            if (Session.SessionID == null | Session.SessionID == "")
            {
                mycommand.Parameters.Add("@SessionID", SqlDbType.VarChar, 100).Value = "No Session ID";
            }
            else
            {
                mycommand.Parameters.Add("@SessionID", SqlDbType.VarChar, 100).Value = Session.SessionID;
            }
            mycommand.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = userid;
            mycommand.Parameters.Add("@macIP", SqlDbType.VarChar, 20).Value = Request.UserHostAddress;
            mycommand.Parameters.Add("@ExternalIP", SqlDbType.VarChar, 50).Value = Request.ServerVariables["REMOTE_ADDR"].ToString();
            mycommand.Parameters.Add("@IPDetails", SqlDbType.VarChar).Value = Request.UserHostAddress; // getgeoloction()
            mycommand.Parameters.Add("@Flag", SqlDbType.Char).Value = "O";
            MyInterface.UPDATEData(mycommand, "UpdateLogDetails");
        }
        catch (Exception ex)
        {
            var objActivityLog = new ActivityLog();
            objActivityLog.insertErrorLog(userid, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "updateLogDetailsGO", ex.Message);
        }
    }
}