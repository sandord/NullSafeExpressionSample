/*
 This code is released under the Creative Commons Attribute 3.0 Unported license.
 You are free to share and reuse this code as long as you keep a reference to the author.
 
 See http://creativecommons.org/licenses/by/3.0/
*/

namespace NullSafeExpressionSampleTests
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NullSafeExpressionSample;
    using NullSafeExpressionSampleTests.TestDoubles;

    using SharpArch.Domain.DomainModel;

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

        [TestMethod]
        public void SafeGet_UsingTwoDifferentObjectsAccessingEquallyNamedProperty_ReturnsCorrectPropertyValue()
        {
            // Arrange.
            TestEntity subject1 = new TestEntity()
            {
                Property1 = new TestEntityPropertyType()
            };

            TestEntity2 subject2 = new TestEntity2()
            {
                Property1 = new TestEntityPropertyType()
            };

            // Act.
            var result = NullSafeExpressionHandler.SafeGet(subject1, n => n.Property1);
            result = NullSafeExpressionHandler.SafeGet(subject2, n => n.Property1);

            // Assert.
            Assert.IsNotNull(result);
            Assert.AreEqual(subject2.Property1, result);
        }
    }
}
