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
        public List<Music> songs; // list of songs of datatype Music
        public string Name; //name of playlist
        private string PlayListFileLocation; //location where playlist is saved

        public PlayList(string name) //constructor with name of playlist to be defined
        {
            this.Name = name;

            var localAppData = Environment.SpecialFolder.LocalApplicationData; //specialFolder is an enum that has special folder locations. LocalApplicationData is one of those special folders where you can write.

            string root = Environment.GetFolderPath(localAppData);// converting a special folder enum to a string path , the result is this C:\Users\tisha\AppData\Local\Packages\48ce0e45-e2e4-4d79-bb60-6cff185029e1_nv98t81mhz8jj\LocalState

            string playListDirectory = $@"{root}\Assets\PlayLists";

            if (Directory.Exists(playListDirectory))
            {
                PlayListFileLocation = $@"{playListDirectory}\{name}.json";
                if (File.Exists(PlayListFileLocation))
                {
                    //read the file
                    string fileText = System.IO.File.ReadAllText(PlayListFileLocation);

                    //converting text into list of music using JSON
                    this.songs = JsonConvert.DeserializeObject<List<Music>>(fileText);
                }
                else //if file doesn't exist it creates a song object so that null reference exception doesn't happen
                {
                    this.songs = new List<Music>();
                }
            }
            else
            {
                Directory.CreateDirectory(playListDirectory);
                this.songs = new List<Music>(); //creates a song object so that null reference exception doesn't happen
            }
        }


        public void Add(Music music) //method of playlist to add music in the playlist
        {
            songs.Add(music); //adding music to list- songs

            string text = JsonConvert.SerializeObject(songs); // converts list- songs into text using JSON

            System.IO.File.WriteAllText(PlayListFileLocation, text); //overwriting the playlist file, so that it can be loaded when opened later.
        }
       
        //TODO: Need to add delete
    }
}
