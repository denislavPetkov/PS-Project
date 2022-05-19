using System;

namespace WindowsFormsApp1
{
    internal class _SecurityDescriptor
    {
        internal static bool DemandRight(Type objectType, string rightType, bool exception)
        {
            if (rightType == "VIEW")
                return true;
            //default
            return false;
        }
    }
}