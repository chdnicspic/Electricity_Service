using System;
using System.Web;
using System.Web.UI;
using VALIDATION;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

public partial class MasterPage : System.Web.UI.MasterPage
{
    private InterfaceCLS.InterfaceCLS MyInterface;
    private BLL.AppProcessing MyImplementer = new BLL.AppProcessing();
    private Validation validation = new Validation();
    private SqlCommand mycommand = new SqlCommand();
    private DataTable mytable = new DataTable();
    public string strRootPath;


    private const string AntiXsrfTokenKey = "__AntiXsrfToken";
    private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
    private string _antiXsrfTokenValue;

    public MasterPage()
    {
        this.Init += Page_Init;
        this.Load += Page_Load;
    }

    protected override void OnInit(EventArgs e)
    {
        if (!this.Page.EnableViewStateMac)
        {
            Response.Write("Invalid Request");
        }
        Page.ViewStateUserKey = Session.SessionID;
        base.OnInit(e);
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.AddHeader("Cache-control", "no-store, must-revalidate,private,no-cache");
        Response.AddHeader("PRAGMA", "NO-Cache");
        Response.Expires = -1;
        Response.Expires = 0;
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now.AddDays(-2));


        // '''Set Token For CSRF Prevention''''
        var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        var requestCookieGuidValue = default(Guid);
        if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        {
            _antiXsrfTokenValue = requestCookie.Value;
            Page.ViewStateUserKey = _antiXsrfTokenValue;
        }
        else
        {
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
            Page.ViewStateUserKey = _antiXsrfTokenValue;
            var responseCookie = new HttpCookie(AntiXsrfTokenKey) { HttpOnly = true, Value = _antiXsrfTokenValue };
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                responseCookie.Secure = true;
            Response.Cookies.Set(responseCookie);
        }


    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetLastModified(DateTime.Now);
        Response.Cache.SetAllowResponseInBrowserHistory(false);
        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();

        // '''Check Token For CSRF Prevention''''
        if (!IsPostBack)
        {
            ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
            ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? string.Empty;
        }
        else if ((ViewState[AntiXsrfTokenKey].ToString() ?? "") != (_antiXsrfTokenValue ?? "") || (ViewState[AntiXsrfUserNameKey].ToString() ?? "") != ((Context.User.Identity.Name ?? string.Empty) ?? ""))
        {
            Response.Write("Invalid Request");
        }


        if (!Request.Url.ToString().Contains("localhost"))
        {
            strRootPath = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath + "/";
        }
        else
        {
            strRootPath = Request.Url.GetLeftPart(UriPartial.Authority) + Request.ApplicationPath;
        }
        if (Session["UserID"] == null)
        {
            Response.Redirect("~/LogOut.aspx", false);
        }

        HttpCookie cookie = Request.Cookies[".ASPXFORMAUTH"];

        if (Session["TicketID"] != null)
        {
            // 'Check authenticated cookie
            if (cookie != null)
            {
                if (Request.ServerVariables["SERVER_NAME"] != "localhost")
                {
                    if (cookie.Value != Session["TicketID"].ToString())
                    {
                        Response.Redirect("~/LogOut.aspx", false);
                        return;
                    }
                }
            }
        }
        // ''Check New Cookie

        else
        {
            Response.Redirect("~/LogOut.aspx", false);
            return;
        }

        // Masterpage code START
        // Masterpage code END
    }
}
