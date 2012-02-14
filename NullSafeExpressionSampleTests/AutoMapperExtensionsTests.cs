/*
 This code is released under the Creative Commons Attribute 3.0 Unported license.
 You are free to share and reuse this code as long as you keep a reference to the author.
 
 See http://creativecommons.org/licenses/by/3.0/
*/

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NullSafeExpressionSampleTests.TestDoubles;

namespace NullSafeExpressionSampleTests
{
    [TestClass]
    public class AutoMapperExtensionsTests
    {
        [TestMethod]
        public void SafeMapFrom_MapUsingSafeMapFrom_Succeeds()
        {
            // Arrange.
            TestEntity subject = new TestEntity()
            {
                Property1 = new TestEntityPropertyType()
                {
                    Property3 = "abc"
                }
            };

            TestEntity subject2 = new TestEntity();

            Mapper.Reset();

            // Act.
            Mapper.Initialize(n => n.AddProfile<TestAutoMapperProfile>());
            Mapper.AssertConfigurationIsValid();
            var result = Mapper.Map<TestEntity, TestEntity>(subject, subject2);

            // Assert.
            Assert.AreEqual(subject.Property1.Property3.Length, subject2.Property2);
        }
    }
}
