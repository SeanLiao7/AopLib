using System;
using AOPLib;
using AOPLibTests.TestFilters;
using NUnit.Framework;
using Shouldly;

namespace AOPLibTests
{
    internal interface ITest
    {
        void DoWork( );

        string ThrowException( );
    }

    [TestFixture]
    public class ProxyFactoryTests
    {
        [Test]
        public void CatchMethodException_IgonoreException_ReturnValueShouldbeSameAsAssigned( )
        {
            var testObject = ProxyFactory.GetProxyInstance<TestObject>( );
            testObject.ThrowException( ).ShouldBe( "MessageModified" );
        }

        [Test]
        public void CatchMethodException_IgonoreException_ShouldNotThrowException( )
        {
            var testObject = ProxyFactory.GetProxyInstance<TestObject>( );
            Should.NotThrow( ( ) => testObject.ThrowException( ) );
        }

        [Test]
        public void GetProxyInstance_Success_ShouldNotThrowException( )
        {
            Should.NotThrow( ( ) => ProxyFactory.GetProxyInstance<TestObject>( ) );
        }
    }

    internal class TestObject : MarshalByRefObject, ITest
    {
        [Log]
        public void DoWork( )
        {
        }

        [CatchException( "MessageModified" )]
        public string ThrowException( )
        {
            throw new NotImplementedException( );
        }
    }
}