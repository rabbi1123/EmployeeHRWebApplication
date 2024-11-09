using HRApplication.Application.DTOs.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication.Application.DTOs
{
    public class EmployeeDto : BaseDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Division { get; set; }
        public string? Building { get; set; }
        public string? Title { get; set; }
        public string? Room { get; set; }
    }
}
