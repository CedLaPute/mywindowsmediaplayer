using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace MyWindowsMediaPlayer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Type { AUDIO, VIDEO, PICTURE };
        private Type typeManager;
        private double playerHeight;
        private double playerWidth;
        private List<AManager> manager = new List<AManager>();
        private List<APlaylist> defaultPlaylist = new List<APlaylist>();
        private MyXmlSerializer xs = new MyXmlSerializer();
        private DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.manager.Add(new SongManager(this.player));
            this.MyListBox.ItemsSource = Datas;
            this.defaultPlaylist.Add(xs.MyDeserialize("../../Resources/AudioPlaylist"));
            this.defaultPlaylist.Add(xs.MyDeserialize("../../Resources/VideoPlaylist"));
            this.defaultPlaylist.Add(xs.MyDeserialize("../../Resources/ImagePlaylist"));
            this.typeManager = Type.AUDIO;
            foreach (var audio in this.defaultPlaylist[0].returnItemList())
            {
                Console.WriteLine(audio.Path);
                this.manager[0].Add(audio.Path);
            }
            this.Audio.Click += new RoutedEventHandler(button_Audio_Click);
            this.Video.Click += new RoutedEventHandler(button_Video_Click);
            this.Picture.Click += new RoutedEventHandler(button_Picture_Click);
            this.button_add.Click += new RoutedEventHandler(button_add_Click);
            this.button_back.Click += new RoutedEventHandler(button_back_Click);
            this.button_forward.Click += new RoutedEventHandler(button_forward_Click);
            this.button_play.Click += new RoutedEventHandler(button_play_Click);
            this.button_stop.Click += new RoutedEventHandler(button_stop_Click);
            this.button_volume_up.Click += new RoutedEventHandler(button_volume_up_Click);
            this.button_volume_down.Click += new RoutedEventHandler(button_volume_down_Click);
            this.timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            this.timer.Tick += timer_Tick;
            this.timer.Start();
            this.MouseMove += Window_MouseMove;
            this.playerWidth = this.player.Width;
            this.playerHeight = this.player.Height;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Audio.Visibility = Visibility.Collapsed;
            this.Picture.Visibility = Visibility.Collapsed;
            this.Video.Visibility = Visibility.Collapsed;
            this.button_add.Visibility = Visibility.Collapsed;
            this.button_back.Visibility = Visibility.Collapsed;
            this.button_forward.Visibility = Visibility.Collapsed;
            this.button_play.Visibility = Visibility.Collapsed;
            this.button_stop.Visibility = Visibility.Collapsed;
            this.button_volume_up.Visibility = Visibility.Collapsed;
            this.button_volume_down.Visibility = Visibility.Collapsed;
            this.button_pause.Visibility = Visibility.Collapsed;
            this.MyListBox.Visibility = Visibility.Collapsed;
            this.MyScrollViewer.Visibility = Visibility.Collapsed;
            this.slider.Visibility = Visibility.Collapsed;
