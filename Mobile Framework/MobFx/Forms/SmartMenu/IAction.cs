using System.Windows.Forms;

namespace ChristianHelle.Framework.WindowsMobile.Forms.SmartMenu
{
    /// <summary>
    /// Represents an action to be performed when an menu item in the smart menu is clicked.
    /// Implement this interface and use the full type name as the value of 
    /// <c><!--<Action>--></c> under <c><!--<MenuItem>--></c> in the 
    /// <c>SmartMenu.xml</c> configuration file
    /// </summary>
    /// <remarks>
    /// This interface is loaded into the <see cref="Control.Tag"/> property and the
    /// method <see cref="Execute"/> is called when the menu item in the smart menu is clicked.
    /// </remarks>
    /// <example>
    /// To display a <see cref="MessageBox"/> by clicking on a smart menu item 
    /// create an <see cref="IAction"/> implementation called HelloWorldAction().
    /// 
    /// <code>
    /// public class HelloWorldAction : IAction
    /// {
    ///     public void Execute()
    ///     {
    ///         MessageBox.Show("Hello World");
    ///     }
    /// }
    /// </code>
    /// 
    /// Then create a <!--<MenuItem>--> using the HelloWorldAction
    /// 
    /// <code>
    /// <!--<MenuItem>-->
    ///     <!--<Idx>0</Idx>-->
    ///     <!--<Text>Hello World!</Text>-->
    ///     <!--<Action>SomeNamespace.HelloWorldAction, SomeAssemblyName</Action>-->
    /// <!--</MenuItem>-->
    /// </code>
    /// </example>
    public interface IAction
    {
        /// <summary>
        /// Executes the action
        /// </summary>
        void Execute();
    }
}
