using E_CommerceAPI.Application.Abstractions.Services;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceAPI.Infrastructure.Services
{
    public class QRCodeService : IQRCodeService
    {
      

        public byte[] GenerateQRCode(string text)
        {
            QRCodeGenerator generator = new();

            QRCodeData data = generator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

            PngByteQRCode qrCode = new(data); // or SvgQRCode

            byte[] byteGtaphic = qrCode.GetGraphic(10, new byte[] { 84, 99, 71 }, new byte[] { 240, 240, 240 }); // 10: pixel size, 84,99,71: dark color, 240,240,240: light color

            return byteGtaphic;
        }
    }
}
