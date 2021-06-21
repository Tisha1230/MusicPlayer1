using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace MusicPlayer1.Model
{
    public static class MusicManager
    {
        private static string extension { get; set; }
        private static string ImageFile { get; set; }

        //Getting All Music from local drive

        public static void GetMusics(ObservableCollection<Music> music)
        {
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\Music";
            music.Clear();

            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);

                    if (extension == ".png" || extension == ".jfif")
                    {
                        ImageFile = file;
                    }
                }

                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);

                    if (!(extension == ".png" || extension == ".jfif"))
                        {
                        string MusicName = Path.GetFileNameWithoutExtension(file);

                        music.Add(new Music(file, ImageFile, MusicName));
                    }
                }
            }

        }
    }
}
