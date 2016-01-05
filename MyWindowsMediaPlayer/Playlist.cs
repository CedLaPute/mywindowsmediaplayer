using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer
{
    class Playlist
    {
        private List<Item> playlist = new List<Item>();

        public Playlist()
        {
            this.playlist.Add(new Item("Test1", "Test1.2"));
            this.playlist.Add(new Item("Test2", "Test2.2"));
            this.playlist.Add(new Item("Test3", "Test3.2"));
            this.playlist.Add(new Item("Test4", "Test4.2"));
            this.playlist.Add(new Item("Test5", "Test5.2"));
        }

        public List<Item>   returnListItem()
        {
            return this.playlist;
        }
    }
}
