namespace WinGif
{
    public interface IMakeParameters
    {
        public string InputDirectory { get; }
        public string OutputFile { get; }
        public int FrameDelay { get; }

    }
}