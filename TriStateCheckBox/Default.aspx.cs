using System;
using Tcp.Controls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void TcpCheckBox2_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Label2.Text = TcpCheckBox2.CheckedState.ToString();
    }

    protected void TcpCheckBox4_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        Label4.Text = TcpCheckBox4.CheckedState.ToString();
    }
}
