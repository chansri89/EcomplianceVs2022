<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage3.master" AutoEventWireup="true" CodeFile="ActivityDocumentMaster.aspx.cs" Inherits="ActivityDocumentMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:Panel ID="pnlDocment" runat="server" Height="589px">


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
                        <td style="width: 744px; text-align: right;">
                            <asp:Label ID="lblActName" runat="server" Text="Act Name" Font-Names="Verdana" 
                                Font-Size="XX-Small"></asp:Label>
                        </td>
                        <td style="width: 673px">
                            <asp:DropDownList ID="ddlActName" runat="server" Font-Names="Verdana" 
                                Font-Size="XX-Small">
                            </asp:DropDownList>
                        </td>
                        <td class="style21" style="width: 1246px">
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
           <asp:Panel ID="pnlDocMaster" runat="server"  Height="274px" Width="1076px">
        <div style="overflow:auto; height:272px; width:1036px">
<asp:GridView ID="GrdDocumentMaster" runat="server" CellPadding="3" 
            Font-Size="XX-Small" Width="1022px" Font-Names="Verdana" AutoGenerateColumns="False" 
            Height="90px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                  onrowcommand="GrdActivityMaster_RowCommand">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
                <asp:TemplateField HeaderText="ActId" Visible="False">
                    <ItemTemplate>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtActId" runat="server" Text='<%# Bind("ActId") %>' ReadOnly="true" Font-Names="Verdana" Font-Size="XX-Small" ></asp:TextBox>
                    </EditItemTemplate>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ActDtlId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblActDtlId" runat="server" Text='<%# Eval("ActDtlId") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtActDtlId" runat="server" Text='<%# Bind("ActDtlId") %>' ReadOnly="true" Font-Names="Verdana" Font-Size="XX-Small" ></asp:TextBox>
                    </EditItemTemplate>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="ActivityId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblActivityId" runat="server" Text='<%# Eval("ActivityId") %>'  Font-Names="Verdana" Font-Size="XX-Small"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtActivityId" runat="server" Text='<%# Bind("ActivityId") %>' ReadOnly="true" Font-Names="Verdana" Font-Size="XX-Small" ></asp:TextBox>
                    </EditItemTemplate>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
            <%------------Act and actdetais----------------%>
            <asp:ButtonField HeaderText="Select" Text="Select" CommandName="Select">
                    <HeaderStyle Height="20px" />
                    <ItemStyle Width="20px" />
                  </asp:ButtonField>
            <asp:TemplateField HeaderText="Act Name">
            <ItemTemplate>
            <asp:Label ID="lnkActName" runat="server" Text='<%# Eval("ActName") %>' Font-Names="Verdana" Font-Size="XX-Small" Width="180px"
            ToolTip="Click Select for Creating New Activity for this Act" ></asp:Label>
           <%-- <asp:Label ID="lblActName" runat="server" Text='<%# Eval("ActName") %>'></asp:Label>--%>
           </ItemTemplate>
             <EditItemTemplate>
             
             <asp:TextBox ID="txtActName" runat="server" Text='<%# Bind("ActName") %>' ReadOnly="true"  Font-Names="Verdana" Font-Size="XX-Small" Width="180px"  > 
           
            
             </asp:TextBox>
             
            </EditItemTemplate>
                <HeaderStyle Width="180px" />
                <ItemStyle Width="180px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Chapter">
            <ItemTemplate>
            <asp:Label ID="lblChapter" runat="server" Text='<%# Eval("Chapter") %>'></asp:Label>
            </ItemTemplate>
             <EditItemTemplate>
              <asp:TextBox ID="txtChapter" runat="server" Text='<%# Bind("Chapter") %>' ReadOnly="true"  Font-Names="Verdana" Font-Size="XX-Small" Width="30px"></asp:TextBox>
            <%-- <asp:DropDownList ID="ddlActName" runat="server" DataValueField="ActivityId" DataTextField="Chapter"   DataSource='<%#getChapter() %>'>
                 </asp:DropDownList>--%>
            </EditItemTemplate>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Head">
            <ItemTemplate>
            <asp:Label ID="lblHead" runat="server" Text='<%# Eval("Head") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
             <asp:TextBox ID="txtHead" runat="server" Text='<%# Bind("Head") %>' ReadOnly="true"  Font-Names="Verdana" Font-Size="XX-Small" Width="30px"></asp:TextBox>
             <%--<asp:DropDownList ID="ddlActName" runat="server" DataValueField="ActivityId" DataTextField="ActName"   DataSource='<%#getActName() %>'>
                 </asp:DropDownList>--%>
            </EditItemTemplate>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Section">
            <ItemTemplate>
            <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
                <asp:TextBox ID="txtSection" runat="server" Text='<%# Bind("Section") %>' ReadOnly="true"  Font-Names="Verdana" Font-Size="XX-Small" Width="30px"></asp:TextBox>
            <%-- <asp:DropDownList ID="ddlActName" runat="server" DataValueField="ActivityId" DataTextField="ActName"   DataSource='<%#getActName() %>'>
                 </asp:DropDownList>--%>
            </EditItemTemplate>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ActRule">
            <ItemTemplate>
            <asp:Label ID="lblActRule" runat="server" Text='<%# Eval("ActRule") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
              <asp:TextBox ID="txtActRule" runat="server" Text='<%# Bind("ActRule") %>' ReadOnly="true"  Font-Names="Verdana" Font-Size="XX-Small" Width="30px"></asp:TextBox>
             <%--<asp:DropDownList ID="ddlActName" runat="server" DataValueField="ActivityId" DataTextField="ActName"   DataSource='<%#getActName() %>'>
                 </asp:DropDownList>--%>
            </EditItemTemplate>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>

            <%------------Actvityname and details----------------%>
            <asp:TemplateField HeaderText="Activity Name">
            <ItemTemplate>
            <asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("ActivityName") %>'></asp:Label></ItemTemplate>
            <EditItemTemplate>
            <asp:TextBox ID="txtActivityName" runat="server" Text='<%# Bind("ActivityName") %>'  Font-Names="Verdana" Font-Size="XX-Small" Width="180px"></asp:TextBox>
                 
            </EditItemTemplate>
                <HeaderStyle Width="180px" />
                 <ItemStyle Width="180px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Dept Name">
            <ItemTemplate>
            <asp:Label ID="lblDepartName" runat="server" Text='<%# Eval("DepartmentName") %>'></asp:Label></ItemTemplate>
              <EditItemTemplate>
                 <asp:DropDownList ID="ddlDepartName" runat="server" DataValueField="DepartmentId" DataTextField="DepartmentShortName"   DataSource='<%#getDepartmentName() %>'
                   Font-Names="Verdana" Font-Size="XX-Small">
                 </asp:DropDownList>
            </EditItemTemplate>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category Name">
            <ItemTemplate>
            <asp:Label ID="lblCategName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label></ItemTemplate>
            <EditItemTemplate>
                 <asp:DropDownList ID="ddlCategName" runat="server" DataValueField="CategoryId" DataTextField="CategoryName"   DataSource='<%#getCategoryName() %>'
                  Font-Names="Verdana" Font-Size="XX-Small">
                 </asp:DropDownList>
            </EditItemTemplate>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Severity Name">
            <ItemTemplate>
            <asp:Label ID="lblSevtyName" runat="server" Text='<%# Eval("SeverityName") %>'></asp:Label></ItemTemplate>
            <EditItemTemplate>
                 <asp:DropDownList ID="ddlSevtyName" runat="server" DataValueField="SeverityId" DataTextField="SeverityShortName"   DataSource='<%#getSeverityName() %>'
                  Font-Names="Verdana" Font-Size="XX-Small">
                 </asp:DropDownList>
            </EditItemTemplate>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Compliance Task Avbl ?">
            <ItemTemplate>
            <asp:Label ID="lblNonComplianceTaskAvailableFlag" runat="server" Text='<%# Eval("NonComplianceTaskAvailableFlag") %>'></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtNonComplianceTaskAvailableFlag" runat="server" Text='<%# Bind("NonComplianceTaskAvailableFlag") %>'
             Font-Names="Verdana" Font-Size="XX-Small" Width="30px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="50px" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="State Specific">
            <ItemTemplate>
             <asp:CheckBox ID="chkStateSpec" runat="server" Checked='<%# Eval("IsStateSpecific") %>' Font-Names="Verdana" Font-Size="XX-Small" Enabled="false" >
             </asp:CheckBox>
            </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkStateSpecific" runat="server" Checked='<%# Bind("IsStateSpecific") %>'  Font-Names="Verdana" Font-Size="XX-Small"/></EditItemTemplate>
            <HeaderStyle Width="30px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Location Specific">
            <ItemTemplate>
             <asp:CheckBox ID="chkLocationSpec" runat="server" Checked='<%# Eval("IsLocationSpecific") %>' Font-Names="Verdana" Font-Size="XX-Small" Enabled="false" >
             </asp:CheckBox>
            </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkLocationSpecific" runat="server" Checked='<%# Bind("IsLocationSpecific") %>'  Font-Names="Verdana" Font-Size="XX-Small"/></EditItemTemplate>
            <HeaderStyle Width="30px" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Regular Activity">
            <ItemTemplate>
             <asp:CheckBox ID="chkRegularActivity" runat="server" Checked='<%# Eval("IsRegularActivity") %>' Font-Names="Verdana" Font-Size="XX-Small" Enabled="false"></asp:CheckBox></ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkRegActivity" runat="server" Checked='<%# Bind("IsRegularActivity") %>'  Font-Names="Verdana" Font-Size="XX-Small"/></EditItemTemplate>
             <HeaderStyle Width="30px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Is  Active">
             <ItemTemplate>
              <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>' Font-Names="Verdana" Font-Size="XX-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
             <EditItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>' Font-Names="Verdana" Font-Size="XX-Small"></asp:CheckBox>
           </EditItemTemplate>
                <HeaderStyle Width="40px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>

            <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:Panel>
    <%--<asp:Panel ID="pnlCompanyAct" runat="server" Width="890px" 
        Enabled="False" Visible="False" >BorderWidth="1px"
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
   
</tr>
</table>
</asp:Panel>--%>
 <asp:TextBox ID="txtActivities" runat="server" Width="209px" 
        Font-Names="Verdana" Font-Size="XX-Small" ReadOnly="true" Font-Bold="True"  Visible="False"></asp:TextBox>
        <asp:TextBox ID="txtActDocId" runat="server" Visible="False" Width="31px"></asp:TextBox>
        <asp:TextBox ID="txtActivityId" runat="server"  Visible="False"
            Font-Names="Verdana" Font-Size="XX-Small" Width="30px" ></asp:TextBox>
  
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
                                                Font-Size="XX-Small" Text="Save" 
                                                style="height: 20px" onclick="btnSave_Click" />
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

