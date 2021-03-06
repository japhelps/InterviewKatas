﻿using System;
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
            };

            IScheduler scheduler = new Scheduler(100, CreateInventory());
            foreach (var order in orders)
                scheduler.Add(order);
            var schedule = scheduler.Create();

            Assert.NotNull(schedule);
            Assert.IsType<Schedule>(schedule);
            Assert.Collection(schedule.Tasks,
                x =>
                {
                    Assert.Equal($"{orders.Count} sandwich order(s) placed, Make sandwich 1", x.Name);
                    Assert.Equal(TimeSpan.Zero, x.Start);
                },
                x =>
                {
                    Assert.Equal("Serve sandwich 1", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(1 * 60), x.Start);
                },
                x =>
                {
                    Assert.Equal("Make sandwich 2", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(1 * 60 + 30), x.Start);
                },
                x =>
                {
                    Assert.Equal("Serve sandwich 2", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(2 * 60 + 30), x.Start);
                },
                x =>
                {
                    Assert.Equal("Make sandwich 3", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(3 * 60), x.Start);
                },
                x =>
                {
                    Assert.Equal("Serve sandwich 3", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(4 * 60), x.Start);
                },
                x =>
                {
                    Assert.Equal("take a well earned break!", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(4 * 60 + 30), x.Start);
                });
        }

        [Fact]
        public void CanAddOrders()
        {
            var o1 = new SandwichOrder(TimeSpan.Zero);
            var o2 = new SandwichOrder(TimeSpan.Zero);
            var o3 = new SandwichOrder(TimeSpan.Zero);
            var orders = new List<IOrder>() { o1, o2, o3 };

            IScheduler scheduler = new Scheduler(100, CreateInventory());

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
        public void AddingTooManyOrdersAtTheSameTimeThrowWaitTimeException()
        {
            var o1 = new SandwichOrder(TimeSpan.Zero);
            var o2 = new SandwichOrder(TimeSpan.Zero);
            var o3 = new SandwichOrder(TimeSpan.Zero);
            var o4 = new SandwichOrder(TimeSpan.Zero);
            var orders = new List<IOrder>() { o1, o2, o3, o4 };

            IScheduler scheduler = new Scheduler(100, CreateInventory());

            var ex = Record.Exception(() =>
            {
                foreach (var order in orders)
                    scheduler.Add(order);
            });

            Assert.NotNull(ex);
            Assert.IsType<WaitTimeTooLongException>(ex);
        }

        [Fact]
        public void AddingTooManyOrdersCloseInTimeThrowWaitTimeException()
        {
            var o1 = new SandwichOrder(TimeSpan.Zero);
            var o2 = new SandwichOrder(TimeSpan.Zero);
            var o3 = new SandwichOrder(TimeSpan.Zero);
            var o4 = new SandwichOrder(TimeSpan.FromSeconds(30));
            var orders = new List<IOrder>() { o1, o2, o3, o4 };

            IScheduler scheduler = new Scheduler(100, CreateInventory());

            var ex = Record.Exception(() =>
            {
                foreach (var order in orders)
                    scheduler.Add(order);
            });

            Assert.NotNull(ex);
            Assert.IsType<WaitTimeTooLongException>(ex);
        }

        [Fact]
        public void AddingOrderInTheMiddleCreatesSchedule()
        {
            var orders = new List<IOrder>()
            {
                new SandwichOrder(TimeSpan.Zero),
                new SandwichOrder(TimeSpan.Zero),
                new SandwichOrder(TimeSpan.Zero),
                new SandwichOrder(TimeSpan.FromMinutes(2)),
            };

            IScheduler scheduler = new Scheduler(100, CreateInventory());
            foreach (var order in orders)
                scheduler.Add(order);
            var schedule = scheduler.Create();

            Assert.NotNull(schedule);
            Assert.IsType<Schedule>(schedule);
            Assert.Collection(schedule.Tasks,
                x =>
                {
                    Assert.Equal($"{orders.Count - 1} sandwich order(s) placed, Make sandwich 1", x.Name);
                    Assert.Equal(TimeSpan.Zero, x.Start);
                },
                x =>
                {
                    Assert.Equal("Serve sandwich 1", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(1 * 60), x.Start);
                },
                x =>
                {
                    Assert.Equal("Make sandwich 2", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(1 * 60 + 30), x.Start);
                },
                x =>
                {
                    Assert.Equal("1 sandwich order(s) placed", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(2 * 60), x.Start);
                },
                x =>
                {
                    Assert.Equal("Serve sandwich 2", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(2 * 60 + 30), x.Start);
                },
                x =>
                {
                    Assert.Equal("Make sandwich 3", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(3 * 60), x.Start);
                },
                x =>
                {
                    Assert.Equal("Serve sandwich 3", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(4 * 60), x.Start);
                },
                x =>
                {
                    Assert.Equal("Make sandwich 4", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(4 * 60 + 30), x.Start);
                },
                x =>
                {
                    Assert.Equal("Serve sandwich 4", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(5 * 60 + 30), x.Start);
                },
                x =>
                {
                    Assert.Equal("take a well earned break!", x.Name);
                    Assert.Equal(TimeSpan.FromSeconds(6 * 60), x.Start);
                });
        }

        [Fact]
        public void OrderingTooManyItemsThrowsException()
        {
            var o1 = new SandwichOrder(TimeSpan.Zero);
            var o2 = new SandwichOrder(TimeSpan.Zero);
            var o3 = new SandwichOrder(TimeSpan.Zero);
            var o4 = new SandwichOrder(TimeSpan.FromSeconds(120));
            var orders = new List<IOrder>() { o1, o2, o3, o4 };

            IScheduler scheduler = new Scheduler(100, CreateInventory(3));

            var ex = Record.Exception(() =>
            {
                foreach (var order in orders)
                    scheduler.Add(order);
            });

            Assert.NotNull(ex);
            Assert.IsType<SoldOutException>(ex);
        }

        #region Helper Methods
        private IInventory CreateInventory()
        {
            return CreateInventory(45);
        }

        private IInventory CreateInventory(int sandwiches)
        {
            return new Inventory(sandwiches);
        } 
        #endregion
    }
}
