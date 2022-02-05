using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCollectionsAsync
{
    class DelegateAndEventsDemo
    {
        public static void RunDemo_1()
        {
            Console.WriteLine("Start preparing myOmlette...");
            Omlette oml = new Omlette("theOmlette");
            oml.Start();
            Console.WriteLine("myOmlette is Ready...");
        }

        public static void RunDemo_2()
        {
            Console.WriteLine("Start preparing myOmlette...");
            Omlette oml = new Omlette("theOmlette");
            //Register to get the event!
            oml.OnProgressUpdate += Progress;
            //Start preparing
            oml.Start();
        }

        //The event OnProgressUpdate will fire this function! 
        static void Progress(Object sender, int percent)
        {
            if (sender is TaskExecutor)
            {
                TaskExecutor obj = (TaskExecutor)sender;
                Console.WriteLine($"Progress for {obj.Name}: {percent}%");
            }
        }
        public static void RunDemo_3()
        {
            //Omlette
            Console.WriteLine("Start preparing myOmlette...");
            Omlette oml = new Omlette("theOmlette");
            //Register to get the event!
            oml.OnProgressUpdate += Progress;
            //Start preparing
            oml.Start();
        }


        
    }
}
