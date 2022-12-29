using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;

namespace ModernGUI.Shared
{
    public static class ImageTools
    {
        /// <summary>
        /// Get image from embedded resource in the given assembly
        /// </summary>
        /// <param name="resourceName">resouce name</param>
        /// <returns>embedded image</returns>
        public static Image GetImageFromEmbeddedResource(string resourceName)
        {
            Stream stream = Assembly.GetCallingAssembly().GetManifestResourceStream(resourceName);
            return new Bitmap(stream);
        }

        /// <summary>
        /// create a thumbnail from the original image
        /// </summary>
        /// <param name="originalImage">the original image from which the thumbnail is created</param>
        /// <param name="imageHeight">the height of the thumbnail</param>
        /// <returns></returns>
        public static Image CreateThumbnail(Image originalImage, int imageHeight)
        {
            // create thumbnail
            float ratio = (float)originalImage.Width / originalImage.Height;
            int imageWidth = (int)(imageHeight * ratio);

            // set the thumbnail image
            Image thumbnailImage = originalImage.GetThumbnailImage(imageWidth, imageHeight,
                new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);

            return thumbnailImage;
        }

        /// <summary>
        /// Required, but not used
        /// </summary>
        /// <returns>true</returns>
        public static bool ThumbnailCallback()
        {
            return true;
        }

        /// <summary>
        /// calculate the correct rectangle according to the image size and targetArea.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="targetArea"></param>
        /// <returns></returns>
        public static Rectangle ScaleToFit(Image image, Rectangle targetArea, bool strechToFit)
        {
            Rectangle result;
            if (image.Width < targetArea.Width && image.Height < targetArea.Height)
            {
                if (strechToFit)
                {
                    float widthRatio = (float)targetArea.Width / (float)image.Width;
                    float heightRatio = (float)targetArea.Height / (float)image.Height;
                    float minRatio = Math.Min(widthRatio, heightRatio);
                    result = new Rectangle(targetArea.X, targetArea.Y, (int)(image.Width * minRatio), (int)(image.Height * minRatio));
                    if (result.Width < targetArea.Width)
                    {
                        result.X += (targetArea.Width - result.Width) / 2;
                    }
                    if (result.Height < targetArea.Height)
                    {
                        result.Y += (targetArea.Height - result.Height) / 2;
                    }
                }
                else
                {
                    // the image size is less than the targetArea size
                    result = new Rectangle(targetArea.Location, image.Size);
                    result.X += (targetArea.Width - result.Width) / 2;
                    result.Y += (targetArea.Height - result.Height) / 2;
                }
            }
            else    // the width or height of the image is greater than the targetArea
            {
                result = new Rectangle(targetArea.Location, targetArea.Size);
                // determine best fit: width or height
                if (image.Width * result.Height > image.Height * result.Width)
                {
                    // final width should match target, determine and center height
                    result.Height = result.Width * image.Height / image.Width;
                    result.Y += (targetArea.Height - result.Height) / 2;
                }
                else
                {
                    // final height should match target, determine and center width
                    result.Width = result.Height * image.Width / image.Height;
                    result.X += (targetArea.Width - result.Width) / 2;
                }
            }

            return result;
        }

        /// <summary>
        /// Get image from embedded resource in excuting assembly
        /// </summary>
        /// <param name="resourceName">resouce name</param>
        /// <returns>embedded image</returns>
        public static Image GetImageFromScalablePictureBoxEmbeddedResource(string resourceName)
        {
            return new Bitmap(GetEmbeddedResourceStream(resourceName));
        }

        /// <summary>
        /// Get embedded resource stream
        /// </summary>
        /// <param name="resourceName">resource name</param>
        /// <returns>the stream of embedded resource</returns>
        private static Stream GetEmbeddedResourceStream(string resourceName)
        {
            return System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }

