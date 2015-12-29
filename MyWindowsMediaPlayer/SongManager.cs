using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace MyWindowsMediaPlayer
{
    public class SongManager : AManager
    {
        private bool    playing = false;
        private bool    paused = false;
        private bool    repeat = false;
        private bool    shuffle = false;
        private ObservableCollection<Item>      datas;
        private MediaElement                    player;
        private int     index = 0;

        public SongManager(MediaElement me)
        {
            this.datas = new ObservableCollection<Item>();
            this.player = me;
        }

        public override ObservableCollection<Item> Datas()
        {
            return this.datas;
        }

        public override void Add(string path)
        {
            if (System.IO.File.Exists(path))
            {
                this.datas.Add(new Item(System.IO.Path.GetFileNameWithoutExtension(path), path)); 
            }
        }

        public override void Delete()
        {
            if (this.datas.Count > this.index)
            {
                this.datas.RemoveAt(this.index);
            }
        }

        public override void Play()
        {
            if (!this.paused && this.playing)
            {
                this.Pause();
            }
            else
            {
                if (!this.paused && this.datas.Count > this.index)
                {
                    Item toPlay = this.datas[this.index];
                    this.player.Source = new Uri(toPlay.Path);
                }
                this.player.Play();
                this.paused = false;
                this.playing = true;
            }
        }

        public override void Pause()
        {
            this.paused = true;
            this.playing = false;
            this.player.Pause();
        }

        public override void Stop()
        {
            this.player.Stop();
            this.player.Position = new TimeSpan(0);
            this.playing = false;
            this.paused = false;
        }

        public override void Next()
        {
            this.Stop();
            this.index++;
            if (this.index >= this.datas.Count)
            {
                this.index = 0;
            }
            if (this.playing)
            {
                this.Play();
            }
        }

        public override void Prev()
        {
            this.Stop();
            this.index--;
            if (this.index < 0)
            {
                this.index = this.datas.Count - 1;
            }
            if (this.playing)
            {
                this.Play();
            }
        }

        public override void Repeat()
        {
            this.repeat = !this.repeat;
        }

        public override void Shuffle()
        {
            this.shuffle = !this.shuffle;
        }
    }
}
