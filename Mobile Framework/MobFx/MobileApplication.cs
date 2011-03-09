#region Imported Namespaces

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using ChristianHelle.Framework.WindowsMobile.Core;
using ChristianHelle.Framework.WindowsMobile.Diagnostics;
using ChristianHelle.Framework.WindowsMobile.Forms;
using ChristianHelle.Framework.WindowsMobile.Forms.SmartMenu;
using ChristianHelle.Framework.WindowsMobile.Patterns;
using Microsoft.WindowsCE.Forms;
using ChristianHelle.Framework.WindowsMobile.Barcode;

#endregion

namespace ChristianHelle.Framework.WindowsMobile
{
    /// <summary>
    /// Provides static methods and properties to manage an application, 
    /// such as methods to start and stop an application, 
    /// to process Windows messages, and properties to get information 
    /// about an application.
    /// </summary>
    /// <remarks>
    /// <see cref="MobileApplication"/> is the main entry point of the 
    /// mobile framework. Use this class instead of <see cref="Application"/>
    /// for loading the windows mobile applications main form.
    /// 
    /// Using this automatically logs all unhandled exceptions to a file 
    /// through the <see cref="ErrorLogFile"/> class
    /// 
    /// This class cannot be inherited.
    /// </remarks>
    public sealed class MobileApplication
    {
        static MobileApplication()
        {
            DispoableResources = new Dictionary<string, IDisposable>();
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledExceptionEvent;
            MobileDevice.Hibernate += MobileDevice_Hibernate;
        }

        /// <summary>
        /// Begins running a standard application message loop on the current 
        /// thread, and makes the specified form visible.
        /// </summary>
        public static void Run()
        {
            SplashScreen.Show();
            using (var presenter = new SmartMenuPresenter())
            {
                var hwnd = SystemWindow.FindWindow(presenter.View.Name, presenter.View.Text);
                if (hwnd != IntPtr.Zero)
                    return;

                SplashScreen.Hide();
                Run((MobileForm)presenter.View);
            }
        }

        /// <summary>
        /// Begins running a standard application message loop on the current 
        /// thread, and makes the specified form visible.
        /// </summary>
        /// <param name="form">
        /// A <see cref="MobileForm"/> that represents the main form of the 
        /// application to make visible.
        /// </param>
        public static void Run(MobileForm form)
        {
            Run(form, false);
        }

        /// <summary>
        /// Begins running a standard application message loop on the current 
        /// thread, and makes the specified form visible.
        /// </summary>
        /// <param name="form">
        /// A <see cref="MobileForm"/> that represents the main form of the 
        /// application to make visible.
        /// </param>
        public static void Run(MobileForm form, bool enableKioskMode)
        {
            var hwnd = SystemWindow.FindWindow(form.Name, form.Text);
            if (hwnd != IntPtr.Zero)
                return;

            form.Load += delegate
            {
                if (enableKioskMode)
                    ForceKioskMode(form);
                SplashScreen.Hide();
            };

            Application.Run(form);
        }

        /// <summary>
        /// Begins running a standard application message loop on the current
        /// thread, and makes the view of the specified presenter visible.
        /// </summary>
        /// <typeparam name="T">Form presenter</typeparam>
        public static void Run<T>() where T : FormPresenter
        {
            SplashScreen.Show();

            var instance = Activator.CreateInstance<T>();
            Run((MobileForm)instance.FormView, false);
        }

        /// <summary>
        /// Loads a login dialog that forces the user 
        /// to log into the application before using it. If the login is 
        /// successful (Meaning the DialogResult is set to 
        /// OK) then the Main Form is loaded.
        /// </summary>
        /// <param name="loginForm">Login form</param>
        public static void RunWithLogin(MobileForm loginForm)
        {
            RunWithLogin(loginForm, false);
        }

        /// <summary>
        /// Loads a login dialog that forces the user 
        /// to log into the application before using it. If the login is 
        /// successful (Meaning the DialogResult is set to 
        /// OK) then the Main Form is loaded.
        /// </summary>
        /// <param name="loginForm">Login form</param>
        public static void RunWithLogin(MobileForm loginForm, bool enableKioskMode)
        {
            SplashScreen.Show();

            loginForm.Load += (sender, e) => ForceKioskMode(loginForm);
            using (var presenter = new SmartMenuPresenter())
            {
                var hwnd = SystemWindow.FindWindow(presenter.View.Name, presenter.View.Text);
                if (hwnd != IntPtr.Zero)
                    return;

                var smartMenuView = (MobileForm)presenter.View;
                if (enableKioskMode)
                    ForceKioskMode(smartMenuView);

                SplashScreen.Hide();

                var dialogResult = loginForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                    Application.Run(smartMenuView);
            }
        }

