<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage3.master" AutoEventWireup="true" CodeFile="WCFService.aspx.cs" Inherits="WCFService" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--<asp:Label ID="Label1" runat="server" Align= "center" Text=" "  width="785px"  style="text-align: center" Height="22px"></asp:Label>--%>

<asp:Label ID="lblCountrymas" runat="server" Align= "center" 
        Text="Web Server Data Synchronisation"   Font ="arial" width="499px"  Font-Size ="12pt" 
        Font-Bold="True" Font-Names="arial" 
        style="text-align: center" Height="30px"></asp:Label>
<div style="overflow:auto; height: 69px; width: 906px;">
    <asp:panel ID="pnlAdd" runat="server" Width="501px">
    <table  align="left">
  
            <tr>
                <td style="width: 90px">
                <asp:Label ID="lblYourVersion" runat="server" Text="System Version" 
                        Width="82px" Font-Bold="True" 
                            Font-Size="X-Small" Font-Names="arial"></asp:Label>
                </td>
                <td style="width: 77px">
                   <asp:TextBox ID="txtYourVersion" runat="server" ReadOnly="false" Text = "1.0"
                            Font-Names="arial" Font-Size="X-Small" Width="60px" ></asp:TextBox>
                 </td>

               <td style="width: 78px">
                <asp:Label ID="lblWebVersion" runat="server" Text="Web Version " Width="78px" Font-Bold="True" 
                            Font-Size="X-Small" Font-Names="arial"></asp:Label>
                </td>
                <td class="style23" style="width: 8px">
                   <asp:TextBox ID="txtWebVersion" runat="server" ReadOnly="false" Text = "1.1"
                            Font-Names="arial" Font-Size="X-Small" Width="55px" ></asp:TextBox>
                 </td>
                <td style="width: 129px">
                    <asp:Button ID="btnGo" runat="server" Text="Synchronise Data" Font-Names="arial" 
                        onclick="btnGo_Click" Font-Size="X-Small" Width="136px" />
                </td>
                </tr>
      <%--          <tr>
                 <td style="width: 61px; font-size: X-Small; font-family: arial; height: 32px;">
                    <asp:Label ID="lblFromDate" runat="server" Visible="true"
                        Text="From Date" Font-Size="X-Small" Font-Names="arial" 
                         
                         style="font-size: small; font-weight: bold; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                </td>
                    <td style="width: 138px; height: 32px;">
                    <asp:TextBox ID="txtFromDate" runat="server" Text="" Width="105px" Visible="true"
                        Font-Size="X-Small" style="font-family: arial" Height="20px" ></asp:TextBox>
                    <asp:ImageButton ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.gif" Visible="false"/>
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate" runat="server" PopupButtonID="imgfromdate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    
                </td>
                <td style="width: 45px; font-size: X-Small; font-family: arial; height: 32px;">
                    <asp:Label ID="lblToDate" runat="server" Visible="true"
                        Text="To Date" Font-Size="X-Small" Font-Bold="False" Font-Names="arial" 
                        
                        style="font-size: small; font-weight: bold; font-family: 'Times New Roman', Times, serif;"></asp:Label>
                </td>
                 <td style="width: 124px; height: 32px;">
                    <asp:TextBox ID="txtToDate" runat="server" Text="" Width="98px" Visible="true"
                        Font-Size="X-Small" style="font-family: arial" Height="20px"></asp:TextBox>
                    <asp:ImageButton ID="imgToDate" runat="server" ImageUrl="~/Images/Calendar.gif" Visible="false"/>
                    <asp:CalendarExtender ID="CalendarExtender2" TargetControlID="txtToDate" runat="server"   PopupButtonID="imgToDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                </td>
                 <td style="width: 129px">
                    <asp:Button ID="btnPost" runat="server" Text="Printed Data Upload" Font-Names="arial" 
                        onclick="btnPost_Click" Font-Size="X-Small" />
                </td>
                </tr>--%>
        </table>
    </asp:panel>
    </div>
    <pre lang="xml"></pre><div>
<marquee direction="left" scrollamount="4" loop="true" width="100%" bgcolor="#ffffff" >
    <asp:Label id="lblMarquee" runat="server" ForeColor="ForestGreen" Font-Bold="True" 
    Text ="Upon Clicking Sychronize button You will be connected to Web Server and Data Down load will take few minutes." ></asp:Label>
