<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage3.master" AutoEventWireup="true" CodeFile="UpLoadOrganization.aspx.cs" Inherits="UpLoadOrganization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label1" runat="server" Align= "center" Text=" "  width="785px"  style="text-align: center" Height="22px"></asp:Label>
<table></table>
<asp:Label ID="lblstatemas" runat="server" Align= "center" Text="Upload Organization Structure Data" 
        Font ="Verdana" width="787px" Font-Size ="12pt" Font-Bold="True" 
        Font-Names="Verdana" style="text-align: center" Height="23px"></asp:Label>
    <div style="height: 79px; width: 1084px;">
    <table id="tblFileType" runat="server" style="width: 881px; height: 47px;">
        <tr>
           <td style="text-align: left; font-family: Verdana; font-size: X-Small;" 
                align="right" class="style21">
                <asp:Label ID="lblOperationGroupName" runat="server" Text="Upload File Type" CssClass="txtbox" Font-Bold="True" Visible="True"></asp:Label>
            </td>
           <%-- <td style="width: 100px" class="style21">
                <asp:DropDownList ID="ddlOperationGroupName"  runat="server" AutoPostBack="True"
                    Font-Names="Verdana" DataTextField="OperationGroupName"
                   DataValueField="OperationGroupId" CssClass="ddl" 
                    Style="font-size: X-Small" Width="100px" 
                    onselectedindexchanged="ddlOperationGroupName_SelectedIndexChanged" >
                 </asp:DropDownList>
            </td>--%>
            <td >
                <asp:radiobuttonlist id="rbtExcelType"  Visible="true" Enabled="false"
                                    RepeatDirection="Horizontal" runat="server" Height="16px" 
                    Width="199px" Font-Names="Verdana" Font-Size="X-Small">
	                                <asp:listitem Text="Excel" Selected="True" Value="1"  />
	                                <asp:listitem Text="CSV"  Value="2"/>
                </asp:radiobuttonlist>
           </td>
            <td style="height: 25px;" class="style21">
                <asp:Label ID="lblFileUpload" runat="server" Text="File Path: " 
                  Font-Bold="true"  Font-Names="Verdana" Font-Size="X-Small" ></asp:Label>
            </td>
            <td style="height: 25px">
                <asp:FileUpload ID="FlUpdExcel" runat="server" Width="227px" Height="21px" 
                    Font-Names="Verdana" Font-Size="X-Small"/>
            </td>
            <td style="height: 25px">
                <asp:Button ID="btnUpload" runat="server" Font-Names="Verdana" Font-Size="X-Small" 
              ForeColor="Black" OnClick="btnUpload_Click" Text="Upload" Width="61px" 
              TabIndex="16" />
            </td>
            <td style="height: 25px">
                <asp:Button ID="btnSave" runat="server" Font-Names="Verdana" Font-Size="X-Small" 
              ForeColor="Black" OnClick="btnSave_Click" Text="Save" Width="61px" 
              TabIndex="17" />
            </td>
            <td style="width: 125px">
                    <asp:TextBox ID="txtWParameterId" runat="server" Font-Names="Verdana" 
                        height="15px" Visible="false"
                        Font-Size="X-Small" Width="85px"></asp:TextBox>
                </td>
        </tr>
    </table>

    <table style="width: 990px; height: 15px">
   <%--  <tr>
        <td>
         <asp:Label ID="Label2" runat="server" CssClass="txtbox" Font-Bold="True" 
         Text = "DATA Must for Organization: Organization Name, Address Proof, Bank Account Number, Organization Type, District Name, State Name, Region Name. Date format to be DD.MM.YYYY"
            Width="990px" ForeColor="BlueViolet"></asp:Label>
          </td>
        
        </tr>--%>
    <tr>
    <td>
     <asp:Label ID="lblMessage" runat="server" CssClass="txtbox" Font-Bold="True" 
            Width="993px" ForeColor="#FF3300"></asp:Label>

    </td>
    </tr>
    </table>
   
           

