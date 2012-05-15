using System.Web.UI;
using System.Web.UI.WebControls;
using Tcp.AspNetCommon.Helper;

namespace Tcp.Controls
{
    public abstract class TcpWebControl : WebControl
    {
        protected Conversion Conversion
        {
            get { return Conversion.GetInstance(); }
        }

        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }
    }
}
