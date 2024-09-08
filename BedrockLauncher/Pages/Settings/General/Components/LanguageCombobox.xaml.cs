﻿using BedrockLauncher.Pages.Play.Home.Components;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BedrockLauncher.Pages.Settings.General.Components
{
    /// <summary>
    /// Interaction logic for LanguageCombobox.xaml
    /// </summary>
    public partial class LanguageCombobox : ComboBox
    {
        public LanguageCombobox()
        {
            InitializeComponent();
        }

        private void LanguageCombobox_DropDownClosed(object sender, EventArgs e)
        {
            var item = this.SelectedItem as BedrockLauncher.Localization.Language.LanguageDefinition;
            if (item == null) return;
            BedrockLauncher.Localization.Language.LanguageManager.SetLanguage(item.Locale);
            Program.OnApplicationRefresh();
            /*string currentDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string ParentDir = Directory.GetParent(currentDir).FullName;
            string path = System.IO.Path.Combine(ParentDir, "StartBedrockLauncher.exe");
            StartProcess(path);
            Trace.WriteLine(path);
            void StartProcess(string path)
            {
                var startInfo = new ProcessStartInfo(path)
                {
                    UseShellExecute = true,
                    Verb = "runas"
                };
                Process.Start(startInfo);
                Application.Current.Shutdown();
            }*/
        }


        private void ReloadLang()
        {
            var items = BedrockLauncher.Localization.Language.LanguageManager.GetResourceDictonaries();
            this.ItemsSource = items;
            string language = BedrockLauncher.Localization.Properties.Settings.Default.Language;

            // Set chosen language in language combobox
            if (items.Exists(x => x.Locale.ToString() == language))
            {
                this.SelectedItem = items.Where(x => x.Locale.ToString() == language).FirstOrDefault();
            }
            else
            {
                this.SelectedItem = items.Where(x => x.Locale.ToString() == "en-US").FirstOrDefault();
            }
        }

        private void LanguageCombobox_Initialized(object sender, EventArgs e)
        {

        }

        private void LanguageCombobox_Loaded(object sender, RoutedEventArgs e)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime) ReloadLang();
        }
    }
}
