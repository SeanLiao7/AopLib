using System.Runtime.Remoting.Messaging;

namespace AOPLib.Model
{
    public class ExecutedContextModel
    {
        public object[ ] Args { get; set; }

        public string MethodName { get; set; }

        /// <summary>
        /// 返回結果(如果非Null)
        /// </summary>
        public object Result { get; set; }

        public ExecutedContextModel( IMethodReturnMessage returnMethod )
        {
            Args = returnMethod.Args;
            MethodName = returnMethod.MethodName;
            Result = returnMethod.ReturnValue;
        }
    }
}