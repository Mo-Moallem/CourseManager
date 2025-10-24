using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager
{
    public class Building
    {
        private int buildingNo;
        private Point point;
        public Building(int buildingNo, Point point)
        {
            this.buildingNo = buildingNo;
            this.point = point;
        }
    }
}
