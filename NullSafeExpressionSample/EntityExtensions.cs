/*
 This code is released under the Creative Commons Attribute 3.0 Unported license.
 You are free to share and reuse this code as long as you keep a reference to the author.

 See http://creativecommons.org/licenses/by/3.0/
*/

using System;
using System.Linq.Expressions;

using SharpArch.Domain.DomainModel;

namespace NullSafeExpressionSample
{
    /// <summary>
    ///     Provides methods that extend entity types.
    /// </summary>
    public static class EntityExtensions
    {
        /// <summary>
        ///     Returns the value of the specified expression, returning the default value of the
        ///     result type (<c>null</c> for reference types) instead of throwing an exception
        ///     when one or more of the referenced members in the expression are <c>null</c>.
        /// </summary>
        /// <typeparam name="TSource">The type of the source instance to evaluate the expression on.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="instance">The instance the method must operate on.</param>
        /// <param name="expression">The expression to evaluate.</param>
        /// <returns>The result of the expression or the default value of the result type, if one or more of the referenced members in the expression are <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">
        ///     The specified <paramref name="instance"/> is <c>null</c>.
        ///     -or-
        ///     The specified <paramref name="expression"/> is <c>null</c>.
        /// </exception>
        public static TResult NullSafeGet<TSource, TResult>(this TSource instance, Expression<Func<TSource, TResult>> expression)
            where TSource : Entity
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            return NullSafeExpressionHandler.SafeGet(instance, expression);
        }
    }
}
