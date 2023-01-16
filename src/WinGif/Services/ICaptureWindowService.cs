namespace WinGif
{
    internal interface ICaptureWindowService
    {
        void StartCapture(ICaptureWindowParameters parameters);
        void StopCapture();
    }
}
