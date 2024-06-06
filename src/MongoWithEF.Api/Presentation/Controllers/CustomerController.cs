using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoWithEF.Api.Application.Requests;
using MongoWithEF.Api.Application.Services;

namespace MongoWithEF.Api.Presentation.Controllers;

[ApiController]
[Route("api/v1/customers")]
public sealed class CustomerController(ICustomerService customerService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
        => Ok(await customerService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(ObjectId id)
    {
        try
        {
            return Ok(await customerService.GetByIdAsync(id));
        }
        catch (InvalidDataException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpGet("byName")]
    public async Task<IActionResult> GetLikeNameAsync([FromQuery] string name)
        => Ok(await customerService.GetLikeNameAsync(name));

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] AddCustomerRequest request)
    {
        try
        {
            await customerService.AddAsync(request);

            return Ok("Cliente criado com sucesso");
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(ObjectId id, [FromBody] UpdateCustomerRequest request)
    {
        try
        {
            await customerService.UpdateAsync(id, request);

            return Ok("Cliente atualizado com sucesso");
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(ObjectId id)
    {
        try
        {
            await customerService.DeleteAsync(id);

            return Ok("Cliente removido com sucesso");
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}