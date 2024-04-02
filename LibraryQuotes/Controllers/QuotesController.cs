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

        public QuotesController(IQuotationService quotationService, IQuoteListService quoteListService)
        {
            _quotationService = quotationService;
            _quoteListService = quoteListService;
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
    }
}
