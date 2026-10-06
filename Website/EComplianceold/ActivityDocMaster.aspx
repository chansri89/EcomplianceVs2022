<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage3.master" AutoEventWireup="true" CodeFile="ActivityDocMaster.aspx.cs" Inherits="ActivityDocMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel id="pnlActivityDocm" runat="server" Height="852px">
<asp:Panel ID="pnlCompany" runat="server" Height="26px" Width="818px">
                <table style="width: 100%; height: 26px;">
                    <tr>
                        <td style="width: 167px; text-align: right;">
                            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name" Font-Names="Verdana"  Visible="false"
                                Font-Size="XX-Small"></asp:Label>
                        </td>
                        <td style="width: 98px">
                            <asp:DropDownList ID="ddlCompanyName" runat="server" Font-Names="Verdana"  Visible="false"
                                Font-Size="XX-Small">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 1050px; text-align: right;">
                            <asp:Label ID="lblActName" runat="server" Text="Act Name" Font-Names="Verdana" 
                                Font-Size="XX-Small"></asp:Label>
                        </td>
                        <td style="width: 673px">
                            <asp:DropDownList ID="ddlActName" runat="server" Font-Names="Verdana" 
                                Font-Size="XX-Small">
                            </asp:DropDownList>
                        </td>
                        <td class="style21" style="width: 666px">
                            <asp:Button ID="btnGo" runat="server" Text="Go" Font-Names="Verdana" 
                                Font-Size="XX-Small" onclick="btnGo_Click" />
                        </td>
                                            <td align="right" style="text-align: right">
                <asp:Button ID="btnCompActivitiesHide" runat="server" Text="- Collapse"
                    Width="70px" BackColor="#00006A" Height="17px" Font-Names="Verdana" 
                            Font-Size="XX-Small" Visible="False" 
                            ForeColor="White" style="margin-left: 49px" onclick="btnCompActivitiesHide_Click"  /> </td><%--ToolTip="Click Expand Hide the CompanyActivity Grid and Show other Grids"--%> 
                        
                    </tr></table>

            </asp:Panel>
