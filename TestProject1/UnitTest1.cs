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
        [TestMethod]
        [DataRow("01:01:00",3660)]
        [DataRow("62:01:00", 3660, 3660)]
        [DataRow("03:02:00", 1, 61, 3660)]
        public void TimePeriod_Constructor(string expected, params long[] values)
        {
            TimePeriod timePeriod = new TimePeriod();
            if (values.Length == 1)
                timePeriod = new TimePeriod(values[0]);
            else if (values.Length == 2)
                timePeriod = new TimePeriod(values[0], values[1]);
            else if (values.Length == 3)
                timePeriod = new TimePeriod(values[0], values[1], values[2]);

            Assert.AreEqual(expected, timePeriod.ToString());
        }

        [TestMethod]
        [DataRow("01:30:45")]
        [DataRow("02:10:20")]
        [DataRow("05:00:00")]
        public void TimePeriod_StringConstructor(string timePeriodString)
        {
            TimePeriod timePeriod = new TimePeriod(timePeriodString);
            Assert.AreEqual(int.Parse(timePeriodString.Substring(0, 2)), timePeriod.Hours);
            Assert.AreEqual(int.Parse(timePeriodString.Substring(3, 2)), timePeriod.Minutes);
            Assert.AreEqual(int.Parse(timePeriodString.Substring(6, 2)), timePeriod.Seconds);
        }

        [TestMethod]
        [DataRow("10:30:00", "12:00:00", 1, 30, 0)]
        [DataRow("00:00:00", "23:59:59", 23, 59, 59)]
        public void TimePeriod_TwoTimeConstructor(string startTimeString, string endTimeString, int expectedHours, int expectedMinutes, int expectedSeconds)
        {
            Time startTime = new Time(startTimeString);
            Time endTime = new Time(endTimeString);
            TimePeriod timePeriod = new TimePeriod(startTime, endTime);

            Assert.AreEqual(expectedHours, timePeriod.Hours);
            Assert.AreEqual(expectedMinutes, timePeriod.Minutes);
            Assert.AreEqual(expectedSeconds, timePeriod.Seconds);
        }
        [TestMethod]
        [DataRow(0, 0, 0)]
        [DataRow(10, 30, 45)]
        [DataRow(23, 59, 59)]
        public void Time_IntConstructor_ShouldCreateTimeWithSpecifiedValues(int hours, int minutes, int seconds)
        {
            Time time = new Time(hours, minutes, seconds);
            Assert.AreEqual(hours, time.Hours);
            Assert.AreEqual(minutes, time.Minutes);
            Assert.AreEqual(seconds, time.Seconds);
        }

        [TestMethod]
        [DataRow(36610)]
        [DataRow(86390)]
        public void Time_LongConstructor(long totalSeconds)
        {
            Time time = new Time(totalSeconds);
            Assert.AreEqual(totalSeconds / 3600, time.Hours);
            Assert.AreEqual((totalSeconds % 3600) / 60, time.Minutes);
            Assert.AreEqual(totalSeconds % 60, time.Seconds);
        }

        [TestMethod]
        [DataRow("00:00:00")]
        [DataRow("10:30:45")]
        [DataRow("23:59:59")]
        public void Time_StringConstructor(string timeString)
        {
            Time time = new Time(timeString);
            Assert.AreEqual(int.Parse(timeString.Substring(0, 2)), time.Hours);
            Assert.AreEqual(int.Parse(timeString.Substring(3, 2)), time.Minutes);
            Assert.AreEqual(int.Parse(timeString.Substring(6, 2)), time.Seconds);
        }
        [TestMethod]
        [DataRow((byte)12, (byte)30, (byte)0, (byte)12, (byte)45, (byte)0, -1)]
        [DataRow((byte)12, (byte)45, (byte)0, (byte)13, (byte)0, (byte)0, -1)]
        [DataRow((byte)12, (byte)45, (byte)0, (byte)12, (byte)45, (byte)0, 0)]   
        public void Time_Comparable(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2, int expectedComparison)
        {

            Time time1 = new Time(h1, m1, s1);
            Time time2 = new Time(h2, m2, s2);

            int comparisonResult = time1.CompareTo(time2);

            Assert.AreEqual(expectedComparison, comparisonResult);
        }

        [TestMethod]
        [DataRow(3600, 1800, 0, 3600, 1800, 0, 0)]
        [DataRow(3600, 1800, 0, 7200, 3600, 0, -1)] 
        [DataRow(7200, 3600, 0, 3600, 1800, 0, 1)]  
        public void TimePeriod_Comparable(long h1, long m1, long s1, long h2, long m2, long s2, int expectedComparison)
        {
            TimePeriod timePeriod1 = new TimePeriod(h1, m1, s1);
            TimePeriod timePeriod2 = new TimePeriod(h2, m2, s2);

            int comparisonResult = timePeriod1.CompareTo(timePeriod2);

            Assert.AreEqual(expectedComparison, comparisonResult);
        }






    }
}