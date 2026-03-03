using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs.Request;
using WebApplication1.UseCases.Categories;
using WebApplication1.UseCases.Report;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly SaveCategoryUseCase _save;
        private readonly UpdateCategoryUseCase _update;
        private readonly DeleteCategoryUseCase _delete;
        private readonly GetAllCategoryUseCase _getAll;
        private readonly GetCategoryUseCase _get;
        private readonly CategoryTransactionReportUseCase _reportTransaction;

        public CategoryController(
            SaveCategoryUseCase saveCategoryUseCase,
            UpdateCategoryUseCase updateCategoryUseCase,
            DeleteCategoryUseCase deleteCategoryUseCase,
            GetAllCategoryUseCase getAllCategoryUseCase,
            GetCategoryUseCase getCategoryUseCase,
            CategoryTransactionReportUseCase reportTransaction
        )
        {
            _save = saveCategoryUseCase;
            _update = updateCategoryUseCase;
            _delete = deleteCategoryUseCase;
            _getAll = getAllCategoryUseCase;
            _get = getCategoryUseCase;
            _reportTransaction = reportTransaction;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategorySaveRequest category)
        {
            var response = await _save.Execute(category);

            return Created(string.Empty, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAll.Execute();
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var result = await _get.Execute(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoryUpdateRequest request)
        {
            var result = await _update.Execute(id, request);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _delete.Execute(id);
            return Ok();
        }

        [HttpGet("report-transaction")]
        public async Task<IActionResult> GetReportTransaction()
        {
            var result = await _reportTransaction.Execute();
            return Ok(result);
        }
    }
}
