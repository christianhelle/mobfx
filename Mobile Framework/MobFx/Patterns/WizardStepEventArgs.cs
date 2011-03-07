#region Imported Namespaces

using System;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Contains state information
    /// </summary>
    public class WizardStepEventArgs : EventArgs
    {
        /// <summary>
        /// Creates an instance of <see cref="WizardStepEventArgs"/>
        /// </summary>
        /// <param name="stateKey">A tag of key identifying the state</param>
        /// <param name="state">State object</param>
        public WizardStepEventArgs(string stateKey, object state)
        {
            Name = stateKey;
            State = state;
        }

        /// <summary>
        /// Creates an instance of <see cref="WizardStepEventArgs"/>
        /// </summary>
        /// <param name="stateKey">A tag of key identifying the state</param>
        /// <param name="state">State object</param>
        /// <param name="wizardIndex">Position index to display</param>
        public WizardStepEventArgs(string stateKey, object state, int? wizardIndex)
            : this(stateKey, state)
        {
            Position = wizardIndex;
        }

        /// <summary>
        /// A tag of key identifying the state
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// State object
        /// </summary>
        public object State { get; private set; }

        /// <summary>
        /// Position index to display
        /// </summary>
        public int? Position { get; private set; }
    }
}