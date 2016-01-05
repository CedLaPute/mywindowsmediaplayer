using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWindowsMediaPlayer
{
    public class Playlist : APlaylist
    {
        private List<Item> playlist = new List<Item>();
        private String name;

        public Playlist(String n)
        {
            this.name = n;
        }

        public override void         addItem(Item i)
        {
            this.playlist.Add(i);
        }

        public override void        addPlaylist(List<Item> items)
        {
            this.playlist = items;
        }

        public override void         serialize()
        {
            MyXmlSerializer mxs = new MyXmlSerializer();

            if (this.playlist.Count > 0)
                mxs.MySerialize(this.name, this.playlist);
        }

        public override List<Item> returnItemList()
        {
            return this.playlist;
        }
    }
}
