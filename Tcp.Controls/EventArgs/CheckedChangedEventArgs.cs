using System;

namespace Tcp.Controls
{
    public class CheckedChangedEventArgs : EventArgs
    {
        # region Constructors

        public CheckedChangedEventArgs(TcpCheckedState state)
        {
            CheckedState = state;
        }

        # endregion

        # region Properties

        public TcpCheckedState CheckedState { get; private set; }

        # endregion
    }
}
