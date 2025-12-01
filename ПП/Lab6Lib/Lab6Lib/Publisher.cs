using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6Lib
{
    public class Publisher
    {
        List<ISubscriber> list = new List<ISubscriber>();
        private string _eventname;
        public Publisher(string eventname)
        {
            _eventname = eventname;
        }
        public void subscribe(ISubscriber subscriber)
        {
            foreach(var sub in list)
            {
                if(sub == subscriber)
                {
                    return;
                }
            }
            list.Add(subscriber);
        }
        public bool unsubscribe(ISubscriber subscriber)
        {
            if (list.Remove(subscriber)) return true;
            else return false;
        }
        public int nonify()
        {
            foreach (var sub in list)
            {
                sub.update(_eventname);
            }
            return list.Count();
        }
    }    
}
