<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="CompanyACT.aspx.cs" Inherits="CompanyACT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="Panel2" runat="server" Height="18px" Width="870px"> </asp:Panel>
<asp:Label ID="lblLoacMas" runat="server" Align= "center" Text="Company And Its Selected Act " 
        Font ="arial" width="650px" Font-Size ="12pt" Font-Bold="True" Font-Names="arial" 
        style="text-align: center" Height="36px"></asp:Label>
    <div style="overflow:auto; height: 727px; width: 970px;">
        <%--</td></tr>
    </table>--%>    <%--</asp:Panel>--%>
    <asp:Label ID="lblCollapse" runat="server" Align= "center" Text="Collapse or Expand Below company Grid by clicking on the button here -->"  
    Font ="arial" width="734px" Font-Size ="10pt"  Font-Names="arial"  ForeColor = "Blue"
            style="text-align: right" Height="19px"></asp:Label>
        <asp:Button ID="btnCollapse" runat="server" OnClick="btnCollapse_Click" 
            Text="-" Font-Size="Small" Width="38px" height="20px"/>
    <asp:panel ID="PnlComp" runat="server" Width="810px" Height="333px">
    <table>
     <tr><td class="style28" style="width: 727px" align="right"> 
     <asp:Label ID="Label1" runat="server" Text="Select Companies for Which Acts are to be Assigned FRESH and Click Add : " Font-Size="Small" ForeColor="#000080"></asp:Label> </td>
     <td align="right">
      <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="Add" 
             Font-Size="Small" Width="44px" height="24px"/>
     </td>
     </tr>
     </table>
        <div id="divCompany" runat="server" 
            style="overflow:auto; height:90%; width:98%">
    <asp:GridView ID="GrdCompanyMaster" runat="server" CellPadding="3" Font-Size="X-Small" Width="98%" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" OnRowCommand="GrdCompanyMaster_RowCommand" BackColor="White" tooltip = "Use EditAct when you want to Modify for a Particular Company"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" >
        <EditRowStyle Font-Size="X-Small" />   <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />  <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />  <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"  HorizontalAlign="Left" />  <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:TemplateField HeaderText="Location Code" >
            <ItemTemplate>
            <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <HeaderStyle Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location Name">
            <ItemTemplate>
            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' Width="110px" Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <HeaderStyle  Width="110px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location Short Name">
             <ItemTemplate>
            <asp:Label ID="lblCompanyShortName" runat="server" Text='<%# Eval("CompanyShortName") %>' Width="60px" Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
                 <HeaderStyle Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Parent Company Name">
             <ItemTemplate>
            <asp:Label ID="lblParentCompanyName" runat="server" Text='<%# Eval("ParentCompanyName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
                <HeaderStyle Width="160px" />
            </asp:TemplateField>
            
          <asp:TemplateField HeaderText="State Short Name">
             <ItemTemplate>
            <asp:Label ID="lblStateShortName" runat="server" Text='<%# Eval("StateShortName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
      
              <HeaderStyle Width="50px" />       <ItemStyle Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate>
            <asp:CheckBox ID="chkActive" runat="server" Checked= "false"
            Font-Names="arial" Font-Size="X-Small" Enabled="true"></asp:CheckBox>
             </ItemTemplate>   <HeaderStyle Width="30px" />     <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>           
            <asp:CommandField HeaderText="Edit"  ShowEditButton="True" Visible="false"  >
            <HeaderStyle Width="30px" />  </asp:CommandField>
             <asp:ButtonField CommandName="Select" HeaderText="Edit Act" Text="EditAct" Visible="true">
                        <HeaderStyle Height="20px" />
                        <ItemStyle Width="20px" />
                    </asp:ButtonField>
           
  
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    <%--</td>
    </tr>--%>
    </asp:panel>

     <asp:panel ID="pnlAct" runat="server" Height="470px" Width="810px">
     <table style="width: 778px">
     <tr>
    <td class="style28" style="width: 510px" align="left"> 
     <asp:Label ID="lblCompanySelected" runat="server" Text="" Font-Size="Small" ForeColor="#000080"></asp:Label> </td>
 
     <td class="style28" style="width: 460px" align="right"> 
     <asp:Label ID="lblSave" runat="server" Text="Select Acts by Checking the Box in Grid and Click Save : " Font-Size="Small" ForeColor="#000080"></asp:Label> </td>
     
     <td align="right">
      <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" 
             Font-Size="Small" Width="44px" height="24px"/>
     </td>
     <td align="right">
      <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" 
             Font-Size="Small" Width="44px" height="24px"/>
     </td>
     </tr>
     </table>
        <div style="overflow:auto; height:98%; width:98%">
    <asp:GridView ID="GrdActMaster" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="98%" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px"  >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />   <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />  <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
         <asp:TemplateField HeaderText="ActId" Visible="False">  <ItemTemplate>
            <asp:Label ID="lblActId" runat="server" Text='<%# Eval("ActId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
            <HeaderStyle Width="20px" />
            </asp:TemplateField>
     
             <asp:TemplateField HeaderText="Act">
            <ItemTemplate>  <asp:Label ID="lblActName" runat="server" Text='<%# Eval("ActName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
              <HeaderStyle Width="250px" /> <ItemStyle Width="250px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="IsActive">
             <ItemTemplate>
              <asp:CheckBox ID="chkActActive" runat="server" Checked='<%# Eval("IsActive") %>'
            Font-Names="arial" Font-Size="X-Small" Enabled="true"></asp:CheckBox>
             </ItemTemplate>   <HeaderStyle Width="40px" /> <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>  
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
    <%--</td></tr>
    </table>--%>
    <%--</asp:Panel>--%>
    </div>
</asp:Content>

