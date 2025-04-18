﻿namespace DBCoursework.Database {
    public static class DatabaseUtils {
        public static bool IsTableColumnWritable(this KeyValuePair<string, object> column) {
            if (column.Key == "id")
                return false;
            return !IsTableColumnEmpty(column.Value);
        }

        public static bool IsTableColumnEmpty(object? value) {
            if (value == null) return true;

            return string.IsNullOrEmpty(value.ToString());
        }

        public static string? ToCommandFormat(this object value) {
            if (value is string || value is DateTime)
                return $"'{value}'";
            return value.ToString();
        }
    }
}
