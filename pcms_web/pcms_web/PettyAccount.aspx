<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PettyAccount.aspx.cs" Inherits="pcms_web.PettyAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h2>Petty Cash Account</h2>
    </div>
    <br />
    <div>
        <span style="font-weight:bold">Last Updated Time : </span>
        <asp:Label ID="PACurrentDay" runat="server" Text="Label"></asp:Label>
        <asp:Label ID="PACurrentTime" runat="server" Text="Label"></asp:Label>
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
    <br />
    <div>
        <span style="font-weight:bold">Credit/Debit from Account : Rs
        <asp:TextBox ID="amount" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
            Text="Creadit" OnClientClick="Confirm()"/>
        <asp:Button ID="Button2" runat="server" Text="Debit" onclick="Button2_Click" OnClientClick="Confirm()"/>
        </span>
    </div>

    <br />

    <div>
        <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" 
            AllowPaging="True" AllowSorting="True" OnPageIndexChanging="GridView1_PageIndexChanging" PageSize="10" >
        </asp:GridView>
    </div>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (document.getElementById('MainContent_amount').value != "") {
                if (confirm("Do you want to update the account?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
            } else {
                alert("Please enter a valid amount value");
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
