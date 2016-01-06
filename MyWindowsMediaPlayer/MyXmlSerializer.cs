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
                String[] SplitName = nameOfXmlFile.Split(new string[] { "../../" }, StringSplitOptions.None);
                Console.WriteLine(SplitName[1]);
                XDocument rd = XDocument.Parse(SplitName[1]);

                IEnumerable<Item> IEitems = from i in rd.Descendants("Item")
                                            select new Item()
                                            {
                                                Title = (String)i.Attribute("Title"),
                                                Path = (String)i.Attribute("Path")
                                            };
                items = IEitems.ToList<Item>();
                Console.WriteLine(items.Count.ToString());
                p.addPlaylist(items);
                return p;
            }
            catch
            {
                using (StreamReader rd = new StreamReader(nameOfXmlFile))
                {
                    items = _xs.Deserialize(rd) as List<Item>;
                    if (items.Count > 0)
                        p.addPlaylist(items);
                }
                return p;
            };
        }
    }
}
