<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="校园旧货交易登记系统.Views.register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center">
        <br />
        <h1>&nbsp;&nbsp;&nbsp; 用户注册</h1> 

        <p >
            <asp:Label ID="lbusername" runat="server" Width="80px">用户名：</asp:Label>
            <asp:TextBox ID="tbusername" runat="server" style="border-radius:10px;outline:none"></asp:TextBox> 
         </p>

        <p>
            <asp:Label ID="Label1" runat="server" Width="80px">用户密码：</asp:Label>
            <asp:TextBox ID="tbuserpwd" runat="server" TextMode="Password" style="border-radius:10px;outline:none"></asp:TextBox> 
        </p>

        <p>
            <asp:Label ID="Label2" runat="server" Width="80px">联系方式：</asp:Label>
            <asp:TextBox ID="tbusertele" runat="server" style="border-radius:10px;outline:none"></asp:TextBox>
        </p>
        <p>

            <asp:Label ID="Label3" runat="server" Width="80px">用户地址：</asp:Label>
            <asp:TextBox ID="tbuseraddress" runat="server" style="border-radius:10px;outline:none"></asp:TextBox>

        </p>
        <p>


           <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal" style="margin:auto" Width="150px" Font-Size="Medium">
                                <asp:ListItem Selected="True">男</asp:ListItem>
                                <asp:ListItem>女</asp:ListItem>
                            </asp:RadioButtonList>


        </p>
        <p>

            <asp:Button ID="btsubmit" runat="server" Text="提交" OnClick="btsubmit_Click" style="border-radius:6px;"/>

        </p>
        <p>
            
            <asp:HyperLink ID="HyperLink1" runat="server"  NavigateUrl="~/Views/login.aspx">已有账号？点这里！</asp:HyperLink>
        </p>
    </div>
    <style>
        .name{
            color:aqua;
        }
    </style>
</asp:Content>
