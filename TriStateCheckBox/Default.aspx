<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="Tcp.Controls" Namespace="Tcp.Controls" TagPrefix="tcp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script language="javascript" type="text/javascript">
        function TcpCheckBox1_ClientCheckedChanged(sender, e) {
            $get('Label1').innerHTML = Get_CheckedStateText(e.checkedState);
        }

        function TcpCheckBox3_ClientCheckedChanged(sender, e) {
            $get('Label3').innerHTML = Get_CheckedStateText(e.checkedState);
        }

        function Get_CheckedStateText(checkedState) {
            if (checkedState == TcpCheckBoxCheckState.Checked) {
                return 'Checked';
            }
            else if (checkedState == TcpCheckBoxCheckState.UnChecked) {
                return 'UnChecked';
            }
            else if (checkedState == TcpCheckBoxCheckState.Indeterminate) {
                return 'Indeterminate';
            }
            else {
                return 'Unknown';
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <tcp:TcpCheckBox ID="TcpCheckBox1" runat="server" Text="No Postback" OnClientCheckedChanged="TcpCheckBox1_ClientCheckedChanged" />
        <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
    <div>
        <tcp:TcpCheckBox ID="TcpCheckBox2" runat="server" Text="With Postback" AutoPostBack="true"
            OnCheckedChanged="TcpCheckBox2_CheckedChanged" />
        <asp:Label ID="Label2" runat="server"></asp:Label>
    </div>
    <div>
        <tcp:TcpCheckBox ID="TcpCheckBox3" runat="server" TriState="true" Text="Tri-State No Postback"
            OnClientCheckedChanged="TcpCheckBox3_ClientCheckedChanged" />
        <asp:Label ID="Label3" runat="server"></asp:Label>
    </div>
    <div>
        <tcp:TcpCheckBox ID="TcpCheckBox4" runat="server" TriState="true" AutoPostBack="true"
            Text="Tri-State With Postback" OnCheckedChanged="TcpCheckBox4_CheckedChanged" />
        <asp:Label ID="Label4" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
