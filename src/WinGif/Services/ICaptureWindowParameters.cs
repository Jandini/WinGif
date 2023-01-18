namespace WinGif
{
    interface ICaptureWindowParameters
    {
        string WindowCaption { get; }
        bool SingleWindow { get; }
        string OutputFile { get; }
        string OutputFramesDirectory { get; }
        int CaptureDelay { get; }
        int FrameDelay { get; }
        bool AllowSelfCapture { get; }
        int CropTop { get; }
        int CropBottom { get; }
        int CropLeft { get; }
        int CropRight { get; }
        bool IsGrayScale { get; }
    }
}