using AOPLib.Model;

namespace AOPLib.Filters
{
    public interface IExecuteFilter

    {
        void OnExecuted( ExecutedContextModel excuteContext );

        void OnExecuting( ExecutingContextModel executingContext );
    }
}