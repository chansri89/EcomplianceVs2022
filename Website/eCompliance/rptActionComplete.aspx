<%@ Page Title="" Language="C#"  MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="rptActionComplete.aspx.cs" Inherits="rptActionComplete" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
 

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
  
 <script src="js/jquery-1.9.1.js" type="text/javascript"></script>
   <script type="text/javascript">
       var prm = Sys.WebForms.PageRequestManager.getInstance();
       //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
       prm.add_beginRequest(BeginRequestHandler);
       // Raised after an asynchronous postback is finished and control has been returned to the browser.
       prm.add_endRequest(EndRequestHandler);
       function BeginRequestHandler(sender, args) {
           //Shows the modal popup - the update progress
           var popup = $find('<%= modalPopup.ClientID %>');
           if (popup != null) {
               popup.show();
           }
       }
       function EndRequestHandler(sender, args) {
           //Hide the modal popup - the update progress
           var popup = $find('<%= modalPopup.ClientID %>');
           if (popup != null) {
               popup.hide();
           }
       }

       </script>
       <asp:UpdateProgress ID="UpdateProgress" runat="server">
<ProgressTemplate>

<asp:Image ID="imgprocess" ImageUrl="~/Images/progressBar.gif" AlternateText="Processing" runat="server" />
</ProgressTemplate>
</asp:UpdateProgress>

<asp:modalpopupextender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
PopupControlID="UpdateProgress" BackgroundCssClass="modalPopup" />
<table>
    <tr>
       <td class="style2" style="width: 877px" align="center">
          <asp:Label ID="Label1" runat="server" Text="Activity Action Due and Completed Date  " Font-Names="arial" Font-Size="Medium" Height="20px"></asp:Label>
       </td>
     </tr>
</table>
    <table style="width: 856px">
