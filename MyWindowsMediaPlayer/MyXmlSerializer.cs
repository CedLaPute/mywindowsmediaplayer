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
        
        public void MySerialize(String nameOfXmlFile)
        {
            Playlist p = new Playlist();
            using (StreamWriter wr = new StreamWriter(nameOfXmlFile))
            {
                _xs.Serialize(wr, p.returnListItem());
            }
        }
    }
}
