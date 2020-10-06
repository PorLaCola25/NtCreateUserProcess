using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace NtCreateUserProccess
{
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ProcessCreateInfo
	{
		IntPtr Size;
		public ProcessCreateState State;
		public ProcessCreateInfoData Data;

		public ProcessCreateInfo()
		{
			Size = new IntPtr(Marshal.SizeOf(this));
			State = ProcessCreateState.InitialState;
		}
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct ProcessCreateInfoData
	{
		// InitialState
		[FieldOffset(0)]
		public ProcessCreateInitFlag InitFlags;
		[FieldOffset(2)]
		public ImageCharacteristics ProhibitedImageCharacteristics;
		[FieldOffset(4)]
		public FileAccessRights AdditionalFileAccess;
		// FailOnSectionCreate
		[FieldOffset(0)]
		public IntPtr FileHandle;
		// FailExeFormat
		[FieldOffset(0)]
		public ushort DllCharacteristics;
		// FailExeName
		[FieldOffset(0)]
		public IntPtr IFEOKey;
		// Success
		[FieldOffset(0)]
		public ProcessCreateStateSuccessData Success;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct ProcessCreateStateSuccessData
	{
		public ProcessCreateStateSuccessOutputFlags OutputFlags;
		public IntPtr FileHandle;
		public IntPtr SectionHandle;
		public ulong UserProcessParametersNative;
		public uint UserProcessParametersWow64;
		public uint CurrentParameterFlags;
		public ulong PebAddressNative;
		public uint PebAddressWow64;
		public ulong ManifestAddress;
		public uint ManifestSize;
	}

	public enum ProcessCreateState
	{
		InitialState,
		FailOnFileOpen,
		FailOnSectionCreate,
		FailExeFormat,
		FailMachineMismatch,
		FailExeName,
		Success,
	};
}
