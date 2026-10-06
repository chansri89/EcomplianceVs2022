<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="NonComplianceAction.aspx.cs" Inherits="NonComplianceAction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="pnlActivityAction" runat="server" Height="881px">
        <asp:Panel ID="pnlCompany" runat="server" Height="26px" Width="870px">
            <table style="width: 100%; height: 26px;">
                <tr>
                    <td style="width: 118px; text-align: left;">
                        <asp:Label ID="lblCompanyName" runat="server" Text="Location Name" Font-Names="arial"
                            Font-Size="X-Small"></asp:Label>
                    </td>
                    <td style="width: 99px" class="style21">
                        <asp:DropDownList ID="ddlCompanyName" runat="server" Font-Names="arial" 
                            Font-Size="X-Small">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 42px; text-align: left;">
                        <asp:Label ID="lblActName" runat="server" Text="Act" Font-Names="arial" 
                            Font-Size="X-Small"></asp:Label>
                    </td>
                    <td style="width: 170px">
                        <asp:DropDownList ID="ddlActName" runat="server" Font-Names="arial" 
                            Font-Size="X-Small">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnGo" runat="server" Text="Go" Font-Names="arial" Font-Size="X-Small"
                            OnClick="btnGo_Click" />
                    </td>
                    <td align="right" style="text-align: right">
                        <asp:Button ID="btnCompActivitiesHide" runat="server" Text="- Collapse" Width="70px"
                            BackColor="#00006A" Height="17px" Font-Names="arial" Font-Size="X-Small" Visible="False"
                            ForeColor="White" Style="margin-left: 49px" OnClick="btnCompActivitiesHide_Click" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
         <asp:Label ID="lblMsg" runat="server" Font-Names="arial" 
                Font-Size="X-Small" ForeColor="#0033CC" 
                Text="Select Company & Act Name then Click Go" Visible="false"></asp:Label>
             <br />
      <%--  <asp:panel ID="pnlGrdCompActivities" runat="server" Height="237px" 
            Width="1048px">--%>
            <asp:Panel ID="pnlGridCompActivities" runat="server" Width="1021px" 
                Height="261px">
                <div style="width: 963px; height: 219px; overflow:auto">
                    <asp:GridView ID="GrdCompActivities" runat="server" AutoGenerateColumns="False" BackColor="White"
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="arial"
                        Font-Size="X-Small" GridLines="Vertical" Height="16px" Width="927px" 
                        style="margin-bottom: 0px" onrowdatabound="GrdCompActivities_RowDataBound" 
                        onrowcommand="GrdCompActivities_RowCommand" 
                        onselectedindexchanged="GrdCompActivities_SelectedIndexChanged">
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:ButtonField CommandName="Select" HeaderText="Select" Text="Select">
                                <HeaderStyle Height="20px" />
                                <ItemStyle Width="20px" />
                            </asp:ButtonField>
                            <asp:TemplateField HeaderText="ActId" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblActId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        Text='<%# Eval("ActId") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtActId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        ReadOnly="true" Text='<%# Bind("ActId") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ActivityId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        Text='<%# Eval("ActivityId") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtActivityId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        ReadOnly="true" Text='<%# Bind("ActivityId") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ActivityForCompanyId" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityForCompanyId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        Text='<%# Eval("ActivityForCompanyId") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtActivityForCompanyId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        ReadOnly="true" Text='<%# Bind("ActivityForCompanyId") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="ActivityActionId" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityActionId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        Text='<%# Eval("ActivityActionId") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtActivityActionId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        ReadOnly="true" Text='<%# Bind("ActivityActionId") %>'>
