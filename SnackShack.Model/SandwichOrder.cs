using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;
using SnackShack.Model.Steps;

namespace SnackShack.Model
{
    public class SandwichOrder : OrderBase
    {
        public SandwichOrder(TimeSpan placed)
            : base(placed)
        {
        }

        public override string Item => "sandwich";

        protected override Queue<IStep> BuildSteps()
        {
            var steps = new Queue<IStep>();
            steps.Enqueue(new PlaceOrderStep(this.Item));
            steps.Enqueue(new MakeStep(this.Item, TimeSpan.FromMinutes(1)));
            steps.Enqueue(new ServeStep(this.Item, TimeSpan.FromSeconds(30)));

            return steps;
        }
    }
}
