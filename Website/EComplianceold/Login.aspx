<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 99%; height: 398px;">
        <tr>
            <td style="width: 526px">
                <asp:Panel ID="Panel1" runat="server" Height="485px" Style="margin-top: 0px" Font-Size="Small"
                    Width="553px">
      <asp:Panel ID="pnlheading" runat="server" Height="152px" Width="489px">
                        <%--<asp:Label ID="lblHead" runat="server" Text="E - Compliance" 
                        Font-Bold="True" Font-Italic="True" Font-Size="Large" ForeColor="#000066" 
                        
                        style="text-align: center;font-style:italic; font-size: xx-large; text-decoration: underline;" Width="621px" 
                        Height="33px"></asp:Label>--%>
                        <asp:Image ID="imgEcompliance" runat="server" ImageUrl="~/Images/E-Compliance.jpg" 
                Width="469px" ImageAlign="Middle" style="text-align: right" />
                        </asp:Panel>
                        <asp:Panel ID="pnlIntroduction" runat="server" Height="288px" Width="535px"  >
                        <p style="height: 57px; width: 526px;text-align: justify"  >
                           <%-- &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; --%>
                          <%--  <span style="font-family: Arial">--%>
                            Organizations are governed by various statutory laws of the land and are audited 
                            for its compliance both internally and externally. To provide quality output and 
                            maximize business, organization sets their own standards and follows set of 
                            procedures and activities to be completed <b>On time Every time.</b> </p>
                           
                            <p  style= "height: 46px; width: 535px;text-align: justify">&nbsp;These standards are not only applicable to 
                                employees but also to machines and instruments involved in the business chain. 
                                All these standards are subject to audit and qualify for creating value among 
                                customers and future prospects of the organization. </p> 
                            
                            <p style= "font:Arial;text-align: justify" ><b>“e- Compliance” </b>is an excellent software tool that provides 
                            facility to ensure, compliance of statutory Laws, Organization’s own standard 
                            practice and other Compliance requirement in order to achieve the goals.<%-- </span>  --%></p>
                           </asp:Panel>
            <%--        <p>
                        <strong style="font-style: italic">e- Compliance provides following features to
                        </strong> 
</p>
                    <p>
                        1. Create all ACTS and corresponding Compliance Activities
                    </p>
                    <p>
                        2. Create the Due date for completion of the Activity</p>
                    <p>
                        &nbsp;3. Create trigger date for reminding about the Activity
                    </p>
                    <p>
                        4. Assign the nature in terms of severity for the activity
                    </p>
                    <p>
                        5. Assign the frequency in terms of (Yearly, Half yearly, Quarterly, Monthly, 
                        Weekly, daily and as and when) to the activities</p>
                    <p>
                        &nbsp;6. Assign responsible (group of) persons and reviewers to maintain strict 
                        compliance. 7. Reminds responsible group for pending activities by daily email 
                        based on trigger date. 8. Auto generate Red Alert Email to the top executive for 
                        Severity “A” activities pending beyond Due date 9. Store template Documents for 
                        the Activities 10. Store documents as proof of action completion.
                    </p>--%>
                </asp:Panel>
                <asp:Panel ID="pnlPower" runat="server" Height="16px" Width="565px">
                    <asp:Label ID="lblPower" runat="server" Text="Powered by:Ganini Infotech India Pvt Ltd"
                        Font-Size="XX-Small"></asp:Label>
                </asp:Panel>
            </td>
            <td align="center">
                <asp:Panel ID="pnlogin" runat="server" Height="379px">
                
                            <table style="height: 346px">
                                <tr>
                                    <td>
                                        <img src="Images/login_topbg.jpg" alt="" class="fl" style="width: 392px; height: 12px;
                                            margin-left: 0px" />
                                    
                                                <div class="txtBoxs" align="left" style="width: 387px; height: 262px; border-right-style: groove;
                                                    border-left-style: groove;">
                                                    <div class="clr" style="text-align: center">
                                                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/login.jpg" Width="80px"
                                                            ImageAlign="Middle" Height="36px" />
                                                    </div>
                                                    <table style="width: 380px; height: 226px">
                                                        <tr>
                                                            <td style="width: 73px; height: 54px; text-align: right;">
                                                                <asp:Label ID="lblUserName" runat="server" Text="User Name" Font-Size="Small" Font-Names="Verdana"></asp:Label>
                                                            </td>
                                                            <td style="height: 54px; width: 215px;">
                                                                <asp:TextBox ID="txtUsername" runat="server" Placeholder="UserName" Width="246px"
                                                                    Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 73px; height: 45px; text-align: right;">
                                                                <asp:Label ID="lblPwd" runat="server" Text="Password" Font-Size="Small" Font-Names="Verdana"></asp:Label>
                                                            </td>
                                                            <td style="height: 45px; width: 215px;">
                                                                <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" Placeholder="Password"
                                                                    Width="244px" Font-Size="XX-Small"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 73px; height: 24px;">
                                                            </td>
                                                            <td style="width: 215px; height: 24px; text-align: center;">
                                                                <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" Font-Size="XX-Small"
                                                                    Font-Names="Verdana" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="height: 41px">
                                                            </td>
                                                            <td style="width: 215px; height: 41px;">
                                                                <asp:LinkButton ID="lnkForgot" runat="server" Enabled="true" ForeColor="Black" Width="221px"
                                                                    Font-Size="XX-Small" Font-Names="Verdana" OnClick="lnkForgot_Click" 
                                                                    Height="16px" style="text-align: center">Forgot Password</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <img src="Images/login_bottombg.jpg" alt="" class="fl" style="width: 388px; height: 11px;" />
                                                </div>
                                                </td>
                                                </tr>
                                                </table>

                </asp:Panel>
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <asp:Panel ID="pnlView" runat="server" Font-Names="Verdana" Style="text-align: right">
                    <asp:Label ID="lblView" runat="server" Text="Best Viewed in:1280x720" Font-Bold="False"
                        Font-Names="Verdana" Font-Size="XX-Small" Style="text-align: right">
                    </asp:Label>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
