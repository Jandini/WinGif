namespace WinGif
{
    internal interface ICaptureService
    {
        void StartCapture(ICaptureParameters parameters);
        void StopCapture();
    }
}
