<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage1.master" AutoEventWireup="true" CodeFile="ActMaster.aspx.cs" Inherits="ActMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="16px" Width="870px"> </asp:Panel>
    <div style="overflow:auto; width: 1026px;">
  <asp:Panel ID="pnlActLoad" runat="server" Height="26px" Width="870px">
            <table style="width: 100%; height: 23px;">
                <tr>
                    
                    <td style="width: 87px; ">
                        <asp:Label ID="lblACtLoad" runat="server" Text="Act" Font-Names="arial" Font-Size="X-Small" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="width: 196px" class="style21">
                        <asp:DropDownList ID="ddlLoadAct" runat="server" Font-Names="arial" 
                            Font-Size="X-Small" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnGo" runat="server" Text="Go" Font-Names="arial" 
                            Font-Size="X-Small" onclick="btnGo_Click" />
                            
                    </td>
                  
                </tr>
            </table>
        </asp:Panel>
        <br />
    <asp:panel ID="Pnlgv" runat="server" Height="244px" Width="979px">
        <div style="overflow:auto; height:235px; width:956px">
    <asp:GridView ID="GrdActMaster" runat="server" CellPadding="3" 
            Font-Size="X-Small" Width="842px" Font-Names="arial" AutoGenerateColumns="False" 
            Height="16px" GridLines="Vertical" BackColor="White" BorderColor="#999999" 
                BorderStyle="None" BorderWidth="1px" 
                onrowcancelingedit="GrdCompanyMaster_RowCancelingEdit" 
                onrowdeleting="GrdCompanyMaster_RowDeleting" 
                onrowediting="GrdActMaster_RowEditing" 
                onrowupdating="GrdCompanyMaster_RowUpdating">
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
            <asp:Label ID="lblActId" runat="server" Text='<%# Eval("ActId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtActId" runat="server" Text='<%# Bind("ActId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="20px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="ActDtlId" Visible="False">
            <ItemTemplate>
            <asp:Label ID="lblActDtlId" runat="server" Text='<%# Eval("ActDtlId") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtActDtlId" runat="server" Text='<%# Bind("ActDtlId") %>' ReadOnly="true" Font-Names="arial" Font-Size="X-Small" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="20px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Act">
            <ItemTemplate>
            <asp:Label ID="lblActName" runat="server" Text='<%# Eval("ActName") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtActName" runat="server" Text='<%# Bind("ActName") %>'  Font-Names="arial" Font-Size="X-Small" Width="250px" ></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="250px" />
                 <ItemStyle Width="250px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Classification Act">
             <ItemTemplate>
            <asp:Label ID="lblClassification" runat="server" Text='<%# Eval("ClassificationAct") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <%--<asp:TextBox ID="txtCompanyType" runat="server" Text='<%# Bind("CompanyFlag") %>'  Font-Names="arial" Font-Size="X-Small"></asp:TextBox>--%>
             <asp:DropDownList ID="ddlClassificationAct" runat="server"  DataTextField="ClassificationAct"   DataSource='<%#LoadClassification() %>'
                  Font-Names="arial" Font-Size="X-Small">
                 <asp:ListItem Text="--Parent--" Value="0" />
                 </asp:DropDownList>
            </EditItemTemplate>
                <HeaderStyle Width="60px" />
            </asp:TemplateField>

          
             <asp:TemplateField HeaderText="Chapter">
            <ItemTemplate>
            <asp:Label ID="lblChapter" runat="server" Text='<%# Eval("Chapter") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtChapter" runat="server" Text='<%# Bind("Chapter") %>'  Font-Names="arial" Font-Size="X-Small" Width="30px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="20px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Head">
            <ItemTemplate>
            <asp:Label ID="lblHead" runat="server" Text='<%# Eval("Head") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtHead" runat="server" Text='<%# Bind("Head") %>'  Font-Names="arial" Font-Size="X-Small" Width="30px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="20px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Section">
            <ItemTemplate>
            <asp:Label ID="lblSection" runat="server" Text='<%# Eval("Section") %>'  Font-Names="arial" Font-Size="X-Small" ></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtSection" runat="server" Text='<%# Bind("Section") %>'  Font-Names="arial" Font-Size="X-Small" Width="30px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="20px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Rule">
            <ItemTemplate>
            <asp:Label ID="lblActRule" runat="server" Text='<%# Eval("ActRule") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtActRule" runat="server" Text='<%# Bind("ActRule") %>'  Font-Names="arial" Font-Size="X-Small" Width="30px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="20px" />
            </asp:TemplateField>
             <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
            <asp:Label ID="lblDescription" runat="server" Text='<%# Eval("Description") %>'  Font-Names="arial" Font-Size="X-Small"></asp:Label></ItemTemplate>
             <EditItemTemplate>
            <asp:TextBox ID="txtDescription" runat="server" Text='<%# Bind("Description") %>'  Font-Names="arial" Font-Size="X-Small" Width="230px"></asp:TextBox></EditItemTemplate>
                <HeaderStyle Width="20px" />
            </asp:TemplateField>
               <asp:TemplateField HeaderText="IsActive">
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
            <HeaderStyle Width="20px" />
            </asp:CommandField>
            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" >
            <HeaderStyle Width="30px" />
            </asp:CommandField>
        </Columns>
        <sortedascendingcellstyle backcolor="#F1F1F1" />
        <sortedascendingheaderstyle backcolor="#0000A9" />
        <sorteddescendingcellstyle backcolor="#CAC9C9" />
        <sorteddescendingheaderstyle backcolor="#000065" />
    </asp:GridView>
    </div>
    </asp:panel>
    <asp:panel ID="pnlAdd" runat="server" Width="813px" 
        Height="806px" style="margin-top: 0px">
         <asp:TextBox ID="txtActDetailId" runat="server" Font-Names="arial" Visible="false"
                        Font-Size="X-Small"></asp:TextBox>
    <table style="width: 83%; height: 180px;" >
            <tr>
                <td class="style1" style="width: 174px; text-align: left;">
                    <asp:Label ID="lblActName" runat="server" Text="Act"  Font-Names="arial" 
                        Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td class="style21" style="width: 181px">
                    <asp:TextBox ID="txtActName" runat="server" Font-Names="arial" 
                        Font-Size="X-Small" Width="168px"></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList ID="ddlActName" runat="server" Height="16px" Width="377px" 
                        Font-Names="arial" Font-Size="X-Small" AutoPostBack="True" 
                        onselectedindexchanged="ddlActName_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
            <td style="width: 174px">
            <asp:Label ID="lblClassificationAct" runat="server" Text="Classification Act"  Font-Names="arial" 
                        Font-Size="X-Small" Font-Bold="True"></asp:Label>
            </td>
            <td class="style21" style="width: 181px">
                    <asp:DropDownList ID="ddlClassificationAct" runat="server" Height="21px" Width="119px" 
                        DataTextField="ClassificationAct" Font-Names="arial" 
                        Font-Size="X-Small">
                    </asp:DropDownList> <%--DataValueField="Id"--%>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 174px; text-align: left;">
                    <asp:Label ID="lblChapter" runat="server" Text="Chapter"  Font-Names="arial" 
                        Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td class="style21" style="width: 181px">
                    <asp:TextBox ID="txtChapter" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 174px; text-align: left;">
                    <asp:Label ID="lblHead" runat="server" Text="Head"  Font-Names="arial" 
                        Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td class="style21" style="width: 181px">
                    <asp:TextBox ID="txtHead" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 174px; text-align: left;">
                    <asp:Label ID="lblSection" runat="server" Text="Section"  Font-Names="arial" 
                        Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td class="style21" style="width: 181px">
                    <asp:TextBox ID="txtSection" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 174px; text-align: left;">
                    <asp:Label ID="lblActRule" runat="server" Text="Rule" 
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td class="style21" style="width: 181px">
                    <asp:TextBox ID="txtActRule" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
            </tr>
              <tr>
                <td class="style1" style="width: 174px; text-align: left;">
                    <asp:Label ID="lblDescription" runat="server" Text="Description" 
                        Font-Names="arial" Font-Size="X-Small" Font-Bold="True"></asp:Label>
                </td>
                <td class="style21" style="width: 181px">
                    <asp:TextBox ID="txtDescription" runat="server" Font-Names="arial" 
                        Font-Size="X-Small"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1" style="width: 174px">
                    &nbsp;</td>
                <td class="style21" style="width: 181px">
                    <asp:Button ID="btnSave" runat="server" Text="Save" Font-Names="arial" 
                        onclick="btnSave_Click" Font-Size="X-Small" />
                </td>
                
            </tr>
        </table>
    </asp:panel>
    
    </div>
</asp:Content>

