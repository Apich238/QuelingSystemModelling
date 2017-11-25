using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ
{
    internal static class modeller
    {
        public static ModellingLog Run(model m,double tmax=100)
        {
            double currtime = 0;
            PQueue q = new PQueue();
            //fill pqueue
            while(currtime<tmax && q.Count > 0)
            {
                var e = q.Dequeue();
                //process event
            }



            return null;
        }
    }
    internal class ModellingLog
    {

    }

}
