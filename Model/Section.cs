using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Section
    {
        SectionType SectionTypes { get; set; }
    }

    enum SectionType
    {
        StartGrid,
        Straight,
        Finish,
        LeftCorner,
        RightCorner
    }
}
