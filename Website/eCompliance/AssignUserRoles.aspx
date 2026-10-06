<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="AssignUserRoles.aspx.cs" Inherits="AssignUserRoles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Label ID="lblassignuserroles" runat="server" Align= "center" Text="Assign User Roles" 
        Font ="Verdana" width="787px" Font-Size ="12pt" Font-Bold="True" Font-Names="Verdana" ForeColor="#3333CC"
        style="text-align: center"></asp:Label>
    <div style="overflow:auto;">
    
        <asp:Panel ID="pnlUsers" runat="server">
    <table>
    <tr>
    <td colspan="20" align="right">
        <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="X-Small" 
            Text="User Name" Width="126px" Font-Bold="True" style="text-align: left"></asp:Label></td>
    <td colspan="20">
        <asp:DropDownList ID="ddlUserName" runat="server" DataTextField="EmployeeName" DataValueField="EmployeeCode"
            Font-Names="arial" Font-Size="X-Small" Width="143px" AutoPostBack="True" 
            OnSelectedIndexChanged="ddlUserName_SelectedIndexChanged" Height="16px" 
            style="text-align: center">
        </asp:DropDownList></td>
    </tr>
    </table>    
        
    </asp:Panel>
        <asp:Panel ID="pnlAssignRoles" runat="server" Visible="true">
      <table> 
  <tr>
  <td colspan ="20" style="text-align: left; width: 163px;"> 
  <asp:Label ID="lblAvailableRoles" runat="server" Font-Bold="False" 
          Font-Names="arial" Font-Size="X-Small" ForeColor="Blue" 
          Text="Available Roles" style="text-align: left"></asp:Label>
  </td>
  <td colspan="20" style="width: 93px">
  
  </td>
  <td colspan="20">
     
      <asp:Label ID="lblAssignedRoles" runat="server" Font-Bold="False" 
          Font-Names="arial" Font-Size="X-Small"
          ForeColor="Blue" Text="Assigned Roles" Width="123px" 
          style="text-align: left"></asp:Label></td>
  </tr>
  <tr>
  <td colspan="20" align="right" 
          style="height: 80px; width: 163px; text-align: left;">
      <asp:ListBox ID="lstbxAvailableRole" runat="server" DataTextField="RoleName" DataValueField="AvRoleId"
          Font-Names="arial" Font-Size="X-Small" Height="114px"
          TabIndex="10" Width="131px">
      </asp:ListBox></td> 
  <td colspan="20" style="height: 80px; width: 93px; text-align: left;" >
      <br />
      <asp:Button ID="btnMove" runat="server" CausesValidation="False" Font-Names="arial"
          Font-Size="X-Small" ForeColor="Blue" OnClick="btnMove_Click" TabIndex="11" Text=">"
          Width="61px" /><br />
      <asp:Button ID="btnMoveFull" runat="server" CausesValidation="False" Font-Names="arial"
          Font-Size="X-Small" ForeColor="Blue" OnClick="btnMoveFull_Click" 
          TabIndex="12" Text=">>"
          Width="61px" Visible="False" /><br />
      <asp:Button ID="btnRemove" runat="server" CausesValidation="False" Font-Names="arial"
          Font-Size="X-Small" ForeColor="Blue" OnClick="btnRemove_Click" 
          TabIndex="13" Text="<"
          Width="61px" /><br />
      <asp:Button ID="btnRemoveFull" runat="server" CausesValidation="False" Font-Names="arial"
          Font-Size="X-Small" ForeColor="Blue" OnClick="btnRemoveFull_Click" TabIndex="14"
          Text="<<" Width="61px" Visible="False" /><br />
      <br />
      </td>
  <td colspan="20" align="left" style="width: 93px; height: 80px">
      <asp:ListBox ID="lstbxAssignedRole" runat="server" DataTextField="RoleName" DataValueField="AsgRoleId"
          Font-Names="arial" Font-Size="X-Small" Height="114px"
          TabIndex="15" Width="131px"></asp:ListBox></td>
          <td colspan="20"></td>
               <td >
          <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="To Assign Roles            --> Click on Roles in Available Roles box. Then Click > button." Width="507px" Font-Bold="True" Height="24px"></asp:Label>
            <%--<asp:Label ID="Lablel3" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="To Assign All Roles        --> Click >> button." Width="507px" Font-Bold="True" Height="24px"></asp:Label>--%>

            <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="To Deselect Asssigned Roles --> Click on Roles in Asssigned Roles box. Then Click < button." 
                  Width="538px" Font-Bold="True" Height="24px"></asp:Label>
          <%--  <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="To Deselect All Roles       --> Click << button." Width="507px" Font-Bold="True" Height="24px"></asp:Label>--%>

          </td>
  </tr> 
  <tr>
  <td colspan="20" style="width: 163px"></td>
  <td colspan ="20" align="center" style="text-align: left; width: 93px">
      
      <asp:Button ID="btnSave" runat="server" Font-Names="arial" 
          Font-Size="X-Small" ForeColor="Blue"
          Text="Save" Width="61px" OnClick="btnSave_Click" />
  </td>
  </tr>
   </table>
      </asp:Panel>
      
    <asp:Panel ID="pnlGetIds" runat="server" Visible="False" Width="46px" style="z-index: 103; left: 11px; position: absolute; top: 307px">
        <asp:TextBox ID="txtUserId" runat="server" Height="25px" Visible="False" Width="21px"></asp:TextBox></asp:Panel>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />

    </div>
</asp:Content>

