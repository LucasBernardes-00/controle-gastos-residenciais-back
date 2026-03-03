using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs.Request;
using WebApplication1.UseCases.Persons;
using WebApplication1.UseCases.Report;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly SavePersonUseCase _save;
        private readonly UpdatePersonUseCase _update;
        private readonly GetPersonUseCase _get;
        private readonly GetAllUseCase _getAll;
        private readonly DeletePersonUseCase _delete;
        private readonly PersonTransactionReportUseCase _reportTransaction;

        public PersonController(
            SavePersonUseCase savePersonUseCase,
            UpdatePersonUseCase updatePersonUseCase,
            GetPersonUseCase getPersonUseCase,
            GetAllUseCase getAllUseCase,
            DeletePersonUseCase deletePersonUseCase,
            PersonTransactionReportUseCase reportTransaction
        )
        {
            _save = savePersonUseCase;
            _update = updatePersonUseCase;
            _get = getPersonUseCase;
            _getAll = getAllUseCase;
            _delete = deletePersonUseCase;
            _reportTransaction = reportTransaction;
        }

        // Método para criar uma nova pessoa
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PersonSaveRequest request)
        {
            var response = await _save.Execute(request);
            return Created(string.Empty, response);
        }

        // Método para obter todas as pessoas
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAll.Execute();
            return Ok(result);
        }

        // Método para obter uma pessoa por ID
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _get.Execute(id);
            return Ok(result);
        }

        // Método para atualizar uma pessoa existente
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PersonUpdateRequest request)
        {
            var result = await _update.Execute(id, request);
            return Ok();
        }

        // Método para deletar uma pessoa por ID e todas as suas transações associadas
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _delete.Execute(id);
            return Ok();
        }

        // Método para buscar o relatório de transações por pessoa
        [HttpGet("report-transaction")]
        public async Task<IActionResult> GetReportTransaction()
        {
            var result = await _reportTransaction.Execute();
            return Ok(result);
        }
    }
}
