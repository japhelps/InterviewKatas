using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api.Data;

namespace SnackShack.Api
{
    public interface IOrderFactory
    {
        IOrder Get(string item, TimeSpan placed);
    }
}
