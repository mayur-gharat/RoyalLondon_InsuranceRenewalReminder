<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InsuranceRenewalReminder._Default" %>

<meta http-equiv="x-ua-compatible" content="ie=edge">

<!--[if IE 9]>
  <link href="Scripts/bootstrap-ie9.min.css" rel="stylesheet">
<![endif]-->
<!--[if lte IE 8]>
  <link href="Scripts/bootstrap-ie8.min.css" rel="stylesheet">
  <script src="Scripts/html5shiv@3.7.3"></script>
<![endif]-->

<form runat="server">

    <%--Including necessary CSS--%>
    <link href="Scripts/bootstrap.min.css" rel="stylesheet" />
    <link href="Scripts/InsuranceRenewal.css" rel="stylesheet" />

    <%--Header--%>
    <div class="jumbotron">
        <h1 class="hd1">ROYAL LONDON INSURANCE RENEWAL UTILITY</h1>
        <p class="p1">Creating Invitation letters & reminders for Insurance renewal for customers of Royal London Insurance</p>
    </div>

    <%--Main section--%>
    <table class="table">
        <tr>
            <td>
                <asp:fileupload id="flupload" runat="server" accept="csv" class="btn btn-info" ></asp:fileupload>    
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:button id="btnInput" runat="server" text="Create Reminders" OnClick="btnInput_Click" onClientClick="return ValidateFileUpload();" class="btn btn-info" />
            </td>
        </tr>
        <tr>
            <td >
                <asp:label id="lblResult" runat="server" text="."></asp:label>
            </td>
        </tr>
    </table>
    
    
   <%--Including necessary Scripts--%>
    <script src="Scripts/jquery-3.2.0.js"></script>
    <script src="Scripts/bootstrap.min.js" ></script>
    <script src="Scripts/InsuranceRenewal.js"></script>

</form>



