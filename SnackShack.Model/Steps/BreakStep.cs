using System;
using System.Collections.Generic;
using System.Text;

namespace SnackShack.Model.Steps
{
    public class BreakStep : StepBase
    {
        public BreakStep() 
            : base("break!", TimeSpan.FromSeconds(30))
        {
        }

        public override int Weight => 100;
        protected override string StepName => "Take a well earned ";
    }
}
