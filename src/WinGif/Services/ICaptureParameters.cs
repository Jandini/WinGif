namespace WinGif
{
    public interface ICaptureParameters
    {
        public string WindowCaption { get; }
        public bool SingleWindow { get; }
        public string OutputFile { get; }
        public string OutputFramesDirectory { get; }
        public int CaptureDelay { get; }
        public int FrameDelay { get; }
        public bool AllowSelfCapture { get; }

    }
}