using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    private readonly AppDbContext _context;

    public BuggyController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFoundRequest()
    {
        var thing = _context.Products.Find(42);

        if (thing == null)
        {
            var statusCode = NotFound().StatusCode;
            return NotFound(new ApiResponse(statusCode));
        }

        return Ok();
    }

    [HttpGet("servererror")]
    public ActionResult GetServerError()
    {
        var thing = _context.Products.Find(42);

        var thingToReturn = thing.ToString();

        return Ok();
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        var statusCode = BadRequest().StatusCode;
        return BadRequest(new ApiResponse(statusCode));
    }

    [HttpGet("badrequest/{id}")]
    public ActionResult GetBadRequest(int id)
    {
        return Ok();
    }

    [HttpGet("math")]
    public ActionResult GetDivideByZero()
    {
        throw new DivideByZeroException();
    }
}
