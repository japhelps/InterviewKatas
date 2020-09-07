using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api;
using SnackShack.Api.Data;
using SnackShack.Model;
using Xunit;

namespace SnackShack.Tests
{
    public class SchedulerTests
    {
        [Fact]
        public void CanCreateValidSchedule()
        {
            var orders = new List<IOrder>()
            { 
                new SandwichOrder(TimeSpan.Zero),
                new SandwichOrder(TimeSpan.Zero),
                new SandwichOrder(TimeSpan.Zero),
                new SandwichOrder(TimeSpan.Zero),
            };

            IScheduler scheduler = new Scheduler(1);

            var schedule = scheduler.Create(orders);

            Assert.NotNull(schedule);
            Assert.IsType<Schedule>(schedule);
            Assert.Collection(schedule.Tasks,
                x => Assert.Equal($"00:00 {orders.Count} orders placed, make sandwich 1", x.Name),
                x => Assert.Equal("01:00 serve sandwich 1", x.Name),
                x => Assert.Equal("01:30 make sandwich 2", x.Name),
                x => Assert.Equal("02:30 serve sandwich 2", x.Name),
                x => Assert.Equal("03:00 make sandwich 3", x.Name),
                x => Assert.Equal("04:00 serve sandwich 3", x.Name),
                x => Assert.Equal("04:30 make sandwich 4", x.Name),
                x => Assert.Equal("05:30 serve sandwich 4", x.Name),
                x => Assert.Equal("06:00 take a well earned break.", x.Name));
        }
    }
}
