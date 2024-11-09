using HRApplication.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication.Application.Features.Employee.Requests.Queries
{
    public class GetEmployeeDetailRequest() : IRequest<EmployeeDto>
    {
        public int Id { get; set; }
    }
}
