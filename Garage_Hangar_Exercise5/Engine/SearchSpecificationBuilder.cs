using Garage_Hangar_Exercise5.Garage_detailed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Garage_Hangar_Exercise5.Engine
{
    public class SearchSpecificationBuilder<T>
    {
        private List<Expression<Func<T, bool>>> _criteria = new List<Expression<Func<T, bool>>>();

        public void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            if (criteria.Body is BinaryExpression binaryExpression)
            {
                if (IsStringComparison(binaryExpression))
                {
                    var adjustedExpression = MakeStringComparisonCaseInsensitive(binaryExpression);
                    _criteria.Add(Expression.Lambda<Func<T, bool>>(adjustedExpression, criteria.Parameters));
                    return;
                }
            }

            _criteria.Add(criteria);
            

        }

        private bool IsStringComparison(BinaryExpression expression)
        {
            return expression.Left.Type == typeof(string) && expression.Right.Type == typeof(string);
        }

        private Expression MakeStringComparisonCaseInsensitive(BinaryExpression expression)
        {
            if (expression.NodeType == ExpressionType.Equal || expression.NodeType == ExpressionType.NotEqual)
            {
                var compareMethod = typeof(string).GetMethod("Equals", new[] { typeof(string), typeof(string), typeof(StringComparison) });
                var callExpression = Expression.Call(null, compareMethod, expression.Left, expression.Right, Expression.Constant(StringComparison.OrdinalIgnoreCase));

                // If it's a NotEqual operation, negate the result
                return expression.NodeType == ExpressionType.NotEqual ? Expression.Not(callExpression) : (Expression)callExpression;
            }

            return expression;
        }

        public Func<T, bool> Build()
        {
            var param = Expression.Parameter(typeof(T));
            Expression body = Expression.Constant(true);  // Starting point: "true AND ..."

            foreach (var criteria in _criteria)
            {
                var visitor = new ReplaceParameterVisitor(criteria.Parameters[0], param);
                body = Expression.AndAlso(body, visitor.Visit(criteria.Body));
            }

            return Expression.Lambda<Func<T, bool>>(body, param).Compile();
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _oldParameter;
            private readonly ParameterExpression _newParameter;

            public ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter)
            {
                _oldParameter = oldParameter ?? throw new ArgumentNullException(nameof(oldParameter));
                _newParameter = newParameter ?? throw new ArgumentNullException(nameof(newParameter));
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _oldParameter ? _newParameter : base.VisitParameter(node);
            }
        }
    }
}