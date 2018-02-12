using AOPLib.Filters;
using AOPLib.Model;

namespace AOPLibTests.TestFilters
{
    internal class LogAttribute : AopExecutedAttribute
    {
        public override void OnExecuted( ExecutedContextModel excuteContext )
        {
            throw new System.NotImplementedException( );
        }

        public override void OnExecuting( ExecutingContextModel executingContext )
        {
            throw new System.NotImplementedException( );
        }
    }
}