        /// <summary>
        /// Loads a login dialog that forces the user 
        /// to log into the application before using it. If the login is 
        /// successful (Meaning the DialogResult is set to 
        /// OK) then the Main Form is loaded.
        /// </summary>
        /// <param name="loginForm">Login form</param>
        /// <param name="mainForm">Main form</param>
        public static void RunWithLogin(MobileForm loginForm, MobileForm mainForm)
        {
            RunWithLogin(loginForm, mainForm, false);
        }

        /// <summary>
        /// Loads a login dialog that forces the user 
        /// to log into the application before using it. If the login is 
        /// successful (Meaning the DialogResult is set to 
        /// OK) then the Main Form is loaded.
        /// </summary>
        /// <param name="loginForm">Login form</param>
        /// <param name="mainForm">Main form</param>
        public static void RunWithLogin(MobileForm loginForm, MobileForm mainForm, bool enableKioskMode)
        {
            var loginHwnd = SystemWindow.FindWindow(loginForm.Name, loginForm.Text);
            var mainHwnd = SystemWindow.FindWindow(mainForm.Name, mainForm.Text);
            if (loginHwnd != IntPtr.Zero || mainHwnd != IntPtr.Zero)
                return;

            SplashScreen.Show();

            if (enableKioskMode)
                loginForm.Load += (sender, e) => ForceKioskMode(loginForm);
            var dialogResult = loginForm.ShowDialog();
            if (dialogResult != DialogResult.OK)
                return;

            SplashScreen.Hide();
            Run(mainForm);
        }

        /// <summary>
        /// Loads a login dialog that forces the user to log into the application before using it.
        /// If the login is successful (meaning the DialogResult is set to OK) then the smart menu
        /// form is loaded.
        /// </summary>
        /// <typeparam name="TLogin">
        /// Type of the <see cref="FormPresenter"/> that represents the login dialog
        /// </typeparam>
        public static void RunWithLogin<TLogin>() where TLogin : FormPresenter
        {
            RunWithLogin<TLogin>(false);
        }

        /// <summary>
        /// Loads a login dialog that forces the user to log into the application before using it.
        /// If the login is successful (meaning the DialogResult is set to OK) then the smart menu
        /// form is loaded.
        /// </summary>
        /// <typeparam name="TLogin">
        /// Type of the <see cref="FormPresenter"/> that represents the login dialog
        /// </typeparam>
        public static void RunWithLogin<TLogin>(bool enableKioskMode) where TLogin : FormPresenter
        {
            SplashScreen.Show();

            var instance = Activator.CreateInstance<TLogin>();
            var loginForm = (Form)instance.FormView;
            if (enableKioskMode)
                loginForm.Load += (sender, e) => ForceKioskMode(loginForm);

            using (var presenter = new SmartMenuPresenter())
            {
                var smartMenuView = (MobileForm)presenter.View;
                ForceKioskMode(smartMenuView);

                var loginHwnd = SystemWindow.FindWindow(loginForm.Name, loginForm.Text);
                var mainHwnd = SystemWindow.FindWindow(smartMenuView.Name, smartMenuView.Text);
                if (loginHwnd != IntPtr.Zero || mainHwnd != IntPtr.Zero)
                    return;

                SplashScreen.Hide();

                var dialogResult = loginForm.ShowDialog();
                if (dialogResult == DialogResult.OK)
                    Application.Run(smartMenuView);
            }
        }

        /// <summary>
        /// Loads a login dialog that forces the user to log into the application before using it.
        /// If the login is successful (meaning the DialogResult is set to OK) then the specified main
        /// form is loaded.
        /// </summary>
        /// <typeparam name="TLogin">
        /// Type of the <see cref="FormPresenter"/> that represents the login dialog
        /// </typeparam>
        /// <typeparam name="TMain">
        /// Type of the <see cref="FormPresenter"/> that represents the main dialog
        /// </typeparam>
        /// <remarks>This methods will start the application without kiosk mode</remarks>
        public static void RunWithLogin<TLogin, TMain>()
            where TLogin : FormPresenter
            where TMain : FormPresenter
        {
            RunWithLogin<TLogin, TMain>(false);
        }