S                                    </asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Non Compliance Available Task" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblNonComplianceAvailable" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        Text='<%# Eval("NonComplianceTaskAvailableFlag") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNonComplianceAvailable" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        ReadOnly="true" Text='<%# Bind("NonComplianceTaskAvailableFlag") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Act">
                                <ItemTemplate>
                                    <asp:Label ID="lblActName" runat="server" Text='<%# Eval("ActName") %>' ToolTip="Click Select For Creating Action Compeltion"></asp:Label></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtActName" runat="server" Text='<%# Bind("ActName") %>'
                                        Font-Names="arial" ReadOnly="true" Font-Size="X-Small" Width="180px"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="180px" />
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Activity Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("ActivityName") %>' ToolTip="Click Select For Creating Action Compeltion"></asp:Label></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtActivityName" runat="server" Text='<%# Bind("ActivityName") %>'
                                        Font-Names="arial" ReadOnly="true" Font-Size="X-Small" Width="180px"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="180px" />
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Reminder Year">
                                <ItemTemplate>
                                    <asp:Label ID="lblReminderYear" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        Text='<%# Eval("ReminderYear") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlReminderMonth" runat="server" DataValueField="DueYear" DataTextField="YearName"
                                        DataSource='<%#LoadReminYear() %>' Font-Names="arial" Font-Size="X-Small" Width="10px">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="10px" />
                                <ItemStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reminder Month">
                                <ItemTemplate>
                                    <asp:Label ID="lblReminderMonth" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        Text='<%# Eval("ReminderMonth") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlReminderMonth" runat="server" DataValueField="Month" DataTextField="MonthName"
                                        DataSource='<%#LoadMonth() %>' Font-Names="arial" Font-Size="X-Small" Width="10px">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="10px" />
                                <ItemStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reminder Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblReminderDate" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        Text='<%# Eval("ReminderDate") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlReminderDate" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        Width="10px">
                                        <asp:ListItem>01</asp:ListItem>
                                        <asp:ListItem>02</asp:ListItem>
                                        <asp:ListItem>03</asp:ListItem>
                                        <asp:ListItem>04</asp:ListItem>
                                        <asp:ListItem>05</asp:ListItem>
                                        <asp:ListItem>06</asp:ListItem>
                                        <asp:ListItem>07</asp:ListItem>
                                        <asp:ListItem>08</asp:ListItem>
                                        <asp:ListItem>09</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>11</asp:ListItem>
                                        <asp:ListItem>12</asp:ListItem>
                                        <asp:ListItem>13</asp:ListItem>
                                        <asp:ListItem>14</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>16</asp:ListItem>
                                        <asp:ListItem>17</asp:ListItem>
                                        <asp:ListItem>18</asp:ListItem>
                                        <asp:ListItem>19</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>21</asp:ListItem>
                                        <asp:ListItem>22</asp:ListItem>
                                        <asp:ListItem>23</asp:ListItem>
                                        <asp:ListItem>24</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>26</asp:ListItem>
                                        <asp:ListItem>27</asp:ListItem>
                                        <asp:ListItem>28</asp:ListItem>
                                        <asp:ListItem>29</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>31</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reminder Day">
                                <ItemTemplate>
                                    <asp:Label ID="lblReminderDay" runat="server" Text='<%# Eval("ReminderDay") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlReminderDay" runat="server" Font-Names="arial" Font-Size="X-Small">
                                        <asp:ListItem Value="1">Monday</asp:ListItem>
                                        <asp:ListItem Value="2">TuesDay</asp:ListItem>
                                        <asp:ListItem Value="3">WednesDay</asp:ListItem>
                                        <asp:ListItem Value="4">ThursDay</asp:ListItem>
                                        <asp:ListItem Value="5">FriDay</asp:ListItem>
                                        <asp:ListItem Value="6">SaturDay</asp:ListItem>
                                        <asp:ListItem Value="7">SunDay</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trig Month" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrigMonth" runat="server" Text='<%# Eval("TriggerMonth") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTrigMonth" runat="server" Font-Names="arial" DataSource="<%#LoadMonth() %>"
                                        DataTextField="MonthName" DataValueField="Month" Font-Size="X-Small">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="10px" />
                                <ItemStyle Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trig Date" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrigDate" runat="server" Text='<%# Eval("TriggerDate") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTrigDate" runat="server" Font-Names="arial" Font-Size="X-Small">
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
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trig Day" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblTrigDay" runat="server" Text='<%# Eval("TriggerDay") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlTrigDay" runat="server" Font-Names="arial" Font-Size="X-Small">
                                        <asp:ListItem Value="1">Monday</asp:ListItem>
                                        <asp:ListItem Value="2">TuesDay</asp:ListItem>
                                        <asp:ListItem Value="3">WednesDay</asp:ListItem>
                                        <asp:ListItem Value="4">ThursDay</asp:ListItem>
                                        <asp:ListItem Value="5">FriDay</asp:ListItem>
                                        <asp:ListItem Value="6">SaturDay</asp:ListItem>
                                        <asp:ListItem Value="7">SunDay</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Execution Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblExecutionEmployeeCode" runat="server" Text='<%# Eval("ExecutionEmployeeCode") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                      <asp:TextBox ID="txtExecutionEmployeeCode" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        ReadOnly="true" Text='<%# Bind("ExecutionEmployeeCode") %>'></asp:TextBox>

                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Execution Person">
                                <ItemTemplate>
                                    <asp:Label ID="lblExecutionPerson" runat="server" Text='<%# Eval("ExecutionEmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlExecutionPerson" runat="server" Font-Names="arial" Font-Size="X-Small"
                                        DataValueField="EmployeeCode" DataTextField="EmployeeName" DataSource='<%#getExecutionPerson() %>'>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Review Person">
                                <ItemTemplate>
                                    <asp:Label ID="lblReviewPerson" runat="server" Text='<%# Eval("ReviewEmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlReviewPerson" runat="server" DataSource="<%#getReviewPerson() %>"
                                        DataTextField="EmployeeName" DataValueField="EmployeeCode" Font-Names="arial"
                                        Font-Size="X-Small">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Head Person">
                                <ItemTemplate>
                                    <asp:Label ID="lblHeadPerson" runat="server" Text='<%# Eval("HeadEmployeeName") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlHeadPerson" runat="server" DataSource="<%#getHeadPerson() %>"
                                        DataTextField="EmployeeName" DataValueField="EmployeeCode" Font-Names="arial"
                                        Font-Size="X-Small">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="30px" />
                                <ItemStyle Width="30px" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Utimate" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblUtimate" runat="server" Text='<%# Eval("UltimateEmployeeName") %>'>

                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                   <asp:DropDownList ID="ddlUtimate" runat="server" >
                                    DataSource="<%#getUtimate() %>" DataTextField="UltimateEmployeeCode" 
                    DataValueField="UltimateEmployeeCode" Font-Names="arial" Font-Size="X-Small">
                                   </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="80px" />
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Frequencey">
                                <ItemTemplate>
                                    <asp:Label ID="lblFrequencey" runat="server" Text='<%# Eval("FrequencyName") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlFrequencey" runat="server" DataSource="<%#getFrequencey() %>"
                                        DataTextField="FrequencyName" DataValueField="FrequencyId" Font-Names="arial"
                                        Font-Size="X-Small">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle Width="20px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is State Specific">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsStateSpecific" runat="server" Checked='<%# Eval("IsStateSpecific") %>' Font-Names="arial"
                                        Font-Size="X-Small" Enabled="false"></asp:CheckBox>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkStateSpecific" runat="server" Checked='<%# Bind("IsStateSpecific") %>' Font-Names="arial"
                                        Font-Size="X-Small"></asp:CheckBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Is Regular Activity">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkIsRegularActivity" runat="server" Checked='<%# Eval("IsRegularActivity") %>' Font-Names="arial"
                                        Font-Size="X-Small" Enabled="false"></asp:CheckBox>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chkRegularActivity" runat="server" Checked='<%# Bind("IsRegularActivity") %>' Font-Names="arial"
                                        Font-Size="X-Small"></asp:CheckBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="20px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                          <asp:TemplateField HeaderText="CompletedDate" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblCompletedDate" runat="server" Text='<%# Eval("CompletedDate") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                             
                                <HeaderStyle Width="180px" />
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                             
                                <HeaderStyle Width="180px" />
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>

                            <asp:CommandField HeaderText="Edit" ShowEditButton="True" Visible="False">
                                <HeaderStyle Width="90px" />
                                <ItemStyle Width="90px" />
                            </asp:CommandField>
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" Visible="False" />
                        </Columns>
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </div>
            </asp:Panel>
    <asp:Panel ID="pnlRoocause" runat="server" Visible="False" >
    
    <table>
    <tr>
    <td style="width: 187px">
    <asp:Panel ID="pnlSave" runat="server" Height="174px" Visible="False" Width="387px">
        <table style="width: 77%; height: 138px;">
         <tr>
                <td style="text-align: left; width: 105px;">
                    <asp:Label ID="lblRootCause" runat="server" Text="Root Cause"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label></td>
                <td style="width: 40px">
                    <asp:TextBox ID="txtRootCause" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: left; width: 105px;">
                    <asp:Label ID="lblCorrectiveAction" runat="server" Text="Corrective Action"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label></td>
                <td style="width: 40px">
                    <asp:TextBox ID="txtCorrectiveAction" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="text-align: left; width: 105px;">
                   <asp:Label ID="lblPreventiveAction" runat="server" Text="Preventive Action"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                <td style="width: 40px">
                    <asp:TextBox ID="txtPreventiveAction" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox></td>
            </tr>
           
            <tr>
                <td style="text-align: left; width: 105px;">
                    <asp:Label ID="lblApprovedBy" runat="server" Text="Approved By"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label></td>
                <td style="width: 40px">
                    <asp:TextBox ID="txtApprovedBy" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" MaxLength="8"></asp:TextBox></td>
            </tr>
            <tr>
            <td style="width: 105px; text-align: left;">
            <asp:TextBox ID="txtActivityActionId" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Visible="False" Height="18px" Width="46px"></asp:TextBox>
                       
                        <asp:TextBox ID="txtActivityForCompanyId" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Visible="False" Height="18px" Width="46px"></asp:TextBox></td>
                <td style="width: 40px">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Font-Names="arial" 
                        Font-Size="X-Small" onclick="btnSave_Click" />
                   </td>
                  
            </tr>
            <%--<tr>
                <td class="style23" style="width: 135px; text-align: left;">
                    <asp:HiddenField ID="HidDeleteCount" runat="server" Value="0" />
                    <asp:HiddenField ID="HidUpdateCount" runat="server" Value="0" />
                </td>
            </tr>--%>
             
        </table>
    </asp:Panel>
    </td>
    <td style="width: 480px">
     <asp:Panel ID="pnlNonCompiance" runat="server" Height="181px" Width="525px">
                            <div style="height: 162px; width: 498px; margin-left: 0px; overflow:auto" >
                                <asp:GridView ID="GrdEDC" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"
                                    Height="16px" Width="476px"  
                                    Font-Names="arial" Font-Size="X-Small" 
                                    onrowediting="GrdDocument_RowEditing" 
                                    onrowcancelingedit="GrdEDC_RowCancelingEdit" onrowdeleting="GrdEDC_RowDeleting" 
                                    onrowupdating="GrdEDC_RowUpdating">                                   
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
    <Columns>
   <%--<asp:ButtonField CommandName="View" HeaderText="View Doc" Text="View" />--%>

            <asp:TemplateField HeaderText="ActivityNonComplianceTaskId" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblActivityNonComplianceTaskId" runat="server" Font-Names="arial" Font-Size="X-Small"
                        Text='<%# Eval("ActivityNonComplianceTaskId") %>'>
                    </asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="20px" />
                <ItemStyle Width="180px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="ActivityNonComplianceTaskMasterId" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="lblActivityNonComplianceTaskMasterId" runat="server" Font-Names="arial" Font-Size="X-Small"
                        Text='<%# Eval("ActivityNonComplianceTaskMasterId") %>'>
                    </asp:Label>
                </ItemTemplate>
                <HeaderStyle Width="20px" />
                <ItemStyle Width="180px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="ActivityActionId" Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblActivityActionId" runat="server" Font-Names="arial" Font-Size="X-Small"
                        Text='<%# Eval("ActivityActionId") %>'>
                    </asp:Label>
                </ItemTemplate>
                
                <HeaderStyle Width="20px" />
                <ItemStyle Width="180px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Compliance Task Name">
             <ItemTemplate>
                    <asp:Label ID="lblComplianceTaskName" runat="server" 
                        Text='<%# Eval("ComplianceTaskName") %>' Font-Names="arial" Font-Size="X-Small">
                    </asp:Label>                   
                    </ItemTemplate>
                    <EditItemTemplate>
                      <asp:TextBox ID="txtComplianceTaskName" runat="server" Text='<%# Bind("ComplianceTaskName") %>' Width="150px" ReadOnly="true" Font-Names="arial" Font-Size="X-Small"></asp:TextBox>
                      </EditItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="EDC">
                <ItemTemplate>
                <asp:Label ID="lblEDC" runat="server" Text='<%# Eval("ExpectedDateOfCompletion","{0:dd/MM/yyyy}") %>' Font-Names="arial" Font-Size="X-Small">
                    </asp:Label>
                      </ItemTemplate>
                       <EditItemTemplate>
                   <asp:TextBox ID="txtEDC" runat="server" Text='<%# Bind("ExpectedDateOfCompletion","{0:dd/MM/yyyy}") %>' Width="75px" Font-Names="arial" Font-Size="X-Small"></asp:TextBox>
                    <asp:ImageButton ID="ImgEDC" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <asp:CalendarExtender ID="cldEDC" TargetControlID="txtEDC" runat="server" PopupButtonID="ImgEDC" Format="dd/MM/yyyy"></asp:CalendarExtender>
              </EditItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Completed Date">
                <ItemTemplate>
                 <asp:Label ID="lblCompletedDate" runat="server" Text='<%# Eval("CompletedDate","{0:dd/MM/yyyy}") %>' Font-Names="arial" Font-Size="X-Small">
                    </asp:Label></ItemTemplate>
                     <EditItemTemplate>
                   <asp:TextBox ID="txtCompletedDate" runat="server" Text='<%# Bind("CompletedDate","{0:dd/MM/yyyy}") %>' Width="75px" Font-Size="X-Small" Font-Names="arial"></asp:TextBox>
                    <asp:ImageButton ID="ImgCompletedDate" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <asp:CalendarExtender ID="cldCompletedDate" TargetControlID="txtCompletedDate" runat="server" PopupButtonID="ImgCompletedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                </EditItemTemplate>
                <HeaderStyle Width="100px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="IsActive" Visible="false">
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
           <%-- <asp:ButtonField CommandName="Edit" HeaderText="Edit" Text="Edit" />--%>
                </Columns>
    </asp:GridView>
</div>
</asp:Panel>
    </td></tr></table>
    </asp:Panel>

       
        </asp:Panel>
</asp:Content>

