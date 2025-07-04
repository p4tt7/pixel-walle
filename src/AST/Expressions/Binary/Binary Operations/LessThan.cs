﻿using pixel_walle.src.CodeLocation_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pixel_walle.src.Errors;

namespace pixel_walle.src.AST.Expressions.Binary
{
    public class LessThan : BinaryExpression
    {
        public override ExpressionType Type { get; protected set; }
        public LessThan(Expression left, Expression right, CodeLocation location) : base(location)
        {
            Left = left;
            Right = right;
            Type = ExpressionType.Bool;
        }

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            bool leftValid = Left.CheckSemantic(scope, errors);
            bool rightValid = Right.CheckSemantic(scope, errors);

            if (!leftValid || !rightValid)
                return false;

            if (Left.Type != ExpressionType.Number || Right.Type != ExpressionType.Number)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Operator '<' can't be applied to operands of type {Left.Type} and {Right.Type}", Location));
                return false;
            }
            return true;
        }

        public override object? Evaluate(Context context, List<Error> errors)
        {
            return (int)Left.Evaluate(context, errors) < (int)Right.Evaluate(context, errors);
        }
    }

}
