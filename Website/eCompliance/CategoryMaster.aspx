<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="CategoryMaster.aspx.cs" Inherits="CategoryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px"> </asp:Panel>
<asp:Label ID="lblStatMas" runat="server" Align= "center" Text="Category Master" Font ="arial" width="450px" Font-Size ="12pt" Font-Bold="True" Font-Names="arial" 
        style="text-align: center" Height="36px"></asp:Label>
    <div style="overflow:auto; height: 619px; width: 967px;">
    <%--<asp:Panel ID="Panel1" runat="server" Height="40px">
        <asp:Button ID="btnAdd" runat="server" Text="Add"
            Font-Names="arial" onclick="btnAdd_Click1" />
    </asp:Panel>--%>
    <asp:panel ID="Pnlgv" runat="server" Height="349px" Width="807px">
        <div style="overflow:auto; height:337px; width:442px">
    <asp:GridView ID="GrdCategoryMaster" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="411px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdCategoryMaster_RowCancelingEdit" 
                onrowdeleting="GrdCategoryMaster_RowDeleting" 
                onrowediting="GrdCategoryMaster_RowEditing" 
                onrowupdating="GrdCategoryMaster_RowUpdating">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
          <asp:TemplateField HeaderText="Category Id" Visible="False">
            <ItemTemplate>
            <asp:Label ID="lblCategoryId" runat="server" Text='<%# Eval("CategoryId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtCategoryId" runat="server" Text='<%# Bind("CategoryId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

           

             <asp:TemplateField HeaderText="Category Full Name">
            <ItemTemplate>
            <asp:Label ID="lblCategoryFullName" runat="server" Text='<%# Eval("CategoryFullName") %>'  Font-Names="arial" Font-Size="X-Small" Width="100px"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtCategoryFullName" runat="server" Text='<%# Bind("CategoryFullName") %>'  Font-Names="arial" Font-Size="X-Small" Width="100px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Category Name">
            <ItemTemplate>
            <asp:Label ID="lblCategoryName" runat="server" Text='<%# Eval("CategoryName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtCategoryName" runat="server" Text='<%# Bind("CategoryName") %>'  MaxLength="8" Font-Names="arial" Font-Size="X-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="60px" />
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
            <HeaderStyle Width="40px" />
            </asp:CommandField>
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" >
            <HeaderStyle Width="30px" />
            </asp:CommandField>
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
    <asp:panel ID="pnlAdd" runat="server" Width="329px" 
            Height="64px">
    <table style="width: 212%; height: 73px;">
           
           
            <tr>
                <td class="style1" style="width: 119px; text-align:left; height: 22px;">
                    <asp:Label ID="lblCategoryfullName" runat="server" Text="Category Full Name"  
                        Font-Names="arial" Font-Size="X-Small" 
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 22px">
                    <asp:TextBox ID="txtCategoryfullName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 119px; text-align:left; height: 21px;">
                    <asp:Label ID="lblCategoryName" runat="server" Text="Category Name"  
                        Font-Names="arial" Font-Size="X-Small" 
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 21px">
                    <asp:TextBox ID="txtCategoryName" runat="server" Font-Names="arial" MaxLength="8" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 119px">
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

