<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InsuranceRenewalReminder._Default" %>

<form runat="server">

    <link href="InsuranceRenewal.css" rel="stylesheet" />
    
    <div class="jumbotron">
        <h1>ROYAL LONDON INSURANCE RENEWAL UTILITY</h1>
        <p class="lead">This utility will help you create templates for emails to be sent to clients for their insurance renewal reminder.</p>
    </div>

    <table class="table_main">
        <tr>
            <td>
                <asp:fileupload id="flupload" runat="server" accept="csv"></asp:fileupload>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <asp:button id="btnInput" runat="server" text="Create Reminders" OnClick="btnInput_Click" onClientClick="return ValidateFileUpload();" />
            </td>
        </tr>
        <tr>
            <td >
                <asp:label id="lblResult" runat="server" text="."></asp:label>
            </td>
        </tr>
    </table>
    
    
<script language="javascript" type="text/javascript">   

    function ValidateFileUpload() {
        var fuData = document.getElementById('<%= flupload.ClientID %>');
        var FileUploadPath = fuData.value;

        if (FileUploadPath == '') {
            // There is no file selected
            alert("Please select input CSV file");
            return false;
        }
        else {
            var Extension = FileUploadPath.substring(FileUploadPath.lastIndexOf('.') + 1).toLowerCase();

            if (Extension == "csv" || Extension == "CSV" || Extension == "txt") {
                // Valid file type
            }
            else {
                // Not valid file type
                alert("Please select appropreate input file");
                return false;
            }
        }
    }
    
    
</script>

    </form>



