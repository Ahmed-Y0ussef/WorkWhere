using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers.ResponseResults
{
    public enum ManageCoursesResults
    {

        Success,
        InvalidModelState,
        CourseNotFound,
        AllreadyApproved,
        CannotReject,
        Error
    }
}
