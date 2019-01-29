﻿using System;
using SQLite;

namespace Model
{
    internal class Mark : ATable
    {
        [Indexed]
        public int SubjectID { get; set; }
        int value;
        public int Value
        {
            get { return value; }
            set
            {
                if (value >= 1 && value <= 5)
                {
                    this.value = value;
                }
                else
                {
                    this.value = -1;
                }
            }
        }
        int weight;
        public int Weight
        {
            get { return weight; }
            set
            {
                if (value >= 1 && value <= 100)
                {
                    weight = value;
                }
                else
                {
                    weight = -1;
                }
            }
        }
    }
}