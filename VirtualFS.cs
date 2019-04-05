using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ProtoPlatformFramework
{
    public class VirtualFS
    {
        public Dictionary<string, string> files = new Dictionary<string, string>();

        public VirtualFS()
        {

        }

        public void WriteToFile(string name, string content, bool append = false)
        {
            if (!FileExists(name))
                files.Add(name, content);
            else
            {
                if (append)
                {
                    string con = files[name] + content;
                    files[name] = con;
                }
                else
                {
                    files[name] = content;
                }
            }
        }

        public bool FileExists(string name)
        {
            if (files.ContainsKey(name))
            {
                return true;
            }
            return false;
        }

        public void CopyFile(string source, string destination)
        {
            WriteToFile(destination, ReadFile(source));
        }

        public void RemoveFile(string name)
        {
            if (!FileExists(name))
                throw new FileNotFoundException($"File \"{name}\" not found in virtual filesystem");
            files.Remove(name);
        }

        public string ReadFile(string name)
        {
            return FileExists(name) ? files[name] : throw new FileNotFoundException($"File \"{name}\" not found in virtual filesystem");
        }

        public string[] ReadDirectory(string path)
        {
            List<string> found = new List<string>();
            foreach (string str in files.Keys)
            {
                if (str.StartsWith(path) || str.StartsWith(path + "/"))
                {
                    found.Add(str);
                }
            }
            return found.ToArray();
        }

        public void RemoveDirectory(string path)
        {
            foreach (string str in files.Keys)
            {
                if (str.StartsWith(path) || str.StartsWith(path + "/"))
                {
                    files.Remove(str);
                }
            }
        }
    }
}