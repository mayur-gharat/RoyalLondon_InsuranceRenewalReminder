<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="InsuranceRenewalReminder._Default" %>

<form runat="server">

    <div class="jumbotron">
        <h1>ROYAL LONDON INSURANCE RENEWAL UTILITY</h1>
        <p class="lead">This utility will help you create templates for emails to be sent to clients for their insurance renewal reminder.</p>
    </div>

    <asp:label id="lblResult" runat="server" text="."></asp:label>
    <br /><br />
    
    <asp:fileupload id="flupload" runat="server" accept="csv"></asp:fileupload>
    <%--<asp:CustomValidator ID="CustomValidator1" runat="server" ClientValidationFunction="ValidateFileUpload" ErrorMessage="Invalid file type. Only .CSV are allowed." ControlToValidate="flupload" ValidationGroup="update">&nbsp;</asp:CustomValidator>--%>
    <br /><br />

    <asp:button id="btnInput" runat="server" text="Create Reminders" OnClick="btnInput_Click" onClientClick="return ValidateFileUpload();" />
    
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



