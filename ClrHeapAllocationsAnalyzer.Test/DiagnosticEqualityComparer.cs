﻿using Microsoft.CodeAnalysis;
using System.Collections.Generic;

namespace ClrHeapAllocationAnalyzer.Test
{
    internal class DiagnosticEqualityComparer : IEqualityComparer<Diagnostic>
    {
        public static DiagnosticEqualityComparer Instance = new DiagnosticEqualityComparer();

        public bool Equals(Diagnostic x, Diagnostic y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Diagnostic obj)
        {
            return Combine(obj?.Descriptor.GetHashCode(),
                        Combine(obj?.GetMessage().GetHashCode(),
                         Combine(obj?.Location.GetHashCode(),
                          Combine(obj?.Severity.GetHashCode(), obj?.WarningLevel)
                        )));
        }

        internal static int Combine(int? newKeyPart, int? currentKey)
        {
            int hash = unchecked(currentKey.Value * (int)0xA5555529);

            return newKeyPart.HasValue ? hash + newKeyPart.Value : hash;
        }
    }
}