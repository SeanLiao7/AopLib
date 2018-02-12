using AOPLib.Model;

namespace AOPLib.Filters
{
    public interface IExceptionFilter
    {
        void OnException( ExceptionContextModel exceptionContext );
    }
}