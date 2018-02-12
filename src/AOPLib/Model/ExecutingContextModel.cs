using System.Runtime.Remoting.Messaging;

namespace AOPLib.Model
{
    public class ExecutingContextModel
    {
        public object[ ] Args { get; set; }

        public string MethodName { get; set; }

        /// <summary>
        /// 返回結果
        /// </summary>
        public object Result { get; set; }

        public ExecutingContextModel( IMethodCallMessage callMessage )
        {
            Args = callMessage.Args;
            MethodName = callMessage.MethodName;
        }
    }
}