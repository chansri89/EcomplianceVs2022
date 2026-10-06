<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage3.master" AutoEventWireup="true"
    CodeFile="ActivityMaster.aspx.cs" Inherits="ActivityMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <script type="text/javascript" language="javascript">
            function GetPath(Path) {

                //document.getElementById('<%= txtExistingPath.ClientID %>').value = Path;


            }
            //      function GetFilename(Filename)
            //       {
            //           document.getElementById("txtDocumentName").value = Filename.value;
            //document.getElementById("txtDocumentName").value = Path.value;
            //      }
        </script>
        <asp:Panel ID="pnlActivityLoad" runat="server" Height="26px" Width="722px" Visible="false">
            <table style="width: 100%; height: 23px;">
                <tr>
                    <td style="width: 41px;">
                        <asp:Label ID="lblACtivityLoad" runat="server" Text="Act" Font-Names="Verdana" Font-Size="XX-Small"
                            Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 196px" class="style21">
                        <asp:DropDownList ID="ddlLoadActivity" runat="server" Font-Names="Verdana" DataTextField="ActName"
                            DataValueField="ActId" Size="XX-Small" AutoPostBack="True" Font-Size="XX-Small"
                            Height="16px" Width="182px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 35px">
                        <asp:Button ID="btnGo" runat="server" Text="Go" Font-Names="Verdana" Font-Size="XX-Small"
                            OnClick="btnGo_Click" />
                    </td>
                    <%-- </tr>
            </table>--%>
                    <%-- <table style="width: 100%; height: 35px;">
        <tr>--%>
                    <td style="width: 90px; text-align: left;">
                        <asp:Label ID="lblActivityNamefilter" runat="server" Text="Activity Name" Font-Names="Verdana"
                            Font-Size="XX-Small" Font-Bold="True" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 124px">
                        <asp:TextBox ID="txtActivityNamefilter" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                            Width="167px" Height="16px" Visible="False"></asp:TextBox>
                    </td>
                    <td class="style22" style="width: 58px">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Names="Verdana" Font-Size="XX-Small"
                            Height="21px" OnClick="btnSearch_Click" Visible="False" />
                    </td>
                    <td>
                        <asp:Button ID="btnSearchClear" runat="server" Text="Clear Search" Font-Names="Verdana"
                            Font-Size="XX-Small" Height="21px" Width="87px" OnClick="btnSearchClear_Click"
                            Visible="False" />
                    </td>
                    <%--    </asp:Panel>--%>
                </tr>
            </table>
        </asp:Panel>
<table>
<tr>
<td>
<asp:Label ID="lblActivityBlue" runat="server" Font-Names="Verdana" 
        Font-Size="XX-Small" ForeColor="Blue" 
        Text="Blue --> Document attributes not entered" Visible="true"></asp:Label>