<br />
<asp:Panel ID="pnlGrdCompActivities" runat="server" Height="197px" Width="1080px" >
            <asp:Panel ID="pnlGridCompActivities" runat="server" Width="1055px" 
                Height="168px">
              <div style="width: 1050px; overflow:auto; height: 189px;">
                <asp:GridView ID="GrdCompActivities" runat="server" AutoGenerateColumns="False" 
                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                    CellPadding="3" Font-Names="Verdana" Font-Size="XX-Small" GridLines="Vertical" 
                    Height="72px" Width="1044px"
                     AllowSorting="True" onrowcommand="GrdCompActivities_RowCommand" >
                    <%--  onrowdatabound="GrdCompActivities_RowDataBound" >--%>
                  <%--onrowediting="GrdCompActivities_RowEditing" --%>
                    <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
                        HorizontalAlign="Left" />
                    <AlternatingRowStyle BackColor="#DCDCDC" />
                    <Columns>
         <asp:ButtonField CommandName="Select" HeaderText="Select" Text="Select">
               <HeaderStyle Height="10px" />
               <ItemStyle Width="10px" />
         </asp:ButtonField>
        <asp:TemplateField HeaderText="ActivityId" Visible="False">
               <ItemTemplate>
                    <asp:Label ID="lblActivityId" runat="server" Font-Names="Verdana" 
                                    Font-Size="XX-Small" Text='<%# Eval("ActivityId") %>'>
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

        <asp:TemplateField HeaderText="Activity Name">
             <ItemTemplate>
            <asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("ActivityName") %>' ToolTip="Click Select For Saving Documents"></asp:Label></ItemTemplate>
            <EditItemTemplate>
            <asp:TextBox ID="txtActivityName" runat="server" Text='<%# Bind("ActivityName") %>'  Font-Names="Verdana" ReadOnly="true" Font-Size="XX-Small" Width="180px"></asp:TextBox>
                 
            </EditItemTemplate>
                <HeaderStyle Width="180px" />
                 <ItemStyle Width="180px" />
        </asp:TemplateField>
                <asp:TemplateField HeaderText="Company Code">
             <ItemTemplate>
            <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>' ></asp:Label></ItemTemplate>
                <HeaderStyle Width="25px" />
                 <ItemStyle Width="25px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Due Month">
            <ItemTemplate>
                <asp:Label ID="lblDueMonth" runat="server" Font-Names="Verdana" 
                    Font-Size="XX-Small" Text='<%# Eval("DueMonth") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
            <asp:DropDownList ID="ddldueMonth" runat="server"  DataValueField="Month" DataTextField="MonthName"   DataSource='<%#LoadMonth() %>'
                 Font-Names="Verdana" Font-Size="XX-Small">
                 </asp:DropDownList>
            </EditItemTemplate>
            <HeaderStyle Width="15px" />
            <ItemStyle Width="15px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Due Date">
            <ItemTemplate>
                <asp:Label ID="lblDueDate" runat="server" Font-Names="Verdana" 
                    Font-Size="XX-Small" Text='<%# Eval("DueDate") %>' >

                    </asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlDueDate" runat="server" Font-Names="Verdana" 
                    Font-Size="XX-Small"> 
           <asp:ListItem value="0" Text="NA"></asp:ListItem>
            <asp:ListItem value="1" Text="1"></asp:ListItem>
            <asp:ListItem value="2" Text="2"></asp:ListItem>
            <asp:ListItem value="3" Text="3"></asp:ListItem>
            <asp:ListItem value="4" Text="4"></asp:ListItem>
            <asp:ListItem value="5" Text="5"></asp:ListItem>
            <asp:ListItem value="6" Text="6"></asp:ListItem>
            <asp:ListItem value="7" Text="7"></asp:ListItem>
            <asp:ListItem value="8" Text="8"></asp:ListItem>
            <asp:ListItem value="9" Text="9"></asp:ListItem>
            <asp:ListItem value="10" Text="10"></asp:ListItem>            
            <asp:ListItem value="11" Text="11"></asp:ListItem>
             <asp:ListItem value="12" Text="12"></asp:ListItem>
            <asp:ListItem value="13" Text="13"></asp:ListItem>
            <asp:ListItem value="14" Text="14"></asp:ListItem>
            <asp:ListItem value="15" Text="15"></asp:ListItem>
            <asp:ListItem value="16" Text="16"></asp:ListItem>
            <asp:ListItem value="17" Text="17"></asp:ListItem>
            <asp:ListItem value="18" Text="18"></asp:ListItem>
            <asp:ListItem value="19" Text="19"></asp:ListItem>
            <asp:ListItem value="20" Text="20"></asp:ListItem>
            <asp:ListItem value="21" Text="21"></asp:ListItem>
              <asp:ListItem value="22" Text="22"></asp:ListItem>
            <asp:ListItem value="23" Text="23"></asp:ListItem>
            <asp:ListItem value="24" Text="24"></asp:ListItem>
            <asp:ListItem value="25" Text="25"></asp:ListItem>
            <asp:ListItem value="26" Text="26"></asp:ListItem>
            <asp:ListItem value="27" Text="27"></asp:ListItem>
            <asp:ListItem value="28" Text="28"></asp:ListItem>
            <asp:ListItem value="29" Text="29"></asp:ListItem>
            <asp:ListItem value="30" Text="30"></asp:ListItem>
            <asp:ListItem value="31" Text="31"></asp:ListItem>
                   
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
                   <asp:DropDownList ID="ddlDueDay" runat="server" Font-Names="Verdana"  DataTextField="Day" DataValueField="Number" DataSource="<%#LoadDays() %> " Width="80px"
                    Font-Size="XX-Small"> 
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
                <asp:DropDownList ID="ddlTrigMonth" runat="server" Font-Names="Verdana" DataSource="<%#LoadMonth() %>" DataTextField="MonthName" 
                    DataValueField="Month" 
                    Font-Size="XX-Small">
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
                <asp:DropDownList ID="ddlTrigDate" runat="server" Font-Names="Verdana"  Font-Size="XX-Small">
            <asp:ListItem value="0" Text="NA"></asp:ListItem>
            <asp:ListItem value="1" Text="1"></asp:ListItem>
            <asp:ListItem value="2" Text="2"></asp:ListItem>
            <asp:ListItem value="3" Text="3"></asp:ListItem>
            <asp:ListItem value="4" Text="4"></asp:ListItem>
            <asp:ListItem value="5" Text="5"></asp:ListItem>
            <asp:ListItem value="6" Text="6"></asp:ListItem>
            <asp:ListItem value="7" Text="7"></asp:ListItem>
            <asp:ListItem value="8" Text="8"></asp:ListItem>
            <asp:ListItem value="9" Text="9"></asp:ListItem>
            <asp:ListItem value="10" Text="10"></asp:ListItem>            
            <asp:ListItem value="11" Text="11"></asp:ListItem>
             <asp:ListItem value="12" Text="12"></asp:ListItem>
            <asp:ListItem value="13" Text="13"></asp:ListItem>
            <asp:ListItem value="14" Text="14"></asp:ListItem>
            <asp:ListItem value="15" Text="15"></asp:ListItem>
            <asp:ListItem value="16" Text="16"></asp:ListItem>
            <asp:ListItem value="17" Text="17"></asp:ListItem>
            <asp:ListItem value="18" Text="18"></asp:ListItem>
            <asp:ListItem value="19" Text="19"></asp:ListItem>
            <asp:ListItem value="20" Text="20"></asp:ListItem>
            <asp:ListItem value="21" Text="21"></asp:ListItem>
              <asp:ListItem value="22" Text="22"></asp:ListItem>
            <asp:ListItem value="23" Text="23"></asp:ListItem>
            <asp:ListItem value="24" Text="24"></asp:ListItem>
            <asp:ListItem value="25" Text="25"></asp:ListItem>
            <asp:ListItem value="26" Text="26"></asp:ListItem>
            <asp:ListItem value="27" Text="27"></asp:ListItem>
            <asp:ListItem value="28" Text="28"></asp:ListItem>
            <asp:ListItem value="29" Text="29"></asp:ListItem>
            <asp:ListItem value="30" Text="30"></asp:ListItem>
            <asp:ListItem value="31" Text="31"></asp:ListItem>
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
                    <asp:DropDownList ID="ddlTrigDay" runat="server" Font-Names="Verdana" DataTextField="Day" DataValueField="Number" DataSource="<%#LoadDays() %> " Width="80px"
                    Font-Size="XX-Small">
                    </asp:DropDownList>
            </EditItemTemplate>
              <HeaderStyle Width="20px" />
                            
            <ItemStyle Width="20px" />

        </asp:TemplateField>
        <asp:TemplateField HeaderText="Execution Person">
            <ItemTemplate>
                <asp:Label ID="lblExecutionPerson" runat="server" 
                    Text='<%# Eval("Executioner") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlExecutionPerson" runat="server" Font-Names="Verdana" 
                    Font-Size="XX-Small" DataValueField="EmployeeCode" DataTextField="EmployeeName"   DataSource='<%#getExecutionPerson() %>'>
                </asp:DropDownList>
            </EditItemTemplate>
            <HeaderStyle Width="30px" />
            <ItemStyle Width="30px" />
        </asp:TemplateField>
      <asp:TemplateField HeaderText="Review Person">
            <ItemTemplate>
                <asp:Label ID="lblReviewPerson" runat="server" 
                    Text='<%# Eval("Reviewer") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList ID="ddlReviewPerson" runat="server"  DataSource="<%#getReviewPerson() %>" DataTextField="EmployeeName" 
                    DataValueField="EmployeeCode" Font-Names="Verdana" Font-Size="XX-Small">
                </asp:DropDownList>
            </EditItemTemplate>
            <HeaderStyle Width="30px" />
            <ItemStyle Width="30px" />
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Head Person">
            <ItemTemplate>
                <asp:Label ID="lblHeadPerson" runat="server" Text='<%# Eval("HeadEmployeeName") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
               <asp:DropDownList ID="ddlHeadPerson" runat="server" 
                    DataSource="<%#getHeadPerson() %>" DataTextField="EmployeeName" 
                    DataValueField="EmployeeCode" Font-Names="Verdana" Font-Size="XX-Small">
                </asp:DropDownList>
            </EditItemTemplate>
            <HeaderStyle Width="30px" />
            <ItemStyle Width="30px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Utimate" Visible="False">
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
                <asp:Label ID="lblFrequencey" runat="server" 
                    Text='<%# Eval("FrequencyName") %>'>
              </asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                    <asp:DropDownList ID="ddlFrequencey" runat="server" 
                    DataSource="<%#getFrequencey() %>" DataTextField="FrequencyName" 
                    DataValueField="FrequencyId" Font-Names="Verdana" Font-Size="XX-Small">
                </asp:DropDownList>
            </EditItemTemplate>
             <HeaderStyle Width="30px" />
                            
            <ItemStyle Width="30px" />
                            
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Is  Active">
             <ItemTemplate>
              <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>' Font-Names="Verdana" Font-Size="XX-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>' Font-Names="Verdana" Font-Size="XX-Small"></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="20px" />
                <ItemStyle Width="20px" HorizontalAlign="Center" />
            </asp:TemplateField>
                        <asp:CommandField HeaderText="Edit" ShowEditButton="True" Visible="False" >
                        <HeaderStyle Width="90px" />
                        <ItemStyle Width="90px" />
                        </asp:CommandField>
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="false" 
                            Visible="False" />
                    </Columns>
                    <sortedascendingcellstyle backcolor="#F1F1F1" />
                    <sortedascendingheaderstyle backcolor="#0000A9" />
                    <sorteddescendingcellstyle backcolor="#CAC9C9" />
                    <sorteddescendingheaderstyle backcolor="#000065" />

                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#0000A9" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#000065" />

                </asp:GridView>
               </div>
            </asp:Panel>
            </asp:Panel>
            <asp:Panel ID="pnlCompanyAct" runat="server" Width="890px" 
        Enabled="False" Visible="False" ><%--BorderWidth="1px"--%>
