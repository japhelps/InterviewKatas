using System;
using System.Collections.Generic;
using System.Text;
using SnackShack.Api;
using SnackShack.Api.Data;
using SnackShack.Model;

namespace SnackShack
{
    class Program
    {
        static void Main(string[] args)
        {
            IScheduler scheduler = new Scheduler(100, new Inventory(45));
            IOrderFactory factory = new OrderFactory();

            try
            {
                var app = new App(scheduler, factory);

                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
        }

        #region Private Methods
        #region UI Methods
        private static void DisplaySchedule(ISchedule schedule)
        {
            foreach (var task in schedule.Tasks)
                Console.WriteLine("{0:mm\\:ss} {1}", task.Start, task.Name);
        }
        #endregion
        #endregion
    }
}
