<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" EnableEventValidation="false" %>
<%@ Register Src="~/ElectricityBoard/CustomUserControl/MyMessageBox.ascx" TagName="MyMessageBox" TagPrefix="uc1" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title>e-Services Chandigarh | Homepage</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" href="favicon.ico" type="image/x-icon">
    <link rel="icon" href="favicon.ico" type="image/x-icon">
    <link rel="stylesheet" href="vendor/bootstrap/css/bootstrap.min.css" type="text/css" />
    <!-- Custom styles for this template -->
    <link rel="stylesheet" href="css/font.css" type="text/css" />
    <link rel="stylesheet" href="css/base-responsive.css" type="text/css" />
    <link rel="stylesheet" href="css/animate.min.css" type="text/css" />
    <link rel="stylesheet" href="css/slicknav.min.css" type="text/css" />
    <link rel="stylesheet" href="css/all.css" type="text/css" />
    <link rel="stylesheet" href="css/base.css" type="text/css" />

    <script src="js/popper.min.js"></script>
    <script src="Scripts/Login.js"></script>
    <script src="Scripts/sha.js"></script>
    <script type="text/javascript">
        javascript: window.history.forward(1);
        function clearids() {

            document.getElementById("txtUserPass").value = "";
        }
    </script>
</head>


<body class="gradient">
    <form id="form1" runat="server">
        <!-- Accessibility -->
        <div class="row bg-black-opacity border-bottom-1 border-warning">
            <div class="container d-flex align-items-center clearfix" id="b-accessibility">
                <div class="b-ministryname">
                    <div class="text-right d-inline-block font-weight-bold b-acc-goi pr-sm-2">
                        <a href="https://www.india.gov.in/" target="_blank" class="text-warning"><span>Government of India</span></a>
                        <a href="https://chandigarh.gov.in/" target="_blank" class="text-warning"><span>Chandigarh Admin</span></a>
                    </div>
                </div>
                <div class="ml-auto d-flex b-acc-icons">
                    <div class="align-self-center">

                        <ul class="my-0 d-inline-flex align-items-center" style="list-style: none;">
                            <li>
                                <a class="text-warning" href="javascript:void(0)"><i class="fas fa-volume-up"></i>Skip To Main Content </a>
                            </li>
                            <li>
                                <a class="text-warning" href="javascript:void(0)"><i class="fas fa-assistive-listening-systems"></i>Screen Reader </a>
                            </li>
                            <li>
                                <a class="text-warning" href="javascript:void(0)">A<sup>+</sup></a>
                            </li>
                            <li>
                                <a class="text-warning" href="javascript:void(0)">A</a>
                            </li>
                            <li>
                                <a class="text-warning" href="javascript:void(0)">A<sup>-</sup></a>
                            </li>
                            <li>
                                <asp:Label ID="lblSelectLanguage" runat="server" class="text-warning" Text="Select Language"></asp:Label>
                            </li>
                            <li>
                                <asp:DropDownList runat="server" ID="ddllang" AutoPostBack="True" CssClass="mx-1" Style="border-radius: 0px; border: none; padding: 4px;">
                                    <asp:ListItem Text="English" Value="en" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Hindi" Value="hi-IN"> </asp:ListItem>
                                </asp:DropDownList>
                            </li>
                            <li>
                                <input type="text" placeholder="Search..." class="mx-1" />
                            </li>
                        </ul>

                    </div>

                </div>
            </div>
        </div>

        <!-- Header -->
        <div class="row mx-0">

            <div class="container d-flex align-items-center clearfix" id="b-header">

                <div class="col-md-8 px-0 d-inline-flex align-items-center">
                    <div class="">
                        <img src="../images/e-logo.png" width="80" class="align-self-center b-emblem-image" title="e-Service Logo" alt="e-Service Logo">
                    </div>

                    <div class="">
                        <h2 class="align-self-center pl-3 b-appname text-light my-0">Services</h2>
                        <h4 class="align-self-center pl-3 text-light"><span class="b-appfullname">(Chandigarh Administration)</span></h4>
                    </div>
                </div>

                <div class="col-md-4 text-right d-inline-flex justify-content-end">
                    <img src="../images/handlogo.png" width="140" />
                </div>

            </div>
        </div>

        <!-- Global Navigation -->
        <div class="bg-black-opacity">
            <div class="container">
                <nav class="navbar navbar-expand-sm navbar-dark px-0 py-0">
                    <div class="d-flex w-100 b-nav-mobile">
                        <button class="navbar-toggler align-self-center b-btn-toggler" type="button" data-toggle="collapse" data-target="#collapsibleNavbar" onclick="myFunction(this)">
                            <span style="display: none;">Menu</span>
                            <div>
                                <div class="bar1"></div>
                                <div class="bar2"></div>
                                <div class="bar3"></div>
                            </div>
                        </button>
                    </div>

                    <div class="collapse navbar-collapse" id="collapsibleNavbar">
                        <ul class="navbar-nav main-menu d-flex py-0 my-0">
                            <li class="nav-item"><a href="index.html" class="nav-link bg-warning">Home</a> </li>
                            <li class="nav-item"><a href="javascript:void(0)" class="nav-link">Institutional Data</a></li>
                            <li class="nav-item"><a href="javascript:void(0)" class="nav-link">Contact Us</a></li>
                        </ul>
                    </div>

                </nav>
            </div>
        </div>
        <div class="b-bg-image py-5">
            <div class="container">
                <div class="row d-flex align-items-center">
                    <div class="col-lg-8 b-heading-sec border-warning">
                        <div class="align-self-center b-head-in">
                            <div id="carouselExampleControls" class="carousel slide" data-ride="carousel" data-interval="2000">
                                <div class="carousel-inner">
                                    <div class="carousel-item active">
                                        <img class="d-block w-100" src="../images/carousel/photo-1495107334309-fcf20504a5ab.jpg" alt="Slide 1" />
                                    </div>
                                    <div class="carousel-item">
                                        <img class="d-block w-100" src="../images/carousel/photo-1605007493699-af65834f8a00.jpg" alt="Slide 2" />
                                    </div>
                                    <div class="carousel-item">
                                        <img class="d-block w-100" src="../images/carousel/photo-1552083375-1447ce886485.jpg" alt="Slide 3" />
                                    </div>
                                </div>
                            </div>
                            <div class="slider-service-panel">
                                <div class="sl-1">
                                    <h1 class="text-center service-icon">
                                        <img src="../images/handlogo1.png" width="100%">
                                    </h1>
                                    <h1 class="text-center text-light">e-Services Chandigarh</h1>
                                    <hr class="hr-warning" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-lg-4 b-login-sec border-warning px-4 py-4">
                        <asp:Label ID="lblmsg" runat="server"></asp:Label>
                        <uc1:MyMessageBox ID="MyMessageBox1" Width="270" runat="server" ShowCloseButton="true" />
                        <div id="divLogin" runat="server">
                            <div class="d-block border-bottom-0">
                                <h2 class="b-login-head">Log In</h2>
                            </div>
                            <form action="dashboard.html" autocomplete="off" method="POST" class="px-5">
                                <div class="form-group">
                                    <div class="form-check">
                                        <%--<label class="form-check-label text-white" for="radio1">--%>
                                        <asp:RadioButtonList CssClass="form-check-input text-light" ID="rdbUserType" runat="server" RepeatDirection="Horizontal" Width="400px">
                                            <asp:ListItem Selected="True" Text="&nbsp;&nbsp;Citizen" Value="C"></asp:ListItem>
                                            <asp:ListItem Text="&nbsp;Employee" Value="E"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <%-- </label>--%>
                                    </div>
                                </div>
                                <br />
                                <div class="form-group ">
                                    <%--<label for="login-email-1" class="text-white">User ID:</label>--%>
                                    <asp:TextBox class="form-control" ID="txtUserID" runat="server" placeholder="Enter User ID" ToolTip="Enter Email"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <%--<label for="login-pwd-1" class="text-white">Password:</label>--%>
                                    <asp:TextBox class="form-control" ID="txtUserPass" runat="server" placeholder="Enter Password" ToolTip="Enter Password" TextMode="Password"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="login-Captcha-1" class="text-white">Captcha:</label>
                                    <asp:Image ID="ImgCaptcha" runat="server" ImageUrl="~/captcha.ashx" />
                                </div>
                                <div class="form-group">
                                    <%--<label for="login-Captcha-1" class="text-white">Enter Captcha:</label>--%>
                                    <asp:TextBox class="form-control" ID="txtCaptcha" runat="server" placeholder="Enter Captcha" ToolTip="Enter Captcha"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCaptcha" InitialValue="" runat="server" ErrorMessage="Enter Catcha" ValidationGroup="Submit" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </div>
                                <%--  <div class="form-group custom-control custom-checkbox">
                                <asp:CheckBox runat="server" ID="chkRem" CssClass="custom-control-input" /> 
                                <label class="custom-control-label light-bg-label" for="login-rem-1">Remember me</label>
                            </div>--%>
                                <!-- <p class="text-right b-notreg ">Don't have an account? <a href="">Sign Up</a></p> -->
                                <div class="text-center py-4">
                                    <input id="hidHash" runat="server" name="hidHash" type="hidden" />

                                    <asp:Button ID="btnLogIn" runat="server" CssClass="btn btn-primary b-btn btn-xs" Text="Log In" OnClick="btnLogIn_Click" />
                                    <asp:Button ID="btnForgetPwd" runat="server" CssClass="btn btn-warning b-btn btn-xs" Text="Forget Password" OnClick="btnForgetPwd_Click" />
                                    <asp:Button ID="btnReg" runat="server" CssClass="btn btn-danger b-btn btn-xs" Text="Register" OnClick="btnReg_Click" />
                                </div>
                            </form>
                        </div>
                        <div id="divReg" runat="server" visible="false">
                            <div class="d-block border-bottom-0">
                                <h2 class="b-login-head">Register</h2>
                            </div>
                            <form action="dashboard.html" autocomplete="off" method="POST" class="px-5">
                                <div class="form-group mb-0 mt-3">
                                    <%--<label for="login-email-1" class="text-white">Name: </label>--%>
                                    <asp:TextBox class="form-control" ID="txtRegName" runat="server" MaxLength="60" placeholder="Enter Name" ToolTip="Enter Name"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqFirstName" ForeColor="Red" ErrorMessage="Enter Name"
                                        ControlToValidate="txtRegName" runat="server" ValidationGroup="G" />
                                </div>
                                <div class="form-group  my-0">
                                    <%--<label for="login-email-1" class="text-white">Mobile:</label>--%>
                                    <asp:TextBox class="form-control" ID="txtRegMobile" runat="server" MaxLength="10" TextMode="Phone" placeholder="Enter Mobile" ToolTip="Enter Mobile"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ErrorMessage="Enter Mobile"
                                        ControlToValidate="txtRegName" runat="server" ValidationGroup="G" />
                                </div>
                                <div class="form-group">
                                    <%--<label for="login-email-1" class="text-white">Email:</label>--%>
                                    <asp:TextBox class="form-control" ID="txtRegEmail" runat="server" MaxLength="50" placeholder="Enter Email" TextMode="Email" ToolTip="Enter Email"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="ReqEmail" Display="None" ErrorMessage="Enter Email ID"
                                        ControlToValidate="txtRegEmail" runat="server" ValidationGroup="G" />
                                    <asp:RegularExpressionValidator ID="EmailIDValidation" runat="server" ControlToValidate="txtRegEmail"
                                        ErrorMessage="Invalid Email Address" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ValidationGroup="G" Display="Dynamic" SetFocusOnError="True" ForeColor="Red"></asp:RegularExpressionValidator>
                                </div>
                                <div runat="server" id="divCaptcha">
                                    <div class="form-group">
                                        <label for="login-Captcha-1" class="text-white">Captcha:</label>
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/captcha.ashx" />
                                    </div>
                                    <div class="form-group">
                                        <%--<label for="login-Captcha-1" class="text-white">Enter Captcha:</label>--%>
                                        <asp:TextBox class="form-control" ID="txtRegCaptcha" runat="server" MaxLength="6" placeholder="Enter Captcha" ToolTip="Enter Captcha"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCaptcha" InitialValue="" runat="server" ErrorMessage="Enter Catcha" ValidationGroup="Submit" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <%--  <div class="form-group custom-control custom-checkbox">
                                <asp:CheckBox runat="server" ID="chkRem" CssClass="custom-control-input" /> 
                                <label class="custom-control-label light-bg-label" for="login-rem-1">Remember me</label>
                            </div>--%>
                                <!-- <p class="text-right b-notreg ">Don't have an account? <a href="">Sign Up</a></p> -->
                                <div class="form-group " id="divOTP" runat="server" visible="false">
                                    <%--<label for="login-email-1" class="text-white">Email OTP:</label>--%>
                                    <asp:TextBox class="form-control" ID="txtOTP" TextMode="Password" MaxLength="6" runat="server" placeholder="Enter Email OTP" ToolTip="Enter Email OTP"></asp:TextBox>
                                </div>
                                <div class="text-center">
                                    <asp:Button ID="btnOtp" runat="server" CssClass="btn btn-primary mb-1" Text="Send Email OTP" OnClick="btnOtp_Click" ValidationGroup="G" />
                                    <asp:Button ID="btnChange" runat="server" CssClass="btn btn-primary mb-1" Text="Resend OTP" OnClick="btnChange_Click" Visible="false" />
                                    <asp:Button ID="btnRegRegister" Visible="false" runat="server" CssClass="btn btn-danger mb-1" Text="Validate OTP" OnClick="btnRegRegister_Click" />
                                    <asp:Button ID="btnRegLogin" runat="server" CssClass="btn btn-warning mb-1" Text="Go to login" OnClick="btnRegLogin_Click" />
                                </div>
                            </form>
                        </div>
                        <div id="divForgot" runat="server" visible="false">
                            <div class="d-block border-bottom-0">
                                <h2 class="b-login-head">Forgot Password</h2>
                            </div>
                            <form action="dashboard.html" autocomplete="off" method="POST" class="px-5">
                                <div class="form-group">
                                    <div class="form-check text-light">
                                        <%--<label class="form-check-label text-white" for="radio1">--%>
                                        <asp:RadioButtonList CssClass="form-check-input " ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" Width="400px">
                                            <asp:ListItem Selected="True" Text="&nbsp;&nbsp;User" Value="U"></asp:ListItem>
                                            <asp:ListItem Text="&nbsp;Employee" Value="E"></asp:ListItem>
                                        </asp:RadioButtonList>
                                        <%--</label>--%>
                                    </div>
                                </div>
                                <br />
                                <div class="form-group ">
                                    <%--<label for="login-email-1" class="text-white">Email:</label>--%>
                                    <asp:TextBox class="form-control" ID="txtForEmail" runat="server" placeholder="Enter Email" ToolTip="Enter Email"></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label for="login-Captcha-1" class="text-white">Captcha:</label>
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/captcha.ashx" />
                                </div>
                                <div class="form-group">
                                    <%--<label for="login-Captcha-1" class="text-white">Enter Captcha:</label>--%>
                                    <asp:TextBox class="form-control" ID="txtForCaptcha" runat="server" placeholder="Enter Captcha" ToolTip="Enter Captcha"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtCaptcha" InitialValue="" runat="server" ErrorMessage="Enter Catcha" ValidationGroup="Submit" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                                </div>
                                <%--  <div class="form-group custom-control custom-checkbox">
                                <asp:CheckBox runat="server" ID="chkRem" CssClass="custom-control-input" /> 
                                <label class="custom-control-label light-bg-label" for="login-rem-1">Remember me</label>
                            </div>--%>
                                <!-- <p class="text-right b-notreg ">Don't have an account? <a href="">Sign Up</a></p> -->
                                <div class="text-center py-4">
                                    <asp:Button ID="btnForPass" runat="server" CssClass="btn btn-primary b-btn" Text="Reset Password" />
                                    <asp:Button ID="btnForLogin" runat="server" CssClass="btn btn-danger b-btn" Text="Go to login" OnClick="btnForLogin_Click" />
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row text-center py-3 b-footer-credit bg-black-opacity text-light">
            <div class="container copyright">
                <p class="m-0">This website belongs to <a class="text-warning font-weight-bold" target="blank" href="https://chandigarh.gov.in/">Chandigarh Administration</a>, <a class="text-warning font-weight-bold" target="blank" href="https://www.india.gov.in/">Govt. of India.</a></p>
            </div>
        </div>


        <!-- Login Modal -->
        <div class="modal fade" id="login-modal">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">

                    <!-- Modal Header -->
                    <div class="modal-header text-center d-block p-5 border-bottom-0">
                        <h2 class="modal-title">Log In</h2>
                        <button type="button" class="close position-absolute" style="right: 15px; top: 8px;" data-dismiss="modal">&times;</button>
                    </div>

                    <!-- Modal body -->
                    <div class="modal-body">
                        <form action="dashboard.html" autocomplete="off" method="POST">
                            <div class="form-group">
                                <label for="login-email">Email:</label>
                                <input type="email" class="form-control" id="login-email" placeholder="Enter email" name="login-email">
                            </div>
                            <div class="form-group">
                                <label for="login-pwd">Password:</label>
                                <input type="password" class="form-control" id="login-pwd" placeholder="Enter password" name="login-pwd">
                            </div>
                            <div class="form-group form-check">
                                <label class="form-check-label" for="login-rem">
                                    <input class="form-check-input" type="checkbox" id="login-rem" name="remember">
                                    Remember me
                                </label>
                            </div>
                            <p class="text-right b-notreg">Don't have an account? <a href="" data-toggle="modal" data-target="#signup-modal" data-dismiss="modal">Sign Up</a></p>
                            <div class="text-center py-4">
                                <button type="submit" class="btn btn-primary b-btn">Log In</button>
                            </div>

                        </form>
                    </div>
                </div>
            </div>
        </div>
        <!-- Signup Modal -->
        <div class="modal fade" id="signup-modal">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">

                    <!-- Modal Header -->
                    <div class="modal-header text-center d-block p-5 border-bottom-0">
                        <h2 class="modal-title">Sign Up</h2>
                        <button type="button" class="close position-absolute" style="right: 15px; top: 8px;" data-dismiss="modal">&times;</button>
                    </div>

                    <!-- Modal body -->
                    <div class="modal-body">
                        <form action="" autocomplete="off">
                            <div class="form-group">
                                <label for="signup-name">Name:</label>
                                <input type="text" class="form-control" id="signup-name" placeholder="Enter name" name="signup-name">
                            </div>
                            <div class="form-group">
                                <label for="signup-email">Email:</label>
                                <input type="email" class="form-control" id="signup-email" placeholder="Enter email" name="signup-email">
                            </div>
                            <div class="form-group">
                                <label for="signup-mobile">Mobile no.:</label>
                                <input type="number" class="form-control" id="signup-mobile" placeholder="Enter mobile no." name="signup-mobile">
                            </div>
                            <div class="form-group">
                                <label for="signup-pwd">Password:</label>
                                <input type="password" class="form-control" id="signup-pwd" placeholder="Enter password" name="signup-pwd">
                            </div>

                            <p class="text-right b-already-reg">Already Registered? <a href="" data-toggle="modal" data-target="#login-modal" data-dismiss="modal">Log In</a></p>
                            <div class="text-center py-4">
                                <button type="submit" class="btn btn-primary b-btn">Sign Up</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>

        <!-- Feedback Modal -->
        <div class="modal fade" id="feedback-modal">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">

                    <!-- Modal Header -->
                    <div class="modal-header text-center d-block p-5 border-bottom-0">
                        <h2 class="modal-title">Feedback</h2>
                        <button type="button" class="close position-absolute" style="right: 15px; top: 8px;" data-dismiss="modal">&times;</button>
                    </div>

                    <!-- Modal body -->
                    <div class="modal-body">
                        <form action="" autocomplete="off">
                            <div class="form-group">
                                <label for="fdbk-name">Name:</label>
                                <input type="text" class="form-control" id="fdbk-name" placeholder="Enter name" name="fdbk-name">
                            </div>
                            <div class="form-group">
                                <label for="fdbk-email">Email:</label>
                                <input type="email" class="form-control" id="fdbk-email" placeholder="Enter email" name="fdbk-email">
                            </div>
                            <div class="form-group">
                                <label for="fdbk-subject">Subject:</label>
                                <select class="form-control" id="fdbk-subject" name="fdbk-subject">
                                    <option>Application issue</option>
                                    <option>Design issue</option>
                                    <option>Any other</option>
                                </select>
                            </div>
                            <div class="form-group">
                                <label for="fdbk-comment">Comment:</label>
                                <textarea class="form-control" id="fdbk-comment" placeholder="Enter feedback" name="fdbk-comment" rows="5" style="resize: none;"></textarea>
                            </div>

                            <div class="text-center py-4">
                                <button type="submit" class="btn btn-primary b-btn">Submit</button>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>


        <!-- Bootstrap core JavaScript -->
        <script src="js/jquery.min.js"></script>
        <script src="vendor/bootstrap/js/bootstrap.min.js"></script>
        <script src="js/jquery.slicknav.min.js"></script>

        <script type="text/javascript">

            $(document).ready(function () {

                $("#menu-toggle").click(function (e) {
                    e.preventDefault();
                    $("#wrapper").toggleClass("toggled");
                });

                $(".b-navdropdown-click").click(function () {
                    if ($(".b-navdropdown").hasClass("hide")) {
                        $(".b-navdropdown").addClass("show");
                        $(".b-navdropdown").removeClass("hide");
                        // $(".b-icon-up").show();
                        // $(".b-icon-down").hide();
                    }
                    else if ($(".b-navdropdown").hasClass("show")) {
                        $(".b-navdropdown").addClass("hide");
                        $(".b-navdropdown").removeClass("show");
                        // $(".b-icon-down").show();
                        // $(".b-icon-up").hide();
                    }
                });

                $('.counter-count').each(function () {
                    $(this).prop('Counter', 0).animate({
                        Counter: $(this).text()
                    }, {
                        duration: 5000,
                        easing: 'swing',
                        step: function (now) {
                            $(this).text(Math.ceil(now));
                        }
                    });
                });
            });
        </script>
    </form>
</body>
</html>


























