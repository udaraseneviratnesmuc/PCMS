<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PetCashRequest.aspx.cs" Inherits="pcms_web.PetCashRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h2>Make a petty cash request</h2>
    </div>
    <br />
    <div>
        <span style="font-weight:bold">Request Title : 
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </span>
    </div>
    <br />
    <div>
        <span style="font-weight:bold">Current Available Balance :
        <asp:Label ID="PACurrentBal" runat="server" Text="Label"></asp:Label>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>">
        </asp:SqlDataSource>
        </span> 
    </div>

</asp:Content>
