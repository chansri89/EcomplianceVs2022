<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true"
    CodeFile="LocationDepartmentMaster.aspx.cs" Inherits="LocationDepartmentMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="overflow: auto; width: 1026px; height: 844px;">      
        <asp:Panel ID="Pnlgv" runat="server" Height="282px" Width="996px">
            <div style="overflow: auto; height: 263px; width: 867px">
                <asp:GridView ID="GrdLocationDeptMaster" runat="server" CellPadding="3" Font-Size="XX-Small"
                    Width="835px" Font-Names="Verdana" AutoGenerateColumns="False" Height="23px"
                    GridLines="Vertical" BackColor="White" BorderColor="#999999" BorderStyle="None"
                    BorderWidth="1px" OnRowCancelingEdit="GrdLocationDeptMaster_RowCancelingEdit"
                    OnRowEditing="GrdLocationDeptMaster_RowEditing" 
                    OnRowUpdating="GrdLocationDeptMaster_RowUpdating">
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                        <asp:TemplateField HeaderText="LocationDeptId" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblLocationDeptId" runat="server" Text='<%# Eval("LocationDeptId") %>'
                                    Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtLocationDeptId" runat="server" Text='<%# Bind("LocationDeptId") %>'
                                    ReadOnly="true" Font-Names="Verdana" Font-Size="XX-Small">
                                </asp:TextBox></EditItemTemplate>
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Company Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>' Font-Names="Verdana"
                                    Font-Size="XX-Small"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCompanyCode" runat="server" Text='<%# Bind("CompanyCode") %>'
                                    ReadOnly="true" Font-Names="Verdana" Font-Size="XX-Small">
                                </asp:TextBox></EditItemTemplate>
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="LocationName">
                            <ItemTemplate>
                                <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>' Font-Names="Verdana"
                                    Font-Size="XX-Small" Width="100px"></asp:Label>
                            </ItemTemplate>
                          
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Responsible GroupName" Visible="True">
                            <ItemTemplate>
                                <asp:Label ID="lblResponsibleGrpName" runat="server" Text='<%# Eval("ResponsibleGrpName") %>' Font-Names="Verdana"
                                    Font-Size="XX-Small" Width="80px"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtResponsibleGrpName" runat="server" Text='<%# Bind("ResponsibleGrpName") %>'
                                    ReadOnly="false" Font-Names="Verdana" Font-Size="XX-Small" Width="80px">
                                </asp:TextBox></EditItemTemplate>
                            <HeaderStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DepartmentId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartmentId" runat="server" Text='<%# Eval("DepartmentId") %>'
                                    Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDepartmentId" runat="server" Text='<%# Bind("DepartmentId") %>'
                                    ReadOnly="true" Font-Names="Verdana" Font-Size="XX-Small">
                                </asp:TextBox>
                            </EditItemTemplate>
                            <HeaderStyle Width="20px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Department Name">
                            <ItemTemplate>
                                <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("DepartmentName") %>'
                                    Font-Names="Verdana" Font-Size="XX-Small" Width="100px"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlDepartment" runat="server" DataValueField="DepartmentId"
                                    DataTextField="DepartmentName" DataSource='<%#getDepartment() %>' Font-Names="Verdana"  Width="100px"
                                    Font-Size="XX-Small">                                   
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle Width="100px" />
                             <ItemStyle Width="100px" />
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="Responsable Person">
                            <ItemTemplate>
                                <asp:Label ID="lblExecutionPerson" runat="server" Text='<%# Eval("ExecutionEmployeeName") %>' Width="80px"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlExecutionPerson" runat="server" Font-Names="Verdana" Font-Size="XX-Small"
                                    DataValueField="EmployeeCode" DataTextField="EmployeeName" DataSource='<%#getExecutionPerson() %>' Width="80px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reporting Officer">
                            <ItemTemplate>
                                <asp:Label ID="lblReviewPerson" runat="server" Text='<%# Eval("ReviewEmployeeName") %>' Width="80px"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlReviewPerson" runat="server" DataSource="<%#getReviewPerson() %>" Width="80px"
                                    DataTextField="EmployeeName" DataValueField="EmployeeCode" Font-Names="Verdana"
                                    Font-Size="XX-Small">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle Width="100px"/>
                            <ItemStyle Width="100px"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reviewing Officer">
                            <ItemTemplate>
                                <asp:Label ID="lblHeadPerson" runat="server" Text='<%# Eval("HeadEmployeeName") %>' Width="80px"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlHeadPerson" runat="server" DataSource="<%#getHeadPerson() %>" Width="80px"
                                    DataTextField="EmployeeName" DataValueField="EmployeeCode" Font-Names="Verdana"
                                    Font-Size="XX-Small">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle Width="100px" />
                            <ItemStyle Width="100px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location Head" Visible="true">
                            <%--added by Abinayaa 08112 after 071112 Meeting--%>
                            <ItemTemplate>
                                <asp:Label ID="lblUtimate" runat="server" Text='<%# Eval("UltimateEmployeeName") %>' Width="80px">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlUtimate" runat="server" DataSource="<%#getUtimate() %>" Width="80px"
                                    DataTextField="EmployeeName" DataValueField="EmployeeCode" Font-Names="Verdana"
                                    Font-Size="XX-Small">
                                </asp:DropDownList>
                            </EditItemTemplate>
                            <HeaderStyle Width="100px" />
                             <ItemStyle Width="100px" />
                        </asp:TemplateField>
                      
                        <asp:CommandField HeaderText="Edit" ShowEditButton="True">
                            <HeaderStyle Width="20px" />
                        </asp:CommandField>
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="false" Visible="false">
                            <HeaderStyle Width="30px" />
                        </asp:CommandField>
                    </Columns>
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />
                </asp:GridView>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlAdd" runat="server" Width="813px" Height="443px" 
            Style="margin-top: 0px">
            <asp:TextBox ID="txtActDetailId" runat="server"  Visible="false" CssClass="txtbox">
                </asp:TextBox>
            <table style="width: 50%; height: 193px;">
                <tr>
                    <td style="width: 54px">
                        <asp:Label ID="lblLocationName" runat="server" Text="Location Name" 
                            Width="130px" Font-Bold="True" Font-Size="XX-Small" Font-Names="Verdana"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLocationName" runat="server" CssClass="ddl"
                            AutoPostBack="True" DataValueField="Companycode" Font-Names="Verdana"
                            DataTextField="Companyname" Width="124px"><%--OnSelectedIndexChanged="ddlLocationName_SelectedIndexChanged" --%>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 54px">
                        <asp:Label ID="lblResponsibleGroupName" runat="server" 
                            Text="Responsible Group Name" Width="145px" Font-Bold="True" 
                            Font-Size="XX-Small" Font-Names="Verdana"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtResponsibleGrpName" runat="server" ReadOnly="false" 
                            Font-Names="Verdana" Font-Size="XX-Small" Width="172px" ></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 54px">
                        <asp:Label ID="lblDepartment" runat="server" Text="Department Name" Width="130px" Font-Bold="True" Font-Size="XX-Small" Font-Names="Verdana"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDepartment" runat="server" DataTextField="DepartmentName"
                            DataValueField="DepartmentId" CssClass="ddl" Font-Names="Verdana" 
                            Height="16px" Width="124px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 54px">
                        <asp:Label ID="lblResponsiblePerson" runat="server" Text="Responsible Person" 
                            Width="130px" Font-Bold="True" Font-Size="XX-Small" Height="16px" Font-Names="Verdana"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlResponsablePerson" runat="server"
                            DataTextField="EmployeeName" DataValueField="EmployeeCode" CssClass="ddl" 
                            Font-Names="Verdana" Width="124px" Height="20px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 54px">
                        <asp:Label ID="lblReportingOfficer" runat="server" Text="Reporting Officer" Width="130px" Font-Bold="True" Font-Size="XX-Small" Font-Names="Verdana"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReportingOfficer" runat="server" 
                            DataTextField="EmployeeName" DataValueField="EmployeeCode" CssClass="ddl" 
                            Font-Names="Verdana" Width="124px" Height="20px">
                            
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 54px">
                        <asp:Label ID="lblReviewingOfficer" runat="server" Text="Reviewing Officer" 
                            Width="118px" Font-Bold="True" Font-Size="XX-Small" Font-Names="Verdana" 
                            Height="16px"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlReviewingOfficer" runat="server"
                            DataTextField="EmployeeName" DataValueField="EmployeeCode" CssClass="ddl" 
                            Font-Names="Verdana" Width="124px" Height="20px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 54px">
                        <asp:Label ID="lblCompanyHead" runat="server" Text="Location Head" Font-Bold="True" Width="130px" Font-Size="XX-Small" Font-Names="Verdana"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCompanyHead" runat="server" CssClass="ddl"
                            DataTextField="EmployeeName" DataValueField="EmployeeCode" 
                            Font-Names="Verdana" Width="124px" Height="20px" >
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td style="width: 54px">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="btnSave" runat="server" Text="Save"  OnClick="btnSave_Click" Font-Names="Verdana"
                            CssClass="btnsave" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
</asp:Content>
