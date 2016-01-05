using System;

namespace MyWindowsMediaPlayer
{
    public class Item
    {
        public string Title { get; set; }
        public string Path { get; set; }

        public Item(string title, string path)
        {
            this.Title = title;
            this.Path = path;
        }

        public Item()
        {
            this.Title = "Test1";
            this.Path = "Test2";
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
