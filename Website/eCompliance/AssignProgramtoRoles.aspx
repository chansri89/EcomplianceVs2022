<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="AssignProgramtoRoles.aspx.cs" Inherits="AssignProgramtoRoles" %>
<script runat="server">

    void ShowAllCreate_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox Create = (CheckBox)sender;
        if (Create.Checked)
        {
            ShowCreateRows(true);
        }
        else
        {
            ShowCreateRows(false);
        }
        
    }
    void ShowCreateRows(bool show)
    {
       // GrdAssignProgam.Visible = false;
        foreach (GridViewRow row in GrdAssignProgam.Rows)
        {
            CheckBox chkCreate = (CheckBox)row.FindControl("chkCreate");
            chkCreate.Checked = show;
        }

  }
    void ShowAllAccess_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox Access = (CheckBox)sender;
        if (Access.Checked)
        {
            ShowAccessRows(true);
        }
        else
        {
            ShowAccessRows(false);
        }

    }
    void ShowAccessRows(bool show)
    {
        // GrdAssignProgam.Visible = false;
        foreach (GridViewRow row in GrdAssignProgam.Rows)
        {
            CheckBox chkAccess = (CheckBox)row.FindControl("chkAccess");
            chkAccess.Checked = show;
        }

    }
    void ShowAllEdit_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox Edit = (CheckBox)sender;
        if (Edit.Checked)
        {
            ShowEditRows(true);
        }
        else
        {
            ShowEditRows(false);
        }

    }
    void ShowEditRows(bool show)
    {
        // GrdAssignProgam.Visible = false;
        foreach (GridViewRow row in GrdAssignProgam.Rows)
        {
            CheckBox chkEdit = (CheckBox)row.FindControl("chkEdit");
            chkEdit.Checked = show;
        }

    }
    void ShowAllDelete_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox Delete = (CheckBox)sender;
        if (Delete.Checked)
        {
            ShowDeleteRows(true);
        }
        else
        {
            ShowDeleteRows(false);
        }

    }
    void ShowDeleteRows(bool show)
    {
        // GrdAssignProgam.Visible = false;
        foreach (GridViewRow row in GrdAssignProgam.Rows)
        {
            CheckBox chkDelete = (CheckBox)row.FindControl("chkDelete");
            chkDelete.Checked = show;
        }

    }

    void GrdAssignProgam_Load(object sender, EventArgs e)
    {
        if (rdbtnRole.SelectedIndex == 0)
        {
            
        }
        if (rdbtnRole.SelectedIndex == 1)
        {
            GridView gvr = (GridView)sender;
        }
    }
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<asp:Panel ID="Panel2" runat="server" Height="20px" Width="870px"> </asp:Panel>--%>
<asp:Label ID="lblStatMas" runat="server" Align= "center" Text="Assign Role" 
        Font ="arial" width="650px" Font-Size ="12pt" Font-Bold="True" Font-Names="arial" 
        style="text-align: center" Height="25px"></asp:Label>
    <div style="overflow:auto;">
    <asp:Panel ID="pnlRoleDetails" runat="server" Height="40px">
        <table style="height: 30px">
            <tr>
                <td colspan="20" align="right">
                        <asp:RadioButtonList ID="rdbtnRole" runat="server" RepeatDirection="Horizontal"
                            Width="201px" Font-Names="arial" Font-Size="X-Small" 
                            OnSelectedIndexChanged="rdbtnRole_SelectedIndexChanged" AutoPostBack="True" 
                            Font-Bold="False" Height="19px" style="text-align: left">
                            <asp:ListItem Selected="True" Value="1" Text="New Role"/>
                            <asp:ListItem  Value="2" Text="Update Role"/>
                        </asp:RadioButtonList>
                </td>
               <%-- <td colspan="20">&nbsp;</td>
           </tr>
           <tr>--%>
                <td id="tdRoleName" colspan="20" align ="right" 
                    style="height: 41px; text-align: left;" runat="server" visible="true">
                    <asp:Label ID="lblRoleName" runat="server" Text="Role Name" Width="76px" 
                    Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td id="tdRoleFields" colspan="20" style="height: 41px" runat="server" visible="true">
                    <asp:TextBox ID="txtNewRoleName" runat="server" Font-Names="arial" 
                       Font-Size="X-Small" Width="273px" Visible="true" MaxLength="20"></asp:TextBox>
                    <asp:DropDownList ID="ddlRoleName" runat="server" Width="279px" 
                        Font-Names="arial" Font-Size="X-Small" Visible="False" AutoPostBack="True" 
                        DataTextField="RoleName" DataValueField="RoleId" 
                        OnSelectedIndexChanged="ddlRoleName_SelectedIndexChanged" Height="16px">
                    </asp:DropDownList>
                </td>
           </tr>
       </table>
    </asp:Panel>
    
    <table><tr>
    <td style="height: 458px">
    <asp:panel ID="Pnlgv" runat="server" Width="673px" Height="416px">
        <div style="overflow:auto; height:389px; width:536px">
    <asp:GridView ID="GrdAssignProgam" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="487px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="126px" GridLines="Vertical" 
             onrowcancelingedit="GrdAssignProgam_RowCancelingEdit" 
                onrowdeleting="GrdAssignProgam_RowDeleting" 
                onrowediting="GrdAssignProgam_RowEditing" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                onrowupdating="GrdAssignProgam_RowUpdating"
                DataKeyNames="ProgramId" onrowcommand="GrdAssignProgam_RowCommand" 
                onrowdatabound="GrdAssignProgam_RowDataBound">
        <EditRowStyle Font-Size="X-Small" />
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
             <asp:TemplateField HeaderText="ProgramId" Visible="false">
            <ItemTemplate>
            <asp:Label ID="lblProgramId" runat="server" Text='<%# Eval("ProgramId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <HeaderStyle Width="100px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ProgramName">
            <ItemTemplate>
            <asp:Label ID="lblProgramName" runat="server" Text='<%# Eval("ProgramName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
            <EditItemTemplate>
            <asp:Label ID="lblProgramName" runat="server" Text='<%# Bind("ProgramName") %>'  Font-Names="arial" Font-Size="X-Small" Width="250px"></asp:Label></ItemTemplate>
            </EditItemTemplate>
             <HeaderStyle Width="250px" />
                <ItemStyle Width="250px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Access" >
             <HeaderTemplate>
            <asp:checkbox id="ShowAllAccess" text="Access" checked="false" autopostback="true" runat="server" OnCheckedChanged="ShowAllAccess_CheckedChanged" />
            <asp:Label ID="lblAccess" Text="Access" runat="server" Visible="false"></asp:Label>
            </HeaderTemplate>
            <ItemTemplate>
            <asp:CheckBox ID="chkAccess" runat="server" Checked='<%# Eval("CanAccess") %>'  Font-Names="arial" Font-Size="X-Small"></asp:CheckBox></ItemTemplate>
             <EditItemTemplate>
            <asp:CheckBox ID="chkEditAccess" runat="server" Checked='<%# Bind("CanAccess") %>'  Font-Names="arial" Font-Size="X-Small"></asp:CheckBox></ItemTemplate>
            </EditItemTemplate>
             <HeaderStyle Width="5px" HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Create">
            <HeaderTemplate>
            <asp:checkbox id="ShowAllCreate" text="Create" checked="false" autopostback="true" runat="server" OnCheckedChanged="ShowAllCreate_CheckedChanged" />
             <asp:Label ID="lblCreate" Text="Create" runat="server" Visible="false"></asp:Label>
            </HeaderTemplate>
             <ItemTemplate>
              <asp:CheckBox ID="chkCreate" runat="server" Checked='<%# Eval("CanCreate") %>'  Font-Names="arial" Font-Size="X-Small"></asp:CheckBox></ItemTemplate>
              <EditItemTemplate>
            <asp:CheckBox ID="chkEditCreate" runat="server" Checked='<%# Bind("CanCreate") %>'  Font-Names="arial" Font-Size="X-Small"></asp:CheckBox></ItemTemplate>
            </EditItemTemplate>
             <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Middle"  />
             <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Edit">
             <HeaderTemplate>
            <asp:checkbox id="ShowAllEdit" text="Edit" checked="false" autopostback="true" runat="server" OnCheckedChanged="ShowAllEdit_CheckedChanged" />
             <asp:Label ID="lblEdit" Text="Edit" runat="server" Visible="false"></asp:Label>
            </HeaderTemplate>
             <ItemTemplate>
              <asp:CheckBox ID="chkEdit" runat="server" Checked='<%# Eval("CanEdit") %>'  Font-Names="arial" Font-Size="X-Small"></asp:CheckBox></ItemTemplate>
              <EditItemTemplate>
            <asp:CheckBox ID="chkModEdit" runat="server" Checked='<%# Bind("CanEdit") %>'  Font-Names="arial" Font-Size="X-Small"></asp:CheckBox></ItemTemplate>
            </EditItemTemplate>
                <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Middle"  />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Delete">
             <HeaderTemplate>
            <asp:checkbox id="ShowAllDelete" text="Delete" checked="false" autopostback="true" runat="server" OnCheckedChanged="ShowAllDelete_CheckedChanged" />
            <asp:Label ID="lblDelete" Text="Delete" runat="server" Visible="false"></asp:Label>
            </HeaderTemplate>
             <ItemTemplate>
              <asp:CheckBox ID="chkDelete" runat="server" Checked='<%# Eval("CanDelete") %>'  Font-Names="arial" Font-Size="X-Small"></asp:CheckBox></ItemTemplate>
              <EditItemTemplate>
            <asp:CheckBox ID="chkEditDelete" runat="server" Checked='<%# Bind("CanDelete") %>'  Font-Names="arial" Font-Size="X-Small"></asp:CheckBox></ItemTemplate>
            </EditItemTemplate>
                <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Middle"  />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
          <asp:TemplateField HeaderText="View" Visible="false">
             <ItemTemplate>
               <asp:CheckBox ID="chkPrint" runat="server" Checked='<%# Eval("CanPrint") %>'  Font-Names="arial" Font-Size="X-Small"></asp:CheckBox></ItemTemplate>
               <EditItemTemplate>
            <asp:CheckBox ID="chkEditPrint" runat="server" Checked='<%# Bind("CanPrint") %>'  Font-Names="arial" Font-Size="X-Small"></asp:CheckBox></ItemTemplate>
            </EditItemTemplate>
              <HeaderStyle Width="10px" HorizontalAlign="Center" VerticalAlign="Middle"  />
              <ItemStyle  HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:ButtonField ButtonType="Link" Text="Select All" HeaderText="Select All" 
                 CommandName="SelectAll" ControlStyle-Width="50px" CausesValidation="True">
            <ControlStyle Width="50px" />
            <ItemStyle Width="60px" />
            </asp:ButtonField>
            <%--<asp:CommandField HeaderText="Edit" ShowEditButton="True" 
                CausesValidation="False" >
            </asp:CommandField>--%>
           <%--   <asp:CommandField HeaderText="Edit" ShowEditButton="True" />--%>
           
             <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
           
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
        <asp:Panel ID="pnlSave" runat="server" Width="535px">
            <table style="width: 670px">
                <tr>
                    <%--<td colspan="20">&nbsp;</td>--%>
                    <td style="width: 216px; text-align: right">
                        <asp:Button ID="btnSave" Text="Save" runat="server" onclick="btnSave_Click" 
                            style="margin-left: 0px; text-align: center;" Font-Size="X-Small" />
                    </td>
                    <td> <asp:HiddenField ID="HidDeleteCount" Value="0" runat="server" />
                <asp:HiddenField ID="HidUpdateCount" Value="0" runat="server" />
                </td>
                </tr>
            </table>
        </asp:Panel>
       </td>
            <td valign = "top"  style="height: 458px">
        <asp:Label ID="Label1" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkBlue"
            Text="To Create New Role" Width="441px" Font-Bold="True" Height="16px"></asp:Label>
            <asp:Label ID="Label2" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="* Select New Role. Give a Name to the Role. " Width="441px" Font-Bold="True" Height="16px"></asp:Label>
            <asp:Label ID="Label3" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="* In the Grid,  Assign Permissions Access, Create and Edit " Width="507px" 
                Font-Bold="True" Height="16px"></asp:Label>
            <asp:Label ID="Label4" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="* by clicking on small box for the programs" Width="507px" Font-Bold="True" Height="16px"></asp:Label>
             <asp:Label ID="lblFiller" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="" Width="507px" Font-Bold="True" Height="16px"></asp:Label>
          <asp:Label ID="Label5" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkBlue"
            Text="To Update Role" Width="441px" Font-Bold="True" Height="16px"></asp:Label>
            <asp:Label ID="Label6" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="* Select Update Role. Existing Roles will be Loaded in Role Name box" 
                  Width="441px" Font-Bold="True" Height="16px"></asp:Label>
            <asp:Label ID="Label7" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="* Select the Role Name to be updated" Width="507px" Font-Bold="True" Height="16px"></asp:Label>
             <asp:Label ID="Label8" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="* Grid will get Displayed with exisiting Permissions ticked" Width="507px" Font-Bold="True" Height="16px"></asp:Label>
              <asp:Label ID="Label9" runat="server" Font-Names="Verdana" 
                Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="* Click Edit link of the Program Name. The Row will change to edit mode." 
                Width="445px" Font-Bold="True" Height="16px"></asp:Label>
               <asp:Label ID="Label10" runat="server" Font-Names="Verdana" Font-Size="X-Small" ForeColor ="DarkOrchid"
            Text="* Check or uncheck the permission and click Update link" Width="507px" Font-Bold="True" Height="16px"></asp:Label>



        </td>
        </tr></table>
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

