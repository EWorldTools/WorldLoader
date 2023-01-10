using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using WorldLoader.Il2CppUnhollower;
using WorldLoader.Il2CppUnhollower.Packages;
using WorldLoader.HookUtils;
using System.Reflection;
using System.Diagnostics;
using Il2CppInterop.Generator;
using Il2CppInterop.Generator.Runners;
using WorldLoader.ModulesLibs.Managers;

namespace WorldLoader
{
	internal partial class LoaderMenu : Form
    {
        private static OpenFileDialog OpenFile;
        internal static bool IsDone { get; private set; }
        public LoaderMenu()
		{
			InitializeComponent();
            OpenFile = new OpenFileDialog
            {
                Title = "Find Mod",
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true,

                DefaultExt = ".dll",
                Filter = ".net files (*.dll)|*.dll",
                FilterIndex = 2,
            };
            flatLabel1.Text = "";
            flatLabel2.Text = "";
            IsDone = true;
            UnhollowerLogTraceTgl.Checked = C.L.Config.UnhollowerLogTrace;
            HollowerPassAllNamesTlg.Checked = C.L.Config.HollowerPassAllNames;
            DebugTog.Checked = C.L.Config.Debug;

        }

        private void RegenAssemblysButton(object sender, EventArgs e) =>
            Core.Run(true);
        
        private void RunCpp2ILGen(object sender, EventArgs e) =>
            Core.dumper.Execute();

        private void RunUnhollowerButton(object sender, EventArgs e)
        {
            Core.Cpp2ILOutputFolder = Cpp2IL.LoadAssembliesFrom(Directory.CreateDirectory(Core.dumper.OutputFolder));
            Core.assemblyunhollower.Execute();
        }

        private void LoadModButton(object sender, EventArgs e)
        {
            if (OpenFile.ShowDialog() == DialogResult.OK) {
                var filePath = OpenFile.FileName;
                DialogResult dialogResult = MessageBox.Show("Would You Like to Invoke \"OnInject\"?", "WorldLoader", MessageBoxButtons.YesNo);
                try {
                    ModManager.LoadMod(File.ReadAllBytes(filePath), dialogResult == DialogResult.Yes);
                }
                catch (Exception E) { 
                    Logs.Error("Error Loading Mod", E);
                }
            }
        }

        private void UnloadModDropDown(object sender, EventArgs e)
        {
            if (flatComboBox2.SelectedItem == null) return;
            var name = flatComboBox2.SelectedItem.ToString();
            if (name == null) return;
            ModManager.UnloadMod(name);
            flatComboBox2.Items.Remove(flatComboBox2.SelectedItem);
        }

        private void flatButton5_Click(object sender, EventArgs e)
        {
            if (OpenFile.ShowDialog() == DialogResult.OK) {
                var filePath = OpenFile.FileName;
                try {
                    Assembly.Load(File.ReadAllBytes(filePath));
                }
                catch (Exception E) { 
                    Logs.Error("Error Loading Mod", E);
                }
            }
        }

        private void ConsoleTab_Click(object sender, EventArgs e)
        {

        }

        private void DebugTog_CheckedChanged(object sender)
        {
            C.L.Config.Debug = DebugTog.Checked;
            C.L.Save();
        }

        private void HollowerPassAllNamesTlg_CheckedChanged(object sender) {
            C.L.Config.HollowerPassAllNames = HollowerPassAllNamesTlg.Checked;
            C.L.Save();
        }

        private void UnhollowerLogTraceTgl_CheckedChanged(object sender) {
            C.L.Config.UnhollowerLogTrace = UnhollowerLogTraceTgl.Checked;
            C.L.Save();
        }

        private void ResetBtn_Click(object sender, EventArgs e)
        {
            C.L.Config.UnhollowerLogTrace = false;
            C.L.Config.HollowerPassAllNames = false;
            C.L.Config.Debug = false;
            UnhollowerLogTraceTgl.Checked = C.L.Config.UnhollowerLogTrace;
            HollowerPassAllNamesTlg.Checked = C.L.Config.HollowerPassAllNames;
            DebugTog.Checked = C.L.Config.Debug;
        }

        private void DownloadDataButton_Click(object sender, EventArgs e)
        {
            Process.Start(Logs.LogLocation);
        }

        private void GenMaop_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Input (Non Obfu Assemblys)");
            var src = Console.ReadLine().Replace("\"",String.Empty);
            Console.WriteLine("Output");
            var output = Console.ReadLine().Replace("\"", String.Empty);
            var opts = new GeneratorOptions
            {
                Source = Cpp2IL.LoadAssembliesFrom(Directory.CreateDirectory(src)),
                DeobfuscationNewAssembliesPath = Core.dumper.OutputFolder,
                OutputDir = output,
            };

            Il2CppInteropGenerator.Create(opts)
                                  .AddDeobfuscationMapGenerator()
                                  .Run();
        }
    }
}
