using System;
using System.Collections.Generic;
using System.Text;

namespace RandomLsfr
{
    public class FlipFlop
    {
        bool d;
        public bool Q;
        public FlipFlop(bool initValue)
        {
            d = initValue;
            Q = d;
        }

        public void Clock(bool D)
        {
            Q = D;
        }
    }
}
