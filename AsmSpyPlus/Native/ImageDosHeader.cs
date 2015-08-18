using System.Runtime.InteropServices;

namespace AsmSpyPlus.Native
{
  [StructLayout(LayoutKind.Explicit)]
  internal struct ImageDosHeader
  {
    [FieldOffset(60)]
    public int FileAddressOfNewExeHeader;
  }
}