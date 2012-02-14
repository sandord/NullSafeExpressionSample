/*
 This code is released under the Creative Commons Attribute 3.0 Unported license.
 You are free to share and reuse this code as long as you keep a reference to the author.
 
 See http://creativecommons.org/licenses/by/3.0/
*/

namespace NullSafeExpressionSampleTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using NullSafeExpressionSample;
    using NullSafeExpressionSampleTests.TestDoubles;

    [TestClass]
    public class EntityExtensionsTests
    {
        [TestMethod]
        public void SafeGet_UsingNestedNonNullEntityProperty_Succeeds()
        {
            // Arrange.
            TestEntity subject = new TestEntity()
            {
                Property1 = new TestEntityPropertyType()
                {
                    Property3 = "abc"
                }
            };

            // Act.
            var result = EntityExtensions.NullSafeGet(subject, n => n.Property1.Property3.Length);

            // Assert.
            Assert.AreEqual(subject.Property1.Property3.Length, result);
        }

        [TestMethod]
        public void SafeGet_UsingNestedEntityPropertyWithNull_Succeeds()
        {
            // Arrange.
            TestEntity subject = new TestEntity();

            // Act.
            var result = EntityExtensions.NullSafeGet(subject, n => n.Property1.Property3.Length);

            // Assert.
            Assert.AreEqual(default(int), result);
        }
    }
}
