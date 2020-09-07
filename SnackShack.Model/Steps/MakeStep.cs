using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model.Steps
{
    public class MakeStep : StepBase
    {
        public MakeStep(string itemName, TimeSpan timeToComplete)
            : base(itemName, timeToComplete)
        {
        }

        public override int Weight => 100;
        protected override string StepName => "Make";
    }
}
