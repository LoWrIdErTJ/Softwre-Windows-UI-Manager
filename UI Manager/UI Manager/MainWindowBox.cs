using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UBotPlugin;
using System.Windows;
using System.Windows.Media;
using System.Threading;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing.Text;
using System.Diagnostics;
using System.Security.Principal;



namespace SoftwareWindows
{
    class MainWindowRunAsAdmin : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowRunAsAdmin()
        {
            //_parameters.Add(new UBotParameterDefinition("Title", UBotType.String));
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "bot runas admin"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            if (IsAdministrator() == false)
            {
                // Restart program and run as admin
                var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                ProcessStartInfo startInfo = new ProcessStartInfo(exeName);
                startInfo.Verb = "runas";
                System.Diagnostics.Process.Start(startInfo);
                Application.Current.Shutdown();
                return;
            }

        }

        private static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainWindowTitle : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowTitle()
        {
            _parameters.Add(new UBotParameterDefinition("Title", UBotType.String));
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "bot title"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    ActiveWindow.Title = parameters["Title"];
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainWindowsize : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowsize()
        {
            _parameters.Add(new UBotParameterDefinition("Width", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("Hight", UBotType.String));
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "bot size"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    ActiveWindow.Width = Convert.ToInt32(parameters["Width"]);
                    ActiveWindow.Height = Convert.ToInt32(parameters["Hight"]);
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class BrowserSize : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public BrowserSize()
        {
            _parameters.Add(new UBotParameterDefinition("width", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("height", UBotType.String));
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "browser size"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    foreach (UBotCoreUI.BrowserContainer browser in FindVisualChildren<UBotCoreUI.BrowserContainer>(ActiveWindow))
                    {
                        browser.Width = Convert.ToInt32(parameters["width"]);
                        browser.Height = Convert.ToInt32(parameters["height"]);
                    }
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
   
    // testing UI refresh
    /*class RefreshUIB : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public RefreshUIB()
        {
           // _parameters.Add(new UBotParameterDefinition("width", UBotType.String));
           // _parameters.Add(new UBotParameterDefinition("height", UBotType.String));
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "refresh ui"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                // if not null
                if (ActiveWindow != null)
                {
                    foreach (UBotCoreUI.UIBrowser browser in FindVisualChildren<UBotCoreUI.UIBrowser>(ActiveWindow))
                    {
                        browser.Refresh();

                    }
                }
                
                // if null
                if (ActiveWindow == null)
                {
                    foreach (UBotCoreUI.UIBrowser browser in FindVisualChildren<UBotCoreUI.UIBrowser>(ActiveWindow))
                    {
                        browser.Refresh();

                    }
                }

            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }*/

    class MainWindowShowInTaskBar : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowShowInTaskBar()
        {
            var xParameter = new UBotParameterDefinition("Show In TaskBar", UBotType.String);
            xParameter.Options = new[] { "Yes", "No" };
            xParameter.DefaultValue = "Yes";
            _parameters.Add(xParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "show in taskbar"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    if (parameters["Show In TaskBar"] == "Yes")
                    {
                        ActiveWindow.ShowInTaskbar = true;
                    }
                    else
                    {
                        ActiveWindow.ShowInTaskbar = false;
                    }
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainWindowTopMost : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowTopMost()
        {
            var xParameter = new UBotParameterDefinition("topmost", UBotType.String);
            xParameter.Options = new[] { "Yes", "No" };
            xParameter.DefaultValue = "Yes";
            _parameters.Add(xParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "window topmost"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    if (parameters["topmost"] == "Yes")
                    {
                        ActiveWindow.Topmost = true;
                    }
                    else
                    {
                        ActiveWindow.Topmost = false;
                    }
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainWindowVisibility : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowVisibility()
        {
            var xParameter = new UBotParameterDefinition("Visibility", UBotType.String);
            xParameter.Options = new[] { "Visible", "Hidden", "Collapsed" };
            xParameter.DefaultValue = "Visible";
            _parameters.Add(xParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "window visibility"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    if (parameters["Visibility"] == "Visible")
                    {
                        ActiveWindow.Visibility = Visibility.Visible;
                    }
                    else if (parameters["Visibility"] == "Hidden")
                    {
                        ActiveWindow.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        ActiveWindow.Visibility = Visibility.Collapsed;
                    }
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainWindowhide : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowhide()
        {
            //_parameters.Add(new UBotParameterDefinition("Title", UBotType.String));
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "bot hide"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    ActiveWindow.Hide();
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainWindowShow : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowShow()
        {
            //_parameters.Add(new UBotParameterDefinition("Title", UBotType.String));
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "bot show"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    ActiveWindow.Show();
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainWindowIsEnabled: IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowIsEnabled()
        {
            var xParameter = new UBotParameterDefinition("Is Enabled", UBotType.String);
            xParameter.Options = new[] { "Yes", "No" };
            xParameter.DefaultValue = "Yes";
            _parameters.Add(xParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "bot Is Enabled"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    if (parameters["Is Enabled"] == "Yes")
                    {
                        ActiveWindow.IsEnabled = true;
                    }
                    else
                    {
                        ActiveWindow.IsEnabled = false;
                    }
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainWindowResizeMode : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowResizeMode()
        {
            var xParameter = new UBotParameterDefinition("ResizeMode", UBotType.String);
            xParameter.Options = new[] { "CanMinimize", "CanResize", "CanResizeWithGrip", "NoResize" };
            xParameter.DefaultValue = "CanResize";
            _parameters.Add(xParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "bot ResizeMode"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    string ResMode = parameters["ResizeMode"];
                    switch (ResMode)
                    {
                        case "CanMinimize":
                            ActiveWindow.ResizeMode = ResizeMode.CanMinimize;
                            break;

                        case "CanResize":
                            ActiveWindow.ResizeMode = ResizeMode.CanResize;
                            break;

                        case "CanResizeWithGrip":
                            ActiveWindow.ResizeMode = ResizeMode.CanResizeWithGrip;
                            break;

                        case "NoResize":
                            ActiveWindow.ResizeMode = ResizeMode.NoResize;
                            break;

                        default:
                            ActiveWindow.ResizeMode = ResizeMode.NoResize;
                            break;
                    }


                   
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainWindowWindowStyle : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowWindowStyle()
        {
            var xParameter = new UBotParameterDefinition("WindowStyle", UBotType.String);
            xParameter.Options = new[] { "None", "SingleBorderWindow", "ThreeDBorderWindow", "ToolWindow" };
            xParameter.DefaultValue = "SingleBorderWindow";
            _parameters.Add(xParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "bot WindowStyle"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    string ResMode = parameters["WindowStyle"];
                    switch (ResMode)
                    {
                        case "None":
                            ActiveWindow.WindowStyle = WindowStyle.None;
                            break;

                        case "SingleBorderWindow":
                            ActiveWindow.WindowStyle = WindowStyle.SingleBorderWindow;
                            break;

                        case "ThreeDBorderWindow":
                            ActiveWindow.WindowStyle = WindowStyle.ThreeDBorderWindow;
                            break;

                        case "ToolWindow":
                            ActiveWindow.WindowStyle = WindowStyle.ToolWindow;
                            break;

                        default:
                            ActiveWindow.WindowStyle = WindowStyle.ThreeDBorderWindow;
                            break;
                    }



                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainWindowWindowState : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowWindowState()
        {
            var xParameter = new UBotParameterDefinition("WindowState", UBotType.String);
            xParameter.Options = new[] { "Normal", "Maximized", "Minimized"};
            xParameter.DefaultValue = "Normal";
            _parameters.Add(xParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "bot WindowState"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    string ResMode = parameters["WindowState"];
                    switch (ResMode)
                    {
                        case "Maximized":
                            ActiveWindow.WindowState = WindowState.Maximized;
                            break;

                        case "Minimized":
                            ActiveWindow.WindowState = WindowState.Minimized;
                            break;

                        case "Normal":
                            ActiveWindow.WindowState = WindowState.Normal;
                            break;

                        default:
                            ActiveWindow.WindowState = WindowState.Normal;
                            break;
                    }



                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainWindowWindowClose : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowWindowClose()
        {
            // no options
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "bot close software"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Application.Current.Shutdown();

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainWindowDisableCloseButton : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainWindowDisableCloseButton()
        {
            //_parameters.Add(new UBotParameterDefinition("Title", UBotType.String));
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "bot Hide Close Button"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    WindowBehavior.SetHideCloseButton(ActiveWindow, true);
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class TabControlIsEnabled : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public TabControlIsEnabled()
        {
            var xParameter = new UBotParameterDefinition("Is Enabled", UBotType.String);
            xParameter.Options = new[] { "Yes", "No" };
            xParameter.DefaultValue = "Yes";
            _parameters.Add(xParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "Tabcontrol Is Enabled"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    foreach (System.Windows.Controls.Grid grid in FindVisualChildren<System.Windows.Controls.Grid>(ActiveWindow))
                    {
                //grid.Children.Add
                        if (grid.Name == "TabGrid")
                        {
                            if (parameters["Is Enabled"] == "Yes")
                            {
                                grid.IsEnabled = true;
                            }
                            else
                            {
                                grid.IsEnabled = false;
                            }
                        }
                        //MessageBox.Show("Grid " + grid.Name + " grid height:" + grid.Height +" nb "+ grids);
                           
                    }
                    
                   
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MenuBarShow : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MenuBarShow()
        {
            var xParameter = new UBotParameterDefinition("Show MenuBar", UBotType.String);
            xParameter.Options = new[] { "Yes", "No" };
            xParameter.DefaultValue = "Yes";
            _parameters.Add(xParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "MenuBar Visibility"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    int grids = 0;
                    foreach (System.Windows.Controls.Menu menubar in FindVisualChildren<System.Windows.Controls.Menu>(ActiveWindow))
                    {
                        grids++;
                        
                            if (parameters["Show MenuBar"] == "Yes")
                            {
                                //menubar.
                                menubar.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                menubar.Visibility = Visibility.Collapsed;
                            }
                        
                        //MessageBox.Show("Grid " + grid.Name + " grid height:" + grid.Height +" nb "+ grids);

                    }


                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class NavBarShow : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public NavBarShow()
        {
            var xParameter = new UBotParameterDefinition("Show NavBar", UBotType.String);
            xParameter.Options = new[] { "Yes", "No" };
            xParameter.DefaultValue = "Yes";
            _parameters.Add(xParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "NavBar Visibility"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    int grids = 2;
                    foreach (System.Windows.Controls.UserControl navb in FindVisualChildren<System.Windows.Controls.UserControl>(ActiveWindow))
                    {
                        
                        //grids++;

                        if (grids == 2)
                        {

                            if (parameters["Show NavBar"] == "Yes")
                            {
                                //navb.
                                navb.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                navb.Visibility = Visibility.Collapsed;
                            }

                            //MessageBox.Show("Grid " + navb.Name + " grid height:" + navb.Height + " nb " + grids);
                        }
                        
                        
                    }


                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class NavbarBackButton : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public NavbarBackButton()
        {
            var xParameter = new UBotParameterDefinition("Text", UBotType.String);
            _parameters.Add(xParameter);

            string familyName;
            string familyList = "";
            System.Drawing.FontFamily[] fontFamilies;

            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            // Get the array of FontFamily objects.
            fontFamilies = installedFontCollection.Families;

            // The loop below creates a large string that is a comma-separated 
            // list of all font family names. 

            int count = fontFamilies.Length;
            for (int j = 0; j < count; ++j)
            {
                familyName = fontFamilies[j].Name;
                familyList = familyList + familyName;
                familyList = familyList + ",";
            }

            string[] split = familyList.Split(',');

            var FontFamily = new UBotParameterDefinition("FontFamily", UBotType.String);
            FontFamily.Options = split;
            _parameters.Add(FontFamily);

            var FontSize = new UBotParameterDefinition("FontSize", UBotType.String);
            FontSize.DefaultValue = "12";
            _parameters.Add(FontSize);
            var FontWeight = new UBotParameterDefinition("FontWeight", UBotType.String);
            FontWeight.Options = new[] { "Normal", "Bold" };
            FontWeight.DefaultValue = "Normal";
            _parameters.Add(FontWeight);
            var TooltipParameter = new UBotParameterDefinition("Tooltip", UBotType.String);
            _parameters.Add(TooltipParameter);
            var ImgParameter = new UBotParameterDefinition("Image", UBotType.String);
            _parameters.Add(ImgParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "NavBar BackButton Content"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    foreach (System.Windows.Controls.Button bt in FindVisualChildren<System.Windows.Controls.Button>(ActiveWindow))
                    {
                        if (bt.Name == "BackButton")
                        {
                            StackPanel stack = new StackPanel();
                            stack.Orientation = Orientation.Horizontal;
                            Image myImage3 = new Image();
                            TextBlock textB = new TextBlock();
                            
                            // Font size
                            if (parameters["FontSize"] != "12")
                            {
                                textB.FontSize = Convert.ToDouble(parameters["FontSize"]);
                            }

                            // Font weight
                            if (parameters["FontWeight"] == "Normal")
                            {
                                textB.FontWeight = FontWeights.Normal;
                            }
                            else if (parameters["FontWeight"] == "Bold")
                            {
                                textB.FontWeight = FontWeights.Bold;
                            }

                            // font
                            textB.FontFamily = new FontFamily(parameters["FontFamily"]);

                            textB.Text = parameters["Text"];

                            if (parameters["Image"] != "")
                            {
                                using (FileStream stream = new FileStream(parameters["Image"], FileMode.Open, FileAccess.Read))
                                {
                                    myImage3.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                                }
                                stack.Children.Add(myImage3);
                            }
                            stack.Children.Add(textB);
                            bt.Content = stack;
                            bt.ToolTip = parameters["Tooltip"];
                        }

                        //if (tb.Name == "StopButton")
                        //{
                        //    Image myImage3 = new Image();

                        //    using (FileStream stream = new FileStream(@"C:\Users\HP\Dropbox\Public\bots files\Stop1NormalRed.png", FileMode.Open, FileAccess.Read))
                        //    {
                        //        myImage3.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        //    }

                        //    tb.Content = myImage3;
                        //    tb.ToolTip = "Start the BotName";
                        //}

                    }


                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class NavBarForwardButton : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public NavBarForwardButton()
        {
            var xParameter = new UBotParameterDefinition("Text", UBotType.String);
            _parameters.Add(xParameter);

            string familyName;
            string familyList = "";
            System.Drawing.FontFamily[] fontFamilies;

            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            // Get the array of FontFamily objects.
            fontFamilies = installedFontCollection.Families;

            // The loop below creates a large string that is a comma-separated 
            // list of all font family names. 

            int count = fontFamilies.Length;
            for (int j = 0; j < count; ++j)
            {
                familyName = fontFamilies[j].Name;
                familyList = familyList + familyName;
                familyList = familyList + ",";
            }

            string[] split = familyList.Split(',');

            var FontFamily = new UBotParameterDefinition("FontFamily", UBotType.String);
            FontFamily.Options = split;
            _parameters.Add(FontFamily);

            var FontSize = new UBotParameterDefinition("FontSize", UBotType.String);
            FontSize.DefaultValue = "12";
            _parameters.Add(FontSize);
            var FontWeight = new UBotParameterDefinition("FontWeight", UBotType.String);
            FontWeight.Options = new[] { "Normal", "Bold" };
            FontWeight.DefaultValue = "Normal";
            _parameters.Add(FontWeight);
            var TooltipParameter = new UBotParameterDefinition("Tooltip", UBotType.String);
            _parameters.Add(TooltipParameter);
            var ImgParameter = new UBotParameterDefinition("Image", UBotType.String);
            _parameters.Add(ImgParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "NavBar ForwardButton Content"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    foreach (System.Windows.Controls.Button bt in FindVisualChildren<System.Windows.Controls.Button>(ActiveWindow))
                    {
                        if (bt.Name == "ForwardButton")
                        {
                            StackPanel stack = new StackPanel();
                            stack.Orientation = Orientation.Horizontal;
                            Image myImage3 = new Image();
                            TextBlock textB = new TextBlock();
                            
                            // Font size
                            if (parameters["FontSize"] != "12")
                            {
                                textB.FontSize = Convert.ToDouble(parameters["FontSize"]);
                            }

                            // Font weight
                            if (parameters["FontWeight"] == "Normal")
                            {
                                textB.FontWeight = FontWeights.Normal;
                            }
                            else if (parameters["FontWeight"] == "Bold")
                            {
                                textB.FontWeight = FontWeights.Bold;
                            }

                            // font
                            textB.FontFamily = new FontFamily(parameters["FontFamily"]);

                            textB.Text = parameters["Text"];

                            if (parameters["Image"] != "")
                            {
                                using (FileStream stream = new FileStream(parameters["Image"], FileMode.Open, FileAccess.Read))
                                {
                                    myImage3.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                                }
                                stack.Children.Add(myImage3);
                            }
                            stack.Children.Add(textB);
                            bt.Content = stack;
                            bt.ToolTip = parameters["Tooltip"];
                        }

                        //if (tb.Name == "StopButton")
                        //{
                        //    Image myImage3 = new Image();

                        //    using (FileStream stream = new FileStream(@"C:\Users\HP\Dropbox\Public\bots files\Stop1NormalRed.png", FileMode.Open, FileAccess.Read))
                        //    {
                        //        myImage3.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        //    }

                        //    tb.Content = myImage3;
                        //    tb.ToolTip = "Start the BotName";
                        //}

                    }


                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class NavBarRefreshButton : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public NavBarRefreshButton()
        {
            var xParameter = new UBotParameterDefinition("Text", UBotType.String);
            _parameters.Add(xParameter);

            string familyName;
            string familyList = "";
            System.Drawing.FontFamily[] fontFamilies;

            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            // Get the array of FontFamily objects.
            fontFamilies = installedFontCollection.Families;

            // The loop below creates a large string that is a comma-separated 
            // list of all font family names. 

            int count = fontFamilies.Length;
            for (int j = 0; j < count; ++j)
            {
                familyName = fontFamilies[j].Name;
                familyList = familyList + familyName;
                familyList = familyList + ",";
            }

            string[] split = familyList.Split(',');

            var FontFamily = new UBotParameterDefinition("FontFamily", UBotType.String);
            FontFamily.Options = split;
            _parameters.Add(FontFamily);

            var FontSize = new UBotParameterDefinition("FontSize", UBotType.String);
            FontSize.DefaultValue = "12";
            _parameters.Add(FontSize);
            var FontWeight = new UBotParameterDefinition("FontWeight", UBotType.String);
            FontWeight.Options = new[] { "Normal", "Bold" };
            FontWeight.DefaultValue = "Normal";
            _parameters.Add(FontWeight);
            var TooltipParameter = new UBotParameterDefinition("Tooltip", UBotType.String);
            _parameters.Add(TooltipParameter);
            var ImgParameter = new UBotParameterDefinition("Image", UBotType.String);
            _parameters.Add(ImgParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "NavBar RefreshButton Content"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    foreach (System.Windows.Controls.Button bt in FindVisualChildren<System.Windows.Controls.Button>(ActiveWindow))
                    {
                        if (bt.Name == "RefreshButton")
                        {
                            StackPanel stack = new StackPanel();
                            stack.Orientation = Orientation.Horizontal;
                            Image myImage3 = new Image();
                            TextBlock textB = new TextBlock();
                            
                            // Font size
                            if (parameters["FontSize"] != "12")
                            {
                                textB.FontSize = Convert.ToDouble(parameters["FontSize"]);
                            }

                            // Font weight
                            if (parameters["FontWeight"] == "Normal")
                            {
                                textB.FontWeight = FontWeights.Normal;
                            }
                            else if (parameters["FontWeight"] == "Bold")
                            {
                                textB.FontWeight = FontWeights.Bold;
                            }

                            // font
                            textB.FontFamily = new FontFamily(parameters["FontFamily"]);

                            textB.Text = parameters["Text"];

                            if (parameters["Image"] != "")
                            {
                                using (FileStream stream = new FileStream(parameters["Image"], FileMode.Open, FileAccess.Read))
                                {
                                    myImage3.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                                }
                                stack.Children.Add(myImage3);
                            }
                            stack.Children.Add(textB);
                            bt.Content = stack;
                            bt.ToolTip = parameters["Tooltip"];
                        }

                        //if (tb.Name == "StopButton")
                        //{
                        //    Image myImage3 = new Image();

                        //    using (FileStream stream = new FileStream(@"C:\Users\HP\Dropbox\Public\bots files\Stop1NormalRed.png", FileMode.Open, FileAccess.Read))
                        //    {
                        //        myImage3.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        //    }

                        //    tb.Content = myImage3;
                        //    tb.ToolTip = "Start the BotName";
                        //}

                    }


                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class NavBarStopButton : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public NavBarStopButton()
        {
            var xParameter = new UBotParameterDefinition("Text", UBotType.String);
            _parameters.Add(xParameter);

            string familyName;
            string familyList = "";
            System.Drawing.FontFamily[] fontFamilies;

            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            // Get the array of FontFamily objects.
            fontFamilies = installedFontCollection.Families;

            // The loop below creates a large string that is a comma-separated 
            // list of all font family names. 

            int count = fontFamilies.Length;
            for (int j = 0; j < count; ++j)
            {
                familyName = fontFamilies[j].Name;
                familyList = familyList + familyName;
                familyList = familyList + ",";
            }

            string[] split = familyList.Split(',');

            var FontFamily = new UBotParameterDefinition("FontFamily", UBotType.String);
            FontFamily.Options = split;
            _parameters.Add(FontFamily);

            var FontSize = new UBotParameterDefinition("FontSize", UBotType.String);
            FontSize.DefaultValue = "12";
            _parameters.Add(FontSize);
            var FontWeight = new UBotParameterDefinition("FontWeight", UBotType.String);
            FontWeight.Options = new[] { "Normal", "Bold" };
            FontWeight.DefaultValue = "Normal";
            _parameters.Add(FontWeight);
            var TooltipParameter = new UBotParameterDefinition("Tooltip", UBotType.String);
            _parameters.Add(TooltipParameter);
            var ImgParameter = new UBotParameterDefinition("Image", UBotType.String);
            _parameters.Add(ImgParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "NavBar StopButton Content"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    foreach (System.Windows.Controls.Button bt in FindVisualChildren<System.Windows.Controls.Button>(ActiveWindow))
                    {
                        if (bt.Name == "StopButton")
                        {
                            StackPanel stack = new StackPanel();
                            stack.Orientation = Orientation.Horizontal;
                            Image myImage3 = new Image();
                            TextBlock textB = new TextBlock();
                            
                            // Font size
                            if (parameters["FontSize"] != "12")
                            {
                                textB.FontSize = Convert.ToDouble(parameters["FontSize"]);
                            }

                            // Font weight
                            if (parameters["FontWeight"] == "Normal")
                            {
                                textB.FontWeight = FontWeights.Normal;
                            }
                            else if (parameters["FontWeight"] == "Bold")
                            {
                                textB.FontWeight = FontWeights.Bold;
                            }

                            // font
                            textB.FontFamily = new FontFamily(parameters["FontFamily"]);

                            textB.Text = parameters["Text"];

                            if (parameters["Image"] != "")
                            {
                                using (FileStream stream = new FileStream(parameters["Image"], FileMode.Open, FileAccess.Read))
                                {
                                    myImage3.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                                }
                                stack.Children.Add(myImage3);
                            }
                            stack.Children.Add(textB);
                            bt.Content = stack;
                            bt.ToolTip = parameters["Tooltip"];
                        }

                        //if (tb.Name == "StopButton")
                        //{
                        //    Image myImage3 = new Image();

                        //    using (FileStream stream = new FileStream(@"C:\Users\HP\Dropbox\Public\bots files\Stop1NormalRed.png", FileMode.Open, FileAccess.Read))
                        //    {
                        //        myImage3.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        //    }

                        //    tb.Content = myImage3;
                        //    tb.ToolTip = "Start the BotName";
                        //}

                    }


                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class MainBrowserShow : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public MainBrowserShow()
        {
            var xParameter = new UBotParameterDefinition("Show Main Browser", UBotType.String);
            xParameter.Options = new[] { "Yes", "No" };
            xParameter.DefaultValue = "Yes";
            _parameters.Add(xParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "MainBrowser Visibility"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    int grids = 0;
                    foreach (System.Windows.Controls.UserControl navb in FindVisualChildren<System.Windows.Controls.UserControl>(ActiveWindow))
                    {

                        grids++;

                        if (grids == 3)
                        {

                            if (parameters["Show Main Browser"] == "Yes")
                            {
                                //navb.
                                navb.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                navb.Visibility = Visibility.Collapsed;
                            }

                            //MessageBox.Show("Grid " + navb.Name + " grid height:" + navb.Height + " nb " + grids);
                        }


                    }


                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class EditRunButton : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();
        
        public EditRunButton()
        {
            var xParameter = new UBotParameterDefinition("Text", UBotType.String);
            _parameters.Add(xParameter);
            
            string familyName;
            string familyList = "";
            System.Drawing.FontFamily[] fontFamilies;

            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            // Get the array of FontFamily objects.
            fontFamilies = installedFontCollection.Families;

            // The loop below creates a large string that is a comma-separated 
            // list of all font family names. 

            int count = fontFamilies.Length;
            for (int j = 0; j < count; ++j)
            {
                familyName = fontFamilies[j].Name;
                familyList = familyList + familyName;
                familyList = familyList + ",";
            }

            string[] split = familyList.Split(',');

            var FontFamily = new UBotParameterDefinition("FontFamily", UBotType.String);
            FontFamily.Options = split;
            _parameters.Add(FontFamily);

            var FontSize = new UBotParameterDefinition("FontSize", UBotType.String);
            FontSize.DefaultValue = "12"; 
            _parameters.Add(FontSize);
            var FontWeight = new UBotParameterDefinition("FontWeight", UBotType.String);
            FontWeight.Options = new[] { "Normal", "Bold" };
            FontWeight.DefaultValue = "Normal";
            _parameters.Add(FontWeight);
            var TooltipParameter = new UBotParameterDefinition("Tooltip", UBotType.String);
            _parameters.Add(TooltipParameter);
            var ImgParameter = new UBotParameterDefinition("Image", UBotType.String);
            _parameters.Add(ImgParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "RunButton Content"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    foreach (System.Windows.Controls.Button bt in FindVisualChildren<System.Windows.Controls.Button>(ActiveWindow))
                    {
                        if (bt.Name == "RunButton")
                        {
                            StackPanel stack = new StackPanel();
                            stack.Orientation = Orientation.Horizontal;
                            Image myImage3 = new Image();
                            TextBlock textB = new TextBlock();
                            
                            // Font size
                            if (parameters["FontSize"] != "12")
                            {
                                textB.FontSize = Convert.ToDouble(parameters["FontSize"]);
                            }

                            // Font weight
                            if (parameters["FontWeight"] == "Normal")
                            {
                                textB.FontWeight = FontWeights.Normal;
                            }
                            else if (parameters["FontWeight"] == "Bold")
                            {
                                textB.FontWeight = FontWeights.Bold;
                            }

                            // font
                            textB.FontFamily = new FontFamily(parameters["FontFamily"]);

                            textB.Foreground = new SolidColorBrush(Colors.Black);

                            textB.Text = parameters["Text"];

                            if (parameters["Image"] != "")
                            {
                                using (FileStream stream = new FileStream(parameters["Image"], FileMode.Open, FileAccess.Read))
                                {
                                    myImage3.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                                }
                                stack.Children.Add(myImage3);
                            }
                            stack.Children.Add(textB);
                            bt.Content = stack;
                            bt.ToolTip = parameters["Tooltip"];
                        }

                        //if (tb.Name == "StopButton")
                        //{
                        //    Image myImage3 = new Image();

                        //    using (FileStream stream = new FileStream(@"C:\Users\HP\Dropbox\Public\bots files\Stop1NormalRed.png", FileMode.Open, FileAccess.Read))
                        //    {
                        //        myImage3.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        //    }

                        //    tb.Content = myImage3;
                        //    tb.ToolTip = "Start the BotName";
                        //}

                    }


                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class EditStopButton : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public EditStopButton()
        {
            var xParameter = new UBotParameterDefinition("Text", UBotType.String);
            _parameters.Add(xParameter);

            string familyName;
            string familyList = "";
            System.Drawing.FontFamily[] fontFamilies;

            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            // Get the array of FontFamily objects.
            fontFamilies = installedFontCollection.Families;

            // The loop below creates a large string that is a comma-separated 
            // list of all font family names. 

            int count = fontFamilies.Length;
            for (int j = 0; j < count; ++j)
            {
                familyName = fontFamilies[j].Name;
                familyList = familyList + familyName;
                familyList = familyList + ",";
            }

            string[] split = familyList.Split(',');

            var FontFamily = new UBotParameterDefinition("FontFamily", UBotType.String);
            FontFamily.Options = split;
            _parameters.Add(FontFamily);

            var FontSize = new UBotParameterDefinition("FontSize", UBotType.String);
            FontSize.DefaultValue = "12";
            _parameters.Add(FontSize);
            var FontWeight = new UBotParameterDefinition("FontWeight", UBotType.String);
            FontWeight.Options = new[] { "Normal", "Bold" };
            FontWeight.DefaultValue = "Normal";
            _parameters.Add(FontWeight);
            var TooltipParameter = new UBotParameterDefinition("Tooltip", UBotType.String);
            _parameters.Add(TooltipParameter);
            var ImgParameter = new UBotParameterDefinition("Image", UBotType.String);
            _parameters.Add(ImgParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "StopButton Content"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    foreach (System.Windows.Controls.Button bt in FindVisualChildren<System.Windows.Controls.Button>(ActiveWindow))
                    {
                        if (bt.Name == "StopScriptButton")
                        {
                            StackPanel stack = new StackPanel();
                            stack.Orientation = Orientation.Horizontal;
                            Image myImage3 = new Image();
                            TextBlock textB = new TextBlock();
                            
                            // Font size
                            if (parameters["FontSize"] != "12")
                            {
                                textB.FontSize = Convert.ToDouble(parameters["FontSize"]);
                            }

                            // Font weight
                            if (parameters["FontWeight"] == "Normal")
                            {
                                textB.FontWeight = FontWeights.Normal;
                            }
                            else if (parameters["FontWeight"] == "Bold")
                            {
                                textB.FontWeight = FontWeights.Bold;
                            }

                            // font
                            textB.FontFamily = new FontFamily(parameters["FontFamily"]);

                            textB.Text = parameters["Text"];

                            using (FileStream stream = new FileStream(parameters["Image"], FileMode.Open, FileAccess.Read))
                            {
                                myImage3.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                            }
                            stack.Children.Add(myImage3);
                            stack.Children.Add(textB);
                            bt.Content = stack;
                            bt.ToolTip = parameters["Tooltip"];
                        }

                       

                    }


                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class EditPauseButton : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public EditPauseButton()
        {
            var xParameter = new UBotParameterDefinition("Text", UBotType.String);
            _parameters.Add(xParameter);

            string familyName;
            string familyList = "";
            System.Drawing.FontFamily[] fontFamilies;

            InstalledFontCollection installedFontCollection = new InstalledFontCollection();

            // Get the array of FontFamily objects.
            fontFamilies = installedFontCollection.Families;

            // The loop below creates a large string that is a comma-separated 
            // list of all font family names. 

            int count = fontFamilies.Length;
            for (int j = 0; j < count; ++j)
            {
                familyName = fontFamilies[j].Name;
                familyList = familyList + familyName;
                familyList = familyList + ",";
            }

            string[] split = familyList.Split(',');

            var FontFamily = new UBotParameterDefinition("FontFamily", UBotType.String);
            FontFamily.Options = split;
            _parameters.Add(FontFamily);

            var FontSize = new UBotParameterDefinition("FontSize", UBotType.String);
            FontSize.DefaultValue = "12";
            _parameters.Add(FontSize);
            var FontWeight = new UBotParameterDefinition("FontWeight", UBotType.String);
            FontWeight.Options = new[] { "Normal", "Bold" };
            FontWeight.DefaultValue = "Normal";
            _parameters.Add(FontWeight);
            var TooltipParameter = new UBotParameterDefinition("Tooltip", UBotType.String);
            _parameters.Add(TooltipParameter);
            var ImgParameter = new UBotParameterDefinition("Image", UBotType.String);
            _parameters.Add(ImgParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "PauseButton Content"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    foreach (System.Windows.Controls.Button bt in FindVisualChildren<System.Windows.Controls.Button>(ActiveWindow))
                    {
                        if (bt.Name == "PauseButton")
                        {
                            StackPanel stack = new StackPanel();
                            stack.Orientation = Orientation.Horizontal;
                            Image myImage3 = new Image();
                            TextBlock textB = new TextBlock();
                            
                            // Font size
                            if(parameters["FontSize"] != "12")
                            {
                            textB.FontSize = Convert.ToDouble(parameters["FontSize"]);
                            }

                            // Font weight
                            if (parameters["FontWeight"] == "Normal")
                            {
                                textB.FontWeight = FontWeights.Normal;
                            }
                            else if (parameters["FontWeight"] == "Bold")
                            {
                                textB.FontWeight = FontWeights.Bold;
                            }

                            // font
                            textB.FontFamily = new FontFamily(parameters["FontFamily"]);

                            textB.Text = parameters["Text"];

                            using (FileStream stream = new FileStream(parameters["Image"], FileMode.Open, FileAccess.Read))
                            {
                                myImage3.Source = BitmapFrame.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                            }
                            stack.Children.Add(myImage3);
                            stack.Children.Add(textB);
                            bt.Content = stack;
                            bt.ToolTip = parameters["Tooltip"];
                        }



                    }


                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class TabMenuBar : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public TabMenuBar()
        {
            var xParameter = new UBotParameterDefinition("Show Tab Controls", UBotType.String);
            xParameter.Options = new[] { "Yes", "No" };
            xParameter.DefaultValue = "Yes";
            _parameters.Add(xParameter);
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "TabMenu Visibility"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    int grids = 2;
                    foreach (System.Windows.Controls.TabControl tabs in FindVisualChildren<System.Windows.Controls.TabControl>(ActiveWindow))
                    {

                        //grids++;

                        if (grids == 2)
                        {

                            
                            if (parameters["Show Tab Controls"] == "Yes")
                            {
                                //navb.
                                tabs.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                tabs.Visibility = Visibility.Collapsed;
                            }
                              

                            //MessageBox.Show("Tabs " + tabs.Name + " grid height:" + tabs.Height + " nb " + grids);
                        }


                    }


                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }
    }

    class ToolBarSize : IUBotCommand
    {
        private List<UBotParameterDefinition> _parameters = new List<UBotParameterDefinition>();

        public ToolBarSize()
        {
            _parameters.Add(new UBotParameterDefinition("size", UBotType.String));
            _parameters.Add(new UBotParameterDefinition("height", UBotType.String));
        }

        public string Category
        {
            get { return "Botguru UI Commands"; }
        }

        public string CommandName
        {
            get { return "toolbar size"; }
        }

        public void Execute(IUBotStudio ubotStudio, Dictionary<string, string> parameters)
        {
            Window ActiveWindow = new Window();

            System.Windows.Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal,
            new Action(delegate()
            {

                ActiveWindow = System.Windows.Application.Current.MainWindow;

                if (ActiveWindow != null)
                {
                    foreach (System.Windows.Controls.ToolBar tb in FindVisualChildren<System.Windows.Controls.ToolBar>(ActiveWindow))
                    {
                        tb.FontSize = Convert.ToInt32(parameters["size"]);
                        tb.Height = Convert.ToInt32(parameters["height"]);
                    }
                }


            }));

        }

        public bool IsContainer
        {
            get { return false; }
        }

        public IEnumerable<UBotParameterDefinition> ParameterDefinitions
        {
            get { return _parameters; }
        }

        public UBotVersion UBotVersion
        {
            get { return UBotVersion.Standard; }
        }

        private static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }

}
