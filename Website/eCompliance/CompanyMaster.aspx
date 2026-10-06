<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="CompanyMaster.aspx.cs" Inherits="CompanyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div id = "DivTop" style="height: 714px; width: 1171px">
 <script src="js/jquery-1.9.1.js" type="text/javascript"></script>
   <script language="javascript" type="text/javascript">
//       
//       $(document).ready(function () {
//           var ctrlDown = false;
//           var ctrlKey = 17, vKey = 86, cKey = 67;
//           $(document).keydown(function (e) 
//                     {
//                         if (e.keyCode == ctrlKey) ctrlDown = true;
//                      }).keyup(function (e) {
//                         if (e.keyCode == ctrlKey) ctrlDown = false;
//                     });

//           $("Div").keydown(function (e) {
//               if (ctrlDown && (e.keyCode == vKey || e.keyCode == cKey)) return false;
//           });
//       });
   </script>

<asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px"> </asp:Panel>
<asp:Label ID="lblLoacMas" runat="server" Align= "center" Text="Location Master" 
        Font ="arial" width="865px" Font-Size ="12pt" Font-Bold="True" Font-Names="arial" 
        style="text-align: center" Height="24px"></asp:Label>
    <div style="overflow:auto; height: 649px; width: 1171px;">
    
        <%--</td></tr>
    </table>--%>    <%--</asp:Panel>--%>
    <asp:panel ID="Pnlgv" runat="server" Width="1049px" Height="328px">
        <div id="divCompany" runat="server" 
            style="overflow:auto; height:317px; width:1042px">
    <asp:GridView ID="GrdCompanyMaster" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="912px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" 
                onrowcancelingedit="GrdCompanyMaster_RowCancelingEdit" 
                onrowdeleting="GrdCompanyMaster_RowDeleting" 
                onrowediting="GrdCompanyMaster_RowEditing" BackColor="White" 
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                onrowupdating="GrdCompanyMaster_RowUpdating" DataKeyNames="CompanyCode">
        <EditRowStyle Font-Size="X-Small" />
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
            <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>' Width="40px"   Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtCompanyCode" runat="server" Text='<%# Bind("CompanyCode") %>' Width="40px"  ReadOnly="true"  Font-Names="arial" Font-Size="X-Small" Enabled="false"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location Name">
            <ItemTemplate>
            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' Width="150px" Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtCompanyName" runat="server" Text='<%# Bind("CompanyName") %>'  Font-Names="arial" Font-Size="X-Small" Width="150px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle  Width="150px" /> 
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location Short Name">
             <ItemTemplate>
            <asp:Label ID="lblCompanyShortName" runat="server" Text='<%# Eval("CompanyShortName") %>' Width="60px" Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtCompanyShortName" runat="server" Text='<%# Bind("CompanyShortName") %>' MaxLength="8" Width="60px" Font-Names="arial" Font-Size="X-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="LocationTypeId" Visible="false">
             <ItemTemplate>
            <asp:Label ID="lblLocationTypeId" runat="server" Text='<%# Eval("LocationTypeId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtLocationTypeId" runat="server" Text='<%# Bind("LocationTypeId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location Type">
             <ItemTemplate>
            <asp:Label ID="lblCompanyType" runat="server" Text='<%# Eval("CompanyFlag") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <%--<asp:TextBox ID="txtCompanyType" runat="server" Text='<%# Bind("CompanyFlag") %>'  Font-Names="arial" Font-Size="X-Small"></asp:TextBox>--%>
             <asp:DropDownList ID="ddlCompanyType" runat="server"  DataValueField="LocationTypeId" DataTextField="LocationTypeShortName"   DataSource='<%#getLocationType() %>'
                  Font-Names="arial" Font-Size="X-Small">
                 <asp:ListItem Text="--Parent--" Value="0" />
                 </asp:DropDownList>
            </EditItemTemplate>
                <HeaderStyle Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Parent Company Name">
             <ItemTemplate>
            <asp:Label ID="lblParentCompanyName" runat="server" Width="150px" Text='<%# Eval("ParentCompanyName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
          <%--  <asp:TextBox ID="txtParentCompanyName" runat="server" Text='<%# Bind("ParentCompanyName") %>'></asp:TextBox>--%>
                 <asp:DropDownList ID="ddlParentCompanyName" runat="server" Width="150px" DataValueField="CompanyCode" DataTextField="CompanyName"   DataSource='<%#getParentCompanyName() %>'
                  Font-Names="arial" Font-Size="X-Small">
                 <asp:ListItem Text="--Parent--" Value="0" />
                 </asp:DropDownList>
            </EditItemTemplate>
                <HeaderStyle Width="150px" />
            </asp:TemplateField>
            
          <asp:TemplateField HeaderText="State Short Name">
             <ItemTemplate>
            <asp:Label ID="lblStateShortName" runat="server" Text='<%# Eval("StateShortName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
                 <asp:DropDownList ID="ddlStateShortName" runat="server" DataValueField="StateId" DataTextField="StateShortName"   DataSource='<%#getState() %>'
                  Font-Names="arial" Font-Size="X-Small">
                
                 </asp:DropDownList>
            </EditItemTemplate>
              <HeaderStyle Width="50px" />
              <ItemStyle Width="50px" />
            </asp:TemplateField>
            
             <asp:TemplateField HeaderText="Location in State">
             <ItemTemplate>
            <asp:Label ID="lblLocationName" runat="server" Text='<%# Eval("LocationName") %>'  Font-Names="arial" Font-Size="X-Small" Width="50px"></asp:Label></ItemTemplate>
             <EditItemTemplate>
                 <asp:DropDownList ID="ddlLocationName" runat="server" DataValueField="LocationinStateId" DataTextField="LocationinState"   DataSource='<%#getLocationstate() %>'
                  Font-Names="arial" Font-Size="X-Small">
                 </asp:DropDownList>
            </EditItemTemplate>
              <HeaderStyle Width="50px" />   <ItemStyle Width="50px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Company HR">
             <ItemTemplate>
            <asp:Label ID="lblCompanyHR" runat="server" Text='<%# Eval("CompanyHRName") %>'  Font-Names="arial" Font-Size="X-Small" Width="80px"></asp:Label></ItemTemplate>
             <EditItemTemplate>
                 <asp:DropDownList ID="ddlCompanyHR" runat="server" DataValueField="EmployeeCode" DataTextField="EmployeeName"   DataSource='<%#getCompanyHR() %>'
                  Font-Names="arial" Font-Size="X-Small">
                 </asp:DropDownList>
            </EditItemTemplate>
              <HeaderStyle Width="60px" />      <ItemStyle Width="60px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate>
            <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>'
            Font-Names="arial" Font-Size="X-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>'
            Font-Names="arial" Font-Size="X-Small"></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="30px" />     <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
            <asp:CommandField HeaderText="Edit"  ShowEditButton="True" >
            <HeaderStyle Width="30px" />
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
    <%--</td>
    </tr>--%>
    </asp:panel>
    <br />
    <asp:panel ID="pnlAdd" runat="server" Width="811px"  
            style="margin-top: 0px" Height="216px">
    <table style="width: 75%" >
            <tr>
                <td class="style1" style="width: 164px; text-align: left;">
                    <asp:Label ID="lblCompanyCode" runat="server" Text="Location Code"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCompanyCode" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
           
                <td class="style1" style="width: 164px; ">
                    <asp:Label ID="lblCompanyName" runat="server" Text="Location Name"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCompanyName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Width="154px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 164px; text-align: left;">
                    <asp:Label ID="lblCompanyShortName" runat="server" Text="Location Short Name"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtCompanyShortName" runat="server" Font-Names="arial" MaxLength="8"
                        Font-Size="X-Small"></asp:TextBox>
                </td>
           
                <td class="style1" style="width: 164px; text-align: left;">
                    <asp:Label ID="lblCompanyType" runat="server" Text="Location Type"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCompanyType" runat="server" DataValueField="LocationTypeId" 
                        DataTextField="LocationTypeShortName" Font-Names="arial" 
                        Font-Size="X-Small" Height="16px" Width="106px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 164px; text-align: left;">
                    <asp:Label ID="lblParentCompanyName" runat="server" Text="Parent Company Name" 
                        Font-Names="arial" Font-Size="X-Small"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlParentCompanyName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" height="18px" width="119px">
                    </asp:DropDownList>
                </td>
           
                <td class="style1" style="width: 164px; text-align: left;">
                    <asp:Label ID="lblStateShortName" runat="server" Text="State Short Name" 
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStateShortName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" height="18px" width="113px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 164px; text-align: left;">
                    <asp:Label ID="txtLocation" runat="server" Text="Location in State" 
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 164px";>
                    <asp:DropDownList ID="ddlLocation" runat="server" Font-Names="arial" 
                        DataTextField="LocationinState" DataValueField="LocationinStateId"
                        Font-Size="X-Small" Height="16px" Width="118px">
                    </asp:DropDownList>
                </td>
           
                <td class="style1" style="width: 164px; text-align: left;">
                    <asp:Label ID="lblCompanyHR" runat="server" Text="Company HR" 
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlCompanyHr" runat="server" Font-Names="arial" 
                        DataTextField="EmployeeName" DataValueField="EmployeeCode"
                        Font-Size="X-Small" Height="16px" Width="172px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 164px">
                    <asp:CheckBox ID="chkIsActive" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Text="IsActive" Visible="False" />
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Font-Names="arial" 
                        onclick="btnSave_Click" Font-Size="X-Small" />
                </td>
                </tr>
                <tr>
                <td style="width: 164px"> <asp:HiddenField ID="HidDeleteCount" Value="0" runat="server" />
                <asp:HiddenField ID="HidUpdateCount" Value="0" runat="server" />
                </td>
            </tr>
        </table>
    </asp:panel>
    <%--</td></tr>
    </table>--%>
    <%--</asp:Panel>--%>
    </div>
    </div>
</asp:Content>

