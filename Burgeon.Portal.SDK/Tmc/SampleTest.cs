using System;
using System.Collections.Generic;
using System.Text;
using Top.Tmc;
using System.Threading;

namespace Top.Api.Tmc
{
    class SampleTest
    {
        public void main()
        {
            int i = 0;
            var c = new TmcClient("445663", "c4ab31cce352aa5df07caf842bb9f48c");
            c.OnMessage += (o, e) =>
            {
                if (Interlocked.Increment(ref i) % 1000 == 0)
                    Console.WriteLine(DateTime.Now + " 1000 message received");
            };
            c.Connect("ws://10.235.174.30:8000/");

            Thread.Sleep(1000 * 60 * 100);
        }
    }
}
