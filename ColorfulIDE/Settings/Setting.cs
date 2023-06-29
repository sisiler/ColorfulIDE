﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace HiTeam.ColorfulIDE.Settings
{
    public class Setting
    {
        private const string Configfile = "config.txt";

        public Setting()
        {
            //var assemblylocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            IsDirectory = true;
            BackgroundImageFileAbsolutePath = Environment.CurrentDirectory + @"\vsixicon.png";
            BackgroundImageAbsolutePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            Opacity = 0.35;
            PositionHorizon = PositionH.Right;
            PositionVertical = PositionV.Bottom;
            Interval = 60000;
            OpacityInterval = 200;
            ChangeBackgroundColor = false;
            AutoResize = true;
            RandomSequence = true;
        }

        public bool IsDirectory { get; set; }
        public string BackgroundImageFileAbsolutePath { get; set; }
        public double Opacity { get; set; }
        public string BackgroundImageAbsolutePath { get; set; }
        public PositionV PositionVertical { get; set; }
        public PositionH PositionHorizon { get; set; }
        public double Interval { get; set; }
        public double OpacityInterval { get; set; }
        public bool ChangeBackgroundColor { get; set; }
        public bool AutoResize { get; set; }
        public bool RandomSequence { get; set; }

        public void Serialize()
        {
            var config = JsonSerializer<Setting>.Serialize(this);

            var assemblylocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var configpath = Path.Combine(string.IsNullOrEmpty(assemblylocation) ? "" : assemblylocation, Configfile);

            using (var s = new StreamWriter(configpath, false, Encoding.ASCII))
            {
                s.Write(config);
            }
        }

        public static Setting Deserialize()
        {
            var assemblylocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var configpath = Path.Combine(string.IsNullOrEmpty(assemblylocation) ? "" : assemblylocation, Configfile);
            string config;

            using (var s = new StreamReader(configpath, Encoding.ASCII, false))
            {
                config = s.ReadToEnd();
            }
            var ret = JsonSerializer<Setting>.DeSerialize(config);
            ret.BackgroundImageAbsolutePath = ToFullPath(ret.BackgroundImageAbsolutePath);
            return ret;
        }

        public static string ToFullPath(string path)
        {
            var assemblylocation = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(string.IsNullOrEmpty(assemblylocation) ? "" : assemblylocation, path);
            }
            return path;
        }
    }

    [CLSCompliant(false), ComVisible(true)]
    [Guid("12d9a45f-ec0b-4a96-88dc-b0cba1f4789a")]
    public enum PositionV
    {
        Top,
        Bottom,
        Center
    }

    [CLSCompliant(false), ComVisible(true)]
    [Guid("8b2e3ece-fbf7-43ba-b369-3463726b828d")]
    public enum PositionH
    {
        Left,
        Right,
        Center
    }
}
