using System;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Microsoft.VisualBasic;

namespace ChandigarheServices
{

    public class Global_asax : HttpApplication
    {

        public void Application_Start(object sender, EventArgs e)
        {
            
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }



        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {

            string _sessionIPAddress = string.Empty;
            string _BrowserDtl = string.Empty;
            // string _sessionBrowserInfo = string.Empty;
            if (HttpContext.Current.Session != null)
            {
                if (HttpContext.Current.Session["UserID"] != null)
                {

                    string _encryptedString = Convert.ToString(HttpContext.Current.Session["encryptedSession"]);
                    var _encodedAsBytes = Convert.FromBase64String(_encryptedString);
                    string _decryptedString = Encoding.ASCII.GetString(_encodedAsBytes);

                    var _separator = new char[] { '^' };
                    if (!string.IsNullOrEmpty(_decryptedString) && !string.IsNullOrEmpty(_decryptedString) && _decryptedString != null)
                    {
                        var _splitStrings = _decryptedString.Split(_separator);
                        if (_splitStrings.Count() > 0)
                        {

                            if (_splitStrings[2].Count() > 0)
                            {
                                var _userBrowserInfo = _splitStrings[2].Split('~');
                                if (_userBrowserInfo.Count() > 0)
                                {

                                    _sessionIPAddress = _userBrowserInfo[1];
                                    _BrowserDtl = _userBrowserInfo[0];
                                }
                            }
                        }
                    }

                    string _currentUseripAddress;
                    if (string.IsNullOrEmpty(Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                    {
                        _currentUseripAddress = Request.ServerVariables["REMOTE_ADDR"];
                    }
                    else
                    {
                        _currentUseripAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                    }

                    IPAddress result;
                    if (!IPAddress.TryParse(_currentUseripAddress, out result))
                    {
                        result = IPAddress.None;
                    }


                }
            }
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            try
            {
                if (Request.IsSecureConnection == true)
                {
                    Response.Cookies["ASP.NET_SessionId"].Secure = true;
                }
            }
            catch (Exception generatedExceptionName)
            {

                string userCode = "";
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    userCode = HttpContext.Current.User.Identity.Name.ToString();
                }

                var objActivityLog = new BLL.ActivityLog();
                objActivityLog.insertErrorLog(userCode, Request.ServerVariables["REMOTE_ADDR"].ToString(), "global.asax", "PageLoad", generatedExceptionName.Message.ToString());
                Server.ClearError();
                Response.Redirect("~/frmError.aspx");
                Response.Clear();
            }
        }


        public void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();
            string errMsg = "";
            string userCode = "";
            if (ex != null)
            {
                if (ex.GetType().IsSubclassOf(typeof(HttpException)))
                {
                    HttpException httpException = (HttpException)ex;
                    if (!(ex == null))
                    {

                        if (HttpContext.Current.User.Identity.IsAuthenticated)
                        {
                            userCode = HttpContext.Current.User.Identity.Name.ToString();
                        }
                        HttpException w32ex = (HttpException)ex;

                        if (!(ex.Message.ToString() == null))
                        {
                            errMsg = Strings.Left(ex.Message.ToString(), 1000);
                        }
                        if (ex.InnerException != null)
                        {
                            errMsg = errMsg + " | " + ex.InnerException.ToString();
                        }
                        var objActivityLog = new BLL.ActivityLog();
                        objActivityLog.insertErrorLog(userCode, Request.ServerVariables["REMOTE_ADDR"].ToString(), "global.asax", "PageLoad", errMsg);
                        Server.ClearError();
                        Response.Redirect("~/frmError.aspx");
                        Response.Clear();
                    }
                }
                else if (!(ex == null))
                {
                    var objActivityLog = new BLL.ActivityLog();
                    if (ex.Message.ToString().Contains("Validation of viewstate MAC failed"))
                    {
                        // 'Validation of viewstate MAC failed. If this application is hosted by a Web Farm or cluster, ensure that <machineKey> configuration specifies the same validationKey and validation algorithm. AutoGenerate cannot be used in a cluster.    See http://go.microsoft.com/fwlink/?LinkID=314055 for more information.
                        Server.ClearError();
                        Response.Clear();
                        Response.Write("Invalid Request");
                    }
                    else
                    {
                        objActivityLog.insertErrorLog(userCode, Request.ServerVariables["REMOTE_ADDR"].ToString(), "global.asax", "PageLoad", ex.Message);
                        Server.ClearError();
                        Response.Redirect("~/frmError.aspx");
                        Response.Clear();
                    }
                }
            }

        }

        public void Application_End(object sender, EventArgs e)
        {
            // Fires when the application ends
        }
    }
}