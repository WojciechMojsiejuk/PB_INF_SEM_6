using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomLsfr
{
    class Lsfr
    {
        List<FlipFlop> flipFlops = new List<FlipFlop>();
        List<bool> polynomial = new List<bool>();
        public Lsfr(string poly, string seed)
        {
            foreach(char c in seed)
            {
                flipFlops.Add(new FlipFlop(Program.ParseToBool(c)));
            }
            foreach(char p in poly)
            {
                polynomial.Add(Program.ParseToBool(p));
            }
        }

        public bool[] Clock()
        {
            //Xor (calculate first bit)
            List<bool> flipFlopToXor = new List<bool>();
            for(int i=0;i<polynomial.Count;i++)
            {
                if (polynomial[i] == true)
                {
                    flipFlopToXor.Add(flipFlops[i].Q);
                }
            }
            bool currentInput = Program.Xor(flipFlopToXor.ToArray());
            //shift
            bool[] previousValues = new bool[flipFlops.Count];
            for (int i = 0; i < previousValues.Length; i++)
                previousValues[i] = flipFlops[i].Q;
            for(int i=1;i<flipFlops.Count;i++)
            {
                flipFlops[i].Q = previousValues[i - 1];
            }
            //set first bit
            flipFlops[0].Q = currentInput;
            return GetValue();
        }

        public bool[] GetValue()
        {
            bool[] value = new bool[flipFlops.Count];
            for (int i = 0; i < flipFlops.Count; i++)
            {
                value[i] = flipFlops[i].Q;
            }
            return value;
        }
    }
}
