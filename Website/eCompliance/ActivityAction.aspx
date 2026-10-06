<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage3.master" AutoEventWireup="true"   CodeFile="ActivityAction.aspx.cs" Inherits="ActivityAction" %>
   
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
   
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblActivityAction" runat="server" Align= "center" Text="Activity Action"     Font ="arial" width="787px"
        Font-Size ="12pt" Font-Bold="True" Font-Names="arial"    style="text-align: center"></asp:Label>
<asp:Panel ID="pnlActivityAction" runat="server" Height="1063px">
    <asp:Panel ID="pnlCompany" runat="server" Height="28px" Width="870px">
    <table style="width: 99%; height: 19px;">
        <tr>
            <td style="width: 78px; text-align: left;">
                <asp:Label ID="lblCompanyName" runat="server" Text="Location Name" Font-Names="arial"
                    Font-Size="X-Small"></asp:Label>
            </td>
            <td style="width: 129px" class="style21">
                <asp:DropDownList ID="ddlCompanyName" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Height="16px" Width="122px">
                </asp:DropDownList>
            </td>
            <td style="width: 39px; text-align: left;">
                <asp:Label ID="lblActName" runat="server" Text="Act" Font-Names="arial" Font-Size="X-Small"></asp:Label>
            </td>
            <td style="width: 140px">
                <asp:DropDownList ID="ddlActName" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Height="16px" Width="371px">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnGo" runat="server" Text="Go" Font-Names="arial" Font-Size="X-Small"
                    OnClick="btnGo_Click" />
            </td>
            <td align="right" style="text-align: right">
                <%--<asp:Button ID="btnCompActivitiesHide" runat="server" Text="- Collapse" Width="70px"
                    BackColor="#00006A" Height="17px" Font-Names="arial" Font-Size="X-Small" Visible="False"
                    ForeColor="White" Style="margin-left: 49px" OnClick="btnCompActivitiesHide_Click" />--%>
            </td>
        </tr>
    </table>
</asp:Panel>
    <asp:Panel ID="PnlSearch" runat="server" Height="30px" Width="870px" Visible="true">
    <table style="width: 100%; height: 35px;">
        <tr>
             <td style="width: 75px; text-align: left;">
                    <asp:Label ID="lblActivityNameFiter" runat="server" Text="Activity Name"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="false"></asp:Label>
                </td>
                <td style="width: 124px">
                    <asp:TextBox ID="txtActivityNameFilter" runat="server" Font-Names="arial" 
                              Font-Size="X-Small" Width="333px" Height="18px"></asp:TextBox>
                </td>
                    
            <td class="style22" style="width: 58px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Names="arial" 
                    Font-Size="X-Small" Height="21px" onclick="btnSearch_Click"/>
            </td>
            <td>
                <asp:Button ID="btnSearchClear" runat="server" Text="Clear Search" Font-Names="arial" 
                    Font-Size="X-Small" Height="21px" 
                    Width="87px" onclick="btnSearchClear_Click"/>
            </td>
        </tr>
    </table>
</asp:Panel>
<asp:Panel ID="PnlColor" runat="server" Height="30px" Width="870px" Visible="true">
    <table>
    <tr>
<td>
<asp:Label ID="lblActivityGrdColorMsg" runat="server" Font-Names="arial" Font-Size="X-Small" ForeColor="#0033CC" 
        Text="Blue --&gt; Action to be Completed" Visible="true"></asp:Label>
</td>
 <td style="width: 30px;"></td>
            <td>
                <asp:Label ID="lblActivityGrdColorgreen" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" ForeColor="#009900" 
                    Text="Green --> Action Completed" Visible="true"></asp:Label>
            </td>
            
<td style="width: 30px;"></td>
          
            <td>
                <asp:Label ID="lblActionColorRed" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" ForeColor="Red" 
                    Text="Red --> Pending for Compliance" Visible="true"></asp:Label>
            </td>
            <td style="width: 30px;"></td>
            <td>
                <asp:Label ID="lblActionColorViolet" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" ForeColor="Violet"
                    Text="Violet --> Document Not Stored" Visible="true" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
  </asp:Panel> 
    <table id="tblTitle" runat="server" visible="true">
    <tr>
        <td style="width: 846px; text-align: right;">
        <asp:TextBox ID="txtTitle" runat="server" Text="Activity Action Details            -------------- To Expand or collapse the below grid  Click +/- button --->" 
                Width="994px" ReadOnly="true"
                style="text-align: left" BackColor="#003366" ForeColor="White" 
                Font-Bold="true" Font-Size="X-Small" Height="16px" Visible="true"></asp:TextBox>
        </td>
            <td>
            <asp:Button ID="btnCollapse" Text="-" runat="server" Width="28px" 
                    Font-Size="X-Small" onclick="btnCollapse_Click" Visible="true"/>
            </td>
    </tr>