</td>            
            <td style="width: 30px;"></td>          
            <td>
                <asp:Label ID="lblActivityRed" runat="server" Font-Names="Verdana" 
                    Font-Size="XX-Small" ForeColor="Red" 
                    Text="Red -->  Document attributes and Task Name not entered " Visible="true"></asp:Label>
            </td>
            <td style="width: 30px;"></td>
            <td>
                <asp:Label ID="lblActivityMaroon" runat="server" Font-Names="Verdana" 
                    Font-Size="XX-Small" ForeColor="Maroon"
                    Text="Maroon --> Task Name Not entered" Visible="true" Font-Bold="true"></asp:Label>
            </td>
        </tr>
    </table>
        <br />
        <asp:Panel ID="Pnlgv" runat="server" Height="271px" Width="1351px">
            <div style="overflow: auto; height: 233px; width: 1289px">
                <asp:GridView ID="GrdActivityMaster" runat="server" CellPadding="3" Font-Size="XX-Small"
                    Width="1266px" Font-Names="Verdana" AutoGenerateColumns="False" Height="16px"
                    GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None"
                    BorderWidth="1px" OnRowCommand="GrdActivityMaster_RowCommand" 
                    DataKeyNames="ActivityId" onrowdatabound="GrdActivityMaster_RowDataBound" >
                    
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="ActId" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblActId" runat="server" Text='<%# Eval("ActId") %>' Font-Names="Verdana"
                                    Font-Size="XX-Small"></asp:Label>
                            </ItemTemplate>
                          
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ActDtlId" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblActDtlId" runat="server" Text='<%# Eval("ActDtlId") %>' Font-Names="Verdana"
                                    Font-Size="XX-Small"></asp:Label>
                            </ItemTemplate>
                          
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ActivityId" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblActivityId" runat="server" Text='<%# Eval("ActivityId") %>' Font-Names="Verdana"
                                    Font-Size="XX-Small"></asp:Label>
                            </ItemTemplate>
                           
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CategoryId" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblCategoryId" runat="server" Text='<%# Eval("CategoryId") %>' Font-Names="Verdana"
                                    Font-Size="XX-Small"></asp:Label>
                            </ItemTemplate>
                          
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SeverityId" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblSeverityId" runat="server" Text='<%# Eval("SeverityId") %>' Font-Names="Verdana"
                                    Font-Size="XX-Small"></asp:Label>
                            </ItemTemplate>
                            
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DepartmentId" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartmentId" runat="server" Text='<%# Eval("DepartmentId") %>'
                                    Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                            </ItemTemplate>
                          
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DocumentTypeId" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblDocumentTypeId" runat="server" Text='<%# Eval("DocumentTypeId") %>'
                                    Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                            </ItemTemplate>
                           
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ActivityDocumentId" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblActivityDocumentId" runat="server" Text='<%# Eval("ActivityDocumentId") %>'
                                    Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                            </ItemTemplate>
                            
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        <%------------Act and actdetais----------------%>
                        <asp:ButtonField HeaderText="Form View" Text="View" CommandName="View">
                            <HeaderStyle Height="20px" />
                            <ItemStyle Width="20px" />
                        </asp:ButtonField>
                        <asp:ButtonField HeaderText="Add Activity" Text="Add" CommandName="Select">
                            <HeaderStyle Height="20px" />
                            <ItemStyle Width="20px" />
                        </asp:ButtonField>
                        <%--Added by Abinayaa 101013 starts--------%>
                         <asp:ButtonField HeaderText="Add Document" Text="Select" CommandName="AddDoc">
                            <HeaderStyle Height="20px" />
                            <ItemStyle Width="20px" />
                        </asp:ButtonField>
                        <%--Added by Abinayaa 101013 ends--------%>
                        <asp:TemplateField HeaderText="Act">
                            <ItemTemplate>
                                <asp:Label ID="lnkActName" runat="server" Text='<%# Eval("ActName") %>' Font-Names="Verdana"
                                    Font-Size="XX-Small" Width="180px" ToolTip="Click Add for Creating New Activity for this Act"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="180px" />
                            <ItemStyle Width="180px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Chapter">
                            <ItemTemplate>
                                <asp:Label ID="lblChapter" runat="server" Text='<%# Eval("Chapter") %>'></asp:Label>
                            </ItemTemplate>
                           
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Head">
                            <ItemTemplate>
                                <asp:Label ID="lblHead" runat="server" Text='<%# Eval("Head") %>'></asp:Label></ItemTemplate>
                           
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section">
                            <ItemTemplate>
                                <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'></asp:Label></ItemTemplate>
                          
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ActRule">
                            <ItemTemplate>
                                <asp:Label ID="lblActRule" runat="server" Text='<%# Eval("ActRule") %>'></asp:Label></ItemTemplate>
                           
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>
                        <%------------Actvityname and details----------------%>
                        <asp:TemplateField HeaderText="Activity Name">
                            <ItemTemplate>
                                <asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("ActivityName") %>'></asp:Label></ItemTemplate>
                        
                            <HeaderStyle Width="180px" />
                            <ItemStyle Width="180px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Form">
                            <ItemTemplate>
                                <asp:Label ID="lblForm" runat="server" Text='<%# Eval("DocumentName") %>'></asp:Label>
                            </ItemTemplate>
                    
                            <HeaderStyle Width="180px" />
                            <ItemStyle Width="180px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dept Name">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartName" runat="server" Text='<%# Eval("DepartmentName") %>'></asp:Label></ItemTemplate>
                           
                            <HeaderStyle Width="40px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category Name">
                            <ItemTemplate>
                                <asp:Label ID="lblCategName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label></ItemTemplate>
                           
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Severity Name">
                            <ItemTemplate>
                                <asp:Label ID="lblSevtyName" runat="server" Text='<%# Eval("SeverityName") %>'></asp:Label>
                            </ItemTemplate>
                           
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Compliance Task Avbl ?">
                            <ItemTemplate>
                                <asp:Label ID="lblNonComplianceTaskAvailableFlag" runat="server" Text='<%# Eval("NonComplianceTaskAvailableFlag") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="State Specific">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkStateSpec" runat="server" Checked='<%# Eval("IsStateSpecific") %>'
                                    Enabled="false" Font-Names="Verdana" Font-Size="XX-Small" />
                            </ItemTemplate>
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location Specific">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkLocationSpec" runat="server" Checked='<%# Eval("IsLocationSpecific") %>'
                                    Enabled="false" Font-Names="Verdana" Font-Size="XX-Small" />
                            </ItemTemplate>
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Regular Activity">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkRegularActivity" runat="server" Checked='<%# Eval("IsRegularActivity") %>'
                                    Enabled="false" Font-Names="Verdana" Font-Size="XX-Small" />
                            </ItemTemplate>
                            <HeaderStyle Width="30px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Is  Active">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>' Enabled="false"
                                    Font-Names="Verdana" Font-Size="XX-Small" />
                            </ItemTemplate>
                            <HeaderStyle Width="40px" />
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Administrator" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAdministrator" runat="server" Text='<%# Eval("Administrator") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="ActivityDocId" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblActivityDocId" runat="server" Text='<%# Eval("ActivityDocId") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="ActivityForCompanyId" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblNonComplianceCompanyId" runat="server" Text='<%# Eval("NonComplianceCompanyId") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="50px" />
                        </asp:TemplateField>
                        
                        <asp:ButtonField HeaderText="Edit" Text="Edit" CommandName="Activity_Edit">
                            <HeaderStyle Height="20px" />
                            <ItemStyle Width="20px" />
                        </asp:ButtonField>
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" Visible="false" />
                    </Columns>
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
            </div>
        </asp:Panel>
        
        <asp:Panel ID="pnlAdd" runat="server" Width="1197px" Height="873px">
            <asp:Panel ID="pnlACt" runat="server" Enabled="false" BorderWidth="1px" Width="1015px"
                Visible="False" Height="26px">
                <table style="width: 97%; height: 3px;">
                    <tr>
                        <td style="width: 96px; text-align: left; height: 16px;">
                            <asp:Label ID="lblActName" runat="server" Text="Act" Font-Names="Verdana" Font-Size="XX-Small"
                                Style="text-align: left"></asp:Label>
                        </td>
                        <td style="width: 127px; height: 16px;">
                            <asp:TextBox ID="txtActName" runat="server" Font-Names="Verdana" ReadOnly="true"
                                Font-Size="XX-Small" ToolTip="Click Select for Creating New Activity in the Grid"
                                Height="16px" Width="219px">
                            </asp:TextBox>
                        </td>
                        <td style="width: 55px; text-align: left; height: 16px;">
                            <asp:Label ID="lblChapter" runat="server" Text="Chapter" Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                        </td>
                        <td style="width: 105px; height: 16px;" class="style21">
                            <asp:TextBox ID="txtChapter" runat="server" Font-Names="Verdana" ReadOnly="true"
                                Font-Size="XX-Small" Width="65px"></asp:TextBox>
                        </td>
                        <td class="style1" style="width: 80px; text-align: right; height: 16px;">
                            <asp:Label ID="lblHead" runat="server" Text="Head" Font-Names="Verdana" Font-Size="XX-Small"
                                Style="text-align: left"></asp:Label>
                        </td>
                        <td style="width: 96px; height: 16px;">
                            <asp:TextBox ID="txtHead" runat="server" Font-Names="Verdana" ReadOnly="true" Font-Size="XX-Small"
                                Width="77px"></asp:TextBox>
                        </td>
                        <td class="style1" style="width: 121px; text-align: right; height: 16px;">
                            <asp:Label ID="lblSection" runat="server" Text="Section" Font-Names="Verdana" Font-Size="XX-Small"
                                Style="text-align: left"></asp:Label>
                        </td>
                        <td style="width: 106px; height: 16px;">
                            <asp:TextBox ID="txtSection" runat="server" Font-Names="Verdana" ReadOnly="true"
                                Font-Size="XX-Small" Width="59px"></asp:TextBox>
                        </td>
                        <td class="style1" style="width: 138px; text-align: left; height: 16px;">
                            <asp:Label ID="lblActRule" runat="server" Text="ActRule" Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                        </td>
                        <td style="width: 126px; height: 16px;">
                            <asp:TextBox ID="txtActRule" runat="server" Font-Names="Verdana" ReadOnly="true"
                                Font-Size="XX-Small" Width="76px"></asp:TextBox>
                        </td>
                        <td style="height: 16px">
                            <asp:TextBox ID="txtActDtlId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Visible="false" ReadOnly="true" Width="21px"></asp:TextBox>
                        </td>
                        <td style="height: 16px">
                            <asp:TextBox ID="txtActivityId" runat="server" Visible="False" Font-Names="Verdana"
                                Font-Size="XX-Small" Width="30px"></asp:TextBox>
                        </td>
                        <td style="height: 16px">
                            <asp:TextBox ID="txtActivityDocumentId" runat="server" Visible="False" Font-Names="Verdana"
                                Font-Size="XX-Small" Width="30px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlColapse" runat="server" Width="1014px" Visible="false" 
                Height="26px">
              <table>
                    <tr>
                        <td style="width: 846px; text-align: right;">
                            <asp:TextBox ID="txtTitle" runat="server"  Width="946px"
                                ReadOnly="true" Style="text-align: left" BackColor="#003366" ForeColor="White"
                                Font-Bold="true" Font-Size="XX-Small" Height="21px" Text="Click this button to add Activity"></asp:TextBox> 
                        </td>
                        <td>
                            <asp:Button ID="btnCollapse" Text="+" runat="server" Width="36px" Font-Size="XX-Small"
                                OnClick="btnCollapse_Click" ToolTip="Click this button to add Activity" />
                        </td>
                    </tr>
                </table>
                </asp:Panel>
            <br />
            <asp:Panel ID="pnlNew" runat="server" Width="909px" Height="118px">
                <table style="width: 98%; margin-right: 0px; height: 68px;">
                    <tr>
                        <td class="style21" style="width: 181px; height: 18px;">
                            <asp:Label ID="lblActivityName" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Text="Activity Name" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 218px; height: 18px;">
                            <asp:TextBox ID="txtActivityName" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Width="249px" ToolTip="Click Add for Creating New Activity in the Grid"></asp:TextBox>
                          
                        </td>
                        <td style="width: 234px; height: 18px;">
                            <asp:Label ID="lblDepartmentName" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Text="Department Name" Font-Bold="True" Style="text-align: right"></asp:Label>
                        </td>
                        <td style="height: 18px">
                            <asp:DropDownList ID="ddlDepartmentName" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                OnSelectedIndexChanged="ddlDepartmentName_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                   
                        <td class="style21" style="width: 203px; height: 18px;">
                            <asp:Label ID="lblCategoryName" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Text="Category Name" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="width: 247px; height: 18px;">
                            <asp:DropDownList ID="ddlCategoryName" runat="server" Font-Names="Verdana" Font-Size="XX-Small">
                            </asp:DropDownList>
                        </td>
                         </tr>
                    <tr>
                        <td style="width: 181px; height: 20px;">
                            <asp:Label ID="lblSeverityName" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Text="Severity Name" Font-Bold="True"></asp:Label>
                        </td>
                        <td style="height: 20px; width: 218px;">
                            <asp:DropDownList ID="ddlSeverityName" runat="server" Font-Names="Verdana" Font-Size="XX-Small">
                            </asp:DropDownList>
                        </td>
                  
                        <td class="style21" style="width: 234px; height: 20px;">
                            <asp:Label ID="lblDocType" runat="server"  Font-Names="Verdana" Font-Size="XX-Small"
                                Text="Document Type"></asp:Label>
                        </td>
                        <td style="width: 247px; height: 20px;">
                            <asp:DropDownList ID="ddlDocType" runat="server" Font-Names="Verdana" Font-Size="XX-Small">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 203px; height: 20px;">
                            <asp:Label ID="lblNonComplianceTaskAvailableFlag" runat="server" Font-Names="Verdana"
                                Font-Size="XX-Small" Text="Compliance Task Avbl (Y/N)"></asp:Label>
                        </td>
                        <td style="height: 20px">
                            <asp:TextBox ID="txtNonComplianceTaskAvailableFlag" runat="server" Font-Names="Verdana"
                                Font-Size="XX-Small" Text="N" Width="27px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="style21" style="width: 181px; height: 20px;">
                        </td>
                        <td style="width: 218px; height: 20px;">
                            <asp:CheckBox ID="chkStateSpecific" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Text="State Specific" Style="text-align: center" />
                        </td>
                        <td style="width: 234px; height: 20px;">
                            <asp:CheckBox ID="chkLocationSpecific" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Text="Location Specific" Style="text-align: right" />
                        </td>
                        <td style="height: 20px">
                            <asp:CheckBox ID="chkRegularActivity" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Style="text-align: right" Text="Regular Activity" />
                        </td>
                    </tr>
                    </table>
                    <table>
                    <tr>
                        <td class="style21" style="width: 120px">
                            <asp:Label ID="lblAccessPath" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Text="File Access Path" Font-Bold="false"></asp:Label>
                        </td>
                        <td style="width: 212px">
                            <asp:FileUpload ID="fluAccessPath" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Style="text-align: right" Width="214px" onchange="GetPath(this.value);" />
                        </td>
                        <td style="width: 117px">
                            <asp:Label ID="lblDocumentName" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Text="Document Name"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDocumentName" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Width="268px"></asp:TextBox>
                        </td>
                        <td style="width: 117px">
                            <asp:CheckBox ID="chkIsActive" runat="server" Text="IsActive" Font-Names="Verdana"
                                Font-Size="XX-Small" ReadOnly="True" Width="117px" Style="text-align: left" Visible="False" />
                        </td>
                    </tr>
                    <tr>
                        <td class="style21" style="width: 120px">
                            <asp:Label ID="lblExistingPath" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Text="Existing Document Path" Width="110px" Visible="False"></asp:Label>
                        </td>
                        <td style="width: 212px">
                            <asp:TextBox ID="txtExistingPath" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                Width="83px" AutoPostBack="True" Visible="false"></asp:TextBox>
                        </td>
                        
                    </tr>
                </table>
            </asp:Panel>
            <table id="tblContinue" runat="server">
                <tr>
                    <td style="text-align: right; " class="style23">
                        &nbsp;
                    </td>
                    <td style="width: 318px">
                        &nbsp;
                    </td>
                    <td style="width: 181px">
                        <asp:Button ID="btnContinue" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                            OnClick="btnContinue_Click" Text="Continue" ToolTip="Click continue to Assign Freuency and Responsible Group"
                            Enabled="False" />
                        <asp:Button ID="btnClear" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                            Text="Clear" OnClick="btnClear_Click" Visible="true" Enabled="False" />
                        <asp:Button ID="btnBack" runat="server" Font-Names="Verdana" Visible="false" Font-Size="XX-Small"
                            Text="Back" OnClick="btnBack_Click" />
                    </td>
                    <td style="width: 146px">
                        &nbsp;
                    </td>
                </tr>
            </table>
           <asp:Panel ID="pnlFreGrp" runat="server">
            <table style="height: 190px; width: 1121px">
                <tr>
                    <td>
                        <asp:Panel ID="Pnlfrequency" runat="server" Visible="false" Height="197px" Width="654px">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtFrequencyTitle" runat="server" Text="Assign Frequency and Responsible Group for Activity"
                                            Font-Size="XX-Small" BackColor="#000084" Font-Bold="True" ForeColor="White" Width="641px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table style="height: 59px">
                                <tr>
                                    <td style="width: 78px; height: 15px;">
                                        <asp:Label ID="lblFrequency" runat="server" Text="Frequency" Font-Names="Verdana"
                                            Width="60px" Font-Size="XX-Small"></asp:Label>
                                    </td>
                                    <td class="style21" style="width: 114px; height: 15px;">
                                        <asp:DropDownList ID="ddlFrequency" runat="server" DataTextField="FrequencyName"
                                            DataValueField="FrequencyId" Font-Names="Verdana" Font-Size="XX-Small" Height="16px"
                                            Width="115px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style21" style="width: 110px; height: 15px;">
                                        <asp:Label ID="lblFrqRemarks" runat="server" Text="Frequency Remarks" Font-Names="Verdana"
                                            Font-Size="XX-Small"></asp:Label>
                                    </td>
                                    <td style="height: 15px">
                                        <asp:TextBox ID="txtFrqRemarks" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                            Width="169px"></asp:TextBox>
                                    </td>
                                    <td style="width: 68px; height: 15px;">
                                    </td>
                                    <td style="height: 15px">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 78px">
                                        <asp:Label ID="lblDueMonth" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                            Text="Due Month"></asp:Label>
                                    </td>
                                    <td class="style21" style="width: 114px">
                                        <asp:DropDownList ID="ddldueMonth" runat="server" DataValueField="Month" DataTextField="MonthName"
                                            Font-Names="Verdana" Font-Size="XX-Small">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style21" style="width: 110px">
                                        <asp:Label ID="lblDueDate" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                            Text="Due Date">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDueDate" runat="server" Font-Names="Verdana" DataValueField="Number"
                                            DataTextField="Date" Font-Size="XX-Small" Height="18px" Width="87px">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 68px">
                                        <asp:Label ID="lblDueDay" runat="server" Text="Due Day" Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDueDay" runat="server" Font-Names="Verdana" DataTextField="Day"
                                            DataValueField="Number" Width="80px" Font-Size="XX-Small">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 78px">
                                        <asp:Label ID="lblTrigMonth" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                            Text="Trigger Month"></asp:Label>
                                    </td>
                                    <td class="style21" style="width: 114px">
                                        <asp:DropDownList ID="ddlTiggerMonth" runat="server" DataValueField="Month" DataTextField="MonthName"
                                            Font-Names="Verdana" Font-Size="XX-Small">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style21" style="width: 110px">
                                        <asp:Label ID="lblTrigDate" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                            Text="Trigger Date">
                                        </asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTriggerDate" runat="server" Font-Names="Verdana" DataValueField="Number"
                                            DataTextField="Date" Font-Size="XX-Small">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 68px">
                                        <asp:Label ID="lblTriggDay" runat="server" Text="Trigger Day" Font-Names="Verdana"
                                            Font-Size="XX-Small"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTriggerDay" runat="server" Font-Names="Verdana" DataTextField="Day"
                                            DataValueField="Number" Width="80px" Font-Size="XX-Small">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td>
                        <asp:Panel ID="PnlAssignResponsibleGroup" runat="server" Height="193px" Width="458px"
                            Visible="false">
                            <div style="overflow: auto; height: 174px; width: 434px">
                                <asp:GridView ID="grdAssignResponsibleGroup" runat="server" CellPadding="3" Font-Size="XX-Small"
                                    Width="405px" Font-Names="Verdana" AutoGenerateColumns="False" Height="16px"
                                    GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None"
                                    BorderWidth="1px" OnRowCommand="GrdActivityMaster_RowCommand" DataKeyNames="ActivityId">
                                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                    <AlternatingRowStyle BackColor="#DCDCDC" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="CompanyCode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>' Font-Names="Verdana"
                                                    Font-Size="XX-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LocationDeptId" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLocationDeptId" runat="server" Text='<%# Eval("LocationDeptId") %>'
                                                    Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CompanyActivityId" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyActivityId" runat="server" Text='<%# Eval("CompanyActivityId") %>'
                                                    Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LocationName" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' Font-Names="Verdana"
                                                    Font-Size="XX-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ResponsibleGroupName" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblResponsibleGrpName" runat="server" Text='<%# Eval("ResponsibleGrpName") %>'
                                                    Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Width="20px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Select" Visible="true">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkLocationSpec" runat="server" Checked='<%# Eval("IsActive") %>'
                                                    Enabled="true" Font-Names="Verdana" Font-Size="XX-Small" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle Width="20px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#000065" />
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            </asp:Panel>
           <table runat="server" id="tblSave" visible="false">
                <tr>
                    <td style="text-align: right; width: 166px;">
                        &nbsp;
                    </td>
                    <td style="width: 156px">
                        &nbsp;
                    </td>
                    <td style="width: 181px">
                        <asp:Button ID="btnSave" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                            OnClick="btnSave_Click" Text="Go" />
                        <asp:Button ID="btnExit" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                            Text="Exit" OnClick="btnExit_Click" Visible="False" />
                    </td>
                    <td>
                       <asp:TextBox ID="txtCompanyActivityId" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                    Visible="False" ReadOnly="True" Width="37px" Style="text-align: right"></asp:TextBox>
                    </td>
                    <td style="width: 146px">
                        &nbsp;
                    </td>
                </tr>
            </table>
     
            <asp:Panel ID="pnlGridSelect" runat="server" Visible="False" Height="374px" 
                Width="956px">
                     <table style="width: 903px; height: 321px;">
                        <tr>
                            <td style="width: 112px; height: 293px;">
                                <asp:Panel ID="pnDocumentGrd" runat="server" Height="322px" 
                                    Style="margin-top: 0px" Width="408px">
                                    <div style="width: 331px; height: 314px; overflow: auto">
                                        <table>
                                            <tr>
                                                <td style="width: 187px">
                                                    <asp:Label ID="lblDocumentMaster" runat="server" Font-Names="Verdana" 
                                                        Font-Size="XX-Small" ForeColor="#3333CC" Style="text-align: left;
                                                            margin-left: 0px" Text="Create Activity Document Attributes" 
                                                        Width="300px"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <asp:GridView ID="GrdDocument" runat="server" AutoGenerateColumns="False" 
                                            BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                                            CellPadding="3" Font-Names="Verdana" Font-Size="XX-Small" GridLines="Vertical" 
                                            Height="16px" OnRowCancelingEdit="GrdDocument_RowCancelingEdit" 
                                            OnRowEditing="GrdDocument_RowEditing" OnRowUpdating="GrdDocument_RowUpdating" 
                                            Width="312px">
                                            <AlternatingRowStyle BackColor="#DCDCDC" />
                                            <FooterStyle BackColor="#CCCCCC" Font-Size="XX-Small" ForeColor="Black" />
                                            <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                            <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                            <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#000065" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="ActvyDocTypeId" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActivityDocumentTypeId" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" Text='<%# Eval("ActDocTypeId") %>'>

                                                            </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtActivityDocumentTypeId" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("ActDocTypeId") %>'>

                                                            </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                    <ItemStyle Width="180px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company ActivityId" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocumentTypeId" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" Text='<%# Eval("DocumentTypeId") %>'>
                                                            </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDocumentTypeId" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("DocumentTypeId") %>'>


                                                            </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                    <ItemStyle Width="180px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Company ActivityId" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCompanyActivityId" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" Text='<%# Eval("ActivityForCompanyId") %>'>


                                                            </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtCompanyActivityId" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("ActivityForCompanyId") %>'>


                                                            </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                    <ItemStyle Width="180px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Document Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDocumentTypeName" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" Text='<%# Eval("DocumentTypeName") %>'>

        
                                                            </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtDocumentTypeName" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("DocumentTypeName") %>' 
                                                            Width="60px">

       
                                                            </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Be Maintained" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblToBeMaintained" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" Text='<%# Eval("ToBeMaintained") %>'>

                                                            </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtToBeMaintained" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" MaxLength="1" Text='<%# Bind("ToBeMaintained") %>' 
                                                            Width="20px">

                                                            </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="To Be Submitted" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblToBeSubmitted" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" Text='<%# Eval("ToBeSubmitted") %>'>

       
                                                            </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtToBeSubmitted" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" MaxLength="1" Text='<%# Bind("ToBeSubmitted") %>' 
                                                            Width="20px">

                                                            </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="50px" />
                                                </asp:TemplateField>
                                                <%--Master Document is not Company Depanded for Every Activity hence Chnged in to Visibe "False" 06/07/12 Abinayaa--%>
                                                <asp:TemplateField HeaderText="Master Document Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMasterPDFName" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" Text='<%# Eval("MasterPDFName") %>'>

       
                                                            </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtMasterPDFName" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" Text='<%# Bind("MasterPDFName") %>' Width="150px">

       
                                                            </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="150px" />
                                                    <ItemStyle Width="150px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AccessPath" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccessPath" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" Text='<%# Eval("AccessPath") %>'>
     
                                                            </asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtAccessPath" runat="server" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" Text='<%# Bind("AccessPath") %>' Width="20px">

      
                                                            </asp:TextBox>
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="30px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is  Active">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkDocActive" runat="server" 
                                                            Checked='<%# Eval("IsActive") %>' Enabled="false" Font-Names="Verdana" 
                                                            Font-Size="XX-Small" />
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:CheckBox ID="chkDocIsActive" runat="server" 
                                                            Checked='<%# Bind("IsActive") %>' Font-Names="Verdana" Font-Size="XX-Small" />
                                                    </EditItemTemplate>
                                                    <HeaderStyle Width="30px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                                                <asp:CommandField HeaderText="Delete" ShowDeleteButton="false" 
                                                    Visible="False" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                            </td>
                            <td style="width: 513px; height: 293px;">
                                <asp:Panel ID="pnlNonComplianceTaskMsg" runat="server" Visible="False">
                                    <asp:Label ID="lblNonComplianceTask" runat="server" Font-Names="Verdana" 
                                        Font-Size="XX-Small" ForeColor="#3333CC" 
                                        Text="Activity does not have Compliance Task set" Width="332px"></asp:Label>
                                </asp:Panel>
                                <asp:Panel ID="PnlTask" runat="server" Height="300px" Style="margin-top: 6px" 
                                    Width="457px">
                                    <table style="height: 22px; width: 338px;">
                                        <tr>
                                            <td style="width: 111px; text-align: right;">
                                                <asp:Label ID="lblTaskName" runat="server" Font-Bold="True" 
                                                    Font-Names="Verdana" Font-Size="XX-Small" 
                                                    Style="text-align: left; margin-left: 0px" Text="Task Name" Width="69px" 
                                                    Height="16px"></asp:Label>
                                            </td>
                                            <td style="width: 157px; text-align: left;">
                                                <asp:TextBox ID="txtTaskName" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" Width="142px"></asp:TextBox>
                                            </td>
                                            <td style="width: 97px">
                                                <asp:CheckBox ID="chkTaskIsActive" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" Text="IsActive" Visible="false" />
                                            </td>
                                            <td style="width: 215px">
                                                <asp:Button ID="btnAdd" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" OnClick="btnAdd_Click" style="height: 20px" Text="Add" 
                                                    Width="30px" />
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="pnlGrdTask" runat="server" Height="185px" Width="413px">
                                        <div style="width: 435px; height: 242px; overflow: auto">
                                            <asp:GridView ID="GrdTaskName" runat="server" AutoGenerateColumns="False" 
                                                BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                                                CellPadding="3" Font-Names="Verdana" Font-Size="XX-Small" GridLines="Vertical" 
                                                Height="16px" OnRowCancelingEdit="GrdTaskName_RowCancelingEdit" 
                                                OnRowEditing="GrdTaskName_RowEditing" OnRowUpdating="GrdTaskName_RowUpdating" 
                                                Width="332px">
                                                <AlternatingRowStyle BackColor="#DCDCDC" />
                                                <FooterStyle BackColor="#CCCCCC" Font-Size="XX-Small" ForeColor="Black" />
                                                <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                                                <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#000065" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ActivityFor CompanyId" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblActivityForCompanyId" runat="server" Font-Names="Verdana" 
                                                                Font-Size="XX-Small" Text='<%# Eval("ActivityForCompanyId") %>'>

                 
                                                                </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtActivityForCompanyId" runat="server" Font-Names="Verdana" 
                                                                Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("ActivityForCompanyId") %>'>

                                                                </asp:TextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="20px" />
                                                        <ItemStyle Width="180px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NonCompliance TaskId" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNonComplianceTaskId" runat="server" Font-Names="Verdana" 
                                                                Font-Size="XX-Small" Text='<%# Eval("NonComplianceTaskId") %>'>


                                                                </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtNonComplianceTaskId" runat="server" Font-Names="Verdana" 
                                                                Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("NonComplianceTaskId") %>'>

                                                                </asp:TextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="20px" />
                                                        <ItemStyle Width="180px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Task Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblComplianceTaskName" runat="server" 
                                                                Text='<%# Eval("ComplianceTaskName") %>'>

                                                                </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtComplianceTaskName" runat="server" Font-Names="Verdana" 
                                                                Font-Size="XX-Small" Text='<%# Bind("ComplianceTaskName") %>' Width="250px">

                                                                </asp:TextBox>
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="250px" />
                                                        <ItemStyle Width="250px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is  Active">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkTaskActive" runat="server" 
                                                                Checked='<%# Eval("IsActive") %>' Enabled="false" Font-Names="Verdana" 
                                                                Font-Size="XX-Small" />
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkTaskIsActive" runat="server" 
                                                                Checked='<%# Bind("IsActive") %>' Font-Names="Verdana" Font-Size="XX-Small" />
                                                        </EditItemTemplate>
                                                        <HeaderStyle Width="40px" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                                                    <asp:CommandField HeaderText="Delete" ShowDeleteButton="false" 
                                                        Visible="False" />
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
               <table>
                <tr>
                    <td style="width: 132px">
                    </td>
                    <td style="width: 132px">
                        <asp:HiddenField ID="HidDeleteCount" runat="server" Value="0" />
                    </td>
                    <td>
                        <asp:HiddenField ID="HidUpdateCount" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
    </div>
    </div>
</asp:Content>