<table style="width: 118%">
<tr>
<td class="style21" style="text-align: right; width: 136px;">
    <asp:Label ID="lblCmpName" runat="server" Text="Company Name"  Visible="false" 
        Font-Names="Verdana" Font-Size="XX-Small" Font-Bold="True"></asp:Label>
</td>
<td style="width: 89px">
    <asp:TextBox ID="txtCmpName" runat="server" Font-Names="Verdana"   Visible="false"
        Font-Size="XX-Small" ReadOnly="true" Width="112px" Font-Bold="True"></asp:TextBox>
</td>
<td style="width: 35px; text-align: right;" class="style22">
    <asp:Label ID="lbAct" runat="server" Text="Act" Font-Names="Verdana" 
        Font-Size="XX-Small" Font-Bold="True" ></asp:Label>
</td>
<td style="width: 175px">
    <asp:TextBox ID="txtAct" runat="server" Width="214px" Font-Names="Verdana" 
        Font-Size="XX-Small" ReadOnly="true" Font-Bold="True"></asp:TextBox>
</td>
<td style="width: 111px; text-align: right;" class="style21">
    <asp:Label ID="lblActivities" runat="server" Text="Activity Name" 
        Font-Names="Verdana" Font-Size="XX-Small" Font-Bold="True"></asp:Label>
