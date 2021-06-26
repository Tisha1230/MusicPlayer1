using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer1.Model
{
    public static class PlayListManager
    {
        public static List<PlayList> GetAllPlayLists()
        {
            var result = new List<PlayList>(); //empty list created

            //path of the playlist
            var localAppData = Environment.SpecialFolder.LocalApplicationData; //specialFolder is an enum that has special folder locations. LocalApplicationData is one of those special folders where you can write.

            string root = Environment.GetFolderPath(localAppData);// converting a special folder enum to a string path , the result is this C:\Users\tisha\AppData\Local\Packages\48ce0e45-e2e4-4d79-bb60-6cff185029e1_nv98t81mhz8jj\LocalState

            string playListDirectory = $@"{root}\Assets\PlayLists";


            if (Directory.Exists(playListDirectory))
            {
                string[] files = Directory.GetFiles(playListDirectory, "*.json", SearchOption.AllDirectories); //loading all json files if the directory exist in an array of string 

                foreach (string file in files)
                {
                    string playListName = Path.GetFileNameWithoutExtension(file); //getting playlist name
                    var playList = new PlayList(playListName); //created PlayList object
                    result.Add(playList); //adding the playlistname to a list- result
                    
                }
            }

            return result;
        }

    }
}
