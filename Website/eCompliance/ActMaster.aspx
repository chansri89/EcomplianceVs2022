<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="ActMaster.aspx.cs" Inherits="ActMaster" %>

 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:Panel ID="Panel1" runat="server" Height="16px" Width="870px"> </asp:Panel>--%>
    <div style="overflow:auto; width: 1238px; height: 549px;">
  <asp:Panel ID="pnlActLoad" runat="server" Height="25px" Width="1077px" 
            BackColor = "Blue">
            <table style="width: 99%; height: 25px;">
                <tr>
                    
                    <td style="width: 40px; " class="style28">
                        <asp:Label ID="lblACtLoad" runat="server" Text="Act" Font-Names="arial" Font-Size="X-Small" Font-Bold="true" ForeColor="White"></asp:Label>
                    </td>
                    <td style="width: 196px" class="style21">
                        <asp:DropDownList ID="ddlLoadAct" runat="server" Font-Names="arial" Font-Size="X-Small" AutoPostBack="false" Height="18px" Width="425px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 40px">
                        <asp:Button ID="btnGo" runat="server" Text="Go" Font-Names="arial" 
                            Font-Size="X-Small" onclick="btnGo_Click" />
                            
                    </td>
                    <td style="width: 470px; ">
                        <asp:Label ID="Label2" runat="server" Text="Acts in BLUE colour indicates Not effective as on date and in RED Already Expired as on Date" Font-Names="arial" ForeColor = "White"
                        Font-Size="X-Small" Font-Bold="true"></asp:Label>
                    </td>
                       <td class="style21" style="width: 75px">
                        <asp:Button ID="btnCollapse" runat="server" Text="Collapse" Font-Names="arial"  Font-Size="X-Small" onclick="btnCollapse_Click" />
                            
                    </td>
                  
                </tr>
            </table>
        </asp:Panel>
 <%--       <br />--%>
 <%--onrowcancelingedit="GrdCompanyMaster_RowCancelingEdit" 
                onrowdeleting="GrdCompanyMaster_RowDeleting" 
                onrowediting="GrdActMaster_RowEditing" OnRowCommand ="GrdActMaster_RowCommand"
                onrowupdating="GrdCompanyMaster_RowUpdating"--%>

    <asp:panel ID="Pnlgv" runat="server" Height="257px" Width="1226px">
        <div style="overflow:auto; height:99%; width:99%">
    <asp:GridView ID="GrdActMaster" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="98%" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px"   
                onrowediting="GrdActMaster_RowEditing"   DataKeyNames="ActDtlId"  >
        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />   <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" /> <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White"  HorizontalAlign="Left" />  <AlternatingRowStyle BackColor="#DCDCDC" />
        <Columns>
          <%--<asp:ButtonField CommandName="Edit" HeaderText="Edit" Text="Edit"> <HeaderStyle Height="20px" />   <ItemStyle Width="20px" />
          </asp:ButtonField>--%>
               <asp:CommandField HeaderText="Edit" ShowEditButton="True" >
            <HeaderStyle Width="20px" />   </asp:CommandField>

         <asp:TemplateField HeaderText="ActId" Visible="False">
            <ItemTemplate>  <asp:Label ID="lblActId" runat="server" Text='<%# Eval("ActId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
              <HeaderStyle Width="20px" /> </asp:TemplateField>

             <asp:TemplateField HeaderText="ActDtlId" Visible="False">
                <ItemTemplate>
                <asp:Label ID="lblActDtlId" runat="server" Text='<%# Eval("ActDtlId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
                 <HeaderStyle Width="20px" />  </asp:TemplateField>

             <asp:TemplateField HeaderText="Act">
            <ItemTemplate>  <asp:Label ID="lblActName" runat="server" Text='<%# Eval("ActName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
               <HeaderStyle Width="120px" />    <ItemStyle Width="120px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Classification Act">
             <ItemTemplate>
            <asp:Label ID="lblClassification" runat="server" Text='<%# Eval("ClassificationAct") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px" />  </asp:TemplateField>

            <asp:TemplateField HeaderText="Chapter">
            <ItemTemplate> <asp:Label ID="lblChapter" runat="server" Text='<%# Eval("Chapter") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
                   <HeaderStyle Width="20px" />    </asp:TemplateField>

             <asp:TemplateField HeaderText="Head">
            <ItemTemplate>  <asp:Label ID="lblHead" runat="server" Text='<%# Eval("Head") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
              <HeaderStyle Width="20px" />      </asp:TemplateField>

             <asp:TemplateField HeaderText="Section">
            <ItemTemplate>    <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'  Font-Names="arial" Font-Size="X-Small" ></asp:Label></ItemTemplate>
             <HeaderStyle Width="20px" />            </asp:TemplateField>

             <asp:TemplateField HeaderText="Rule">
            <ItemTemplate>      <asp:Label ID="lblActRule" runat="server" Text='<%# Eval("ActRule") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
                 <HeaderStyle Width="20px" />        </asp:TemplateField>

             <asp:TemplateField HeaderText="Description">
            <ItemTemplate>   <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
                <HeaderStyle Width="500px" />        </asp:TemplateField>

               <asp:TemplateField HeaderText="Is Active">
             <ItemTemplate> <asp:CheckBox ID="chkActive" runat="server" Checked='<%# Eval("IsActive") %>' Font-Names="arial" Font-Size="X-Small" Enabled="false"></asp:CheckBox>
             </ItemTemplate>
                  <HeaderStyle Width="30px" />       <ItemStyle HorizontalAlign="Center" />    </asp:TemplateField>

                <%--  scs 020316 introduced other columns on act and set it as row command for editing--%>
            <asp:TemplateField HeaderText="Frequency" Visible="true">
            <ItemTemplate>  <asp:Label ID="lblFrequency" runat="server" Text='<%# Eval("Frequency") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
               <HeaderStyle Width="25px" />    <ItemStyle Width="25px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Implication Section" Visible="false">
             <ItemTemplate>  <asp:Label ID="lblImplicationSection" runat="server" Text='<%# Eval("ImplicationSection") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
              <HeaderStyle Width="30px" />  </asp:TemplateField>

            <asp:TemplateField HeaderText="Implication" Visible="true">
            <ItemTemplate> <asp:Label ID="lblImplication" runat="server" Text='<%# Eval("Implication") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
                   <HeaderStyle Width="110px" />    </asp:TemplateField>

             <asp:TemplateField HeaderText="Liability" Visible="true">
            <ItemTemplate>  <asp:Label ID="lblLiability" runat="server" Text='<%# Eval("Liability") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
              <HeaderStyle Width="40px" />      </asp:TemplateField>

             <asp:TemplateField HeaderText="Affected Person" Visible="true">
            <ItemTemplate>  <asp:Label ID="lblAffectedPerson" runat="server" Text='<%# Eval("AffectedPerson") %>'  Font-Names="arial" Font-Size="X-Small" ></asp:Label></ItemTemplate>
             <HeaderStyle Width="30px" />            </asp:TemplateField>

             <asp:TemplateField HeaderText="Impor tance" Visible="true">
            <ItemTemplate>  <asp:Label ID="lblImportance" runat="server" Text='<%# Eval("Importance") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
                 <HeaderStyle Width="20px" />        </asp:TemplateField>

             <asp:TemplateField HeaderText="Version Number" Visible="false">
            <ItemTemplate>  <asp:Label ID="lblVersionNumber" runat="server" Text='<%# Eval("VersionNumber") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
               <HeaderStyle Width="40px" />    <ItemStyle Width="40px" />
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Effective Date" Visible="true" >
             <ItemTemplate>  <asp:Label ID="lblEffectiveDate" runat="server" Text='<%# Eval("EffectiveDate","{0:dd-MM-yyyy}") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
              <HeaderStyle Width="50px" />  </asp:TemplateField>

            <asp:TemplateField HeaderText="ExpiryDate" Visible="true">
            <ItemTemplate> <asp:Label ID="lblExpiryDate" runat="server" Text='<%# Eval("ExpiryDate","{0:dd-MM-yyyy}") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
                   <HeaderStyle Width="20px" />    </asp:TemplateField>

             <asp:TemplateField HeaderText="IsNew" Visible="false">
            <ItemTemplate>  <asp:CheckBox ID="ChkIsNew" runat="server" Checked='<%# Eval("IsNew") %>'
                     Font-Names="arial" Font-Size="X-Small" Enabled="false"></asp:CheckBox>  </ItemTemplate>
              <HeaderStyle Width="20px" />      </asp:TemplateField>

             <asp:TemplateField HeaderText="IsUpdated" Visible="false">
                <ItemTemplate>   <asp:CheckBox ID="ChkIsUpDated" runat="server" Checked='<%# Eval("IsUpdated") %>'
                     Font-Names="arial" Font-Size="X-Small" Enabled="false"></asp:CheckBox>  </ItemTemplate>         
               </asp:TemplateField>

             <asp:TemplateField HeaderText="Validity Status" Visible="false">
            <ItemTemplate>  <asp:CheckBox ID="ChkValidityStatus" runat="server" Checked='<%# Eval("ValidityStatus") %>'
                     Font-Names="arial" Font-Size="X-Small" Enabled="false"></asp:CheckBox></ItemTemplate>
                 </asp:TemplateField>
              <%--   SCS020316 over--%>
       <%--     <asp:CommandField HeaderText="Edit" ShowEditButton="True" >
            <HeaderStyle Width="20px" />   </asp:CommandField>--%>

            <%--<asp:CommandField HeaderText="Delete" ShowDeleteButton="True" Visible="false" >
            <HeaderStyle Width="30px" />
            </asp:CommandField>--%>
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
    <asp:panel ID="pnlAdd" runat="server" Width="1018px" Height="212px" 
            style="margin-top: 1px">
         
    <table style="width: 93%; height: 26px;" >
            <tr>
                <td style="width: 69px; text-align: left;">
                    <asp:Label ID="lblActName" runat="server" Text="Act"  Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td class="style21" style="width: 181px">
                    <asp:TextBox ID="txtActName" runat="server" Font-Names="arial"  Font-Size="X-Small" Width="248px" Height="14px"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ddlActName" runat="server" Height="14px" Width="377px"  Font-Names="arial" Font-Size="X-Small" AutoPostBack="True" 
                        onselectedindexchanged="ddlActName_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                 <td>
                <asp:TextBox ID="txtActDetailId" runat="server" Font-Names="arial" Visible="false" 
                         Font-Size="X-Small" Width="38px"></asp:TextBox>
                        <%--scs020316--%>
                        <asp:TextBox ID="txtActId" runat="server" Font-Names="arial" Visible="false"
                        Font-Size="X-Small" Width="38px" height="19px"></asp:TextBox>
                        <asp:TextBox ID="txtOldFDate" runat="server" Text="" Width="38px"  Visible="false"
                        Font-Size="X-Small" style="font-family: arial" Height="19px" ></asp:TextBox>
                        <asp:TextBox ID="txtOldTDate" runat="server" Text="" Width="38px"  Visible="false"
                        Font-Size="X-Small" style="font-family: arial" Height="19px" ></asp:TextBox>
                </td>
            </tr>
           </table>
        
           <table style="width: 1006px; height: 177px;">
            <tr>
                            <td style="width: 95px" class="style23">
            <asp:Label ID="lblClassificationAct" runat="server" Text="Classification Act"  Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
            </td>
             <td class="style21" style="width: 121px">
                    <asp:DropDownList ID="ddlClassificationAct" runat="server" Height="14px" Width="125px"  DataTextField="ClassificationAct" Font-Names="arial" 
                        Font-Size="X-Small">  </asp:DropDownList> <%--DataValueField="Id"--%>
                </td>
               <%-- </tr>
                <tr>--%>
              <td style="width: 66px; text-align: left;">
                    <asp:Label ID="lblChapter" runat="server" Text="Chapter"  Font-Names="arial" Font-Size="X-Small" Font-Bold="false"></asp:Label>
                </td>
              <td class="style21" style="width: 181px">
                    <asp:TextBox ID="txtChapter" runat="server" Font-Names="arial" Font-Size="X-Small" Width="130px" height="14px"></asp:TextBox>
                </td>
                <%--</tr>
                <tr>--%>
               <td class="style1" style="width: 95px; text-align: left;">
                    <asp:Label ID="lblHead" runat="server" Text="Head"  Font-Names="arial" Font-Size="X-Small" Font-Bold="false"></asp:Label>
                </td>
                <td class="style21" style="width: 121px">
                    <asp:TextBox ID="txtHead" runat="server" Font-Names="arial"  Font-Size="X-Small" Width="131px" height="14px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                    <td style="width: 66px; text-align: left;">
                        <asp:Label ID="lblSection" runat="server" Text="Section"  Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style21" style="width: 181px">
                        <asp:TextBox ID="txtSection" runat="server" Font-Names="arial" Font-Size="X-Small" height="14px" width="117px"></asp:TextBox>
                    </td>
               <%-- </tr>
                <tr>--%>
                    <td style="width: 95px; text-align: left;">
                        <asp:Label ID="lblActRule" runat="server" Text="Rule" Font-Names="arial" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="style21" style="width: 121px">
                        <asp:TextBox ID="txtActRule" runat="server" Font-Names="arial" Font-Size="X-Small" Width="130px" height="14px"></asp:TextBox>
                    </td>
              <%--  </tr>
                  <tr>--%>
                    <td style="width: 66px; text-align: left;">
                        <asp:Label ID="lblDescription" runat="server" Text="Description" Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style21" style="width: 181px">
                        <asp:TextBox ID="txtDescription" runat="server" Font-Names="arial" TextMode = "MultiLine"  MaxLength="512" Font-Size="X-Small" Width="468px" height="28px"></asp:TextBox>
                    </td>
            </tr>
            <tr>
                    <td style="width: 66px; text-align: left;">
                        <asp:Label ID="lblFrequency" runat="server" Text="Frequency"  Font-Names="arial"  Font-Size="X-Small" Font-Bold="true"></asp:Label>
                    </td>
                    <td class="style21" style="width: 181px">
                        <asp:TextBox ID="txtFrequency" runat="server" Font-Names="arial" Font-Size="X-Small" height="14px" width="117px"></asp:TextBox>
                    </td>
                    <td style="width: 95px; text-align: left;">
                        <asp:Label ID="lblImplicationSection" runat="server" Text="Implication Section" Font-Names="arial" Font-Size="X-Small" Font-Bold="false"></asp:Label>
                    </td>
                    <td class="style21" style="width: 121px">
                        <asp:TextBox ID="txtImplicationSection" runat="server" Font-Names="arial" Font-Size="X-Small" Width="130px" height="14px"></asp:TextBox>
                    </td>
                    <td style="width: 66px; text-align: left;">
                        <asp:Label ID="lblImplication" runat="server" Text="Implication"  Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style21" style="width: 181px">
                        <asp:TextBox ID="txtImplication" runat="server" Font-Names="arial" MaxLength="512" Font-Size="X-Small" Width="468px" height="28px" TextMode = "MultiLine" ></asp:TextBox>
                    </td>
            </tr>
            <tr>
                    <td style="width: 66px; text-align: left; height: 21px;">
                        <asp:Label ID="lblLiability" runat="server" Text="Liability"  Font-Names="arial"  Font-Size="X-Small" Font-Bold="false"></asp:Label>
                    </td>
                    <td class="style21" style="width: 181px; height: 21px;">
                        <asp:TextBox ID="txtLiability" runat="server" Font-Names="arial"  Font-Size="X-Small" height="14px" width="117px"></asp:TextBox>
                    </td>
               <%-- </tr>
                <tr>--%>
                    <td style="width: 95px; text-align: left; height: 21px;">
                        <asp:Label ID="lblAffectedPerson" runat="server" Text="Affected Person"  Font-Names="arial" Font-Size="X-Small" Font-Bold="false"></asp:Label>
                    </td>
                    <td class="style21" style="width: 121px; height: 21px;">
                        <asp:TextBox ID="txtAffectedPerson" runat="server" Font-Names="arial" Font-Size="X-Small" Width="130px" height="14px"></asp:TextBox>
                    </td>
                
                
                   <td style="width: 95px; text-align: left; height: 21px;">
                        <asp:Label ID="lblImportance" runat="server" Text="Importance" Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style21" style="width: 181px; height: 21px;">
                        <asp:TextBox ID="txtImportance" runat="server" Font-Names="arial"  
                            Font-Size="X-Small" Width="28px" height="16px" style="text-align: center"></asp:TextBox>
                      <asp:CheckBox ID="ChkIsNew" runat="server" Font-Names="arial" Font-Size="X-Small" Text="Is New" Visible="True" />
                        <asp:CheckBox ID="ChkValidityStatus" runat="server" Font-Names="arial" Font-Size="X-Small" Text="Validity Status" Visible="True" />
                    </td>
                    </tr>
                  <tr>
                     <%--<td style="width: 95px; text-align: left;">
                       <asp:Label ID="lblValidityStatus" runat="server" Text="Validity Status" Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                        
                    </td>--%>
                   
                     <%--<td style="width: 95px; text-align: left;">
                        <asp:Label ID="lblIsNew" runat="server" Text="Is New"  Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                    </td>--%>
                   <%-- <td class="style21" style="width: 121px">
                        
                         
                    </td>
                     <td class="style21" style="width: 121px">
                       
                    </td>--%>
                     <td style="width: 95px; text-align: left;">
                        <asp:Label ID="lblVersionNumber" runat="server" Text="Version Number" Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                    </td>
                    <td class="style21" style="width: 121px">
                        <asp:TextBox ID="txtVersionNumber" runat="server" Font-Names="arial"  Font-Size="X-Small" Width="130px" height="14px"></asp:TextBox>
                    </td>

                 
           <%-- </tr>

            <tr>--%>
             <td style="width: 60px; font-size: X-Small; font-family: arial; height: 32px;">
                    <asp:Label ID="lblFromDate" runat="server"  Text="Effective Date" Font-Size="X-Small" Font-Names="arial" Font-Bold="true" ></asp:Label>
                </td>
         <td style="width: 137px; height: 32px;">
                    <asp:TextBox ID="txtEffectiveDate" runat="server" Text="" Width="91px"  Font-Size="X-Small" style="font-family: arial" Height="21px" ></asp:TextBox>
                    <asp:ImageButton ID="imgEffectiveDate" runat="server" ImageUrl="~/Images/Calendar.gif" />
                    <asp:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEffectiveDate" runat="server" PopupButtonID="imgEffectivedate" Format="dd/MM/yyyy"></asp:CalendarExtender>
                    
                </td>
         <td style="width: 72px; font-size: X-Small; font-family: arial; height: 32px;">
                    <asp:Label ID="lblToDate" runat="server" Text="Expiry Date" Font-Size="X-Small" Font-Names="arial" Font-Bold="true" ></asp:Label>
                </td>

         <td style="width: 187px">
                    <asp:TextBox ID = "txtExpiryDate" runat= "server" Text="" Width = "85px" 
                        Font-Size="X-Small" Font-Names= "arial" Height= "20px"></asp:TextBox>
                    <asp:ImageButton ID="imgExpiryDate" runat= "server" ImageUrl="~/Images/calendar.gif" />
                    <asp:CalendarExtender  ID= "Cal1" TargetControlID= "txtExpiryDate" runat="server" PopupButtonID="imgExpiryDate" Format= "dd/MM/yyyy"></asp:CalendarExtender>
                    <asp:Label ID="Label1" runat="server" Text=" " Font-Size="X-Small" Font-Names="arial" Font-Bold="true" Width="10px"></asp:Label>
                    <asp:Button ID="btnSave" runat="server" Text="Save" Font-Names="arial"   onclick="btnSave_Click" Font-Size="X-Small" />
                </td>

               <%-- <td class="style21" style="width: 121px">
                    
                </td>--%>
               
                
            </tr>
        </table>
    </asp:panel>
    
    </div>
</asp:Content>

