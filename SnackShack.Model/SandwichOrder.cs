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

        protected override List<IStep> BuildSteps()
        {
            return new List<IStep>()
            {
                new PlaceOrderStep(this.Item),
                new MakeStep(this.Item, TimeSpan.FromMinutes(1)),
                new ServeStep(this.Item, TimeSpan.FromSeconds(30)),
            };
        }
    }
}
