using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OnlineFoodDelivery.Exceptions;

namespace OnlineFoodDelivery.Aspect
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var message = exception.Message;

            context.Result = exception switch
            {
                CartItemNotFoundException => new OkObjectResult(message),
                CartEmptyException => new OkObjectResult(message),
                RestaurantMismatchException => new OkObjectResult(message),
                OrderNotFoundException => new OkObjectResult(message),
                InvalidOperationException => new OkObjectResult(message),
                _ => new OkObjectResult("An unexpected error occurred: " + message)
            };

            context.ExceptionHandled = true;
        }
    }
}
