using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace NtCreateUserProccess
{
    public struct ProcessAttributeList
    {
        public IntPtr TotalLength;
        public ProcessAttribute Attributes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ProcessAttribute
    {
        public ulong Attribute;
        public ushort Size;
        //public IntPtr ValuePtr;
        public IntPtr Value;

    }
}
