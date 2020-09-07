using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;
using SnackShack.Model.Steps;

namespace SnackShack.Model
{
    public class SandwichOrder : OrderBase
    {
        #region Private Members
        private List<IStep> steps;
        #endregion

        public SandwichOrder(TimeSpan placed)
            : base(placed)
        {
            
        }

        public override string Item => "sandwich";

        public override IReadOnlyCollection<IStep> Steps
        {
            get
            {
                if (this.steps == null)
                    this.steps = BuildSteps2();

                return this.steps;
            }
        }

        private List<IStep> BuildSteps2()
        {
            return new List<IStep>()
            {
                new PlaceOrderStep(this.Item),
                new MakeStep(this.Item, TimeSpan.FromMinutes(1)),
                new ServeStep(this.Item, TimeSpan.FromSeconds(30)),
            };
        }

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
