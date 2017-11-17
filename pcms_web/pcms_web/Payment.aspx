<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payment.aspx.cs" Inherits="pcms_web.Payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h2>Petty Cash Payment</h2>
    </div>
    <br />
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        
        
        SelectCommand="SELECT [ClaimId], [Title], [TransactionDate], [Amount], [UserId], [Status] FROM [petty_cash_requests] WHERE ([Status] = @Status)">
        <SelectParameters>
            <asp:Parameter DefaultValue="APPROVED" Name="Status" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataKeyNames="ClaimId" 
        DataSourceID="SqlDataSource1" 
        onselectedindexchanged="GridView1_SelectedIndexChanged1">
        <Columns>
            <asp:CommandField ShowSelectButton="True" SelectText="Pay" />
            <asp:BoundField DataField="ClaimId" HeaderText="ClaimId" InsertVisible="False" 
                ReadOnly="True" SortExpression="ClaimId" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="TransactionDate" HeaderText="TransactionDate" 
                SortExpression="TransactionDate" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" 
                SortExpression="Amount" />
            <asp:BoundField DataField="UserId" HeaderText="UserId" 
                SortExpression="UserId" />
            <asp:BoundField DataField="Status" HeaderText="Status" 
                SortExpression="Status" />
        </Columns>
    </asp:GridView>
    <br />

    <br />
    <div>
        <span style="font-weight:bold">Last Updated Time : </span>

        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>

    </div>
    <br />
    <div>
        <span style="font-weight:bold">Current Available Balance : Rs 

        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

        </span> 
    </div>
    <br />


</asp:Content>
