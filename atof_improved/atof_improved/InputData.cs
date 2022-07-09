using System;

namespace atof_improved
{
    public class InputData : Input 
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime DateTime { get; set; }
        public double ResultValue { get; set; }
        public bool Error { get; set; }
    }
}