</table>
    <asp:Panel ID="pnlGrdCompActivities" runat="server" Height="215px" 
        Width="1163px">
   <%-- <asp:Panel ID="pnlGridCompActivities" runat="server" Width="1031px"  Height="203px"> --%>
       
        <div style="width: 1155px; height: 190px; overflow:auto">
            <asp:GridView ID="GrdCompActivities" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="arial"
                Font-Size="X-Small" GridLines="Vertical" Height="16px" Width="1003px" OnRowCommand="GrdCompActivities_RowCommand"
               style="margin-bottom: 0px">
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>  <%--OnRowDataBound="GrdCompActivities_RowDataBound" --%>
                    <asp:ButtonField CommandName="Select" HeaderText="Select" Text="Select">
                        <HeaderStyle Height="20px" />   <ItemStyle Width="20px" />
                    </asp:ButtonField>
                    <asp:TemplateField HeaderText="ActId" Visible="False">
                        <ItemTemplate>  <asp:Label ID="lblActId" runat="server" Font-Names="arial" Font-Size="X-Small"  Text='<%# Eval("ActId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </asp:Label>    </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtActId" runat="server" Font-Names="arial" Font-Size="X-Small"          ReadOnly="true" Text='<%# Bind("ActId") %>'>&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;     
                        </asp:TextBox>
                        </EditItemTemplate>     <HeaderStyle Width="20px" />     <ItemStyle Width="180px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ActivityId" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblActivityId" runat="server" Font-Names="arial" Font-Size="X-Small"       Text='<%# Eval("ActivityId") %>'>&nbsp;&nbsp;&nbsp;    </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>  <asp:TextBox ID="txtActivityId" runat="server" Font-Names="arial" Font-Size="X-Small"   ReadOnly="true" Text='<%# Bind("ActivityId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                         </asp:TextBox>
                        </EditItemTemplate>     <HeaderStyle Width="20px" />     <ItemStyle Width="180px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ActivityForCompanyId" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblActivityForCompanyId" runat="server" Font-Names="arial" Font-Size="X-Small"  Text='<%# Eval("ActivityForCompanyId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:Label>  </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtActivityForCompanyId" runat="server" Font-Names="arial" Font-Size="X-Small" ReadOnly="true" Text='<%# Bind("ActivityForCompanyId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </EditItemTemplate>  <HeaderStyle Width="20px" />  <ItemStyle Width="180px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ActivityActionId" Visible="true">
                        <ItemTemplate>
                            <asp:Label ID="lblActivityActionId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                Text='<%# Eval("ActivityActionId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtActivityActionId" runat="server" Font-Names="arial" Font-Size="X-Small" ReadOnly="true" Text='<%# Bind("ActivityActionId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:TextBox>
                        </EditItemTemplate>   <HeaderStyle Width="20px" />   <ItemStyle Width="180px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Non Compliance Available Task" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblNonComplianceAvailable" runat="server" Font-Names="arial" Font-Size="X-Small"  Text='<%# Eval("NonComplianceTaskAvailableFlag") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNonComplianceAvailable" runat="server" Font-Names="arial" Font-Size="X-Small"   ReadOnly="true" Text='<%# Bind("NonComplianceTaskAvailableFlag") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                           </asp:TextBox>
                        </EditItemTemplate>       <HeaderStyle Width="20px" />   <ItemStyle Width="180px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Act"><%--Changed by Abinayaa 08112 after 071112 Meeting--%>
                        <ItemTemplate>
                            <asp:Label ID="lblActName" runat="server" Text='<%# Eval("ActName") %>' ToolTip="Click Select For Creating Action Compeltion"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtActName" runat="server" Text='<%# Bind("ActName") %>'
                                Font-Names="arial" ReadOnly="true" Font-Size="X-Small" Width="180px"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="180px" />     <ItemStyle Width="180px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Compliance Item"><%--Changed by Abinayaa 08112 after 071112 Meeting--%>
                        <ItemTemplate>
                            <asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("ActivityName") %>' ToolTip="Click Select For Creating Action Compeltion"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtActivityName" runat="server" Text='<%# Bind("ActivityName") %>'
                                Font-Names="arial" ReadOnly="true" Font-Size="X-Small" Width="180px"></asp:TextBox>
                        </EditItemTemplate>           <HeaderStyle Width="180px" />   <ItemStyle Width="180px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Location Code">
                            <ItemTemplate>   <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>' ></asp:Label></ItemTemplate>
                            <HeaderStyle Width="25px" />       <ItemStyle Width="25px" />
                    </asp:TemplateField>

                        <asp:TemplateField HeaderText="Due Year">
                        <ItemTemplate>
                            <asp:Label ID="lblReminderYear" runat="server" Font-Names="arial" Font-Size="X-Small"  Text='<%# Eval("ReminderYear") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlReminderMonth" runat="server" DataValueField="DueYear" DataTextField="YearName"
                                DataSource='<%#LoadReminYear() %>' Font-Names="arial" Font-Size="X-Small" Width="10px">
                            </asp:DropDownList>
                        </EditItemTemplate>       <HeaderStyle Width="10px" />  <ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Due Month">
                        <ItemTemplate>
                            <asp:Label ID="lblReminderMonth" runat="server" Font-Names="arial" Font-Size="X-Small" Text='<%# Eval("ReminderMonth") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlReminderMonth" runat="server" DataValueField="Month" DataTextField="MonthName"
                                DataSource='<%#LoadMonth() %>' Font-Names="arial" Font-Size="X-Small" Width="10px">
                            </asp:DropDownList>
                        </EditItemTemplate>  <HeaderStyle Width="10px" />    <ItemStyle Width="10px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Due Date">
                        <ItemTemplate>
                            <asp:Label ID="lblReminderDate" runat="server" Font-Names="arial" Font-Size="X-Small"  Text='<%# Eval("ReminderDate") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlReminderDate" runat="server" Font-Names="arial" Font-Size="X-Small"    Width="10px">
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
                    <asp:TemplateField HeaderText="Due Day">
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
                            <asp:Label ID="lblTrigMonth" runat="server" Text='<%# Eval("TriggerMonth") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTrigMonth" runat="server" Font-Names="arial" DataSource="<%#LoadMonth() %>"
                                DataTextField="MonthName" DataValueField="Month" Font-Size="X-Small">
                                <%-- <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
<asp:ListItem Value="2" Text="Feb"></asp:ListItem>
<asp:ListItem Value="3" Text="Mar"></asp:ListItem>
<asp:ListItem Value="4" Text="Apr"></asp:ListItem>
<asp:ListItem Value="5" Text="May"></asp:ListItem>
<asp:ListItem Value="6" Text="Jun"></asp:ListItem>
<asp:ListItem Value="7" Text="Jul"></asp:ListItem>
<asp:ListItem Value="8" Text="Aug"></asp:ListItem>
<asp:ListItem Value="9" Text="Sep"></asp:ListItem>
<asp:ListItem Value="10" Text="Oct"></asp:ListItem>
<asp:ListItem Value="11" Text="Nov"></asp:ListItem>
<asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                --%>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <HeaderStyle Width="10px" />
                        <ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trig Date" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblTrigDate" runat="server" Text='<%# Eval("TriggerDate") %>'  Visible="false"></asp:Label>
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
                            <asp:Label ID="lblTrigDay" runat="server" Text='<%# Eval("TriggerDay") %>'  Visible="false"></asp:Label>
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
                    <asp:TemplateField HeaderText="Responsable Person"><%--Changed by Abinayaa 08112 after 071112 Meeting--%>
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
                    <asp:TemplateField HeaderText="Reporting Officer"><%--Changed by Abinayaa 08112 after 071112 Meeting--%>
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
                    <asp:TemplateField HeaderText="Reviewing Officer"><%--Changed by Abinayaa 08112 after 071112 Meeting--%>
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
                    <asp:TemplateField HeaderText="Company Head" Visible="true"><%--added by Abinayaa 08112 after 071112 Meeting--%>
                        <ItemTemplate>
                            <asp:Label ID="lblUtimate" runat="server" Text='<%# Eval("UltimateEmployeeName") %>'>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                    
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlUtimate" runat="server" 
                            DataSource="<%#getUtimate() %>" DataTextField="UltimateEmployeeCode" 
                            DataValueField="UltimateEmployeeCode" Font-Names="arial" Font-Size="X-Small">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <HeaderStyle Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Frequencey">
                        <ItemTemplate>
                            <asp:Label ID="lblFrequencey" runat="server" Text='<%# Eval("FrequencyName") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             
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
                    <asp:TemplateField HeaderText="Completed Date">
                        <ItemTemplate>
                            <asp:Label ID="lblCompletDate" runat="server"  Text='<%# Eval("CompletedDate","{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCompletDate" runat="server" Text='<%# Bind("CompletedDate") %>'
                                Font-Names="arial" ReadOnly="true" Font-Size="X-Small" Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" />
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
                    <asp:TemplateField HeaderText="Is Regular Activity" Visible="false">
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  
                            </asp:Label>
                        </ItemTemplate>
                             
                        <HeaderStyle Width="180px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  
                            </asp:Label>
                        </ItemTemplate>
                             
                        <HeaderStyle Width="180px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
 <%-- ------------------------------- added by Abinayaa 040213--Check doc is available or not ---------------------------------------------------------------------------------%>
                     <asp:TemplateField HeaderText="docAvbl" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lbldocAvbl" runat="server" Text='<%# Eval("docAvbl") %>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                  
                            </asp:Label>
                        </ItemTemplate>                             
                        <HeaderStyle Width="40px" />
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="IsdocReq" Visible="true"> <%--SCS070116--%>
                        <ItemTemplate>
                            <asp:Label ID="lblIsdocReq" runat="server" Text='<%# Eval("IsdocReq") %>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                  
                            </asp:Label>
                        </ItemTemplate>                             
                        <HeaderStyle Width="40px" />
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
  <%--  ------------------------------ added by Abinayaa 040213--Check doc is available or not ----------------------------------------------------------------------------------%>
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
   <%-- </asp:Panel>--%>

