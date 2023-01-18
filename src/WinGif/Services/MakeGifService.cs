using Microsoft.Extensions.Logging;
using System.IO;
using System.Linq;

namespace WinGif
{
    internal class MakeGifService : IMakeGifService
    {
        private readonly ILogger<MakeGifService> _logger;

        public MakeGifService(ILogger<MakeGifService> logger)
        {
            _logger = logger;
        }

        public void Make(IMakeGifParameters parameters)
        {
            _logger.LogInformation("Making {gif} from PNG files in {directory}", parameters.OutputFile, parameters.InputDirectory);

            using (var creator = AnimatedGif.AnimatedGif.Create(parameters.OutputFile, parameters.FrameDelay, 0))
            {
                foreach (var file in Directory.GetFiles(parameters.InputDirectory, "*.png").OrderBy(a => a))
                {
                    _logger.LogInformation("Adding frame from {file}", file);
                    creator.AddFrame(file, -1, AnimatedGif.GifQuality.Bit8);
                }
            }

            _logger.LogInformation("Making animated GIF file complete.");
        }
    }
}
