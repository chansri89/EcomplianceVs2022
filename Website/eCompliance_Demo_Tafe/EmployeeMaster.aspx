<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="EmployeeMaster.aspx.cs" Inherits="EmployeeMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" Height="16px" Width="870px"> </asp:Panel>
    <asp:Label ID="lblEmpMas" runat="server" Align= "center" Text="Employee Master" 
        Font ="arial" width="787px"
        Font-Size ="12pt" Font-Bold="True" Font-Names="arial" 
        style="text-align: center"></asp:Label>
 <div style="overflow:auto; height: 644px; width: 1193px;">
   
    <asp:Panel ID="pnlEmployeeLoad" runat="server" Height="26px" Width="870px">
            <table style="width: 100%; height: 23px;">
                <tr>
                    
                    <td style="width: 117px; ">
                        <asp:Label ID="lblEmpLoad" runat="server" Text="Employee Name" Font-Names="arial" 
                            Font-Size="X-Small" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 196px" class="style21">
                        <asp:DropDownList ID="ddlLoadEmp" runat="server" Font-Names="arial" 
                            DataTextField="EmployeeName" Size="X-Small" 
                            AutoPostBack="True" Font-Size="X-Small" Height="16px" Width="144px">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnGo" runat="server" Text="Go" Font-Names="arial" 
                            Font-Size="X-Small" onclick="btnGo_Click"  />
                            
                    </td>
                  
                </tr>
            </table>
        </asp:Panel>
    <asp:panel ID="Pnlgv" runat="server" Height="230px" Width="933px">
        <div style="overflow:auto; height:234px; width:994px">
    <asp:GridView ID="GrdEmployeeMaster" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="943px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdEmployeeMaster_RowCancelingEdit" 
                onrowdeleting="GrdEmployeeMaster_RowDeleting" 
                onrowediting="GrdEmployeeMaster_RowEditing" 
                onrowupdating="GrdEmployeeMaster_RowUpdating" >
                
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
            <asp:TemplateField HeaderText="Employee Code" >
            <ItemTemplate>
            <asp:Label ID="lblEmployeeCode" runat="server" Text='<%# Eval("EmployeeCode") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtEmployeeCode" runat="server" Text='<%# Bind("EmployeeCode") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" Enabled="false" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Employee Name" >
            <ItemTemplate>
            <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("EmployeeName") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtEmpName" runat="server" Text='<%# Bind("EmployeeName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="90px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="EmailId">
             <ItemTemplate>
            <asp:Label ID="lblEmailId" runat="server" Text='<%# Eval("EmailId") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtEmailId" runat="server" Text='<%# Bind("EmailId") %>' Font-Names="arial" Font-Size="X-Small" Width="150px"></asp:TextBox></EditItemTemplate>
                 <HeaderStyle Width="180px" />
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Location Name">
             <ItemTemplate>
            <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
                 <asp:DropDownList ID="ddlCompanyName" runat="server" DataValueField="CompanyCode" DataTextField="CompanyName"   DataSource='<%#getCompanyName() %>'
                 Font-Names="arial" Font-Size="X-Small">
                 </asp:DropDownList>
            </EditItemTemplate>  <HeaderStyle Width="80px" />
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Manager Name">
             <ItemTemplate>
            <asp:Label ID="lblManagerName" runat="server" Text='<%# Eval("ManagerName") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
                 <asp:DropDownList ID="ddlManagerName" runat="server" DataValueField="EmployeeCode" DataTextField="EmployeeName"   DataSource='<%#getManagerName() %>'
                 Font-Names="arial" Font-Size="X-Small">
                
                 </asp:DropDownList>
            </EditItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Activity Designation">
            <ItemTemplate>
            <asp:Label ID="lblActivityDesignation" runat="server" Text='<%# Eval("ActivityDesignation") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtActivityDesignation" runat="server" Font-Names="arial" Font-Size="X-Small" Text='<%# Bind("ActivityDesignation") %>'>
            </asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>
           
          <asp:TemplateField HeaderText="Is Company Admin">
             <ItemTemplate>
          <%--  <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive") %>'
             Font-Names="arial" Font-Size="X-Small"></asp:Label>--%>
              <asp:CheckBox ID="chkCompanyAdmin" runat="server" Checked='<%# Eval("IsCompanyAdmin") %>'
            Font-Names="arial" Font-Size="X-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkCompanyAdmin" runat="server" Checked='<%# Bind("IsCompanyAdmin") %>' 
            Font-Names="arial" Font-Size="X-Small"></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="30px" />  <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

          <asp:TemplateField HeaderText="Is Group Level Person">
             <ItemTemplate>
          <%--  <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive") %>'
             Font-Names="arial" Font-Size="X-Small"></asp:Label>--%>
              <asp:CheckBox ID="chkGroupLevel" runat="server" Checked='<%# Eval("IsGroupLevel") %>'
            Font-Names="arial" Font-Size="X-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkIsGroupLevel" runat="server" Checked='<%# Bind("IsGroupLevel") %>' 
            Font-Names="arial" Font-Size="X-Small"></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="30px" />  <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="IsActive">
             <ItemTemplate>
          <%--  <asp:Label ID="lblIsActive" runat="server" Text='<%# Eval("IsActive") %>'
             Font-Names="arial" Font-Size="X-Small"></asp:Label>--%>
              <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>'
            Font-Names="arial" Font-Size="X-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>'
            Font-Names="arial" Font-Size="X-Small"></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="30px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            
            <asp:CommandField HeaderText="Edit" ShowEditButton="True" 
                CausesValidation="False" >
            <HeaderStyle Width="40px" />
            </asp:CommandField>
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" >
            <HeaderStyle Width="40px" />
            </asp:CommandField>
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
    <br />
    <asp:panel ID="pnlAdd" runat="server" Width="811px" >
    <table style="width: 61%">
            <tr>
                <td style="width: 129px; text-align: left;">
                    <asp:Label ID="lblEmployeeCode" runat="server" Text="Employee Code"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 134px">
                    <asp:TextBox ID="txtEmployeeCode" runat="server" Font-Names="arial"  MaxLength="8"
                        Font-Size="X-Small" Width="124px"></asp:TextBox>
                </td>

            </tr>
             <tr>
                <td style="width: 129px; text-align: left;">
                    <asp:Label ID="lblEmpname" runat="server" Text="Employee Name"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 134px">
                    <asp:TextBox ID="txtEmpname" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Height="16px" Width="124px"></asp:TextBox>
                </td>

            </tr>
             
             <tr>
                <td style="width: 129px; text-align: left;">
                    <asp:Label ID="lblEmailid" runat="server" Text="EmailId"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 134px">
                    <asp:TextBox ID="txtEmailId" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Height="16px" Width="124px"></asp:TextBox>
                </td>
                <td style="width: 220px">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="txtEmailId" ErrorMessage="Enter EmailId in Correct Format" 
                        Font-Names="arial" Font-Size="X-Small" ForeColor="#FF3300" 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                 </td>
            </tr>
            <tr>
                <td style="width: 129px; text-align: left;">
                    <asp:Label ID="lblUserPassword" runat="server" Text="Password"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 134px">
                    <asp:TextBox ID="txtPassword" runat="server" Font-Names="arial" Width="124px" 
                        Font-Size="X-Small" TextMode="Password" Height="16px"></asp:TextBox>
                </td>

            </tr>
            <tr>
                <td style="width: 129px; text-align: left;">
                    <asp:Label ID="lblCompanyName" runat="server" Text="Location Name"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 134px">
                    <asp:DropDownList ID="ddlCompanyName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Height="16px" Width="133px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 129px; text-align: left;">
                    <asp:Label ID="lblManagerName" runat="server" Text="Manager Name"  
                        Font-Names="arial" Font-Size="X-Small"></asp:Label>
                </td>
                <td style="width: 134px">
                    <asp:DropDownList ID="ddlManagerName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Height="16px" Width="133px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 129px; text-align: left;">
                    <asp:Label ID="lblActivityDesignation" runat="server" 
                        Text="Position"  Font-Names="arial" Font-Size="X-Small" 
                        Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 134px">
                    <asp:TextBox ID="txtActivityDesignation" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Width="124px" Height="16px"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td></td>
                <td style="width: 129px; text-align: left;">
                    <asp:CheckBox ID="ChkIsCompanyAdmin" runat="server" Font-Names="arial" 
                            Font-Size="X-Small" Text="Company Admin" Visible="True" />
                </td>

                <td style="width: 129px; text-align: left;">
                    <asp:CheckBox ID="IsGroupLevel" runat="server" Font-Names="arial" 
                            Font-Size="X-Small" Text="GroupLevel Person" Visible="True" />
                </td>
         
           </tr>
            <tr>

                <td style="width: 129px; text-align: left;">
                    <asp:CheckBox ID="chkIsActive" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Text="IsActive" Visible="False" />
                </td>
                <td style="width: 134px">
                    <asp:Button ID="btnSave" runat="server" Font-Names="arial" Text="Save" 
                        onclick="btnSave_Click" Font-Size="X-Small" style="height: 20px" />
                </td>
                <td style="width: 220px"> <asp:HiddenField ID="HidDeleteCount" Value="0" runat="server" />
                <asp:HiddenField ID="HidUpdateCount" Value="0" runat="server" />
                </td>
            </tr>
            
        </table>
    </asp:panel>
    
    </div>
</asp:Content>

