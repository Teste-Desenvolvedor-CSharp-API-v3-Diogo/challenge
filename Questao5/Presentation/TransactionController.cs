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

    /// <summary>
    /// Cria uma nova transação bancária (crédito ou débito).
    /// </summary>
    /// <param name="command">Dados da transação</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Identificador da transação criada</returns>
    /// <response code="200">Transação criada com sucesso</response>
    /// <response code="400">Erro ao processar a transação</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Cria uma nova transação bancária")]
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

    /// <summary>
    /// Retorna o saldo atual da conta corrente.
    /// </summary>
    /// <param name="accountNumber">Número da conta corrente</param>
    /// <returns>Informações da conta e o saldo atual</returns>
    /// <response code="200">Saldo consultado com sucesso</response>
    /// <response code="400">Erro ao consultar saldo</response>
    [HttpGet("balance/{accountNumber}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(Summary = "Consulta o saldo da conta corrente")]
    public async Task<IActionResult> GetBalance(string accountNumber)
    {
        try
        {

            var result = await mediator.Send(new GetAccountBalanceQuery(accountNumber));
            return Ok(result);

        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                error = ex.Message
            });
        }
    }
}
