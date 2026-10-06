<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="CompanyMaster.aspx.cs" Inherits="CompanyMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px"> </asp:Panel>
<asp:Label ID="lblLoacMas" runat="server" Align= "center" Text="Location Master" 
        Font ="arial" width="650px" Font-Size ="12pt" Font-Bold="True" Font-Names="arial" 
        style="text-align: center" Height="36px"></asp:Label>
    <div style="overflow:auto; height: 649px; width: 1171px;">
        <%--</td></tr>
    </table>--%>    <%--</asp:Panel>--%>
    <asp:panel ID="Pnlgv" runat="server" Width="986px" Height="333px">
        <div id="divCompany" runat="server" 
            style="overflow:auto; height:323px; width:962px">
    <asp:GridView ID="GrdCompanyMaster" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="929px" Font-Names="arial" AutoGenerateColumns="False" 
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
            <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtCompanyCode" runat="server" Text='<%# Bind("CompanyCode") %>' ReadOnly="true"  Font-Names="arial" Font-Size="X-Small" Enabled="false"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Location Name">
            <ItemTemplate>
            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' Width="110px" Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtCompanyName" runat="server" Text='<%# Bind("CompanyName") %>'  Font-Names="arial" Font-Size="X-Small" Width="120px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle  Width="110px" />
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
             <asp:DropDownList ID="ddlCompanyType" runat="server" DataValueField="LocationTypeId" DataTextField="LocationTypeShortName"   DataSource='<%#getLocationType() %>'
                  Font-Names="arial" Font-Size="X-Small">
                 <asp:ListItem Text="--Parent--" Value="0" />
                 </asp:DropDownList>
            </EditItemTemplate>
                <HeaderStyle Width="50px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Parent Company Name">
             <ItemTemplate>
            <asp:Label ID="lblParentCompanyName" runat="server" Text='<%# Eval("ParentCompanyName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
          <%--  <asp:TextBox ID="txtParentCompanyName" runat="server" Text='<%# Bind("ParentCompanyName") %>'></asp:TextBox>--%>
                 <asp:DropDownList ID="ddlParentCompanyName" runat="server" DataValueField="CompanyCode" DataTextField="CompanyName"   DataSource='<%#getParentCompanyName() %>'
                  Font-Names="arial" Font-Size="X-Small">
                 <asp:ListItem Text="--Parent--" Value="0" />
                 </asp:DropDownList>
            </EditItemTemplate>
                <HeaderStyle Width="160px" />
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
</asp:Content>

