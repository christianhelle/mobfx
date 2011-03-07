#region Imported Namespaces

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Patterns
{
    /// <summary>
    /// Base interface to all IView implementations
    /// </summary>
    /// <remarks>
    /// All IView implementations must inherit from this interface
    /// </remarks>
    public interface IView : IDisposable
    {
        /// <summary>
        /// Gets or sets the size of the form
        /// </summary>
        Size Size { get; set; }

        /// <summary>
        /// Gets or sets the size and location of the control including 
        /// its nonclient elements, in pixels, relative to the parent control.
        /// </summary>
        Rectangle Bounds { get; set; }

        /// <summary>
        /// Gets a value indicating whether the caller must call an 
        /// invoke method when making method calls to the control because 
        /// the caller is on a different thread than the one the control 
        /// was created on.
        /// </summary>
        /// <remarks>
        /// Returns TRUE if the control's System.Windows.Forms.Control.Handle 
        /// was created on a different thread than the calling thread 
        /// (indicating that you must make calls to the control through an 
        /// invoke method); otherwise, FALSE.
        /// </remarks>
        bool InvokeRequired { get; }

        /// <summary>
        /// Gets the window handle that the control is bound to.
        /// </summary>
        /// <remarks>
        /// An <see cref="IntPtr"/> that contains the window handle (HWND) of the control.
        /// </remarks>
        IntPtr Handle { get; }

        /// <summary>
        /// Gets or sets the caption of the control
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets the collection of controls contained within the control
        /// </summary>
        Control.ControlCollection Controls { get; }


        /// <summary>
        /// Executes the specified delegate on the thread that owns the control's 
        /// underlying window handle.
        /// </summary>
        /// <param name="method">
        /// A delegate that contains a method to be called in the control's 
        /// thread context.
        /// </param>
        /// <returns>
        /// The return value from the delegate being invoked, 
        /// or null if the delegate has no return value.
        /// </returns>
        object Invoke(Delegate method);

        /// <summary>
        /// Executes the specified delegate, on the thread that owns the control's 
        /// underlying window handle, with the specified list of arguments.
        /// </summary>
        /// <param name="method">
        /// A delegate to a method that takes parameters of the same number 
        /// and type that are contained in the args parameter.
        /// </param>
        /// <param name="args">
        /// An array of objects to pass as arguments to the specified method. 
        /// This parameter can be null if the method takes no arguments.
        /// </param>
        /// <returns>
        /// An <see cref="System.Object"/> that contains the return value from the delegate 
        /// being invoked, or null if the delegate has no return value.
        /// </returns>
        object Invoke(Delegate method, params object[] args);

        /// <summary>
        /// Fired when the view loads
        /// </summary>
        event EventHandler ViewLoad;

        /// <summary>
        /// Forces the control to invalidate its client area and immediately redraw 
        /// itself and any child controls.
        /// </summary>
        void Refresh();

        /// <summary>
        /// Gets or sets a value indicating whether the control and all its parent controls are displayed.
        /// </summary>
        /// <remarks>
        /// Returns <c>true</c> if the control and all its parent controls are displayed; 
        /// Otherwise, <c>false</c>. The default is <c>true</c>
        /// </remarks>
        bool Visible { get; set; }

        /// <summary>
        /// Invalidates the entire surface of the control and causes the control to be redrawn.
        /// </summary>
        void Invalidate();

        /// <summary>
        /// Causes the control to redraw the invalidated regions within its client area.
        /// </summary>
        void Update();

        /// <summary>
        /// Brings the control to the front of the z-order.
        /// </summary>
        void BringToFront();

        /// <summary>
        /// Sends the control to the back of the z-order.
        /// </summary>
        void SendToBack();

        /// <summary>
        /// Sets input focus to the control
        /// </summary>
        /// <returns><c>True</c> if the input focus request was successful; otherwise, <c>False</c></returns>
        bool Focus();

        /// <summary>
        /// Gets or sets the class name of the control
        /// </summary>
        string Name { get; set; }
    }
}