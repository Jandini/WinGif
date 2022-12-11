using Microsoft.Extensions.Logging;

namespace WinGif
{
    internal class MakeService : IMakeService
    {
        private readonly ILogger<MakeService> _logger;

        public MakeService(ILogger<MakeService> logger)
        {
            _logger = logger;
        }

        public void Make(IMakeParameters parameters)
        {
            throw new System.NotImplementedException();
        }
    }
}
