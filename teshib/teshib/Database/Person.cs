using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace teshib
{
    public class Person
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Piece { get; set; }
        public int kayitBeadsName { get; set; }
        public string Date { get; set; }
    }
}
