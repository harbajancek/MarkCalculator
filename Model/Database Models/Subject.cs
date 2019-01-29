using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Model
{
    internal class Subject : ATable
    {
        [Indexed]
        public int SubjectBookID { get; set; }
        public string Name { get; set; }
    }
}
