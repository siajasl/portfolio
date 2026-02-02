using System.Runtime.Serialization;
using System;

namespace Keane.CH.Framework.DataAccess.Core.Configuration
{
    /// <summary>
    /// Enumeration over the supported db parameter types.
    /// </summary>
    [DataContract(Namespace="www.Keane.com/CH/2009/01")]
    [Serializable]
    public enum ORMappingDbParameterType
    {
        // Summary:
        //     Undefined ... the default value.
        [EnumMember()]
        Undefined = 0,
        // Summary:
        //     A variable-length stream of non-Unicode characters ranging between 1 and
        //     8,000 characters.
        [EnumMember()]
        AnsiString = 1,
        //
        // Summary:
        //     A variable-length stream of binary data ranging between 1 and 8,000 bytes.
        [EnumMember()]
        Binary = 2,
        //
        // Summary:
        //     An 8-bit unsigned integer ranging in value from 0 to 255.
        [EnumMember()]
        Byte = 3,
        //
        // Summary:
        //     A simple type representing Boolean values of true or false.
        [EnumMember()]
        Boolean = 4,
        //
        // Summary:
        //     A currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63
        //     -1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of
        //     a currency unit.
        [EnumMember()]
        Currency = 5,
        //
        // Summary:
        //     A type representing a date value.
        [EnumMember()]
        Date = 6,
        //
        // Summary:
        //     A type representing a date and time value.
        [EnumMember()]
        DateTime = 7,
        //
        // Summary:
        //     A simple type representing values ranging from 1.0 x 10 -28 to approximately
        //     7.9 x 10 28 with 28-29 significant digits.
        [EnumMember()]
        Decimal = 8,
        //
        // Summary:
        //     A floating point type representing values ranging from approximately 5.0
        //     x 10 -324 to 1.7 x 10 308 with a precision of 15-16 digits.
        [EnumMember()]
        Double = 9,
        //
        // Summary:
        //     A globally unique identifier (or GUID).
        [EnumMember()]
        Guid = 10,
        //
        // Summary:
        //     An integral type representing signed 16-bit integers with values between
        //     -32768 and 32767.
        [EnumMember()]
        Int16 = 11,
        //
        // Summary:
        //     An integral type representing signed 32-bit integers with values between
        //     -2147483648 and 2147483647.
        [EnumMember()]
        Int32 = 12,
        //
        // Summary:
        //     An integral type representing signed 64-bit integers with values between
        //     -9223372036854775808 and 9223372036854775807.
        [EnumMember()]
        Int64 = 13,
        //
        // Summary:
        //     A general type representing any reference or value type not explicitly represented
        //     by another DbType value.
        [EnumMember()]
        Object = 14,
        //
        // Summary:
        //     An integral type representing signed 8-bit integers with values between -128
        //     and 127.
        [EnumMember()]
        SByte = 15,
        //
        // Summary:
        //     A floating point type representing values ranging from approximately 1.5
        //     x 10 -45 to 3.4 x 10 38 with a precision of 7 digits.
        [EnumMember()]
        Single = 16,
        //
        // Summary:
        //     A type representing Unicode character strings.
        [EnumMember()]
        String = 17,
        //
        // Summary:
        //     A type representing a time value.
        [EnumMember()]
        Time = 18,
        //
        // Summary:
        //     An integral type representing unsigned 16-bit integers with values between
        //     0 and 65535.
        [EnumMember()]
        UInt16 = 19,
        //
        // Summary:
        //     An integral type representing unsigned 32-bit integers with values between
        //     0 and 4294967295.
        [EnumMember()]
        UInt32 = 20,
        //
        // Summary:
        //     An integral type representing unsigned 64-bit integers with values between
        //     0 and 18446744073709551615.
        [EnumMember()]
        UInt64 = 21,
        //
        // Summary:
        //     A variable-length numeric value.
        [EnumMember()]
        VarNumeric = 22,
        //
        // Summary:
        //     A fixed-length stream of non-Unicode characters.
        [EnumMember()]
        AnsiStringFixedLength = 23,
        //
        // Summary:
        //     A fixed-length string of Unicode characters.
        [EnumMember()]
        StringFixedLength = 24,
        //
        // Summary:
        //     A parsed representation of an XML document or fragment.
        [EnumMember()]
        Xml = 25,
        //
        // Summary:
        //     Date and time data. Date value range is from January 1,1 AD through December
        //     31, 9999 AD. Time value range is 00:00:00 through 23:59:59.9999999 with an
        //     accuracy of 100 nanoseconds.
        [EnumMember()]
        DateTime2 = 26,
        //
        // Summary:
        //     Date and time data with time zone awareness. Date value range is from January
        //     1,1 AD through December 31, 9999 AD. Time value range is 00:00:00 through
        //     23:59:59.9999999 with an accuracy of 100 nanoseconds. Time zone value range
        //     is -14:00 through +14:00.
        [EnumMember()]
        DateTimeOffset = 27,
    }
}
