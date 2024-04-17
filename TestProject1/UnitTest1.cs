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
            Assert.AreEqual(expected, t1-timePeriod);
        }
        [TestMethod]
        [DataRow(12, 12, 12, 12, 12, 12, true)]
        [DataRow(12, 12, 12, 10, 10, 10, false)]
        public void EqualityTest(int a, int b, int c, int d, int e, int f, bool expected)
        {
            Time t1 = new Time(a, b, c);
            Time t2 = new Time(d, e, f);
            Assert.AreEqual(expected, t1 == t2);
        }

        [TestMethod]
        [DataRow(12, 12, 12, 12, 12, 12, false)]
        [DataRow(12, 12, 12, 10, 10, 10, true)]
        public void InequalityTest(int a, int b, int c, int d, int e, int f, bool expected)
        {
            Time t1 = new Time(a, b, c);
            Time t2 = new Time(d, e, f);
            Assert.AreEqual(expected, t1 != t2);
        }

        [TestMethod]
        [DataRow(12, 12, 12, 10, 10, 10, true)]
        [DataRow(12, 12, 12, 12, 12, 12, false)]
        public void GreaterThanTest(int a, int b, int c, int d, int e, int f, bool expected)
        {
            Time t1 = new Time(a, b, c);
            Time t2 = new Time(d, e, f);
            Assert.AreEqual(expected, t1 > t2);
        }

        [TestMethod]
        [DataRow(12, 12, 12, 10, 10, 10, false)]
        [DataRow(12, 12, 12, 12, 12, 12, false)]
        public void LessThanTest(int a, int b, int c, int d, int e, int f, bool expected)
        {
            Time t1 = new Time(a, b, c);
            Time t2 = new Time(d, e, f);
            Assert.AreEqual(expected, t1 < t2);
        }

        [TestMethod]
        [DataRow(12, 12, 12, 12, 12, 12, true)]
        [DataRow(12, 12, 12, 10, 10, 10, true)]
        [DataRow(9, 12, 12, 10, 10, 10, false)]
        [DataRow(12, 12, 12, 12, 12, 12, true)]
        public void GreaterThanOrEqualTest(int a, int b, int c, int d, int e, int f, bool expected)
        {
            Time t1 = new Time(a, b, c);
            Time t2 = new Time(d, e, f);
            Assert.AreEqual(expected, t1 >= t2);
        }

        [TestMethod]
        [DataRow(12, 12, 12, 10, 10, 10, false)]
        [DataRow(12, 12, 12, 12, 12, 12, true)]
        [DataRow(12, 12, 12, 12, 12, 12, true)]
        public void LessThanOrEqualTest(int a, int b, int c, int d, int e, int f, bool expected)
        {
            Time t1 = new Time(a, b, c);
            Time t2 = new Time(d, e, f);
            Assert.AreEqual(expected, t1 <= t2);
        }

        [TestMethod]
        [DataRow(12, 12, 12, 12, 12, 12, true)]
        [DataRow(12, 12, 12, 10, 10, 10, false)]
        public void EqualsMethodTest(int a, int b, int c, int d, int e, int f, bool expected)
        {
            Time t1 = new Time(a, b, c);
            Time t2 = new Time(d, e, f);
            Assert.AreEqual(expected, t1.Equals(t2));
        }
        [TestMethod]
        [DataRow(12, 12, 12, 2, 2, 2, 14, 14, 14)]
        [DataRow(20, 45, 56, 1, 15, 30, 22, 1, 26)]
        public void PlusTest(int a, int b, int c, int d, int e, int f, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            Time t1 = new Time(a, b, c);
            TimePeriod t2 = new TimePeriod(d, e, f);
            Time expected = new Time(expectedHours, expectedMinutes, expectedSeconds);

            t1.Plus(t2);


            Assert.AreEqual(expected, t1);
        }
        [TestMethod]
        [DataRow(12, 12, 12, 2, 2, 2, 10, 10, 10)]
        [DataRow(23, 45, 56, 1, 15, 30, 22, 30, 26)]
        public void MinusTest(int a, int b, int c, int d, int e, int f, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            Time t1 = new Time(a, b, c);
            TimePeriod timePeriod = new TimePeriod(d, e, f);
            Time expected = new Time(expectedHours, expectedMinutes, expectedSeconds);
            t1.Minus(timePeriod);
            Assert.AreEqual(expected, t1);
        }
        [TestMethod]
        [DataRow(2, 30, 10, 1, 45, 30, 4, 15, 40)]
        [DataRow(2, 30, 10, 0, 45, 30, 3, 15, 40)]
        public void PlusTest_TP(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            TimePeriod tp1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod tp2 = new TimePeriod(hours2, minutes2, seconds2);
            TimePeriod expected = new TimePeriod(expectedHours, expectedMinutes, expectedSeconds);

            tp1.Plus(tp2);

            Assert.AreEqual(expected, tp1);
        }

        [TestMethod]
        [DataRow(2, 30, 10, 1, 45, 30, 0, 44, 40)]
        [DataRow(2, 30, 10, 0, 45, 30, 1, 44, 40)]
        public void MinusTest_TP(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            TimePeriod tp1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod tp2 = new TimePeriod(hours2, minutes2, seconds2);
            TimePeriod expected = new TimePeriod(expectedHours, expectedMinutes, expectedSeconds);

            tp1.Minus(tp2);

            Assert.AreEqual(expected, tp1);
        }

        [TestMethod]
        [DataRow(2, 30, 10, 1, 45, 30, false)]
        [DataRow(2, 30, 10, 2, 30, 10, true)]
        [DataRow(2, 30, 10, 3, 15, 30, false)]
        public void EqualityTest_TP(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod tp2 = new TimePeriod(hours2, minutes2, seconds2);

            Assert.AreEqual(expected, tp1 == tp2);
        }

        [TestMethod]
        [DataRow(2, 30, 10, 1, 45, 30, true)]
        [DataRow(2, 30, 10, 2, 30, 10, false)]
        [DataRow(2, 30, 10, 3, 15, 30, true)]
        public void InequalityTest_TP(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod tp2 = new TimePeriod(hours2, minutes2, seconds2);

            Assert.AreEqual(expected, tp1 != tp2);
        }
        [TestMethod]
        [DataRow(2, 30, 10, 1, 45, 30, false)]
        [DataRow(2, 30, 10, 2, 30, 10, false)]
        [DataRow(2, 30, 10, 3, 15, 30, true)]
        public void LessThanTest_TP(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod tp2 = new TimePeriod(hours2, minutes2, seconds2);

            Assert.AreEqual(expected, tp1 < tp2);
        }

        [TestMethod]
        [DataRow(2, 30, 10, 1, 45, 30, true)]
        [DataRow(2, 30, 10, 2, 30, 10, false)]
        [DataRow(2, 30, 10, 3, 15, 30, false)]
        public void GreaterThanTest_TP(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod tp2 = new TimePeriod(hours2, minutes2, seconds2);

            Assert.AreEqual(expected, tp1 > tp2);
        }

        [TestMethod]
        [DataRow(2, 30, 10, 1, 45, 30, false)]
        [DataRow(2, 30, 10, 2, 30, 10, true)]
        [DataRow(2, 30, 10, 3, 15, 30, true)]
        public void LessThanOrEqualTest_TP(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod tp2 = new TimePeriod(hours2, minutes2, seconds2);

            Assert.AreEqual(expected, tp1 <= tp2);
        }

        [TestMethod]
        [DataRow(2, 30, 10, 1, 45, 30, true)]
        [DataRow(2, 30, 10, 2, 30, 10, true)]
        [DataRow(2, 30, 10, 3, 15, 30, false)]
        public void GreaterThanOrEqualTest_TP(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2, bool expected)
        {
            TimePeriod tp1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod tp2 = new TimePeriod(hours2, minutes2, seconds2);

            Assert.AreEqual(expected, tp1 >= tp2);
        }
        [TestMethod]
        [DataRow(2, 30, 10, 1, 45, 30, 4, 15, 40)]
        [DataRow(1, 45, 30, 0, 15, 30, 2, 1, 0)]
        public void AdditionTest_TP(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            TimePeriod tp1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod tp2 = new TimePeriod(hours2, minutes2, seconds2);
            TimePeriod expected = new TimePeriod(expectedHours, expectedMinutes, expectedSeconds);

            TimePeriod result = tp1 + tp2;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [DataRow(2, 30, 10, 1, 45, 30, 0, 44, 40)]
        [DataRow(2, 0, 0, 0, 30, 0, 1, 30, 0)]
        public void SubtractionTest_TP(int hours1, int minutes1, int seconds1, int hours2, int minutes2, int seconds2, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            TimePeriod tp1 = new TimePeriod(hours1, minutes1, seconds1);
            TimePeriod tp2 = new TimePeriod(hours2, minutes2, seconds2);
            TimePeriod expected = new TimePeriod(expectedHours, expectedMinutes, expectedSeconds);

            TimePeriod result = tp1 - tp2;

            Assert.AreEqual(expected, result);
        }





    }
}