using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Xsl;

namespace MiddlewareTool.Utility
{
    /// <summary>
    /// ImageUtility
    /// </summary>
    public class ImageUtility
    {
        public byte[] Transparent(byte[] imgByte)
        {
            byte[] bitmapData = null;
            #region Xử lý tách background cho file hình
            using (var stream = new MemoryStream(imgByte))
            {
                var image = Image.FromStream(stream);
                Bitmap source = new Bitmap(image);
                //Bitmap source = CropWhiteSpace(source1);
                source.MakeTransparent(Color.Transparent);
                for (int x = 0; x < source.Width; x++)
                {
                    for (int y = 0; y < source.Height; y++)
                    {
                        Color currentColor = source.GetPixel(x, y);
                        if (currentColor.R >= 128 && currentColor.G >= 128 && currentColor.B >= 128)//220
                        {
                            source.SetPixel(x, y, Color.Transparent);
                        }
                    }
                }
                ImageConverter converter = new ImageConverter();
                bitmapData = (byte[])converter.ConvertTo(source, typeof(byte[]));
                source.Dispose();
                image.Dispose();
            }
            #endregion

            return bitmapData;
        }
    }
}
