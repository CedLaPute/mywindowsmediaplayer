using System;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;

namespace MyWindowsMediaPlayer
{
    public class MyXmlSerializer
    {
        private XmlSerializer _xs;

        public MyXmlSerializer()
        {
            _xs = new XmlSerializer(typeof(List<Item>));
        }
        
        public void MySerialize(String nameOfXmlFile, List<Item> p)
        {
            using (StreamWriter wr = new StreamWriter(nameOfXmlFile))
            {
                _xs.Serialize(wr, p);
            }
        }

        public Playlist MyDeserialize(String nameOfXmlFile)
        {
            Playlist p = new Playlist(nameOfXmlFile);
            List<Item> items = new List<Item>();

            if (File.Exists(nameOfXmlFile))
            {
                using (StreamReader rd = new StreamReader(nameOfXmlFile))
                {
                    items = _xs.Deserialize(rd) as List<Item>;
                    if (items.Count > 0)
                        p.addPlaylist(items);
                }
            }
            return p;
        }
    }
}
