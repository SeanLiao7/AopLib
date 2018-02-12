using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using AOPLib.Filters;
using AOPLib.Model;

namespace AOPLib
{
    internal class DynamicProxy<T> : RealProxy
        where T : MarshalByRefObject
    {
        private readonly T _target;
        private IMethodCallMessage _callMethod;
        public IMethodReturnMessage ReturnMethod { get; private set; }

        public DynamicProxy( T target )
            : base( typeof( T ) )
        {
            _target = target;
        }

        /// <inheritdoc />
        /// <summary>
        /// When overridden in a derived class, invokes the method that is specified in the provided <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> on the remote object that is represented by the current instance.
        /// </summary>
        /// <param name="msg">A <see cref="T:System.Runtime.Remoting.Messaging.IMessage" /> that contains a <see cref="T:System.Collections.IDictionary" /> of information about the method call.</param>
        /// <returns>
        /// The message returned by the invoked method, containing the return value and any out or ref parameters.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// MethodBase
        /// or
        /// MethodBase
        /// </exception>
        public override IMessage Invoke( IMessage msg )
        {
            _callMethod = msg is IMethodCallMessage callmethod
                ? callmethod
                : throw new ArgumentException( $"取得呼叫方法錯誤,輸入參數名稱 : {nameof( msg )}" );

            var targetMethod = _callMethod.MethodBase is MethodInfo target
                ? target
                : throw new ArgumentException( $"取得目標物件方法錯誤,方法名稱 : {nameof( _callMethod.MethodBase )}" );

            var attributes = new FilterInfoModel( _target, targetMethod );

            try
            {
                var excuting = Executing( attributes.ExecuteFilters );
                if ( excuting.Result != null )
                    ReturnMethod = GetReturnMessage( excuting.Result, excuting.Args );
                else
                {
                    InvokeMethod( targetMethod, excuting );
                    var excuted = Executed( attributes.ExecuteFilters );
                    if ( excuted.Result != null )
                        ReturnMethod = GetReturnMessage( excuted.Result, excuted.Args );
                }
            }
            catch ( Exception ex )
            {
                var exception = OnException( attributes.ExceptionFilters, ex );
                //是否要自行處理錯誤
                ReturnMethod = exception.Result != null
                    ? GetReturnMessage( exception.Result, exception.Args )
                    : new ReturnMessage( ex, _callMethod );
            }

            return ReturnMethod;
        }

        private ExecutedContextModel Executed( IEnumerable<IExecuteFilter> filters )
        {
            var excutedContext = new ExecutedContextModel( ReturnMethod );

            foreach ( var filter in filters )
            {
                filter.OnExecuted( excutedContext );
                if ( excutedContext.Result != null )
                    break;
            }

            return excutedContext;
        }

        private ExecutingContextModel Executing( IEnumerable<IExecuteFilter> filters )
        {
            var excuteContext = new ExecutingContextModel( _callMethod );

            foreach ( var filter in filters )
            {
                filter.OnExecuting( excuteContext );
                if ( excuteContext.Result != null )
                    break;
            }

            return excuteContext;
        }

        private ReturnMessage GetReturnMessage( object result, object[ ] args )
        {
            return new ReturnMessage(
                result,
                args,
                args.Length,
                _callMethod.LogicalCallContext,
                _callMethod );
        }

        private void InvokeMethod( MethodInfo targetMethod, ExecutingContextModel executing )
        {
            var result = targetMethod.Invoke( _target, executing.Args );
            ReturnMethod = GetReturnMessage( result, executing.Args );
        }

        /// <summary>
        /// 執行Exception過濾器
        /// </summary>
        /// <param name="exceptionFilters"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private ExceptionContextModel OnException( IEnumerable<IExceptionFilter> exceptionFilters, Exception ex )
        {
            var excptionContext = new ExceptionContextModel( _callMethod )
            {
                Exception = ex
            };

            foreach ( var filter in exceptionFilters )
            {
                filter.OnException( excptionContext );
                if ( excptionContext.Result != null )
                    break;
            }

            return excptionContext;
        }
    }
}