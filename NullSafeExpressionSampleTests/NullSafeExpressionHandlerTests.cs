/*
 This code is released under the Creative Commons Attribute 3.0 Unported license.
 You are free to share and reuse this code as long as you keep a reference to the author.
 
 See http://creativecommons.org/licenses/by/3.0/
*/

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using SharpArch.Core.DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NullSafeExpressionSample;
using NullSafeExpressionSampleTests.TestDoubles;

namespace NullSafeExpressionSampleTests
{
    [TestClass]
    public class NullSafeExpressionHandlerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SafeGet_PassingNullAsExpression_ThrowsException()
        {
            // Arrange.
            TestEntity subject = new TestEntity()
            {
                Property1 = new TestEntityPropertyType()
            };

            // Act.
            var result = NullSafeExpressionHandler.SafeGet<Entity, object>(subject, null);

            // Assert.
        }

        [TestMethod]
        public void SafeGet_UsingDirectReferenceTypeProperty_ReturnsPropertyValue()
        {
            // Arrange.
            TestEntity subject = new TestEntity()
            {
                Property1 = new TestEntityPropertyType()
            };

            // Act.
            var result = NullSafeExpressionHandler.SafeGet(subject, n => n.Property1);

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(subject.Property1, result);
        }

        [TestMethod]
        public void SafeGet_UsingDirectValueTypeProperty_ReturnsPropertyValue()
        {
            // Arrange.
            TestEntity subject = new TestEntity()
            {
                Property2 = 123
            };

            // Act.
            var result = NullSafeExpressionHandler.SafeGet(subject, n => n.Property2);

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(subject.Property2, result);
        }

        [TestMethod]
        public void SafeGet_UsingNestedProperty_ReturnsPropertyResult()
        {
            // Arrange.
            string stringValue = "Hello";

            TestEntity subject = new TestEntity()
            {
                Property1 = new TestEntityPropertyType()
                {
                    Property3 = stringValue
                }
            };

            // Act.
            object result = NullSafeExpressionHandler.SafeGet(subject, n => n.Property1.Property3.Length);

            // Assert.
            Assert.IsNotNull(result);
            Assert.IsTrue(result is int);
            Assert.AreEqual(stringValue.Length, (int)result);
        }

        [TestMethod]
        public void SafeGet_UsingNestedReferenceTypePropertyWithNullInChain_ReturnsNull()
        {
            // Arrange.
            TestEntity subject = new TestEntity();

            // Act.
            object result = NullSafeExpressionHandler.SafeGet(subject, n => n.Property1.Property3);

            // Assert.
            Assert.AreEqual(result, null);
        }

        [TestMethod]
        public void SafeGet_UsingNestedValueTypePropertyWithNullInChain_ReturnsDefault()
        {
            // Arrange.
            TestEntity subject = new TestEntity();

            // Act.
            object result = NullSafeExpressionHandler.SafeGet(subject, n => n.Property1.Property3.Length);

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(result.GetType(), typeof(int));
            Assert.AreEqual(result, default(int));
        }
    }

}
