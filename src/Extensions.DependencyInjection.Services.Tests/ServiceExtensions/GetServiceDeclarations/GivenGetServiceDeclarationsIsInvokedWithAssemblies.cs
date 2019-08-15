﻿using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace Extensions.DependencyInjection.Services.Tests
{
    [TestFixture]
    public class GivenGetServiceDeclarationsIsInvokedWithAssemblies
    {
        private ServiceDeclaration[] result = null;

        [OneTimeSetUp]
        public void Setup()
        {
            Assembly assembly = typeof(TestImplementation).Assembly;

            result = ServiceExtensions
                .GetServiceDeclarations(new[] { assembly })
                .ToArray();
        }

        [Test]
        public void ThenAServiceIsReturnedForEveryServiceAttributeOnTheClass()
        {
            Assert.That(result.Length, Is.EqualTo(2));
        }

        [Test]
        public void ThenTheTestImplementationServiceDescriptorIsReturned()
        {
            ServiceDeclaration serviceDeclaration = result.Single(x => x.ServiceType == typeof(ITestInterface));
            Assert.That(serviceDeclaration.DeclaringType, Is.EqualTo(typeof(TestImplementation)));
            Assert.That(serviceDeclaration.Scope, Is.EqualTo(ServiceScope.Transient));
        }

        [Test]
        public void ThenTheSecondaryImplementationServiceDescriptorIsReturned()
        {
            ServiceDeclaration serviceDeclaration = result.Single(x => x.ServiceType == typeof(ISecondaryTestInterface));
            Assert.That(serviceDeclaration.DeclaringType, Is.EqualTo(typeof(TestImplementation)));
            Assert.That(serviceDeclaration.Scope, Is.EqualTo(ServiceScope.Singleton));
        }
    }
}
