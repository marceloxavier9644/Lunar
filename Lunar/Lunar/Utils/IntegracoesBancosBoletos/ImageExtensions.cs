using System.Drawing;
using System.IO;

namespace Lunar.Utils.IntegracoesBancosBoletos
{
    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                // Salva a imagem no MemoryStream
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
