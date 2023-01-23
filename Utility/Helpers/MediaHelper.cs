using Microsoft.AspNetCore.Http;
using SelectPdf;
using SkiaSharp;
using System;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Utility
{
    public class MediaHelper
    {
        public static void SaveImage(ref dynamic model, string path, string resolution)
        {
            if (model.Image != null && model.Image.Length > 0)
            {
                var fileName = SaveImageToFile(model.Image, "/" + path, resolution);
                if (!string.IsNullOrEmpty(fileName))
                    model.ImageName = fileName;
            }
        }
        public static string SaveImageToFile(IFormFile file, string shortPath, string resolution = "")
        {
            string filename = string.Empty;
            string[] metaData = file.FileName.Split(".");
            if (metaData.Length > 1)
            {
                filename = Guid.NewGuid() + "." + metaData[metaData.Length - 1];
                var directory = Directory.GetCurrentDirectory();
                var actualPath = directory + shortPath + filename;

                //new feature for direcotry creation, if not exists
                if (!new FileInfo(directory + shortPath).Exists)
                {
                    new DirectoryInfo(directory + shortPath).Create();
                }
                using (FileStream fs = File.Create(actualPath))
                {
                    file.CopyTo(fs);
                }

                int width = 0;
                int height = 0;
                if (!string.IsNullOrEmpty(resolution))
                {
                    var arrResolution = resolution.Split("*");
                    if (arrResolution.Length == 2)
                    {
                        int.TryParse(arrResolution[0], out width);
                        int.TryParse(arrResolution[1], out height);
                    }
                }

                var resizedPath = directory + shortPath + @"\ResizedImage\" + filename;
                if (!new FileInfo(directory + shortPath + @"\ResizedImage\").Exists)
                {
                    new DirectoryInfo(directory + shortPath + @"\ResizedImage\").Create();
                }
                if (width > 0 && height > 0)
                {
                    using var image = SKBitmap.Decode(actualPath);
                    var codec = SKCodec.Create(actualPath);
                    var format = codec.EncodedFormat;
                    var pictureBinary = ImageResize(image, format, width, height);
                    File.WriteAllBytesAsync(resizedPath, pictureBinary);
                }
                else
                {
                    using (FileStream fs = System.IO.File.Create(resizedPath))
                    {
                        file.CopyTo(fs);
                    }
                }
            }

            return filename;
        }
        protected static byte[] ImageResize(SKBitmap image, SKEncodedImageFormat format, int targetSize)
        {
            if (image == null)
                throw new ArgumentNullException("Image is null");

            float width, height;
            if (image.Height > image.Width)
            {
                // portrait
                width = image.Width * (targetSize / (float)image.Height);
                height = targetSize;
            }
            else
            {
                // landscape or square
                width = targetSize;
                height = image.Height * (targetSize / (float)image.Width);
            }

            if ((int)width == 0 || (int)height == 0)
            {
                width = image.Width;
                height = image.Height;
            }
            try
            {
                using var resizedBitmap = image.Resize(new SKImageInfo((int)width, (int)height), SKFilterQuality.Medium);
                using var cropImage = SKImage.FromBitmap(resizedBitmap);

                //In order to exclude saving pictures in low quality at the time of installation, we will set the value of this parameter to 80 (as by default)
                return cropImage.Encode(format, 80).ToArray();
            }
            catch
            {
                return image.Bytes;
            }
        }
        protected static byte[] ImageResize(SKBitmap image, SKEncodedImageFormat format, int width, int height)
        {
            if (image == null)
                throw new ArgumentNullException("Image is null");

            if ((int)width == 0 || (int)height == 0)
            {
                width = image.Width;
                height = image.Height;
            }
            try
            {
                using var resizedBitmap = image.Resize(new SKImageInfo((int)width, (int)height), SKFilterQuality.Medium);
                using var cropImage = SKImage.FromBitmap(resizedBitmap);

                //In order to exclude saving pictures in low quality at the time of installation, we will set the value of this parameter to 80 (as by default)
                return cropImage.Encode(format, 80).ToArray();
            }
            catch
            {
                return image.Bytes;
            }
        }
        public static async Task<String> ImageToBase64(string filepath)
        {
            var directory = Directory.GetCurrentDirectory();
            var serverpath = Path.Combine(directory, filepath);
            if (System.IO.File.Exists(serverpath))
            {
                var contents = await System.IO.File.ReadAllBytesAsync(serverpath);
                return $"data:image/png;base64,{Convert.ToBase64String(contents)}";
            }
            else
            {
                return "image file not found";
            }
        }

        public static string HtmlToImage(string htmlContent, string filepath)
        {
            string imageName = string.Empty;
            try
            {
                int webPageWidth = 1024;
                int webPageHeight = 0;

                // instantiate a html to image converter object
                HtmlToImage imgConverter = new HtmlToImage();

                // set converter options
                imgConverter.WebPageWidth = webPageWidth;
                imgConverter.WebPageHeight = webPageHeight;

                // create a new image converting an url
                System.Drawing.Image image = imgConverter.ConvertHtmlString(htmlContent);

                // save image
                imageName = Guid.NewGuid() + "." + ImageFormat.Png.ToString();
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), filepath);
                imagePath = imagePath + "/" + imageName;
                image.Save(imagePath);

                var imageResizePath = Path.Combine(Directory.GetCurrentDirectory(), filepath + @"\ResizedImage\");
                imageResizePath = imageResizePath + "/" + imageName;
                image.Save(imageResizePath);
            }
            catch { }

            return imageName;
        }
        public static string HtmlToPdf(string htmlContent, string filepath)
        {
            var url = string.Empty;
            try
            {
                // instantiate the html to pdf converter
                HtmlToPdf converter = new HtmlToPdf();

                //converter.Options.WebPageHeight = 1050;
                converter.Options.WebPageWidth = 1200;

                PdfDocument doc = converter.ConvertHtmlString(htmlContent);

                // save pdf document
                var pdfFileName = Guid.NewGuid() + ".pdf";
                var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), filepath);
                pdfPath = pdfPath + "/" + pdfFileName;
                doc.Save(pdfPath);

                // close pdf document
                doc.Close();

                url = filepath + "/" + pdfFileName;
            }
            catch { }

            return url;
        }
        public static string HtmlToPdfFront(string htmlContent, string filepath, string header)
        {
            var url = string.Empty;
            try
            {
                // instantiate the html to pdf converter
                HtmlToPdf converter = new HtmlToPdf();

                //converter.Options.WebPageHeight = 1050;
                converter.Options.WebPageWidth = 1200;
                converter.Options.MarginTop = 10;
                converter.Options.MarginBottom = 10;

                if (!string.IsNullOrEmpty(header))
                {
                    // header settings
                    converter.Options.DisplayHeader = true;
                    converter.Header.DisplayOnFirstPage = true;
                    converter.Header.DisplayOnOddPages = true;
                    converter.Header.DisplayOnEvenPages = true;
                    converter.Header.Height = 50;

                    // add some html content to the header
                    PdfHtmlSection headerHtml = new PdfHtmlSection(header, "");
                    headerHtml.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;
                    converter.Header.Add(headerHtml);
                }

                PdfDocument doc = converter.ConvertHtmlString(htmlContent);

                // save pdf document
                var pdfFileName = Guid.NewGuid() + ".pdf";
                var pdfPath = Path.Combine(Directory.GetCurrentDirectory(), filepath);
                pdfPath = pdfPath + "/" + pdfFileName;
                doc.Save(pdfPath);

                // close pdf document
                doc.Close();

                url = filepath + "/" + pdfFileName;
            }
            catch (Exception ex)
            {
            }

            return url;
        }
    }
}
