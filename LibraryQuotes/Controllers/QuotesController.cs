using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryQuotes.Controllers
{
    [ApiController]
    [Route("/api")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotationService _quotationService;
        private readonly IQuoteListService _quoteListService;
        private readonly IBudgetService _budgetService;

        public QuotesController(IQuotationService quotationService, IQuoteListService quoteListService, IBudgetService budgetService)
        {
            _quotationService = quotationService;
            _quoteListService = quoteListService;
            _budgetService = budgetService;
        }

        [HttpPost("/calculateCopyPrice")]
        public async Task<IActionResult> CalculateCopyPrice(ClientDTO payload)
        {
            return StatusCode(StatusCodes.Status200OK, _quotationService.CalculatePrice(payload));
        }

        [HttpPost("/calculateListCopyPrice")]
        public async Task<IActionResult> CalculateListCopyPrice(ClientDTO payload)
        {
            return StatusCode(StatusCodes.Status200OK, _quoteListService.CalculatePriceListCopies(payload));
        }

        [HttpPost("/calculateBudget")]
        public async Task<IActionResult> CalculateBudget(BudgetClientDTO payload)
        {
            return StatusCode(StatusCodes.Status200OK, _budgetService.CalculateBudget(payload));
        }
    }
}
