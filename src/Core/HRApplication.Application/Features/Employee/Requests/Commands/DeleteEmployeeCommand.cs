using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication.Application.Features.Employee.Requests.Commands
{
    public class DeleteEmployeeCommand() : IRequest
    {
        public int Id { get; set; }
    }
}
