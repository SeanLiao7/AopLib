using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AOPLib.Filters;

namespace AOPLib.Model
{
    /// <summary>
    /// 攔截當前註冊的過濾點
    /// 1.類別上
    /// 2.方法上
    /// </summary>
    public class FilterInfoModel
    {
        public IList<IExceptionFilter> ExceptionFilters { get; }
        public IList<IExecuteFilter> ExecuteFilters { get; }

        public FilterInfoModel( MarshalByRefObject target, MethodInfo method )
        {
            var classAttributes = target.GetType( ).GetCustomAttributes( typeof( Attribute ), true );
            var methodAttributes = Attribute.GetCustomAttributes( method, typeof( Attribute ), true );
            var unionAttributes = classAttributes.Union( methodAttributes ).ToList( );

            ExecuteFilters = unionAttributes.OfType<IExecuteFilter>( ).ToList( );
            ExceptionFilters = unionAttributes.OfType<IExceptionFilter>( ).ToList( );
        }
    }
}