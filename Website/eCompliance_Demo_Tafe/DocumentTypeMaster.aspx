<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="DocumentTypeMaster.aspx.cs" Inherits="DocumentTypeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="Panel2" runat="server" Height="20px" Width="870px"> </asp:Panel>
<asp:Label ID="lblStatMas" runat="server" Align= "center" Text="Document Type  Master" Font ="arial" width="450px" Font-Size ="12pt" Font-Bold="True" Font-Names="arial" 
        style="text-align: center" Height="36px"></asp:Label>
    <div style="overflow:auto; height: 664px;">
    <%--<asp:Panel ID="Panel1" runat="server" Height="41px">
        <asp:Button ID="btnAdd" runat="server" Text="Add"
            Font-Names="arial" onclick="btnAdd_Click1" />
    </asp:Panel>--%>
   <%-- <asp:Panel ID="Panel1" runat="server" Height="16px" Width="870px"> </asp:Panel>--%>
    <asp:panel ID="Pnlgv" runat="server" Height="422px" Width="871px">
        <div style="overflow:auto; height:400px; width:416px">
    <asp:GridView ID="GrdDocumentMaster" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="387px" Font-Names="arial" AutoGenerateColumns="False" 
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
         <asp:TemplateField HeaderText="DocumentType Id" Visible="False">
            <ItemTemplate>
            <asp:Label ID="lblDocumId" runat="server" Text='<%# Eval("DocumentTypeId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtDocumId" runat="server" Text='<%# Bind("DocumentTypeId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="DocumentType Name">
            <ItemTemplate>
            <asp:Label ID="lblDocumName" runat="server" Text='<%# Eval("DocumentTypeName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtDocumName" runat="server" Text='<%# Bind("DocumentTypeName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="DocumentType ShortName">
            <ItemTemplate>
            <asp:Label ID="lblDocumShName" runat="server" Text='<%# Eval("DocumentTypeShortName") %>'  Font-Names="arial" Font-Size="X-Small" ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtDocumhName" runat="server" Text='<%# Bind("DocumentTypeShortName") %>' MaxLength="2"  Font-Names="arial" Font-Size="X-Small"  Width="30px"></asp:TextBox></EditItemTemplate>
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
                <HeaderStyle Width="20px" />
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
    <asp:panel ID="pnlAdd" runat="server" Width="529px">
    <table style="width: 100%" >
            <tr>
                <td class="style1" style="width: 159px; text-align: left;">
                    <asp:Label ID="lblDocumentTypetName" runat="server" Text="Document Type Name"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDocumentTypetName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 159px; text-align: left;">
                    <asp:Label ID="lblDocumentTypeShortName" runat="server" Text="Document Type Short Name"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDocumentTypeShortName" runat="server" Font-Names="arial" MaxLength="2" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td class="style1" style="width: 159px">
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Font-Names="arial" 
                        onclick="btnSave_Click" Font-Size="X-Small" />
                </td>
                </tr>
                
        </table>
    </asp:panel>
    
    </div>
</asp:Content>

