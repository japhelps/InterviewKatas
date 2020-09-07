using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model.Steps
{
    public abstract class StepBase : IStep
    {
        public StepBase(string itemName, TimeSpan timeToComplete)
        {
            if (string.IsNullOrWhiteSpace(itemName))
                throw new ArgumentNullException(nameof(itemName));

            this.Name = $"{this.StepName} {itemName}";
            this.TimeToComplete = timeToComplete;
        }

        public string Name { get; }
        public TimeSpan TimeToComplete { get; }
        public abstract int Weight { get; }

        protected abstract string StepName { get; }
    }
}
