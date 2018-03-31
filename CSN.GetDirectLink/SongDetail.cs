using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSN.GetDirectLink
{
    public class Utils
    {
        public const string FLAC_LOSSLESS = "Lossless";
        public const string M4A_500kbps = "500kbps";
        public const string MP3_320kbps = "320kbps";
        public const string MP3_128kbps = "128kbps";
        public const string NOT_SURE = "Not sure";
        public const string NONE = "None";
        public const string VERIFIED = "Verified";

        public static List<string> SongQuality = new List<string>(new string[] { FLAC_LOSSLESS, M4A_500kbps, MP3_320kbps, MP3_128kbps });

        public static Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        public static byte[] ReadFully(Stream input)
        {
            try
            {
                int bytesBuffer = 1024;
                byte[] buffer = new byte[bytesBuffer];
                using (MemoryStream ms = new MemoryStream())
                {
                    int readBytes;
                    while ((readBytes = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, readBytes);
                    }
                    return ms.ToArray();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

    public class SongDetail
    {
        public string FullName = string.Empty;
        public string DownloadURL = string.Empty;
        public string Quality = string.Empty;
        public string MaximumQuality = string.Empty;
        public bool VerifiedLossless = false;
        public byte[] Spectrum = null;

    }
}
