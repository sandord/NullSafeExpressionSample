/*
 This code is released under the Creative Commons Attribute 3.0 Unported license.
 You are free to share and reuse this code as long as you keep a reference to the author.
 
 See http://creativecommons.org/licenses/by/3.0/
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SharpArch.Core.DomainModel;

namespace NullSafeExpressionSampleTests.TestDoubles
{
    internal class TestEntity : Entity
    {
        public virtual TestEntityPropertyType Property1
        {
            get;
            set;
        }

        public virtual int Property2
        {
            get;
            set;
        }
    }
}
