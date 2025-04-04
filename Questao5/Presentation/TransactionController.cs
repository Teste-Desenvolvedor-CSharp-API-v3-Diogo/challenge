using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.CreateTransaction;
using Questao5.Application.Queries.GetAccountBalance;

namespace Questao5.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionController : ControllerBase
{
    private readonly CreateTransactionCommandHandler createTransactionHandler;
    private readonly GetAccountBalanceQueryHandler getAccountBalanceHandler;

    public TransactionController(
        CreateTransactionCommandHandler createTransactionHandler,
        GetAccountBalanceQueryHandler getAccountBalanceHandler)
    {
        this.createTransactionHandler = createTransactionHandler;
        this.getAccountBalanceHandler = getAccountBalanceHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
    {
        try
        {
            var result = await createTransactionHandler.Handle(command);
            return Ok(new { transactionId = result });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet("balance")]
    public async Task<IActionResult> GetAccountBalance([FromQuery] string accountNumber)
    {
        try
        {
            var query = new GetAccountBalanceQuery(accountNumber);
            var result = await getAccountBalanceHandler.Handle(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}
