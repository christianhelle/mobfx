#region Imported Namespaces

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using ChristianHelle.Framework.WindowsMobile.Drawing;
using ChristianHelle.Framework.WindowsMobile.IO;
using ChristianHelle.Framework.WindowsMobile.Patterns;

#endregion

namespace ChristianHelle.Framework.WindowsMobile.Forms.SmartMenu
{
    /// <summary>
    /// Represents the Presenter/Controller of the Smart menu form Passive View
    /// </summary>
    public class SmartMenuPresenter : FormPresenter
    {
        private Control[] actionButtons;
        private Control navButton;
        private Dictionary<int, Rectangle> bounds;
        private int currentPosition;
        private SmartMenuConfiguration configuration;
        private Dictionary<int, SmartMenuItem> list;
        private int menuItemCount;

        /// <summary>
        /// Creates an instance of <see cref="SmartMenuPresenter"/>
        /// </summary>
        public SmartMenuPresenter()
            : this(new SmartMenuFormView())
        {
        }

        /// <summary>
        /// Creates an instance of <see cref="SmartMenuPresenter"/>
        /// </summary>
        /// <param name="view">
        /// <see cref="ISmartMenuView"/> implementation to use as the actual view
        /// </param>
        public SmartMenuPresenter(ISmartMenuView view)
            : base(view)
        {
            View = view;
            AttachView();
            Load();
        }

        /// <summary>
        /// Instance of the actual View
        /// </summary>
        public ISmartMenuView View { get; set; }

        /// <summary>
        /// Subscribes this presenter to the events of its corresponding View
        /// </summary>
        protected override void AttachView()
        {
            View.ViewClose += (sender, e) => MobileApplication.Exit();
        }

        /// <summary>
        /// Loads the smart menu configuration
        /// </summary>
        public void Load()
        {
            using (new WaitCursor())
            {
                LoadConfiguration();
                DefineCoordinates();
                LoadMenuItems();
            }
        }

        private void DefineCoordinates()
        {
            if (string.IsNullOrEmpty(configuration.Layout))
            {
                SetCoordinateTableBounds(8);
                LoadDefaultLayout();
            }
            else
            {
                switch (configuration.Layout.ToLower())
                {
                    case "rect4":
                        SetCoordinateTableBounds(3);
                        LoadRect4Layout();
                        if (configuration.ButtonStyle.ToLower() != "win32")
                            LoadControls<ButtonEx>();
                        break;
                    case "square6":
                        SetCoordinateTableBounds(5);
                        LoadSquare6Layout();
                        if (configuration.ButtonStyle.ToLower() != "win32")
                            LoadControls<ImageButton>();
                        break;
                    default:
                        SetCoordinateTableBounds(8);
                        LoadDefaultLayout();
                        LoadControls<Button>();
                        return;
                }

                if (configuration.ButtonStyle.ToLower() == "win32")
                    LoadControls<Button>();
            }
        }

        private void SetCoordinateTableBounds(int size)
        {
            menuItemCount = size;
            bounds = new Dictionary<int, Rectangle>(size);
        }

        private void LoadSquare6Layout()
        {
            bounds.Add(0, Dpi.ScaleRectangle(new Rectangle(1, 2, 119, 105)));
            bounds.Add(1, Dpi.ScaleRectangle(new Rectangle(121, 2, 119, 105)));
            bounds.Add(2, Dpi.ScaleRectangle(new Rectangle(1, 108, 119, 105)));
            bounds.Add(3, Dpi.ScaleRectangle(new Rectangle(121, 108, 119, 105)));
            bounds.Add(4, Dpi.ScaleRectangle(new Rectangle(1, 214, 119, 105)));
            bounds.Add(5, Dpi.ScaleRectangle(new Rectangle(121, 214, 119, 105)));
        }

        private void LoadRect4Layout()
        {
            var height = Screen.PrimaryScreen.Bounds.Height / 4;
            var width = Screen.PrimaryScreen.Bounds.Width;
            var y = 0;

            for (var row = 0; row < menuItemCount + 1; row++)
            {
                bounds.Add(row, new Rectangle(0, y, width, height));
                y += height;
            }
        }

        private void LoadDefaultLayout()
        {
            int idx = 0, y = 43;
            for (var row = 0; row < 3; row++)
            {
                var x = 9;
                for (var col = 0; col < 3; col++)
                {
                    bounds.Add(idx++, Dpi.ScaleRectangle(new Rectangle(x, y, 70, 70)));
                    x += 76;
                }
                y += 76;
            }
        }

        private void LoadControls<T>() where T : Control, new()
        {
            actionButtons = new T[menuItemCount];
            for (var idx = 0; idx < menuItemCount; idx++)
            {
                if (actionButtons[idx] != null)
                    actionButtons[idx].Dispose();

                actionButtons[idx] = Activator.CreateInstance<T>();
                actionButtons[idx].Bounds = bounds[idx];
                actionButtons[idx].MouseUp += (sender, e) => HandleActionButton((Control)sender);
                View.Controls.Add(actionButtons[idx]);
            }

            if (navButton != null)
                navButton.Dispose();

            navButton = Activator.CreateInstance<T>();
            navButton.Bounds = bounds[menuItemCount];
            navButton.MouseUp += (sender, e) => HandleForwardButton();
            navButton.Text = "More..."; //">>";
            View.Controls.Add(navButton);

            if (navButton is ImageButton && !string.IsNullOrEmpty(configuration.ForwardButtonImage))
                ((ImageButton)navButton).Image = new Bitmap(configuration.ForwardButtonImage);

            foreach (Control control in View.Controls)
            {
                control.BackColor = SystemColors.ControlDark;
                control.ForeColor = SystemColors.ControlLight;
                control.Font = new Font("Arial", 12f, FontStyle.Regular);
            }
        }

