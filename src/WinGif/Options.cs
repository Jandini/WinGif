﻿using CommandLine;

namespace WinGif
{
    class Options
    {
        internal class Gif
        {
            [Option("frame-delay", HelpText = "Delay between GIF frames in milliseconds. (33ms delay between frames equals ~30fps)", Default = 1000, Required = false)]
            public int FrameDelay { get; set; }

            [Option('l', "self-capture", HelpText = "Allow self capture. By default WinGif will stop capturing when the window is active. (Require -t WinGif)", Default = false, Required = false)]
            public bool AllowSelfCapture { get; set; }
        }


        [Verb("capture", isDefault: true, HelpText = "Create animated GIF by capturing active window. Examples:\n\ncapture -s -t \" Oracle VM VirtualBox\" -o %TEMP%/vbox.gif --crop-top 89 --crop-bottom -43 --crop-left 1 --crop-right -1")]
        internal class Capture : Gif, ICaptureParameters 
        {
            [Option('t', "title", HelpText = "Case sensitive, full or partial window title. The capture will start when the window containing this title becomes active.", Required = true)]
            public string WindowCaption { get; set; }

            [Option('s', "single", HelpText = "Capture only window with given title.", Default = false, Required = false)]
            public bool SingleWindow { get; set; }

            [Option('o', "output", HelpText = "Output GIF file name.", Required = true)]
            public string OutputFile { get; set; }

            [Option('f', "output-frames", HelpText = "Save frames in given directory.", Required = false)]
            public string OutputFramesDirectory { get; set; }

            [Option("capture-delay", HelpText = "Delay between taking screen shoots in milliseconds.", Default = 1000, Required = false)]
            public int CaptureDelay { get; set; }

            [Option("crop-top", HelpText = "Positive or negative number of pixels.", Default = 0, Required = false)]
            public int CropTop { get; set; }

            [Option("crop-bottom", HelpText = "Positive or negative number of pixels.", Default = 0, Required = false)]
            public int CropBottom { get; set; }

            [Option("crop-left", HelpText = "Positive or negative number of pixels.", Default = 0, Required = false)]
            public int CropLeft { get; set; }

            [Option("crop-right", HelpText = "Positive or negative number of pixels.", Default = 0, Required = false)]
            public int CropRight { get; set; }
        }


        [Verb("make", isDefault: false, HelpText = "Make animated GIF from PNG files.")]
        internal class Make : Gif, IMakeParameters
        {
            [Option('i', "input", HelpText = "Path to directory with PNG files.", Required = true)]
            public string InputDirectory { get; set; }

            [Option('o', "output", HelpText = "Output GIF file name.", Required = true)]
            public string OutputFile { get; set; }
        }
    }
}
