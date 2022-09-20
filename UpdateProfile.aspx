<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UpdateProfile.aspx.cs" Inherits="UpdateProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="b-bg-image py-5" style="padding-bottom: 200px!important">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 b-login-sec">
                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    <div class="d-block px-5 pt-5 pb-4 border-bottom-0">
                        <h2 class="b-login-head">Profile</h2>
                    </div>
                    <form action="dashboard.html" autocomplete="off" method="POST" class="form-inline">
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-First-1" class="text-white">First Name:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtFName" runat="server" placeholder="Enter First Name" MaxLength="50" ToolTip="Enter First Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-Middle-1" class="text-white">Middle Name:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtMName" runat="server" placeholder="Enter Middle Name" MaxLength="50" ToolTip="Enter Middle Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-Last-1" class="text-white">Last Name:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtLName" runat="server" placeholder="Enter Last Name" MaxLength="50" ToolTip="Enter Last Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-Mobile-1" class="text-white">Mobile No:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtMobile" runat="server" placeholder="Enter Mobile No." TextMode="Phone" MaxLength="10" ToolTip="Enter Mobile No."></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-email-1" class="text-white">Email address:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txteMail" runat="server" MaxLength="50" placeholder="Enter Email" ToolTip="Enter Email"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-Father-1" class="text-white">LandLine No:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtLandLine" runat="server" TextMode="Number" MaxLength="13" placeholder="Enter Landline No" ToolTip="Enter Landline No"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-email-1" class="text-white">Relation:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList runat="server" ID="ddlRelation" CssClass="form-control">
                                                <asp:ListItem Text="Father" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="Mother" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="Brother" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Sister" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Uncle" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="Aunt" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="Other" Value="7"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-email-1" class="text-white">Relative Name:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtRelative" runat="server" placeholder="Enter Relative Name" MaxLength="80" ToolTip="Enter Relative Name"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-DOB-1" class="text-white">DOB:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox CssClass="form-control" ID="txtDOB" runat="server" TextMode="Date" placeholder="Enter DOB" ToolTip="Enter DOB"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-email-1" class="text-white">Gender</label>
                                        </div>
                                        <div class="col-md-8">
                                            <label class="form-check-label text-white margin-radio" for="radio1">
                                                <asp:RadioButtonList CssClass="form-check-input " ID="rdbGender" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True" Text="&nbsp;&nbsp;Male" Value="M"></asp:ListItem>
                                                    <asp:ListItem Text="&nbsp;&nbsp;Female" Value="F"></asp:ListItem>
                                                    <asp:ListItem Text="&nbsp;&nbsp;Trans Gender" Value="T"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-email-1" class="text-white">Country:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-Father-1" class="text-white">State:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList runat="server" ID="ddlSTate" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlSTate_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-email-1" class="text-white">District:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-email-1" class="text-white">Pincode:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtPincode" runat="server" placeholder="Enter Pincode" TextMode="Month" MaxLength="6" ToolTip="Enter Pincode"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-Father-1" class="text-white">House No:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtHNo" runat="server" placeholder="Enter House No" MaxLength="10" ToolTip="Enter House No"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-Father-1" class="text-white">Vill/Street:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtStreet" runat="server" placeholder="Enter Vill/Street" ToolTip="Enter Vill/Street"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-Father-1" class="text-white">Landmark:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtLandmark" runat="server" placeholder="Enter Landmark" ToolTip="Enter Landmark"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <!-- <p class="text-right b-notreg ">Don't have an account? <a href="">Sign Up</a></p> -->

                        <%--<div class="d-block px-5 pt-5 pb-4 border-bottom-0">
                            <h2 class="b-login-head"></h2>
                        </div>

                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-email-1" class="text-white">Password:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtPass" runat="server" placeholder="Enter Password" ToolTip="Enter Password" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-Father-1" class="text-white">Confirm Password:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtConfirm" runat="server" placeholder="Enter Confirm Password" ToolTip="Enter Confirm Password" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-email-1" class="text-white">Captcha:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/captcha.ashx" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label for="login-Father-1" class="text-white">Enter Captcha:</label>
                                        </div>
                                        <div class="col-md-8">
                                            <asp:TextBox class="form-control" ID="txtRegCaptcha" runat="server" placeholder="Enter Captcha" ToolTip="Enter Captcha"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>--%>

                        <div class="form-group">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-8">
                                            <asp:Button ID="btnUpdateProfile" runat="server" CssClass="btn btn-primary b-btn" Text="Update Profile" OnClick="btnUpdateProfile_Click" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-primary b-btn" Text="Cancel" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="row">
                                        <div class="col-md-4">
                                        </div>
                                        <div class="col-md-8">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--<div class="text-center py-4">
                        </div>--%>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

