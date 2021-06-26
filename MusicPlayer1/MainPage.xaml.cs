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
        private ObservableCollection<MenuItem> MenuItemList1; //observablecollection is used instead of List to make it dynamic

       
        public MainPage() 
        {
            this.InitializeComponent();
            music = new ObservableCollection<Music>();
            MusicManager.GetMusics(music);

            var playLists= PlayListManager.GetAllPlayLists(); //gets all playLists and stores it in variable playList

            MenuItemList1 = new ObservableCollection<MenuItem>()
            {
                new MenuItem
                {
                    menuListCategory = "My Library",
                    IconFile= "Assets/Icons/AllSongsIcon.png"
                }
            };

            foreach (var playList in playLists)
            {
                var menuItem = new MenuItem
                {
                    menuListCategory = playList.Name,
                    IconFile = "Assets/Icons/PlayListIcon.png"
                };

                MenuItemList1.Add(menuItem);
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySpiltView.IsPaneOpen = !MySpiltView.IsPaneOpen;
        }

        private void MenuItemsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menuItem = (MenuItem)e.ClickedItem;
            CategoryTextBlock.Text = menuItem.menuListCategory;

            if(menuItem.menuListCategory=="My Library")
            {
                MusicManager.GetMusics(music);
            }
            else
            {
                var playList = new PlayList(menuItem.menuListCategory);
                music.Clear();
                foreach(var song in playList.songs)
                {
                    music.Add(song); //since musiclistview binds to music, adding songs in music, to display songs in playlist
                }
            }
            
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
                var menuItem = new MenuItem
                {
                    menuListCategory = playListName,
                    IconFile = "Assets/Icons/PlayListIcon.png"
                };

                bool isFound = false;
                foreach (var m in MenuItemList1)
                {
                    if (playListName == m.menuListCategory)
                    {
                        isFound = true;
                        break;
                    }
                }

                if(isFound == false)
                {
                    MenuItemList1.Add(menuItem);
                }
            }
        }

        private void SaveDetails_Click(object sender, RoutedEventArgs e)
        {
            string DetailsText = Details.Text;
            MusicManager.SaveDetails(DetailsText, CurrentMusic.SongName);
        }
    }
}
