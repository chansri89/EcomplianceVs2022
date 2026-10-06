<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogOut.aspx.cs" Inherits="LogOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 205px">
        <table style="width: 1266px; height: 188px">
            <tr>
                <td align="center">
                <asp:Label ID="lblLicence" runat="server" Text="You Have Logged out of System." Font-Size="Large" ForeColor="Blue"
                 Font-Bold = "true"></asp:Label> 
               </td>
            </tr>
            <tr>
                <td align="center">
                <asp:Label ID="lblCheck" runat="server" Text="Please click on the right corner of Window X and close" Font-Size="Large" ForeColor="Blue"
                 Font-Bold = "true"></asp:Label> 
               </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
