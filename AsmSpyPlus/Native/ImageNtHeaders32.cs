﻿using System.Runtime.InteropServices;
using AsmSpy.Native;

namespace AsmSpyPlus.Native
{
  [StructLayout(LayoutKind.Explicit)]
  internal struct ImageNtHeaders32
  {
    [FieldOffset(0)]
    public uint Signature;
    [FieldOffset(4)]
    public ImageFileHeader FileHeader;
    [FieldOffset(24)]
    public ImageOptionalHeader32 OptionalHeader;
  }
}