﻿using System.Buffers;
using System.ComponentModel;
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

        public event PropertyChangedEventHandler? PropertyChanged;

        public byte Hours { get { return _hours; } private set { if (value < 24) { _hours = value;  } else { throw new ArgumentOutOfRangeException(); } } }
        public byte Minutes { get { return _minutes; } private set { if (value < 60) { _minutes = value;  } else { throw new ArgumentOutOfRangeException(); } } }
        public byte Seconds { get { return _seconds; } private set { if (value < 60) { _seconds = value;  } else { throw new ArgumentOutOfRangeException(); } } }
        public int NumberOfSecondsFromMidnight { get { return Hours * 3600 + Minutes * 60 + Seconds; } }

        public int Id { get; set; }


        public Time(byte HH=0, byte MM=0, byte SS=0)
        {
            if (HH > 23 || MM > 59 || SS > 59) 
            { throw new ArgumentOutOfRangeException(); }
            Hours = HH;
            Minutes = MM;
            Seconds = SS;
            Id = Guid.NewGuid().GetHashCode();
        }
        public Time(int HH = 0, int MM = 0, int SS = 0)
        {
            if (HH > 23 || MM > 59 || SS > 59)
            { throw new ArgumentOutOfRangeException(); }
            Hours = (byte)HH;
            Minutes = (byte)MM;
            Seconds = (byte)SS;
            Id = Guid.NewGuid().GetHashCode();
        }
        public Time(long SS = 1) 
        { 
            if(SS>86400)
            { throw new ArgumentOutOfRangeException(); }
            Hours = (byte)(SS / 3600);
            Minutes = (byte)((SS %3600) /60);
            Seconds = (byte)(((SS%3600)%60));
            Id = Guid.NewGuid().GetHashCode();
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
            Id = Guid.NewGuid().GetHashCode();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("", 8);
            if (Hours < 10)
            {
              sb.Append('0');
              sb.Append(Hours);
                sb.Append(':');
            }

            else
            { 
                sb.Append(Hours);
                sb.Append(':');
            }
            if (Minutes < 10)
            {
                sb.Append('0');
                sb .Append(Minutes);
                sb.Append(':');
            }
            else
            { 
                sb.Append(Minutes);
                sb.Append(':');
            }
            if (Seconds < 10)
            { 
              sb.Append("0");
              sb.Append (Seconds);
                
            }
            else
            {
              sb.Append(Seconds);
                
            }
            return sb.ToString();
        }

        public int CompareTo(Time other) => (this == other ? 0 : (this > other ? -1 : 1));
        public static bool operator <(Time A, Time B) => (A.NumberOfSecondsFromMidnight < B.NumberOfSecondsFromMidnight);
        public static bool operator >(Time A, Time B) => (A.NumberOfSecondsFromMidnight > B.NumberOfSecondsFromMidnight);
        public static bool operator >=(Time A, Time B) => A == B ? true : A > B;
        public static bool operator <=(Time A, Time B) => A == B ? true : A < B;
        public static bool operator ==(Time A, Time B) => A.Equals(B);
        public static bool operator !=(Time A, Time B) => !A.Equals(B);
        public override bool Equals(object? obj)
        {   
            if (obj == null) return false;
            if (obj is Time time)
            { return Equals(time); }
            return base.Equals(obj);
        }
        public bool Equals(Time other) => this.NumberOfSecondsFromMidnight == other.NumberOfSecondsFromMidnight;
        public override int GetHashCode() => (int)Hours ^ (int)Minutes ^ (int)Seconds;
        public void Plus(TimePeriod TP)
        {
            int totalSeconds = NumberOfSecondsFromMidnight + (int)TP.SecondsTotal;
            int newHours = totalSeconds / 3600 % 24;
            int newMinutes = (totalSeconds % 3600) / 60;
            int newSeconds = totalSeconds % 60;
            _hours = (byte)newHours;
            _minutes = (byte)newMinutes;
            _seconds = (byte)newSeconds;
        }
        public void Minus(TimePeriod TP)
        {
            int totalSeconds = NumberOfSecondsFromMidnight - (int)TP.SecondsTotal;
            if (totalSeconds < 0)
            {
                totalSeconds += 24 * 3600; 
            }
            int newHours = totalSeconds / 3600 % 24;
            int newMinutes = (totalSeconds % 3600) / 60;
            int newSeconds = totalSeconds % 60;
            _hours = (byte)newHours;
            _minutes = (byte)newMinutes;
            _seconds = (byte)newSeconds;
        }
        public static void Plus(Time This, TimePeriod TP) => This.Plus(TP);

        public static void Minus(Time This, TimePeriod TP) => This.Minus(TP);
        public static Time operator +(Time This, TimePeriod TP) 
        {
            This.Plus(TP);
            return This;
        }
        public static Time operator -(Time This, TimePeriod TP)
        {
            This.Minus(TP);
            return This;
        }
        public void Tick() => this.Plus(new TimePeriod(1));
        
    }
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        /*        private long _miliseconds;
                public long Miliseconds
                { get; private set; }
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
                    SecondsTotal = seconds + minutes * 60;
                }
                public TimePeriod(long hours, long minutes, long seconds, long milliseconds)
                {
                    SecondsTotal = seconds + minutes * 60 + hours * 3600 + milliseconds/1000;
                }
                public TimePeriod(Time A, Time B)
                {
                    long H = Math.Abs(A.Hours - B.Hours);
                    long M = Math.Abs(A.Minutes - B.Minutes);
                    long S = Math.Abs(A.Seconds - B.Seconds);
                    SecondsTotal = H * 3600 + M * 60 + S;
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
                    return $"{Hours}:{Minutes}:{Seconds}";
                }*/
        private long _seconds;
        private long _milliseconds;
        public int Id { get; set; }
        public long SecondsTotal
        {
            get { return _seconds; }
            private set { _seconds = value; }
        }


        public long Milliseconds
        {
            get { return _milliseconds; }
            private set { 
                if (value>=1000)
                { _milliseconds = 0;
                    SecondsTotal++;
                }
                else { _milliseconds = value; } 
            }
        }

        public long Seconds
        {
            get { return SecondsTotal % 60; }
        }

        public long Minutes
        {
            get { return SecondsTotal / 60 % 60; }
        }

        public long Hours
        {
            get { return SecondsTotal / (60 * 60); }
        }

        public TimePeriod(long seconds, long milliseconds)
        {
            SecondsTotal = seconds;
            Milliseconds = milliseconds;
            Id = Guid.NewGuid().GetHashCode();
        }

        public TimePeriod(long hours, long minutes, long seconds)
        {
            SecondsTotal = seconds + minutes * 60 + hours*3600;
            Id = Guid.NewGuid().GetHashCode();
        }

        public TimePeriod(long hours, long minutes, long seconds, long milliseconds)
        {
            SecondsTotal = seconds + minutes * 60 + hours * 3600;
            Milliseconds = milliseconds;
            Id = Guid.NewGuid().GetHashCode();
        }
        public TimePeriod(long seconds)
        {
            SecondsTotal = seconds;
            Id = Guid.NewGuid().GetHashCode();
        }

        public TimePeriod(Time A, Time B)
        {
            long H = Math.Abs(A.Hours - B.Hours);
            long M = Math.Abs(A.Minutes - B.Minutes);
            long S = Math.Abs(A.Seconds - B.Seconds);
            
            SecondsTotal = H * 3600 + M * 60 + S;
            Milliseconds = SecondsTotal*1000;
            Id = Guid.NewGuid().GetHashCode();
        }
        
        public TimePeriod(string input)
        {
            long H;
            long M;
            long S;
            long MS;
            string[] strings = input.Split(":");
            if (strings.Length == 3 && long.TryParse(strings[0], out H) && long.TryParse(strings[1], out M) && long.TryParse(strings[2], out S))
            {
                SecondsTotal = S + M * 60 + H * 3600;
                Milliseconds = 0;
            }
            else if (strings.Length == 2 && long.TryParse(strings[0], out M) && long.TryParse(strings[1], out S))
            {
                SecondsTotal = S + M * 60;
                Milliseconds = 0;
            }
            else if (strings.Length == 1 && long.TryParse(strings[0], out S))
            {
                SecondsTotal = S;
                Milliseconds = 0;
            }
            else if (strings.Length == 4 && long.TryParse(strings[0], out H) && long.TryParse(strings[1], out M) && long.TryParse(strings[2], out S) && long.TryParse(strings[3], out MS))
            {
                SecondsTotal = S + M * 60 + H * 3600;
                Milliseconds = MS;
            }
            else
            {
                throw new ArgumentException("invalid input string format");
            }
        }

        public override string ToString()
        {
            return $"{Hours}:{Minutes}:{Seconds}";
        }
        public string ToString(string format)
        { 
        if (format == "ms")
                return $"{Hours}:{Minutes}:{Seconds}:{Milliseconds}";
            return $"{Hours}:{Minutes}:{Seconds}";
        }
        public void MSTick() => this.Milliseconds++;
        public void Plus(TimePeriod other) => this.SecondsTotal += other.SecondsTotal;
        public void Minus(TimePeriod other) => this.SecondsTotal -= other.SecondsTotal;
        public static void Plus(TimePeriod This, TimePeriod other) => This.SecondsTotal += other.SecondsTotal;
        public static void Minus(TimePeriod This, TimePeriod other) => This.SecondsTotal -= other.SecondsTotal;
        public int CompareTo(TimePeriod other) => (this.SecondsTotal == other.SecondsTotal ? 0 : (this.SecondsTotal > other.SecondsTotal ? -1 : 1));
        public override bool Equals(object? obj)
        {
            if (obj == null)
            { return false; }
            return Equals(obj);
        }

        public bool Equals(TimePeriod other) => (this.SecondsTotal == other.SecondsTotal);

        public override int GetHashCode() => this.SecondsTotal.GetHashCode();

        public static bool operator <(TimePeriod A, TimePeriod B) => (A.SecondsTotal < B.SecondsTotal);
        public static bool operator >(TimePeriod A, TimePeriod B) => (A.SecondsTotal > B.SecondsTotal);
        public static bool operator <=(TimePeriod A, TimePeriod B) => (A.SecondsTotal <= B.SecondsTotal);
        public static bool operator >=(TimePeriod A, TimePeriod B) => (A.SecondsTotal >= B.SecondsTotal);
        public static bool operator ==(TimePeriod A, TimePeriod B) => (A.SecondsTotal == B.SecondsTotal);
        public static bool operator !=(TimePeriod A, TimePeriod B) => (A.SecondsTotal != B.SecondsTotal);
        public static TimePeriod operator +(TimePeriod A, TimePeriod B) => (new TimePeriod(A.SecondsTotal + B.SecondsTotal));
        public static TimePeriod operator -(TimePeriod A, TimePeriod B) => (new TimePeriod(A.SecondsTotal - B.SecondsTotal));
    }
}
