using System;
using System.Globalization;

namespace Tcp.AspNetCommon.Helper
{
    public class Conversion
    {
        public const byte DEFAULT_BYTE_VALUE = 0;
        public const short DEFAULT_NUMERIC_VALUE = 0;
        public const bool DEFAULT_BOOLEAN_VALUE = false;
        public static readonly DateTime DEFAULT_DATETIME_VALUE = DateTime.MinValue;

        private static Conversion conversion;
        private static readonly object padlock = new object();

        # region Byte Methods

        public byte ToByte(object value)
        {
            return value == null ? DEFAULT_BYTE_VALUE : ToByte(value.ToString());
        }

        public byte ToByte(string value)
        {
            return ToByte(value, DEFAULT_BYTE_VALUE);
        }

        public byte ToByte(object value, byte defaultValue)
        {
            return value == null ? defaultValue : ToByte(value.ToString(), defaultValue);
        }

        public byte ToByte(string value, byte defaultValue)
        {
            byte result;
            if (byte.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        # endregion

        # region Int16 Methods

        public bool IsInt16(object value)
        {
            return value == null ? false : IsInt16(value.ToString());
        }

        public bool IsInt16(string value)
        {
            short result;
            return short.TryParse(value, out result);
        }

        public short ToInt16(object value, short defaultValue)
        {
            return value == null ? defaultValue : ToInt16(value.ToString(), defaultValue);
        }

        public short ToInt16(string value, short defaultValue)
        {
            short result;
            if (short.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        public short ToInt16(object value)
        {
            return value == null ? DEFAULT_NUMERIC_VALUE : ToInt16(value.ToString());
        }

        public short ToInt16(string value)
        {
            return ToInt16(value, DEFAULT_NUMERIC_VALUE);
        }

        # endregion

        # region Int32 Methods

        public bool IsInt32(object value)
        {
            return value == null ? false : IsInt32(value.ToString());
        }

        public bool IsInt32(string value)
        {
            int result;
            return int.TryParse(value, out result);
        }

        public int ToInt32(object value, int defaultValue)
        {
            return value == null ? defaultValue : ToInt32(value.ToString(), defaultValue);
        }

        public int ToInt32(string value, int defaultValue)
        {
            int result;
            if (int.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        public int ToInt32(object value)
        {
            return value == null ? DEFAULT_NUMERIC_VALUE : ToInt32(value.ToString());
        }

        public int ToInt32(string value)
        {
            return ToInt32(value, 0);
        }

        # endregion

        # region Int64 Methods

        public bool IsInt64(object value)
        {
            return value == null ? false : IsInt64(value.ToString());
        }

        public bool IsInt64(string value)
        {
            long result;
            return long.TryParse(value, out result);
        }

        public long ToInt64(object value, long defaultValue)
        {
            return value == null ? defaultValue : ToInt64(value.ToString(), defaultValue);
        }

        public long ToInt64(string value, long defaultValue)
        {
            long result;
            if (long.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        public long ToInt64(object value)
        {
            return value == null ? DEFAULT_NUMERIC_VALUE : ToInt64(value.ToString());
        }

        public long ToInt64(string value)
        {
            return ToInt64(value, DEFAULT_NUMERIC_VALUE);
        }

        # endregion

        # region Double Methods

        public bool IsDouble(object value)
        {
            return value == null ? false : IsDouble(value.ToString());
        }

        public bool IsDouble(string value)
        {
            double result;
            return double.TryParse(value, out result);
        }

        public double? ToNullableDouble(object value, double? defaultValue)
        {
            return value == null ? defaultValue : ToNullableDouble(value.ToString(), defaultValue);
        }

        public double? ToNullableDouble(string value, double? defaultValue)
        {
            double result;
            if (double.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        public double? ToNullableDouble(object value)
        {
            return value == null ? null : ToNullableDouble(value.ToString());
        }

        public double? ToNullableDouble(string value)
        {
            return ToNullableDouble(value, null);
        }

        public double ToDouble(object value, double defaultValue)
        {
            return value == null ? defaultValue : ToDouble(value.ToString(), defaultValue);
        }

        public double ToDouble(string value, double defaultValue)
        {
            return ToNullableDouble(value, defaultValue).Value;
        }

        public double ToDouble(object value)
        {
            return value == null ? DEFAULT_NUMERIC_VALUE : ToDouble(value.ToString());
        }

        public double ToDouble(string value)
        {
            return ToDouble(value, 0);
        }

        # endregion

        # region Decimal Methods

        public bool IsDecimal(object value)
        {
            return value == null ? false : IsDecimal(value.ToString());
        }

        public bool IsDecimal(string value)
        {
            decimal result;
            return decimal.TryParse(value, out result);
        }

        public decimal ToDecimal(object value, decimal defaultValue)
        {
            return value == null ? defaultValue : ToDecimal(value.ToString(), defaultValue);
        }

        public decimal ToDecimal(string value, decimal defaultValue)
        {
            decimal result;
            if (decimal.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        public decimal ToDecimal(object value)
        {
            return value == null ? DEFAULT_NUMERIC_VALUE : ToDecimal(value.ToString());
        }

        public decimal ToDecimal(string value)
        {
            return ToDecimal(value, 0);
        }

        # endregion

        # region DateTime Methods

        public bool IsDateTime(object value)
        {
            return value == null ? false : IsDateTime(value.ToString());
        }

        public bool IsDateTime(string value)
        {
            DateTime result;
            return DateTime.TryParse(value, out result);
        }

        public bool IsDateTime(object value, string format)
        {
            return value == null ? false : IsDateTime(value.ToString(), format);
        }

        public bool IsDateTime(string value, string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                return IsDateTime(value);
            }

            DateTime result;
            return DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);
        }

        public DateTime? ToNullableDateTime(object value, string format, DateTime? defaultValue)
        {
            return value == null ? defaultValue : ToNullableDateTime(value.ToString(), format, defaultValue);
        }

        public DateTime? ToNullableDateTime(string value, string format, DateTime? defaultValue)
        {
            if (string.IsNullOrEmpty(format))
            {
                return ToNullableDateTime(value, defaultValue);
            }

            DateTime result;
            if (DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        public DateTime? ToNullableDateTime(object value, string format)
        {
            return value == null ? null : ToNullableDateTime(value.ToString(), format);
        }

        public DateTime? ToNullableDateTime(string value, string format)
        {
            return ToNullableDateTime(value, format, null);
        }

        public DateTime? ToNullableDateTime(object value, DateTime? defaultValue)
        {
            return value == null ? defaultValue : ToNullableDateTime(value.ToString(), defaultValue);
        }

        public DateTime? ToNullableDateTime(string value, DateTime? defaultValue)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        public DateTime? ToNullableDateTime(object value)
        {
            return value == null ? null : ToNullableDateTime(value.ToString());
        }

        public DateTime? ToNullableDateTime(string value)
        {
            return ToNullableDateTime(value, null, null);
        }

        public DateTime ToDateTime(object value, DateTime defaultValue)
        {
            return value == null ? defaultValue : ToDateTime(value.ToString(), defaultValue);
        }

        public DateTime ToDateTime(string value, DateTime defaultValue)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            else
            {
                return defaultValue;
            }
        }

        public DateTime ToDateTime(object value)
        {
            return value == null ? DEFAULT_DATETIME_VALUE : ToDateTime(value.ToString());
        }

        public DateTime ToDateTime(string value)
        {
            return ToDateTime(value, DEFAULT_DATETIME_VALUE);
        }

        public DateTime ToDateTime(long ticks)
        {
            try
            {
                return new DateTime(ticks);
            }
            catch (ArgumentOutOfRangeException)
            {
                return DEFAULT_DATETIME_VALUE;
            }
        }

        # endregion

        # region Boolean Methods

        public bool IsBoolean(object value)
        {
            return value == null ? false : IsBoolean(value.ToString());
        }

        public bool IsBoolean(string value)
        {
            bool result;
            return bool.TryParse(value, out result);
        }

        public bool ToBoolean(object value, bool defaultValue)
        {
            return value == null ? defaultValue : ToBoolean(value.ToString(), defaultValue);
        }

        public bool ToBoolean(string value, bool defaultValue)
        {
            int i = ToInt16(value, -1);
            if (i == 0)
                return false;
            else if (i == 1)
                return true;
            else
            {
                bool result;
                if (bool.TryParse(value, out result))
                {
                    return result;
                }
                else
                {
                    return defaultValue;
                }
            }
        }

        public bool ToBoolean(object value)
        {
            return value == null ? DEFAULT_BOOLEAN_VALUE : ToBoolean(value.ToString());
        }

        public bool ToBoolean(string value)
        {
            return ToBoolean(value, DEFAULT_BOOLEAN_VALUE);
        }

        # endregion

        # region String Methods

        public string ToString(object o, string defaultValue)
        {
            if (o == null)
            {
                return defaultValue;
            }

            return o.ToString();
        }

        public string ToString(object o)
        {
            return ToString(o, string.Empty);
        }

        # endregion

        public static Conversion GetInstance()
        {
            lock (padlock)
            {
                if (conversion == null)
                {
                    conversion = new Conversion();
                }
                return conversion;
            }
        }
    }
}

