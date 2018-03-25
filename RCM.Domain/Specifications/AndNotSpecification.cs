﻿using System;
using System.Linq.Expressions;

namespace RCM.Domain.Specifications
{
    public class AndNotSpecification<T> : BaseSpecification<T>, ISpecification<T> where T : class
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndNotSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            ParameterExpression param = Expression.Parameter(typeof(T));
            Expression leftExpr = _left.ToExpression();
            Expression rightExpr = _right.ToExpression();
            BinaryExpression body = Expression.And(leftExpr, rightExpr);
            Expression<Func<T, bool>> expression = Expression.Lambda<Func<T, bool>>(body, param);

            return expression;
        }
    }
}
