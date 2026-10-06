<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="21px" Width="870px"> </asp:Panel>
    <asp:Label ID="lblheading" runat="server" Align= "center" Text="Change Password" 
        Font ="arial" width="458px" Font-Size ="12pt" Font-Bold="True" Font-Names="arial" 
        style="text-align: center" Height="20px"></asp:Label>

   <div style="overflow:auto; height: 870px; width: 930px;">
   
      <asp:panel ID="pnlPassPolicy" runat="server" Height="230px" Width="518px" 
           Visible = "false">  <asp:Label ID="lblPasswordPolicy" runat="server" Text="TAFE Password Policy" ForeColor="DarkBlue"  Font-Names="arial" Font-Size="Small"></asp:Label>
        <div style="overflow:auto; height:203px; width:501px">
    <asp:GridView ID="GrdPassPolicy" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="463px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />  <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /> <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"      HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:TemplateField HeaderText="Password Policy Description">
            <ItemTemplate>
            <asp:Label ID="lblPasswordPolicy" runat="server" Text='<%# Eval("PolicyDescription") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
            <HeaderStyle Width="150px" />   <ItemStyle Width="150px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Character Length Required">
             <ItemTemplate>
            <asp:Label ID="lblMaxLength" runat="server" Text='<%# Eval("CharacterLength") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
    
                <HeaderStyle Width="70px" />
            </asp:TemplateField>
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>

    <asp:Panel ID="pnlPwd" runat="server" Height="867px" Width="924px">
    
<table style="height: 140px; width: 542px">
<tr>
<td style="width: 119px">  <asp:Label ID="lblOldPassword" runat="server" Font-Names="arial" Font-Size="X-Small" Text="Old Password" Font-Bold="False" 
            style="font-weight: bold"></asp:Label></td>
<td>
   <asp:TextBox id="txtOldPassword" runat="server" Width="119px" Font-Size="X-Small" 
        Font-Names="arial"  MaxLength="20"  TextMode="Password"    TabIndex="1" Height="16px" 
        ToolTip="Enter minimum 6  characters"></asp:TextBox> <%--AutoCompleteType="Disabled"  --%>
    </td>
    <td style="width: 349px">
<asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="Small" Text="Key in Your Old Password" 
Font-Bold="true" ForeColor ="DarkOrchid"   style="font-weight: bold"></asp:Label>
</td>
</tr>
<tr>
<td style="width: 119px">
   <asp:Label ID="lblNewPassword" runat="server" Font-Names="arial" 
        Font-Size="X-Small" Text="New Password" Font-Bold="False" AutoCompleteType="Disabled"
        style="font-weight: bold"></asp:Label></td>
<td>
   <asp:TextBox id="txtNewPassword" runat="server" Width="119px" Font-Size="X-Small" 
        Font-Names="arial" MaxLength="20"    TextMode="Password"     AutoCompleteType="Disabled"
        Height="16px" ToolTip="Enter minimum 6  characters" TabIndex="2"></asp:TextBox>
    </td>   
      <td style="width: 349px">
<asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="Small" Text="Key in a New Password as per above Policy" 
Font-Bold="true" ForeColor ="DarkOrchid"   style="font-weight: bold"></asp:Label>
</td>
<%----%>
</tr>
<tr>
<td style="width: 119px">
    <asp:Label ID="lblConfirmPassword" runat="server" Font-Names="arial" Font-Size="X-Small"
        Text="Confirm Password" Font-Bold="False" style="font-weight: bold"></asp:Label></td>
<td>


<asp:TextBox id="txtConfirmPassword" tabIndex="3" runat="server" Width="119px" TextMode="Password" AutoCompleteType="Disabled"
        Font-Size="X-Small" Font-Names="arial"  MaxLength="20" Height="16px" 
        ToolTip="Enter minimum 6  characters" ></asp:TextBox>  <%-- ontextchanged="txtConfirmPassword_TextChanged"    TextMode="Password" --%>
</td>
<td style="width: 349px">
<asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="Small" Text="Confirm New Password by Entering it Again" 
Font-Bold="true" ForeColor ="DarkOrchid"   style="font-weight: bold"></asp:Label>
</td>
</tr>
<tr>
<td style="width: 119px"></td>
<td>
    <asp:Button id="btnSave" tabIndex="4" onclick="btnSave_Click" runat="server" 
        Width="44px" Font-Size="X-Small" Font-Names="arial" Text="Save" ToolTip="Save" 
        ForeColor="Black"></asp:Button>
</td>
<td style="width: 349px">
<asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="Small" Text="Click Save button" 
Font-Bold="true" ForeColor ="DarkOrchid"   style="font-weight: bold"></asp:Label>
</td>
</tr>
</table>
<table style="width: 538px; height: 76px">
<tr>
<td>
<asp:Label ID="lblMessage" runat="server" Font-Names="arial" Font-Size="Small" Text="" Font-Bold="true" ForeColor = "Red"
        style="font-weight: bold"></asp:Label>
</td>
</tr>
</table>
 </asp:Panel>
 </div>
</asp:Content>

