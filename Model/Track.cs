using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Track
    {
        public string Name { get; set; }
		public LinkedList<Section> Sections { get; set; }

		//Constructor for Track
		public Track(string name, SectionTypes[] sectionType)
        {
            Name = name;
			Sections = SectionTypeToLinkedList(sectionType);
		}

		//Converts the array of sectiontypes to a linkedlist of sections
		public LinkedList<Section> SectionTypeToLinkedList(SectionTypes[] sectionTypes)
        {
			LinkedList<Section> sectionList = new LinkedList<Section>();
			foreach (SectionTypes sectionType in sectionTypes)
			{
				sectionList.AddLast(new Section(sectionType));
			}
			return sectionList;
		}
	}
}
