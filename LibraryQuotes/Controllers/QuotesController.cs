using LibraryQuotes.Models.DTOS;
using LibraryQuotes.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryQuotes.Controllers
{
    [ApiController]
    [Route("/api")]
    public class QuotesController : ControllerBase
    {
        private readonly IQuotesService _quotesService;

        public QuotesController(IQuotesService quotesService)
        {
            _quotesService = quotesService;
        }

        [HttpPost("/calculateCopyPrice")]
        public async Task<IActionResult> calculateCopyPrice(CopyDTO payload)
        {
            return StatusCode(StatusCodes.Status200OK, _quotesService.CalculatePrice(payload));
        }
    }
}
