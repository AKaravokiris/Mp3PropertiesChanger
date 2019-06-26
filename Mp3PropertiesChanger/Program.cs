using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mp3PropertiesChanger
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files =Directory.GetFiles(@"F:\" , "*.mp3", SearchOption.AllDirectories);
            Console.WriteLine(string.Format("converting {0} files",files.Count()));
            int i = 0;
            foreach (string file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string[] fileTexts = fileName.Split('-');
                string originalTitle = string.Empty;
                string originalArtist = string.Empty;

                if (fileTexts.Count() < 1)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Wrong Format {0}", fileName);
                    Console.ResetColor();
                    continue;
                }
                else if (fileTexts.Count() < 2)
                {
                    originalTitle = fileTexts[0];
                    originalArtist = "Unknown";
                }
                else
                {
                    originalTitle = fileTexts[0];
                    originalArtist = fileTexts[1];
                }


                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(fileName);
                Console.ResetColor();

                try
                {
                    var tfile = TagLib.File.Create(file);
                    tfile.Tag.Title = originalTitle.Trim();
                    tfile.Tag.AlbumArtists = new string[] { originalArtist.Trim() };

                    Console.WriteLine("Title: {0}, Artist: {1}", originalTitle.Trim(), originalArtist.Trim());
                    tfile.Save();
                    tfile.Dispose();
                    i++;

                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error on {0}", fileName);
                    Console.ResetColor();
                }


            }
            Console.WriteLine();
            Console.WriteLine("process complete. {0} of {1} files where modified",i, files.Count());
            Console.Read();




            // change title in the file
           // tfile.Tag.Title = "my new title";

        }
    }


}
