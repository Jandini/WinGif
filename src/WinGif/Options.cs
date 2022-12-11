using CommandLine;

namespace WinGif
{
    class Options
    {
        internal class Gif
        {
            [Option('d', "frame-delay", HelpText = "Delay between GIF frames in milliseconds. (33ms delay between frames equals ~30fps)", Default = 1000, Required = false)]
            public int FrameDelay { get; set; }
        }


        [Verb("capture", isDefault: true, HelpText = "Create animated GIF by capturing active window.")]
        internal class Capture : Gif, ICaptureParameters 
        {
            [Option('t', "title", HelpText = "Full or partial window title to capture.", Required = true)]
            public string WindowCaption { get; set; }

            [Option('o', "output", HelpText = "Output GIF file name.", Required = true)]
            public string OutputFile { get; set; }

            [Option('f', "output-frames", HelpText = "Save frames in given directory.", Required = false)]
            public string OutputFramesDirectory { get; set; }

            [Option('e', "capture-delay", HelpText = "Delay between taking screen shoots in milliseconds.", Default = 1000, Required = false)]
            public int CaptureDelay { get; set; }
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