</asp:Panel>       
    <asp:Panel ID="pnlasonwhen" runat="server" Height="30px" Width="1046px" >
        <table>
        <tr>
        <td align="right" style="width: 1025px; text-align: left;">
      <asp:TextBox ID="txtasonwhen" runat="server" Text="As and When Details" 
                Width="994px" ReadOnly="true"
                style="text-align: left" BackColor="#003366" ForeColor="White" 
                Font-Bold="true" Font-Size="X-Small" Height="16px" Visible="true"></asp:TextBox>
        </td>
        </tr>
        </table>
</asp:Panel>   
    <asp:Panel ID="pnlGrdAsonwhen" runat="server" Width ="1080px" 
        Height="180px">
        <div id="divGrdAswhen" runat="server" 
            style="width: 1071px; height: 177px; overflow:auto">
            <asp:GridView ID="GrdAsonWhen" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Names="arial"
                Font-Size="X-Small" GridLines="Vertical" Height="16px" Width="1057px"  
                style="margin-bottom: 0px" onrowcommand="GrdAsonWhen_RowCommand" 
                >
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" /><%--OnRowCommand="GrdAsonWhen_RowCommand"
                OnRowDataBound="GrdAsonWhen_RowDataBound"--%> <%--onrowdatabound="GrdAsonWhen_RowDataBound"--%>
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

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                    
                        </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtActId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                ReadOnly="true" Text='<%# Bind("ActId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    
                        </asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="20px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ActivityId" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblActivityId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                Text='<%# Eval("ActivityId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtActivityId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                ReadOnly="true" Text='<%# Bind("ActivityId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  
                            </asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="20px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ActivityForCompanyId" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblActivityForCompanyId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                Text='<%# Eval("ActivityForCompanyId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                   
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtActivityForCompanyId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                ReadOnly="true" Text='<%# Bind("ActivityForCompanyId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            
                            </asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="20px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="ActivityActionId" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblActivityActionId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                Text='<%# Eval("ActivityActionId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtActivityActionId" runat="server" Font-Names="arial" Font-Size="X-Small"
                                ReadOnly="true" Text='<%# Bind("ActivityActionId") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  
                            </asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="20px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Non Compliance Available Task" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblNonComplianceAvailable" runat="server" Font-Names="arial" Font-Size="X-Small"
                                Text='<%# Eval("NonComplianceTaskAvailableFlag") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtNonComplianceAvailable" runat="server" Font-Names="arial" Font-Size="X-Small"
                                ReadOnly="true" Text='<%# Bind("NonComplianceTaskAvailableFlag") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
              
                            </asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="20px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Act"><%--Changed by Abinayaa 08112 after 071112 Meeting--%>
                        <ItemTemplate>
                            <asp:Label ID="lblActName" runat="server" Text='<%# Eval("ActName") %>' ToolTip="Click Select For Creating Action Compeltion"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtActName" runat="server" Text='<%# Bind("ActName") %>'
                                Font-Names="arial" ReadOnly="true" Font-Size="X-Small" Width="180px"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="180px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Compliance Item"><%--Changed by Abinayaa 08112 after 071112 Meeting--%>
                        <ItemTemplate>
                            <asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("ActivityName") %>' ToolTip="Click Select For Creating Action Compeltion"></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtActivityName" runat="server" Text='<%# Bind("ActivityName") %>'
                                Font-Names="arial" ReadOnly="true" Font-Size="X-Small" Width="180px"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="180px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Location Code">
                            <ItemTemplate>
                        <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>' ></asp:Label></ItemTemplate>
                            <HeaderStyle Width="25px" />
                                <ItemStyle Width="25px" />
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Due Year">
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
                    <asp:TemplateField HeaderText="Due Month">
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
                    <asp:TemplateField HeaderText="Due Date">
                        <ItemTemplate>
                            <asp:Label ID="lblReminderDate" runat="server" Font-Names="arial" Font-Size="X-Small"
                                Text='<%# Eval("ReminderDate") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  
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
                    <asp:TemplateField HeaderText="Due Day">
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
                            <asp:Label ID="lblTrigMonth" runat="server" Text='<%# Eval("TriggerMonth") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlTrigMonth" runat="server" Font-Names="arial" DataSource="<%#LoadMonth() %>"
                                DataTextField="MonthName" DataValueField="Month" Font-Size="X-Small">
                                <%-- <asp:ListItem Value="1" Text="Jan"></asp:ListItem>
