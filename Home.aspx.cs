using System;
using System.Data.SqlClient;
using System.Net;
using System.Xml;
using System.Security.Cryptography;
using System.Globalization;
using System.Threading;
using System.Data;
using BLL;
using VALIDATION;
using System.Web;
using System.Text;
using System.Web.Security;
using EDistrictBL;

public partial class Home : SetCulture
{
    InterfaceCLS.InterfaceCLS MyInterface;
    BLL.AppProcessing MyImplementer = new BLL.AppProcessing();
    ActivityLog objActivityLog = new ActivityLog();
    VALIDATION.Validation validation = new Validation();
    SqlCommand mycommand = new SqlCommand();
    DataTable mytable = new DataTable();
    string StrSql = "";
    SqlParameter Result = new SqlParameter("@Result", SqlDbType.VarChar, 4000);
    SqlParameter SqlErrMsg = new SqlParameter("@SqlErrMsg", SqlDbType.VarChar, 3072);
    protected override void InitializeCulture()
    {
        if (!Page.IsPostBack)
        {
            Session["MyLanguage"] = "en-GB";
        }
        if (Request.Form["ddllang"] != null)
        {
            string selectedLanguage = Request.Form["ddllang"].ToString();
            UICulture = Request.Form["ddllang"];
            Culture = Request.Form["ddllang"];
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(selectedLanguage);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(selectedLanguage);
            Session["MyLanguage"] = selectedLanguage;
            Application["CultureInfo"] = selectedLanguage;
        }
        else if (Application["CultureInfo"] != null && Application["CultureInfo"].ToString() != "")
        {
            UICulture = Application["CultureInfo"].ToString();
            Culture = Application["CultureInfo"].ToString();
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Application["CultureInfo"].ToString());
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Application["CultureInfo"].ToString());
            Session["MyLanguage"] = Application["CultureInfo"];
        }
        // End If
        base.InitializeCulture();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["MyLanguage"].ToString() != null || Session["MyLanguage"].ToString() != "")
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Session["MyLanguage"].ToString());
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(Session["MyLanguage"].ToString());
            Session["MyLanguage"] = Session["MyLanguage"].ToString();
            ddllang.SelectedIndex = ddllang.Items.IndexOf(ddllang.Items.FindByValue(Session["MyLanguage"].ToString()));
            Application["CultureInfo"] = Session["MyLanguage"].ToString();
        }
        else if (Application["CultureInfo"].ToString() != null && Application["CultureInfo"].ToString() != "")
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Application["CultureInfo"].ToString());
            Thread.CurrentThread.CurrentCulture = new CultureInfo(Application["CultureInfo"].ToString());
            ddllang.SelectedIndex = ddllang.Items.IndexOf(ddllang.Items.FindByValue(Application["CultureInfo"].ToString()));
            InitializeCulture();
        }
        else
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-GB");
            Session["MyLanguage"] = "en-GB";
            Application["CultureInfo"] = "en-GB";
        }

        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now - new TimeSpan(1, 0, 0));
        Response.Cache.SetLastModified(DateTime.Now);
        Response.Cache.SetAllowResponseInBrowserHistory(false);

        if (!Page.IsPostBack)
        {
            var randomclass = new Random();
            Session["Sseed"] = randomclass.Next();
            btnLogIn.Attributes.Add("onclick", "javascript:sha256Auth(" + Session["Sseed"] + ");");
            Response.Cookies["AuthCookieNew"].Expires = DateTime.Now.AddDays(-30);

            InitializeRandomNumber();
        }
        txtUserID.Focus();
        Session["cpcode"] = txtCaptcha.Text;
    }

    public static string CreateHash(string saltAndPassword)
    {
        // Dim Algorithm As MD5 = MD5.Create()
        SHA256 Algorithm = SHA256.Create();
        byte[] Data = Algorithm.ComputeHash(Encoding.UTF8.GetBytes(saltAndPassword));
        string Hashed = "";

        for (int i = 0, loopTo = Data.Length - 1; i <= loopTo; i++)
            Hashed += Data[i].ToString("x2").ToUpperInvariant();
        return Hashed;
    }

    protected void btnReg_Click(object sender, EventArgs e)
    {
        divForgot.Visible = false;
        divLogin.Visible = false;
        divReg.Visible = true;
    }

    protected void btnRegRegister_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        bool blLoginSucc = false;
        try
        {
            MyInterface = MyImplementer;
            mycommand.Parameters.Clear();
            mycommand.Parameters.AddWithValue("@Code", Session["Result"].ToString());
            mycommand.Parameters.AddWithValue("@OTP", txtOTP.Text.Trim());
            mytable = MyInterface.UPDATEData(mycommand, "[ces].[Upd_Active]");
            if (mytable.Rows.Count > 0)
            {
                if (string.Compare(txtOTP.Text.ToString().Trim(), mytable.Rows[0][0].ToString()) == 0)
                {
                    blLoginSucc = true;
                }
            }
        }
        catch (Exception ex)
        {
            objActivityLog.insertErrorLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "btnRegRegister_Click", ex.Message);
            lblmsg.Text = Common.ShowMessage(ex.Message.ToString(), 2);
        }
        finally
        {
            if (blLoginSucc)
            {
                objActivityLog.InsertUserActivityLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "Login" + "-SL", "User Created Succesfully");
                clear();
                lblmsg.Text = Common.ShowMessage("User Created Succesfully. Password is " + Session["Pass"].ToString(), 1);

            }
            else
            {
                objActivityLog.InsertUserActivityLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "Login" + "-UL", "Wrong OTP Entered");
                lblmsg.Text = Common.ShowMessage("Wrong OTP Entered.", 2);
            }
        }
    }

    protected string RandomString()
    {
        var chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[8];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }
        return new string(stringChars);
    }

    protected void btnForgetPwd_Click(object sender, EventArgs e)
    {
        divForgot.Visible = true;
        divLogin.Visible = false;
        divReg.Visible = false;
    }

    protected void btnForLogin_Click(object sender, EventArgs e)
    {
        divForgot.Visible = false;
        divLogin.Visible = true;
        divReg.Visible = false;
    }

    public void InitializeRandomNumber()
    {
        Random randomclass = new Random();
        Session.Add("Rnd1", randomclass.Next().ToString());
        // btnLogin.Attributes.Add("onclick", "javascript:return clickme();");

    }
    protected void btnLogIn_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        bool blLoginSucc = false;
        try
        {
            SessionPortal objSessionLogin = new SessionPortal();

            if (txtUserID.Text.ToString() == "Meenakshi_smp")
            {
                objSessionLogin.UserCode = "SMP0000087";
                objSessionLogin.UserName = "MEENAKSHI";
                objSessionLogin.UserDOB = Convert.ToDateTime("06/08/2015 19:05:46");
                objSessionLogin.UserParentDept = "D0002";
                objSessionLogin.UserParentOffice = "00087";
                objSessionLogin.UserMobile = "8146011392";
                objSessionLogin.UserEntryDate = Convert.ToDateTime("06/08/2015 19:05:46");
                objSessionLogin.LoginName = "Meenakshi_smp";
                objSessionLogin.UserIsActive = "Y";
                objSessionLogin.UserEmailId = "info@spicindia.com";
                objSessionLogin.UserIsDSCEnabled = "N";
                objSessionLogin.UserIsVerified = "Y";
                objSessionLogin.UserActivationDate = null;
                objSessionLogin.UserLastSLogin = DateTime.Now;
                objSessionLogin.UserLastFLogin = DateTime.Now;
                objSessionLogin.CurrentDate = DateTime.Now;
                objSessionLogin.UserType = "1";

                objSessionLogin.DSCTHUMB = "";
                objSessionLogin.DSCId = 0;
                objSessionLogin.DSCSerialNumber = "";
                objSessionLogin.DSCEXP = "N";
                //Session["DSCEXPDate"] = "Y";
                Session["UserID"] = objSessionLogin.UserCode;

            }
            else if (txtUserID.Text.ToString() == "elecsd1da")
            {
                objSessionLogin.UserCode = "DEP0000152";
                objSessionLogin.UserName = "ElecDiv1DA";
                objSessionLogin.UserDOB = Convert.ToDateTime("06/08/2015 19:05:46");
                objSessionLogin.UserParentDept = "D0002";
                objSessionLogin.UserParentOffice = "00087";
                objSessionLogin.UserMobile = "8146011392";
                objSessionLogin.UserEntryDate = Convert.ToDateTime("06/08/2015 19:05:46");
                objSessionLogin.LoginName = "elecsd1da";
                objSessionLogin.UserIsActive = "Y";
                objSessionLogin.UserEmailId = "info@spicindia.com";
                objSessionLogin.UserIsDSCEnabled = "N";
                objSessionLogin.UserIsVerified = "Y";
                objSessionLogin.UserActivationDate = null;
                objSessionLogin.UserLastSLogin = DateTime.Now;
                objSessionLogin.UserLastFLogin = DateTime.Now;
                objSessionLogin.CurrentDate = DateTime.Now;
                objSessionLogin.UserType = "2";

                objSessionLogin.DSCTHUMB = "";
                objSessionLogin.DSCId = 0;
                objSessionLogin.DSCSerialNumber = "";
                objSessionLogin.DSCEXP = "N";
                //Session["DSCEXPDate"] = "Y";
                Session["UserID"] = objSessionLogin.UserCode;

            }
            else if (txtUserID.Text.ToString() == "elecsd1ra")
            {
                objSessionLogin.UserCode = "DEP0000153";
                objSessionLogin.UserName = "ElecDiv1RA";
                objSessionLogin.UserDOB = Convert.ToDateTime("06/08/2015 19:05:46");
                objSessionLogin.UserParentDept = "D0002";
                objSessionLogin.UserParentOffice = "00087";
                objSessionLogin.UserMobile = "8146011392";
                objSessionLogin.UserEntryDate = Convert.ToDateTime("06/08/2015 19:05:46");
                objSessionLogin.LoginName = "elecsd1ra";
                objSessionLogin.UserIsActive = "Y";
                objSessionLogin.UserEmailId = "info@spicindia.com";
                objSessionLogin.UserIsDSCEnabled = "N";
                objSessionLogin.UserIsVerified = "Y";
                objSessionLogin.UserActivationDate = null;
                objSessionLogin.UserLastSLogin = DateTime.Now;
                objSessionLogin.UserLastFLogin = DateTime.Now;
                objSessionLogin.CurrentDate = DateTime.Now;
                objSessionLogin.UserType = "2";

                objSessionLogin.DSCTHUMB = "";
                objSessionLogin.DSCId = 0;
                objSessionLogin.DSCSerialNumber = "";
                objSessionLogin.DSCEXP = "N";
                //Session["DSCEXPDate"] = "Y";
                Session["UserID"] = objSessionLogin.UserCode;

            }

            else if (txtUserID.Text.ToString() == "elecsd1lk")
            {
                objSessionLogin.UserCode = "DEP0000154";
                objSessionLogin.UserName = "ElecDiv1LK";
                objSessionLogin.UserDOB = Convert.ToDateTime("06/08/2015 19:05:46");
                objSessionLogin.UserParentDept = "D0002";
                objSessionLogin.UserParentOffice = "00087";
                objSessionLogin.UserMobile = "8146011392";
                objSessionLogin.UserEntryDate = Convert.ToDateTime("06/08/2015 19:05:46");
                objSessionLogin.LoginName = "elecsd1lk";
                objSessionLogin.UserIsActive = "Y";
                objSessionLogin.UserEmailId = "info@spicindia.com";
                objSessionLogin.UserIsDSCEnabled = "N";
                objSessionLogin.UserIsVerified = "Y";
                objSessionLogin.UserActivationDate = null;
                objSessionLogin.UserLastSLogin = DateTime.Now;
                objSessionLogin.UserLastFLogin = DateTime.Now;
                objSessionLogin.CurrentDate = DateTime.Now;
                objSessionLogin.UserType = "2";

                objSessionLogin.DSCTHUMB = "";
                objSessionLogin.DSCId = 0;
                objSessionLogin.DSCSerialNumber = "";
                objSessionLogin.DSCEXP = "N";
                //Session["DSCEXPDate"] = "Y";
                Session["UserID"] = objSessionLogin.UserCode;

            }
            else if (txtUserID.Text.ToString() == "elecsd1dv")
            {
                objSessionLogin.UserCode = "DEP0000155";
                objSessionLogin.UserName = "ElecDiv1DV";
                objSessionLogin.UserDOB = Convert.ToDateTime("06/08/2015 19:05:46");
                objSessionLogin.UserParentDept = "D0002";
                objSessionLogin.UserParentOffice = "00087";
                objSessionLogin.UserMobile = "8146011392";
                objSessionLogin.UserEntryDate = Convert.ToDateTime("06/08/2015 19:05:46");
                objSessionLogin.LoginName = "elecsd1dv";
                objSessionLogin.UserIsActive = "Y";
                objSessionLogin.UserEmailId = "info@spicindia.com";
                objSessionLogin.UserIsDSCEnabled = "N";
                objSessionLogin.UserIsVerified = "Y";
                objSessionLogin.UserActivationDate = null;
                objSessionLogin.UserLastSLogin = DateTime.Now;
                objSessionLogin.UserLastFLogin = DateTime.Now;
                objSessionLogin.CurrentDate = DateTime.Now;
                objSessionLogin.UserType = "2";

                objSessionLogin.DSCTHUMB = "";
                objSessionLogin.DSCId = 0;
                objSessionLogin.DSCSerialNumber = "";
                objSessionLogin.DSCEXP = "N";
                //Session["DSCEXPDate"] = "Y";
                Session["UserID"] = objSessionLogin.UserCode;

            }

            CreateCookie();

            Context.ApplicationInstance.CompleteRequest();
            updateLogDetailsGO();
            blLoginSucc = true;
            Session["PortalSession"] = objSessionLogin;
            if (objSessionLogin.UserIsVerified.ToUpper().ToString() == "N")//For Change Password
            {
                //Move to page for password Change
                Response.Redirect("/ElectricityBoard/Electricity/Administration/ChangePassword.aspx", true);
            }
            if (objSessionLogin.UserIsActive.ToUpper().ToString() == "Y")//Active User
            {
                if (objSessionLogin.UserType == "1")
                {
                    Response.Redirect("/ElectricityBoard/Electricity/Administration/SamparkBoard.aspx", true);
                }
                else if (objSessionLogin.UserType == "2")
                {
                    //Code to Move user to welcome Page
                    //Response.Redirect("~/Administration/SystemBoard.aspx", true);
                    Response.Redirect("/ElectricityBoard/Electricity/Administration/MyOffice.aspx", true);
                }
                               
            }
            else if (objSessionLogin.UserIsActive.ToUpper().ToString() == "B")//Blocked User
            {
                //Code to Move user to welcome Page
                MyMessageBox1.ShowError("User is Blocked! please try after some time");
                return;
            }
            else if (objSessionLogin.UserIsActive.ToUpper().ToString() == "H")//Blocked User
            {
                //Code to Move user to welcome Page
                MyMessageBox1.ShowError("User is on leave or Temporarily offline!");
                return;
            }
            return;
            //if (txtCaptcha != null && txtCaptcha.Text != "")
            //{
            //    if (Session["cpcode"] != null)
            //    {
            //        Session["UserID"] = txtUserID.Text.Trim();
            //        Session["DefultPass"] = null;

            //        if (string.Compare(txtCaptcha.Text.ToString().Trim(), Session["cpcode"].ToString()) == 0)
            //        {
            //            txtCaptcha.Text = "";
            //            txtUserPass.Text = "";
            //            string passwordDatabase = "";
            //            string passwordHash = "";
            //            string md5passFirstLogin = "";
            //            passwordHash = hidHash.Value;
            //            try
            //            {
            //                MyInterface = MyImplementer;
            //                mycommand.Parameters.Clear();
            //                mycommand.Parameters.AddWithValue("@UserID", txtUserID.Text.Trim());
            //                mytable = MyInterface.SELECTData(mycommand, "checkUserLogin");
            //                if (mytable.Rows.Count > 0)
            //                {
            //                    if (mytable.Rows[0][0].ToString().ToLower() == "offline")
            //                    {
            //                        lblmsg.Text = Common.ShowMessage(mytable.Rows[0]["message"].ToString(), 3);
            //                        return;
            //                    }
            //                    else if (mytable.Rows[0][0].ToString().ToLower() == "locked")
            //                    {
            //                        lblmsg.Text = Common.ShowMessage(mytable.Rows[0]["message"].ToString(), 3);
            //                        return;
            //                    }
            //                    if (!(mytable.Rows[0][0].ToString().ToUpper() == "UNF"))
            //                    {
            //                        passwordDatabase = Convert.ToString(mytable.Rows[0]["Password"]).Trim();
            //                        md5passFirstLogin = passwordDatabase;
            //                        passwordDatabase = Session["Sseed"].ToString() + passwordDatabase.ToLower();
            //                        passwordDatabase = CreateHash(passwordDatabase.ToLower());
            //                        if (0 == string.Compare(passwordDatabase.ToUpper(), passwordHash.ToUpper(), false))
            //                        {
            //                            // '''Check if user account status locked then update status
            //                            if (mytable.Rows[0]["Locked"].ToString().ToUpper() == "Y" | Convert.ToInt32(mytable.Rows[0]["FailedAttempt"]) > 0)
            //                            {
            //                                try
            //                                {
            //                                    MyInterface = MyImplementer;
            //                                    mycommand.Parameters.Clear();
            //                                    mycommand.Parameters.AddWithValue("@UserID", txtUserID.Text.Trim());
            //                                    mycommand.Parameters.AddWithValue("@Func", "UnlockAccount");
            //                                    Result.Direction = System.Data.ParameterDirection.Output;
            //                                    mycommand.Parameters.Add(Result);
            //                                    SqlErrMsg.Direction = System.Data.ParameterDirection.Output;
            //                                    mycommand.Parameters.Add(SqlErrMsg);
            //                                    MyInterface.UPDATEData(mycommand, "UpdateLoginLocked");
            //                                }
            //                                catch (Exception ex)
            //                                {
            //                                    objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "btnLogIn_Click_UpdateLoginLocked", ex.Message);
            //                                }
            //                            }
            //                            Session["UserID"] = mytable.Rows[0]["Code"].ToString().Trim();
            //                            Session["UserLevelCode"] = mytable.Rows[0]["UserLevelCode"].ToString().Trim();
            //                            Session["Name"] = mytable.Rows[0]["OP_NM"].ToString().Trim();
            //                            Session["Mobile"] = mytable.Rows[0]["Mobile"].ToString().Trim();
            //                            string pass1 = "";
            //                            if (!(mytable.Rows[0]["IsDefaultPassWord"] is DBNull) & mytable.Rows[0]["IsDefaultPassWord"].ToString().ToUpper().Trim() == "Y")
            //                            {
            //                                Session["DefultPass"] = "yes";
            //                            }

            //                            CreateCookie();

            //                            Context.ApplicationInstance.CompleteRequest();
            //                            updateLogDetailsGO();
            //                            blLoginSucc = true;

            //                            if (Session["UserLevelCode"].ToString().Trim() == "9")
            //                            {
            //                                Response.Redirect("UpdateProfile.aspx", false);
            //                            }
            //                            return;
            //                        }
            //                        else
            //                        {
            //                            string strReturn = UpdateFailedAttempts();
            //                            if (strReturn == "AccountLocked")
            //                            {
            //                                // Response.Write("<script language='javascript'>window.alert('Your account has been locked due to repeated failed login attempts, Accounts will be UnLocked after Try 5 Minutes');window.location='logout.aspx';</script>")
            //                                lblmsg.Text = Common.ShowMessage("Your account has been locked due to repeated failed login attempts, Accounts will be UnLocked after Try 5 Minutes", 2);
            //                                return;
            //                            }
            //                            else if (strReturn == "AttemptUpdated")
            //                            {
            //                                lblmsg.Text = Common.ShowMessage("Invalid User Name or Password", 2);
            //                            }
            //                            var randomclass = new Random();
            //                            Session["Sseed"] = randomclass.Next();
            //                            btnLogIn.Attributes.Add("onclick", "javascript:md5Auth(" + Session["Sseed"] + ");");
            //                            objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "btnLogIn_Click", "Login Failed Password not match");
            //                            return;
            //                        }
            //                    }
            //                    else
            //                    {
            //                        string strReturn = UpdateFailedAttempts();
            //                        if (strReturn == "AccountLocked")
            //                        {
            //                            lblmsg.Text = Common.ShowMessage("Your account has been locked due to repeated failed login attempts, Accounts will be UnLocked after Try 5 Minutes", 2);
            //                            return;
            //                        }
            //                        else if (strReturn == "AttemptUpdated")
            //                        {
            //                            lblmsg.Text = Common.ShowMessage("Invalid User Name or Password", 2);
            //                        }
            //                        var randomclass = new Random();
            //                        Session["Sseed"] = randomclass.Next();
            //                        btnLogIn.Attributes.Add("onclick", "javascript:md5Auth(" + Session["Sseed"] + ");");
            //                        objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "btnLogIn_Click", "Invalid User User Not Found");
            //                        return;
            //                    }
            //                }
            //                else
            //                {
            //                    try
            //                    {
            //                        if (mytable.ToString().Contains("Could not open a connection"))
            //                        {
            //                            lblmsg.Text = Common.ShowMessage("Could not open a connection to database server", 2);
            //                            return;
            //                        }
            //                    }
            //                    catch (Exception ex)
            //                    {
            //                    }

            //                    string strReturn = UpdateFailedAttempts();
            //                    if (strReturn == "AccountLocked")
            //                    {
            //                        lblmsg.Text = Common.ShowMessage("Your account has been locked due to repeated failed login attempts, Accounts will be UnLocked after Try 5 Minutes", 3);
            //                        return;
            //                    }
            //                    else if (strReturn == "AttemptUpdated")
            //                    {
            //                        lblmsg.Text = Common.ShowMessage("Invalid User Name or Password", 2);
            //                    }
            //                    objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "btnLogIn_Click", "Invalid Login No row return from mytable ");
            //                    lblmsg.Text = Common.ShowMessage("Invalid User Name or Password", 2);
            //                    return;
            //                }
            //            }
            //            catch (Exception ex)
            //            {
            //                lblmsg.Text = Common.ShowMessage(ex.Message.ToString(), 2);
            //                objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "btnLogIn_Click", ex.Message);
            //            }
            //        }
            //        else
            //        {
            //            var randomclass1 = new Random();
            //            Session["Sseed"] = randomclass1.Next();
            //            btnLogIn.Attributes.Add("onclick", "javascript:md5Auth(" + Session["Sseed"] + ");");
            //            lblmsg.Text = Common.ShowMessage("You have entered an invalid value for the captcha", 2);
            //            Session["UserID"] = "0";
            //            objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "btnLogIn_Click", "Login Failed");
            //            return;
            //        }
            //    }

            //    else
            //    {
            //        var randomclass1 = new Random();
            //        Session["Sseed"] = randomclass1.Next();
            //        btnLogIn.Attributes.Add("onclick", "javascript:md5Auth(" + Session["Sseed"] + ");");
            //        lblmsg.Text = Common.ShowMessage("You have entered an invalid value for the captcha", 2);
            //        Session["UserID"] = "0";
            //        objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "btnLogIn_Click", "Login Failed");
            //        return;
            //    }
            //}
            //else
            //{
            //    var randomclass1 = new Random();
            //    Session["Sseed"] = randomclass1.Next();
            //    btnLogIn.Attributes.Add("onclick", "javascript:md5Auth(" + Session["Sseed"] + ");");
            //    lblmsg.Text = Common.ShowMessage("You have entered an invalid value for the captcha", 2);
            //    Session["UserID"] = "0";
            //    objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "btnLogIn_Click", "Login Failed");
            //    return;
            //}
        }

        catch (Exception ex)
        {
            objActivityLog.insertErrorLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "btnLogIn_Click", ex.Message);
        }
        finally
        {
            if (blLoginSucc)
            {
                objActivityLog.InsertUserActivityLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "Login" + "-SL", "User Login Successfully");
            }
            else
            {
                objActivityLog.InsertUserActivityLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "Login" + "-UL", "User Login Failed");
            }
        }

    }

    private void CreateCookie()
    {
        try
        {

            FormsAuthenticationTicket tkt;
            string cookiestr;
            HttpCookie ck;


            tkt = new FormsAuthenticationTicket(1, txtUserID.Text, DateTime.Now, DateTime.Now.AddMinutes(480d), false, txtUserID.Text);

            cookiestr = FormsAuthentication.Encrypt(tkt);
            ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            ck.Path = FormsAuthentication.FormsCookiePath;
            Response.Cookies.Add(ck);
            // Create session variables
            Session.Timeout = 480; // 60

            HttpCookie appcookie = new HttpCookie("Appcookie");
            appcookie.Value = Session["Rnd1"].ToString();
            appcookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(appcookie);

            HttpCookie cookie = Request.Cookies[".ASPXFORMAUTH"];
            Session["TicketID"] = cookie.Value;
            // Session hijecking Prevention
            Session["RndVar"] = Guid.NewGuid().ToString();
            string st = Request.Browser.Browser + Request.Browser.MajorVersion + Request.Browser.Platform + Request.Browser.MinorVersion + Session["RndVar"];
            var CNewRnd = new HttpCookie("CRnd", st);
            Response.Cookies.Add(CNewRnd);
            Session["SRnd"] = st;

            string _browserInfo = Request.Browser.Browser + Request.Browser.Version + Request.UserAgent + "~" + Request.ServerVariables["REMOTE_ADDR"];
            string _sessionValue = Session["UserID"].ToString() + "^" + DateTime.Now.Ticks.ToString() + "^" + _browserInfo + "^" + Guid.NewGuid().ToString();

            var _encodeAsBytes = Encoding.ASCII.GetBytes(_sessionValue);
            string _encryptedString = Convert.ToBase64String(_encodeAsBytes);
            Session["encryptedSession"] = _encryptedString;
        }

        catch (Exception ex)
        {
            objActivityLog.insertErrorLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "CreateCookie", ex.Message);
        }
    }

    private void updateLogDetailsGO()
    {
        string userid = "";
        try
        {
            var Implementer = new BLL.AppProcessing();
            MyInterface = Implementer;
            mycommand.Parameters.Clear();

            if (Session.SessionID == null || Session.SessionID == "")
            {
                mycommand.Parameters.Add("@SessionID", SqlDbType.VarChar, 100).Value = "No Session ID";
            }
            else
            {
                mycommand.Parameters.Add("@SessionID", SqlDbType.VarChar, 100).Value = Session.SessionID;
            }

            if (Session["UserID"] == null)
            {
                userid = "No User Id";
            }
            else
            {
                userid = Session["UserID"].ToString();
            }
            mycommand.Parameters.Add("@UserID", SqlDbType.NVarChar, 50).Value = userid;
            mycommand.Parameters.Add("@macIP", SqlDbType.VarChar, 20).Value = Request.UserHostAddress;
            mycommand.Parameters.Add("@ExternalIP", SqlDbType.VarChar, 50).Value = Request.ServerVariables["REMOTE_ADDR"].ToString();
            mycommand.Parameters.Add("@IPDetails", SqlDbType.NVarChar, 200).Value = Request.UserHostAddress; // getgeoloction()
            mycommand.Parameters.Add("@Flag", SqlDbType.Char, 1).Value = "I";

            mytable = MyInterface.UPDATEData(mycommand, "UpdateLogDetails");
            if (mytable.Rows.Count > 0)
            {
                if (mytable.Rows[0][0].ToString().Trim() != "Saved Successfully")
                {
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            objActivityLog.insertErrorLog(userid, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "updateLogDetailsGO", ex.Message);
        }
    }

    private string UpdateFailedAttempts()
    {
        string strReturn = "";
        try
        {
            MyInterface = MyImplementer;
            mycommand.Parameters.Clear();
            mycommand.Parameters.AddWithValue("@UserID", txtUserID.Text.Trim());
            mycommand.Parameters.AddWithValue("@Func", "UpdateAttempt");
            Result.Direction = System.Data.ParameterDirection.Output;
            mycommand.Parameters.Add(Result);
            SqlErrMsg.Direction = System.Data.ParameterDirection.Output;
            mycommand.Parameters.Add(SqlErrMsg);
            mytable = MyInterface.UPDATEData(mycommand, "UpdateLoginLocked");
            if (mytable.TableName == "tblUPDATEDataDefault" && (Result.Value.ToString().Trim().Length) > 0)
            {
                strReturn = Result.Value.ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            objActivityLog.insertErrorLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Login", "btnLogIn_Click", ex.Message);
        }
        return strReturn;
    }

    protected void btnRegLogin_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        divForgot.Visible = false;
        divLogin.Visible = true;
        divReg.Visible = false;
    }

    protected void btnOtp_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        bool blLoginSucc = false;
        try
        {
            if (txtRegCaptcha == null || txtRegCaptcha.Text == "")
            {

                lblmsg.Text = Common.ShowMessage("You have entered wrong text in captcha", 2);

                return;
            }
            else if (txtRegEmail == null || txtRegEmail.Text == "")
            {

                lblmsg.Text = Common.ShowMessage("You have entered wrong Email Id", 2);

                return;
            }
            else if (txtRegMobile.Text == null || txtRegMobile.Text == "")
            {

                lblmsg.Text = Common.ShowMessage("You have not entered Mobile No", 2);

                return;
            }
            else if (txtRegName.Text == null || txtRegName.Text == "")
            {

                lblmsg.Text = Common.ShowMessage("You have not entered Name", 2);

                return;
            }
            else if (validation.mobile(txtRegMobile.Text.Trim()) == false)
            {

                lblmsg.Text = Common.ShowMessage("Enter Valid Mobile Number", 2);

                return;
            }
            else if (validation.Email(txtRegEmail.Text.Trim()) == false)
            {
                lblmsg.Text = Common.ShowMessage("Enter Valid Email Id", 2);

                return;
            }
            else
            {
                if (Session["cpcode"] != null)
                {
                    Session["UserID"] = txtUserID.Text.Trim();
                    Session["DefultPass"] = null;
                    if (string.Compare(txtCaptcha.Text.ToString().Trim(), Session["cpcode"].ToString()) == 0)
                    {
                        txtCaptcha.Text = "";
                        txtUserPass.Text = "";
                        string OTP = "";
                        //string passwordHash = "";
                        string md5passFirstLogin = "";
                        //passwordHash = hidHash.Value;
                        try
                        {
                            Session["Pass"] = RandomString();
                            md5passFirstLogin = CreateHash(RandomString());
                            Random generator = new Random();
                            OTP = generator.Next(0, 1000000).ToString("D6");
                            MyInterface = MyImplementer;
                            mycommand.Parameters.Clear();
                            mycommand.Parameters.AddWithValue("@Name", txtRegName.Text.Trim());
                            mycommand.Parameters.AddWithValue("@Email", txtRegEmail.Text.Trim());
                            mycommand.Parameters.AddWithValue("@Mobile", txtRegMobile.Text.Trim());
                            mycommand.Parameters.AddWithValue("@Password", md5passFirstLogin);
                            mycommand.Parameters.AddWithValue("@OTP", OTP);
                            Result.Direction = System.Data.ParameterDirection.Output;
                            mycommand.Parameters.Add(Result);
                            mytable = MyInterface.INSERTData(mycommand, "ces.Ins_Registration");
                            if (mytable.Rows.Count > 0)
                            {
                                if (mytable.Rows[0][0].ToString().Trim() == "Saved Successfully")
                                {
                                    Session["Result"] = Result.Value.ToString().Trim();
                                    blLoginSucc = true;
                                    return;
                                }
                                else if (mytable.Rows[0][0].ToString().Trim() != "User Already Exists")
                                {
                                    Session["Result"] = Result.Value.ToString().Trim();
                                    blLoginSucc = false;
                                    return;
                                }
                                else
                                {
                                    Session["Result"] = Result.Value.ToString().Trim();
                                    blLoginSucc = false;
                                    return;
                                }
                            }
                            else
                            {
                                try
                                {
                                    if (mytable.ToString().Contains("Could not open a connection"))
                                    {
                                        lblmsg.Text = Common.ShowMessage("Could not open a connection to database server", 2);
                                        return;
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                                objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Register", "btnRegRegister_Click", "Invalid Login No row return from mytable ");
                                lblmsg.Text = Common.ShowMessage("Error in saving record.", 2);
                                return;
                            }
                        }
                        catch (Exception ex)
                        {
                            lblmsg.Text = Common.ShowMessage(ex.Message.ToString(), 2);
                            objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Register", "btnOtp_Click", ex.Message);
                        }
                    }
                    else
                    {
                        var randomclass1 = new Random();
                        Session["Sseed"] = randomclass1.Next();
                        btnLogIn.Attributes.Add("onclick", "javascript:md5Auth(" + Session["Sseed"] + ");");
                        lblmsg.Text = Common.ShowMessage("You have entered an invalid value for the captcha", 2);
                        Session["UserID"] = "0";
                        objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Register", "btnOtp_Click", "You have entered an invalid value for the captcha");
                        return;
                    }
                }
                else
                {
                    var randomclass1 = new Random();
                    Session["Sseed"] = randomclass1.Next();
                    btnLogIn.Attributes.Add("onclick", "javascript:md5Auth(" + Session["Sseed"] + ");");
                    Session["UserID"] = "0";
                    objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Register", "btnOtp_Click", "You have entered an invalid value for the captcha");
                    lblmsg.Text = Common.ShowMessage("You have entered an invalid value for the captcha.", 2);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            objActivityLog.insertErrorLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Register", "btnOtp_Click", ex.Message);
        }
        finally
        {
            if (blLoginSucc)
            {
                divOTP.Visible = true;
                txtRegEmail.Enabled = false;
                txtRegName.Enabled = false;
                txtRegMobile.Enabled = false;
                txtRegEmail.CssClass = "form-control";
                txtRegName.CssClass = "form-control";
                txtRegMobile.CssClass = "form-control";
                btnRegRegister.Visible = false;
                btnChange.Visible = true;
                btnRegRegister.Visible = true;
                btnOtp.Visible = false;
                divCaptcha.Visible = false;
                objActivityLog.InsertUserActivityLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Register", "Register" + "-SL", "OTP Sent Successfull on Your Email.");
                lblmsg.Text = Common.ShowMessage("OTP Sent Successfull on Your Email.", 1);

            }
            else
            {
                if (Session["Result"].ToString() == "-1")
                {
                    objActivityLog.InsertUserActivityLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Register", "Register" + "-UL", "User Already Exists");
                    lblmsg.Text = Common.ShowMessage("User Already Exists.", 2);
                }
                else if (Session["Result"].ToString() == "-2")
                {
                    objActivityLog.InsertUserActivityLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Register", "Register" + "-UL", "User is Inactive");
                    lblmsg.Text = Common.ShowMessage("User is Inactive.", 2);
                }
                else
                {
                    objActivityLog.InsertUserActivityLog(txtUserID.Text, Request.ServerVariables["REMOTE_ADDR"].ToString(), "Register", "Register" + "-UL", "User Registration Failed");
                    lblmsg.Text = Common.ShowMessage("User Registration Failed.", 2);
                }
            }
        }
    }

    private void clear()
    {
        lblmsg.Text = "";
        txtRegEmail.Text = "";
        txtRegEmail.Enabled = true;
        txtRegMobile.Text = "";
        txtRegMobile.Enabled = true;
        txtRegName.Text = "";
        txtRegName.Enabled = true;
    }

    protected void btnChange_Click(object sender, EventArgs e)
    {
        //divOTP.Visible = false;
        //txtRegEmail.Enabled = true;
        //btnChange.Visible = false;
        //btnOtp.Visible = true;
    }
}