</td>
<td style="width: 259px">
    <asp:TextBox ID="txtActivities" runat="server" Width="209px" 
        Font-Names="Verdana" Font-Size="XX-Small" ReadOnly="true" Font-Bold="True"></asp:TextBox>
</td>
    <td>
        <asp:TextBox ID="txtActDocId" runat="server" Visible="False" Width="31px"></asp:TextBox>
        <asp:TextBox ID="txtActivityId" runat="server" Enabled="False" 
            Font-Names="Verdana" Font-Size="XX-Small" Width="30px"></asp:TextBox>
    </td>
</tr>
</table>
</asp:Panel>
<br />
            <asp:panel ID="pnlAdd" runat="server" Width="1044px"  
            style="margin-top: 0px" Height="399px" Visible="False">

                <table style="width: 100%; height: 353px;">
                    <tr>
                        <td style="height: 414px; width: 418px;">
                            <asp:Panel ID="pnlActivityDocument" runat="server" Height="373px" Width="417px">
                                <table style="width: 91%">
                                <tr>
                                        <td class="style1" style="width: 210px; text-align: right;">
                                            <asp:Label ID="lblAccessPath" runat="server" Font-Names="Verdana" 
                                                Font-Size="XX-Small" Text="File Access Path" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 121px">
                                            <asp:FileUpload ID="fluAccessPath" runat="server" Font-Names="Verdana" 
                                                Font-Size="XX-Small" style="text-align: right" Width="230px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1" style="width: 210px; text-align: right;">
                                            <asp:Label ID="lblDocumentName" runat="server" Font-Names="Verdana" 
                                                Font-Size="XX-Small" Text="Document Name" Visible="false"></asp:Label>
                                        </td>
                                        <td style="width: 121px">
                                            <asp:TextBox ID="txtDocumentName" runat="server" Font-Names="Verdana" 
                                                Font-Size="XX-Small" ReadOnly="True" Visible="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1" style="width: 210px; text-align: right;">
                                            <asp:Label ID="lblDocType" runat="server" Font-Names="Verdana" 
                                                Font-Size="XX-Small" Text="Document Type" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 121px">
                                            <asp:DropDownList ID="ddlDocType" runat="server" Font-Names="Verdana" 
                                                Font-Size="XX-Small">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="style1" style="width: 210px; text-align: right;">
                                            <asp:Label ID="lblLocation" runat="server" Font-Names="Verdana" 
                                                Font-Size="XX-Small" Text="Location" Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 121px">
                                            <asp:DropDownList ID="ddlLocation" runat="server" Font-Names="Verdana" 
                                                Font-Size="XX-Small">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style1" style="width: 210px; text-align: right;">
                                            <asp:Label ID="lblState" runat="server" Font-Names="Verdana" 
                                                Font-Size="XX-Small" Text="State " Font-Bold="True"></asp:Label>
                                        </td>
                                        <td style="width: 121px">
                                            <asp:DropDownList ID="ddlState" runat="server" Font-Names="Verdana" 
                                                Font-Size="XX-Small">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style1" style="width: 210px">
                                            <%--<asp:CheckBox ID="chkIsActive" runat="server" Font-Names="Verdana" 
                        Font-Size="XX-Small" Text="IsActive" Visible="False" />--%>
                                        </td>
                                        <td align="left" style="width: 121px">
                                            <asp:CheckBox ID="chkSaveIsActive" runat="server" Font-Names="Verdana" 
                                                Font-Size="XX-Small" Text="IsActive" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style1" style="width: 210px">
                                            &nbsp;</td>
                                        <td align="left" style="width: 121px">
                                            <asp:Button ID="btnSave" runat="server" Font-Names="Verdana" 
                                                Font-Size="XX-Small" onclick="btnSave_Click" Text="Save" 
                                                style="height: 20px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 210px">
                                            <asp:HiddenField ID="HidDeleteCount" runat="server" Value="0" />
                                            <asp:HiddenField ID="HidUpdateCount" runat="server" Value="0" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                        <td style="height: 414px">
                            <asp:Panel ID="pnlgrdActivityDoc" runat="server" Height="374px" Width="575px">
                            <div style="height: 172px; width: 560px; overflow:auto;">
                                <asp:GridView ID="GrdDocument" runat="server" AutoGenerateColumns="False" 
                                    BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="3" GridLines="Vertical" Height="89px" 
                                    OnRowEditing="GrdDocument_RowEditing" Width="538px" 
                                    onrowcommand="GrdDocument_RowCommand" Font-Names="Verdana" 
                                    Font-Size="XX-Small">
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
                                     <asp:TemplateField HeaderText="ActivityId" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityId" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" Text='<%# Eval("ActivityId") %>'>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </asp:Label>
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:TextBox ID="txtActivityId" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("ActivityId") %>'>                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <HeaderStyle Width="20px" />
                                            <ItemStyle Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ActivityDocumentId" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActivityDocumentId" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" Text='<%# Eval("ActivityDocumentId") %>'>
                                                
                                                </asp:Label>
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:TextBox ID="txtActivityDocumentId" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("ActivityDocumentId") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <HeaderStyle Width="20px" />
                                            <ItemStyle Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DocumentTypeId" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocumentTypeId" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" Text='<%# Eval("DocumentTypeId") %>'>
                                                                                              </asp:Label>
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:TextBox ID="txtDocumentTypeId" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("DocumentTypeId") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <HeaderStyle Width="20px" />
                                            <ItemStyle Width="180px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="LocationId" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLocationId" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" Text='<%# Eval("LocationId") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:TextBox ID="txtLocationId" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("LocationId") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <HeaderStyle Width="20px" />
                                            <ItemStyle Width="180px" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="StateId" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStateId" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" Text='<%# Eval("StateId") %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <%--<EditItemTemplate>
                                                <asp:TextBox ID="txtStateId" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" ReadOnly="true" Text='<%# Bind("StateId") %>'>
                                                </asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <HeaderStyle Width="20px" />
                                            <ItemStyle Width="180px" />
                                        </asp:TemplateField>

                                       <asp:CommandField HeaderText="View" Visible="true" ShowSelectButton="True" >
                                        <FooterStyle Font-Names="Verdana" Font-Size="XX-Small" />
                                        </asp:CommandField>
                                        <asp:TemplateField HeaderText="Document Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDocumentName" runat="server" 
                                                    Text='<%# Eval("DocumentName") %>'> 
                                                
                                                
                  </asp:Label>
                                            </ItemTemplate>
                                           <%-- <EditItemTemplate>
                                                <asp:TextBox ID="txtDocumentName" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" Text='<%# Bind("DocumentName") %>' Width="150px">
                                           
                    </asp:TextBox>
                                            </EditItemTemplate>--%>
                                            <HeaderStyle Width="150px" />
                                            <ItemStyle Width="150px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Document Type">
                                             <ItemTemplate>
                    <asp:Label ID="lblDocumentType" runat="server" 
                        Text='<%# Eval("DocumentType") %>'>
                    </asp:Label>
                    </ItemTemplate>
               <%-- <EditItemTemplate>
                <asp:TextBox ID="txtDocumentType" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" Text='<%# Bind("DocumentType") %>' Width="60px">
                                                    </asp:TextBox>--%>
                   <%-- <asp:DropDownList ID="ddlDocumentType" runat="server"  DataSource="<%#getReviewPerson() %>" DataTextField="DocumentType" 
                    DataValueField="DocumentTypeId" Font-Names="Verdana" Font-Size="XX-Small">
                </asp:DropDownList>--%>
              <%--  </EditItemTemplate>--%>
                                            <HeaderStyle Width="50px" />
                                        </asp:TemplateField>
                                         
                                         <asp:TemplateField HeaderText="Location">
            <ItemTemplate>
                <asp:Label ID="lblLocation" runat="server" 
                    Text='<%# Eval("Location") %>'></asp:Label>
            </ItemTemplate>
            <%--<EditItemTemplate>
             <asp:TextBox ID="txtLocation" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" Text='<%# Bind("Location") %>' >
                                                    </asp:TextBox>--%>
                <%--<asp:DropDownList ID="ddlLocation" runat="server"  DataSource="<%#getReviewPerson() %>" DataTextField="Location" 
                    DataValueField="LocationId" Font-Names="Verdana" Font-Size="XX-Small">
                </asp:DropDownList>--%>
           <%-- </EditItemTemplate>--%>
            <HeaderStyle Width="80px" />
            <ItemStyle Width="80px" />
        </asp:TemplateField>
          <asp:TemplateField HeaderText="State">
            <ItemTemplate>
                <asp:Label ID="lblState" runat="server" Text='<%# Eval("State") %>'></asp:Label>
            </ItemTemplate>
           <%-- <EditItemTemplate>
              <asp:TextBox ID="txtState" runat="server" Font-Names="Verdana" 
                                                    Font-Size="XX-Small" Text='<%# Bind("State") %>' Width="60px">
                                                    </asp:TextBox>--%>
              <%-- <asp:DropDownList ID="ddlState" runat="server" 
                    DataSource="<%#getHeadPerson() %>" DataTextField="State" 
                    DataValueField="StateId" Font-Names="Verdana" Font-Size="XX-Small">
                </asp:DropDownList>--%>
            <%--</EditItemTemplate>--%>
            <HeaderStyle Width="30px" />
            <ItemStyle Width="30px" />
        </asp:TemplateField>
         <asp:TemplateField HeaderText="IsActive">
             <ItemTemplate>
              <asp:CheckBox ID="chkDocIsActive" runat="server" Checked='<%# Eval("IsActive") %>' Font-Names="Verdana" Font-Size="XX-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
            <%-- <EditItemTemplate>
           <asp:CheckBox ID="chkDocumentIsActive" runat="server" Checked='<%# Bind("IsActive") %>' Font-Names="Verdana" Font-Size="XX-Small"></asp:CheckBox>
           </EditItemTemplate>--%>
                <HeaderStyle Width="20px" />
                <ItemStyle Width="20px" HorizontalAlign="Center" />
            </asp:TemplateField>
                                       
                                        <asp:CommandField CancelText="" DeleteText="" HeaderText="Edit" 
                                            ShowCancelButton="False" ShowEditButton="True" UpdateText="Edit" 
                                            Visible="False"/>
                                        <asp:ButtonField CommandName="Doc_Edit" HeaderText="Edit" Text="Edit" />
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
    </asp:panel>
</asp:Panel>
</asp:Content>

