using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManager
{
    public class TimeParser
    {
        public static bool TryParse(object? input, out TimeOnly result) {
            result = default;
            if (input is null) return false;

            if (input is double d) { result = TimeOnly.FromDateTime(DateTime.FromOADate(d)); return true; }

            var s = Convert.ToString(input)?.Trim();
            if (string.IsNullOrEmpty(s)) return false;

            // exact "HHmm" like 0800
            if (s.Length == 4 &&
                int.TryParse(s.AsSpan(0, 2), out var hh) &&
                int.TryParse(s.AsSpan(2, 2), out var mm))
            { result = new TimeOnly(hh, mm); return true; }

            // fallback (e.g., "8:00", "8:00 AM")
            return TimeOnly.TryParse(s, out result);

        }
    }

}
