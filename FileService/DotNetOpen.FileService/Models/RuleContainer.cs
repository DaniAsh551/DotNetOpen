using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DotNetOpen.FileService
{
    /// <summary>
    /// The default rule container.
    /// </summary>
    public class RuleContainer : IRuleContainer
    {
        private readonly ConcurrentBag<IRule> _rules = new ConcurrentBag<IRule>();

        /// <inheritdoc/>
        public long Length => _rules.Count;
        /// <inheritdoc/>
        public void AddRule<TRule>() where TRule:IRule
        {
            var type = typeof(TRule);
            var paramlessCtor = type.GetConstructors()?.FirstOrDefault(x => 
            {
                var @params = x.GetParameters();
                return !@params?.Any() ?? true;
            });

            var publicNameSet = type.GetProperty("Name").GetSetMethod(false);

            if (paramlessCtor == null || publicNameSet != null)
                throw new InvalidRuleException(type, "Rules must have atleast one public constructor and a non-public set method for the 'Name'.");

            var rule = (TRule)paramlessCtor.Invoke(new object[] { });
            var ruleWithSameNameExists = _rules.Any(x => x.Name.ToLower() == rule.Name.ToLower());

            if (ruleWithSameNameExists)
                throw new InvalidRuleException(type, "A rule with a similar name already exists");

            _rules.Add(rule);
        }
        /// <inheritdoc/>
        public void ExecuteAllRules(IFileServiceConfig fileServiceConfig, Stream inputStream, string fileType, string fileName = null)
        {
            foreach (var rule in _rules)
            {
                rule.Execute(fileServiceConfig, inputStream, fileType, fileName);
            }
        }
        /// <inheritdoc/>
        public void ExecuteAllRules(IFileServiceConfig fileServiceConfig, byte[] inputBytes, string fileType, string fileName = null)
        {
            foreach (var rule in _rules)
            {
                rule.Execute(fileServiceConfig, inputBytes, fileType, fileName);
            }
        }
        /// <inheritdoc/>
        IEnumerator GetEnumerator()
            => _rules.GetEnumerator();
        /// <inheritdoc/>
        IEnumerator<IRule> IEnumerable<IRule>.GetEnumerator()
            => _rules.GetEnumerator();
        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
           => _rules.GetEnumerator();
        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{nameof(RuleContainer)}[{Length}]";
        }
    }
}
