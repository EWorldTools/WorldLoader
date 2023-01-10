using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Il2CppInterop.Json
{
    public class Deobb
    {
        public string AssemblyName { get; set; }
        public string? AssemblyFile { get; set; }
        public string? Type { get; set; }
        public List<string>? WithMethods { get; set; } = new List<string>();
        public List<string>? WithOutMethods { get; set; } = new List<string>();
        public List<string>? Properties { get; set; } = new List<string>();
        public List<string>? WithFields { get; set; } = new List<string>();
        public List<string>? WithOutFields { get; set; } = new List<string>();
        public List<string>? WithProperties { get; set; } = new List<string>();
        public List<string>? WithOutProperties { get; set; } = new List<string>();
    }
}
