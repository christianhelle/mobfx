#region Imported Namespaces

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Patterns.Wizard;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Represents the presenter/controller of a wizard form
    /// </summary>
    public abstract class WizardPresenter : FormPresenter
    {
        private int position;

        /// <summary>
        /// Sets the <see cref="WizardView"/> of the <see cref="WizardPresenter"/>
        /// </summary>
        /// <param name="view">Instance of <see cref="IWizardView"/></param>
        protected WizardPresenter(IWizardView view)
            : base(view)
        {
            if (view == null)
                throw new ArgumentNullException("view");

            WizardView = view;

            Steps = new Dictionary<int, WizardStepPresenter>();
            ViewState = new WizardStateBag();

            LoadSteps();
            AttachWizardView();
        }

        /// <summary>
        /// Instance of the Wizard Form
        /// </summary>
        public IWizardView WizardView { get; set; }

        /// <summary>
        /// Gets the steps in the wizard
        /// </summary>
        public Dictionary<int, WizardStepPresenter> Steps { get; private set; }

        /// <summary>
        /// Gets the current step
        /// </summary>
        public UserControlPresenter CurrentPresenter { get; private set; }

        /// <summary>
        /// Gets the current step as a specified <paramref name="{T}"/>
        /// </summary>
        public T GetCurrentPresenter<T>() where T : WizardStepPresenter
        {
            return CurrentPresenter as T;
        }

        /// <summary>
        /// Gets the the specified step in the wizard as <paramref name="{T}"/>
        /// </summary>
        /// <param name="index">Index of position of the step in the wizard</param>
        public T GetWizardStep<T>(int index) where T : WizardStepPresenter
        {
            return (T)Steps[index];
        }

        /// <summary>
        /// Gets or sets the current position or step in the wizard
        /// </summary>
        public int Position
        {
            get { return position; }
            set
            {
                foreach (var step in Steps.Values)
                    step.BaseView.Visible = false;

                ((Control)Steps[value].BaseView).Visible = true;
                ((Control)Steps[value].BaseView).Focus();
                CurrentPresenter = Steps[value];

                if (CurrentPresenter is BarcodeEntryPresenter)
                {
                    var view = ((BarcodeEntryPresenter)CurrentPresenter).BarcodeEntryView;
                    if (view != null && view.BarcodeInputControl != null)
                        view.BarcodeInputControl.Focus();
                }

                position = value;
            }
        }

        /// <summary>
        /// Releases the resources used by the wizard
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();

            DeattachWizardView();
            DisposeWizardView();
            DisposeViewStateValues();
        }

        private void DisposeViewStateValues()
        {
            foreach (var value in ViewState.Values)
            {
                if (value is IDisposable)
                    ((IDisposable)value).Dispose();
            }
        }

        private void DisposeWizardView()
        {
            if (WizardView == null) return;
            WizardView.Dispose();
            WizardView = null;
        }

        private void DeattachWizardView()
        {
            WizardView.Next -= OnWizardViewOnNext;
            WizardView.Previous -= OnWizardViewOnPrevious;
            WizardView.Finish -= OnWizardViewOnFinish;
            WizardView.Cancel -= OnWizardViewOnCancel;
        }

        /// <summary>
        /// Subscribes this presenter to the events of its corresponding View
        /// </summary>
        protected virtual void AttachWizardView()
        {
            WizardView.Next += OnWizardViewOnNext;
            WizardView.Previous += OnWizardViewOnPrevious;
            WizardView.Finish += OnWizardViewOnFinish;
            WizardView.Cancel += OnWizardViewOnCancel;
        }

        private void OnWizardViewOnCancel(object sender, EventArgs e)
        {
            CancelWizard();
        }

        private void OnWizardViewOnFinish(object sender, EventArgs e)
        {
            CompleteWizard();
        }

        private void OnWizardViewOnPrevious(object sender, EventArgs e)
        {
            NavigateBackward();
        }

        private void OnWizardViewOnNext(object sender, EventArgs e)
        {
            NavigateForward();
        }

        /// <summary>
        /// Override this to load the nescessary <see cref="IWizardStep"/>s to the wizard
        /// </summary>
        public abstract void LoadSteps();

        /// <summary>
        /// Override this to implement the desired behavior when the user cancels the wizard
        /// </summary>
        public abstract void CancelWizard();

        /// <summary>
        /// Override this to implement the desired behavior when the user completes the wizard
        /// </summary>
        public abstract bool CompleteWizard();

        /// <summary>
        /// Navigate to the previous step
        /// </summary>
        public void NavigateBackward()
        {
            if (Position - 1 >= 0)
                Position--;
            else
                CancelWizard();
        }

        /// <summary>
        /// Navigate to the next step
        /// </summary>
        public void NavigateForward()
        {
            if (Position + 1 < Steps.Count)
                Position++;
        }

        /// <summary>
        /// Navigates to the specified step
        /// </summary>
        /// <param name="index">Wizard step index</param>
        public void NavigateTo(int index)
        {
            if (index < 0)
                throw new IndexOutOfRangeException();

            if (index < Steps.Count)
                Position = index;
        }

        /// <summary>
        /// Adds an instance of T to the <see cref="Presenter.PresenterBag"/>
        /// then adds T to as a wizard step in <see cref="Steps"/>
        /// </summary>
        /// <typeparam name="T"> <see cref="UserControlPresenter"/> that implements <see cref="IWizardStep"/></typeparam>
        /// <param name="index">Index of the step in the wizard</param>
        /// <returns>Returns an instance of <see cref="IWizardStep"/> for the step</returns>
        public virtual IWizardStep AddStep<T>(int index) where T : WizardStepPresenter, IWizardStep, new()
        {
            var presenter = Activator.CreateInstance<T>();
            return AddStep<T>(presenter, index);
        }

        /// <summary>
        /// Adds an instance of T to the <see cref="Presenter.PresenterBag"/>
        /// then adds T to as a wizard step in <see cref="Steps"/>
        /// </summary>
        /// <typeparam name="T"> <see cref="WizardStepPresenter"/> that implements <see cref="IWizardStep"/></typeparam>
        /// <param name="presenter">Instance of the <see cref="WizardStepPresenter"/> that represents the step</param>
        /// <param name="index">Index of the step in the wizard</param>
        /// <returns>Returns an instance of <see cref="IWizardStep"/> for the step</returns>
        public virtual IWizardStep AddStep<T>(T presenter, int index) where T : WizardStepPresenter, IWizardStep
        {
            AddToPresenterBag(presenter);
            HookEventHandlers((IWizardStep)presenter);
            AddStep(index, presenter);

            return presenter;
        }

        /// <summary>
        /// Adds an instance of <see cref="IWizardStep"/> to the wizard and type casts it to a
        /// <see cref="WizardStepPresenter"/> to represent its presenter
        /// </summary>
        /// <param name="wizardStep">Instance of <see cref="IWizardStep"/></param>
        /// <param name="index">Index of the step in the wizard</param>
        /// <returns>Returns an instance of <see cref="IWizardStep"/> for the step</returns>
        /// <exception cref="InvalidCastException">
        /// Thrown if the <c>wizardStep</c> parameter passed is not of type <see cref="WizardStepPresenter"/>
        /// </exception>
        public virtual IWizardStep AddStep(IWizardStep wizardStep, int index)
        {
            var userControlPresenter = wizardStep as WizardStepPresenter;
            if (userControlPresenter == null)
                throw new InvalidCastException("wizardStep parameter must be of type UserControlPresenter");

            AddToPresenterBag(userControlPresenter);
            AddStep(index, userControlPresenter);
            HookEventHandlers(wizardStep);

            return wizardStep;
        }

        private void AddToPresenterBag(WizardStepPresenter userControlPresenter)
        {
            var type = userControlPresenter.GetType();
            if (!PresenterBag.ContainsKey(type))
                PresenterBag.Add(type, userControlPresenter);
            else
            {
                PresenterBag[type].Dispose();
                PresenterBag[type] = userControlPresenter;
            }
        }

        private void AddStep(int index, WizardStepPresenter userControlPresenter)
        {
            var userControl = (UserControl)userControlPresenter.BaseView;
            userControl.Dock = DockStyle.Fill;

            WizardView.Container.Controls.Add(userControl);
            Steps.Add(index, userControlPresenter);
        }

        private void HookEventHandlers(IWizardStep wizardStep)
        {
            wizardStep.Previous += OnWizardStepOnPrevious;
            wizardStep.Next += OnWizardStepOnNext;
            wizardStep.Finish += OnWizardStepOnFinish;
            wizardStep.Cancel += (sender, e) => CancelWizard();
        }

        private void OnWizardStepOnFinish(object sender, WizardStepEventArgs e)
        {
            if (e != null)
                AddOrUpdateState(e.Name, e.State);

            CompleteWizard();
        }

        private void OnWizardStepOnNext(object sender, WizardStepEventArgs e)
        {
            if (e != null)
            {
                AddOrUpdateState(e.Name, e.State);

                if (e.Position.HasValue)
                {
                    NavigateTo(e.Position.Value);
                    return;
                }
            }

            NavigateForward();
        }

        private void OnWizardStepOnPrevious(object sender, WizardStepEventArgs e)
        {
            if (e != null)
            {
                if (ViewState.ContainsKey(e.Name))
                    ViewState[e.Name] = e.State;

                if (e.Position.HasValue)
                {
                    NavigateTo(e.Position.Value);
                    return;
                }
            }

            NavigateBackward();
        }

        /// <summary>
        /// If the key doesn't exist, the state is added to the <see cref="ViewState"/>,
        /// otherwise the value of the state is updated
        /// </summary>
        /// <param name="name">Name of key of the state in the <see cref="ViewState"/></param>
        /// <param name="state">Value or state</param>
        private void AddOrUpdateState(string name, object state)
        {
            if (!ViewState.ContainsKey(name))
                ViewState.Add(name, state);
            else
                ViewState[name] = state;
        }

        /// <summary>
        /// A state bag of objects shared through out the wizard
        /// </summary>
        public WizardStateBag ViewState { get; private set; }

        /// <summary>
        /// A key/value pair collection to containing objects shared through out the wizard
        /// </summary>
        public class WizardStateBag : Dictionary<string, object>
        {
            /// <summary>
            /// Retrieves T from the state bag
            /// </summary>
            /// <typeparam name="T">Type of the object to retrieve</typeparam>
            /// <param name="key">Name of the object</param>
            /// <returns>Returns an instance of T</returns>
            public T GetItem<T>(string key)
            {
                return (T)base[key];
            }

            /// <summary>
            /// Gets or sets the value associated with the specified key.
            /// </summary>
            /// <param name="key">The key of the value to get or set</param>
            /// <returns>
            /// The value associated with the specified key. If the specified key is not
            /// found, a get operation throws a <see cref="KeyNotFoundException"/>,
            /// and a set operation creates a new element with the specified key
            /// </returns>
            /// <exception cref="ArgumentNullException">the key is null</exception>
            /// <exception cref="KeyNotFoundException">
            /// The property is retrieved and key does not exist in the collection
            /// </exception>
            public new object this[string key]
            {
                get { return base[key]; }
                set
                {
                    if (string.IsNullOrEmpty(key))
                        throw new ArgumentNullException("key", "The key is null");

                    if (!ContainsKey(key))
                        Add(key, value);
                    else
                        base[key] = value;
                }
            }
        }
    }
}