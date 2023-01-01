using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WorldLoader.HookUtils;
using WorldLoader.Utils;

namespace WorldLoader.ModulesLibs.Managers
{
    public class AssemblyResolveManager
    {
		internal static Dictionary<string, string> UnhollowedAssemblys { get; set; } = new();
		internal static List<DirectoryInfo> AdditionalChecks = new();
		internal static List<FileInfo> AdditionalFiles = new();
		internal static Dictionary<string, Assembly> AdditionalAsmChecks = new();

		public static Action<Assembly> OnAsmResolve;

        [Obsolete]
        public AssemblyResolveManager() { 
			AppDomain.CurrentDomain.AppendPrivatePath(Directory.GetCurrentDirectory() + "\\WorldLoader\\UnhollowedAsm");
			AppDomain.CurrentDomain.AppendPrivatePath(Directory.CreateDirectory("UserLibs").FullName);			
		}

		private static void invOnRelv(Assembly assembly) {
			try
			{
				OnAsmResolve?.Invoke(assembly);
			}
			catch (Exception e)
			{
				Logs.Error("Error During OnAsmResolve", e);
			}
		}

		/// <summary>
		/// Adds an Additional Asm Check to the AssemblyResolveFix, 
		/// </summary>
		/// <param name="string">name</param>
		/// <param name="Assembly">asm</param>
		public static void AddAdditionalAsmCheck(string name, Assembly asm) => AdditionalAsmChecks.Add(name, asm);
		public static void AdditionalFileCheck(FileInfo file) => AdditionalFiles.Add(file);
		public static void AdditionalFileCheck(string file) => AdditionalFiles.Add(new FileInfo(file));
		public static void AdditionalDirectoryChecks(DirectoryInfo dir) => AdditionalChecks.Add(dir);

	}
}
