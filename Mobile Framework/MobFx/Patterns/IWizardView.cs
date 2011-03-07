#region Imported Namespaces

using System;
using System.ComponentModel;
using System.Windows.Forms;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Represents a wizard form
    /// </summary>
    public interface IWizardView : IFormView
    {
        /// <summary>
        /// Gets the container where the wizard steps are to be docked to
        /// </summary>
        Control Container { get; }

        /// <summary>
        /// Fired to navigate to the next step in the wizard
        /// </summary>
        event EventHandler Next;

        /// <summary>
        /// Fired to navigate to the previous step in the wizard
        /// </summary>
        event EventHandler Previous;

        /// <summary>
        /// Fired to complete the wizard
        /// </summary>
        event EventHandler Finish;

        /// <summary>
        /// Fired when the wizard is cancelled
        /// </summary>
        event EventHandler Cancel;
    }
}