<asp:ListItem Value="2" Text="Feb"></asp:ListItem>
<asp:ListItem Value="3" Text="Mar"></asp:ListItem>
<asp:ListItem Value="4" Text="Apr"></asp:ListItem>
<asp:ListItem Value="5" Text="May"></asp:ListItem>
<asp:ListItem Value="6" Text="Jun"></asp:ListItem>
<asp:ListItem Value="7" Text="Jul"></asp:ListItem>
<asp:ListItem Value="8" Text="Aug"></asp:ListItem>
<asp:ListItem Value="9" Text="Sep"></asp:ListItem>
<asp:ListItem Value="10" Text="Oct"></asp:ListItem>
<asp:ListItem Value="11" Text="Nov"></asp:ListItem>
<asp:ListItem Value="12" Text="Dec"></asp:ListItem>
                                --%>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <HeaderStyle Width="10px" />
                        <ItemStyle Width="10px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Trig Date" Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblTrigDate" runat="server" Text='<%# Eval("TriggerDate") %>'  Visible="false"></asp:Label>
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
                            <asp:Label ID="lblTrigDay" runat="server" Text='<%# Eval("TriggerDay") %>'  Visible="false"></asp:Label>
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
                    <asp:TemplateField HeaderText="Responsable Person"><%--Changed by Abinayaa 08112 after 071112 Meeting--%>
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
                    <asp:TemplateField HeaderText="Reporting Officer"><%--Changed by Abinayaa 08112 after 071112 Meeting--%>
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
                    <asp:TemplateField HeaderText="Reviewing Officer"><%--Changed by Abinayaa 08112 after 071112 Meeting--%>
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
                    <asp:TemplateField HeaderText="Company Head" Visible="true"><%--added by Abinayaa 08112 after 071112 Meeting--%>
                        <ItemTemplate>
                            <asp:Label ID="lblUtimate" runat="server" Text='<%# Eval("UltimateEmployeeName") %>'>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                    
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlUtimate" runat="server" 
                            DataSource="<%#getUtimate() %>" DataTextField="UltimateEmployeeCode" 
                            DataValueField="UltimateEmployeeCode" Font-Names="arial" Font-Size="X-Small">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <HeaderStyle Width="80px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Frequencey">
                        <ItemTemplate>
                            <asp:Label ID="lblFrequencey" runat="server" Text='<%# Eval("FrequencyName") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
             
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlFrequencey" runat="server" DataSource="<%#getFrequencey() %>"
                                DataTextField="FrequencyName" DataValueField="FrequencyId" Font-Names="arial"
                                Font-Size="X-Small">
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <HeaderStyle Width="40px" />
                        <ItemStyle Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Completed Date">
                        <ItemTemplate>
                            <asp:Label ID="lblCompletDate" runat="server"  Text='<%# Eval("CompletedDate","{0:dd/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCompletDate" runat="server" Text='<%# Bind("CompletedDate") %>'
                                Font-Names="arial" ReadOnly="true" Font-Size="X-Small" Width="60px"></asp:TextBox>
                        </EditItemTemplate>
                        <HeaderStyle Width="60px" />
                        <ItemStyle Width="60px" />
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
                    <asp:TemplateField HeaderText="Is Regular Activity" Visible="false">
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  
                            </asp:Label>
                        </ItemTemplate>
                             
                        <HeaderStyle Width="180px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  
                            </asp:Label>
                        </ItemTemplate>
                             
                        <HeaderStyle Width="180px" />
                        <ItemStyle Width="180px" />
                    </asp:TemplateField>
    <asp:TemplateField HeaderText="docAvbl" Visible="false">
     <ItemTemplate> <asp:Label ID="lbldocAvbl" runat="server" Text='<%# Eval("docAvbl") %>'> </asp:Label>  </ItemTemplate>                             
      <HeaderStyle Width="40px" /><ItemStyle Width="40px" />
     </asp:TemplateField>
  <asp:TemplateField HeaderText="IsdocReq" Visible="true"> <%--SCS070116--%>
                        <ItemTemplate>
                            <asp:Label ID="lblIsdocReq" runat="server" Text='<%# Eval("IsdocReq") %>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                  
                            </asp:Label>
                        </ItemTemplate>                             
                        <HeaderStyle Width="40px" />
                        <ItemStyle Width="40px" />
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

    <asp:Panel ID="pnlCompanyAct" runat="server" Width="968px" Visible="False" 
    BorderWidth="1px" Height="50px">
    <table style="width: 99%; height: 52px;">
        <tr>
            <td class="style21" style="text-align: left; ">
                <asp:Label ID="lblCmpName" runat="server" Text="Location Name" Font-Names="arial"
                    Font-Size="X-Small" Font-Bold="True" style="text-align: left"></asp:Label>
            </td>
            <td style="width: 89px">
                <asp:TextBox ID="txtCmpName" runat="server" Font-Names="arial" Font-Size="X-Small"
                    ReadOnly="true" Width="142px" Font-Bold="True" Height="16px"></asp:TextBox>
            </td>
            <td style="width: 43px; text-align: left;">
                <asp:Label ID="lbAct" runat="server" Text="Act" Font-Names="arial" Font-Size="X-Small"
                    Font-Bold="True"></asp:Label>
            </td>
            <td style="width: 211px">
                <asp:TextBox ID="txtAct" runat="server" Width="207px" Font-Names="arial" Font-Size="X-Small"
                    ReadOnly="true" Font-Bold="True" Height="16px"></asp:TextBox>
            </td>
            <td style="width: 100px; text-align: right;" class="style21">
                <asp:Label ID="lblActivities" runat="server" Text="Activity Name" Font-Names="arial"
                    Font-Size="X-Small" Font-Bold="True"></asp:Label>
            </td>
            <td style="width: 220px">
                <asp:TextBox ID="txtActivities" runat="server" Width="209px" Font-Names="arial"
                    Font-Size="X-Small" ReadOnly="true" Font-Bold="True"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="txtCompanyActivityId" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Visible="False" ReadOnly="True" Width="37px" Style="text-align: right"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="style21" style="text-align: left; " 
                id="tdlblExecutioner" runat="server" visible="false">
                <asp:Label ID="lblExecutioner" runat="server" Text="ExecutionerCode" Font-Names="arial"
                    Font-Size="X-Small" Visible="False"></asp:Label>
            </td>
            <td style="width: 89px" id="tdExecutioner" runat="server" visible="false">
                <asp:TextBox ID="txtExecutioner" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Visible="False" ReadOnly="True" Width="37px" Style="text-align: right"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 43px;" id="tdlblRemainderDate" 
                runat="server" visible="false">
                <asp:Label ID="lblRemainderDate" runat="server" Text="DueDate" Font-Names="arial" Font-Size="X-Small"
                    Visible="False"></asp:Label>
            </td>
            <td style="width: 211px" id="tdtxtRemainderDate" runat="server" visible="false">
                <asp:TextBox ID="txtRemainderDate" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Visible="False" ReadOnly="True" Width="44px" Style="text-align: right"></asp:TextBox>
            </td>
            <td class="style21" style="text-align: right; width: 100px;" 
                id="tdlblRemainderMonth" runat="server" visible="false">
                <asp:Label ID="lblRemainderMonth" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Text="DueMonth" Visible="False"></asp:Label>
            </td>
            <td style="width: 220px" id="tdtxtRemainderMonth" runat="server" visible="false">
                <asp:TextBox ID="txtRemainderMonth" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Visible="False" ReadOnly="True" Width="37px" Style="text-align: right"></asp:TextBox>
                <asp:Label ID="lblRemainderDay" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Text="DueDay" Visible="False"></asp:Label>
                <asp:TextBox ID="txtRemainderDay" runat="server" Font-Names="arial" Font-Size="X-Small"
                    ReadOnly="True" Style="text-align: right" Visible="False" Width="37px"></asp:TextBox>
                        <asp:TextBox ID="txtActivityActionId" runat="server" Font-Names="arial" Font-Size="X-Small"
                    ReadOnly="True" Style="text-align: right" Visible="False" Width="37px"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 102px;" id="td1" 
                runat="server" visible="false">
                <asp:Label ID="lblReminYear" runat="server" Text="DueDate" Font-Names="arial" Font-Size="X-Small"
                    Visible="False"></asp:Label>
            </td>
            <td style="width: 211px" id="td2" runat="server" visible="false">
                <asp:TextBox ID="txtReminderYear" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Visible="False" ReadOnly="True" Width="44px" Style="text-align: right"></asp:TextBox>
            </td>
            <td class="style21" style="text-align: right" id="tdsample1" runat="server" visible="false">
                &nbsp;
            </td>
            <td style="width: 89px" id="tdsample2" runat="server" visible="false">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style21" style="text-align: left; "  
                id="tdlbltriggMonth" runat="server" visible="false">
                <asp:Label ID="lbltriggMonth" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Text="Trigger Month" Visible="False"></asp:Label>
            </td>
            <td style="width: 89px"  id="tdtxttriggMonth" runat="server" visible="false">
                <asp:TextBox ID="txttriggMonth" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" ReadOnly="True" Style="text-align: right" Visible="False" 
                    Width="37px"></asp:TextBox>
            </td>
            <td style="text-align: right; width: 43px;"  id="tdlbltriggDay" runat="server" 
                visible="false">
                <asp:Label ID="lbltriggDay" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Text="Trigger Day" Visible="False"></asp:Label>
            </td>
            <td style="width: 211px"  id="tdtxttriggDay" runat="server" visible="false">
                <asp:TextBox ID="txttriggDay" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" ReadOnly="True" Style="text-align: right" Visible="False" 
                    Width="37px"></asp:TextBox>
            </td>
            <td class="style21" style="text-align: right; width: 100px;"  
                id="tdlbltriggDate" runat="server" visible="false">
                <asp:Label ID="lbltriggDate" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Text="Trigger Date" Visible="False"></asp:Label>
            </td>
            <td style="width: 220px"  id="tdtxttriggDate" runat="server" visible="false">
                <asp:TextBox ID="txttriggDate" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" ReadOnly="True" Style="text-align: right" Visible="False" 
                    Width="37px"></asp:TextBox>
            </td>
            <td class="style21" style="text-align: right"  id="tdsample3" runat="server" visible="false">
                &nbsp;</td>
            <td style="width: 89px"  id="tdsample4" runat="server" visible="false">
                &nbsp;</td>
        </tr>
    </table>
