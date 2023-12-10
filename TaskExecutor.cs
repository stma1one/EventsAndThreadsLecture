using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreCollectionsAsync
{
    class TaskExecutor
    {
        private string name;
        private int timeInMiliSec;
        public TaskExecutor(string name, int ms)
        {
            this.timeInMiliSec = ms;
            this.name = name;
        }

        public string Name { get { return this.name; } }
        public void Start()
        {
            #region With Events
            for (int i = 0; i < 25; i++)
            {
                Thread.Sleep(this.timeInMiliSec / 25);
                if (OnProgressUpdate != null)
                    OnProgressUpdate(this, (i + 1) * 4);
            }
            if (OnFinish != null)
                OnFinish(this);
            #endregion
        }
        #region With Events
        public delegate void ProgressEventHandler(Object sender, int percent);
        public event ProgressEventHandler OnProgressUpdate;

        public delegate void TaskDoneEventHandler(Object sender);
        public event TaskDoneEventHandler OnFinish;
        #endregion
    }

    class Omlette : TaskExecutor
    {
        public Omlette(string name) : base(name, 15000)
        {

        }
    }

    class Cucumber : TaskExecutor
    {
        public Cucumber(string name) : base(name, 1000)
        { }
    }

    class Tomato : TaskExecutor
    {
        public Tomato(string name) : base(name, 1000)
        { }
    }

    class Toast : TaskExecutor
    {
        public Toast(string name) : base(name, 7500)
        {

        }
    }

}
