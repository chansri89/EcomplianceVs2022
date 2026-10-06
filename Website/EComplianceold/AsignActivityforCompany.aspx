<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true"
    CodeFile="AsignActivityforCompany.aspx.cs" Inherits="AsignActivityforCompany" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <asp:DropDownList ID="ddlUtimate" runat="server" >--%>
    <div style="overflow: auto; height: 947px; width: 1202px">
        <asp:Panel ID="pnlCompany" runat="server" Height="26px" Width="924px">
            <table style="width: 100%; height: 26px;">
                <tr>
                    <td style="width: 105px; text-align: left;">
                        <asp:Label ID="lblCompanyName" runat="server" Text="Location Name" Font-Names="Verdana"
                            Font-Size="XX-Small"></asp:Label>
                    </td>
                    <td style="width: 107px">
                        <asp:DropDownList ID="ddlCompanyName" runat="server" Font-Names="Verdana" Font-Size="XX-Small">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 43px; text-align: left;">
                        <asp:Label ID="lblActName" runat="server" Text="Act" Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                    </td>
                    <td style="width: 126px">
                        <asp:DropDownList ID="ddlActName" runat="server" Font-Names="Verdana" Font-Size="XX-Small">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnGo" runat="server" Text="Go" Font-Names="Verdana" Font-Size="XX-Small"
                            OnClick="btnGo_Click" />
                    </td>
                    <td align="right" style="text-align: right">
                        <%--   <asp:Button ID="btnCompActivitiesHide" runat="server" Text="- Collapse"
                    Width="70px" BackColor="#00006A" Height="17px" Font-Names="Verdana" 
                            Font-Size="XX-Small" onclick="btnCompActivitiesHide_Click" Visible="False" 
                            ForeColor="White" style="margin-left: 49px" /> </td>--%>
                        <%--ToolTip="Click Expand Hide the CompanyActivity Grid and Show other Grids"--%>
                </tr>
            </table>
        </asp:Panel>
        <%--    <asp:Label ID="lblMsg" runat="server" Font-Names="Verdana" 
                Font-Size="XX-Small" ForeColor="#0033CC" 
                Text="Select Company & Act then Click Go" Visible="false"></asp:Label>--%>
        <br />
        <%--//commented by Abinayaa.Bcoz the Msg not shown -040213--%>
        <%--<asp:Label ID="lblActivityGrdColorMsg" runat="server" Font-Names="Verdana" 
                Font-Size="XX-Small" ForeColor="#0033CC" 
                Text="Blue Colour Indicates Activity not Yet Assigned " Visible="False"></asp:Label>--%>
        <%--//commented by Abinayaa.Bcoz the Msg not shown -040213--%>
        <%--  <br />--%>
        <asp:Panel ID="pnlGrdCompActivities" runat="server" Height="310px">
            <asp:Panel ID="pnlGridCompActivities" runat="server" Width="957px" Height="163px">
                <div style="width: 1111px; overflow: auto; height: 274px;">
                    <asp:GridView ID="GrdCompActivities" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="Verdana"
                        Font-Size="XX-Small" GridLines="Vertical" Height="16px" Width="1039px" OnRowEditing="GrdCompActivities_RowEditing"
                        OnRowCancelingEdit="GrdCompActivities_RowCancelingEdit" OnRowUpdating="GrdCompActivities_RowUpdating"
                        OnRowCommand="GrdCompActivities_RowCommand" AllowSorting="True" OnRowDataBound="GrdCompActivities_RowDataBound">
                        <%--  onrowdatabound="GrdCompActivities_RowDataBound">--%>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <%--<asp:ButtonField CommandName="Select" HeaderText="Select" Text="Select">
               <HeaderStyle Height="10px" />
               <ItemStyle Width="10px" />
         </asp:ButtonField>--%>
                            <asp:CommandField HeaderText="Select" ShowSelectButton="True">
                                <HeaderStyle Width="40px" />
                            </asp:CommandField>
                            <asp:TemplateField HeaderText="ActivityId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                        Text='<%# Eval("ActivityId") %>'>

                                    </asp:Label>
                                </ItemTemplate>
                                <%-- <EditItemTemplate>
                     <asp:TextBox ID="txtActivityId" runat="server" Font-Names="Verdana" 
                                    Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("ActivityId") %>'>

                     </asp:TextBox>
               </EditItemTemplate>--%>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Company ActivityId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompanyActivityId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                        Text='<%# Eval("CompanyActivityId") %>'>

                                    </asp:Label>
                                </ItemTemplate>
                                <%--<EditItemTemplate>
                     <asp:TextBox ID="txtCompanyActivityId" runat="server" Font-Names="Verdana" 
                                    Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("CompanyActivityId") %>'>

                     </asp:TextBox>
               </EditItemTemplate>--%>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("ActivityName") %>'
                                        ToolTip="Click Select to Assign New Activity document for the Location"></asp:Label></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtActivityName" runat="server" Text='<%# Bind("ActivityName") %>'
                                        Font-Names="Verdana" ReadOnly="true" Font-Size="XX-Small" Width="250px"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="250px" />
                                <ItemStyle Width="250px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Month">
                                <ItemTemplate>
                                    <asp:Label ID="lblDueMonth" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                        Text='<%# Eval("DueMonth") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddldueMonth" runat="server" DataValueField="Month" DataTextField="MonthName"
                                        DataSource='<%#LoadMonth() %>' Font-Names="Verdana" Font-Size="XX-Small">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="15px" />
                                <ItemStyle Width="15px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDueDate" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                        Text='<%# Eval("DueDate") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <%-- <asp:DropDownList ID="ddldueDate" runat="server"  DataValueField="Date" DataTextField="Date"   DataSource='<%#LoadDate() %>'
                 Font-Names="Verdana" Font-Size="XX-Small">
                 </asp:DropDownList>--%>
                                    <asp:DropDownList ID="ddlDueDate" runat="server" Font-Names="Verdana" Font-Size="XX-Small">
                                        <asp:ListItem Value="0" Text="NA"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="15px" />
                                <ItemStyle Width="15px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Day">
                                <ItemTemplate>
                                    <asp:Label ID="lblDueDay" runat="server" Text='<%# Eval("DueDay") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlDueDay" runat="server" Font-Names="Verdana" DataTextField="Day"
                                        DataValueField="Number" DataSource="<%#LoadDays() %> " Width="80px" Font-Size="XX-Small">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trig Month">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrigMonth" runat="server" Text='<%# Eval("TriggerMonth") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTrigMonth" runat="server" Font-Names="Verdana" DataSource="<%#LoadMonth() %>"
                                        DataTextField="MonthName" DataValueField="Month" Font-Size="XX-Small">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="15px" />
                                <ItemStyle Width="15px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trig Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrigDate" runat="server" Text='<%# Eval("TriggerDate") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTrigDate" runat="server" Font-Names="Verdana" Font-Size="XX-Small">
                                        <asp:ListItem Value="0" Text="NA"></asp:ListItem>
                                        <asp:ListItem Value="1" Text="1"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="2"></asp:ListItem>
                                        <asp:ListItem Value="3" Text="3"></asp:ListItem>
                                        <asp:ListItem Value="4" Text="4"></asp:ListItem>
                                        <asp:ListItem Value="5" Text="5"></asp:ListItem>
                                        <asp:ListItem Value="6" Text="6"></asp:ListItem>
                                        <asp:ListItem Value="7" Text="7"></asp:ListItem>
                                        <asp:ListItem Value="8" Text="8"></asp:ListItem>
                                        <asp:ListItem Value="9" Text="9"></asp:ListItem>
                                        <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                        <asp:ListItem Value="11" Text="11"></asp:ListItem>
                                        <asp:ListItem Value="12" Text="12"></asp:ListItem>
                                        <asp:ListItem Value="13" Text="13"></asp:ListItem>
                                        <asp:ListItem Value="14" Text="14"></asp:ListItem>
                                        <asp:ListItem Value="15" Text="15"></asp:ListItem>
                                        <asp:ListItem Value="16" Text="16"></asp:ListItem>
                                        <asp:ListItem Value="17" Text="17"></asp:ListItem>
                                        <asp:ListItem Value="18" Text="18"></asp:ListItem>
                                        <asp:ListItem Value="19" Text="19"></asp:ListItem>
                                        <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                        <asp:ListItem Value="21" Text="21"></asp:ListItem>
                                        <asp:ListItem Value="22" Text="22"></asp:ListItem>
                                        <asp:ListItem Value="23" Text="23"></asp:ListItem>
                                        <asp:ListItem Value="24" Text="24"></asp:ListItem>
                                        <asp:ListItem Value="25" Text="25"></asp:ListItem>
                                        <asp:ListItem Value="26" Text="26"></asp:ListItem>
                                        <asp:ListItem Value="27" Text="27"></asp:ListItem>
                                        <asp:ListItem Value="28" Text="28"></asp:ListItem>
                                        <asp:ListItem Value="29" Text="29"></asp:ListItem>
                                        <asp:ListItem Value="30" Text="30"></asp:ListItem>
                                        <asp:ListItem Value="31" Text="31"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="15px" />
                                <ItemStyle Width="15px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trig Day">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrigDay" runat="server" Text='<%# Eval("TriggerDay") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTrigDay" runat="server" Font-Names="Verdana" DataTextField="Day"
                                        DataValueField="Number" DataSource="<%#LoadDays() %> " Width="80px" Font-Size="XX-Small">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Execution Person" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblExecutionPerson" runat="server" Text='<%# Eval("Executioner") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlExecutionPerson" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                        DataValueField="EmployeeCode" DataTextField="EmployeeName" DataSource='<%#getExecutionPerson() %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="30px" />
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Review Person" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblReviewPerson" runat="server" Text='<%# Eval("Reviewer") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlReviewPerson" runat="server" DataSource="<%#getReviewPerson() %>"
                                        DataTextField="EmployeeName" DataValueField="EmployeeCode" Font-Names="Verdana"
                                        Font-Size="XX-Small">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="30px" />
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Head Person" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblHeadPerson" runat="server" Text='<%# Eval("HeadEmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlHeadPerson" runat="server" DataSource="<%#getHeadPerson() %>"
                                        DataTextField="EmployeeName" DataValueField="EmployeeCode" Font-Names="Verdana"
                                        Font-Size="XX-Small">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="30px" />
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Utimate Person" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblUtimate" runat="server" Text='<%# Eval("UltimateEmployeeName") %>'>

                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <%--  <asp:DropDownList ID="ddlUtimate" runat="server" >--%>
                                    <%-- DataSource="<%#getUtimate() %>" DataTextField="UltimateEmployeeCode" 
                    DataValueField="UltimateEmployeeCode" Font-Names="Verdana" Font-Size="XX-Small">--%>
                                    <%--</asp:DropDownList>--%>
                                </EditItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Frequencey">
                                <ItemTemplate>
                                    <asp:Label ID="lblFrequencey" runat="server" Text='<%# Eval("FrequencyName") %>'
                                        Width="60px">

                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlFrequencey" runat="server" DataSource="<%#getFrequencey() %>"
                                        DataTextField="FrequencyName" DataValueField="FrequencyId" Font-Names="Verdana"
                                        Font-Size="XX-Small">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Responsible Group Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblResponsibleGroupName" runat="server" Text='<%# Eval("ResponsibleGroupName") %>'
                                        Width="60px">

                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="40px" />
                                <ItemStyle Width="40px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Frq Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblFrqRemarks" runat="server" Text='<%# Eval("FrqRemarks") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFrqRemarks" runat="server" Text='<%# Bind("FrqRemarks") %>' Font-Names="Verdana"
                                        Font-Size="XX-Small" Width="30px">
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="50px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Compliance Flag">
                                <ItemTemplate>
                                    <asp:Label ID="lblNonComplianceTaskAvailableFlag" runat="server" Text='<%# Eval("NonComplianceTaskAvailableFlag") %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <%-- <EditItemTemplate>
            <asp:TextBox ID="txtNonComplianceTaskAvailableFlag" runat="server" Text='<%# Bind("NonComplianceTaskAvailableFlag") %>'
             Font-Names="Verdana" Font-Size="XX-Small" Width="30px"></asp:TextBox></EditItemTemplate>--%>
                                <HeaderStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is  Active">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>' Font-Names="Verdana"
                                        Font-Size="XX-Small" Enabled="false"></asp:CheckBox>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>' Font-Names="Verdana"
                                        Font-Size="XX-Small"></asp:CheckBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Edit" ShowEditButton="True" Visible="false">
                                <HeaderStyle Width="90px" />
                                <ItemStyle Width="90px" />
                            </asp:CommandField>
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="false" Visible="False" />
                        </Columns>
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </div>
            </asp:Panel>
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlGridSelect" runat="server" Visible="False">
            <table>
                <tr>
                    <td style="width: 846px; text-align: right;">
                        <asp:TextBox ID="txtTitle" runat="server" Text="Assign Activity Document and Task Name                 
                                                                                       
                                        Click - button to Assign Activity Document for Location" Width="878px" ReadOnly="true"
                            Style="text-align: left" BackColor="#003366" ForeColor="White" Font-Bold="true"
                            Font-Size="XX-Small" Height="16px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnCollapse" Text="-" runat="server" Width="36px" Font-Size="XX-Small"
                            OnClick="btnCollapse_Click" ToolTip="Click this button to Assign Activity for Location" />
                    </td>
                </tr>
            </table>
            <asp:Panel ID="pnlDocumentType" runat="server" Height="333px" Width="1020px">
                <asp:Panel ID="pnlCompanyAct" runat="server" Width="890px">
                    <%--BorderWidth="1px"--%>
                    <table style="width: 118%">
                        <tr>
                            <td style="text-align: left; width: 96px;">
                                <asp:Label ID="lblCmpName" runat="server" Text="Location Name" Font-Names="Verdana"
                                    Font-Size="XX-Small" Font-Bold="True" Style="text-align: left"></asp:Label>
                            </td>
                            <td style="width: 89px">
                                <asp:TextBox ID="txtCmpName" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                    ReadOnly="true" Width="122px" Font-Bold="True"></asp:TextBox>
                            </td>
                            <td style="width: 35px; text-align: left;" class="style22">
                                <asp:Label ID="lbAct" runat="server" Text="Act" Font-Names="Verdana" Font-Size="XX-Small"
                                    Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 175px">
                                <asp:TextBox ID="txtAct" runat="server" Width="214px" Font-Names="Verdana" Font-Size="XX-Small"
                                    ReadOnly="true" Font-Bold="True"></asp:TextBox>
                            </td>
                            <td style="width: 88px; text-align: left;" class="style21">
                                <asp:Label ID="lblActivities" runat="server" Text="Activity Name" Font-Names="Verdana"
                                    Font-Size="XX-Small" Font-Bold="True" Style="text-align: left"></asp:Label>
                            </td>
                            <td style="width: 259px">
                                <asp:TextBox ID="txtActivities" runat="server" Width="209px" Font-Names="Verdana"
                                    Font-Size="XX-Small" ReadOnly="true" Font-Bold="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCompanyActivityId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                    Visible="False" ReadOnly="True" Width="37px" Style="text-align: right"></asp:TextBox>
                                <asp:TextBox ID="txtActDocId" runat="server" Visible="False" Width="31px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCompliance" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                    Visible="False" ReadOnly="True" Width="37px" Style="text-align: right"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <asp:Panel ID="pnlGrdDocumentTask" runat="server" Width="658px" Height="34px" Style="margin-top: 11px">
                    <table style="width: 171%; height: 277px;">
                        <tr>
                            <td style="width: 112px; height: 293px;">
                                <asp:Panel ID="pnDocumentGrd" runat="server" Width="408px" Height="322px" Style="margin-top: 0px">
                                    <%--<br />--%>
                                    <div style="width: 331px; height: 314px; overflow: auto">
                                        <table>
                                            <tr>
                                                <td style="width: 187px">
                                                    <asp:Label ID="lblDocumentMaster" runat="server" Text="Create Activity Document Attributes"
                                                        Font-Names="Verdana" Font-Size="XX-Small" Width="300px" Style="text-align: left;
                                                        margin-left: 0px" ForeColor="#3333CC"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:GridView ID="GrdDocument" runat="server" BackColor="White" BorderColor="#999999"
                                            BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False"
                                            Height="16px" Width="312px" OnRowEditing="GrdDocument_RowEditing" OnRowCancelingEdit="GrdDocument_RowCancelingEdit"
                                            OnRowUpdating="GrdDocument_RowUpdating" Font-Names="Verdana" Font-Size="XX-Small">
                                            <AlternatingRowStyle BackColor="#DCDCDC" />
                                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" Font-Size="XX-Small" />
                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#000065" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ActivityDocumentTypeId" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActivityDocumentTypeId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                            Text='<%# Eval("ActDocTypeId") %>'>

                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtActivityDocumentTypeId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                            ReadOnly="true" Text='<%# Bind("ActDocTypeId") %>'>

                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                    <ItemStyle Width="180px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company ActivityId" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocumentTypeId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                            Text='<%# Eval("DocumentTypeId") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDocumentTypeId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                            ReadOnly="true" Text='<%# Bind("DocumentTypeId") %>'>


                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                    <ItemStyle Width="180px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company ActivityId" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompanyActivityId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                            Text='<%# Eval("ActivityForCompanyId") %>'>


                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCompanyActivityId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                            ReadOnly="true" Text='<%# Bind("ActivityForCompanyId") %>'>


                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                    <ItemStyle Width="180px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Document Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocumentTypeName" runat="server" Text='<%# Eval("DocumentTypeName") %>'
                                                            Font-Names="Verdana" Font-Size="XX-Small">

        
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDocumentTypeName" runat="server" Text='<%# Bind("DocumentTypeName") %>'
                                                            Font-Names="Verdana" ReadOnly="true" Font-Size="XX-Small" Width="60px">

       
                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Be Maintained" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblToBeMaintained" runat="server" Text='<%# Eval("ToBeMaintained") %>'
                                                            Font-Names="Verdana" Font-Size="XX-Small">

                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtToBeMaintained" runat="server" Text='<%# Bind("ToBeMaintained") %>'
                                                            Font-Names="Verdana" Font-Size="XX-Small" Width="20px" MaxLength="1">

                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Be Submitted" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblToBeSubmitted" runat="server" Text='<%# Eval("ToBeSubmitted") %>'
                                                            Font-Names="Verdana" Font-Size="XX-Small">

       
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtToBeSubmitted" runat="server" Text='<%# Bind("ToBeSubmitted") %>'
                                                            Font-Names="Verdana" Font-Size="XX-Small" Width="20px" MaxLength="1">

                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                </asp:TemplateField>
                                                <%--Master Document is not Company Depanded for Every Activity hence Chnged in to Visibe "False" 06/07/12 Abinayaa--%>
                                                <asp:TemplateField HeaderText="Master Document Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMasterPDFName" runat="server" Text='<%# Eval("MasterPDFName") %>'
                                                            Font-Names="Verdana" Font-Size="XX-Small">

       
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtMasterPDFName" runat="server" Text='<%# Bind("MasterPDFName") %>'
                                                            Font-Names="Verdana" Font-Size="XX-Small" Width="150px">

       
                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AccessPath" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccessPath" runat="server" Text='<%# Eval("AccessPath") %>' Font-Names="Verdana"
                                                            Font-Size="XX-Small">
     
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtAccessPath" runat="server" Text='<%# Bind("AccessPath") %>' Font-Names="Verdana"
                                                            Font-Size="XX-Small" Width="20px">

      
                                                        </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is  Active">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDocActive" runat="server" Checked='<%# Eval("IsActive") %>'
                                                            Font-Names="Verdana" Font-Size="XX-Small" Enabled="false"></asp:CheckBox>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkDocIsActive" runat="server" Checked='<%# Bind("IsActive") %>'
                                                            Font-Names="Verdana" Font-Size="XX-Small"></asp:CheckBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                                                <asp:CommandField HeaderText="Delete" ShowDeleteButton="false" Visible="False" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </td>
                            <td style="height: 293px; width: 513px;">
                                <asp:Panel ID="pnlNonComplianceTaskMsg" runat="server" Visible="False">
                                    <asp:Label ID="lblNonComplianceTask" runat="server" Text="Activity does not have Compliance Task set"
                                        Font-Names="Verdana" Font-Size="XX-Small" Width="332px" ForeColor="#3333CC"></asp:Label>
                                </asp:Panel>
                                <asp:Panel ID="PnlTask" runat="server" Width="508px" Height="322px" Style="margin-top: 6px">
                                    <table style="height: 22px; width: 472px;">
                                        <tr>
                                            <td style="width: 111px; text-align: right;">
                                                <asp:Label ID="lblTaskName" runat="server" Text="Task Name" Font-Names="Verdana"
                                                    Font-Size="XX-Small" Width="114px" Style="text-align: left; margin-left: 0px"
                                                    Font-Bold="True"></asp:Label>
                                            </td>
                                            <td style="width: 157px; text-align: left;">
                                                <asp:TextBox ID="txtTaskName" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                    Width="142px"></asp:TextBox>
                                            </td>
                                            <td style="width: 97px">
                                                <asp:CheckBox ID="chkTaskIsActive" runat="server" Text="IsActive" Font-Names="Verdana"
                                                    Font-Size="XX-Small" />
                                            </td>
                                            <td style="width: 215px">
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" Font-Names="Verdana" Font-Size="XX-Small"
                                                    Width="30px" OnClick="btnAdd_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="pnlGrdTask" runat="server" Width="537px" Height="293px">
                                        <div style="width: 496px; height: 217px; overflow: auto">
                                            <asp:GridView ID="GrdTaskName" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                                Height="16px" Width="365px" Font-Names="Verdana" Font-Size="XX-Small" OnRowCancelingEdit="GrdTaskName_RowCancelingEdit"
                                                OnRowEditing="GrdTaskName_RowEditing" OnRowUpdating="GrdTaskName_RowUpdating">
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" Font-Size="XX-Small" />
                                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#000065" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ActivityFor CompanyId" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActivityForCompanyId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                                Text='<%# Eval("ActivityForCompanyId") %>'>

                 
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtActivityForCompanyId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                                ReadOnly="true" Text='<%# Bind("ActivityForCompanyId") %>'>

                                                            </asp:TextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="20px" />
                                                        <ItemStyle Width="180px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NonCompliance TaskId" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNonComplianceTaskId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                                Text='<%# Eval("NonComplianceTaskId") %>'>


                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtNonComplianceTaskId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                                ReadOnly="true" Text='<%# Bind("NonComplianceTaskId") %>'>

                                                            </asp:TextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="20px" />
                                                        <ItemStyle Width="180px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblComplianceTaskName" runat="server" Text='<%# Eval("ComplianceTaskName") %>'>

                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtComplianceTaskName" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                                                Text='<%# Bind("ComplianceTaskName") %>' Width="250px">

                                                            </asp:TextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="250px" />
                                                        <ItemStyle Width="250px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is  Active">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkTaskActive" runat="server" Checked='<%# Eval("IsActive") %>'
                                                                Enabled="false" Font-Names="Verdana" Font-Size="XX-Small" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkTaskIsActive" runat="server" Checked='<%# Bind("IsActive") %>'
                                                                Font-Names="Verdana" Font-Size="XX-Small" />
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                                                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="false" Visible="False" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </asp:Panel>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </asp:Panel>
        </asp:Panel>
        <table>
            <tr>
                <td>
                    <asp:HiddenField ID="HidDeleteCount" runat="server" Value="0" />
                </td>
                <td>
                    <asp:HiddenField ID="HidUpdateCount" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="HidDocDeleteCount" runat="server" Value="0" />
                </td>
                <td>
                    <asp:HiddenField ID="HidDocUpdateCount" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hidTaskDeeteCount" runat="server" Value="0" />
                </td>
                <td>
                    <asp:HiddenField ID="HidTaskUpdateCount" runat="server" Value="0" />
                </td>
            </tr>
        </table>
    </div>
    <%--<br />--%>
</asp:Content>
