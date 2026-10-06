<%@ Page Title="" Language="C#"  MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="rptActActivityAssigned.aspx.cs" Inherits="rptActActivityAssigned" %>
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
          <asp:Label ID="Label1" runat="server" Text="List of Execution Employees Assigned to Activities " Font-Names="arial" Font-Size="Medium" Height="20px"></asp:Label>
       </td>
     </tr>
</table>
    <table style="width: 535px">

       <tr>
         <td class="style1" style="text-align: left; width: 38px;">
                    <asp:Label ID="lblLocation" runat="server" Text="Location" Font-Names="arial" Font-Size="X-Small" Visible= "true" ></asp:Label>
                </td>
         <td style="width: 137px">
            <asp:DropDownList ID="ddlLocation" runat="server" Font-Size="X-Small" 
                 Height="16px" Width="156px" DataValueField="CompanyCode" Visible="true"
                DataTextField="CompanyShortName" Font-Names="arial" 
                 style="margin-bottom: 0px" AutoPostBack = "true" OnSelectedIndexChanged = "ddlLocation_Changed" >
            </asp:DropDownList>
        </td>
         <td class="style1" style="text-align: left; width: 99px;">
                    <asp:Label ID="lblExecutionEmployee" runat="server" Text="Execution Employee" Visible="true"    Font-Names="arial" Font-Size="X-Small"  ></asp:Label>
                </td>
         <td style="width: 67px">
                    <asp:DropDownList ID="ddlExecutionEmployee" runat="server" Font-Size="X-Small" Visible="true"
                        Height="16px" Width="122px" DataValueField="EmployeeCode" Enabled="true"
                        DataTextField="ExecutionEmployee" Font-Names="arial"           >
                    </asp:DropDownList>
                </td>
       <td style="width: 45px">
          <asp:Button ID="btnView" runat="server" Text="View" onclick="btnView_Click" Font-Names="arial" 
            Font-Size="10px"/>
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