        /// <summary>
        /// Loads a login dialog that forces the user to log into the application before using it.
        /// If the login is successful (meaning the DialogResult is set to OK) then the specified main
        /// form is loaded.
        /// </summary>
        /// <typeparam name="TLogin">
        /// Type of the <see cref="FormPresenter"/> that represents the login dialog
        /// </typeparam>
        /// <typeparam name="TMain">
        /// Type of the <see cref="FormPresenter"/> that represents the main dialog
        /// </typeparam>
        /// <param name="enableKioskMode">
        /// Set to <c>true</c> to run the application in kiosk mode (full screen)
        /// </param>
        public static void RunWithLogin<TLogin, TMain>(bool enableKioskMode)
            where TLogin : FormPresenter
            where TMain : FormPresenter
        {
            SplashScreen.Show();

            var instance = Activator.CreateInstance<TLogin>();
            var loginForm = (Form)instance.FormView;
            if (enableKioskMode)
                loginForm.Load += (sender, e) => ForceKioskMode(loginForm);

            var mainForm = Activator.CreateInstance<TMain>();
            SplashScreen.Hide();

            var loginHwnd = SystemWindow.FindWindow(loginForm.Name, loginForm.Text);
            var mainHwnd = SystemWindow.FindWindow(mainForm.FormView.Name, mainForm.FormView.Text);
            if (loginHwnd != IntPtr.Zero || mainHwnd != IntPtr.Zero)
                return;

            var dialogResult = loginForm.ShowDialog();
            if (dialogResult == DialogResult.OK)
                Application.Run((Form)mainForm.FormView);
        }

        /// <summary>
        /// Shuts down the mobile application and terminates all services that runs within it
        /// </summary>
        public static void Exit()
        {
            DisposeCachedResources();
            Application.Exit();
        }

        /// <summary>
        /// Fired when an unhandled exception occurs
        /// </summary>
        public static event EventHandler<UnhandledFrameworkExceptionEventArgs> UnhandledExceptionMode;

        private static void ForceKioskMode(Control control)
        {
            SystemWindow.SetTaskBarVisibility(control.Handle, false);
            SystemWindow.SetForegroundWindow(control.Handle);
        }

        private static void InvokeUnhandledExceptionMode(object sender, UnhandledFrameworkExceptionEventArgs e)
        {
            var unhandledExceptionModeHandler = UnhandledExceptionMode;
            if (unhandledExceptionModeHandler != null)
                unhandledExceptionModeHandler(sender, e);
        }

        private static void OnUnhandledExceptionEvent(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                ErrorLogFile.Instance.WriteLine(
                    ErrorMessageBuilder.BuildExceptionMessage(
                        exception,
                        "Unhandled Framework Exception"));
            }

            InvokeUnhandledExceptionMode(sender, new UnhandledFrameworkExceptionEventArgs(exception));
        }

        private static void MobileDevice_Hibernate(object sender, EventArgs e)
        {
            ErrorLogFile.Instance.WriteLine("The device is running low on memory");
            DisposeCachedResources();
        }

        private static void DisposeCachedResources()
        {
            if (DispoableResources.Count == 0)
                return;

            try
            {
                foreach (var key in DispoableResources.Keys)
                {
                    try
                    {
                        ErrorLogFile.Instance.WriteLine("Releasing resources used by: " + key);
                        if (DispoableResources[key] != null)
                        {
                            DispoableResources[key].Dispose();
                            DispoableResources[key] = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorLogFile.Instance.WriteLine("Unable to release resources used by: " + key);
                        ErrorLogFile.Instance.AppendException(ex);
                    }
                }

                DispoableResources.Clear();
            }
            catch (Exception ex)
            {
                ErrorLogFile.Instance.WriteLine("Unable to release resources");
                ErrorLogFile.Instance.AppendException(ex);
            }
        }

        /// <summary>
        /// Add static memory constraint objects to this collection for the framework to handle
        /// freeing the resources used by the object by calling Dispose() and setting the instance
        /// of it to NULL.
        /// </summary>
        /// <remarks>
        /// The contents of this collection are disposed and set to NULL when the application
        /// receives the WM_HIBERNATE message (when the device runs critically low on memory)
        /// </remarks>
        public static Dictionary<string, IDisposable> DispoableResources { get; private set; }
    }
}