<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="Panel1" runat="server" Height="16px" Width="870px"> </asp:Panel>
    <asp:Panel ID="pnlPwd" runat="server" Height="877px" Width="934px">
<table style="height: 0px; width: 333px">
<tr>
<td style="width: 119px">
        <asp:Label ID="lblOldPassword" runat="server" Font-Names="arial" 
            Font-Size="X-Small" Text="Old Password" Font-Bold="False" 
            style="font-weight: bold"></asp:Label></td>
<td>
   <asp:TextBox id="txtOldPassword" runat="server" Width="119px" Font-Size="X-Small" 
        Font-Names="arial" TextMode="Password" MaxLength="20" 
        AutoCompleteType="Disabled" TabIndex="1" Height="16px" 
        ToolTip="Enter minimum 6  characters"></asp:TextBox>
    </td>
</tr>
<tr>
<td style="width: 119px">
   <asp:Label ID="lblNewPassword" runat="server" Font-Names="arial" 
        Font-Size="X-Small" Text="New Password" Font-Bold="False" 
        style="font-weight: bold"></asp:Label></td>
<td>
   <asp:TextBox id="txtNewPassword" runat="server" Width="119px" Font-Size="X-Small" 
        Font-Names="arial" TextMode="Password" MaxLength="20"        
        Height="16px" ToolTip="Enter minimum 6  characters" TabIndex="2"></asp:TextBox>
    </td>
</tr>
<tr>
<td style="width: 119px">
    <asp:Label ID="lblConfirmPassword" runat="server" Font-Names="arial" Font-Size="X-Small"
        Text="Confirm Password" Font-Bold="False" style="font-weight: bold"></asp:Label></td>
<td>


<asp:TextBox id="txtConfirmPassword" tabIndex=3 runat="server" Width="119px" 
        Font-Size="X-Small" Font-Names="arial" TextMode="Password" MaxLength="20" 
        AutoCompleteType="Disabled" Height="16px" 
        ToolTip="Enter minimum 6  characters" 
        ontextchanged="txtConfirmPassword_TextChanged"></asp:TextBox>
</td>
</tr>
<tr>
<td style="width: 119px"></td>
<td>
    <asp:Button id="btnSave" tabIndex=4 onclick="btnSave_Click" runat="server" 
        Width="44px" Font-Size="X-Small" Font-Names="arial" Text="Save" ToolTip="Save" 
        ForeColor="Black"></asp:Button>
</td>
</tr>
</table>
 </asp:Panel>
</asp:Content>

