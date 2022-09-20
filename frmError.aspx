<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="frmError.aspx.cs" Inherits="frmError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="main-container" style="margin: 0 auto;">
        <h1 style="color: red;">Sorry! An error occured, please try again.</h1>

        <a href="login.aspx" class="btn btn-large btn-success"><span class="glyphicon glyphicon-step-backward" aria-hidden="true"></span>&nbsp;Go To Home Page</a>
    </div>
</asp:Content>

