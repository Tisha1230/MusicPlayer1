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
       
        public string AudioFile { get; set; }
        public Uri ImageFile { get; set; }
        
        public Music (string fileName,string imageFile, string songName)
        {
            // AudioFile = $@"C:\";

            FileName = fileName;
            SongName = songName;

            /* the image should change in every song if the imageFile of that songName is found
             * if the imageFile is not found then assigning it the default music icon
             */

            if (imageFile == null)
            {
                string root = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;
                ImageFile = new Uri($"{root}/Assets/Icons/MusicIcon.png", UriKind.Absolute); //Getting the ImageFile from an absolute path where user has stored it. C:\Users\tisha\Desktop\Bootcamp\MusicPlayer1\MusicPlayer1\bin\x86\Debug\AppX\Assets\Music
            }
            else
            {
                ImageFile = new Uri(imageFile, UriKind.Absolute);
            }

            
        }
    }
}
