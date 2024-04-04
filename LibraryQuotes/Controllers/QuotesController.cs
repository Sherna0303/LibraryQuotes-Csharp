using FluentValidation;
using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace LibraryQuotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotationService _quotationService;
        private readonly IQuoteListService _quoteListService;
        private readonly IBudgetService _budgetService;
        private readonly IValidator<ClientDTO> _clientValidator;
        private readonly IValidator<BudgetClientDTO> _budgetClientValidator;

        public QuotesController(IQuotationService quotationService, IQuoteListService quoteListService, IBudgetService budgetService, IValidator<ClientDTO> clientValidator, IValidator<BudgetClientDTO> budgetClientValidator)
        {
            _quotationService = quotationService;
            _quoteListService = quoteListService;
            _budgetService = budgetService;
            _clientValidator = clientValidator;
            _budgetClientValidator = budgetClientValidator;
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
        ///                 "Price": 20,
        ///                 "Type": 0
        ///              }
        ///          ]
        ///     }
        ///
        /// </remarks>
        [HttpPost("/calculateCopyPrice")]
        public async Task<IActionResult> CalculateCopyPrice(ClientDTO payload)
        {
            var validateClient = await _clientValidator.ValidateAsync(payload);

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
        public async Task<IActionResult> CalculateListCopyPrice(ClientListDTO payload)
        {
            //var validateClient = await _clientValidator.ValidateAsync(payload);

            //if (!validateClient.IsValid)
            //{
            //    return StatusCode(StatusCodes.Status400BadRequest, validateClient.Errors);
            //}

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

            return StatusCode(StatusCodes.Status200OK, _budgetService.CalculateBudget(payload));
        }
    }
}
