using AutoMapper;
using HRApplication.Application.DTOs.Validators;
using HRApplication.Application.Features.Employee.Requests.Commands;
using HRApplication.Application.Persistence.Contracts;
using HRApplication.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRApplication.Application.Features.Employee.Handlers.Commands
{
    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }
        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var validator = new EmployeeCreateValidator();
            var validationResult = await validator.ValidateAsync(request.EmployeeCreateDto);
            if (validationResult.IsValid == false)
            {
                throw new Exception();   
            }
            var employee = mapper.Map<HRApplication.Domain.Employee>(request.EmployeeCreateDto);
            employee = await employeeRepository.Add(employee);
            return employee.Id;
        }
    }
}