</asp:Panel>

    <asp:Panel ID="pnlActionMonth" runat="server" Font-Names="arial" Font-Size="X-Small" 
    Enabled="False" Visible="False" BorderWidth="1px" Width="968px" Height="50px">
                
        <table style="width: 97%">
            <tr>
                <td style="width: 99px">
                    <asp:Label ID="lblRemaingerYear" runat="server" Text="For the Year" 
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 86px">
                    <%--<asp:TextBox ID="txtReminMonth" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlRemaingerYear" runat="server" Font-Names="arial" DataTextField="YearName" DataValueField="DueYear"
                        Font-Size="X-Small">
                    </asp:DropDownList>
                </td>
                <td style="width: 91px" class="style21">
                    <asp:Label ID="lblReminMonth" runat="server" Text="For the Month" 
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 86px">
                    <%--<asp:TextBox ID="txtReminMonth" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlReminMonth" runat="server" Font-Names="arial" DataTextField="MonthName" DataValueField="Month"
                        Font-Size="X-Small">
                    </asp:DropDownList>
                </td>
                <td style="width: 91px">
                    <asp:Label ID="lblReminDate" runat="server" Text="For the Date" 
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 123px">
                    <%-- <asp:TextBox ID="txtReminDate" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlReminDate" runat="server" Font-Names="arial" 
                        Font-Size="X-Small">
                        <asp:ListItem Value="0">-- Select Please --</asp:ListItem>
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
                </td>
                <td class="style22" style="width: 98px">
                    <asp:Label ID="lblReminDay" runat="server" Text="For the Day" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <%-- <asp:TextBox ID="txtReminDay" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlReminDay" runat="server" Font-Names="arial" DataTextField="Day" DataValueField="Number"
                        Font-Size="X-Small" Height="16px"> 
                        <asp:ListItem Value="0">-- Select Please --</asp:ListItem>
                            <asp:ListItem Value="1">Monday</asp:ListItem>
                                <asp:ListItem Value="2">TuesDay</asp:ListItem>
                                <asp:ListItem Value="3">WednesDay</asp:ListItem>
                                <asp:ListItem Value="4">ThursDay</asp:ListItem>
                                <asp:ListItem Value="5">FriDay</asp:ListItem>
                                <asp:ListItem Value="6">SaturDay</asp:ListItem>
                                <asp:ListItem Value="7">SunDay</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 99px">
                    <asp:Label ID="lblTrigYear" runat="server" Text="Trigger Year" 
                        Font-Bold="True"  Visible="false"></asp:Label>
                </td>
                <td style="width: 86px">
                    <%-- <asp:TextBox ID="txttrigMonth" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddlTrigYear" runat="server" Font-Names="arial" DataTextField="TriggerYear" DataValueField="Year" 
                        Font-Size="X-Small"  Visible="false">
                    </asp:DropDownList>
                </td>
                <td style="width: 91px" class="style21">
                    <asp:Label ID="lbltrigMonth" runat="server" Text="Trigger Month" 
                        Font-Bold="True"  Visible="false"></asp:Label>
                </td>
                <td style="width: 86px">
                    <%-- <asp:TextBox ID="txttrigMonth" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddltrigMonth" runat="server" Font-Names="arial" DataTextField="MonthName" DataValueField="Month" 
                        Font-Size="X-Small"  Visible="false">
                    </asp:DropDownList>
                </td>
                <td style="width: 91px">
                    <asp:Label ID="lbltrigDate" runat="server" Text="Trigger Date" Font-Bold="True"  Visible="false"></asp:Label>
                </td>
                <td style="width: 123px">
                    <%-- <asp:TextBox ID="txttrigDate" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddltrigDate" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"  Visible="false">
                        <asp:ListItem Value="0">-- Select Please --</asp:ListItem>
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
                </td>
                <td class="style22" style="width: 98px">
                    <asp:Label ID="lbltrigDay" runat="server" Text="Trigger Day" Font-Bold="True"  Visible="false"></asp:Label>
                </td>
                <td>
                    <%--<asp:TextBox ID="txttrigDay" runat="server"></asp:TextBox>--%>
                    <asp:DropDownList ID="ddltrigDay" runat="server" Font-Names="arial" DataTextField="Day" DataValueField="Number"
                        Font-Size="X-Small"  Visible="false"> 
                        <asp:ListItem Value="0">-- Select Please --</asp:ListItem>
                            <asp:ListItem Value="1">Monday</asp:ListItem>
                                <asp:ListItem Value="2">TuesDay</asp:ListItem>
                                <asp:ListItem Value="3">WednesDay</asp:ListItem>
                                <asp:ListItem Value="4">ThursDay</asp:ListItem>
                                <asp:ListItem Value="5">FriDay</asp:ListItem>
                                <asp:ListItem Value="6">SaturDay</asp:ListItem>
                                <asp:ListItem Value="7">SunDay</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
                   
        </table>
                
        </asp:Panel>

    <asp:Panel ID="pnlCompletedDate" runat="server" Width="992px" BorderWidth="1px" 
    Visible="False" Height="56px">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>--%>
    <table style="width: 99%" >
        <tr>
            <td style="width: 85px">
                <asp:Label ID="lblCompletedDate" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Text="Completed Date" Font-Bold="True"></asp:Label>
            </td>
            <td style="width: 180px">
            
             <asp:TextBox ID="txtCompletedDate" runat="server" Font-Names="arial" Font-Size="X-Small"></asp:TextBox>
                <asp:ImageButton ID="imgCompletedDate" runat="server" ImageUrl="~/Images/Calendar.gif" Style="height: 15px; width: 16px;" CausesValidation="false"  OnClick="imgCompletedDate_Click" />
                 <%-- <cc1:CalendarExtender TargetControlID="txtCompletedDate" runat="server" PopupButtonID="imgCompletedDate" Format="dd/MM/yyyy" ></cc1:CalendarExtender>--%>
            <%-- <asp:ImageButton ID="imgCompletedDate" runat="server"  ImageUrl="~/Images/Calendar.gif" /></td>--%>
            <%--  <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtCompletedDate" runat="server" PopupButtonID="imgCompletedDate" Format="dd/MM/yyyy"></asp:CalendarExtender>--%>
                    
             <%--<asp:ImageButton ID="imgCompletedDate" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/Calendar.gif" OnClick="imgCompletedDate_Click"/>--%>
               <%--<asp:Button ID="imgCompletedDate" runat="server" Font-Names="arial"  Font-Size="X-Small" OnClick="imgCompletedDate_Click" Style="text-align: center" 
                    Text="Calender" Width="58px" />  --%>
 </td>
            <td style="width: 75px"> 
                <asp:Label ID="lblRemarks" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Text="Remarks" Font-Bold="True"></asp:Label>
            </td>
            <td style="width: 580px">
                <asp:TextBox ID="txtRemarks" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Width="555px"></asp:TextBox>     </td>
                     <td style="width: 52px" class="style25">
                        <asp:Button ID="btnSave" runat="server" Font-Names="arial"     
                            Font-Size="X-Small" OnClick="btnSave_Click" Style="text-align: center" 
                    Text="Save" Width="54px" /></td>
                   
                    </tr>
        <tr>
        <td style="margin-right: 40px; width: 83px;">
          <%--      <br />--%>
                <asp:Panel ID="LayerCal" runat="server" Height="224px" Style="z-index: 50; left: 459px;
                    position: absolute; top: 302px" Width="251px">
                    <asp:Calendar ID="clndrGetdate" runat="server" BackColor="White" BorderColor="#3366CC"
                        BorderWidth="1px" DayNameFormat="Shortest" Font-Names="arial" Font-Size="8pt" 
                        ForeColor="#003399" Height="200px" OnSelectionChanged="clndrGetdate_SelectionChanged" on
                        Visible="False" Width="220px" CellPadding="1">
                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                        <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                        <OtherMonthDayStyle ForeColor="#999999" />
                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                        <DayHeaderStyle BackColor="#99CCCC" Height="1px" ForeColor="#336666" />
                        <TitleStyle BackColor="#003399" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF"
                            BorderColor="#3366CC" BorderWidth="1px" Height="25px" />
                        <WeekendDayStyle BackColor="#CCCCFF" />
                    </asp:Calendar>
                    <asp:TextBox ID="txtCalValue" runat="server" Visible="False" Width="26px"></asp:TextBox>
                    <asp:TextBox ID="txtCalLeft" runat="server" Visible="False" Width="26px"></asp:TextBox>
                    <asp:TextBox ID="txtCalTop" runat="server" Visible="False" Width="26px"></asp:TextBox>
                </asp:Panel>
            </td>
            <td style="width: 180px"></td>
            <%-- <td style="width: 52px" class="style25">
                        <asp:Button ID="btnSave" runat="server" Font-Names="arial"     
                            Font-Size="X-Small" OnClick="btnSave_Click" Style="text-align: center" 
                    Text="Save" Width="54px" /></td>--%>
                    <td></td>
                    <td style="width: 581px">
                    <asp:Label ID="lblrem" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Text="When Button is SAVE Enter Data for Action taken and Save. Then Upload Documents One by One in below Panel" Font-Bold="True" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            
            
           <%-- <td>
                &nbsp;
                </td>--%>
        </tr>
    </table>
