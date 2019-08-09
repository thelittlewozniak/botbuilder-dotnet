﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.AI.TriggerTrees;
using Microsoft.Bot.Builder.Expressions.Parser;

namespace Microsoft.Bot.Builder.Dialogs.Adaptive.Selectors
{
    /// <summary>
    /// Select the most specific true rule implementation of <see cref="IEventSelector"/>.
    /// </summary>
    public class MostSpecificSelector : IEventSelector
    {
        private readonly TriggerTree _tree = new TriggerTree();

        /// <summary>
        /// Gets or sets optional rule selector to use when more than one most specific rule is true.
        /// </summary>
        /// <value>
        /// Optional rule selector to use when more than one most specific rule is true.
        /// </value>
        public IEventSelector Selector { get; set; }

        public virtual void Initialize(IEnumerable<IOnEvent> rules, bool evaluate)
        {
            var parser = new ExpressionEngine(TriggerTree.LookupFunction);
            foreach (var rule in rules)
            {
                _tree.AddTrigger(rule.GetExpression(parser), rule);
            }
        }

        public virtual async Task<IReadOnlyList<IOnEvent>> Select(SequenceContext context, CancellationToken cancel)
        {
            var nodes = _tree.Matches(context.State);
            var matches = new List<IOnEvent>();
            foreach (var node in nodes)
            {
                foreach (var trigger in node.AllTriggers)
                {
                    matches.Add((IOnEvent)trigger.Action);
                }
            }

            IReadOnlyList<IOnEvent> selections = matches;
            if (Selector != null)
            { 
                Selector.Initialize(matches, false);
                selections = await Selector.Select(context, cancel).ConfigureAwait(false);
            }

            return selections;
        }
    }
}
