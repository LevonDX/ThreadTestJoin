namespace ThreadTest4
{
    class Range
    {
        public int Min { get; set; }
        public int Max { get; set; }
    }

    internal class Program
    {
        static void Main()
        {
            bool stopThread  = false;

            int[] partialSums = new int[3];

            Thread t1 = new Thread(() =>
            {
                int s = 0;
                for (int i = 0; i < 1000; i++)
                {
                    s += i;
                }

                partialSums[0] += s;
            });
            Thread t2 = new Thread(() =>
            {
                int s = 0;
                for (int i = 1000; i < 2000; i++)
                {
                    if(stopThread)
                    {
                        break;
                    }
                    s += i;
                }

                partialSums[1] += s;
            });
            Thread t3 = new Thread(() =>
            {
                int s = 0;
                for (int i = 2000; i < 3001; i++)
                {
                    s += i;
                }

                partialSums[2] += s;
            });

            t1.Start();
            t2.Start();
            t3.Start();

            //while (t1.ThreadState == ThreadState.Running
            //    || t2.ThreadState == ThreadState.Running
            //    || t3.ThreadState == ThreadState.Running) ;

            stopThread = true;

            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine(partialSums.Sum());
        }
    }
}