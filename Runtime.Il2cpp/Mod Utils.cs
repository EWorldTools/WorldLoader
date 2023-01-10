using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using UnityEngine;
using Object = UnityEngine.Object;

namespace WorldLoader.HookUtils
{
    public static class ModUtils
    {
		public static string GetGameObjectPath(this Transform transform)
		{
			string path = transform.name;
            while (transform.parent != null) {
                transform = transform.parent;
                path = transform.name + "/" + path;
            }
            return path;
        }


		internal static void RunInTry(this Action action
			, string ErrorMessage = null, bool ShowError = false) {
			try {
				action();
			}
			catch (Exception e) {
				Logs.Error(ErrorMessage, e);
				if (ShowError) MessageBox.Show(e.ToString(), "Fatal Error");
			}
		}

		/// <summary>
		///  Returns a string with the color (Works With Hex)
		/// </summary>
		/// <param name="text"></param>
		/// <param name="color"></param>
		/// <returns></returns>
		public static string UnityColor(this string text, string color) =>
            $"<color={color}>{text}</color>";

        /// <summary>
        ///  Returns a string with the color
        /// </summary>
        /// <param name="text"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string UnityColor(this string text, ConsoleColor color) =>
            $"<color={ConsoleColorToHex(color)}>{text}</color>";

        public static HarmonyMethod ToNewHarmonyMethod(this MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException(nameof(methodInfo));
            return new HarmonyMethod(methodInfo);
        }

