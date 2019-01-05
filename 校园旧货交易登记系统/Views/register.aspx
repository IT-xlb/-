<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="校园旧货交易登记系统.Views.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center">

        <h1>&nbsp;&nbsp;&nbsp; 用户注册</h1> 

        <p>
            <asp:Label ID="lbusername" runat="server" Width="80px">用户名：</asp:Label>
            <asp:TextBox ID="tbusername" runat="server" style="margin-left: 0px"></asp:TextBox> 
         </p>

        <p>
            <asp:Label ID="Label1" runat="server" Width="80px">密码：</asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" TextMode="Password" ></asp:TextBox> 
        </p>

        <p>
            <asp:Label ID="Label2" runat="server"  Text="联系方式：" Width="80px"></asp:Label>
            <asp:TextBox ID="TextBox2" runat="server" ></asp:TextBox>
        </p>

        <p>

            <asp:Button ID="btsubmit" runat="server" Text="提交" OnClick="btsubmit_Click" />

        </p>
    </div>
</asp:Content>
