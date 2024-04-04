using System.Buffers;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace TimePeriodLibrary
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        
        private byte _hours;
        private byte _minutes;
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
            if (A.Hours > B.Hours)
            { return true; }
            else if (A.Hours < B.Hours)
            { return false; }
            else if (A.Hours == B.Hours)
            {
                if (A.Minutes > B.Minutes)
                { return true; }
                else if (A.Minutes < B.Minutes)
                { return false; }
                else if (A.Minutes == B.Minutes)
                {
                    if (A.Seconds > B.Seconds)
                    { return true; }
                    else if (A.Seconds < B.Seconds)
                    { return false; }
                    else if (A.Seconds == B.Seconds)
                    { return false; }
                }
            }
            return false;
        }
        public static bool operator >=(Time A, Time B)
        {
            if (A.Hours > B.Hours)
            { return true; }
            else if (A.Hours < B.Hours)
            { return false; }
            else if (A.Hours == B.Hours)
            {
                if (A.Minutes > B.Minutes)
                { return true; }
                else if (A.Minutes < B.Minutes)
                { return false; }
                else if (A.Minutes == B.Minutes)
                {
                    if (A.Seconds > B.Seconds)
                    { return true; }
                    else if (A.Seconds < B.Seconds)
                    { return false; }
                    else if (A.Seconds == B.Seconds)
                    { return true; }
                }
            }
            return false;
        }
        public static bool operator <=(Time A, Time B)
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
                    { return true; }
                }
            }
            return false;
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
    }
    public struct TimePeriod 
    {
        private long _seconds;
        public long Seconds
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
        public long Minutes
        {
            get { return _seconds/60; }
            private set 
            { 
                _seconds = value*60;
            }
        }
        public long Hours
        {
            get { return _seconds / 3600; }
            private set
            {
                _seconds = value * 3600;
            }
        }
        public TimePeriod(long seconds)
        {
            Seconds = seconds;
        }
        public TimePeriod(long minutes, long seconds) 
        { 
            Seconds= seconds;
            Seconds += minutes * 60;
        }
        public TimePeriod(long hours, long minutes, long seconds)
        {
            Seconds = seconds;
            Seconds += minutes * 60;
            Seconds += hours * 3600;
        }

    }
}
