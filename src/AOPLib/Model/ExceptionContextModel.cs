using System;
using System.Runtime.Remoting.Messaging;

namespace AOPLib.Model
{
    public class ExceptionContextModel
    {
        /// <summary>
        /// 傳入參數
        /// </summary>
        public object[ ] Args { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// 返回錯誤回傳值
        /// </summary>
        public object Result { get; set; }

        public ExceptionContextModel( IMethodCallMessage callMessage )
        {
            Args = callMessage.InArgs;
        }
    }
}