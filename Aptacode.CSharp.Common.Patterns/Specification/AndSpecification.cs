﻿using System;
using System.Linq.Expressions;

namespace Aptacode.CSharp.Common.Patterns.Specification
{
    public sealed class AndSpecification<T> : Specification<T>
    {
        public Specification<T> Left { get; set; }
        public Specification<T> Right { get; set; }

        public AndSpecification(Specification<T> left, Specification<T> right)
        {
            Right = right;
            Left = left;
        }

        public AndSpecification() : this(new NullSpecification<T>(), new NullSpecification<T>())
        {

        }

        public override Expression<Func<T, bool>> ToExpression()
        {
            var leftExpression = Left.ToExpression();
            var rightExpression = Right.ToExpression();

            var paramExpr = Expression.Parameter(typeof(T));
            var exprBody = Expression.AndAlso(leftExpression.Body, rightExpression.Body);
            exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);

            return Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);
        }
    }
}