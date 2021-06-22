using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MusicPlayer1.Model
{
    public class PlayList
    {
        public List<Music> songs;
        public string Name;
        private string PlayListFileLocation;

        public PlayList(string name)
        {
            this.Name = name;
            string root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string playListDirectory = $@"{root}\Assets\PlayLists";
            if (Directory.Exists(playListDirectory))
            {
                PlayListFileLocation = $@"{playListDirectory}\{name}.json";
                if (File.Exists(PlayListFileLocation))
                {
                    string fileText = System.IO.File.ReadAllText(PlayListFileLocation);
                    this.songs = JsonConvert.DeserializeObject<List<Music>>(fileText);
                }
                else
                {
                    this.songs = new List<Music>();
                }
            }
            else
            {
                Directory.CreateDirectory(playListDirectory);
                this.songs = new List<Music>();
            }
        }


        public void Add(Music music)
        {
            songs.Add(music);
            string text = JsonConvert.SerializeObject(songs);
            System.IO.File.WriteAllText(PlayListFileLocation, text);
        }
    }
}