</asp:Panel>

    <%--  <asp:Panel ID="pnlSave" runat="server" Visible="False" Width="879px">
<table style="width: 876px">
<tr>
<td style="width: 409px">
</td>
<td style="width: 56px">                        
<asp:Button ID="btnSave" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" OnClick="btnSave_Click" Style="text-align: center" 
                    Text="Save" />
            </td>
            <td></td></tr></table>
</asp:Panel>--%>
    <asp:Panel ID="pnlUpload" runat="server" Height="216px" Width="1012px" 
            BorderWidth="1px"  Visible="False">
<%--<table style="width: 102%; height: 155px;">
<tr>
<td style="width: 484px">--%>
    <table style="width: 90%; height: 149px">
        <tr>
            <td style="width: 127px">
                <asp:Panel ID="pnlDocType" runat="server" Height="196px" Width="457px">
<asp:TextBox ID="txtDocTypeId" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtActivityActionDocId" runat="server" Visible="false"></asp:TextBox>
<asp:TextBox ID="txtEditDocName" runat="server" Visible="false"></asp:TextBox>
    <table style="width: 95%">
        <tr>
            <td style="width: 102px; ">
                <asp:Label ID="lblDocUpload" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Text="Document Upload" Font-Bold="True"></asp:Label>
            </td>
            <td style="width: 302px">
                <%--<asp:FileUpload ID="fluDocfile" runat="server" Font-Names="arial" Font-Size="X-Small"  onchange="GetPath(this.value);"/>--%>
                 <asp:FileUpload ID="fluDocfile" runat="server" Font-Names="arial" Font-Size="X-Small" style="text-align: right" Width="244px" 
                                              onchange="GetPath(this.value);"/>
            </td>
        </tr>
            <tr>
                                <td class="style1" style="width: 102px; ">
                                    <asp:Label ID="lblDocumentName" runat="server" Font-Names="arial" 
                                        Font-Size="X-Small" Text="Document Name" Visible="False" Font-Bold="True"></asp:Label>
                                </td>
                                <td style="width: 121px">
                                    <asp:TextBox ID="txtDocumentName" runat="server" Font-Names="arial" 
                                        Font-Size="X-Small" ReadOnly="True" Visible="false"></asp:TextBox>
                                </td>
                            </tr>
        <tr>
            <td style="width: 102px; ">
                <asp:Label ID="lblDocumentType0" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Text="Document Type" Font-Bold="True"></asp:Label>
            </td>
            <td style="width: 302px">
                <asp:DropDownList ID="ddlDocumentType" runat="server" Font-Names="arial" Font-Size="X-Small" DataTextField="DocumentTypeName" 
                DataValueField="ActivityForCompanyId">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 102px; text-align: left;">
                <asp:Label ID="lblNotes0" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Text="Remarks" style="text-align: right" ></asp:Label>
            </td>
            <td style="width: 302px">
                <asp:TextBox ID="txtNotes" runat="server" Font-Names="arial" Font-Size="X-Small"
                    Width="273px" Height="16px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 102px">
                &nbsp;
            </td>
            <td style="width: 302px">
                <asp:Button ID="btnAdd" runat="server" Font-Names="arial" Font-Size="X-Small" Text="Add"
                    OnClick="btnAdd_Click" />
            </td>
        </tr>

        <tr>
       <%-- scs added 080216 for storing data as per the year of creation Hence created below txt box for use--%>
        <td><asp:TextBox ID="txtDocPath" runat="server" Font-Names="arial" 
                Font-Size="X-Small" Visible="false"
                    Width="68px" Height="16px"></asp:TextBox></td>
                                <%-- <td style="width: 102px">
                                    <asp:HiddenField ID="HidUpdateCount" runat="server" Value="0" />
                                </td>--%>
                            </tr>
    </table>
    </asp:Panel>
