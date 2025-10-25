using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager
{
    public class Building
    {
        private string? buildingNo;
        private Point point;
        public Building(string? buildingNo)
        {
            this.buildingNo = buildingNo;
            this.point = point;
        }

        internal string? GetBuildingNo()
        {
            return this.buildingNo;
        }

        private Point GetPoint(int buildingNo)
        {
            return new Point(0,0);
        }
    }
}
