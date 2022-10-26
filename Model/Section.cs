using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Section
    {
        public SectionTypes SectionType { get; set; }

		public Section(SectionTypes sectionTypes)
		{
			SectionType = sectionTypes;
		}
	}

	//Enum for the different types of sections
	public enum SectionTypes
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
