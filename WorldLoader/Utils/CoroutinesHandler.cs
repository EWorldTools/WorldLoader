using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldLoader.Utils
{
    public static class CoroutinesHandler
    {
        internal static List<IEnumerator> BackLog = new();

        public static void Start(this IEnumerator enumerator) { 
            if (!WorldLoader.MonoBhvMade) BackLog.Add(enumerator);
            else { 
                
            }

        }
    }
}
