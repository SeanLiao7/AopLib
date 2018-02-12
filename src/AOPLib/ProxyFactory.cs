using System;

namespace AOPLib
{
    public static class ProxyFactory
    {
        /// <summary>
        /// 取得代理實體
        /// </summary>
        /// <param name="para">建構子參數</param>
        public static TOjbect GetProxyInstance<TOjbect>( object[ ] para = null )
            where TOjbect : MarshalByRefObject
        {
            if ( !( Activator.CreateInstance( typeof( TOjbect ), para ) is TOjbect obj ) )
                throw new ArgumentException( $"建立物件失敗，傳入參數：{nameof( MarshalByRefObject )}" );
            return GetProxyInstance( obj );
        }

        /// <summary>
        /// 取得代理實體
        /// </summary>
        /// <typeparam name="TOjbect">代理類型別</typeparam>
        /// <param name="realSubjcet">被代理類別實體</param>
        /// <returns></returns>
        public static TOjbect GetProxyInstance<TOjbect>( TOjbect realSubjcet )
             where TOjbect : MarshalByRefObject
        {
            var proxy = new DynamicProxy<TOjbect>( realSubjcet );
            return proxy.GetTransparentProxy( ) as TOjbect;
        }
    }
}