<%--       <tr>
         <td style="width: 40px; font-size: X-Small; font-family: arial; height: 32px;">
                    <asp:Label ID="lblFromDate" runat="server" 
                        Text="From Date" Font-Size="X-Small" Font-Names="arial" 
                         
                        ></asp:Label>
                </td>
         <td style="width: 109px; height: 32px;">
                    <asp:TextBox ID="txtFromDate" runat="server" Text="" Width="80px" 
                        Font-Size="X-Small" style="font-family: arial" Height="20px" ></asp:TextBox>
                    <asp:ImageButton ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate" runat="server" PopupButtonID="imgfromdate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    
                </td>
         <td style="width: 34px; font-size: X-Small; font-family: arial; height: 32px;">
                    <asp:Label ID="lblToDate" runat="server" 
                        Text="To Date" Font-Size="X-Small" Font-Bold="False" Font-Names="arial" 
                        
                       ></asp:Label>
                </td>

                <td style="width: 107px">
                    <asp:TextBox ID = "txtEndDate" runat= "server" Text="" Width = "80px" Font-Size="X-Small" Font-Names= "arial" Height= "20px"></asp:TextBox>
                    <asp:ImageButton ID="imgEndDate" runat= "server" ImageUrl="~/Images/calendar.gif" />
                    <asp:CalendarExtender ID= "Cal1" TargetControlID= "txtEndDate" runat="server" PopupButtonID="imgendDate" Format= "dd/MM/yyyy"></asp:CalendarExtender>
 
                </td>
          <td class="style1" style="text-align: left; width: 53px;">
                    <asp:Label ID="lblLocation" runat="server" Text="Location" Font-Names="arial" Font-Size="X-Small" Visible= "true" ></asp:Label>
                </td>
         <td style="width: 132px">
            <asp:DropDownList ID="ddlLocation" runat="server" Font-Size="X-Small" 
                 Height="16px" Width="121px" DataValueField="LocationCode" Visible="true"
                DataTextField="LocationName" Font-Names="arial" 
                 style="margin-bottom: 0px"  >
            </asp:DropDownList>
        </td>
     
         <td class="style1" style="text-align: left; width: 32px;">
                    <asp:Label ID="lblAct" runat="server" Text="Act" Font-Names="arial" Font-Size="X-Small"></asp:Label>
                </td>
         <td style="width: 244px">
                    <asp:DropDownList ID="ddlAct" runat="server" Font-Size="X-Small" Height="17px" 
                        Width="245px" DataValueField="ActId" 
                        DataTextField="ActName" Font-Names="arial"    >
                    </asp:DropDownList>
                </td>
         <td class="style1" style="text-align: left; width: 55px;">
                    <asp:Label ID="lblSeverity" runat="server" Text="Severity"     Font-Names="arial" Font-Size="X-Small"  ></asp:Label>
                </td>
         <td style="width: 67px">
                    <asp:DropDownList ID="ddlSeverity" runat="server" Font-Size="X-Small" 
                        Height="16px" Width="61px" DataValueField="SeverityId" Enabled="true"
                        DataTextField="SeverityName" Font-Names="arial"           >
                    </asp:DropDownList>
                </td>
 
         <td style="width: 45px">
          <asp:Button ID="btnView" runat="server" Text="View" onclick="btnView_Click" Font-Names="arial" 
            Font-Size="10px"/>
        </td>
  
    </tr>   --%> 
           <tr>
         <td style="width: 60px; font-size: X-Small; font-family: arial; height: 32px;">
                    <asp:Label ID="lblFromDate" runat="server" 
                        Text="From Date" Font-Size="X-Small" Font-Names="arial" 
                         
                        ></asp:Label>
                </td>
         <td style="width: 137px; height: 32px;">
                    <asp:TextBox ID="txtFromDate" runat="server" Text="" Width="91px" 
                        Font-Size="X-Small" style="font-family: arial" Height="21px" ></asp:TextBox>
                    <asp:ImageButton ID="imgFromDate" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtFromDate" runat="server" PopupButtonID="imgfromdate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    
                </td>
         <td style="width: 72px; font-size: X-Small; font-family: arial; height: 32px;">
                    <asp:Label ID="lblToDate" runat="server" 
                        Text="To Date" Font-Size="X-Small" Font-Bold="False" Font-Names="arial" 
                        
                       ></asp:Label>
                </td>

         <td style="width: 107px">
                    <asp:TextBox ID = "txtEndDate" runat= "server" Text="" Width = "80px" Font-Size="X-Small" Font-Names= "arial" Height= "20px"></asp:TextBox>
                    <asp:ImageButton ID="imgEndDate" runat= "server" ImageUrl="~/Images/calendar.gif" />
                    <asp:CalendarExtender ID= "Cal1" TargetControlID= "txtEndDate" runat="server" PopupButtonID="imgendDate" Format= "dd/MM/yyyy"></asp:CalendarExtender>
 
                </td>
      
         <td class="style1" style="text-align: left; width: 32px;">
                    <asp:Label ID="lblAct" runat="server" Text="Act" Font-Names="arial" Font-Size="X-Small"></asp:Label>
                </td>
         <td style="width: 156px">
                    <asp:DropDownList ID="ddlAct" runat="server" Font-Size="X-Small" Height="17px" 
                        Width="245px" DataValueField="ActId" 
                        DataTextField="ActName" Font-Names="arial"    >
                    </asp:DropDownList>
                </td>
                       <td style="width: 45px">
          <asp:Button ID="btnView" runat="server" Text="View" onclick="btnView_Click" Font-Names="arial" 
            Font-Size="10px"/>
        </td>
        
       </tr>
       <tr>
         <td class="style1" style="text-align: left; width: 60px;">
                    <asp:Label ID="lblLocation" runat="server" Text="Location" Font-Names="arial" Font-Size="X-Small" Visible= "false" ></asp:Label>
                </td>
         <td style="width: 137px">
            <asp:DropDownList ID="ddlLocation" runat="server" Font-Size="X-Small" 
                 Height="16px" Width="121px" DataValueField="LocationCode" Visible="false"
                DataTextField="LocationName" Font-Names="arial" 
                 style="margin-bottom: 0px"  >
            </asp:DropDownList>
        </td>
         <td class="style1" style="text-align: left; width: 55px;">
                    <asp:Label ID="lblSeverity" runat="server" Text="Severity" Visible="false"    Font-Names="arial" Font-Size="X-Small"  ></asp:Label>
                </td>
         <td style="width: 67px">
                    <asp:DropDownList ID="ddlSeverity" runat="server" Font-Size="X-Small" Visible="false"
                        Height="16px" Width="91px" DataValueField="SeverityId" Enabled="true"
                        DataTextField="SeverityName" Font-Names="arial"           >
                    </asp:DropDownList>
                </td>
         <td style="text-align: left; width: 72px;">
                    <asp:Label ID="lblActionStatus" runat="server" Text="Action Status" Visible="false"     Font-Names="arial" Font-Size="X-Small"  ></asp:Label>
                </td>
         <td style="width: 156px">
                    <asp:DropDownList ID="ddlActionStatus" runat="server" Font-Size="X-Small"  Visible="false"
                        Height="16px" Width="94px" DataValueField="ActionStatus" Enabled="true"
                        DataTextField="ActionStatusName" Font-Names="arial"  >
                          <asp:ListItem Value="A" Text="-- ALL --"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Completed"></asp:ListItem>
                            <asp:ListItem Value="D" Text="Deviation"></asp:ListItem>
                            <asp:ListItem Value="P" Text="Pending"></asp:ListItem>
                        </asp:DropDownList>
                                
                </td>
  
  
    </tr>   
    </table>
    </div>
<asp:Panel ID="pnlcrop" runat="server" Height="540px" Width= "1145px">
       <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>--%>
        <rsweb:ReportViewer ID="ReportViewer" runat="server" Width="1123px" Height="427px" 
            Font-Names="arial" Font-Size="8pt" InteractiveDeviceInfos="(Collection)" 
            WaitMessageFont-Names="arial" WaitMessageFont-Size="14pt" 
           ShowExportControls= "true" ShowBackButton="false" ShowFindControls="false"
       ShowRefreshButton="false" style="margin-right: 0px; margin-top: 0px;">
        <LocalReport ReportPath="">  
        <%--<DataSources>
                <rsweb:ReportDataSource DataSourceId="AgilerWFDataSet" Name="Dataset1" />
            </DataSources>--%>
        </LocalReport>
    </rsweb:ReportViewer>
       <%-- <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            SelectMethod="GetData" 
            TypeName="">
        </asp:ObjectDataSource>--%>
        </asp:Panel>
</asp:Content>
