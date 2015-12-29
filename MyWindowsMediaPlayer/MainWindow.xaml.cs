using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MyWindowsMediaPlayer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Type { AUDIO, VIDEO, PICTURE };
        private Type typeManager;
        private List<AManager> manager = new List<AManager>();

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.manager.Add(new SongManager(this.player));
            this.typeManager = Type.AUDIO;
            this.Audio.Click += new RoutedEventHandler(button_Audio_Click);
            this.Video.Click += new RoutedEventHandler(button_Video_Click);
            this.Picture.Click += new RoutedEventHandler(button_Picture_Click);
            this.button_add.Click += new RoutedEventHandler(button_add_Click);
            this.button_back.Click += new RoutedEventHandler(button_back_Click);
            this.button_forward.Click += new RoutedEventHandler(button_forward_Click);
            this.button_play.Click += new RoutedEventHandler(button_play_Click);
            this.button_repeat.Click += new RoutedEventHandler(button_repeat_Click);
            this.button_shuffle.Click += new RoutedEventHandler(button_shuffle_Click);
            this.button_stop.Click += new RoutedEventHandler(button_stop_Click);
        }

        void    button_Audio_Click(object s, RoutedEventArgs e)
        {
            this.manager.RemoveAt(0);
            this.manager.Add(new SongManager(this.player));
            this.typeManager = Type.AUDIO;
        }

        void    button_Video_Click(object s, RoutedEventArgs e)
        {
            this.manager.RemoveAt(0);
            this.manager.Add(new VideoManager(this.player));
            this.typeManager = Type.VIDEO;
        }

        void    button_Picture_Click(object s, RoutedEventArgs e)
        {
        }

        void    button_add_Click(object s, RoutedEventArgs e)
        {
            //boite de dialogue a rajouter
            if (this.typeManager == Type.AUDIO)
                this.manager[0].Add(@"C:/Users/héhéhéhéhéhéhéhé/Music/Flatbush Zombies - Palm Trees Music Video (Prod. By The Architect).mp3");
            else if (this.typeManager == Type.VIDEO)
                this.manager[0].Add(@"C:/Users/héhéhéhéhéhéhéhé/Videos/Faune.wmv");
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

        void    button_repeat_Click(object s, RoutedEventArgs e)
        {
            this.manager[0].Repeat();
        }

        void    button_shuffle_Click(object s, RoutedEventArgs e)
        {
            this.manager[0].Shuffle();
        }

        void    button_stop_Click(object s, RoutedEventArgs e)
        {
            this.manager[0].Stop();
        }

        public ObservableCollection<Item> Datas
        {
            get { return this.manager[0].Datas(); }
        }
    }
}
