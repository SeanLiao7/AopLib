using System;
using AOPLib.Model;

namespace AOPLib.Filters
{
    public abstract class AopExceptionAttribute : Attribute, IExceptionFilter
    {
        /// <summary>
        /// Called when [exception].
        /// </summary>
        /// <param name="exceptionContext">The exception context.</param>
        public abstract void OnException( ExceptionContextModel exceptionContext );
    }
}