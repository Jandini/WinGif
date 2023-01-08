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
        public int CropTop { get; }
        public int CropBottom { get; }
        public int CropLeft { get; }
        public int CropRight { get; }

    }
}