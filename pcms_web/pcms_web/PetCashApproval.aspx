<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PetCashApproval.aspx.cs" Inherits="pcms_web.PetCashApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h2>Approve Petty Cash Claims</h2>
    </div>
    <br />
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="ClaimId" 
        DataSourceID="SqlDataSource1" 
        onselectedindexchanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="ClaimId" HeaderText="ClaimId" InsertVisible="False" 
                ReadOnly="True" SortExpression="ClaimId" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="TransactionDate" HeaderText="TransactionDate" 
                SortExpression="TransactionDate" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" 
                SortExpression="Amount" />
            <asp:BoundField DataField="Description" HeaderText="Description" 
                SortExpression="Description" />
            <asp:BoundField DataField="UserId" HeaderText="UserId" 
                SortExpression="UserId" />
            <asp:BoundField DataField="Status" HeaderText="Status" 
                SortExpression="Status" />
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT * FROM [petty_cash_requests]"></asp:SqlDataSource>

        <br />
        <div>
            <span style="font-weight:bold;padding-right:10px">Selected Requst ID : </span>
            <asp:Label ID="requestId" runat="server"></asp:Label>
            <asp:Button ID="approve" runat="server" onclick="Button1_Click" 
                Text="Approve" />
            <asp:Button ID="reject" runat="server" Text="Reject" />
            <asp:Button ID="pend" runat="server" Text="Pending" />
        </div>
        
        <br />


</asp:Content>
