/*
 This code is released under the Creative Commons Attribute 3.0 Unported license.
 You are free to share and reuse this code as long as you keep a reference to the author.

 See http://creativecommons.org/licenses/by/3.0/
*/

namespace NullSafeExpressionSample
{
    using System;
    using System.Linq.Expressions;

    using AutoMapper;

    /// <summary>
    ///     Provides extension methods that add additional functionality to the AutoMapper library.
    /// </summary>
    public static class AutoMapperExtensions
    {
        /// <summary>
        ///     Maps the specified source member in such way, that the default value of the result
        ///     type (<c>null</c> for reference types) is used instead of throwing an exception
        ///     when one or more of the referenced members in the expression are <c>null</c>.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TMember">The member type.</typeparam>
        /// <param name="instance">The instance the method must operate on.</param>
        /// <param name="mapping">The mapping expression.</param>
        /// <exception cref="ArgumentNullException">
        ///     The specified <paramref name="instance"/> is <c>null</c>.
        ///     -or-
        ///     The specified <paramref name="mapping"/> is <c>null</c>.
        /// </exception>
        public static void NullSafeMapFrom<TSource, TMember>(this IMemberConfigurationExpression<TSource> instance, Expression<Func<TSource, TMember>> mapping)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else if (mapping == null)
            {
                throw new ArgumentNullException("mapping");
            }

            instance.MapFrom<TMember>(n => NullSafeExpressionHandler.GetSafeGetDelegate(mapping)(n));
        }
    }
}
