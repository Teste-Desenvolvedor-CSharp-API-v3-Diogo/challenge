using MediatR;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.CreateTransaction;
using Questao5.Application.Queries.GetAccountBalance;
using Questao5.Presentation.SwaggerExamples;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Questao5.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IMediator mediator;

    public TransactionController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria uma nova transação bancária")]
    [SwaggerRequestExample(typeof(CreateTransactionCommand), typeof(CreateTransactionCommandExample))]

    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await mediator.Send(command);
            return Ok(new { transactionId = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("balance/{accountNumber}")]
    public async Task<IActionResult> GetBalance(string accountNumber)
    {
        var result = await mediator.Send(new GetAccountBalanceQuery(accountNumber));
        return Ok(result);
    }
}
