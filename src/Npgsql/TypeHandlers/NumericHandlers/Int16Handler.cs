﻿#region License
// The PostgreSQL License
//
// Copyright (C) 2017 The Npgsql Development Team
//
// Permission to use, copy, modify, and distribute this software and its
// documentation for any purpose, without fee, and without a written
// agreement is hereby granted, provided that the above copyright notice
// and this paragraph and the following two paragraphs appear in all copies.
//
// IN NO EVENT SHALL THE NPGSQL DEVELOPMENT TEAM BE LIABLE TO ANY PARTY
// FOR DIRECT, INDIRECT, SPECIAL, INCIDENTAL, OR CONSEQUENTIAL DAMAGES,
// INCLUDING LOST PROFITS, ARISING OUT OF THE USE OF THIS SOFTWARE AND ITS
// DOCUMENTATION, EVEN IF THE NPGSQL DEVELOPMENT TEAM HAS BEEN ADVISED OF
// THE POSSIBILITY OF SUCH DAMAGE.
//
// THE NPGSQL DEVELOPMENT TEAM SPECIFICALLY DISCLAIMS ANY WARRANTIES,
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY
// AND FITNESS FOR A PARTICULAR PURPOSE. THE SOFTWARE PROVIDED HEREUNDER IS
// ON AN "AS IS" BASIS, AND THE NPGSQL DEVELOPMENT TEAM HAS NO OBLIGATIONS
// TO PROVIDE MAINTENANCE, SUPPORT, UPDATES, ENHANCEMENTS, OR MODIFICATIONS.
#endregion

using System;
using Npgsql.BackendMessages;
using NpgsqlTypes;
using System.Data;
using JetBrains.Annotations;
using Npgsql.PostgresTypes;

namespace Npgsql.TypeHandlers.NumericHandlers
{
    /// <remarks>
    /// http://www.postgresql.org/docs/current/static/datatype-numeric.html
    /// </remarks>
    [TypeMapping("int2", NpgsqlDbType.Smallint, new[] { DbType.Int16, DbType.Byte, DbType.SByte }, new[] { typeof(short), typeof(byte), typeof(sbyte) }, DbType.Int16)]
    class Int16Handler : SimpleTypeHandler<short>,
        ISimpleTypeHandler<byte>, ISimpleTypeHandler<sbyte>, ISimpleTypeHandler<int>, ISimpleTypeHandler<long>,
        ISimpleTypeHandler<float>, ISimpleTypeHandler<double>, ISimpleTypeHandler<decimal>,
        ISimpleTypeHandler<string>
    {
        internal Int16Handler(PostgresType postgresType) : base(postgresType) { }

        public override short Read(ReadBuffer buf, int len, FieldDescription fieldDescription = null)
            => buf.ReadInt16();

        byte ISimpleTypeHandler<byte>.Read(ReadBuffer buf, int len, [CanBeNull] FieldDescription fieldDescription)
            => (byte)Read(buf, len, fieldDescription);

        sbyte ISimpleTypeHandler<sbyte>.Read(ReadBuffer buf, int len, [CanBeNull] FieldDescription fieldDescription)
            => (sbyte)Read(buf, len, fieldDescription);

        int ISimpleTypeHandler<int>.Read(ReadBuffer buf, int len, [CanBeNull] FieldDescription fieldDescription)
            => Read(buf, len, fieldDescription);

        long ISimpleTypeHandler<long>.Read(ReadBuffer buf, int len, [CanBeNull] FieldDescription fieldDescription)
            => Read(buf, len, fieldDescription);

        float ISimpleTypeHandler<float>.Read(ReadBuffer buf, int len, [CanBeNull] FieldDescription fieldDescription)
            => Read(buf, len, fieldDescription);

        double ISimpleTypeHandler<double>.Read(ReadBuffer buf, int len, [CanBeNull] FieldDescription fieldDescription)
            => Read(buf, len, fieldDescription);

        decimal ISimpleTypeHandler<decimal>.Read(ReadBuffer buf, int len, [CanBeNull] FieldDescription fieldDescription)
            => Read(buf, len, fieldDescription);

        string ISimpleTypeHandler<string>.Read(ReadBuffer buf, int len, [CanBeNull] FieldDescription fieldDescription)
            => Read(buf, len, fieldDescription).ToString();

        public override int ValidateAndGetLength(object value, NpgsqlParameter parameter = null)
        {
            if (!(value is short))
            {
                var converted = Convert.ToInt16(value);
                if (parameter == null)
                    throw CreateConversionButNoParamException(value.GetType());
                parameter.ConvertedValue = converted;
            }
            return 2;
        }

        protected override void Write(object value, WriteBuffer buf, NpgsqlParameter parameter = null)
        {
            if (parameter?.ConvertedValue != null)
                value = parameter.ConvertedValue;
            buf.WriteInt16((short)value);
        }
    }
}
