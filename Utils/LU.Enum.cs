using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaceLib.Utils
{
    public static partial class LU
    {
        public class EnumNum<T> where T : Enum
        {
            public enum Limit
            {
                Cap,
                Cycle,
                First,
                Last,
                Bounce
            }

            public T EType;
            public Limit Limiting = Limit.Cycle;
            private int index;
            public int Index
            {
                get
                {
                    return index;
                }
                set
                {
                    index = Math.Clamp(value, 0, Count);
                }
            }

            public T Value
            {
                get
                {
                    return (T)Enum.GetValues(typeof(T)).GetValue(Index);
                }
            }
            public int Count
            {
                get
                {
                    return Enum.GetValues(typeof(T)).Length;
                }
            }
            public T[] Values
            {
                get
                {
                    return (T[])Enum.GetValues(typeof(T));
                }
            }
            public EnumNum(int index = 0, Limit limiting = Limit.Cycle)
            {
                Index = index;
                Limiting = limiting;
            }
            public EnumNum(T type, int index = 0, Limit limiting = Limit.Cycle)
            {
                EType = type;
                Index = index;
                Limiting = limiting;
            }

            private static int ChangeIndex(int index, int change, int count, Limit limiting)
            {
                int result = index + change;
                bool high = result >= count, low = result < 0;
                if (!high && !low)
                {
                    return result;
                }

                if (limiting == Limit.Cap)
                {
                    if (high)
                    {
                        return count - 1;
                    }
                    return 0;
                }
                if (limiting == Limit.Cycle)
                {
                    if (high)
                    {
                        return result % count;
                    }
                    return count - 1 + result % count;
                }
                if (limiting == Limit.First)
                {
                    return 0;
                }
                if (limiting == Limit.Last)
                {
                    return count - 1;
                }
                if (limiting == Limit.Bounce)
                {
                    change %= ((count - 1) * 2);
                    int dir = high ? 1 : -1;
                    result = index;
                    for (int i = 0; i < change; i++, result += dir)
                    {
                        if (result == count - 1 || result == 0)
                        {
                            dir *= -1;
                        }
                    }
                    return result;
                }
                return result;
            }

            public static EnumNum<T> operator +(EnumNum<T> num, int addition)
            {
                num.Index = ChangeIndex(num.Index, addition, num.Count, num.Limiting);
                return num;
            }
            public static EnumNum<T> operator -(EnumNum<T> num, int subtraction)
            {
                num.Index = ChangeIndex(num.Index, subtraction, num.Count, num.Limiting);
                return num;
            }
            public static EnumNum<T> operator *(EnumNum<T> num, int multiply)
            {
                num.Index = ChangeIndex(num.Index, num.Index * multiply, num.Count, num.Limiting);
                return num;
            }
            public static EnumNum<T> operator /(EnumNum<T> num, int divide)
            {
                num.Index = ChangeIndex(num.Index, num.Index / divide, num.Count, num.Limiting);
                return num;
            }
            public static EnumNum<T> operator %(EnumNum<T> num, int module)
            {
                num.Index = ChangeIndex(num.Index, num.Index % module, num.Count, num.Limiting);
                return num;
            }
            public static bool operator ==(EnumNum<T> num, T @enum)
            {
                if (!num.Values.Contains(@enum))
                {
                    return false;
                }
                if (num.Values.ToList().IndexOf(@enum) == num.Index)
                {
                    return true;
                }
                return false;
            }
            public static bool operator !=(EnumNum<T> num, T @enum)
            {
                if (!num.Values.Contains(@enum))
                {
                    return true;
                }
                if (num.Values.ToList().IndexOf(@enum) == num.Index)
                {
                    return false;
                }
                return true;
            }
            public override bool Equals(object obj)
            {
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                if (ReferenceEquals(obj, null))
                {
                    return false;
                }

                if (obj is not T)
                {
                    return false;
                }

                if (ReferenceEquals(Value, (T)obj))
                {
                    return true;
                }

                return false;
            }
            public override string ToString()
            {
                return Value.ToString();
            }
            public int ToInt()
            {
                return Index;
            }
        }
    }
}
