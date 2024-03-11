<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebForm._Default" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <input style="display: none;" type="text" id="SN" runat="server" />
    <label for="Name">姓名:</label>
    <input type="text" id="Name" runat="server" /><br>
    <label for="Age">年齡:</label>
    <input type="text" id="Age" runat="server" /><br>
    <label for="Birthday">生日:</label>
    <input type="date" id="Birthday" runat="server" /><br>
    <asp:Button ID="AddOrUpdate" runat="server" Text="建立帳號" OnClick="AddOrUpdate_Click"></asp:Button>
    <asp:DataGrid ID="DataGrid" runat="server" AutoGenerateColumns="False" OnItemCommand="DataGrid_ItemCommand" CssClass="account-DataGrid">
        <Columns>
            <asp:BoundColumn DataField="SN" Visible="false" />
            <asp:BoundColumn DataField="Name" HeaderText="姓名" />
            <asp:BoundColumn DataField="Age" HeaderText="年齡" />
            <asp:BoundColumn DataField="Birthday" HeaderText="生日" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:ButtonColumn Text="修改" CommandName="Edit" />
            <asp:ButtonColumn Text="刪除" CommandName="Delete" />
        </Columns>
    </asp:DataGrid>
</asp:Content>
