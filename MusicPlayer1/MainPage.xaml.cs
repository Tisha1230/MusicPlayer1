using MusicPlayer1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MusicPlayer1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Music> music;
        public string ImageFile { get; set; }
        public string SongName { get; set; }
        private Music CurrentMusic { get; set; }
        private List<MenuItem> MenuItemListView1;

       
        public MainPage() 
        {
            this.InitializeComponent();
            music = new ObservableCollection<Music>();
            MusicManager.GetMusics(music);

            MenuItemListView1 = new List<MenuItem>()
            {
                new MenuItem
                {
                    menuListCategory = MenuList.MyLibrary,
                    IconFile= "Assets/Icons/AllSongsIcon.png"
                },

                new MenuItem
                {
                    menuListCategory = MenuList.Playlists,
                    IconFile= "Assets/Icons/PlayListIcon.png"
                },
            };
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySpiltView.IsPaneOpen = !MySpiltView.IsPaneOpen;
        }

        private void MenuItemsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = (MenuItem)e.ClickedItem;
            CategoryTextBlock.Text = menuItem.menuListCategory.ToString();
        }


        private void MusicListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Music userClickedItem = (Music)e.ClickedItem;
            CurrentMusic = userClickedItem;
            string extension = Path.GetExtension(userClickedItem.FileName);
            if (extension ==".mp3")
            {
                MyMediaElement.Source = new Uri(this.BaseUri, userClickedItem.FileName);
                MyMediaElement.Play();
                NowPlaying.Text = userClickedItem.SongName;

                /* to set the Image.source property in code requires an instance of BitmapImage*/
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = userClickedItem.ImageFile;
                CoverImage.Source = bitmapImage;

                //calling ReadDetails to set text, if nothting exixts, it will clear text
                Details.Text =  MusicManager.ReadDetails(CurrentMusic.SongName);

            }


        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            MyMediaElement.Pause();
        }

        private void AddToList_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            MyMediaElement.Stop();
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            MyMediaElement.Play();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentMusic != null)
            {
                string playListName = PlayListName.Text;
                var playList = new PlayList(playListName);
                playList.Add(CurrentMusic);
            }
        }

        private void SaveDetails_Click(object sender, RoutedEventArgs e)
        {
            string DetailsText = Details.Text;
            MusicManager.SaveDetails(DetailsText, CurrentMusic.SongName);
        }
    }
}
