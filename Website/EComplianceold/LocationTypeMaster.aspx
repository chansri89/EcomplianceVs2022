<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="LocationTypeMaster.aspx.cs" Inherits="LocationTypeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="overflow:auto; height: 619px; width: 967px;">
    <%--<asp:Panel ID="Panel1" runat="server" Height="40px">
        <asp:Button ID="btnAdd" runat="server" Text="Add"
            Font-Names="Verdana" onclick="btnAdd_Click1" />
    </asp:Panel>--%>
    <asp:panel ID="Pnlgv" runat="server" Height="413px" Width="955px">
        <div style="overflow:auto; height:342px; width:459px">
    <asp:GridView ID="GrdLocationTypeMaster" runat="server" CellPadding="3" 
            Font-Size="XX-Small" Width="404px" Font-Names="Verdana" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdLocationTypeMaster_RowCancelingEdit" 
                onrowdeleting="GrdLocationTypeMaster_RowDeleting" 
                onrowediting="GrdLocationTypeMaster_RowEditing" 
                onrowupdating="GrdLocationTypeMaster_RowUpdating">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
          <asp:TemplateField HeaderText="LocationTypeId" Visible="False">
            <ItemTemplate>
            <asp:Label ID="lblLocationTypeId" runat="server" Text='<%# Eval("LocationTypeId") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtLocationTypeId" runat="server" Text='<%# Bind("LocationTypeId") %>' ReadOnly="true" Font-Names="Verdana" Font-Size="XX-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Location Type Name">
            <ItemTemplate>
            <asp:Label ID="lblLocationTypeName" runat="server" Text='<%# Eval("LocationTypeName") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtLocationTypeName" runat="server" Text='<%# Bind("LocationTypeName") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="80px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Location Type short Name">
            <ItemTemplate>
            <asp:Label ID="lblLocationTypeShortName" runat="server" Text='<%# Eval("LocationTypeShortName") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtLocationTypeShortName" runat="server" Text='<%# Bind("LocationTypeShortName") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="80px" />
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
    <table style="width: 212%; height: 30px;">
           
            <tr>
                <td class="style1" style="width: 148px; text-align:left; height: 13px;">
                    <asp:Label ID="lblLocationTypeName" runat="server" Text="Location Type Name"  
                        Font-Names="Verdana" Font-Size="XX-Small" 
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 13px">
                    <asp:TextBox ID="txtLocationType" runat="server" Font-Names="Verdana" 
                        Font-Size="XX-Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 148px; text-align:left; height: 11px;">
                    <asp:Label ID="lblLocationTypeShName" runat="server" Text="Location Type Short Name"  
                        Font-Names="Verdana" Font-Size="XX-Small" 
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="height: 11px">
                    <asp:TextBox ID="txtLocationTypeShName" runat="server" Font-Names="Verdana" 
                        Font-Size="XX-Small"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td class="style1" style="width: 148px; height: 3px;">
                    </td>
                <td style="height: 3px">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Font-Names="Verdana" 
                        onclick="btnSave_Click" Font-Size="XX-Small" />
                </td>
               
            </tr>
        </table>
    </asp:panel>
    
    </div>
</asp:Content>