</td>
<td style="height: 114px; width: 261px;">

<asp:Panel ID="PnlDoc" runat="server" Height="181px" Width="441px">
                    <div style="height: 171px; width: 427px; margin-left: 0px; overflow:auto" >
                        <asp:GridView ID="GrdDocument" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical"  Height="16px" Width="394px"
                            Font-Names="arial" Font-Size="X-Small" onrowcommand="GrdDocument_RowCommand">
                            <AlternatingRowStyle BackColor="#DCDCDC" /> <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" /> <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />         <SortedAscendingHeaderStyle BackColor="#0000A9" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />      <SortedDescendingHeaderStyle BackColor="#000065" />
<Columns>
<asp:TemplateField HeaderText="DocumentTypeId" Visible="false">
        <ItemTemplate>
            <asp:Label ID="lblDocTypeId" runat="server" Font-Names="arial" Font-Size="X-Small"
                Text='<%# Eval("DocumentTypeId") %>'>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            </asp:Label>
        </ItemTemplate>
        <%--  <EditItemTemplate>
            <asp:TextBox ID="txtDocumentTypeId" runat="server" Font-Names="arial" Font-Size="X-Small"
                ReadOnly="true" Text='<%# Bind("DocumentTypeId") %>'>
            </asp:TextBox>
        </EditItemTemplate>--%>
        <HeaderStyle Width="20px" />
        <ItemStyle Width="180px" />
    </asp:TemplateField>



    <asp:TemplateField HeaderText="ActivityActionId" Visible="false">
        <ItemTemplate>
            <asp:Label ID="lblDocActivityActionId" runat="server" Font-Names="arial" Font-Size="X-Small"
                Text='<%# Eval("ActivityActionId") %>'>

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

            </asp:Label>
        </ItemTemplate>
        <%--  <EditItemTemplate>
            <asp:TextBox ID="txtDocActivityActionId" runat="server" Font-Names="arial" Font-Size="X-Small"
                ReadOnly="true" Text='<%# Bind("ActivityActionId") %>'>
            </asp:TextBox>
        </EditItemTemplate>--%>
        <HeaderStyle Width="20px" />
        <ItemStyle Width="180px" />
    </asp:TemplateField>

 

    <asp:TemplateField HeaderText="ActivityActionDocId" Visible="false">
        <ItemTemplate>
            <asp:Label ID="lblActivityActionDocId" runat="server" Font-Names="arial" Font-Size="X-Small"
                Text='<%# Eval("ActivityActionDocId") %>'>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
            </asp:Label>
        </ItemTemplate>
        <EditItemTemplate>
            <asp:TextBox ID="txtActivityActionDocId" runat="server" Font-Names="arial" Font-Size="X-Small"
                ReadOnly="true" Text='<%# Bind("ActivityActionDocId") %>'>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  
            </asp:TextBox>
        </EditItemTemplate>
        <HeaderStyle Width="20px" />
        <ItemStyle Width="180px" />
    </asp:TemplateField>

        <asp:TemplateField HeaderText="Document Name">
        <ItemTemplate>
            <asp:Label ID="lblDocName" runat="server" 
                Text='<%# Eval("DocumentName") %>'>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                  
            </asp:Label>
            </ItemTemplate>
        <%-- <EditItemTemplate>
            <asp:TextBox ID="txtDocumentName" runat="server" Font-Names="arial" 
                Font-Size="X-Small" Text='<%# Bind("DocumentName") %>' Width="60px">
                </asp:TextBox>
        </EditItemTemplate>--%>
        <HeaderStyle Width="100px" />
    </asp:TemplateField>

        <asp:TemplateField HeaderText="storage Path">
        <ItemTemplate> <asp:Label ID="lblStPath" runat="server" Text='<%# Eval("StoragePath") %>'> </asp:Label>
            </ItemTemplate>  <HeaderStyle Width="100px" />
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Document Type">
        <ItemTemplate>
            <asp:Label ID="lblDocType" runat="server" Text='<%# Eval("DocumentType") %>'> 
                    
            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    
            </asp:Label>
        </ItemTemplate>
        <%-- <EditItemTemplate>
            <asp:TextBox ID="txtDocumentType" runat="server" Font-Names="arial" Font-Size="X-Small"
                Text='<%# Bind("DocumentType") %>' Width="60px">
            </asp:TextBox>
        </EditItemTemplate>--%>
        <HeaderStyle Width="50px" />
    </asp:TemplateField>
           
    <asp:TemplateField HeaderText="Notes">
    <ItemTemplate>
        <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;            
        </asp:Label>
    </ItemTemplate>
    <%--  <EditItemTemplate>
        <asp:TextBox ID="txtNotes0" runat="server" Font-Names="arial" 
            Font-Size="X-Small" Text='<%# Bind("Notes") %>' Width="20px"> 
        </asp:TextBox>
    </EditItemTemplate>--%>
        <HeaderStyle Width="60px" />
        <ItemStyle width="150px" />
    </asp:TemplateField>
    <asp:ButtonField CommandName="EditDoc" HeaderText="Edit" Text="Edit" Visible="true"/>
       <asp:ButtonField CommandName="View" HeaderText="View Doc" Text="View"  />
        </Columns>