</div>
   <asp:panel ID="PnlWtOK" runat="server" Height="210px" Width="1089px" >

<table><tr><td> <b style ="color: #002880" > Data Status Row wise  </b></td></tr></table>
        <div style="overflow:auto; height:176px; width:844px" 
            title = "OK Data in UpLoad">
    <asp:GridView ID="GrdWtOK" runat="server" CellPadding="3" Font-Size="X-Small" 
                Width="791px" Font-Names="Verdana"  AutoGenerateColumns="False" 
            Height="37px" GridLines="Vertical" BackColor="White" BorderColor="#999999"  
                BorderStyle="None" BorderWidth="1px" >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />     <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /> <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"      HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
             <asp:TemplateField HeaderText="Record Number" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblRowNumber" runat="server" Text='<%# Eval("RKount") %>' Width="30px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location Code" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblOrganizationCode" runat="server" Text='<%# Eval("OrganizationCode") %>' Width="40px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="40px" />
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Location Name" Visible="true" >
            <ItemTemplate>
            <asp:Label ID="lblOrganizationName" runat="server" Text='<%# Eval("OrganizationName") %>' Width="450px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="450px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Status" >
            <ItemTemplate>
            <asp:Label ID="lblResult" runat="server" Text='<%# Eval("Result") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="150px" /> <ItemStyle Width="150px" />
            </asp:TemplateField>
      

        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
    <br />
       <asp:panel ID="PnlWtstatus" runat="server" Height="200px" Width="950px">
        <div style="overflow:auto; height:190px; width:880px">
        <table><tr><td> <b style ="color: #002880"> Organization Upload Status</b></td></tr></table>
    <asp:GridView ID="GrdWtStatus" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="731px" Font-Names="Verdana" 
                AutoGenerateColumns="False" 
            Height="37px" GridLines="Vertical" BackColor="White" BorderColor="#999999"  
                BorderStyle="None" BorderWidth="1px" 
               >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>

                  <asp:TemplateField HeaderText="Location Code" >
            <ItemTemplate>
            <asp:Label ID="lblLocationCode" runat="server" width = "50px" Text='<%# Eval("LocationCode") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="50px" />
            </asp:TemplateField>

   
             <asp:TemplateField HeaderText="Location Name">
             <ItemTemplate>
            <asp:Label ID="lblLocationName" runat="server" width = "250px" Text='<%# Eval("LocationName") %>'></asp:Label></ItemTemplate>
                 <HeaderStyle Width="250px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Location Type" >
            <ItemTemplate>
            <asp:Label ID="lblLocationType" runat="server" width = "40px" Text='<%# Eval("LocationType") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Parent Unit Code" >
            <ItemTemplate>
            <asp:Label ID="lblParentUnitCode" runat="server" width = "50px" Text='<%# Eval("LocationParentCode") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="50px" />
            </asp:TemplateField>
            
    <%--         <asp:TemplateField HeaderText="Address Proof Detail">
             <ItemTemplate>
            <asp:Label ID="lblAddressProofDetail" runat="server" width = "50px"  Text='<%# Eval("AddressProofDetail") %>'></asp:Label></ItemTemplate>
                 <HeaderStyle Width="50px" />
            </asp:TemplateField>--%>

 <%--            <asp:TemplateField HeaderText="Id Proof Detail">
             <ItemTemplate>
            <asp:Label ID="lblIdProofDetail" runat="server" width = "50px" Text='<%# Eval("IdProofDetail") %>'></asp:Label></ItemTemplate>
            <HeaderStyle Width="50px" />
              </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="Reason"  >
            <ItemTemplate>
            <asp:Label ID="lblReason" runat="server" Text='<%# Eval("Reason") %>' Width="150px" ></asp:Label></ItemTemplate>
            <HeaderStyle Width="150px" /><ItemStyle Width="150px" />
            </asp:TemplateField>

        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
</asp:Content>

