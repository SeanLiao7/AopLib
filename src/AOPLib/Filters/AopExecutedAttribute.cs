using System;
using AOPLib.Model;

namespace AOPLib.Filters
{
    public abstract class AopExecutedAttribute : Attribute, IExecuteFilter
    {
        /// <summary>
        /// Called when [executed].
        /// </summary>
        /// <param name="excuteContext">The excute context.</param>
        public abstract void OnExecuted( ExecutedContextModel excuteContext );

        /// <summary>
        /// Called when [executing].
        /// </summary>
        /// <param name="executingContext">The executing context.</param>
        public abstract void OnExecuting( ExecutingContextModel executingContext );
    }
}