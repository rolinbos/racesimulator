using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public enum SectionTypes
    {
        Straight,
        LeftCorner,
        RightCorner,
        StartGrid,
        Finish,
        Grass
    }

    public class Section
    {
        public SectionTypes SectionType { get; set; }

        public Section(SectionTypes sectionType)
        {
            this.SectionType = sectionType;
        }
    }
}
