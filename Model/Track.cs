using System;
using System.Collections.Generic;
using System.Text;
using static Model.Section;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
        public LinkedList<Section> Sections = new LinkedList<Section>();

        public Track(string name, SectionTypes[] sectionTypes)
        {
            this.Name = name;
            this.Sections = setSections(sectionTypes);
        }

        public LinkedList<Section> setSections(SectionTypes[] sectionTypes)
        {
            LinkedList<Section> sections = new LinkedList<Section>();
            for (int i = 0; i < sectionTypes.Length; i++)
            {
                sections.AddLast(new Section(sectionTypes[i]));
            }

            return sections;
        }
    }
}
