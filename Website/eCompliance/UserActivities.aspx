<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="UserActivities.aspx.cs" Inherits="UserActivities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="pnlGrd" runat="server" Height="545px">
                                 <asp:GridView ID="GrdActivity" runat="server" CellPadding="3" 
                        Font-Size="X-Small" Width="730px" Font-Names="arial" AutoGenerateColumns="False" 
                        Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                            BorderStyle="None" BorderWidth="1px" AllowPaging="True" AllowSorting="True" 
                                     onpageindexchanging="GrdActivity_PageIndexChanging">
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
                            <asp:BoundField DataField="ActName" HeaderText="Act Name" >
                            <HeaderStyle Width="800px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ActivityName" HeaderText="Activity Name" >
           
                            <HeaderStyle Width="600px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SeverityName" HeaderText="Severity Name" />
           
                            <%--<asp:BoundField DataField="FrequencyName" HeaderText="Frequency Name" />--%>
                            <asp:BoundField DataField="DepartmentName" HeaderText="Department Name" />
                            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                           <%-- <asp:BoundField DataField="DueMonth" HeaderText="Due Month" />
                            <asp:BoundField DataField="DueDate" HeaderText="Due Date" />
           
                            <asp:BoundField DataField="ActivityForm" HeaderText="Activity Form" />--%>
           
                    </Columns>
                    <sortedascendingcellstyle backcolor="#F1F1F1" />
                    <sortedascendingheaderstyle backcolor="#0000A9" />
                    <sorteddescendingcellstyle backcolor="#CAC9C9" />
                    <sorteddescendingheaderstyle backcolor="#000065" />
                </asp:GridView>
                            </asp:Panel>

</asp:Content>

