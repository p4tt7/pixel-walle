﻿using pixel_walle.src.CodeLocation_;
using pixel_walle.src.Lexical;
using pixel_walle.src.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pixel_walle.src.AST.Expressions
{
    public class Add : BinaryExpression
    {
        private object? _value;

        public Add(Expression left, Expression right, CodeLocation location) : base(location)
        {
            Left = left;
            Right = right;
        }

        public override ExpressionType Type => ExpressionType.Number;
        public override object? Value => _value;

        public override bool CheckSemantic(Scope scope, List<Error> errors)
        {
            if(Right.Type != ExpressionType.Number || Left.Type != ExpressionType.Number)
            {
                errors.Add(new Error(Error.ErrorType.SemanticError, $"Operator '+' can't be applied to operands of type {Right.Type} and {Left.Type}", Location));
                return false;
            }
            return true;
        }

        public override object? Evaluate()
        {
            Right.Evaluate();
            Left.Evaluate();

            _value = (int)Right.Value + (int)Left.Value;
            return Value;
        }
    }
}
