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

		public AssemblyResolveManager() {
			var files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\WorldLoader\\UnhollowedAsm");
			if (files == null) throw new Exception("Files Are Null!");
			foreach (var Asm in files) {
				if (Asm.EndsWith(".dll"))
					UnhollowedAssemblys.Add(Asm.RemoveFullPath(), Asm);
			}
			AdditionalChecks.Add(Directory.CreateDirectory("UserLibs"));
			AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => {
				string name = args.Name;
				byte[] ByteData = null;
				if (args.Name.Contains(','))
					name = args.Name.Substring(0, args.Name.IndexOf(','));// This isn't too good, but it works ig
				if (AdditionalFiles.Any(a => a.Name == name)) return Assembly.Load(File.ReadAllBytes(AdditionalFiles.SingleOrDefault(a => a.Name == name).FullName));
				if (AdditionalAsmChecks.TryGetValue(name, out var prasm)) return prasm;
				Logs.Debug($"[AssemblyResolve] Failed Finding an Assembly Normally! Checking Unhollowed For Assembly {name}");
				if (UnhollowedAssemblys.TryGetValue(name + ".dll", out var asm))
					return Assembly.Load(File.ReadAllBytes(asm));
				if (AdditionalChecks.Count > 0) AdditionalChecks.ForEach(a => {
					var files = a.GetFiles();
					var filepath = files.FirstOrDefault(a => a.Name == name).FullName;
					if (!string.IsNullOrEmpty(filepath))
						ByteData = File.ReadAllBytes(filepath);
				});
				if (ByteData.Length > 10)
					return Assembly.Load(ByteData);
				Logs.Warn($"Sender {sender} Tried to get an Assembly ({name}) and FAILED!");
				return null;
			};
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
