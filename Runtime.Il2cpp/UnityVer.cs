using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WorldLoader.DataClasses
{
    public class UnityVer
    {
        public UnityVer() {
            var ver = Application.unityVersion;
            ver = ver.Remove(ver.IndexOf('f'));
            var split = ver.Split('.');
            int.TryParse(split[0].Replace(".", string.Empty), out var major);
            int.TryParse(split[1].Replace(".", string.Empty), out var minor);
            int.TryParse(split[2].Replace(".", string.Empty), out var build);
            Major = major;
            Minor = minor;
            Build = build;
            version = new Version(Major, Minor, Build);
        }

        public Version version { get; private set; }

        public int Major { get; private set; }
        public int Minor { get; private set; }
        public int Build { get; private set; }
    }
}
