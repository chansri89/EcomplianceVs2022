<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="DepartmentMaster.aspx.cs" Inherits="DepartmentMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="overflow:auto; height: 852px; width: 674px;">
   <%-- <asp:Panel ID="Panel1" runat="server" Height="41px">
        <asp:Button ID="btnAdd" runat="server" Text="Add"
            Font-Names="arial" onclick="btnAdd_Click1" />
    </asp:Panel>--%>
    <asp:Panel ID="Panel1" runat="server" Height="16px" Width="870px"> </asp:Panel>
    <asp:Label ID="lblStatMas" runat="server" Align= "center" Text="Department Master" 
            Font ="arial" width="450px" Font-Size ="12pt" Font-Bold="True" Font-Names="arial" 
        style="text-align: center" Height="22px"></asp:Label>
    <asp:panel ID="Pnlgv" runat="server" Width="637px" Height="384px">
        <div style="overflow:auto; height:367px; width:413px">
    <asp:GridView ID="GrdDepartmentMaster" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="366px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdDepartmentMaster_RowCancelingEdit" 
                onrowdeleting="GrdDepartmentMaster_RowDeleting" 
                onrowediting="GrdDepartmentMaster_RowEditing" 
                onrowupdating="GrdDepartmentMaster_RowUpdating">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
         <asp:TemplateField HeaderText="Department Id" Visible="False">
            <ItemTemplate>
            <asp:Label ID="lblDeptId" runat="server" Text='<%# Eval("DepartmentId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtDeptId" runat="server" Text='<%# Bind("DepartmentId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="DepartmentName">
            <ItemTemplate>
            <asp:Label ID="lblDeptName" runat="server" Text='<%# Eval("DepartmentName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtDeptName" runat="server" Text='<%# Bind("DepartmentName") %>'  Font-Names="arial" Font-Size="X-Small" Width="80px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="80px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Department ShortName">
            <ItemTemplate>
            <asp:Label ID="lblDeptShName" runat="server" Text='<%# Eval("DepartmentShortName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtDeptShName" runat="server" Text='<%# Bind("DepartmentShortName") %>' MaxLength="3"  Font-Names="arial" Font-Size="X-Small" Width="40px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

               <asp:TemplateField HeaderText="IsActive">
             <ItemTemplate>
              <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>'
            Font-Names="arial" Font-Size="X-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>'
            Font-Names="arial" Font-Size="X-Small"></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
           
            <asp:CommandField HeaderText="Edit" ShowEditButton="True" >
            <HeaderStyle Width="30px" />
            </asp:CommandField>
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" >
            <HeaderStyle Width="20px" />
            </asp:CommandField>
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
    <asp:panel ID="pnlAdd" runat="server" Width="584px"  
            Height="197px">
    <table style="width: 101%" align="center">
            <tr>
                <td class="style1" style="width: 210px; text-align: left;">
                    <asp:Label ID="lblDepartmentName" runat="server" Text="Department Name"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 225px">
                    <asp:TextBox ID="txtDepartmentName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Width="168px"></asp:TextBox>
                </td>
            <%--</tr>
            <tr>--%>
                <td class="style1" style="width: 354px; text-align: left;">
                    <asp:Label ID="lblDepartmentShortName" runat="server" Text="Department Short Name"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 155px">
                    <asp:TextBox ID="txtDepartmentShortName" runat="server" Font-Names="arial" MaxLength="3" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
           <%-- </tr>
            
            
            <tr>--%>
                <%--<td class="style1" style="width: 198px" align="left">
                    &nbsp;</td>--%>
                <td style="width: 225px">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Font-Names="arial" 
                        onclick="btnSave_Click" Font-Size="X-Small" /></td>
                        </tr>
                        <tr>
                        <td style="width: 210px">
                </td>
                 <td style="width: 225px"> <asp:HiddenField ID="HidDeleteCount" Value="0" runat="server" />
                <asp:HiddenField ID="HidUpdateCount" Value="0" runat="server" />
                </td>
            </tr>
        </table>
    </asp:panel>
    
    </div>
</asp:Content>

