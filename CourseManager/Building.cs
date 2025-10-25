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
        public Building(string? buildingNo, Point point)
        {
            this.buildingNo = buildingNo;
            this.point = point;
        }

        internal string? GetBuildingNo()
        {
            return this.buildingNo;
        }

        public Point GetPoint()
        {
            return this.point;
        }
    }
}
