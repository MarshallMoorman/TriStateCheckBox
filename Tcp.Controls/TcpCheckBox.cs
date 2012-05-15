using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

[assembly: WebResource("Tcp.Controls.Javascript.TcpCheckBox.TcpCheckBox.js", "text/javascript", PerformSubstitution = true)]
[assembly: WebResource("Tcp.Controls.StyleSheets.TcpCheckBox.css", "text/css", PerformSubstitution = true)]
[assembly: WebResource("Tcp.Controls.Images.TcpCheckBox.CheckBox_Sprites.gif", "image/gif")]

namespace Tcp.Controls
{
    # region Enums

    public enum TcpCheckedState
    {
        UnChecked = 0,
        Checked = 1,
        Indeterminate = 2,
    }

    # endregion

    [ToolboxData(@"<{0}:TcpCheckBox runat=""server"" />")]
    [DefaultEvent("CheckedChanged")]
    public class TcpCheckBox : TcpAjaxControl, ITextControl, IPostBackEventHandler
    {
        private HiddenField hdnCheckedState = new HiddenField();
        private Label lblCheckBox = new Label();
        private CheckBox chkDesign = new CheckBox();

        # region Constructors

        public TcpCheckBox()
        {
            hdnCheckedState.ID = "hdnCheckedState";
            lblCheckBox.ID = "lblCheckBox";
            CheckedState = TcpCheckedState.Indeterminate;

            if (this.DesignMode)
            {
                base.Controls.Add(chkDesign);
            }
            else
            {
                base.Controls.Add(lblCheckBox);
                base.Controls.Add(hdnCheckedState);
            }
        }

        # endregion

        # region Properties

        public bool AutoPostBack
        {
            get { return Conversion.ToBoolean(ViewState["AutoPostback"]); }
            set { ViewState["AutoPostback"] = value; }
        }

        public bool Checked
        {
            get { return CheckedState == TcpCheckedState.Checked; }
            set { CheckedState = value ? TcpCheckedState.Checked : TcpCheckedState.UnChecked; }
        }

        public TcpCheckedState CheckedState
        {
            get { return GetCheckedState(); }
            set { SetCheckedState(value); }
        }

        public override string ID
        {
            get { return base.ID; }
            set
            {
                base.ID = value;
                hdnCheckedState.ID = "hdnCheckedState_" + value;
                lblCheckBox.ID = "lblCheckBox_" + value;
            }
        }

        public string OnClientCheckedChanged { get; set; }

        protected override HtmlTextWriterTag TagKey
        {
            get { return HtmlTextWriterTag.Span; }
        }

        public string Text
        {
            get { return lblCheckBox.Text; }
            set { lblCheckBox.Text = value; }
        }

        public bool TriState
        {
            get { return Conversion.ToBoolean(ViewState["TriState"]); }
            set { ViewState["TriState"] = value; }
        }

        # endregion

        # region Events

        public event EventHandler<CheckedChangedEventArgs> CheckedChanged;

        # endregion

        # region Event Handlers

        protected override void OnInit(System.EventArgs e)
        {
            if (this.DesignMode)
            {
                chkDesign.Checked = CheckedState == TcpCheckedState.Checked;
                chkDesign.Text = Text;
                
                base.Controls.Add(chkDesign);
            }
            base.OnInit(e);
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            this.Style.Add("padding-left", "2px");

            AddSafeCssReference(Page.ClientScript.GetWebResourceUrl(GetType(), "Tcp.Controls.StyleSheets.TcpCheckBox.css"));
            lblCheckBox.Attributes.Add("onselectstart", "return false;");

            if (CheckedState == TcpCheckedState.Indeterminate && !TriState)
            {
                CheckedState = TcpCheckedState.UnChecked;
            }

            if (Text.Trim().Length == 0)
            {
                Text = "&nbsp;";
            }

            base.OnPreRender(e);
        }

        # endregion

        # region Shared Methods

        protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            ScriptControlDescriptor descriptor = new ScriptControlDescriptor(GetType().ToString(), this.ClientID);
            descriptor.AddProperty("_hdnCheckedStateCID", hdnCheckedState.ClientID);
            descriptor.AddProperty("_lblCheckBoxCID", lblCheckBox.ClientID);
            descriptor.AddProperty("enabled", this.Enabled);
            descriptor.AddProperty("triState", this.TriState);
            if (AutoPostBack)
            {
                descriptor.AddProperty("onclick", Page.ClientScript.GetPostBackEventReference(this, string.Empty, true));
            }

            string[] clientEvents = { "ClientCheckedChanged" };

            foreach (string eventName in clientEvents)
            {
                string eventHandler = (string)DataBinder.GetPropertyValue(this, string.Format("On{0}", eventName));
                if (!String.IsNullOrEmpty(eventHandler))
                {
                    descriptor.AddEvent(eventName[0].ToString().ToLower() + eventName.Substring(1), eventHandler);
                }
            }

            //descriptor.AddEvent("click", "_clickHandler");

            return new ScriptDescriptor[] { descriptor };
        }

        protected override IEnumerable<ScriptReference> GetScriptReferences()
        {
            ScriptReference reference = new ScriptReference();
            reference.Path = Page.ClientScript.GetWebResourceUrl(GetType(), "Tcp.Controls.Javascript.TcpCheckBox.TcpCheckBox.js");

            return new ScriptReference[] { reference };
        }

        # endregion

        # region Helper Methods

        private TcpCheckedState GetCheckedState()
        {
            return (TcpCheckedState)Conversion.ToInt32(hdnCheckedState.Value);
        }

        private void SetCheckedState(TcpCheckedState state)
        {
            hdnCheckedState.Value = ((int)state).ToString();
        }

        # endregion

        #region IPostBackEventHandler Members

        public void RaisePostBackEvent(string eventArgument)
        {
            CheckedChangedEventArgs e = new CheckedChangedEventArgs(GetCheckedState());
            RaiseCheckedChangedEvent(e);
        }

        private void RaiseCheckedChangedEvent(CheckedChangedEventArgs e)
        {
            if (CheckedChanged != null)
            {
                CheckedChanged(this, e);
            }
        }

        #endregion
    }
}