        public static string RandomString(int length, bool numbersOnly = false) {
            System.Random randomString = new System.Random();
            string element = numbersOnly ? "0123456789" : "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string((from temp in Enumerable.Repeat(element, length)
                               select temp[randomString.Next(temp.Length)]).ToArray());
        }

		private static string lastsrt;

		public static string Random(this string s, string spliter = " ", int length = 9, bool numbersOnly = false) {
			System.Random randomString = new System.Random();
			string element = numbersOnly ? "0123456789" : "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
			var randomstr = new string((from temp in Enumerable.Repeat(element, length)
										select temp[randomString.Next(temp.Length)]).ToArray());
			return s + spliter + randomstr;
		}

		internal static List<GameObject> GetChildren(this Transform transform)
		{
			List<GameObject> list = new List<GameObject>();
			for (int i = 0; i < transform.childCount; i++)
			{
				GameObject gameObject = transform.GetChild(i).gameObject;
				list.Add(gameObject);
			}
			return list;
		}


		public static string SHA256(string value) {
            HashAlgorithm hashAlgorithm = new SHA256Managed();
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(value)))
                stringBuilder.Append(b.ToString("x2"));
            
            return stringBuilder.ToString();
        }


        public static long GetByteSize(string FilePath) => 
            new System.IO.FileInfo(FilePath).Length;

        //internal static Sprite SpriteFromFile(string path) {
        //    byte[] array = File.ReadAllBytes(path);
        //    Texture2D texture2D = new Texture2D(256, 256);
        //    ImageConversion.LoadImage(texture2D, array);
        //    Rect rect = new Rect(0.0f, 0.0f, texture2D.width, texture2D.height);
        //    Vector2 vector = new Vector2(0.5f, 0.5f);
        //    Vector4 zero = Vector4.zero;
        //    return Sprite.CreateSprite_Injected(texture2D, ref rect, ref vector, 100f, 0, SpriteMeshType.Tight, ref zero, false);
        //}

        //internal static Sprite SpriteFromBytes(byte[] bytes) {
        //    Texture2D texture2D = new Texture2D(256, 256);
        //    ImageConversion.LoadImage(texture2D, bytes);
        //    Rect rect = new Rect(0.0f, 0.0f, texture2D.width, texture2D.height);
        //    Vector2 vector = new Vector2(0.5f, 0.5f);
        //    Vector4 zero = Vector4.zero;
        //    return Sprite.CreateSprite_Injected(texture2D, ref rect, ref vector, 100f, 0, SpriteMeshType.Tight, ref zero, false);
        //}

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
                return gameObject.AddComponent<T>();

            return component;
        }

        public static T GetOrAddComponent<T>(this Transform transform) where T : Component
        {
            T component = transform.GetComponent<T>();
            if (component == null)
                return transform.gameObject.AddComponent<T>();

            return component;
        }

        public static void DestroyChildren(this Transform transform, Func<Transform, bool> exclude)
        {
            for (var childcount = transform.childCount - 1; childcount >= 0; childcount--)
                if (exclude == null || exclude(transform.GetChild(childcount)))
                    UnityEngine.Object.DestroyImmediate(transform.GetChild(childcount).gameObject);
        }

        public static void DestroyChildren(this Transform transform) =>
            transform.DestroyChildren(null);

        public static List<string> LogComponets(this GameObject Obj)
        {
            List<string> list = new();
            Component[] components = Obj.GetComponents<Component>();
            for (int i = 0; i < components.Length; i++) {
				Logs.Log(components[i].ToString());

			}
			return list;
        }

        public static List<string> LogAllComponets(this GameObject Obj)
        {
            List<string> list = new();
            Component[] components = Obj.GetComponentsInChildren<Component>();
            for (int i = 0; i < components.Length; i++)
                Logs.Log(components[i].ToString());
            return list;
        }


        /// <summary>
        ///  Returns a string, Ex. "<color=#000000>InputText</color>"
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="A"></param>
        /// <returns></returns>
        public static string ConsoleColorToHex(string Text, ConsoleColor A = ConsoleColor.White)
		{

			string result;
			switch (A)
			{
				case ConsoleColor.Black:
					result = "<color=#000000>" + Text + "</color>";
					break;
				case ConsoleColor.DarkBlue:
					result = "<color=#0b0785>" + Text + "</color>";
					break;
				case ConsoleColor.DarkGreen:
					result = "<color=#00800b>" + Text + "</color>";
					break;
				case ConsoleColor.DarkCyan:
					result = "<color=#00a39b>" + Text + "</color>";
					break;
				case ConsoleColor.DarkRed:
					result = "<color=#8a0000>" + Text + "</color>";
					break;
				case ConsoleColor.DarkMagenta:
					result = "<color=#8a007c>" + Text + "</color>";
					break;
				case ConsoleColor.DarkYellow:
					result = "<color=#8a8a00>" + Text + "</color>";
					break;
				case ConsoleColor.Gray:
					result = "<color=##b5b5b5>" + Text + "</color>";
					break;
				case ConsoleColor.DarkGray:
					result = "<color=#787878>" + Text + "</color>";
					break;
				case ConsoleColor.Blue:
					result = "<color=#0400ff>" + Text + "</color>";
					break;
				case ConsoleColor.Green:
					result = "<color=#11ff00>" + Text + "</color>";
					break;
				case ConsoleColor.Cyan:
					result = "<color=#00ffea>" + Text + "</color>";
					break;
				case ConsoleColor.Red:
					result = "<color=#ff0800>" + Text + "</color>";
					break;
				case ConsoleColor.Magenta:
					result = "<color=#ff00b3>" + Text + "</color>";
					break;
				case ConsoleColor.Yellow:
					result = "<color=#fbff00>" + Text + "</color>";
					break;
				default:
					result = "<color=#ffffff>" + Text + "</color>";
					break;
			}

			return result;
		}

		/// <summary>
		///  Returns a string, Ex. "#000000"
		/// </summary>
		/// <param name="Text"></param>
		/// <param name="A"></param>
		/// <returns></returns>
		public static string ConsoleColorToHex(ConsoleColor A = ConsoleColor.White)
		{
			string result;
			switch (A)
			{
				case ConsoleColor.Black:
					result = "#000000";
					break;
				case ConsoleColor.DarkBlue:
					result = "#0b0785";
					break;
				case ConsoleColor.DarkGreen:
					result = "#00800b";
					break;
				case ConsoleColor.DarkCyan:
					result = "#00a39b";
					break;
				case ConsoleColor.DarkRed:
					result = "#8a0000";
					break;
				case ConsoleColor.DarkMagenta:
					result = "#8a007c";
					break;
				case ConsoleColor.DarkYellow:
					result = "#8a8a00";
					break;
				case ConsoleColor.Gray:
					result = "##b5b5b5";
					break;
				case ConsoleColor.DarkGray:
					result = "#787878";
					break;
				case ConsoleColor.Blue:
					result = "#0400ff";
					break;
				case ConsoleColor.Green:
					result = "#11ff00";
					break;
				case ConsoleColor.Cyan:
					result = "#00ffea";
					break;
				case ConsoleColor.Red:
					result = "#ff0800";
					break;
				case ConsoleColor.Magenta:
					result = "#ff00b3";
					break;
				case ConsoleColor.Yellow:
					result = "#fbff00";
					break;
				default:
					result = "#ffffff";
					break;
			}

			return result;
		}
	}
}
