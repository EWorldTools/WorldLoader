using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using WorldLoader.HookUtils;

namespace WorldLoader.Il2CppUnhollower
{
    internal static class FileHandler
    {
        private static WSHA512 sha512 = new WSHA512();
        private static FieldInfo HashAlgorithm_HashSizeValue;
        public static void SetHashSizeValue(this HashAlgorithm _this, int value)
        {
            if (HashAlgorithm_HashSizeValue == null)
                HashAlgorithm_HashSizeValue = typeof(HashAlgorithm).GetField("HashSizeValue", BindingFlags.Public | BindingFlags.Instance);
            if (HashAlgorithm_HashSizeValue != null)
                HashAlgorithm_HashSizeValue.SetValue(_this, value);
        }
        internal static string Hash(string filepath)
            => BitConverter.ToString(sha512.ComputeHash(File.ReadAllBytes(filepath))).Replace("-", "").ToLowerInvariant();
            
        internal static bool Download(string url, string destination)
        {
            if (string.IsNullOrEmpty(url))
            {
                Logs.Error($"url cannot be Null or Empty!");
                return false;
            }

            if (string.IsNullOrEmpty(destination))
            {
                Logs.Error($"destination cannot be Null or Empty!");
                return false;
            }

            if (File.Exists(destination))
                File.Delete(destination);

            Logs.Log($"Downloading {url} to {destination}");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try { Core.webClient.DownloadFile(url, destination); }
            catch (Exception ex)
            {
                Logs.Error($"Error Downloading {url}", ex);

                if (File.Exists(destination))
                    File.Delete(destination);

                return false;
            }

            return true;
        }

        internal static bool Process(string filepath, string destination)
        {
            if (string.IsNullOrEmpty(filepath))
            {
                Logs.Error($"filepath cannot be Null or Empty!");
                return false;
            }

            if (string.IsNullOrEmpty(destination))
            {
                Logs.Error($"destination cannot be Null or Empty!");
                return false;
            }

            if (filepath.Equals(destination))
                return true;

            if (!File.Exists(filepath))
            {
                Logs.Error($"{filepath} does not Exist!");
                return false;
            }

            if (Path.HasExtension(destination))
            {
                if (File.Exists(destination))
                    File.Delete(destination);
            }
            else
            {
                if (Directory.Exists(destination))
                {
                    Logs.Log($"Cleaning {destination}");
                    foreach (var entry in Directory.EnumerateFileSystemEntries(destination))
                    {
                        if (Directory.Exists(entry))
                            Directory.Delete(entry, true);
                        else
                            File.Delete(entry);
                    }
                }
                else
                {
                    Logs.Log($"Creating Directory {destination}");
                    Directory.CreateDirectory(destination);
                }
            }

            string filename = Path.GetFileName(filepath);
            if (!filename.EndsWith(".zip"))
            {
                Logs.Log($"Moving {filepath} to {destination}");
                File.Move(filepath, destination);
                return true;
            }

            Logs.Log($"Extracting {filepath} to {destination}");
            try { ZipFile.ExtractToDirectory(filepath, destination); }
            catch (Exception ex)
            {
                Logs.Error(ex.ToString());

                if (File.Exists(filepath))
                    File.Delete(filepath);

                if (Directory.Exists(destination))
                {
                    foreach (var entry in Directory.EnumerateFileSystemEntries(destination))
                    {
                        if (Directory.Exists(entry))
                            Directory.Delete(entry, true);
                        else
                            File.Delete(entry);
                    }
                }

                return false;
            }

            if (File.Exists(filepath))
                File.Delete(filepath);

            return true;
        }
    }

    public class WSHA512
    {
        private HashAlgorithm algorithm;

        public WSHA512()
        {
            algorithm = (HashAlgorithm)CryptoConfig.CreateFromName("System.Security.Cryptography.SHA512");
            algorithm.SetHashSizeValue(512);
        }


        public byte[] ComputeHash(byte[] buffer) => algorithm.ComputeHash(buffer);
        public byte[] ComputeHash(byte[] buffer, int offset, int count) => algorithm.ComputeHash(buffer, offset, count);
        public byte[] ComputeHash(Stream inputStream) => algorithm.ComputeHash(inputStream);
    }
}
