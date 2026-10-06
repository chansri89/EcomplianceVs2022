<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="ResetPassword.aspx.cs" Inherits="ForgotPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
<asp:Label ID="Label1" runat="server" Align= "center" Text=" "  width="785px"  style="text-align: center" Height="22px"></asp:Label>
<asp:Label ID="lblCountrymas" runat="server" Align= "center" 
        Text="Reset Password"   Font ="Verdana" width="446px"  Font-Size ="12pt" 
        Font-Bold="True" Font-Names="Verdana" 
        style="text-align: center" Height="21px"></asp:Label>

            <asp:panel ID="pnlPassPolicy" runat="server" Height="232px" Width="522px" 
           Visible = "false">  <asp:Label ID="lblPasswordPolicy" runat="server" Text="TAFE Password Policy" ForeColor="DarkBlue"  Font-Names="arial" Font-Size="Small"></asp:Label>
        <div style="overflow:auto; height:200px; width:501px">
    <asp:GridView ID="GrdPassPolicy" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="446px" Font-Names="arial" AutoGenerateColumns="False" 
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

    <asp:Panel ID="pnlForgotPwd" runat="server" Height="595px" Width="831px">                   
    <table>
    <tr>
    <td class="style30">  &nbsp; &nbsp; &nbsp;  </td>
    <td style="width: 72px">
    <asp:Label ID="lblUserName" runat="server" Text="User Name" Font-Size="X-Small" 
            Font-Names="Verdana" style="font-weight: 700" ></asp:Label>
    </td>
    <td>
   <asp:DropDownList ID="ddlEmployeeName" runat="server" Font-Names="Verdana"  Visible="true"
                        Font-Size="X-Small" Height="16px" Width="196px">
                    </asp:DropDownList>
            
    </td>

        <td style="width: 72px">
    <asp:Label ID="lblPassword" runat="server" Text="Password" Font-Size="X-Small" 
            Font-Names="Verdana" style="font-weight: 700" ></asp:Label>
    </td>
    <td>
    <asp:TextBox ID="txtPassword" runat="server" Placeholder="Password" Width="153px" TextMode="Password"  
            Font-Size="X-Small"></asp:TextBox>
    </td>

    <td>
        <asp:Button ID="btnGo" runat="server" onclick="btnGo_Click" 
            Text="Go" Font-Size="X-Small" Font-Names="Verdana" />
           
        </td>
    </tr>
   
    </table>   
    <table style="width: 824px; height: 76px">
<tr>
<td class="style23" style="width: 13px">  &nbsp; &nbsp; &nbsp;  </td>
<td style="width: 193px">
<asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="Small" Text="Select the User and assign a password as per Policy listed above and Click GO button" 
Font-Bold="true" ForeColor ="DarkOrchid"   style="font-weight: bold"></asp:Label>
</td>
<td style="width: 372px">
<asp:Label ID="lblMessage" runat="server" Font-Names="arial" Font-Size="X-Small" Text="" Font-Bold="true" ForeColor = "Blue"
        style="font-weight: bold"></asp:Label>
</td>


</tr>
</table>                            
    </asp:Panel>                                          
 </asp:Content>






