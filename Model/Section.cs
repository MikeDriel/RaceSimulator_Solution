using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Section
    {
        public SectionType SectionTypes { get; set; }

		public Section(SectionType sectionTypes)
		{
			SectionTypes = sectionTypes;
		}
	}

	//Enum for the different types of sections
	public enum SectionType
    {
        StartGrid,
        Straight,
        Finish,
        LeftCorner,
        RightCorner,
		StartGridV,
		StraightV,
		FinishV,
		LeftCornerV,
		RightCornerV
	}
}
