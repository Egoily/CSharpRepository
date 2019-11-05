using System;

namespace EgoDevil.Utilities.UI.IndustrialCtrls.Utils
{
    /// <summary>
    /// Mathematic Functions
    /// </summary>
    public class LBMath : Object
    {
        public static float GetRadian(float val)
        {
            return (float)(val * Math.PI / 180);
        }
    }
}