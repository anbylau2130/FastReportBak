using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using USP.Common;
using USP.Utility;

namespace USP.Service.Impl
{
    public class CaptchaService : ICaptchaService
    {
        private Color background = Color.LightGray;
        private Color color = Color.Black;
        private int length = 6;
        private string visualChars= "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
        private char[] chars;
        public CaptchaService(string chars, string background, string color, int length)
        {
            if (chars.Length >= 10)
            {
                this.chars = chars.ToCharArray();
            }
            else
            {
                this.chars = visualChars.ToCharArray();
            }
            this.background = ColorTranslator.FromHtml(background);
            this.color = ColorTranslator.FromHtml(color);
            this.length = length;
        }

        //Creating an array for most readable yet cryptic fonts for OCR's
        // This is entirely up to developer's discretion
        private String[] crypticFonts ={
            "Arial",
            "Verdana",
            "Comic Sans MS",
            "Impact",
            "Haettenschweiler",
            "Lucida Sans Unicode",
            "Garamond",
            "Courier New",
            "Book Antiqua",
            "Arial Narrow",
            "Estrangelo Edessa"
        };

        private int[] offsets ={
            -5,
            -4,
            -3,
            -2,
            -1,
            0,
            1,
            2,
            3,
            4,
            5
        };

        public void RenderImage(ControllerContext context)
        {
            Bitmap objBMP = new System.Drawing.Bitmap(150, 60);
            Graphics objGraphics = System.Drawing.Graphics.FromImage(objBMP);
            string captchaChars = SelectRandomWord(length);
            context.HttpContext.Session[Constants.CAPTCHA] = captchaChars;
            objGraphics.Clear(background);


            // Instantiate object of brush with black color
            SolidBrush objBrush = new SolidBrush(color);

            Font objFont = null;
            int a;
            String myFont, str;


            //Loop to write the characters on image  
            // with different fonts.
            int random;
            for (a = 0; a <= captchaChars.Length - 1; a++)
            {
                random = new Random().Next(crypticFonts.Length - a);
                myFont = crypticFonts[random];
                if (random % 2 == 0)
                {
                    objFont = new Font(myFont, 19, FontStyle.Bold | FontStyle.Italic);
                }
                else
                {
                    objFont = new Font(myFont, 17, FontStyle.Bold);
                }
                str = captchaChars.Substring(a, 1);
                objGraphics.DrawString(str, objFont, objBrush, a * 20, 20 - (offsets[random] * 2));
                objGraphics.Flush();
            }
            context.HttpContext.Response.ContentType = "image/gif";
            objBMP.Save(context.HttpContext.Response.OutputStream, ImageFormat.Gif);
            objFont.Dispose();
            objGraphics.Dispose();
            objBMP.Dispose();
        }

        private string SelectRandomWord(int numberOfChars)
        {
            StringBuilder randomBuilder = new StringBuilder();
            // pick charecters from random position
            Random randomSeed = new Random();
            for (int incr = 0; incr < numberOfChars; incr++)
            {
                randomBuilder.Append(chars[randomSeed.Next(chars.Length)].ToString());

            }

            return randomBuilder.ToString();
        }
    }
}