using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCollectionsAsync
{
    class BreafastWithAsync
    {

    }


    class TaskExecutorAsync
    {
        private string name;
        private int timeInMiliSec;
        public TaskExecutorAsync(string name, int ms)
        {
            this.timeInMiliSec = ms;
            this.name = name;
        }

        public string Name { get { return this.name; } }
        public void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(this.timeInMiliSec / 10);
                if (OnProgressUpdate != null)
                    OnProgressUpdate(this, (i + 1) * 10);
            }
            if (OnFinish != null)
                OnFinish(this);
        }

        public delegate void ProgressEventHandler(Object sender, int percent);
        public event ProgressEventHandler OnProgressUpdate;

        public delegate void TaskDoneEventHandler(Object sender);
        public event TaskDoneEventHandler OnFinish;
    }

    class OmletteAsync : TaskExecutorAsync
    {
        public OmletteAsync(string name) : base(name, 90000)
        {

        }
    }

    class CucumberAsync : TaskExecutorAsync
    {
        public CucumberAsync(string name) : base(name, 5000)
        { }
    }

    class TomatoAsync : TaskExecutorAsync
    {
        public TomatoAsync(string name) : base(name, 1000)
        { }
    }

    class ToastAsync : TaskExecutorAsync
    {
        public ToastAsync(string name) : base(name, 30000)
        {

        }
    }

}
