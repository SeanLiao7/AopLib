using AOPLib.Filters;
using AOPLib.Model;

namespace AOPLibTests.TestFilters
{
    internal class CatchExceptionAttribute : AopExceptionAttribute
    {
        public string Message { get; set; }

        public CatchExceptionAttribute( string message )
        {
            Message = message;
        }

        public override void OnException( ExceptionContextModel exceptionContext )
        {
            exceptionContext.Result = Message;
        }
    }
}