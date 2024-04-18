using System.Buffers;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace TimePeriodLibrary
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        //Zmienne składowe
        //zmienne Hours, Minutes oraz Seconds zwracają część godzin, minut, lub sekund
        //zmienna NumberOfSecondsFromMidnight zwraca ilość sekund od północy, przydatne w obliczeniach
        //zmienna Id została dodana na potrzeby wykonania programu z zadania dodatkowego
        #region Zmienne składowe
        private byte _hours = 0;
        private byte _minutes = 0;
        private byte _seconds;
        public byte Hours { get { return _hours; } private set { if (value < 24) { _hours = value;  } else { throw new ArgumentOutOfRangeException(); } } }
        public byte Minutes { get { return _minutes; } private set { if (value < 60) { _minutes = value;  } else { throw new ArgumentOutOfRangeException(); } } }
        public byte Seconds { get { return _seconds; } private set { if (value < 60) { _seconds = value;  } else { throw new ArgumentOutOfRangeException(); } } }
        public int NumberOfSecondsFromMidnight { get { return Hours * 3600 + Minutes * 60 + Seconds; } }

        public int Id { get; set; }
        #endregion
        //Konstruktory
        #region Konstruktory
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
        public Time(long SS = 0) 
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
        #endregion
        //Nadpisanie ToString
        #region ToString
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
        #endregion
        //Implementacja IComparable
        public int CompareTo(Time other) => (this == other ? 0 : (this > other ? 1 : -1));
        //Operatory Relacyjne
        #region Operatory relacyjne
        public static bool operator <(Time A, Time B) => (A.NumberOfSecondsFromMidnight < B.NumberOfSecondsFromMidnight);
        public static bool operator >(Time A, Time B) => (A.NumberOfSecondsFromMidnight > B.NumberOfSecondsFromMidnight);
        public static bool operator >=(Time A, Time B) => A == B ? true : A > B;
        public static bool operator <=(Time A, Time B) => A == B ? true : A < B;
        public static bool operator ==(Time A, Time B) => A.Equals(B);
        public static bool operator !=(Time A, Time B) => !A.Equals(B);
        #endregion
        //Implementacja IEquatable
        #region IEquatable
        public override bool Equals(object? obj)
        {   
            if (obj == null) return false;
            if (obj is Time time)
            { return Equals(time); }
            return base.Equals(obj);
        }
        public bool Equals(Time other) => this.NumberOfSecondsFromMidnight == other.NumberOfSecondsFromMidnight;
        public override int GetHashCode() => (int)Hours ^ (int)Minutes ^ (int)Seconds;
        #endregion
        //Działania arytmetyczne
        #region arytmetyka
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

        public static Time Plus(Time This, TimePeriod TP)
        { 
            This.Plus(TP);
            return This;
        }

        public static Time Minus(Time This, TimePeriod TP)
        {
            This.Minus(TP);
            return This;
        }
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
        #endregion
    }
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        //zmienne składowe
        //_seconds przechowuje liczbę sekund
        //SecondsTotal ją zwraca
        //Seconds, Minutes oraz Hours Zwracają daną część godziny po odpowiednich obliczeniach modulo
        #region zmienne składowe
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
                return (SecondsTotal % 3600) % 60;
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
        #endregion
        //konstruktory
        #region konstruktory
        public TimePeriod(long seconds)
        {
            SecondsTotal = seconds;
        }
        public TimePeriod(long minutes, long seconds)
        {
            SecondsTotal = seconds + minutes * 60;
        }
        public TimePeriod(long hours, long minutes, long seconds)
        {
            SecondsTotal = seconds + minutes * 60 + hours * 3600;
        }
        public TimePeriod(Time A, Time B)
        {
            SecondsTotal = Math.Abs(A.NumberOfSecondsFromMidnight - B.NumberOfSecondsFromMidnight);
            
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
        #endregion
        //nadpisanie ToString
        #region ToString
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
                sb.Append(Minutes);
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
                sb.Append(Seconds);

            }
            else
            {
                sb.Append(Seconds);

            }
            return sb.ToString();
        }
        #endregion
        //Działania arytmetyczne
        #region arytmetyka
        public void Plus(TimePeriod other) => this.SecondsTotal += other.SecondsTotal;
        public void Minus(TimePeriod other) => this.SecondsTotal -= other.SecondsTotal;
        public static TimePeriod Plus(TimePeriod This, TimePeriod other)
        { 
            This.Plus(other);
            return This;
        }
        public static TimePeriod Minus(TimePeriod This, TimePeriod other)
        {
            This.Minus(other);
            return This;
        }
        #endregion
        //Implementacja IComparable
        public int CompareTo(TimePeriod other) => (this.SecondsTotal == other.SecondsTotal ? 0 : (this.SecondsTotal > other.SecondsTotal ? 1 : -1));
        //Implementacja IEquatable
        #region IEquatable
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is TimePeriod time)
            { return Equals(time); }
            return base.Equals(obj);
        }
        public bool Equals(TimePeriod other) => (this.SecondsTotal == other.SecondsTotal);
        public override int GetHashCode() => this.SecondsTotal.GetHashCode();
        #endregion
        //operatory relacyjne
        #region operatory relacyjne
        public static bool operator <(TimePeriod A, TimePeriod B) => (A.SecondsTotal < B.SecondsTotal);
        public static bool operator >(TimePeriod A, TimePeriod B) => (A.SecondsTotal > B.SecondsTotal);
        public static bool operator <=(TimePeriod A, TimePeriod B) => (A.SecondsTotal <= B.SecondsTotal);
        public static bool operator >=(TimePeriod A, TimePeriod B) => (A.SecondsTotal >= B.SecondsTotal);
        public static bool operator ==(TimePeriod A, TimePeriod B) => (A.SecondsTotal == B.SecondsTotal);
        public static bool operator !=(TimePeriod A, TimePeriod B) => (A.SecondsTotal != B.SecondsTotal);
        public static TimePeriod operator +(TimePeriod A, TimePeriod B) => (new TimePeriod(A.SecondsTotal + B.SecondsTotal));
        public static TimePeriod operator -(TimePeriod A, TimePeriod B) => (new TimePeriod(A.SecondsTotal - B.SecondsTotal));
        #endregion
        // Dołożenie obsługi milisekund
        #region milisekundy
        public long Milliseconds
        { get; set; }
        public void MSTick()
        {
            Milliseconds++;
            if (Milliseconds == 100)
            {
                this.SecondsTotal++;
                Milliseconds = 0;
            }

        }
        public string ToString(string format)
        {
            if (format == "ms")
            {
                StringBuilder sb = new StringBuilder("", 12);
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
                    sb.Append(Minutes);
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
                    sb.Append(Seconds);
                    sb.Append(':');
                }
                else
                {
                    sb.Append(Seconds);
                    sb.Append(':');
                }
                if (Milliseconds < 10)
                {
                    sb.Append("0");
                    sb.Append(Milliseconds);
                }
                else
                {
                        sb.Append(Milliseconds);
                }

                return sb.ToString();
            }
            return this.ToString();
            
        }
        #endregion 
    }
}
