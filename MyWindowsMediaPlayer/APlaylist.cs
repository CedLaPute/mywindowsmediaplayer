using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyWindowsMediaPlayer
{
    public abstract class APlaylist
    {
        public abstract void addItem(Item i);
        public abstract void addPlaylist(List<Item> items);
        public abstract void serialize();
        public abstract List<Item> returnItemList();
    }
}
