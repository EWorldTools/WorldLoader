using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using WorldLoader.HookUtils;

namespace WorldLoader.Il2CppUnhollower.Packages.Models // MelonLoader
{
    internal class ExecutablePackage : PackageBase
    {
        internal static AutoResetEvent ResetEvent_Output;
        internal static AutoResetEvent ResetEvent_Error;
        internal string OutputFolder;
        internal string ExeFilePath;

        internal virtual bool Execute() => true;
        internal bool Execute(string[] args, bool parenthesize_args = true, Dictionary<string, string> environment = null, bool UnhollowDlls = false)
        {

            if (!File.Exists(ExeFilePath))
            {
                Logs.Error($"{ExeFilePath} does not Exist!");
                ThrowInternalFailure($"Failed to Execute {Name}!");
                return false;
            }

            Logs.Log($"Executing {Name}...");
            try
            {
                ResetEvent_Output = new AutoResetEvent(false);
                ResetEvent_Error = new AutoResetEvent(false);

                ProcessStartInfo processStartInfo = new ProcessStartInfo($"\"{ExeFilePath.Replace("\"", "\\\"")}\"", // Replacing double quotes for Linux
                    parenthesize_args
                    ?
                    string.Join(" ", args.Where(s => !string.IsNullOrEmpty(s)).Select(it => "\"" + Regex.Replace(it, @"(\\+)$", @"$1$1") + "\""))
                    :
                    string.Join(" ", args.Where(s => !string.IsNullOrEmpty(s)).Select(it => Regex.Replace(it, @"(\\+)$", @"$1$1"))));
                processStartInfo.UseShellExecute = false;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.CreateNoWindow = true;
                processStartInfo.WorkingDirectory = Path.GetDirectoryName(ExeFilePath);

                if (environment != null)
                    foreach (var kvp in environment)
                        processStartInfo.EnvironmentVariables[kvp.Key] = kvp.Value;

                Logs.Log("\"" + ExeFilePath + "\" " + processStartInfo.Arguments);

                Process process = new Process();
                process.StartInfo = processStartInfo;
                process.OutputDataReceived += OutputStream;
                process.ErrorDataReceived += ErrorStream;
                process.Start();


                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                process.WaitForExit();
                ResetEvent_Output.WaitOne();
                ResetEvent_Error.WaitOne();
                if (UnhollowDlls)
                    Core.Cpp2ILOutputFolder = LoadAssembliesFrom(Directory.CreateDirectory(OutputFolder));


                return process.ExitCode == 0;
            }
            catch (Exception ex)
            {
                Logs.Error(ex.ToString());
                ThrowInternalFailure($"Failed to Execute {Name}!");
            }

            return false;
        }

        internal static List<AssemblyDefinition> LoadAssembliesFrom(DirectoryInfo directoryInfo)
        {
            var resolver = new BasicResolver();
            var inputAssemblies = directoryInfo.EnumerateFiles("*.dll").Select(f => AssemblyDefinition.ReadAssembly(
                f.FullName,
                new ReaderParameters { AssemblyResolver = resolver })).ToList();
            foreach (var assembly in inputAssemblies)
            {
                resolver.Register(assembly);
            }

            return inputAssemblies;
        }

        private static void OutputStream(object sender, DataReceivedEventArgs e) { if (e.Data == null) ResetEvent_Output.Set(); else Logs.Debug(e.Data); }
        private static void ErrorStream(object sender, DataReceivedEventArgs e) { if (e.Data == null) ResetEvent_Error.Set(); else Logs.Error(e.Data); }
    }

    internal class BasicResolver : DefaultAssemblyResolver
    {
        public void Register(AssemblyDefinition ad) =>
            base.RegisterAssembly(ad);
        
    }

}
