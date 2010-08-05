/*
 This code is released under the Creative Commons Attribute 3.0 Unported license.
 You are free to share and reuse this code as long as you keep a reference to the author.
 
 See http://creativecommons.org/licenses/by/3.0/
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;

using NullSafeExpressionSample;

namespace NullSafeExpressionSampleTests.TestDoubles
{
    public class TestAutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            CreateMap<TestEntity, TestEntity>()
                .ForMember(n => n.Property2, p => p.NullSafeMapFrom(q => q.Property1.Property3.Length));
        }
    }
}
