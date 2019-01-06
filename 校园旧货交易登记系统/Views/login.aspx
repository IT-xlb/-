<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="校园旧货交易登记系统.Views.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div align="center"> 
        <h1>&nbsp;&nbsp; 用户登录</h1> 
        
            <p> 
                <asp:Label ID="lbusername" runat="server">用户名：</asp:Label> 
                <asp:TextBox ID="tbusername" runat="server" style="border-radius:10px;outline:none"></asp:TextBox> 
            </p> 
  
            <p>
                <asp:Label ID="lbpsw" runat="server">密 码：</asp:Label>&nbsp;  
                <asp:TextBox ID="tbpsw" runat="server" TextMode="Password" style="border-radius:10px;outline:none"></asp:TextBox> 
            </p> 
            <p>
                <asp:Button ID="btnLogin" runat="server" Text="登录" OnClick="btnLogin_Click" Width="44px" style="margin-right: 30px ;border-radius:6px"/>
                            
                            
                <asp:Button ID="btnRegister" runat="server" Text="注册" Height="25px" Width="44px" OnClick="btnRegister_Click" style="margin-left:20px;border-radius:6px" />           
                  
            </p> 
            <p>
            
                <asp:HyperLink ID="HyperLink1" runat="server"  NavigateUrl="~/Views/register.aspx">忘记密码？</asp:HyperLink>
            </p>
        
     </div> 
</asp:Content>
