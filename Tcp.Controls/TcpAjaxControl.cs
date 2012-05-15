using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

namespace Tcp.Controls
{
    public abstract class TcpAjaxControl : TcpWebControl, IScriptControl
    {
        # region Properties

        protected ScriptManager SM { get; set; }

        # endregion

        # region Shared Methods

        public void AddSafeCssReference(string actualFile)
        {
            if (!Page.ClientScript.IsClientScriptBlockRegistered(GetType(), actualFile))
            {
                Page.ClientScript.RegisterClientScriptBlock(GetType(), actualFile, string.Format("<link rel='stylesheet' type='text/css' href='{0}'/>\r\n", actualFile));
            }
        }

        protected virtual IEnumerable<ScriptReference> GetScriptReferences()
        {
            return new ScriptReference[0];
        }

        protected virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            return new ScriptDescriptor[0];
        }

        # endregion

        # region Event Handlers

        protected override void OnPreRender(EventArgs e)
        {
            if (!this.DesignMode)
            {
                SM = ScriptManager.GetCurrent(Page);

                if (SM == null)
                {
                    throw new HttpException("A ScriptManager control must exist on the current page.");
                }
                SM.RegisterScriptControl(this);
            }
            base.OnPreRender(e);
        }

        protected override void Render(HtmlTextWriter writer)
        {
            base.Render(writer);
            if (!this.DesignMode)
            {
                SM.RegisterScriptDescriptors(this);
            }
        }

        # endregion

        #region IScriptControl Members

        IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
        {
            return GetScriptDescriptors();
        }

        IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
        {
            return GetScriptReferences();
        }

        #endregion
    }
}
