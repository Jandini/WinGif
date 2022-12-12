using AnimatedGif;
using Microsoft.Extensions.Logging;
using System.Threading;
using System;
using System.IO;
using System.Drawing.Imaging;

namespace WinGif
{
    internal class CaptureService : ICaptureService
    {
        private readonly ILogger<CaptureService> _logger;
        private AnimatedGifCreator _creator = null;
        private bool _capturing = true;
        private int _frames = 0;

        public CaptureService(ILogger<CaptureService> logger)
        {
            _logger = logger;
        }

        public void StartCapture(ICaptureParameters parameters)
        {
            _logger.LogWarning("Press {key} to stop capturing", "Ctrl+C");
            
            if (!string.IsNullOrEmpty(parameters.OutputFramesDirectory))
                Directory.CreateDirectory(parameters.OutputFramesDirectory);                   

            _creator = AnimatedGif.AnimatedGif.Create(parameters.OutputFile, parameters.FrameDelay, 0);

            bool waiting = true;
            bool matched = false;

            while (_capturing)
            {

                try
                {
                    var text = NativeMethods.GetActiveWindowText();

                    if (text.Contains(parameters.WindowCaption) || (matched && !parameters.SingleWindow))
                    {
                        // Do not capture itself
                        if (!parameters.SingleWindow && text.Contains("WinGif.exe") && !parameters.AllowSelfCapture)
                        {
                            Thread.Sleep(parameters.CaptureDelay);
                            continue;
                        }

                        var bitmap = NativeMethods.CaptureActiveWindow();

                        _logger.LogInformation("Added frame number {frame} for {text} window", ++_frames, text);
                        _creator.AddFrame(bitmap, delay: -1, quality: GifQuality.Bit8);

                        if (!string.IsNullOrEmpty(parameters.OutputFramesDirectory))
                            bitmap.Save(Path.Combine(parameters.OutputFramesDirectory, _frames.ToString().PadLeft(8, '0')) + ".png", ImageFormat.Png);

                        waiting = true;
                        matched = true;
                    }
                    else
                    {
                        if (waiting)
                            _logger.LogInformation("Waiting for {caption} window to be active", parameters.WindowCaption);

                        waiting = false;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(message: ex.Message, ex);
                }
                finally
                {
                    Thread.Sleep(parameters.CaptureDelay);
                }
            }

            _logger.LogInformation("Capture ended");
        }

        public void StopCapture()
        {
            _logger.LogWarning("Stopping capture");

            if (_creator != null)
            {
                _creator.Dispose();
                _logger.LogInformation("Window capture saved in {file}", _creator.FilePath);

                _creator = null;
            }
            
            _capturing = false;
        }
    }
}
