using Microsoft.Extensions.Logging;

namespace WinGif
{
    internal class CaptureService : ICaptureService
    {
        private readonly ILogger<CaptureService> _logger;

        public CaptureService(ILogger<CaptureService> logger)
        {
            _logger = logger;
        }

        public void StartCapture(ICaptureParameters parameters)
        {
            throw new System.NotImplementedException();
        }

        public void StopCapture()
        {
            throw new System.NotImplementedException();
        }
    }
}