</marquee>
</div>
<%--             <asp:panel ID="Pnlgv" runat="server" Height="182px" Width="667px">
     <div style="overflow:auto; margin-left:1px; height: 160px; width: 657px;">   
        <asp:GridView ID="GrdUpLoad" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="632px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" OnRowCommand= "GrdUpLoad_RowCommand" >               
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />  <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /> <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" /> <AlternatingRowStyle BackColor="#DCDCDC" />
        
            <Columns>
         <asp:TemplateField HeaderText="CouponHdrId" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lblCouponHdrId" runat="server" Text='<%# Eval("CouponHdrId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <HeaderStyle Width="40px" />    </asp:TemplateField>

             <asp:TemplateField HeaderText="PartNumber">
            <ItemTemplate>
            <asp:Label ID="lblPartNumber" runat="server" Text='<%# Eval("PartNumber") %>'  Font-Names="arial" Font-Size="X-Small" Width="70px"></asp:Label></ItemTemplate>
              <HeaderStyle Width="70px" />   <ItemStyle Width="70px" />  </asp:TemplateField>

             <asp:TemplateField HeaderText="DepotName">
            <ItemTemplate>
            <asp:Label ID="lblDepotName" runat="server" Text='<%# Eval("DepotName")  %>'  Font-Names="arial" Width="60px" Font-Size="X-Small"></asp:Label></ItemTemplate>
              <HeaderStyle Width="60px" />   </asp:TemplateField>


              <asp:TemplateField HeaderText="PlantName">
            <ItemTemplate>
            <asp:Label ID="lblPlantName" runat="server" Text='<%# Eval("PlantName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
              <HeaderStyle Width="80px" />   </asp:TemplateField>
                   
            
             <asp:ButtonField CommandName="Print" HeaderText="Select" Text="Select">
                       <HeaderStyle Height="20px" />   <ItemStyle Width="20px" />
                     </asp:ButtonField>  
            </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>

    </asp:panel>--%>
 <%--      <asp:panel ID="Pnlgv" runat="server" Height="377px" Width="587px">
        <div style="overflow:auto; height:357px; width:543px">
    <asp:GridView ID="GrdUpLoad" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="399px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                 OnRowCommand ="GrdUpLoad_RowCommand">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>

  
      <asp:TemplateField HeaderText="CouponHdrId" Visible="true">
            <ItemTemplate> <asp:Label ID="lblCouponHdrId" runat="server" Text='<%# Eval("CouponHdrId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
            <HeaderStyle Width="40px" />  </asp:TemplateField>
           <asp:TemplateField HeaderText="Plant Name">
            <ItemTemplate> <asp:Label ID="lblPlantName" runat="server" Text='<%# Eval("PlantName") %>'  Font-Names="arial" Font-Size="X-Small" Width="70px"></asp:Label></ItemTemplate>
             <HeaderStyle Width="70px" />  </asp:TemplateField>
             <asp:TemplateField HeaderText="DepotName">
            <ItemTemplate> <asp:Label ID="lblDepotName" runat="server"  Width="120px" Text='<%# Eval("DepotName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
            <HeaderStyle Width="100px" />   </asp:TemplateField>
            <asp:TemplateField HeaderText="Work Order" Visible="False">
            <ItemTemplate> <asp:Label ID="lblWorkOrderNumber" runat="server" Text='<%# Eval("WorkOrderNumber") %>'  Font-Names="arial" Font-Size="X-Small" Width="70px"></asp:Label></ItemTemplate>
             <HeaderStyle Width="70px" />   </asp:TemplateField>
            <asp:TemplateField HeaderText="Part Number">
            <ItemTemplate><asp:Label ID="lblPartNumber" runat="server" Text='<%# Eval("PartNumber") %>'  Font-Names="arial" Font-Size="X-Small" Width="70px"></asp:Label></ItemTemplate>
             <HeaderStyle Width="70px" />    </asp:TemplateField>
             <asp:TemplateField HeaderText="Barcode Qty">
            <ItemTemplate> <asp:Label ID="lblBarcodeQty" runat="server" Text='<%# Eval("BarcodeQty") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
            <HeaderStyle Width="80px" />     </asp:TemplateField>
           <asp:ButtonField CommandName="UpLoad" HeaderText="UpLoad" Text="UpLoad">
                       <HeaderStyle Height="20px" />   <ItemStyle Width="20px" />
                     </asp:ButtonField>  --%>
        <%--     <asp:CommandField HeaderText="UpLoad" ShowEditButton="True" SelectText="UpLoad" >
            <HeaderStyle Width="30px" />   </asp:CommandField>--%>
               <%--     </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>--%>
</asp:Content>

