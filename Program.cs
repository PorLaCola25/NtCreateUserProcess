using System;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.ComponentModel;
using System.Security;

namespace NtCreateUserProccess
{
    class Program
    {
        [DllImport("ntdll.dll")]
        public static extern void RtlInitUnicodeString(ref UNICODE_STRING DestinationString, [MarshalAs(UnmanagedType.LPWStr)] string SourceString);
                      
        static void Main()
        {
            unsafe
            {           
                UNICODE_STRING uFileName = new UNICODE_STRING();
                RtlInitUnicodeString(ref uFileName, @"C:\Users\Public\calc.exe");
                IntPtr pHandle = IntPtr.Zero;
                IntPtr tHandle = IntPtr.Zero;

                // Create Info //
                ProcessCreateInfo createInfo = new ProcessCreateInfo();
                createInfo.Data.InitFlags = ProcessCreateInitFlag.None;
                createInfo.Data.ProhibitedImageCharacteristics = ImageCharacteristics.ExecutableImage;
                createInfo.Data.AdditionalFileAccess = FileAccessRights.GenericAll;
                // Create Info //

                // Process Attributes //
                ProcessAttributeList pAttr = new ProcessAttributeList();
                pAttr.Attributes = new ProcessAttribute();
                pAttr.Attributes.Attribute = 5;
                pAttr.Attributes.Size = uFileName.Length;
                pAttr.Attributes.Value = uFileName.buffer;
                // Process Attributes //

                // User Parameters //
                UserProcessParameters uParams = new UserProcessParameters();
                uParams.Flags = 0x01;
                uParams.DebugFlags = 0;
                uParams.ConsoleHandle = IntPtr.Zero;
                uParams.ConsoleFlags = 0;
                uParams.StandardInput = IntPtr.Zero;
                uParams.StandardOutput = IntPtr.Zero;
                uParams.StandardError = IntPtr.Zero;
                CURDIR CurrentDirectory = new CURDIR();
                RtlInitUnicodeString(ref CurrentDirectory.DosPath, @"C:\Users\kuni\");
                uParams.ImagePathName = uFileName;
                RtlInitUnicodeString(ref uParams.CommandLine, "\"C:\\Users\\kuni\\calc.exe\"");

                UNICODE_STRING Environ = new UNICODE_STRING();
                RtlInitUnicodeString(ref Environ, @"=::=::\");
                uParams.Environment = Environ.buffer;

                uParams.StartingX = 0;
                uParams.StartingY = 0;
                uParams.CountX = 0;
                uParams.CountY = 0;
                uParams.CountCharX = 0;
                uParams.CountCharY = 0;
                uParams.FillAttribute = 0;
                uParams.WindowFlags = 0;
                uParams.ShowWindowFlags = 0;
                uParams.WindowTitle = uFileName;
                RtlInitUnicodeString(ref uParams.DesktopInfo, @"Winsta0\Default");
                RtlInitUnicodeString(ref uParams.ShellInfo, @"");
                uParams.CurrentDirectories = new RTL_DRIVE_LETTER_CURDIR();
                
                uParams.Length = (ulong)Marshal.SizeOf(uParams);
                uParams.MaximumLength = (ulong)Marshal.SizeOf(uParams);
                // User Parameters //
                
                NTSTATUS status = NtCreateUserProcess10(out pHandle,
                    out tHandle,
                    ProcessAccessRights.MaximumAllowed,
                    ThreadAccessRights.MaximumAllowed,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    ProcessCreateFlags.None,
                    ThreadCreateFlags.Suspended,
                    ref uParams,
                    ref createInfo,
                    ref pAttr
                    );

                int error = Marshal.GetLastWin32Error();

                Console.ReadKey();
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////

        [DllImport("kernel32.dll")]
        public static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint flNewProtect, out uint lpflOldProtect);

        public static NTSTATUS NtCreateUserProcess10(out IntPtr ProcessHandle,
          out IntPtr ThreadHandle,
          ProcessAccessRights ProcessDesiredAccess,
          ThreadAccessRights ThreadDesiredAccess,
          IntPtr ProcessObjectAttributes,
          IntPtr ThreadObjectAttributes,
          ProcessCreateFlags ProcessFlags,
          ThreadCreateFlags ThreadFlags,
          ref UserProcessParameters ProcessParameters,
          ref ProcessCreateInfo CreateInfo,
          ref ProcessAttributeList AttributeList)
        {
            byte[] syscall = new byte[] { 0x49, 0x89, 0xCA, 0xB8, 0xBC, 0x01, 0x00, 0x00, 0x0F, 0x05, 0xC3 };

            unsafe
            {
                fixed (byte* ptr = syscall)
                {

                    IntPtr memoryAddress = (IntPtr)ptr;

                    if (!VirtualProtectEx(Process.GetCurrentProcess().Handle, memoryAddress,
                        (UIntPtr)syscall.Length, 0x40, out uint oldprotect))
                    {
                        throw new Win32Exception();
                    }

                    NtCreateUserProcess11 myAssemblyFunction = (NtCreateUserProcess11)Marshal.GetDelegateForFunctionPointer(memoryAddress, typeof(NtCreateUserProcess11));

                    return (NTSTATUS)myAssemblyFunction(out ProcessHandle, out ThreadHandle, ProcessDesiredAccess, ThreadDesiredAccess, ProcessObjectAttributes, ThreadObjectAttributes, ProcessFlags, ThreadFlags, ref ProcessParameters, ref CreateInfo, ref AttributeList);
                }
            }
        }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int NtCreateUserProcess11(out IntPtr ProcessHandle,
          out IntPtr ThreadHandle,
          ProcessAccessRights ProcessDesiredAccess,
          ThreadAccessRights ThreadDesiredAccess,
          IntPtr ProcessObjectAttributes,
          IntPtr ThreadObjectAttributes,
          ProcessCreateFlags ProcessFlags,
          ThreadCreateFlags ThreadFlags,
          ref UserProcessParameters ProcessParameters,
          ref ProcessCreateInfo CreateInfo,
          ref ProcessAttributeList AttributeList);
    }
}
