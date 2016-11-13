// <copyright file="RegistrationControllerTest.cs">© , 2016</copyright>
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApplication3.Controllers.Registration;

namespace WebApplication3.Controllers.Registration.Tests
{
    /// <summary>Этот класс содержит параметризованные модульные тесты для RegistrationController</summary>
    [PexClass(typeof(RegistrationController))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class RegistrationControllerTest
    {
        /// <summary>Тестовая заглушка для Сheck(String)</summary>
        [PexMethod]
        public Task<ActionResult> СheckTest([PexAssumeUnderTest]RegistrationController target, string ID)
        {
            Task<ActionResult> result = target.Сheck(ID);
            return result;
            // TODO: добавление проверочных утверждений в метод RegistrationControllerTest.СheckTest(RegistrationController, String)
        }
    }
}
