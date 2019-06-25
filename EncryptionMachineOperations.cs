using System;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionMachine
{
    static class MachineOperations
    {
        public static void Input(ref Machine.Context context, object value)
        {
            if (value is long intval)
            {
                context.accumulator = intval;
            }
            else if (value is string strval)
            {
                context.accumulator = strval;
            }
        }

        public static void Save(ref Machine.Context context, object name)
        {
            if (name is string strname)
            {
                context.variables[strname] = context.accumulator;
            }
        }

        public static void Use(ref Machine.Context context, object name)
        {
            if (name is string strname)
            {
                context.accumulator = context.variables[strname];
            }
        }

        public static void Print(ref Machine.Context context)
        {
            if (context.accumulator is long || context.accumulator is string)
            {
                context.log += context.accumulator;
            }
        }

        public static void Add(ref Machine.Context context, object argument)
        {
            try
            {
                if (argument is long && context.accumulator is long)
                {
                    context.accumulator = (long)context.accumulator + (long)argument;
                }
                else if (argument is string && context.accumulator is long)
                {
                    context.accumulator = (long)context.accumulator + (long)context.variables[(string)argument];
                }
                else if (argument is string && context.accumulator is long[] && context.variables[(string)argument] is long)
                {
                    long length = ((long[])context.accumulator).Length;
                    long[] resultArray = new long[length];
                    for (long i = 0; i < length; i++)
                    {
                        resultArray[i] = ((long[])context.accumulator)[i] + ((long)context.variables[(string)argument]);
                    }
                    context.accumulator = resultArray;
                }
                else if (argument is string && context.accumulator is long[] && context.variables[(string)argument] is long[])
                {
                    long minLength = Math.Min(((long[])context.variables[(string)argument]).Length, ((long[])context.accumulator).Length);
                    long maxLength = Math.Max(((long[])context.variables[(string)argument]).Length, ((long[])context.accumulator).Length);
                    long[] resultArray = new long[maxLength];
                    for (long i = 0; i < minLength; i++)
                    {
                        resultArray[i] = ((long[])context.accumulator)[i] + ((long[])context.variables[(string)argument])[i];
                    }
                    context.accumulator = resultArray;
                }
            }
            catch
            {
                context.error = true;
            }
        }

        public static void Sub(ref Machine.Context context, object argument)
        {
            try
            {
                if (argument is long && context.accumulator is long)
                {
                    context.accumulator = (long)context.accumulator - (long)argument;
                }
                else if (argument is string && context.accumulator is long)
                {
                    context.accumulator = (long)context.accumulator - (long)context.variables[(string)argument];
                }
                else if (argument is string && context.accumulator is long[] && context.variables[(string)argument] is long)
                {
                    long length = ((long[])context.accumulator).Length;
                    long[] resultArray = new long[length];
                    for (long i = 0; i < length; i++)
                    {
                        resultArray[i] = ((long[])context.accumulator)[i] - ((long)context.variables[(string)argument]);
                    }
                    context.accumulator = resultArray;
                }
                else if (argument is string && context.accumulator is long[] && context.variables[(string)argument] is long[])
                {
                    long minLength = Math.Min(((long[])context.variables[(string)argument]).Length, ((long[])context.accumulator).Length);
                    long maxLength = Math.Max(((long[])context.variables[(string)argument]).Length, ((long[])context.accumulator).Length);
                    long[] resultArray = new long[maxLength];
                    for (long i = 0; i < minLength; i++)
                    {
                        resultArray[i] = ((long[])context.accumulator)[i] - ((long[])context.variables[(string)argument])[i];
                    }
                    context.accumulator = resultArray;
                }
            }
            catch
            {
                context.error = true;
            }
        }

        public static void Mul(ref Machine.Context context, object argument)
        {
            try
            {
                if (argument is long && context.accumulator is long)
                {
                    context.accumulator = (long)context.accumulator * (long)argument;
                }
                else if (argument is string && context.accumulator is long)
                {
                    context.accumulator = (long)context.accumulator * (long)context.variables[(string)argument];
                }
                else if (argument is string && context.accumulator is long[] && context.variables[(string)argument] is long)
                {
                    long length = ((long[])context.accumulator).Length;
                    long[] resultArray = new long[length];
                    for (long i = 0; i < length; i++)
                    {
                        resultArray[i] = ((long[])context.accumulator)[i] * ((long)context.variables[(string)argument]);
                    }
                    context.accumulator = resultArray;
                }
                else if (argument is string && context.accumulator is long[] && context.variables[(string)argument] is long[])
                {
                    long minLength = Math.Min(((long[])context.variables[(string)argument]).Length, ((long[])context.accumulator).Length);
                    long maxLength = Math.Max(((long[])context.variables[(string)argument]).Length, ((long[])context.accumulator).Length);
                    long[] resultArray = new long[maxLength];
                    for (long i = 0; i < minLength; i++)
                    {
                        resultArray[i] = ((long[])context.accumulator)[i] * ((long[])context.variables[(string)argument])[i];
                    }
                    context.accumulator = resultArray;
                }
            }
            catch
            {
                context.error = true;
            }
        }

        public static void Sum(ref Machine.Context context)
        {
            try
            {
                if (context.accumulator is long[] accumulator)
                {
                    long sum = 0;
                    foreach (long item in accumulator)
                    {
                        sum += item;
                    }
                    context.accumulator = sum;
                }
            }
            catch
            {
                context.error = true;
            }
        }

        public static void Indexes(ref Machine.Context context)
        {
            try
            {
                if (context.accumulator is string accumulator)
                {
                    long[] indexes = new long[accumulator.Length];
                    for (long i = 0; i < indexes.Length; i++)
                    {
                        indexes[i] = i + 1;
                    }
                    context.accumulator = indexes;
                }
            }
            catch
            {
                context.error = true;
            }
        }

        public static void Ato1(ref Machine.Context context)
        {
            try
            {
                if (context.accumulator is string accumulator)
                {
                    char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
                    long[] hash = new long[accumulator.Length];
                    char[] accumulatorChars = accumulator.ToUpper().ToCharArray();
                    for (long i = 0; i < accumulatorChars.Length; i++)
                    {
                        long n = 0;
                        for (long j = 0; j < alpha.Length; j++)
                        {
                            if (accumulatorChars[i] == alpha[j])
                            {
                                n = j + 1;
                                break;
                            }
                        }
                        hash[i] = n;
                    }
                    context.accumulator = hash;
                }
            }
            catch
            {
                context.error = true;
            }
        }

        public static void SubStr(ref Machine.Context context, long start, long end)
        {
            try
            {
                if (context.accumulator is string accumulator)
                {
                    int length = (int)end - (int)start;
                    start--; length++;
                    context.accumulator = accumulator.Substring((int)start, length);
                }
            }
            catch
            {
                context.error = true;
            }
        }

        public static void Lower(ref Machine.Context context)
        {
            try
            {
                if (context.accumulator is string accumulator)
                {
                    context.accumulator = accumulator.ToLower();
                }
            }
            catch
            {
                context.error = true;
            }
        }

        public static void Upper(ref Machine.Context context)
        {
            try
            {
                if (context.accumulator is string accumulator)
                {
                    context.accumulator = accumulator.ToUpper();
                }
            }
            catch
            {
                context.error = true;
            }
        }

        public static void MD5(ref Machine.Context context)
        {
            try
            {
                if (context.accumulator is string accumulator)
                {
                    using (MD5 md5 = System.Security.Cryptography.MD5.Create())
                    {
                        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(accumulator);
                        byte[] hashBytes = md5.ComputeHash(inputBytes);

                        // Convert the byte array to hexadecimal string
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < hashBytes.Length; i++)
                        {
                            sb.Append(hashBytes[i].ToString("X2"));
                        }
                        context.accumulator = sb.ToString();
                    }
                }
            }
            catch
            {
                context.error = true;
            }
        }

        public static void SHA1(ref Machine.Context context)
        {
            try
            {
                if (context.accumulator is string accumulator)
                {
                    using (SHA1Managed sha1 = new SHA1Managed())
                    {
                        var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(accumulator));
                        var sb = new StringBuilder(hash.Length * 2);
                        foreach (byte b in hash)
                        {
                            sb.Append(b.ToString("X2"));
                        }
                        context.accumulator = sb.ToString();
                    }
                }
            }
            catch
            {
                context.error = true;
            }
        }
    }
}