#region Imported Namespaces

using System;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Represents a wizard step
    /// </summary>
    public interface IWizardStep
    {
        /// <summary>
        /// Fired to navigate to the next step in the wizard
        /// </summary>
        event EventHandler<WizardStepEventArgs> Next;

        /// <summary>
        /// Fired to navigate to the previous step in the wizard
        /// </summary>
        event EventHandler<WizardStepEventArgs> Previous;

        /// <summary>
        /// Fired to complete the wizard
        /// </summary>
        event EventHandler<WizardStepEventArgs> Finish;

        /// <summary>
        /// Fired when the wizard is cancelled
        /// </summary>
        event EventHandler<WizardStepEventArgs> Cancel;
    }
}