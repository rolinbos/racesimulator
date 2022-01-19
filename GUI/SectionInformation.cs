using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Model;
using RaceSimulator;

namespace GUI
{
    public class SectionInformation
    {
        public Direction Direction { get; set; }
        public Section Section { get; set; }
        public SectionTypes SectionType { get; set; }
        public int row { get; set; }
        public int col { get; set; }
        public Bitmap Bitmap { get; set; }

        public SectionInformation(int row, int col, Section section, Direction direction, Bitmap bitmap)
        {
            this.Direction = direction;
            this.Section = section;
            this.row = row;
            this.col = col;
            this.SectionType = section.SectionType;
            this.Bitmap = bitmap;
        }
    }
}
