<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PetCashRequest.aspx.cs" Inherits="pcms_web.PetCashRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h2>Make a petty cash Request</h2>
    </div>
    <br />
    <div>
        <span style="font-weight:bold">Request Title : 
        <asp:TextBox ID="title" runat="server"></asp:TextBox>
        </span>
    </div>
    <br />
    <div>
        <span style="font-weight:bold">Transaction Date :   
        <asp:Calendar ID="transactionDate" runat="server"></asp:Calendar>
        </span>
    </div>
    <br />
    <div>
        <span style="font-weight:bold">Description :   <br />
        <asp:TextBox ID="description" runat="server" TextMode="MultiLine" Columns="31"></asp:TextBox>
        </span>
    </div>
    <br />
    <div>
        <span style="font-weight:bold">Claiming amount : Rs
        <asp:TextBox ID="amount" runat="server"></asp:TextBox>
        
        </span> 
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        SelectCommand="SELECT * FROM [petty_cash_requsts]"></asp:SqlDataSource>
    <br />
    <div>
         <asp:Button ID="Button1" runat="server" Text="Claim" onclick="Button1_Click" OnClientClick="Confirm()"/>
    </div>
    
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";

            if (document.getElementById('MainContent_amount').value != "") {
                if (confirm("Do you want to request the claim?")) {
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