</asp:GridView>
</div>
</asp:Panel>
</td>
</tr>
</table>
        </asp:Panel>
</asp:Panel>
<%--Below area not used--%>
        <asp:Panel ID="PnlActionSummary" runat="server" Width="415px" 
    Visible="false">
    <table>
    <tr>
        <td style="width: 846px; text-align: right;">
        <asp:TextBox ID="txtActionSummary" runat="server" Text="Activity Action Summary" 
                Width="405px" ReadOnly="true"
                style="text-align: left" BackColor="#003366" ForeColor="White" 
                Font-Bold="true" Font-Size="X-Small" Height="16px"></asp:TextBox>
        </td>
    </tr>
    </table>
    <div style="overflow:auto; height:117px; width:423px">
<asp:GridView ID="GrdActionSummary" runat="server" CellPadding="3" 
    Font-Size="X-Small" Width="408px" Font-Names="arial" AutoGenerateColumns="False" 
    Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
        BorderStyle="None" BorderWidth="1px" 
            onrowdatabound="GrdActionSummary_RowDataBound">
<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
<RowStyle BackColor="#EEEEEE" ForeColor="Black" />
<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
<SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
<HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
    HorizontalAlign="Left" />
<AlternatingRowStyle BackColor="#DCDCDC" />
<Columns>
    <asp:TemplateField HeaderText="Severity" Visible="True">
    <ItemTemplate>
    <asp:Label ID="lblSeverity" runat="server" Text='<%# Eval("SeverityName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
        <HeaderStyle Width="100px" />
        <ItemStyle Width="300px" />
    </asp:TemplateField>

        <asp:TemplateField HeaderText="R">
    <ItemTemplate>
    <asp:Label ID="lblRedCount" runat="server" Text='<%# Eval("RedCount") %>'  Font-Names="arial" Font-Size="X-Small" ForeColor="Red"></asp:Label></ItemTemplate>
        <HeaderStyle Width="20px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="B">
    <ItemTemplate>
    <asp:Label ID="lblBlueCount" runat="server" Text='<%# Eval("BlueCount") %>'  Font-Names="arial" Font-Size="X-Small" ForeColor="Blue"></asp:Label></ItemTemplate>
        <HeaderStyle Width="20px" />
    </asp:TemplateField>
    <asp:TemplateField HeaderText="G">
    <ItemTemplate>
    <asp:Label ID="lblGreenCount" runat="server" Text='<%# Eval("GreenCount") %>'  Font-Names="arial" Font-Size="X-Small" ForeColor="Green"></asp:Label></ItemTemplate>
        <HeaderStyle Width="20px" />
    </asp:TemplateField>
<%----------------------------------------------added by abinayaa 280213-------------------------------------------------------------------------%>
<asp:TemplateField HeaderText="V">
    <ItemTemplate>
    <asp:Label ID="lblvioletCount" runat="server" Text='<%# Eval("violetCount") %>'  Font-Names="arial" Font-Size="X-Small" ForeColor="violet" Font-Bold="true"></asp:Label></ItemTemplate>
        <HeaderStyle Width="20px" />
    </asp:TemplateField>
  <%---------------------------------------------added by abinayaa 280213---------------------------------------------------------------------%>
</Columns>
<sortedascendingcellstyle backcolor="#F1F1F1" />
<sortedascendingheaderstyle backcolor="#0000A9" />
<sorteddescendingcellstyle backcolor="#CAC9C9" />
<sorteddescendingheaderstyle backcolor="#000065" />
</asp:GridView>
</div>
    </asp:Panel>
        <asp:Panel ID="pnlNext" runat="server" Height="18px" Width="1041px" >
        <table>
        <tr>
        <td align="center" style="width: 1025px">
        <asp:Button ID="btnNext" runat="server" Text="Next->" 
                ToolTip="Click Next to view As&When Activities" 
                Visible="true" onclick="btnNext_Click" />
        </td>
        </tr>
        </table>
</asp:Panel>
        <asp:Panel ID="pnlConirm" runat="server" Height="31px" Width="1045px" Visible="false" >
<table>
<tr>
<td style="width: 500px" align="right">
<asp:Button ID="btnBack" runat="server" Text="<-Back" 
        ToolTip="Click back to view Activity Action Summary & Other Frequency Detials"
        Visible="true" onclick="btnBack_Click" Width="67px" />
</td>
<td  align="right">
<asp:Button ID="btnAsonwhenConfirm" runat="server" Text="Confirm" 
        ToolTip="Before Confirming to enable the menu, Please Read through the grid for Action" 
        Visible="true" onclick="btnAsonwhenConfirm_Click" />
</td>
</tr>
</table>
</asp:Panel>    
<%--area not used--%>
</asp:Content>
                