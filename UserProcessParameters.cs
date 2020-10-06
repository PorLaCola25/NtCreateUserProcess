using System;
using System.Runtime.InteropServices;

namespace NtCreateUserProccess
{
    [StructLayout(LayoutKind.Sequential)]
    public struct CURDIR
    {
        public UNICODE_STRING DosPath;
        public IntPtr Handle;
    }

    //[StructLayout(LayoutKind.Aut)]
    public struct UserProcessParameters
    {
        public ulong MaximumLength;
        public ulong Length;
        public ulong Flags;
        public ulong DebugFlags;
        public IntPtr ConsoleHandle;
        public ulong ConsoleFlags;
        public IntPtr StandardInput;
        public IntPtr StandardOutput;
        public IntPtr StandardError;
        public CURDIR CurrentDirectory;
        public UNICODE_STRING DllPath;
        public UNICODE_STRING ImagePathName;
        public UNICODE_STRING CommandLine;
        public IntPtr Environment;
        public ulong StartingX;
        public ulong StartingY;
        public ulong CountX;
        public ulong CountY;
        public ulong CountCharX;
        public ulong CountCharY;
        public ulong FillAttribute;
        public ulong WindowFlags;
        public ulong ShowWindowFlags;
        public UNICODE_STRING WindowTitle;
        public UNICODE_STRING DesktopInfo;
        public UNICODE_STRING ShellInfo;
        public UNICODE_STRING RuntimeData;
        public RTL_DRIVE_LETTER_CURDIR CurrentDirectories;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct UserProcessParametersX
    {
        public ulong Flags;
        public ulong Length;
        //public ulong Flags;
        public ulong DebugFlags;
        public IntPtr ConsoleHandle;
        public ulong ConsoleFlags;
        public IntPtr StandardInput;
        public IntPtr StandardOutput;
        public IntPtr StandardError;
        public CURDIR CurrentDirectory;
        public UNICODE_STRING DllPath;
        public UNICODE_STRING ImagePathName;
        public UNICODE_STRING CommandLine;
        public IntPtr Environment;
        public ulong StartingX;
        public ulong StartingY;
        public ulong CountX;
        public ulong CountY;
        public ulong CountCharX;
        public ulong CountCharY;
        public ulong FillAttribute;
        public ulong WindowFlags;
        public ulong ShowWindowFlags;
        public UNICODE_STRING WindowTitle;
        public UNICODE_STRING DesktopInfo;
        public UNICODE_STRING ShellInfo;
        public UNICODE_STRING RuntimeData;
        public RTL_DRIVE_LETTER_CURDIR CurrentDirectories;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RTL_DRIVE_LETTER_CURDIR
    {
        public ushort Flags;
        public ushort Length;
        public ulong TimeStamp;
        //public UNICODE_STRING DosPath;
    }
    /*
    public sealed class SafeProcessParametersBuffer : SafeBuffer
    {
        public SafeProcessParametersBuffer(IntPtr proc_params, bool owns_handle) : base(owns_handle)
        {
            SetHandle(proc_params);
            uint size = 0;
            if (proc_params != IntPtr.Zero)
                size = (uint)Marshal.ReadInt32(proc_params);
            Initialize(size);
        }

        public static SafeProcessParametersBuffer Null
        {
            get => new SafeProcessParametersBuffer(IntPtr.Zero, false);
        }

        protected override bool ReleaseHandle()
        {
            if (!IsInvalid)
            {
                NtRtl.RtlDestroyProcessParameters(handle);
                handle = IntPtr.Zero;
            }
            return true;
        }

        private static UnicodeString GetString(string s)
        {
            return s != null ? new UnicodeString(s) : null;
        }

        public static NtResult<SafeProcessParametersBuffer> Create(
                string image_path_name,
                string dll_path,
                string current_directory,
                string command_line,
                byte[] environment,
                string window_title,
                string desktop_info,
                string shell_info,
                string runtime_data,
                CreateProcessParametersFlags flags,
                bool throw_on_error)
        {
            return NtRtl.RtlCreateProcessParametersEx(out IntPtr ret, GetString(image_path_name), GetString(dll_path), GetString(current_directory),
              GetString(command_line), environment, GetString(window_title), GetString(desktop_info), GetString(shell_info),
              GetString(runtime_data), flags).CreateResult(throw_on_error, () => new SafeProcessParametersBuffer(ret, true));
        }

        public static SafeProcessParametersBuffer Create(
                string image_path_name,
                string dll_path,
                string current_directory,
                string command_line,
                byte[] environment,
                string window_title,
                string desktop_info,
                string shell_info,
                string runtime_data,
                CreateProcessParametersFlags flags)
        {
            return Create(image_path_name, dll_path, current_directory, command_line, environment,
                window_title, desktop_info, shell_info, runtime_data, flags, true).Result;
        }
    }
    */
}