/*            this.player.Width = this.Width - 10;
            this.player.Height = this.Height - 10;*/
            this.timer.Stop();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            this.Audio.Visibility = Visibility.Visible;
            this.Video.Visibility = Visibility.Visible;
            this.Picture.Visibility = Visibility.Visible;
            this.button_add.Visibility = Visibility.Visible;
            this.button_back.Visibility = Visibility.Visible;
            this.button_forward.Visibility = Visibility.Visible;
            this.button_play.Visibility = Visibility.Visible;
            this.button_stop.Visibility = Visibility.Visible;
            this.button_volume_up.Visibility = Visibility.Visible;
            this.button_volume_down.Visibility = Visibility.Visible;
            this.button_pause.Visibility = Visibility.Visible;
            this.MyListBox.Visibility = Visibility.Visible;
            this.MyScrollViewer.Visibility = Visibility.Visible;
            this.slider.Visibility = Visibility.Visible;
            /*            this.player.Width = this.playerWidth;
                        this.player.Height = this.playerHeight;*/
            this.timer.Stop();
            this.timer.Start();
        }

        void button_Audio_Click(object s, RoutedEventArgs e)
        {
            this.slider.Opacity = 1;
            this.manager.RemoveAt(0);
            this.manager.Add(new SongManager(this.player));
            this.typeManager = Type.AUDIO;
            this.defaultPlaylist[1].serialize();
            this.defaultPlaylist[2].serialize();
            foreach (var audio in this.defaultPlaylist[0].returnItemList())
            {
                this.manager[0].Add(audio.Path);
            }
            this.MyListBox.ItemsSource = Datas;
        }

        void    button_Video_Click(object s, RoutedEventArgs e)
        {
            this.slider.Opacity = 1;
            this.manager.RemoveAt(0);
            this.manager.Add(new VideoManager(this.player));
            this.typeManager = Type.VIDEO;
            this.defaultPlaylist[0].serialize();
            this.defaultPlaylist[2].serialize();
            foreach (var video in this.defaultPlaylist[1].returnItemList())
            {
                this.manager[0].Add(video.Path);
            }
            this.MyListBox.ItemsSource = Datas;
        }

        void    button_Picture_Click(object s, RoutedEventArgs e)
        {
            this.slider.Opacity = 0;
            this.manager.RemoveAt(0);
            this.manager.Add(new ImageManager(this.player));
            this.typeManager = Type.PICTURE;
            this.defaultPlaylist[0].serialize();
            this.defaultPlaylist[1].serialize();
            foreach (var picture in this.defaultPlaylist[2].returnItemList())
            {
                this.manager[0].Add(picture.Path);
            }
            this.MyListBox.ItemsSource = Datas;
        }

        void    button_add_Click(object s, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
            if (this.typeManager == Type.AUDIO)
            {
                dialog.FileName = "Music";
                dialog.DefaultExt = ".mp3";
                dialog.Filter = "MP3|*.mp3";
            }
            else if (this.typeManager == Type.VIDEO)
            {
                dialog.FileName = "Videos";
                dialog.Filter = "AVI|*.avi|WMV|*.wmv|MKV|*.mkv";
            }
            else if (this.typeManager == Type.PICTURE)
            {
                dialog.FileName = "Images";
                dialog.DefaultExt = ".JPG";
                dialog.Filter = "JPG|*.jpg|PNG|*.png";
            }

            Nullable<bool> res = dialog.ShowDialog();

            if (res == true)
            {
                this.manager[0].Add(dialog.FileName);
                switch (this.typeManager)
                {
                    case Type.AUDIO:
                        this.defaultPlaylist[0].addItem(new Item(System.IO.Path.GetFileNameWithoutExtension(dialog.FileName), dialog.FileName));
                        break;
                    case Type.VIDEO:
                        this.defaultPlaylist[1].addItem(new Item(System.IO.Path.GetFileNameWithoutExtension(dialog.FileName), dialog.FileName));
                        break;
                    case Type.PICTURE:
                        this.defaultPlaylist[2].addItem(new Item(System.IO.Path.GetFileNameWithoutExtension(dialog.FileName), dialog.FileName));
                        break;
                }
                this.MyListBox.ItemsSource = Datas;
            }
        }

        void    button_back_Click(object s, RoutedEventArgs e)
        {
            this.manager[0].Prev();
        }

        void    button_forward_Click(object s, RoutedEventArgs e)
        {
            this.manager[0].Next();
        }

        void    button_play_Click(object s, RoutedEventArgs e)
        {
            this.manager[0].Play();
        }

        void    button_stop_Click(object s, RoutedEventArgs e)
        {
            this.manager[0].Stop();
        }

        void button_volume_up_Click(object s, RoutedEventArgs e)
        {
            this.manager[0].VolumeUp();
        }

        void button_volume_down_Click(object s, RoutedEventArgs e)
        {
            this.manager[0].VolumeDown();
        }
        public ObservableCollection<Item> Datas
        {
            get { return this.manager[0].Datas(); }
        }
    }
}
