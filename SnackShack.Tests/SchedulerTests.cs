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

            IScheduler scheduler = new Scheduler(100);

            var schedule = scheduler.Create();

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

        [Fact]
        public void CanAddOrders()
        {
            var o1 = new SandwichOrder(TimeSpan.Zero);
            var o2 = new SandwichOrder(TimeSpan.Zero);
            var o3 = new SandwichOrder(TimeSpan.Zero);
            var orders = new List<IOrder>() { o1, o2, o3 };

            IScheduler scheduler = new Scheduler(100);

            foreach (var order in orders)
                scheduler.Add(order);

            Assert.True(scheduler.Orders.Count > 0);
            Assert.Equal(3, scheduler.Orders.Count);
            Assert.Collection(scheduler.Orders,
                x => Assert.Equal(o1, x),
                x => Assert.Equal(o2, x),
                x => Assert.Equal(o3, x));
        }

        [Fact]
        public void AddingTooManyOrdersThrowWaitTimeException()
        {
            var o1 = new SandwichOrder(TimeSpan.Zero);
            var o2 = new SandwichOrder(TimeSpan.Zero);
            var o3 = new SandwichOrder(TimeSpan.Zero);
            var o4 = new SandwichOrder(TimeSpan.Zero);
            var orders = new List<IOrder>() { o1, o2, o3, o4 };

            IScheduler scheduler = new Scheduler(100);

            var ex = Record.Exception(() =>
            {
                foreach (var order in orders)
                    scheduler.Add(order);
            });

            Assert.NotNull(ex);
            Assert.IsType<WaitTimeTooLongException>(ex);
        }
    }
}
