using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    internal class SubjectBook : ATable
    {
        public string Name { get; set; }
        public int Year { get; set; }

    }
}
