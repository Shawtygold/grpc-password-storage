using System.Reflection;

namespace PasswordService.Application
{
    public static class AssemblyReference
    {
        public readonly static Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
