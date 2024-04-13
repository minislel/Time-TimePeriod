using System.Diagnostics;
using TimePeriodLibrary;
namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        [DataRow(0, 0, 0, 1, 0, 0, 1)]
        [DataRow(12, 0, 0, 1, 12, 0, 1)]
        [DataRow(0, 12, 0, 1, 0, 12, 1)]
        [DataRow(0, 0, 12, 1, 0, 0, 13)]
        public void TimeAddition(int a, int b, int c, int d, int f, int g, int h)
        {
            Time t1 = new Time(a, b, c);
            Time t2 = new Time(f, g, h);
            t1.Plus(new TimePeriod(d));
            Assert.AreEqual(t1, t2);
        }
        [TestMethod]
        [DataRow(0, 0, 0, 12)]
        [DataRow(12, 0, 12, 12)]

        public void RepeatedTimeTickAndPlusOperator(int a, int b, int c, int d)
        {
            Time t1 = new Time(a, b, c);
            Time t2 = t1 + new TimePeriod(d);
            for (int i = 0; i < d; i++)
            { t1.Tick(); }

            Assert.AreEqual(t1, t2);
        }
        [TestMethod]
        [DataRow(0, 0, 0, 12,12,12, 0,0,12, 12,12,24,12)]
        

        public void forEachTickLoopTest(int a, int b, int c, int d, int e, int f, int h, int i, int j, int k, int l, int m, int n)
        {
            Time t1 = new Time(a, b, c);
            Time t2 = new Time(d, e, f);
            Time t1e = new Time(h,i,j);
            Time t2e = new Time(k,l,m);
            List<Time> list = new List<Time>();
            List<Time> listExpected = new List<Time>();
            list.Add(t1);
            list.Add(t2);
            listExpected.Add(t1e);
            listExpected.Add(t2e);
            for (int ij = 0; ij < list.Count; ij++)
            { 
                for (int z = 0; z < n; z++)
                { list[ij] = list[ij] + new TimePeriod(1); }
                Debug.WriteLine(list[ij].ToString());
            }

            
            Debug.WriteLine(String.Join(", ", list.ToArray()));


            Assert.AreEqual(list[0], listExpected[0]);

        }
        [TestMethod]
        [DataRow(12, 12, 12, 2, 2, 2, 10, 10, 10)]
        public void subtractionTest(int a, int b, int c, int d, int e, int f, int g, int h, int i)
        { 
            Time t1 = new Time(a,b,c);
            TimePeriod timePeriod = new TimePeriod(d,e,f);
            Time expected = new Time(g,h,i);
            Assert.Equals(expected, t1-timePeriod);
        }


    }
}