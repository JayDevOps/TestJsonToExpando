using System;
using System.Collections.Generic;
using System.Text;

namespace TestJsonToExpando
{
    public class Sample
    {
        public int[] A { get; set; }
        public bool B { get; set; }
        public Sample C { get; set; }

        public static Sample Create()
        {
            return new Sample
            {
                A = new[] { 1, 2, 3 },
                C = new Sample
                {
                    A = new[] { 6, 5, 4 },
                    B = true
                }
            };
        }
    }
}
