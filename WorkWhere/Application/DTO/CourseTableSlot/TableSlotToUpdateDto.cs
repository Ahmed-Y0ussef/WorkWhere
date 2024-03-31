using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.CourseTableSlot
{
    public class TableSlotToUpdateDto
    {

        public string[]? Dates { get; set; }

        public string? startHour { get; set; }

        public string? endHour { get; set; }

        private bool _updateDates = false;  // Use a private backing field

        public bool UpdateDates => _updateDates;

        public void SetUpdateDatesFlag()
        {
            _updateDates = Dates != null && Dates.Length > 0;
        }


    }
}
