using AnimatedGif;
using Microsoft.Extensions.Logging;
using System.Threading;
using System;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace WinGif
{
    internal class CaptureWindowService : ICaptureWindowService
    {
        private readonly ILogger<CaptureWindowService> _logger;
        private AnimatedGifCreator _creator = null;
        private bool _capturing = true;
        private int _frames = 0;

        public CaptureWindowService(ILogger<CaptureWindowService> logger)
        {
            _logger = logger;
        }

        public void StartCapture(ICaptureWindowParameters parameters)
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
                        if (!parameters.SingleWindow && text.Contains(Program.Title) && !parameters.AllowSelfCapture)
                        {
                            Thread.Sleep(parameters.CaptureDelay);
                            continue;
                        }

                        var bitmap = NativeMethods.CaptureActiveWindow(
                            new NativeMethods.Rect() { 
                                Top = parameters.CropTop,
                                Bottom = parameters.CropBottom,
                                Left = parameters.CropLeft,
                                Right = parameters.CropRight
                            });
                        
                        if (parameters.IsGrayScale)
                            bitmap = ToGrayScale(bitmap);

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

        private static Bitmap ToGrayScale(Bitmap c)
        {
            var d = new Bitmap(c.Width, c.Height);

            for (int i = 0; i < c.Width; i++)
            {
                for (int x = 0; x < c.Height; x++)
                {
                    Color oc = c.GetPixel(i, x);
                    int grayScale = (int)((oc.R * 0.3) + (oc.G * 0.59) + (oc.B * 0.11));
                    Color nc = Color.FromArgb(oc.A, grayScale, grayScale, grayScale);
                    d.SetPixel(i, x, nc);
                }
            }

            return d;
        }
    }
}
