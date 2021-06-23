using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayer1.Model
{
    public enum MenuList
    {
        MyLibrary,
        Playlists
    }
   public class Music
    {
        public string FileName { get; set; }
        public string SongName { get; set; }
        public string ArtistName;
        public string AlbumName;
        public string AudioFile { get; set; }
        public Uri ImageFile { get; set; }
        
        public Music (string fileName,string imageFile, string songName)
        {
            // AudioFile = $@"C:\";

            FileName = fileName;
            SongName = songName;

            if (imageFile == null)
            {
                string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
                ImageFile = new Uri($"{root}/Assets/Icons/MusicIcon.png", UriKind.Absolute);
            }
            else
            {
                ImageFile = new Uri(imageFile, UriKind.Absolute);
            }

            
        }
    }
}
