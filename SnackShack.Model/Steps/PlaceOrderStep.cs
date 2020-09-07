using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Model.Steps
{
    public class PlaceOrderStep : StepBase
    {
        public PlaceOrderStep(string itemName)
            : base(itemName, TimeSpan.Zero)
        {
        }

        protected override string StepName => "Order placed for";
        public override int Weight => 0;
    }
}
