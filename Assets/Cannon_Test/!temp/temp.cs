using System;
using System.Diagnostics;

[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Constructor | System.AttributeTargets.Method | System.AttributeTargets.Struct, Inherited = false)]
public sealed class DebuggerStepThroughAttribute : Attribute
{

}