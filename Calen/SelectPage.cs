using System;
using System.Collections.Generic;

namespace Calen
{
    public class DayItems    
    {
        public static List<DayItems> days = Filecs.Deserialization<DayItems>(MainWindow.sohrnJson);
        public DateTime Day { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public string Name { get; set; }
        public string IconPath { get; set; }
        public bool IsSelected { get; set; }
        public string MusicPath { get; set; }
    }
}