using System;

namespace MyWindowsMediaPlayer
{
    public class Item
    {
        public string Title { get; private set; }
        public string Path { get; private set; }

        public Item(string title, string path)
        {
            this.Title = title;
            this.Path = path;
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
