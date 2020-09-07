using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
    internal sealed class OrderStep
    {
        public OrderStep(IOrder order, IStep step)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            if (step == null)
                throw new ArgumentNullException(nameof(step));

            this.Order = order;
            this.Step = step;
        }

        public IOrder Order { get; }
        public IStep Step { get; }
        public string Description => $"{this.Step.Name} {this.Order.Position}";


        #region Comparison Methods
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 47;
                hash = hash * 53 + this.Order.GetHashCode();
                hash = hash * 53 + this.Step.GetHashCode();
                return hash;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (ReferenceEquals(obj, null))
                return false;

            return Equals(obj as OrderStep);
        }

        public bool Equals(OrderStep obj)
        {
            if (ReferenceEquals(obj, this))
                return true;

            if (ReferenceEquals(this, null))
                return false;

            if (ReferenceEquals(obj, null))
                return false;

            return ReferenceEquals(obj.Order, this.Order) &&
                ReferenceEquals(obj.Step, this.Step);
        }

        public static bool operator ==(OrderStep lhs, OrderStep rhs)
        {
            if (ReferenceEquals(lhs, rhs))
                return true;

            if (ReferenceEquals(lhs, null))
                return false;

            return lhs.Equals(rhs);
        }

        public static bool operator !=(OrderStep lhs, OrderStep rhs)
        {
            return !(lhs == rhs);
        }
        #endregion
    }
}
