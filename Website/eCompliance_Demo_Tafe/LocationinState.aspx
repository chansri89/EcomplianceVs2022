<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="LocationinState.aspx.cs" Inherits="LocationinState" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px"> </asp:Panel>
<asp:Label ID="lblStatMas" runat="server" Align= "center" Text="Location in State Master" Font ="arial" width="650px" Font-Size ="12pt" Font-Bold="True" Font-Names="arial" 
        style="text-align: center" Height="36px"></asp:Label>

<div style="overflow:auto; height: 619px; width: 1072px;">
    
    <asp:panel ID="Pnlgv" runat="server" Height="403px" Width="620px">
        <div style="overflow:auto; height:374px; width:342px">
    <asp:GridView ID="GrdLocState" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="302px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdLocState_RowCancelingEdit" 
                onrowdeleting="GrdLocState_RowDeleting" 
                onrowediting="GrdLocState_RowEditing" 
                onrowupdating="GrdLocState_RowUpdating" >
                
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
              
          <asp:TemplateField HeaderText="LocationinStateId" Visible="False">
            <ItemTemplate>
            <asp:Label ID="lblLocationinStateId" runat="server" Text='<%# Eval("LocationinStateId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtLocationinStateId" runat="server" Text='<%# Bind("LocationinStateId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Location in State">
            <ItemTemplate>
            <asp:Label ID="lblLocationinState" runat="server" Text='<%# Eval("LocationinState") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtLocationinState" runat="server" Text='<%# Bind("LocationinState") %>'  Font-Names="arial" Font-Size="X-Small"> </asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="100px" />
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
    <asp:panel ID="pnlAdd" runat="server" Width="272px" 
            Height="16px">
    <table style="width: 210%; height: 51px;">
           
            <tr>
                <td width="100px">
                    <asp:Label ID="lblLocationinState" runat="server" Text="Location in State"  
                        Font-Names="arial" Font-Size="X-Small" style="font-weight: 700" >
                        </asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtLocationinState" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Width="190px"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td>
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

