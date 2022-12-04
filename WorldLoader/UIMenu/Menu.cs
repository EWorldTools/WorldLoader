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

namespace WorldLoader
{
	internal partial class LoaderMenu : Form
    {
        private static OpenFileDialog OpenFile;

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
                    WorldLoader._ModManager.LoadMod(filePath, dialogResult == DialogResult.Yes);
                }
                catch (Exception E) { 
                    Logs.Error("Error Loading Mod", E);
                }
            }
        }

        private void UnloadModDropDown(object sender, EventArgs e)
        {

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
    }
}