        private static string FilterFromImageFormatEnum(System.Drawing.Imaging.ImageFormat format)
        {

            if (format == System.Drawing.Imaging.ImageFormat.Bmp)
            {
                return "Bitmap files (*.bmp)|*.bmp";
            }
            else if (format == System.Drawing.Imaging.ImageFormat.Gif)
            {
                return "GIF files (*.gif)|*.gif";
            }
            else if (format == System.Drawing.Imaging.ImageFormat.Jpeg)
            {
                return "JPG files (*.jpg)|*.jpg";
            }
            else if (format == System.Drawing.Imaging.ImageFormat.Png)
            {
                return "PNG files (*.png)|*.png";
            }
            else if (format == System.Drawing.Imaging.ImageFormat.Tiff)
            {
                return "TIF files (*.tif)|*.tif";
            }
            else
            {
                return "";
            }


        }
        /// <summary>
        /// Save image in a number of different formats
        /// </summary>
        /// <param name="Picture">Input image</param>
        public static void SaveImage(Image Picture, System.Drawing.Imaging.ImageFormat format = null)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (format != null)
            {
                dialog.Filter = FilterFromImageFormatEnum(format);

            }
            else
            {
                dialog.Filter = "Bitmap files (*.bmp)|*.bmp|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|PNG files (*.png)|*.png|TIF files (*.tif)|*.tif";
            }
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;
            dialog.OverwritePrompt = true;
            dialog.ShowHelp = true;
            dialog.AddExtension = true;



            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (Path.GetExtension(dialog.FileName).ToLower() == ".bmp")
                    Picture.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                else if (Path.GetExtension(dialog.FileName).ToLower() == ".jpg")
                    Picture.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                else if (Path.GetExtension(dialog.FileName).ToLower() == ".gif")
                    Picture.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Gif);
                else if (Path.GetExtension(dialog.FileName).ToLower() == ".png")
                    Picture.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                else if (Path.GetExtension(dialog.FileName).ToLower() == ".tif")
                    Picture.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Tiff);
                else
                    Picture.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        /// <summary>
        /// Creates color with corrected brightness.
        /// </summary>
        /// <param name="color">Color to correct.</param>
        /// <param name="correctionFactor">The brightness correction factor. Must be between -1 and 1. 
        /// Negative values produce darker colors.</param>
        /// <returns>
        /// Corrected <see cref="Color"/> structure.
        /// </returns>
        public static Color ChangeColorBrightness(Color color, float correctionFactor)
        {
            float red = (float)color.R;
            float green = (float)color.G;
            float blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }

        /// <summary>
        /// This changes pixels of one color with another color
        /// </summary>
        /// <param name="bmp">The image</param>
        /// <param name="InitialColor">Color you are replacing</param>
        /// <param name="newColor">the new color that replaces the old</param>
        /// <param name="tolerance">as a decimal percentage, so 1 will be 100 percent tolerance</param>
        /// <returns></returns>
        public static Bitmap ChangePixelColorOfEntireImage(Bitmap bmp, Color InitialColor, Color newColor, decimal tolerance = 0)
        {

            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                bmp.PixelFormat);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
            tolerance = tolerance * 255;
            // Set every third value to 255. A 24bpp bitmap will look red.  
            for (int i = 0; i < (bmp.Width * bmp.Height * 4); i = i + 4)
            {
                if (decimal.Zero == tolerance)
                {
                    if (InitialColor.B == rgbValues[i] && InitialColor.G == rgbValues[i + 1]
                    && InitialColor.R == rgbValues[i + 2])
                    {

                        rgbValues[i] = newColor.B;
                        rgbValues[i + 1] = newColor.G;
                        rgbValues[i + 2] = newColor.R;
                    }

                }
                else
                {
                    if (Math.Abs(rgbValues[i + 2] - InitialColor.R) <= tolerance &&
                        Math.Abs(rgbValues[i + 1] - InitialColor.G) <= tolerance &&
                        Math.Abs(rgbValues[i] - InitialColor.B) <= tolerance)
                    {
                        //  new Thread(() => { MessageBox.Show("Value:"); }).Start();
                        rgbValues[i] = newColor.B;
                        rgbValues[i + 1] = newColor.G;
                        rgbValues[i + 2] = newColor.R;
                    }
                }


            }

            // Copy the RGB values back to the bitmap
            System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

            // Unlock the bits.
            bmp.UnlockBits(bmpData);

            return bmp;
        }
        /// <summary>
        /// This changes pixels of one color with another color and shades the pixels within the tolerance with a shade of the new color.
        /// </summary>
        /// <param name="bmp">The image</param>
        /// <param name="InitialColor">Color you are replacing</param>
        /// <param name="newColor">the new color that replaces the old</param>
        /// <param name="tolerance">as a decimal percentage, so 1 will be 100 percent tolerance</param>
        /// <returns></returns>
        public static Bitmap ChangePixelColorOfEntireImageWithShading(Bitmap bmp, Color InitialColor, Color newColor, decimal tolerance = 0)
        {
            try
            {
                // Lock the bitmap's bits.  
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData bmpData =
                    bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    bmp.PixelFormat);

                // Get the address of the first line.
                IntPtr ptr = bmpData.Scan0;

                // Declare an array to hold the bytes of the bitmap.
                int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
                byte[] rgbValues = new byte[bytes];

                // Copy the RGB values into the array.
                System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
                tolerance = tolerance * 255;
                // Set every third value to 255. A 24bpp bitmap will look red.  
                for (int i = 0; i < (bmp.Width * bmp.Height * 4); i = i + 4)
                {
                    if (decimal.Zero == tolerance)
                    {
                        if (InitialColor.B == rgbValues[i] && InitialColor.G == rgbValues[i + 1]
                        && InitialColor.R == rgbValues[i + 2])
                        {

                            rgbValues[i] = newColor.B;
                            rgbValues[i + 1] = newColor.G;
                            rgbValues[i + 2] = newColor.R;
                        }

                    }
                    else
                    {
                        if (Math.Abs(rgbValues[i + 2] - InitialColor.R) <= tolerance &&
                            Math.Abs(rgbValues[i + 1] - InitialColor.G) <= tolerance &&
                            Math.Abs(rgbValues[i] - InitialColor.B) <= tolerance)
                        {
                            Color color = Color.FromArgb(rgbValues[i + 2], rgbValues[i + 1], rgbValues[i]);

                            float bright = color.GetBrightness();

                            rgbValues[i] = (byte)(newColor.B * (1 - bright));
                            rgbValues[i + 1] = (byte)(newColor.G * (1 - bright));
                            rgbValues[i + 2] = (byte)(newColor.R * (1 - bright));
                        }
                    }


                }

                // Copy the RGB values back to the bitmap
                System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

                // Unlock the bits.
                bmp.UnlockBits(bmpData);

                return bmp;
            }
            catch (Exception ex)
            {
                ModernGUI.Shared.DevTools.ErrorLog(ex, "ChangePixelColorOfEntireImageWithShading");
                throw;
            }
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }

        public static void MakeIcon(Color CrginalColor, Color NewColor, int Width, int Height)
        {
            Bitmap IconOriginal = (Bitmap)OpenImageDialoge(ImageFormat.Png);

            if (IconOriginal != null)
            {
                SaveImage(ResizeImage(ChangePixelColorOfEntireImageWithShading(IconOriginal, CrginalColor, NewColor, (decimal)0.25), Width, Height), ImageFormat.Png);
            }
        }

        public static Image OpenImageDialoge(System.Drawing.Imaging.ImageFormat format = null)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Title = "Open Image";

            if (format != null)
            {
                dialog.Filter = FilterFromImageFormatEnum(format);
            }
            else
            {
                dialog.Filter = "Bitmap files (*.bmp)|*.bmp|JPG files (*.jpg)|*.jpg|GIF files (*.gif)|*.gif|PNG files (*.png)|*.png|TIF files (*.tif)|*.tif";
            }

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return Bitmap.FromFile(dialog.FileName);
            }
            else
            {
                return null;
            }
        }
    }
}
