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

		public AssemblyResolveManager() { // TODO : Rewrite this, its done really bad, turn on Debug and you'll see
			var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\WorldLoader\\UnhollowedAsm");
			if (files == null) throw new Exception("Files Are Null!");
			foreach (var Asm in files) {
				if (Asm.EndsWith(".dll"))
					UnhollowedAssemblys.Add(Asm.RemoveFullPath(), Asm);
			}
			AdditionalChecks.Add(Directory.CreateDirectory("UserLibs"));
			AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => {
				for (long i = 0; i < AppDomain.CurrentDomain.GetAssemblies().LongCount(); i++)
					if (args.Name == AppDomain.CurrentDomain.GetAssemblies()[i].FullName) return AppDomain.CurrentDomain.GetAssemblies()[i];
				string name = args.Name;
				byte[] ByteData = null;
				if (args.Name.Contains(','))
					name = args.Name.Substring(0, args.Name.IndexOf(','));// This isn't too good, but it works ig
				if (AdditionalFiles.Any(a => a.Name == name)) {

					var addFiles = Assembly.Load(File.ReadAllBytes(AdditionalFiles.SingleOrDefault(a => a.Name == name).FullName));
					invOnRelv(addFiles);
					return addFiles;
				}
				if (AdditionalAsmChecks.TryGetValue(name, out var prasm)) {
					invOnRelv(prasm);
					return prasm;
				}
				Logs.Debug($"[AssemblyResolve] Failed Finding an Assembly Normally! Checking Unhollowed For Assembly {name}");
				if (UnhollowedAssemblys.TryGetValue(name + ".dll", out var asm)) { 
					var norAsm = Assembly.Load(File.ReadAllBytes(asm));
					invOnRelv(norAsm);
					return norAsm;
				}
				if (AdditionalChecks.Count > 0) AdditionalChecks.ForEach(a => {
					var files = a.GetFiles();
					var filepath = files.FirstOrDefault(a => a.Name == name).FullName;
					if (!string.IsNullOrEmpty(filepath))
						ByteData = File.ReadAllBytes(filepath);
				});
				if (ByteData.Length > 10) {
					var byteDataAssembly = Assembly.Load(ByteData);
					invOnRelv(byteDataAssembly);
					return byteDataAssembly;
				}
				Logs.Warn($"Sender {args.RequestingAssembly.FullName} Tried to get an Assembly ({name}) and FAILED!"); // I dont think i did this right
				return null;
			};
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
