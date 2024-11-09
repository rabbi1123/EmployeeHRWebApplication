using AutoMapper;
using HRApplication.Application.Features.Employee.Requests.Commands;
using HRApplication.Application.Persistence.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication.Application.Features.Employee.Handlers.Commands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await employeeRepository.Get(request.EmployeeDto.Id);
            mapper.Map(request.EmployeeDto, employee);
            await employeeRepository.Update(employee);
            return Unit.Value;
        }
    }
}
