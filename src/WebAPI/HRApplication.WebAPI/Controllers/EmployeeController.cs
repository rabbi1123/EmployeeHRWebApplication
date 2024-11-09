using HRApplication.Application.DTOs;
using HRApplication.Application.Features.Employee.Requests.Commands;
using HRApplication.Application.Features.Employee.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HRApplication.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ISender sender;

        public EmployeeController(ISender sender) => this.sender = sender;

        // GET: api/<EmployeeController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var employees = await sender.Send(new GetEmployeeListRequest());
            return Ok(employees);
        }

        // GET api/<EmployeeController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var employee = await sender.Send(new GetEmployeeDetailRequest { Id = id});
            return Ok(employee);
        }

        // POST api/<EmployeeController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployeeCreateDto value)
        {
            var result = await sender.Send(new CreateEmployeeCommand { EmployeeCreateDto = value});
            return Ok(result);

        }

        // PUT api/<EmployeeController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] EmployeeDto value)
        {
            var result = await sender.Send(new UpdateEmployeeCommand { EmployeeDto = value});
            return Ok(result);
        }

        // DELETE api/<EmployeeController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await sender.Send(new DeleteEmployeeCommand { Id = id});
            return Ok();
        }
    }
}
