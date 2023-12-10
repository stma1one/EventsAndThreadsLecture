using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace AwaitAsyncEx
{
    public class Battery
    {
        const int MAX_CAPACITY = 1000;
        private static Random r = new Random();
        //Add events to the class to notify upon threshhold reached and shut down!
        #region events
        public event Action ReachThreshold;
        public event Action ShutDown;
        #endregion
        private int Threshold { get; }
        public int Capacity { get; set; }
        public int Percent
        {
            get
            {
                return 100 * Capacity / MAX_CAPACITY;
            }
        }
        public Battery()
        {
            Capacity = MAX_CAPACITY;
            Threshold = 400;
        }

        public void Usage()
        {
            Capacity -= r.Next(50, 150);
            //Add calls to the events based on the capacity and threshhold
            #region Fire Events
            if (Capacity <= 0)
            {
                if (ShutDown != null)
                    ShutDown();
            }
            else if (Capacity <= Threshold)
                if (ReachThreshold != null)
                    ReachThreshold();

            #endregion
        }

    }

    class ElectricCar
    {
        public Battery Bat { get; set; }
        private int id;

        //Add event to notify when the car is shut down
        public event Action OnCarShutDown;
        public ElectricCar()
        {
            id = 0;
            Bat = new Battery();
            //Add code to register to the events to be notified upon threshhold reached and shutdown of engine!
            #region Register to battery events
            Bat.ReachThreshold += Bat_ReachThreshold;
            Bat.ShutDown += Bat_ShutDown;
            #endregion
        }
        public ElectricCar(int id)
        {
            this.id = id;
            Bat = new Battery();
            Bat.ReachThreshold += Bat_ReachThreshold;
            Bat.ShutDown += Bat_ShutDown;
        }
        public void StartEngine()
        {
            while (Bat.Capacity > 0)
            {
                Console.WriteLine($"{this} {Bat.Percent}% Thread: {Thread.CurrentThread.ManagedThreadId}");
                Task.Delay(1000);
                Bat.Usage();
            }
        }

        //Add code to Define and implement the battery event implementations
        #region events implementation
        private void Bat_ShutDown()
        {
            Console.WriteLine($"CAR ID: {this.id} Low Battery - Shutting down Thread: {Thread.CurrentThread.ManagedThreadId}");
            if (OnCarShutDown != null)
                OnCarShutDown();
        }

        private void Bat_ReachThreshold()
        {
            Console.WriteLine($"CAR ID: {this.id} Low Battery. Please Charge Thread: {Thread.CurrentThread.ManagedThreadId}");
        }
        #endregion

        public override string ToString()
        {
            return $"Car: {id}";
        }
        



    }
    
}
