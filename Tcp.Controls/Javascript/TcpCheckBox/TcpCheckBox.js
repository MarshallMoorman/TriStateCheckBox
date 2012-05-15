/// <reference name="MicrosoftAjax.js"/>

Type.registerNamespace("Tcp.Controls");

TcpCheckBoxCheckState = {
    UnChecked: 0,
    Checked: 1,
    Indeterminate: 2
};

Tcp.Controls.TcpCheckBox = function(element) {
    Tcp.Controls.TcpCheckBox.initializeBase(this, [element]);

    this._clickDelegate = null;
    this._hoverDelegate = null;
    this._mousedownDelegate = null;
    this._unhoverDelegate = null;

    this._hdnCheckedStateCID = null;
    this._lblCheckBoxCID = null;
    this.enabled = false;
    this.triState = false;
    this.onclick = null;
}

Tcp.Controls.TcpCheckBox.prototype = {
    initialize: function() {
        var element = this.get_element();
        if (this._clickDelegate === null) {
            this._clickDelegate = Function.createDelegate(this, this._clickHandler);
        }
        Sys.UI.DomEvent.addHandler(element, 'click', this._clickDelegate);

        if (this._hoverDelegate === null) {
            this._hoverDelegate = Function.createDelegate(this, this._hoverHandler);
        }
        Sys.UI.DomEvent.addHandler(element, 'mouseover', this._hoverDelegate);
        Sys.UI.DomEvent.addHandler(element, 'focus', this._hoverDelegate);

        if (this._unhoverDelegate === null) {
            this._unhoverDelegate = Function.createDelegate(this, this._unhoverHandler);
        }
        Sys.UI.DomEvent.addHandler(element, 'mouseout', this._unhoverDelegate);
        Sys.UI.DomEvent.addHandler(element, 'blur', this._unhoverDelegate);

        this._updateUI();

        Tcp.Controls.TcpCheckBox.callBaseMethod(this, 'initialize');
    },

    // inner control accessors
    get_hiddenField: function() {
        return $get(this._hdnCheckedStateCID);
    },

    get_label: function() {
        return $get(this._lblCheckBoxCID);
    },

    // text property accessors.
    get_text: function() {
        return this.get_element().innerHTML;
    },
    set_text: function(value) {
        this.get_element().innerHTML = value;
    },

    // Bind and unbind to click event.
    add_click: function(handler) {
        this.get_events().addHandler('click', handler);
    },
    remove_click: function(handler) {
        this.get_events().removeHandler('click', handler);
    },

    // Bind and unbind to click event.
    add_clientCheckedChanged: function(handler) {
        this.get_events().addHandler('clientCheckedChanged', handler);
    },
    remove_clientCheckedChanged: function(handler) {
        this.get_events().removeHandler('clientCheckedChanged', handler);
    },

    // Bind and unbind to hover event.
    add_hover: function(handler) {
        this.get_events().addHandler('hover', handler);
    },
    remove_hover: function(handler) {
        this.get_events().removeHandler('hover', handler);
    },

    // Bind and unbind to unhover event.
    add_unhover: function(handler) {
        this.get_events().addHandler('unhover', handler);
    },
    remove_unhover: function(handler) {
        this.get_events().removeHandler('unhover', handler);
    },

    // public methods
    changeState: function() {
        var checkedState = this.get_CheckedState();
        if (checkedState == TcpCheckBoxCheckState.Checked && !this.triState) {
            this.set_UnCheckedState();
        }
        else if (checkedState == TcpCheckBoxCheckState.Checked) {
            this.set_IndeterminateState();
        }
        else if (checkedState == TcpCheckBoxCheckState.Indeterminate) {
            this.set_UnCheckedState();
        }
        else {
            this.set_CheckedState();
        }

        // postback to the server if required
        if (this.onclick) {
            eval(this.onclick);
        }
    },

    get_CheckedState: function() {
        var hiddenField = this.get_hiddenField();
        return parseInt(hiddenField.value, 10);
    },

    raiseEvent: function(eventName, args) {
        var handler = this.get_events().getHandler(eventName);
        if (handler) {
            handler(this, args);
        }
    },

    set_CheckedState: function() {
        this.get_hiddenField().value = TcpCheckBoxCheckState.Checked;
        this._updateUI();
    },

    set_IndeterminateState: function() {
        this.get_hiddenField().value = TcpCheckBoxCheckState.Indeterminate;
        this._updateUI();
    },

    set_UnCheckedState: function() {
        this.get_hiddenField().value = TcpCheckBoxCheckState.UnChecked;
        this._updateUI();
    },

    // private methods
    _updateUI: function() {
        var checkedState = this.get_CheckedState();
        var label = this.get_label();
        var cssClass = '';
        if (checkedState == TcpCheckBoxCheckState.Checked) {
            if (this.enabled) {
                cssClass = 'TcpCheckBox Checked_Enabled';
            }
            else {
                cssClass = 'TcpCheckBox Checked_Disabled';
            }
        }
        else if (checkedState == TcpCheckBoxCheckState.Indeterminate && !this.triState) {
            this.set_UnCheckedState();
        }
        else if (checkedState == TcpCheckBoxCheckState.Indeterminate) {
            if (this.enabled) {
                cssClass = 'TcpCheckBox Indeterminate_Enabled';
            }
            else {
                cssClass = 'TcpCheckBox Indeterminate_Disabled';
            }
        }
        else {
            if (this.enabled) {
                cssClass = 'TcpCheckBox Unchecked_Enabled';
            }
            else {
                cssClass = 'TcpCheckBox Unchecked_Disabled';
            }
        }

        label.className = cssClass;
    },

    _addHovered: function() {
        // gaurd clause
        if (!this.enabled) {
            return;
        }

        var checkedState = this.get_CheckedState();
        var label = this.get_label();
        var cssClass = '';
        if (checkedState == TcpCheckBoxCheckState.Checked) {
            cssClass = 'TcpCheckBox Checked_Hover';
        }
        else if (checkedState == TcpCheckBoxCheckState.Indeterminate) {
            cssClass = 'TcpCheckBox Indeterminate_Hover';
        }
        else {
            cssClass = 'TcpCheckBox Unchecked_Hover';
        }

        label.className = cssClass;
    },

    _raiseClientCheckedChanged: function() {
        var args = new ClientChangedEventArgs(this.get_CheckedState());
        this.raiseEvent('clientCheckedChanged', args);
    },

    _removeHovered: function() {
        this._updateUI();
    },

    // event handlers
    _clickHandler: function(event) {
        this.changeState();
        this._raiseClientCheckedChanged();
        var h = this.get_events().getHandler('click');
        if (h) h(this, Sys.EventArgs.Empty);
    },
    _hoverHandler: function(event) {
        this._addHovered();
        var h = this.get_events().getHandler('hover');
        if (h) h(this, Sys.EventArgs.Empty);
    },
    _unhoverHandler: function(event) {
        this._removeHovered();
        var h = this.get_events().getHandler('unhover');
        if (h) h(this, Sys.EventArgs.Empty);
    },

    // dispose
    dispose: function() {

        var element = this.get_element();

        if (this._clickDelegate) {
            Sys.UI.DomEvent.removeHandler(element, 'click', this._clickDelegate);
            delete this._clickDelegate;
        }

        if (this._hoverDelegate) {
            Sys.UI.DomEvent.removeHandler(element, 'focus', this._hoverDelegate);
            Sys.UI.DomEvent.removeHandler(element, 'mouseover', this._hoverDelegate);
            delete this._hoverDelegate;
        }

        if (this._unhoverDelegate) {
            Sys.UI.DomEvent.removeHandler(element, 'blur', this._unhoverDelegate);
            Sys.UI.DomEvent.removeHandler(element, 'mouseout', this._unhoverDelegate);
            delete this._unhoverDelegate;
        }

        Tcp.Controls.TcpCheckBox.callBaseMethod(this, 'dispose');
    }
}
Tcp.Controls.TcpCheckBox.registerClass('Tcp.Controls.TcpCheckBox', Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();


function ClientChangedEventArgs(checkedState) {
    this.checkedState = checkedState;
}