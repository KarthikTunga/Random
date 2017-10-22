using System;

namespace Code.RainDrops
{
    class RainDrops
    {
        static void Main(string[] args)
        {
            try
            {
                double[] input = new double[]{0.0,0.2,0.3,0.4,0.6, 1.0, 1.5, 2.0};
                var output = new RainDrops().Covered(input);
                Console.WriteLine(output);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public bool Covered(double[] points)
        {
            int size = 4;
            Tuple<double,double>[] slots = new Tuple<double, double>[size];
            
            for(int i = 0;i < size; ++i)
            {
                slots[i] = Tuple.Create(i * 0.5, (i * 0.5 + 0.5));
            }

            for(int i = 0; i < points.Length; ++i)
            {
                double start = points[i] - 0.25;
                double end = points[i] + 0.25;

                int startIndex = (int)(start / 0.5);
                int endIndex = (int)(end / 0.5);

                if(startIndex >=0 && slots[startIndex].Item2 > start) 
                {
                    slots[startIndex] = Tuple.Create(slots[startIndex].Item1, start);
                }

                if(endIndex < size && slots[endIndex].Item1 < end) 
                {
                    slots[endIndex] = Tuple.Create(end, slots[endIndex].Item2);
                }
            }

            for(int i = 0; i < size; ++i)
            {
                if(slots[i].Item1 < slots[i].Item2) return false;
            }

            return true;
        }
    }
}
