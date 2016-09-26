using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DirectiveGenerator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Parameter checkng
            if (args.Count() != 2)
                WaitAndExit("Improper usage! Expected parameters: <image_one_path> <image_two_path>\n" +
                            "Try dragging two image files directly on top of the application!");

            // Image checking
            Bitmap b1 = null, b2 = null;

            try
            {
                b1 = new Bitmap(args[0]);
            }

            catch (ArgumentException)
            {
                WaitAndExit("The first file ({0}) is not a valid image or does not exist.", args[0]);
            }

            try
            {
                b2 = new Bitmap(args[1]);
            }
            catch (ArgumentException)
            {
                WaitAndExit("The second file \"{0}\" is not a valid image or does not exist.", args[1]);
            }
            
            if (!b1.Size.Equals(b2.Size))
                WaitAndExit("Image resolutions do not match.\n\"{0}\": {1}\n\"{2}\": {3}\nThat'd make comparing colors quite difficult!", args[0], b1.Size, args[1], b2.Size);

            // Conversion order
            while (true)
            {
                Console.WriteLine("Convert \"{0}\" to \"{1}\"?", args[0], args[1]);
                Console.WriteLine("Y: Start. N: Switch order. ESC: Abort.");

                ConsoleKeyInfo cki = Console.ReadKey();
                Console.WriteLine();

                if (cki.Key == ConsoleKey.Escape)
                    WaitAndExit();
                else if (cki.Key == ConsoleKey.Y)
                    break;
                else if (cki.Key == ConsoleKey.N)
                {
                    string temp = args[0];
                    args[0] = args[1];
                    args[1] = temp;
                    break;
                }
            }

            // Comparing images
            Dictionary<Color, Color> conversions = new Dictionary<Color, Color>();

            Console.WriteLine("Starting comparison from \"{0}\" to \"{1}\".", args[0], args[1]);

            for (int y = b1.Height - 1; y >= 0; y--)
            {
                for (int x = b1.Width - 1; x >= 0; x--)
                {
                    Color cFrom = b1.GetPixel(x, y),
                        cTo = b2.GetPixel(x, y);

                    if (!cFrom.Equals(cTo))
                        conversions[cFrom] = cTo;
                }
            }

            // Copy results to clipboard
            Clipboard.SetText(CreateDirectives(conversions));
            WaitAndExit("Directives copied to clipboard!");
        }

        static void WaitAndExit(string message = null, params object[] args)
        {
            if (!string.IsNullOrEmpty(message))
                Console.WriteLine(message, args);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Environment.Exit(0);
        }

        static string ColorToString(Color c)
        {
            string  r = c.R.ToString("X2"),
                    g = c.G.ToString("X2"),
                    b = c.B.ToString("X2"),
                    a = c.A.ToString("X2");

            return (r + g + b + a).ToLower();
        }

        static string CreateDirectives(Dictionary<Color, Color> conversions)
        {
            string directives = "?replace";

            foreach (KeyValuePair<Color, Color> conversion in conversions)
            {
                directives += string.Format(";{0}={1}", ColorToString(conversion.Key), ColorToString(conversion.Value));
            }

            Console.WriteLine(directives);
            return directives;
        }
    }
}
