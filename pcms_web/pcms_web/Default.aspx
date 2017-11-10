<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="pcms_web._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>Welcome to Company Petty Cash Management system</h2>

        <h3 class="terms-heading">This system is to manage internal petty cash flow. Sharing the access to this 
        system or sharing data from this system with outside parties is strictly 
        prohibited.</h3> 
        <div>
            <h3>Using this system you can</h3> 
            <ul>
                <li>Request for petty cash claims</li>
                <li>View claim status</li>
                <li>File a claiming query</li> 
                <li>Update petty cash account details</li>
                <li>View petty cash transaction history</li> 
            </ul>
            based on your user type.
        </div>
    <p>
        In any case of isse please contact <a href="http://go.microsoft.com/fwlink/?LinkID=152368&amp;clcid=0x409"
            title="MSDN ASP.NET Docs">PCMS Admin</a>.
    </p>
</asp:Content>