        private void HandleForwardButton()
        {
            if (currentPosition >= list.Count)
                return;

            currentPosition += menuItemCount;
            if (currentPosition >= list.Count)
                currentPosition = 0;

            LoadMenuItems();
        }

        /// <summary>
        /// Retrieves an instance of <see cref="IAction"/> from the <see cref="Control.Tag"/>
        /// property then calls the <see cref="IAction.Execute"/> method
        /// </summary>
        /// <param name="button">Button pressed</param>
        public void HandleActionButton(Control button)
        {
            try
            {
                var action = (IAction)button.Tag;
                if (action != null)
                    action.Execute();

                currentPosition = 0;
                LoadMenuItems();
            }
            catch (InvalidCastException ex)
            {
                throw new MobileApplicationException(
                    "The Tag property of the button must be of type IAction", ex);
            }
        }

        private void LoadMenuItems()
        {
            for (var i = 0; i < menuItemCount; i++)
            {
                actionButtons[i].Visible = false;
                actionButtons[i].Text = string.Empty;

                if (actionButtons[i] is ImageButton)
                    if (((ImageButton)actionButtons[i]).Image != null)
                        ((ImageButton)actionButtons[i]).Image.Dispose();
            }

            if (list.Count < menuItemCount)
                for (var i = 0; i < list.Count; i++)
                {
                    actionButtons[i].Visible = true;
                    actionButtons[i].Text = list[i].Text;

                    SetImage(i, i);

                    if (!string.IsNullOrEmpty(list[i].Action))
                        actionButtons[i].Tag = TypeInstantiator<IAction>.LoadType(list[i].Action);
                }
            else
                for (var i = currentPosition; i < currentPosition + menuItemCount && i < list.Count; i++)
                {
                    actionButtons[i - currentPosition].Visible = true;
                    actionButtons[i - currentPosition].Text = list[i].Text;

                    SetImage(i - currentPosition, i);

                    if (!string.IsNullOrEmpty(list[i].Action))
                        actionButtons[i - currentPosition].Tag = TypeInstantiator<IAction>.LoadType(list[i].Action);
                }

            if (list.Count > menuItemCount)
                navButton.Visible = true;
        }

        private void SetImage(int buttonIndex, int menuIndex)
        {
            if (actionButtons[buttonIndex] is ImageButton && File.Exists(list[menuIndex].Image))
                ((ImageButton)actionButtons[buttonIndex]).Image = new Bitmap(list[menuIndex].Image);
        }

        private void LoadConfiguration()
        {
            var xmlFile = string.Format("{0}\\SmartMenu.xml", DirectoryEx.GetCurrentDirectory());
            if (LoadActionsFromSerializedXml(xmlFile))
                return;

            ParseXml(xmlFile);
        }

        private void ParseXml(string xmlFile)
        {
            list = new Dictionary<int, SmartMenuItem>();
            var item = new SmartMenuItem();
            var node = string.Empty;

            var reader = new XmlTextReader(xmlFile);
            reader.Normalization = true;

            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    node = reader.Name;
                    if (node == "menuitem")
                        item = new SmartMenuItem();
                }
                else if (reader.NodeType == XmlNodeType.Text)
                {
                    switch (node)
                    {
                        case "idx":
                            item.Idx = int.Parse(reader.Value);
                            break;
                        case "text":
                            item.Text = reader.Value;
                            break;
                        case "image":
                            item.Image = reader.Value;
                            break;
                        case "action":
                            item.Action = reader.Value;
                            list.Add(item.Idx, item);
                            break;
                    }
                }
            }
            reader.Close();
        }

        /// <summary>
        /// An alternate method of loading the actions. By using the XmlSerializer,
        /// we can easily modify the SmartMenuItem structure without worrying about the parser.
        /// </summary>
        public bool LoadActionsFromSerializedXml(string xml)
        {
            if (!File.Exists(xml))
                return false;

            using (var reader = File.OpenText(xml))
            {
                var serializer = new XmlSerializer(typeof(SmartMenuConfiguration));
                configuration = (SmartMenuConfiguration)serializer.Deserialize(reader);
                reader.Close();
            }

            if (configuration.SmartMenuItem == null)
                return false;

            list = new Dictionary<int, SmartMenuItem>();
            for (var i = 0; i < configuration.SmartMenuItem.Length; i++)
            {
                if (!string.IsNullOrEmpty(configuration.SmartMenuItem[i].Image))
                {
                    configuration.SmartMenuItem[i].Image = Path.Combine(DirectoryEx.GetCurrentDirectory(),
                                                                        configuration.SmartMenuItem[i].Image);
                    if (!File.Exists(configuration.SmartMenuItem[i].Image))
                        configuration.SmartMenuItem[i].Image = null;
                }
                try
                {
                    list.Add(configuration.SmartMenuItem[i].Idx, configuration.SmartMenuItem[i]);
                }
                catch (ArgumentException)
                {
                    // An element with the same key already exists
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(configuration.ForwardButtonImage))
            {
                var imageFile = Path.Combine(DirectoryEx.GetCurrentDirectory(), configuration.ForwardButtonImage);
                if (File.Exists(imageFile))
                    configuration.ForwardButtonImage = imageFile;
            }

            return true;
        }
    }
}