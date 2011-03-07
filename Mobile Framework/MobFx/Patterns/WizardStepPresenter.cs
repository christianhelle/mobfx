using System;

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Base presenter for wizard steps
    /// </summary>
    /// <remarks>this class is to be used as a step in <see cref="WizardPresenter"/></remarks>
    public abstract class WizardStepPresenter : UserControlPresenter, IWizardStep
    {
        /// <summary>
        /// Creates an instance of <see cref="WizardStepPresenter"/>
        /// </summary>
        /// <param name="view">Wizard step</param>
        /// <param name="stateKey">Wizard Step state key</param>
        protected WizardStepPresenter(IUserControlView view, string stateKey)
            : base(view)
        {
            WizardStateKey = stateKey;
        }

        /// <summary>
        /// gets or sets the state key to use for the <see cref="WizardPresenter.ViewState"/>
        /// </summary>
        public string WizardStateKey { get; set; }

        #region IWizardStep Members

        /// <summary>
        /// Fired to navigate to the next step in the wizard
        /// </summary>
        public virtual event EventHandler<WizardStepEventArgs> Next;

        /// <summary>
        /// Fired to navigate to the previous step in the wizard
        /// </summary>
        public virtual event EventHandler<WizardStepEventArgs> Previous;

        /// <summary>
        /// Fired when the wizard is cancelled
        /// </summary>
        public virtual event EventHandler<WizardStepEventArgs> Cancel;

        /// <summary>
        /// Fired to complete the wizard
        /// </summary>
        public virtual event EventHandler<WizardStepEventArgs> Finish;

        #endregion

        /// <summary>
        /// Invokes the Next event
        /// </summary>
        /// <param name="e">Event arguments containing wizard step and state information</param>
        protected void InvokeNext(WizardStepEventArgs e)
        {
            var next = Next;
            if (next != null)
                next.Invoke(this, e);
        }

        /// <summary>
        /// Invokes the Previous event
        /// </summary>
        /// <param name="e">Event arguments containing wizard step and state information</param>
        protected virtual void InvokePrevious(WizardStepEventArgs e)
        {
            var previous = Previous;
            if (previous != null)
                previous.Invoke(this, e);
        }

        /// <summary>
        /// Invokes the Cancel event
        /// </summary>
        /// <param name="e">Event arguments containing wizard step and state information</param>
        protected void InvokeCancel(WizardStepEventArgs e)
        {
            var cancel = Cancel;
            if (cancel != null)
                cancel.Invoke(this, e);
        }

        /// <summary>
        /// Invokes the Finish event
        /// </summary>
        /// <param name="e">Event arguments containing wizard step and state information</param>
        protected void InvokeFinish(WizardStepEventArgs e)
        {
            var finish = Finish;
            if (finish != null)
                finish.Invoke(this, e);
        }
    }
}
