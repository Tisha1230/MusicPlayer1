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
        //Getting All Music from local drive

        public static void GetMusics(ObservableCollection<Music> music)
        {
            string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
            string path = root + @"\Assets\Music";
            music.Clear();

            string[] files = Directory.GetFiles(path, "*.mp3", SearchOption.AllDirectories);

            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    string MusicName = Path.GetFileNameWithoutExtension(file);
                    string imagePathRoot = $@"{path}\{MusicName}";
                    var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp", "svg" };
                    bool imageFound = false;
                    foreach(var filter in filters)
                    {
                        string imagePath = $"{imagePathRoot}.{filter}";
                        if (File.Exists(imagePath))
                        {
                            music.Add(new Music(file, imagePath, MusicName));
                            imageFound = true;
                            break;
                        }
                    }

                    if(imageFound == false)
                    {
                        music.Add(new Music(file, null, MusicName));
                    }

                }
            }

        }
    }
}
