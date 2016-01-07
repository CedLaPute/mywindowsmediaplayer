using System;
using System.Linq;
using System.Xml.Linq;
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

            if (!File.Exists(nameOfXmlFile))
                return p;

            try
            {
                XDocument rd = XDocument.Load(nameOfXmlFile);
                IEnumerable<XElement> EItems = rd.Elements();
                foreach (var i in EItems.Elements("Item"))
                {
                    items.Add(new Item((string)i.Element("Title"), (string)i.Element("Path")));
                }
                if (items.Count > 0)
                    p.addPlaylist(items);
            }
            catch
            {
                //Emergency reading
                Console.WriteLine("error occured with LINQ, using StreamReader to open the playlists");
                using (StreamReader rd = new StreamReader(nameOfXmlFile))
                {
                    items = _xs.Deserialize(rd) as List<Item>;
                    if (items.Count > 0)
                        p.addPlaylist(items);
                }
            };
            return p;
        }
    }
}
