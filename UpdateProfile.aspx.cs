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

public partial class UpdateProfile : SetCulture
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
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetNoStore();
        Response.Cache.SetExpires(DateTime.Now - new TimeSpan(1, 0, 0));
        Response.Cache.SetLastModified(DateTime.Now);
        Response.Cache.SetAllowResponseInBrowserHistory(false);

        if (!IsPostBack)
        {
            var randomclass = new Random();
            Session["Sseed"] = randomclass.Next();

            //ltrName.Text = Session["Name"].ToString();
            //ltrMobile.Text = Session["Mobile"].ToString();

            Response.Cookies["AuthCookieNew"].Expires = DateTime.Now.AddDays(-30);
            txtFName.Text = Session["Name"].ToString();
            txtMobile.Text = Session["Mobile"].ToString();
            txteMail.Text = Session["UserID"].ToString();
            txteMail.Enabled = false;
            txteMail.CssClass = "form-control";

            getMasters();
            GetStateAccToCountry();
            GetDistrictAccToState();
            GetProfile();
            //ddlCountry_SelectedIndexChanged(ddlCountry, e);
        }
    }

    private void getMasters()
    {
        lblmsg.Text = "";
        bool blLoginSucc = false;
        try
        {
            MyInterface = MyImplementer;
            mycommand.Parameters.Clear();
            Result.Direction = System.Data.ParameterDirection.Output;
            mycommand.Parameters.Add(Result);
            SqlErrMsg.Direction = System.Data.ParameterDirection.Output;
            mycommand.Parameters.Add(SqlErrMsg);
            mytable = MyInterface.SELECTData(mycommand, "[ces].[Get_mCountryMaster]");
            if (mytable.Rows.Count > 0)
            {
                ddlCountry.DataSource = mytable;
                ddlCountry.DataTextField = "CountryDesc";
                ddlCountry.DataValueField = "CountryCode";
                ddlCountry.DataBind();
                ddlCountry.Items.Insert(0, "--Select Country--");
                blLoginSucc = true;

            }
        }
        catch (Exception ex)
        {
            objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Profile", "PageLoad", ex.Message);
            lblmsg.Text = Common.ShowMessage(ex.Message.ToString(), 2);
        }
        finally
        {
            if (blLoginSucc)
            {
                objActivityLog.InsertUserActivityLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Profile", "PageLoad" + "-SL", "Data Fetched Succesfully");
            }
            else
            {
                objActivityLog.InsertUserActivityLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Profile", "PageLoad" + "-UL", "MasterError");
            }
        }
    }

    private void clear()
    {
        lblmsg.Text = "";
        txtDOB.Text = "";
        txteMail.Text = "";
        txtRelative.Text = "";
        txtFName.Text = "";
        txtLName.Text = "";
        txtMName.Text = "";
        txtMobile.Text = "";
        txtPincode.Text = "";
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("Home.aspx");
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetStateAccToCountry();
    }

    private void GetStateAccToCountry()
    {
        MyInterface = MyImplementer;
        mycommand.Parameters.Clear();
        mycommand.Parameters.AddWithValue("@CountryCode", ddlCountry.SelectedValue);
        mytable = MyInterface.SELECTData(mycommand, "[ces].[GetStateAccToCountry]");
        if (mytable.Rows.Count > 0)
        {
            ddlSTate.DataSource = mytable;
            ddlSTate.DataTextField = "StateName";
            ddlSTate.DataValueField = "StateCode";
            ddlSTate.DataBind();
            ddlSTate.Items.Insert(0, "--Select State--");
        }
    }
    protected void ddlSTate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSTate.SelectedIndex > 0)
        {
            GetDistrictAccToState();
        }
    }

    private void GetDistrictAccToState()
    {
        MyInterface = MyImplementer;
        mycommand.Parameters.Clear();
        mycommand.Parameters.AddWithValue("@StateCode", ddlSTate.SelectedValue);
        mytable = MyInterface.SELECTData(mycommand, "[ces].[GetDistrictAccToState]");
        if (mytable.Rows.Count > 0)
        {
            ddlDistrict.DataSource = mytable;
            ddlDistrict.DataTextField = "DistrictName";
            ddlDistrict.DataValueField = "DistrictCode";
            ddlDistrict.DataBind();
            ddlDistrict.Items.Insert(0, "--Select District--");
        }
    }

    private void GetProfile()
    {
        MyInterface = MyImplementer;
        mycommand.Parameters.Clear();
        mycommand.Parameters.AddWithValue("@EmailId", Session["UserID"].ToString());
        mytable = MyInterface.SELECTData(mycommand, "[ces].[GetUserProfileCitizen]");
        if (mytable.Rows.Count > 0)
        {
            txteMail.Text = mytable.Rows[0]["EmailID"].ToString();
            txtFName.Text = mytable.Rows[0]["FirstName"].ToString();
            txtMName.Text = mytable.Rows[0]["MiddleName"].ToString();
            txtLName.Text = mytable.Rows[0]["LastName"].ToString();
            txtRelative.Text = mytable.Rows[0]["RelativeName"].ToString();
            txtDOB.Text = mytable.Rows[0]["DOB"].ToString(); //Convert.ToString(list[0].DOB.ToShortDateString());
            txtHNo.Text = mytable.Rows[0]["HouseNo"].ToString();
            txtStreet.Text = mytable.Rows[0]["VillStreet"].ToString();
            txtLandmark.Text = mytable.Rows[0]["Landmark"].ToString();
            txtPincode.Text = mytable.Rows[0]["PIN"].ToString();
            txtMobile.Text = mytable.Rows[0]["Mobileno"].ToString();
            txtLandLine.Text = mytable.Rows[0]["Landlineno"].ToString();
            rdbGender.SelectedValue = mytable.Rows[0]["Gender"].ToString();
            ddlRelation.SelectedValue = mytable.Rows[0]["Gender"].ToString();
            ddlCountry.SelectedValue = mytable.Rows[0]["CountryCode"].ToString();

            ddlSTate.SelectedValue = mytable.Rows[0]["State"].ToString();

            ddlDistrict.SelectedValue = mytable.Rows[0]["District"].ToString();
        }
    }

    protected void btnUpdateProfile_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        bool blLoginSucc = false;
        try
        {
            if (txtFName.Text == "")
            {

                lblmsg.Text = Common.ShowMessage("You have not entered First Name", 2);

                return;
            }
            else if (txtLName.Text == "")
            {

                lblmsg.Text = Common.ShowMessage("You have not entered Last Name", 2);

                return;
            }
            else if (txtMobile.Text == "")
            {

                lblmsg.Text = Common.ShowMessage("You have not entered Mobile No", 2);

                return;
            }
            else if (txtRelative.Text == "")
            {

                lblmsg.Text = Common.ShowMessage("You have not entered Relative Name", 2);

                return;
            }
            else if (txtDOB.Text == "")
            {

                lblmsg.Text = Common.ShowMessage("You have not entered DOB", 2);

                return;
            }
            else if (ddlCountry.SelectedIndex == 0)
            {

                lblmsg.Text = Common.ShowMessage("You have not selected Country", 2);

                return;
            }
            else if (ddlSTate.SelectedIndex == 0)
            {

                lblmsg.Text = Common.ShowMessage("You have not selected State", 2);

                return;
            }
            else if (ddlDistrict.SelectedIndex == 0)
            {

                lblmsg.Text = Common.ShowMessage("You have not selected District", 2);

                return;
            }
            else if (txtPincode.Text == "")
            {

                lblmsg.Text = Common.ShowMessage("You have not entered Pincode", 2);

                return;
            }
            else if (txtHNo.Text == "")
            {

                lblmsg.Text = Common.ShowMessage("You have not entered House No", 2);

                return;
            }
            else if (validation.mobile(txtMobile.Text.Trim()) == false)
            {
                lblmsg.Text = Common.ShowMessage("Enter Valid Mobile Number", 2);

                return;
            }
            else if (validation.Email(txteMail.Text.Trim()) == false)
            {

                lblmsg.Text = Common.ShowMessage("Enter Valid Email Id", 2);

                return;
            }
            else
            {
                try
                {
                    MyInterface = MyImplementer;
                    mycommand.Parameters.Clear();
                    mycommand.Parameters.AddWithValue("@EmailID", txteMail.Text.Trim());
                    mycommand.Parameters.AddWithValue("@FirstName", txtFName.Text.Trim());
                    mycommand.Parameters.AddWithValue("@MiddleName", txtMName.Text.Trim());
                    mycommand.Parameters.AddWithValue("@LastName", txtLName.Text.Trim());
                    mycommand.Parameters.AddWithValue("@Relation", ddlRelation.SelectedValue);
                    mycommand.Parameters.AddWithValue("@RelativeName", txtRelative.Text.Trim());
                    mycommand.Parameters.AddWithValue("@DOB", txtDOB.Text.Trim());
                    mycommand.Parameters.AddWithValue("@HouseNo", txtHNo.Text.Trim());
                    mycommand.Parameters.AddWithValue("@VillStreet", txtStreet.Text.Trim());
                    mycommand.Parameters.AddWithValue("@District", ddlDistrict.SelectedValue);
                    mycommand.Parameters.AddWithValue("@Tehsil", "");
                    mycommand.Parameters.AddWithValue("@State", ddlSTate.SelectedValue);
                    mycommand.Parameters.AddWithValue("@Landmark", txtLandmark.Text.Trim());
                    mycommand.Parameters.AddWithValue("@PIN", txtPincode.Text.Trim());
                    mycommand.Parameters.AddWithValue("@Gender", rdbGender.SelectedValue);
                    mycommand.Parameters.AddWithValue("@Mobileno", txtMobile.Text.Trim());
                    mycommand.Parameters.AddWithValue("@Landlineno", txtLandLine.Text.Trim());
                    mycommand.Parameters.AddWithValue("@CountryCode", ddlCountry.SelectedValue);
                    mycommand.Parameters.AddWithValue("@Type", "S");
                    Result.Direction = System.Data.ParameterDirection.Output;
                    mycommand.Parameters.Add(Result);
                    mytable = MyInterface.INSERTData(mycommand, "ces.Ins_mUserProfileCitizen");
                    if (mytable.Rows.Count > 0)
                    {
                        if (mytable.Rows[0][0].ToString().Trim() == "Saved Successfully")
                        {
                            Session["Code"] = Result.Value.ToString().Trim();
                            blLoginSucc = true;
                            return;
                        }
                        else if (mytable.Rows[0][0].ToString().Trim() == "Updated Successfully")
                        {
                            Session["Code"] = Result.Value.ToString().Trim();
                            blLoginSucc = true;
                            return;
                        }
                        else
                        {
                            Session["Code"] = Result.Value.ToString().Trim();
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
                        objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Update Profile", "btnUpdateProfile_Click", "Invalid Login No row return from mytable");
                        lblmsg.Text = Common.ShowMessage("Error in saving record.", 2);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    lblmsg.Text = Common.ShowMessage(ex.Message.ToString(), 2);
                    objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Update Profile", "btnUpdateProfile_Click", ex.Message);
                }
            }
        }
        catch (Exception ex)
        {
            objActivityLog.insertErrorLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Update Profile", "btnUpdateProfile_Click", ex.Message);
        }
        finally
        {
            if (blLoginSucc)
            {
                objActivityLog.InsertUserActivityLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Update Profile", "Update Profile" + "-SL", "Profile Updated Succesfully.");
                lblmsg.Text = Common.ShowMessage("Profile Updated Succesfully.", 1);
            }
            else
            {
                objActivityLog.InsertUserActivityLog(Session["UserID"].ToString(), Request.ServerVariables["REMOTE_ADDR"].ToString(), "Update Profile", "Update Profile" + "-UL", "Some Error Occured Try again.");
                lblmsg.Text = Common.ShowMessage("Some Error Occured Try again.", 2);

            }
        }
    }
}