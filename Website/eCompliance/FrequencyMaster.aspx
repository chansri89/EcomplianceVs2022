<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="FrequencyMaster.aspx.cs" Inherits="FrequencyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px"> </asp:Panel>
    <asp:Label ID="lblStatMas" runat="server" Align= "center" Text="Frequency Master" Font ="arial" width="650px" Font-Size ="12pt" Font-Bold="True" Font-Names="arial" 
        style="text-align: center" Height="36px"></asp:Label>
<div style="overflow:auto; height: 703px;">
  <%--  <asp:Panel ID="Panel1" runat="server" Height="41px">
        <asp:Button ID="btnAdd" runat="server" Text="Add"
            Font-Names="arial" onclick="btnAdd_Click1" />
    </asp:Panel>--%>
    <asp:Panel ID="Panel1" runat="server" Height="16px" Width="870px"> </asp:Panel>
    <asp:panel ID="Pnlgv" runat="server" Height="365px" Width="621px">
        <div style="overflow:auto; height:341px; width:542px" >
    <asp:GridView ID="GrdFrequencyMaster" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="519px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdFrequencyMaster_RowCancelingEdit" 
                onrowdeleting="GrdFrequencyMaster_RowDeleting" 
                onrowediting="GrdFrequencyMaster_RowEditing" 
                onrowupdating="GrdFrequencyMaster_RowUpdating" style="margin-bottom: 0px">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
           <asp:TemplateField HeaderText="Frequence Id" Visible="False">
            <ItemTemplate>
            <asp:Label ID="lblFreqId" runat="server" Text='<%# Eval("FrequencyId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtFreqId" runat="server" Text='<%# Bind("FrequencyId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Freq Name">
            <ItemTemplate>
            <asp:Label ID="lblFreqName" runat="server" Text='<%# Eval("FrequencyName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtFreqName" runat="server" Text='<%# Bind("FrequencyName") %>'  Font-Names="arial" Font-Size="X-Small"  Width="60px" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Freq ShortName">
            <ItemTemplate>
            <asp:Label ID="lblFreqShName" runat="server" Text='<%# Eval("FrequencyShortName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtFreqShName" runat="server" Text='<%# Bind("FrequencyShortName") %>' MaxLength="2"  Font-Names="arial" Font-Size="X-Small"  Width="30px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>
              <asp:TemplateField HeaderText="Frequency Days">
            <ItemTemplate>
            <asp:Label ID="lblFreqDays" runat="server" Text='<%# Eval("FrequencyDays") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtFreqDays" runat="server" Text='<%# Bind("FrequencyDays") %>'  Font-Names="arial" Font-Size="X-Small"  Width="30px"></asp:TextBox></EditItemTemplate>
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
    <asp:panel ID="pnlAdd" runat="server" Width="563px">
    <table style="width: 50%" >
            <tr>
                <td class="style1" style="width: 135px; text-align: left;">
                    <asp:Label ID="lbFrequencyName" runat="server" Text="Frequency Name"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFrequencyName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 135px; text-align: left;">
                    <asp:Label ID="lblFrequencyShortName" runat="server" Text="Frequency Short Name"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFrequencyShortName" runat="server" Font-Names="arial" MaxLength="2" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 135px; text-align: left;">
                    <asp:Label ID="lblFrequencyDays" runat="server" Text="Frequency Days"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFrequencyDays" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td class="style1" style="width: 135px">
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

