using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api;
using SnackShack.Api.Data;

namespace SnackShack.Model
{
    public class OrderFactory : IOrderFactory
    {
        public IOrder Get(string item, TimeSpan placed)
        {
            if (item.Equals("sandwich", StringComparison.InvariantCultureIgnoreCase))
                return new SandwichOrder(placed);

            throw new InvalidOperationException("Unknown order type.");
        }
    }
}
