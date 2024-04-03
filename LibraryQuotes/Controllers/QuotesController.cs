using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryQuotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        /// <summary>
        /// Calculate the price of a single copy
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     POST /calculateCopyPrice
        ///     {
        ///         "AntiquityYears":0,
        ///         "Copies": [
        ///             {
        ///                 "Name": "Libro",
        ///                 "Author": "AutorLibro",
        ///                  "Price": 20,
        ///                 "Type": 0
        ///              }
        ///          ]
        ///     }
        ///
        /// </remarks>
        [HttpPost("/calculateCopyPrice")]
        public async Task<IActionResult> CalculateCopyPrice(ClientDTO payload)
        {
            return StatusCode(StatusCodes.Status200OK, _quotationService.CalculatePrice(payload));
        }

        /// <summary>
        /// Calculate the price of a list of copies
        /// </summary>
        [HttpPost("/calculateListCopyPrice")]
        public async Task<IActionResult> CalculateListCopyPrice(ClientDTO payload)
        {
            return StatusCode(StatusCodes.Status200OK, _quoteListService.CalculatePriceListCopies(payload));
        }

        /// <summary>
        /// Calculate the number of copies to buy with a budget
        /// </summary>
        [HttpPost("/calculateBudget")]
        public async Task<IActionResult> CalculateBudget(BudgetClientDTO payload)
        {
            return StatusCode(StatusCodes.Status200OK, _budgetService.CalculateBudget(payload));
        }
    }
}
