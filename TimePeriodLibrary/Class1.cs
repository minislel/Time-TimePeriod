using System.Buffers;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace TimePeriodLibrary
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        
        private byte _hours = 0;
        private byte _minutes = 0;
        private byte _seconds;
        public byte Hours { get { return _hours; } private set { if (value < 24) { _hours = value; } else { throw new ArgumentOutOfRangeException(); } } }
        public byte Minutes { get { return _minutes; } private set { if (value < 60) { _minutes = value; } else { throw new ArgumentOutOfRangeException(); } } }
        public byte Seconds { get { return _seconds; } private set { if (value < 60) { _seconds = value; } else { throw new ArgumentOutOfRangeException(); } } }
        

        public Time(byte HH=0, byte MM=0, byte SS=0)
        {
            if (HH > 23 || MM > 59 || SS > 59)
            { throw new ArgumentOutOfRangeException(); }
            Hours = HH;
            Minutes = MM;
            Seconds = SS;
        }
        public Time(string s)
        {
            string[] strings = s.Split(":");
            if (strings.Length != 3)
            {
                { throw new FormatException(); }
            }
            byte HH;
            byte MM;
            byte SS;
            try
            {
                HH = byte.Parse(strings[0]);
                MM = byte.Parse(strings[1]);
                SS = byte.Parse(strings[2]);
            }
            catch  
            { 
                throw new FormatException();
            }
            if (HH > 23 || MM > 59 || SS > 59)
            { throw new ArgumentOutOfRangeException(); }
            Hours = HH;
            Minutes = MM;
            Seconds = SS;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("", 8);
            if (Hours < 10)
            { sb.Append(new char[] {'0', (char)Hours, ':'}); }
            else
            { sb.Append(new char[] {(char)Hours, ':' }); }
            if (Minutes < 10)
            { sb.Append(new char[] { '0', (char)Minutes, ':' }); }
            else
            { sb.Append(new char[] { (char)Minutes, ':' }); }
            if (Seconds < 10)
            { sb.Append(new char[] { '0', (char)Seconds}); }
            else
            { sb.Append(new char[] { (char)Seconds}); }
            return sb.ToString();
        }

        public int CompareTo(Time other)
        {
            if (this.Hours > other.Hours)
            { return -1; }
            else if (this.Hours < other.Hours)
            { return 1; }
            else if (this.Hours == other.Hours)
            {
                if (this.Minutes > other.Minutes)
                { return -1; }
                else if (this.Minutes < other.Minutes)
                { return 1; }
                else if (this.Minutes == other.Minutes)
                {
                    if (this.Seconds > other.Seconds)
                    { return -1; }
                    else if (this.Seconds < other.Seconds)
                    { return 1; }
                    else if (this.Seconds == other.Seconds)
                    { return 0; }
                }
            }
            return 0;
        }
        public static bool operator <(Time A, Time B)
        {
            if (A.Hours > B.Hours)
            { return false; }
            else if (A.Hours < B.Hours)
            { return true; }
            else if (A.Hours == B.Hours)
            {
                if (A.Minutes > B.Minutes)
                { return false; }
                else if (A.Minutes < B.Minutes)
                { return true; }
                else if (A.Minutes == B.Minutes)
                {
                    if (A.Seconds > B.Seconds)
                    { return false; }
                    else if (A.Seconds < B.Seconds)
                    { return true; }
                    else if (A.Seconds == B.Seconds)
                    { return false; }
                }
            }
            return false;
        }
        public static bool operator >(Time A, Time B)
        {
            return !(A<B);
        }
        public static bool operator >=(Time A, Time B)
        {
            if (A == B)
            { return true; }
            else return A > B;     
        }
        public static bool operator <=(Time A, Time B)
        {
            if (A == B)
            { return true; }
            else return A < B;
        }
        public static bool operator ==(Time A, Time B)
        {
            return A.Equals(B);
        }
        public static bool operator !=(Time A, Time B)
        {
            return !A.Equals(B);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null)
            { return false; }
            return Equals(obj);
        }
        public bool Equals(Time other)
        {
            if (this.Hours == other.Hours && this.Minutes == other.Minutes && this.Seconds == other.Seconds)
            { return true; }
            else { return false; }
        }

        public override int GetHashCode()
        {
            return (int)Hours ^ (int)Minutes ^ (int)Seconds;
        }
        public void Plus(TimePeriod TP)
        {
            if ((byte)(this.Hours + TP.Hours) < 24) { Hours = (byte)(this.Hours + TP.Hours); }
            else { throw new ArgumentOutOfRangeException(); }
            if((byte)(this.Minutes + TP.Minutes)<60) { Minutes = (byte)(this.Minutes + TP.Minutes); }
            else { Minutes = (byte)((this.Minutes + TP.Minutes)%60); Hours = (byte)(this.Hours + ((this.Minutes + TP.Minutes) / 60)); }
            if ((byte)(this.Seconds + TP.Seconds) < 60) { Seconds = (byte)(this.Seconds + TP.Seconds); }
            else { Minutes = (byte)(this.Minutes + (TP.Seconds / 60)); Seconds = (byte)((this.Seconds + TP.Seconds) % 60); }
        }
        public void Minus(TimePeriod TP)
        {
            if ((sbyte)(this.Hours - TP.Hours) > 0) { Hours = (byte)(this.Hours - TP.Hours); }
            else { throw new ArgumentOutOfRangeException(); }
            if ((sbyte)(this.Minutes - TP.Minutes) > 0) { Minutes = (byte)(this.Minutes - TP.Minutes); }
            else { Hours -= (byte)Math.Abs(Math.Ceiling((decimal)((this.Minutes - TP.Minutes)/60))); Minutes -= (byte)(TP.Minutes % 60); }
            if ((byte)(this.Seconds - TP.Seconds) > 0) { Seconds = (byte)(this.Seconds - TP.Seconds); }
            else { Minutes -= (byte)Math.Abs(Math.Ceiling((decimal)(this.Seconds - TP.Seconds)/60)); Seconds -= (byte)(TP.Seconds % 60); }
        }
    }
    public struct TimePeriod 
    {
        private long _seconds;
        public long SecondsTotal
        {
            get
            {
                return _seconds;
            }
         private set 
            {
                _seconds = value;
            }
        }
        public long Seconds
        {
            get 
            {
                if (SecondsTotal % 3600 > 0)
                {
                    return (SecondsTotal % 3600) % 60;
                }
                else if (SecondsTotal % 60 > 0)
                {
                    return SecondsTotal % 60;
                }
                else return SecondsTotal;
            }
        }
        public long Minutes
        {
            get
            {
                return SecondsTotal % 3600 / 60;
            }
        }
        public long Hours
        {
            get { return SecondsTotal / 3600; }
        }
        public TimePeriod(long seconds)
        {
            SecondsTotal = seconds;
        }
        public TimePeriod(long minutes, long seconds) 
        { 
            SecondsTotal= seconds + minutes * 60;
        }
        public TimePeriod(long hours, long minutes, long seconds)
        {
            SecondsTotal = seconds + minutes * 60 + hours * 3600;
        }
        public TimePeriod(string input) 
        {
            long H;
            long M;
            long S;
           string[] strings = input.Split(":");
            if (strings.Length == 3 && long.TryParse(strings[0], out H) && long.TryParse(strings[1], out M) && long.TryParse(strings[2], out S))
            {
                SecondsTotal = S + M * 60 + H * 3600;
            }
            else if (strings.Length == 2 && long.TryParse(strings[0], out M) && long.TryParse(strings[1], out S))
            {
                SecondsTotal = S + M * 60;
            }
            else if (strings.Length == 1 && long.TryParse(strings[0], out S))
            {
                SecondsTotal = S;
            }
            else throw new ArgumentException("invalid input string format");
        }
        public override string ToString()
        {
/*            long H = SecondsTotal / 3600;
            long M = SecondsTotal / 60;
            long S = SecondsTotal;
            if (SecondsTotal % 3600 > 0)
            {
                M = (SecondsTotal % 3600) / 60;
                if ((SecondsTotal % 3600) % 60 > 0)
                    { S = (SecondsTotal % 3600) % 60; }
                else { SecondsTotal = 0; }
            }
            else if (SecondsTotal % 60 > 0)
            {
                M = SecondsTotal / 60;
                S = SecondsTotal % 60;
            }
            else
             { S = SecondsTotal; }*/
            return $"{Hours}:{Minutes}:{Seconds}";
        }


    }
}
