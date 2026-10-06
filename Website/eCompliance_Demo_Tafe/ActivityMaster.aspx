<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage3.master" AutoEventWireup="true" CodeFile="ActivityMaster.aspx.cs" Inherits="ActivityMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    <script type="text/javascript" language="javascript">
     function GetPath(Path)
      {

          //document.getElementById('<%= txtExistingPath.ClientID %>').value = Path;
          

      }
//      function GetFilename(Filename)
//       {
      //           document.getElementById("txtDocumentName").value = Filename.value;
      //document.getElementById("txtDocumentName").value = Path.value;
//      }
</script>
<asp:Panel ID="Panel1" runat="server" Height="16px" Width="870px"> </asp:Panel>
       <asp:Panel ID="pnlActivityLoad" runat="server" Height="26px" Width="722px">
            <table style="width: 100%; height: 23px;">
                <tr>
                    <td style="width: 41px; ">
                        <asp:Label ID="lblACtivityLoad" runat="server" Text="Act" Font-Names="arial" 
                            Font-Size="X-Small" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 196px" class="style21">
                        <asp:DropDownList ID="ddlLoadActivity" runat="server" Font-Names="arial" 
                            DataTextField="ActName" DataValueField="ActId" Size="X-Small" 
                             Font-Size="X-Small" Height="16px" Width="182px">  <%--AutoPostBack="True"--%>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 35px">
                        <asp:Button ID="btnGo" runat="server" Text="Go" Font-Names="arial" 
                            Font-Size="X-Small" onclick="btnGo_Click" />
                            
                    </td>
                  
            <%-- </tr>
            </table>--%>
         
   <%-- <table style="width: 100%; height: 35px;">
        <tr>--%>
             <td style="width: 90px; text-align: left;">

                    <asp:Label ID="lblActivityNamefilter" runat="server" Text="Activity Name"  
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True" Visible="False"></asp:Label>
                </td>
                <td style="width: 124px">
                    <asp:TextBox ID="txtActivityNamefilter" runat="server" Font-Names="arial" 
                              Font-Size="X-Small" Width="167px" Height="16px" Visible="False"></asp:TextBox>
                </td>
                    
            <td class="style22" style="width: 58px">
                <asp:Button ID="btnSearch" runat="server" Text="Search" Font-Names="arial" 
                    Font-Size="X-Small"  Height="21px" onclick="btnSearch_Click" 
                    Visible="False"/>
            </td>
            <td>
                <asp:Button ID="btnSearchClear" runat="server" Text="Clear Search" Font-Names="arial" 
                    Font-Size="X-Small"  Height="21px" 
                    Width="87px" onclick="btnSearchClear_Click" Visible="False"/>
            </td>
        <%--    </asp:Panel>--%>
        </tr>
    </table>

        </asp:Panel>
        <br />
    <asp:panel ID="Pnlgv" runat="server" Height="271px" Width="1351px">
        <div style="overflow:auto; height:258px; width:1313px">
    <asp:GridView ID="GrdActivityMaster" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="1266px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px"
                onrowcommand="GrdActivityMaster_RowCommand" DataKeyNames="ActivityId">
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
                   <asp:Label ID="lblActId" runat="server" Text='<%# Eval("ActId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtActId" runat="server" Text='<%# Bind("ActId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox>
                    </EditItemTemplate>--%>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ActDtlId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblActDtlId" runat="server" Text='<%# Eval("ActDtlId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                   <%-- <EditItemTemplate>
                        <asp:TextBox ID="txtActDtlId" runat="server" Text='<%# Bind("ActDtlId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox>
                    </EditItemTemplate>--%>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="ActivityId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblActivityId" runat="server" Text='<%# Eval("ActivityId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtActivityId" runat="server" Text='<%# Bind("ActivityId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox>
                    </EditItemTemplate>--%>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CategoryId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblCategoryId" runat="server" Text='<%# Eval("CategoryId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtCategoryId" runat="server" Text='<%# Bind("CategoryId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox>
                    </EditItemTemplate>--%>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="SeverityId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblSeverityId" runat="server" Text='<%# Eval("SeverityId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtSeverityId" runat="server" Text='<%# Bind("SeverityId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox>
                    </EditItemTemplate>--%>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="DepartmentId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblDepartmentId" runat="server" Text='<%# Eval("DepartmentId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtDepartmentId" runat="server" Text='<%# Bind("DepartmentId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox>
                    </EditItemTemplate>--%>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="DocumentTypeId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblDocumentTypeId" runat="server" Text='<%# Eval("DocumentTypeId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtDocumentTypeId" runat="server" Text='<%# Bind("DocumentTypeId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox>
                    </EditItemTemplate>--%>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ActivityDocumentId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblActivityDocumentId" runat="server" Text='<%# Eval("ActivityDocumentId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtActivityDocumentId" runat="server" Text='<%# Bind("ActivityDocumentId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox>
                    </EditItemTemplate>--%>
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
            <asp:TemplateField HeaderText="Act">
            <ItemTemplate>
            <asp:Label ID="lnkActName" runat="server" Text='<%# Eval("ActName") %>' Font-Names="arial" Font-Size="X-Small" Width="180px"
            ToolTip="Click Add for Creating New Activity for this Act" ></asp:Label>
           <%-- <asp:Label ID="lblActName" runat="server" Text='<%# Eval("ActName") %>'></asp:Label>--%>
           </ItemTemplate>
            <%-- <EditItemTemplate>
             <asp:TextBox ID="txtActName" runat="server" Text='<%# Bind("ActName") %>' ReadOnly="true"  Font-Names="arial" Font-Size="X-Small" Width="180px"  > 
             </asp:TextBox>      </EditItemTemplate>--%>
                <HeaderStyle Width="180px" />      <ItemStyle Width="180px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Chapter">
            <ItemTemplate>
            <asp:Label ID="lblChapter" runat="server" Text='<%# Eval("Chapter") %>'></asp:Label>
            </ItemTemplate>
            <%-- <EditItemTemplate>
              <asp:TextBox ID="txtChapter" runat="server" Text='<%# Bind("Chapter") %>' ReadOnly="true"  Font-Names="arial" Font-Size="X-Small" Width="30px"></asp:TextBox>
            -- <asp:DropDownList ID="ddlActName" runat="server" DataValueField="ActivityId" DataTextField="Chapter"   DataSource='<%#getChapter() %>'>
                 </asp:DropDownList>--
            </EditItemTemplate>--%>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Head">
            <ItemTemplate>
            <asp:Label ID="lblHead" runat="server" Text='<%# Eval("Head") %>'></asp:Label></ItemTemplate>
            <%-- <EditItemTemplate>
             <asp:TextBox ID="txtHead" runat="server" Text='<%# Bind("Head") %>' ReadOnly="true"  Font-Names="arial" Font-Size="X-Small" Width="30px"></asp:TextBox>
            --<asp:DropDownList ID="ddlActName" runat="server" DataValueField="ActivityId" DataTextField="ActName"   DataSource='<%#getActName() %>'>
                 </asp:DropDownList>--
            </EditItemTemplate>--%>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Section">
            <ItemTemplate>
            <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'></asp:Label></ItemTemplate>
            <%-- <EditItemTemplate>
                <asp:TextBox ID="txtSection" runat="server" Text='<%# Bind("Section") %>' ReadOnly="true"  Font-Names="arial" Font-Size="X-Small" Width="30px"></asp:TextBox>
            -- <asp:DropDownList ID="ddlActName" runat="server" DataValueField="ActivityId" DataTextField="ActName"   DataSource='<%#getActName() %>'>
                 </asp:DropDownList>--
            </EditItemTemplate>--%>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ActRule">
            <ItemTemplate>
            <asp:Label ID="lblActRule" runat="server" Text='<%# Eval("ActRule") %>'></asp:Label></ItemTemplate>
            <%-- <EditItemTemplate>
              <asp:TextBox ID="txtActRule" runat="server" Text='<%# Bind("ActRule") %>' ReadOnly="true"  Font-Names="arial" Font-Size="X-Small" Width="30px"></asp:TextBox>
             --<asp:DropDownList ID="ddlActName" runat="server" DataValueField="ActivityId" DataTextField="ActName"   DataSource='<%#getActName() %>'>
                 </asp:DropDownList>--
            </EditItemTemplate>--%>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>

            <%------------Actvityname and details----------------%>
            
            <asp:TemplateField HeaderText="Activity Name">
            <ItemTemplate>
           
            <asp:Label ID="lblActivityName" runat="server" Text='<%# Eval("ActivityName") %>'></asp:Label></ItemTemplate>
                 <HeaderStyle Width="180px" />      <ItemStyle Width="180px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Form">
            <ItemTemplate>   <asp:Label ID="lblForm" runat="server" Text='<%# Eval("DocumentName") %>'></asp:Label>
            </ItemTemplate>
            <%--<EditItemTemplate>
            <asp:TextBox ID="txtForm" runat="server" Text='<%# Bind("DocumentName") %>'  Font-Names="arial" Font-Size="X-Small" Width="180px"></asp:TextBox>
            </EditItemTemplate>--%>
                <HeaderStyle Width="180px" />   <ItemStyle Width="180px" />
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Dept Name">
            <ItemTemplate>
            <asp:Label ID="lblDepartName" runat="server" Text='<%# Eval("DepartmentName") %>'></asp:Label></ItemTemplate>
             <%-- <EditItemTemplate>
                 <asp:DropDownList ID="ddlDepartName" runat="server" DataValueField="DepartmentId" DataTextField="DepartmentShortName"   DataSource='<%#getDepartmentName() %>'
                   Font-Names="arial" Font-Size="X-Small">
                 </asp:DropDownList>
            </EditItemTemplate>--%>
                <HeaderStyle Width="40px" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category Name">
            <ItemTemplate>
            <asp:Label ID="lblCategName" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label></ItemTemplate>
            <%--<EditItemTemplate>
                 <asp:DropDownList ID="ddlCategName" runat="server" DataValueField="CategoryId" DataTextField="CategoryName"   DataSource='<%#getCategoryName() %>'
                  Font-Names="arial" Font-Size="X-Small">
                 </asp:DropDownList>
            </EditItemTemplate>--%>
                <HeaderStyle Width="30px" />
            </asp:TemplateField>
                <asp:TemplateField HeaderText="Severity Name">
                    <ItemTemplate>
                        <asp:Label ID="lblSevtyName" runat="server" Text='<%# Eval("SeverityName") %>'></asp:Label>
                    </ItemTemplate>
                    <%--<EditItemTemplate>
                 <asp:DropDownList ID="ddlSevtyName" runat="server" DataValueField="SeverityId" DataTextField="SeverityShortName"   DataSource='<%#getSeverityName() %>'
                  Font-Names="arial" Font-Size="X-Small">
                 </asp:DropDownList>
            </EditItemTemplate>--%>
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>

                 <asp:TemplateField HeaderText="ActivityCategorizationId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblActivityCategorizationId" runat="server" Text='<%# Eval("ActivityCategorizationId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>  <HeaderStyle Width="20px" />   </asp:TemplateField>               

               <asp:TemplateField HeaderText="Activity Categorization">
                    <ItemTemplate> <asp:Label ID="lblActivityCategorization" runat="server" Text='<%# Eval("ActivityCategorization") %>'></asp:Label>
                    </ItemTemplate>  <HeaderStyle Width="30px" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Compliance Task Avbl ?">
                    <ItemTemplate>
                        <asp:Label ID="lblNonComplianceTaskAvailableFlag" runat="server" 
                            Text='<%# Eval("NonComplianceTaskAvailableFlag") %>'></asp:Label>
                    </ItemTemplate>
                    <%-- <EditItemTemplate>
            <asp:TextBox ID="txtNonComplianceTaskAvailableFlag" runat="server" Text='<%# Bind("NonComplianceTaskAvailableFlag") %>'
             Font-Names="arial" Font-Size="X-Small" Width="30px"></asp:TextBox></EditItemTemplate>--%>
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State Specific">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkStateSpec" runat="server" 
                            Checked='<%# Eval("IsStateSpecific") %>' Enabled="false" Font-Names="arial" 
                            Font-Size="X-Small" />
                    </ItemTemplate>
                    <%--<EditItemTemplate>
             <asp:CheckBox ID="chkStateSpecific" runat="server" Checked='<%# Bind("IsStateSpecific") %>'  Font-Names="arial" Font-Size="X-Small"/></EditItemTemplate>--%>
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Location Specific">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkLocationSpec" runat="server" 
                            Checked='<%# Eval("IsLocationSpecific") %>' Enabled="false" 
                            Font-Names="arial" Font-Size="X-Small" />
                    </ItemTemplate>
                    <%--  <EditItemTemplate>
           <asp:CheckBox ID="chkLocationSpecific" runat="server" Checked='<%# Bind("IsLocationSpecific") %>'  Font-Names="arial" Font-Size="X-Small"/></EditItemTemplate>--%>
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Regular Activity">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkRegularActivity" runat="server" 
                            Checked='<%# Eval("IsRegularActivity") %>' Enabled="false" Font-Names="arial" 
                            Font-Size="X-Small" />
                    </ItemTemplate>
                    <%-- <EditItemTemplate>
           <asp:CheckBox ID="chkRegActivity" runat="server" Checked='<%# Bind("IsRegularActivity") %>'  Font-Names="arial" Font-Size="X-Small"/></EditItemTemplate>--%>
                    <HeaderStyle Width="30px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Is  Active">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>' 
                            Enabled="false" Font-Names="arial" Font-Size="X-Small" />
                    </ItemTemplate>
                    <%--<EditItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Bind("IsActive") %>' Font-Names="arial" Font-Size="X-Small"></asp:CheckBox>
           </EditItemTemplate>--%>
                    <HeaderStyle Width="40px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="Administrator" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblAdministrator" runat="server" 
                            Text='<%# Eval("Administrator") %>'></asp:Label>
                    </ItemTemplate>
                  
                    <HeaderStyle Width="50px" />
                </asp:TemplateField>
            <%--<asp:CommandField HeaderText="Edit" ShowEditButton="True"  CancelText="" 
                    UpdateText="" />--%>
                      <asp:ButtonField HeaderText="Edit" Text="Edit" CommandName="Activity_Edit">
                    <HeaderStyle Height="20px" />
                    <ItemStyle Width="20px" />
                  </asp:ButtonField>
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" Visible="false"/>
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
    <%--<EditItemTemplate>
                        <asp:TextBox ID="txtDepartmentId" runat="server" Text='<%# Bind("DepartmentId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox>
                    </EditItemTemplate>--%><%--<EditItemTemplate>
                        <asp:TextBox ID="txtDocumentTypeId" runat="server" Text='<%# Bind("DocumentTypeId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox>
                    </EditItemTemplate>--%><%--<EditItemTemplate>
                        <asp:TextBox ID="txtActivityDocumentId" runat="server" Text='<%# Bind("ActivityDocumentId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox>
                    </EditItemTemplate>--%>
    <asp:panel ID="pnlAdd" runat="server" Width="1192px" 
            Height="654px">
        <asp:panel ID="pnlACt" runat="server" Enabled="false" BorderWidth="1px" 
            Width="1015px" Visible="False">
   
        <table style="width: 97%; height: 3px;">
            <tr>
         
                    <td style="width: 96px; text-align: left;">
                            <asp:Label ID="lblActName" runat="server" Text="Act"  Font-Names="arial" 
                                Font-Size="X-Small" style="text-align: left"></asp:Label>
                    </td>
                    <td style="width: 127px">
                            <asp:TextBox ID="txtActName" runat="server" Font-Names="arial" ReadOnly="true"
                                Font-Size="X-Small" 
                            ToolTip="Click Select for Creating New Activity in the Grid" Height="16px" 
                            Width="219px">
                            </asp:TextBox>
                    </td>
                
                        <td style="width: 55px; text-align: left;">
                            <asp:Label ID="lblChapter" runat="server" Text="Chapter"  Font-Names="arial" 
                                Font-Size="X-Small"></asp:Label>
                        </td>
                        <td style="width: 105px" class="style21">
                            <asp:TextBox ID="txtChapter" runat="server" Font-Names="arial" ReadOnly="true" 
                                Font-Size="X-Small" Width="65px"></asp:TextBox>
                           
                        </td>
                        <td class="style1" style="width: 80px; text-align: right;">
                            <asp:Label ID="lblHead" runat="server" Text="Head"  Font-Names="arial" 
                                Font-Size="X-Small" style="text-align: left"></asp:Label>
                        </td>
                        <td style="width: 96px">
                           <asp:TextBox ID="txtHead" runat="server" Font-Names="arial" ReadOnly="true" 
                                Font-Size="X-Small" Width="77px"></asp:TextBox>
                            
                        </td>
                        <td class="style1" style="width: 121px; text-align: right;">
                            <asp:Label ID="lblSection" runat="server" Text="Section"  Font-Names="arial" 
                                Font-Size="X-Small" style="text-align: left"></asp:Label>
                        </td>
                        <td style="width: 106px">
                           <asp:TextBox ID="txtSection" runat="server" Font-Names="arial" ReadOnly="true" 
                                Font-Size="X-Small" Width="59px"></asp:TextBox>
                            
                        </td>
                        <td class="style1" style="width: 138px; text-align: left;">
                            <asp:Label ID="lblActRule" runat="server" Text="ActRule"  Font-Names="arial" 
                                Font-Size="X-Small"></asp:Label>
                        </td>
                        <td style="width: 126px">
                            <asp:TextBox ID="txtActRule" runat="server" Font-Names="arial" ReadOnly="true" 
                                Font-Size="X-Small" Width="76px"></asp:TextBox>
                        </td>
                      
                        <td>
                              <asp:TextBox ID="txtActDtlId" runat="server" Font-Names="arial" 
                                Font-Size="X-Small"  Visible="false" ReadOnly="true" Width="21px"></asp:TextBox>
                        </td>
                        <td>
                                <asp:TextBox ID="txtActivityId" runat="server"  Visible="False"
            Font-Names="arial" Font-Size="X-Small" Width="30px" ></asp:TextBox></td>
             <td>
                                <asp:TextBox ID="txtActivityDocumentId" runat="server"  Visible="False"
            Font-Names="arial" Font-Size="X-Small" Width="30px" ></asp:TextBox></td>
  
  

            </tr>

        </table>
         </asp:panel>
        <br />
        <asp:Panel ID="pnlNew" runat="server" Width="1018px" Height="118px" >
       
        <table style="width: 97%; margin-right: 0px;">
        <tr>   
           
            <td class="style21" style="width: 156px">
                <asp:Label ID="lblActivityName" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Text="Activity Name" Font-Bold="True"></asp:Label>
            </td>
            <td style="width: 259px">
                <asp:TextBox ID="txtActivityName" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Width="249px"  ToolTip="Click Add for Creating New Activity in the Grid"></asp:TextBox>
               <%-- <asp:DropDownList ID="ddlActivityName" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Height="23px" Width="274px">
                </asp:DropDownList>--%>
            </td>
            <td style="width: 243px">
                <asp:Label ID="lblDepartmentName" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Text="Department Name" Font-Bold="True" 
                    style="text-align: right"></asp:Label>
            </td>
            <td style="width: 169px">
                <asp:DropDownList ID="ddlDepartmentName" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" 
                    onselectedindexchanged="ddlDepartmentName_SelectedIndexChanged" 
                    Height="16px" Width="111px">
                </asp:DropDownList>
            </td>
           
             
                
                <td class="style21" style="width: 109px">
                    <asp:Label ID="lblCategoryName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Text="Category Name" Font-Bold="True"></asp:Label>
                </td>
                <td style="width: 259px">
                    <asp:DropDownList ID="ddlCategoryName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small">
                    </asp:DropDownList>
                </td>
                </tr>
                <tr>
                <td style="width: 156px">
                    <asp:Label ID="lblSeverityName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Text="Severity Name" Font-Bold="True"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSeverityName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Height="16px" Width="81px">
                    </asp:DropDownList>
                </td>
               
                 <td class="style21" style="width: 243px">
                   
                     <asp:Label ID="lblDocType" runat="server" Font-Bold="True" Font-Names="arial" 
                         Font-Size="X-Small" Text="Document Type"></asp:Label>
                   
                </td>
                <td style="width: 169px">
                    <asp:DropDownList ID="ddlDocType" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Height="16px" Width="114px">
                    </asp:DropDownList>
                </td>
                <td style="width: 168px">
                    <asp:Label ID="lblNonComplianceTaskAvailableFlag" runat="server" 
                        Font-Names="arial" Font-Size="X-Small" Text="Compliance Task Avbl (Y/N)"></asp:Label>
                </td>
                 <td>
                   <asp:TextBox ID="txtNonComplianceTaskAvailableFlag" runat="server" 
                       Font-Names="arial" Font-Size="X-Small" Text="N" Width="27px"></asp:TextBox>
               </td>
               </tr>
                
                    <tr>
                        <td class="style21" style="width: 156px">
                    <asp:Label ID="lblActivityCat" runat="server" Font-Names="arial" Font-Bold="true"
                        Font-Size="X-Small" Text="Activity Categorization" Width="134px"></asp:Label>
                </td>
                <td style="width: 168px">
                    <asp:DropDownList ID="ddlActivityCategorization" runat="server" 
                        Font-Names="arial"  Font-Size="X-Small" Height="16px" Width="138px">
                    </asp:DropDownList>
                </td>
                        <td style="width: 243px">
                            <asp:CheckBox ID="chkStateSpecific" runat="server" Font-Names="arial" 
                                Font-Size="X-Small" Text="State Specific" style="text-align: center" />
                        </td>
                        <td style="width: 168px">
                            <asp:CheckBox ID="chkLocationSpecific" runat="server" Font-Names="arial" 
                                Font-Size="X-Small" Text="Location Specific" style="text-align: right" />
                        </td>
                        <td style="width: 185px">
                            <asp:CheckBox ID="chkRegularActivity" runat="server" Font-Names="arial" 
                                Font-Size="X-Small" style="text-align: right" Text="Regular Activity" />
                        </td>
                         
                <td style="width: 160px">
                <asp:CheckBox id="chkIsActive" runat="server" Text="IsActive" Font-Names="arial" 
                        Font-Size="X-Small" ReadOnly="True" Width="117px" 
                        style="text-align: left" Visible="False" />
                </td>
                    </tr>
                    <tr>
            <td class="style21" style="width: 156px">
                                            <asp:Label ID="lblAccessPath" runat="server" Font-Names="arial" 
                                                Font-Size="X-Small" Text="File Access Path" Font-Bold="false"></asp:Label>
                                        </td>
                                        <td style="width: 259px">
                                            <asp:FileUpload ID="fluAccessPath" runat="server" Font-Names="arial" 
                                                Font-Size="X-Small" style="text-align: right" Width="244px" 
                                              onchange="GetPath(this.value);"/>
                                              
                                        </td>
                <td style="width: 243px">
                    <asp:Label ID="lblDocumentName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Text="Document Name"></asp:Label>
                </td>
                <td style="width: 169px">
                    <asp:TextBox ID="txtDocumentName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Width="143px"></asp:TextBox>
                </td>
                <td class="style21" style="width: 109px">
                    <asp:Label ID="lblExistingPath" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Text="Existing Document Path" Width="110px" 
                         Visible = "False"></asp:Label>
                </td>
                <td style="width: 259px">
                    <asp:TextBox ID="txtExistingPath" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Width="154px" AutoPostBack="True" Visible = "false" ></asp:TextBox>
                </td>
                </tr>
                </table>
                </asp:Panel>
                <table id="tblContinue" runat="server">
                <tr>
                        <td style="text-align: right; width: 166px;">
                            &nbsp;</td>
                        <td style="width: 156px">
                            &nbsp;</td>
                        <td style="width: 181px">
                            <asp:Button ID="btnContinue" runat="server" Font-Names="arial" 
                                Font-Size="X-Small" onclick="btnContinue_Click" Text="Continue" 
                                ToolTip="Click continue to Assign Freuency and Responsible Group" 
                                Enabled="False" />
                                <asp:Button ID="btnClear" runat="server" Font-Names="arial" 
                                Font-Size="X-Small" Text="Clear" onclick="btnClear_Click" Visible="true" 
                                Enabled="False" />
                                 <asp:Button ID="btnBack" runat="server" Font-Names="arial" Visible="false" 
                                Font-Size="X-Small" Text="Back" onclick="btnBack_Click" />
                        </td>
                        <td style="width: 146px">
                            &nbsp;</td>
                    </tr>
                </table>
                <table style="height: 190px; width: 1121px">
                <tr>
                <td>
               
    <asp:Panel ID="Pnlfrequency" runat="server" Visible="false" Height="197px" 
            Width="654px">
    <table>
        <tr>
        <td>
        <asp:TextBox ID="txtFrequencyTitle" runat="server" 
                Text="Assign Frequency and Responsible Group for Activity" 
                Font-Size="X-Small" BackColor="#000084" 
                Font-Bold="True" ForeColor="White" Width="641px"></asp:TextBox>
        </td>
        </tr>
        </table>
    <table>
    <tr>
    <td style="width: 78px"><asp:Label ID="lblFrequency" runat="server" Text="Frequency" Font-Names="arial"  Width="60px" Font-Size="X-Small"></asp:Label></td>
    <td class="style21" style="width: 114px"><asp:DropDownList ID="ddlFrequency" runat="server" 
                    DataTextField="FrequencyName" DataValueField="FrequencyId" Font-Names="arial" 
            Font-Size="X-Small" Height="16px" Width="115px"></asp:DropDownList></td>
    <td class="style21" style="width: 110px"><asp:Label ID="lblFrqRemarks" runat="server" 
                            Text="Frequency Remarks" Font-Names="arial"  Font-Size="X-Small"></asp:Label></td>
    <td><asp:TextBox ID="txtFrqRemarks" runat="server" Font-Names="arial" 
            Font-Size="X-Small" Width="169px"></asp:TextBox></td>
    <td style="width: 68px"></td>
    <td></td>
    </tr>
    <tr>
    <td style="width: 78px"><asp:Label ID="lblDueMonth" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Text="Due Month"></asp:Label></td>
    <td class="style21" style="width: 114px"> <asp:DropDownList ID="ddldueMonth" runat="server"  DataValueField="Month" DataTextField="MonthName" 
                     Font-Names="arial" Font-Size="X-Small">
                 </asp:DropDownList></td>
    <td class="style21" style="width: 110px"><asp:Label ID="lblDueDate" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Text="Due Date" >
                    </asp:Label></td>
    <td><asp:DropDownList ID="ddlDueDate" runat="server" Font-Names="arial"
            DataValueField="Number" DataTextField="Date" Font-Size="X-Small" Height="18px" Width="87px"> 
                </asp:DropDownList></td>
    <td style="width: 68px"><asp:Label ID="lblDueDay" runat="server" Text="Due Day" Font-Names="arial"  Font-Size="X-Small"></asp:Label></td>
    <td><asp:DropDownList ID="ddlDueDay" runat="server" Font-Names="arial"  DataTextField="Day" DataValueField="Number" Width="80px"
                    Font-Size="X-Small"> 
                    </asp:DropDownList></td>
    </tr>
    <tr>
    <td style="width: 78px"><asp:Label ID="lblTrigMonth" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Text="Trigger Month"></asp:Label></td>
    <td class="style21" style="width: 114px"> <asp:DropDownList ID="ddlTiggerMonth" runat="server"  DataValueField="Month" DataTextField="MonthName"
                     Font-Names="arial" Font-Size="X-Small">
                 </asp:DropDownList></td>
    <td class="style21" style="width: 110px"><asp:Label ID="lblTrigDate" runat="server" Font-Names="arial" 
                    Font-Size="X-Small" Text="Trigger Date" >
                    </asp:Label></td>
    <td><asp:DropDownList ID="ddlTriggerDate" runat="server" Font-Names="arial" DataValueField="Number" DataTextField="Date" 
                    Font-Size="X-Small"> 
                </asp:DropDownList></td>
    <td style="width: 68px"><asp:Label ID="lblTriggDay" runat="server" Text="Trigger Day" Font-Names="arial"  Font-Size="X-Small"></asp:Label></td>
    <td><asp:DropDownList ID="ddlTriggerDay" runat="server" Font-Names="arial"  DataTextField="Day" DataValueField="Number" Width="80px"
                    Font-Size="X-Small"> 
                    </asp:DropDownList></td>
    </tr>
    
    </table>
    </asp:Panel>
    </td>
    <td>

    <asp:panel ID="PnlAssignResponsibleGroup" runat="server" Height="193px" 
            Width="458px" Visible="false">
        <div style="overflow:auto; height:174px; width:434px">
    <asp:GridView ID="grdAssignResponsibleGroup" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="405px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px"
                onrowcommand="GrdActivityMaster_RowCommand" DataKeyNames="ActivityId">
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" 
            HorizontalAlign="Left" />
        <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
                <asp:TemplateField HeaderText="CompanyCode" Visible="False">
                    <ItemTemplate>
                   <asp:Label ID="lblCompanyCode" runat="server" Text='<%# Eval("CompanyCode") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="LocationDeptId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblLocationDeptId" runat="server" Text='<%# Eval("LocationDeptId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CompanyActivityId" Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblCompanyActivityId" runat="server" Text='<%# Eval("CompanyActivityId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="Company Name" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblCompanyName" runat="server" Text='<%# Eval("CompanyName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ResponsibleGroupName" Visible="true">
                    <ItemTemplate>
                        <asp:Label ID="lblResponsibleGrpName" runat="server" Text='<%# Eval("ResponsibleGrpName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label>
                    </ItemTemplate>
                        <HeaderStyle Width="20px" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Select" Visible="true">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkLocationSpec" runat="server" 
                            Checked='<%# Eval("IsActive") %>' Enabled="true"  
                            Font-Names="arial" Font-Size="X-Small" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="20px" HorizontalAlign="Center" />
                </asp:TemplateField>

        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
     </td></tr></table>
    <table runat="server" id="tblSave" visible="false"> <tr>
                        <td style="text-align: right; width: 166px;">
                            &nbsp;</td>
                        <td style="width: 156px">
                            &nbsp;</td>
                        <td style="width: 181px">
                            <asp:Button ID="btnSave" runat="server" Font-Names="arial" 
                                Font-Size="X-Small" onclick="btnSave_Click" Text="Save" />
                                <asp:Button ID="btnExit" runat="server" Font-Names="arial" 
                                Font-Size="X-Small" Text="Exit" onclick="btnExit_Click" Visible="False" />
                        </td>
                        <td style="width: 146px">
                            &nbsp;</td>
                    </tr></table>
    <table>
                <tr>
                <td style="width: 132px"></td>
                    <td style="width: 132px">
                        <asp:HiddenField ID="HidDeleteCount" runat="server" Value="0" />
                    </td>
                    <td>
                        <asp:HiddenField ID="HidUpdateCount" runat="server" Value="0" />
                    </td>
            </tr>
                    
        </table>
    </asp:panel>
    <%-- <asp:Label ID="lblActName" runat="server" Text='<%# Eval("ActName") %>'></asp:Label>--%>
    </div>
</asp:Content>

