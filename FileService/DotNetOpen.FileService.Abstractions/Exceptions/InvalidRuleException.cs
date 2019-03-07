using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetOpen.FileService
{
    /// <summary>
    /// Exception thrown when an invalid rule is attempted.
    /// </summary>
    public class InvalidRuleException : Exception
    {
        /// <summary>
        /// The invalid rule.
        /// </summary>
        public readonly Type RuleType;

        public InvalidRuleException(Type ruleType, string message) : base(message)
        {
            RuleType = ruleType;
        }

        public InvalidRuleException(Type ruleType, string message, Exception inner) : base(message, inner)
        {
            RuleType = ruleType;
        }
    }
}
