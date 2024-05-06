using Microsoft.AspNetCore.Mvc;

namespace BubberBreakfast.Controllers;

public class ErrorsController() : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }

}
