﻿/***************************************************************************
 *   FileManager.cs
 *   Part of UltimaXNA: http://code.google.com/p/ultimaxna
 *   Based on code from UltimaSDK: http://ultimasdk.codeplex.com/
 *   
 *   begin                : May 31, 2009
 *   email                : poplicola@ultimaxna.com
 *
 ***************************************************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 3 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
#region usings
using System;
using System.IO;
using Microsoft.Win32;
#endregion

namespace UltimaXNA.Data
{
    class FileManager
    {
        private static string m_FileDirectory;

        static FileManager()
        {
            // Fix to address different base client install paths -BERT
            string basePath = @"SOFTWARE\Origin Worlds Online\Ultima Online\";

            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(basePath);

            registryKey = Registry.LocalMachine.OpenSubKey(basePath + registryKey.GetSubKeyNames()[0]); // assumes only one subkey for the basePath

            string exePath = registryKey.GetValue("ExePath") as string;

            if (exePath != null)
                m_FileDirectory = Path.GetDirectoryName(exePath);
        }

        public static string GetFilePath(string name)
        {
            name = Path.Combine(m_FileDirectory, name);
            // Fix for opening files which don't exist -Smjert
            if (File.Exists(name))
                return name;

            return null;
        }

        public static bool Exists(string name)
        {
            try
            {
                name = Path.Combine(m_FileDirectory, name);

                if (File.Exists(name))
                {
                    return true;
                }

                return false;
            }
            catch { return false; }
        }

        public static bool Exists(string name, int index)
        {
            return Exists(String.Format(name, index));
        }

        public static bool Exists(string name, int index, string type)
        {
            return Exists(String.Format("{0}{1}.{2}", name, index, type));
        }

        public static bool Exists(string name, string type)
        {
            return Exists(String.Format("{0}.{1}", name, type));
        }

        public static FileStream GetFile(string name)
        {
            try
            {
                name = Path.Combine(m_FileDirectory, name);

                return new FileStream(name, FileMode.Open, FileAccess.Read, FileShare.Read);
            }
            catch { return null; }
        }

        public static FileStream GetFile(string name, int index)
        {
            return GetFile(String.Format(name, index));
        }

        public static FileStream GetFile(string name, int index, string type)
        {
            return GetFile(String.Format("{0}{1}.{2}", name, index, type));
        }

        public static FileStream GetFile(string name, string type)
        {
            return GetFile(String.Format("{0}.{1}", name, type));
        }
    }
}