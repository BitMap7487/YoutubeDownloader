using System;
using System.Linq;
using System.Threading.Tasks;
using YoutubeExplode;
using YoutubeExplode.Videos.Streams;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Youtube_downloader
{
    class Program
    {
        [STAThread]
        static void Main()
        {

            Console.Title = "YT Downloader";

            Console.Clear();


            Console.Write("\n\n\n(A) Single Video Mode\n(B) Multi Video Mode\n\n> ");
            string mode = Console.ReadLine().ToUpper();

            Console.Write("\n\nCurrently we only support Video and/or Audio only.\nSelecting both will download audio and video seperately.\n");
            Console.Write("(A) Audio Only\n");
            Console.Write("(B) Video Only\n");
            Console.Write("(C) Both\n\n");
            Console.Write("> ");

            string choice = Console.ReadLine().ToUpper();

            if (choice == "A")
                choice = "Audio";
            else if (choice == "B")
                choice = "Video";
            else if (choice == "C")
                choice = "Both";
            else
                Environment.Exit(0);

            if (mode == "A")
            {

                Console.Write("\n\n\nInsert Video URL: ");
                string link = Console.ReadLine();

                Downloader.start(link, choice);
            }
            if(mode == "B")
            {

                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog.Title = "Select Link file:";

                    if(openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        string filePath = openFileDialog.FileName;

                        foreach(string line in File.ReadAllLines(filePath))
                        {

                            Downloader.start(line, choice);
                            Thread.Sleep(2000);

                        }
                    }
                }

            }

            Console.WriteLine("\n\nAll Done");

            Console.ReadLine();
            Console.Clear();
            Main();

        }

        
    }
}
