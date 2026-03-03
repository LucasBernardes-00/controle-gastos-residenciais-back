using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs.Request;
using WebApplication1.UseCases.Transactions;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly SaveTransactionUseCase _save;
        private readonly UpdateTransactionUseCase _update;
        private readonly DeleteTransactionUseCase _delete;
        private readonly GetAllTransactionUseCase _getAll;
        private readonly GetTransactionUseCase _get;

        public TransactionController(
            SaveTransactionUseCase save,
            UpdateTransactionUseCase update,
            DeleteTransactionUseCase delete,
            GetAllTransactionUseCase getAll,
            GetTransactionUseCase get
        )
        {
            _save = save;
            _update = update;
            _delete = delete;
            _getAll = getAll;
            _get = get;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionSaveRequest request)
        {
            var response = await _save.Execute(request);

            return Created(string.Empty, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAll.Execute();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _get.Execute(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TransactionUpdateRequest request)
        {
            await _update.Execute(id, request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _delete.Execute(id);
            return Ok();
        }
    }
}
