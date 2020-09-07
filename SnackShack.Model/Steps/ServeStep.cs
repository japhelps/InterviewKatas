using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model.Steps
{
    public class ServeStep : StepBase
    {
        public ServeStep(string itemName, TimeSpan timeToComplete)
            : base(itemName, timeToComplete)
        {
        }

        public override int Weight => 100;
        protected override string StepName => "Serve";
    }
}
