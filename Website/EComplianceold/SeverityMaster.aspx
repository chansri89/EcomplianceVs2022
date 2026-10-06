<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="SeverityMaster.aspx.cs" Inherits="SeverityMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="overflow:auto; height: 600px;">
    <%--<asp:Panel ID="Panel1" runat="server" Height="41px">
        <asp:Button ID="btnAdd" runat="server" Text="Add"
            Font-Names="Verdana" onclick="btnAdd_Click1" />
    </asp:Panel>--%>
    <asp:panel ID="Pnlgv" runat="server" Height="376px" Width="814px">
        <div style="overflow:auto; height:342px; width:762px">
    <asp:GridView ID="GrdSeverityMaster" runat="server" CellPadding="3" 
            Font-Size="XX-Small" Width="707px" Font-Names="Verdana" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdSeverityMaster_RowCancelingEdit" 
                onrowdeleting="GrdSeverityMaster_RowDeleting" 
                onrowediting="GrdSeverityMaster_RowEditing" 
                onrowupdating=" GrdSeverityMaster_RowUpdating">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
        <asp:TemplateField HeaderText="Severity Id" Visible="False">
            <ItemTemplate>
            <asp:Label ID="lblSevtId" runat="server" Text='<%# Eval("SeverityId") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtSevId" runat="server" Text='<%# Bind("SeverityId") %>' ReadOnly="true" Font-Names="Verdana" Font-Size="XX-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Severity Name">
            <ItemTemplate>
            <asp:Label ID="lblSevName" runat="server" Text='<%# Eval("SeverityName") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtSevName" runat="server" Text='<%# Bind("SeverityName") %>'  Font-Names="Verdana" Font-Size="XX-Small"  Width="250px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="250px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Severity ShortName">
            <ItemTemplate>
            <asp:Label ID="lblSevShName" runat="server" Text='<%# Eval("SeverityShortName") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtSevShName" runat="server" Text='<%# Bind("SeverityShortName") %>'  Font-Names="Verdana" Font-Size="XX-Small"  Width="30px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Remarks">
            <ItemTemplate>
            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>'  Font-Names="Verdana" Font-Size="XX-Small" Width="250px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="350px" />
            </asp:TemplateField>

               <asp:TemplateField HeaderText="IsActive">
             <ItemTemplate>
              <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>'
            Font-Names="Verdana" Font-Size="XX-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>'
            Font-Names="Verdana" Font-Size="XX-Small"></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
           
            <asp:CommandField HeaderText="Edit" ShowEditButton="True" >
            <HeaderStyle Width="20px" />
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
    <asp:panel ID="pnlAdd" runat="server" Width="634px" >
    <table style="width: 101%" align="center">
            <tr>
                <td class="style1" style="width: 124px; text-align: left;">
                    <asp:Label ID="lblSeverityName" runat="server" Text="Severity Name"  
                        Font-Names="Verdana" Font-Size="XX-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtSeverityName" runat="server" Font-Names="Verdana" 
                        Font-Size="XX-Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 124px; height: 21px; text-align: left;">
                    <asp:Label ID="lblSeverityShortName" runat="server" Text="Severity Short Name"  
                        Font-Names="Verdana" Font-Size="XX-Small" Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 21px">
                    <asp:TextBox ID="txtSeverityShort" runat="server" Font-Names="Verdana" 
                        Font-Size="XX-Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 124px; text-align: left;">
                    <asp:Label ID="lblRemarks" runat="server" Text="Remarks"  Font-Names="Verdana" 
                        Font-Size="XX-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtRemarks" runat="server" Font-Names="Verdana" 
                        Width="365px" Font-Size="XX-Small" MaxLength="350"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td class="style1" style="width: 124px" align="left">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Font-Names="Verdana" 
                        onclick="btnSave_Click" Font-Size="XX-Small" />
                </td>
                </tr>
                
        </table>
    </asp:panel>
    
    </div>
</asp:Content>

