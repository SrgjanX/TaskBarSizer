using System;
using System.Diagnostics;
using System.Windows;

namespace TaskBarSizer
{
    public partial class MainWindow : Window
    {
        private const string TaskBarRegistryKeyName = "TaskbarSi";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (RegistryManager regMgr = new RegistryManager())
            {
                regMgr.OnRegistyManagerErrorOccurred += RegMgr_OnRegistyManagerErrorOccurred;
                string val = regMgr.Read(TaskBarRegistryKeyName);
                switch (val)
                {
                    case "0":
                        radio0.IsChecked = true;
                        break;
                    case "1":
                        radio1.IsChecked = true;
                        break;
                    case "2":
                        radio2.IsChecked = true;
                        break;
                }
            }
        }

        private void RegMgr_OnRegistyManagerErrorOccurred(Exception ex, string methodName)
        {
            MessageBox.Show(ex.Message, methodName, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void ButtonSet_Click(object sender, RoutedEventArgs e)
        {
            void SetValue(int val)
            {
                using (RegistryManager regMgr = new RegistryManager())
                    regMgr.WriteDword(TaskBarRegistryKeyName, val);
                RestartExplorer();
            }

            if (radio0.IsChecked == true)
            {
                SetValue(0);
            }
            else if (radio1.IsChecked == true)
            {
                SetValue(1);
            }
            else if (radio2.IsChecked == true)
            {
                SetValue(2);
            }
        }

        private void RestartExplorer()
        {
            try
            {
                foreach (Process process in Process.GetProcesses())
                {
                    try
                    {
                        if (process?.MainModule?.FileName?.ToLower()?.EndsWith(":\\windows\\explorer.exe") == true)
                        {
                            process.Kill();
                            break;
                        }
                    }
                    catch
                    {
                    }
                }
                //Process.Start("explorer.exe");
            }
            catch (Exception)
            {
                MessageBox.Show("Could not restart explorer.exe.", "Error Occurred", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}