using FluentValidation;
using LibraryQuotes.Models.DTOS.Budget;
using LibraryQuotes.Models.DTOS.Quoation;
using LibraryQuotes.Models.DTOS.QuoteList;
using LibraryQuotes.Services.Interfaces;
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
        private readonly IValidator<CopyDTO> _copyValidator;
        private readonly IValidator<ClientListAndAmountDTO> _clientValidator;
        private readonly IValidator<BudgetClientDTO> _budgetClientValidator;

        public QuotesController(IQuotationService quotationService, IQuoteListService quoteListService, IBudgetService budgetService, IValidator<CopyDTO> copyValidator, IValidator<ClientListAndAmountDTO> clientValidator, IValidator<BudgetClientDTO> budgetClientValidator)
        {
            _quotationService = quotationService;
            _quoteListService = quoteListService;
            _budgetService = budgetService;
            _copyValidator = copyValidator;
            _clientValidator = clientValidator;
            _budgetClientValidator = budgetClientValidator;
        }

        /// <summary>
        /// Calculate the price of a single copy
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///         POST /calculateCopyPrice
        ///         {
        ///            "Name": "Libro",
        ///            "Author": "AutorLibro",
        ///            "Price": 20,
        ///            "Type": 0
        ///         }
        ///
        /// </remarks>
        [HttpPost("/calculateCopyPrice")]
        public async Task<IActionResult> CalculateCopyPrice(CopyDTO payload)
        {
            var validateClient = await _copyValidator.ValidateAsync(payload);

            if (!validateClient.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validateClient.Errors);
            }

            var result = await _quotationService.CalculatePrice(payload);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { Error = "Copy cannot be created" });
            }

            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Calculate the price of a list of copies
        /// </summary>
        [HttpPost("/calculateListCopyPrice")]
        public async Task<IActionResult> CalculateListCopyPrice(ClientListAndAmountDTO payload)
        {
            var validateClient = await _clientValidator.ValidateAsync(payload);

            if (!validateClient.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validateClient.Errors);
            }

            return StatusCode(StatusCodes.Status200OK, _quoteListService.CalculatePriceListCopiesAndConvertToClientDTO(payload));
        }

        /// <summary>
        /// Calculate the number of copies to buy with a budget
        /// </summary>
        [HttpPost("/calculateBudget")]
        public async Task<IActionResult> CalculateBudget(BudgetClientDTO payload)
        {
            var validateBudget = await _budgetClientValidator.ValidateAsync(payload);

            if (!validateBudget.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest, validateBudget.Errors);
            }

            return StatusCode(StatusCodes.Status200OK, _budgetService.CalculateBudgetAndConvertToClientDTO(payload));
        }
    }
}
