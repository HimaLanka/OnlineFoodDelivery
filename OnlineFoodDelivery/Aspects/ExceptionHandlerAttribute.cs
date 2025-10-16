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
            var exceptionType = context.Exception.GetType();
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

            if (exceptionType == typeof(UserAlreadyExistsException))

            {

                var result = new ConflictObjectResult(message);

                context.Result = result;

            }
            else if (exceptionType == typeof(UserNotFoundException))
            {
                var result = new NotFoundObjectResult(message);

                context.Result = result;
            }


            else

            {

                var result = new StatusCodeResult(500);

                context.Result = result;

            }

        }
    }
}
