using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffWeb.Data
{
    // Minimal Data Reader Wrapper to handle long-to-int conversion
    public class SqliteInterceptorDataReader : DbDataReader
    {
        private readonly DbDataReader _innerReader;

        public SqliteInterceptorDataReader(DbDataReader innerReader)
        {
            _innerReader = innerReader;
        }

        // Fixes column indexing (e.g., reader["UserId"])
        public override object GetValue(int ordinal)
        {
            object val = _innerReader.GetValue(ordinal);
            if (val is long)
            {
                return Convert.ToInt32(val);
            }
            return val;
        }

        // Fixes direct typed conversions (e.g., reader.GetInt32(0))
        public override int GetInt32(int ordinal)
        {
            return Convert.ToInt32(_innerReader.GetValue(ordinal));
        }

        // --- NEW: FIX FOR ERROR 102 ---
        public override System.Collections.IEnumerator GetEnumerator()
        {
            return ((System.Collections.IEnumerable)_innerReader).GetEnumerator();
        }

        // --- MANDATORY PASS-THROUGH IMPLEMENTATIONS FOR DBREADER ABSTRACTION ---
        public override bool Read() { return _innerReader.Read(); }
        public override bool NextResult() { return _innerReader.NextResult(); }
        public override bool HasRows { get { return _innerReader.HasRows; } }
        public override bool IsClosed { get { return _innerReader.IsClosed; } }
        public override int FieldCount { get { return _innerReader.FieldCount; } }
        public override int Depth { get { return _innerReader.Depth; } }
        public override int RecordsAffected { get { return _innerReader.RecordsAffected; } }
        public override void Close() { _innerReader.Close(); }
        public override bool GetBoolean(int ordinal) { return _innerReader.GetBoolean(ordinal); }
        public override byte GetByte(int ordinal) { return _innerReader.GetByte(ordinal); }
        public override long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length) { return _innerReader.GetBytes(ordinal, fieldOffset, buffer, bufferOffset, length); }
        public override char GetChar(int ordinal) { return _innerReader.GetChar(ordinal); }
        public override long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length) { return _innerReader.GetChars(ordinal, fieldOffset, buffer, bufferOffset, length); }
        public override string GetDataTypeName(int ordinal) { return _innerReader.GetDataTypeName(ordinal); }
        public override DateTime GetDateTime(int ordinal) { return _innerReader.GetDateTime(ordinal); }
        public override decimal GetDecimal(int ordinal) { return _innerReader.GetDecimal(ordinal); }
        public override double GetDouble(int ordinal) { return _innerReader.GetDouble(ordinal); }
        public override Type GetFieldType(int ordinal) { return _innerReader.GetFieldType(ordinal); }
        public override float GetFloat(int ordinal) { return _innerReader.GetFloat(ordinal); }
        public override Guid GetGuid(int ordinal) { return _innerReader.GetGuid(ordinal); }
        public override short GetInt16(int ordinal) { return _innerReader.GetInt16(ordinal); }
        public override long GetInt64(int ordinal) { return _innerReader.GetInt64(ordinal); }
        public override string GetName(int ordinal) { return _innerReader.GetName(ordinal); }
        public override string GetString(int ordinal) { return _innerReader.GetString(ordinal); }
        public override int GetValues(object[] values) { return _innerReader.GetValues(values); }
        public override bool IsDBNull(int ordinal) { return _innerReader.IsDBNull(ordinal); }
        public override object this[int ordinal] { get { return this.GetValue(ordinal); } }
        public override object this[string name] { get { return this.GetValue(_innerReader.GetOrdinal(name)); } }
        public override int GetOrdinal(string name) { return _innerReader.GetOrdinal(name); }
        public override DataTable GetSchemaTable() { return _innerReader.GetSchemaTable(); }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _innerReader != null) { _innerReader.Dispose(); }
            base.Dispose(disposing);
        }
    }
}
