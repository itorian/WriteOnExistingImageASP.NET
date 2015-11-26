using System;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace WriteOnImage
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Bitmap bitMapImage = new Bitmap(Server.MapPath("certificate-format.jpg"));
            Graphics graphicImage = Graphics.FromImage(bitMapImage);
            graphicImage.SmoothingMode = SmoothingMode.AntiAlias;

            // Student name
            string studentName = RandomString(8) + " " + RandomString(4);

            // Write text on it
            graphicImage.DrawString(studentName, new Font("Arial", 20, FontStyle.Bold), SystemBrushes.WindowText, new Point(300, 250));

            // Draw QR Code, for demo i'll be using seal.png image
            var barCodeImageSrc = new Bitmap(Server.MapPath("seal.png"));  // File source maybe generated QR Code
            graphicImage.DrawImage(barCodeImageSrc, new Point(570, 400));

            // Save Bitmap to stream
            var memStream = new MemoryStream();
            bitMapImage.Save(memStream, ImageFormat.Jpeg);

            // Save memory stream into file
            var fileName = new Random().Next().ToString();
            using (var fileStream = new FileStream(Server.MapPath("~/Certificates/") + fileName + ".jpg", FileMode.Append, FileAccess.Write))
            {
                memStream.Position = 0;
                memStream.CopyTo(fileStream);
            }

            Response.Redirect("~/Certificate.aspx?id=" + fileName);
        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}