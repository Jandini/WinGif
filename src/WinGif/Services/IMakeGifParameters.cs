namespace WinGif
{
    public interface IMakeGifParameters
    {
        public string InputDirectory { get; }
        public string OutputFile { get; }
        public int FrameDelay { get; }

    }
}