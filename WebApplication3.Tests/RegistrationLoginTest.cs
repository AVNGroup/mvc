// <copyright file="RegistrationLoginTest.cs">© , 2016</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace <no name>.Tests
{
    [TestClass]
    [PexClass(typeof(global::RegistrationLogin))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class RegistrationLoginTest
    {
    }
}
