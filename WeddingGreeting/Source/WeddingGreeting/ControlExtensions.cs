using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace WeddingGreeting
{
    public static class ControlExtensions
    {
        public static T InvokeIfRequired<T>(this T source, Action<T> action)
            where T : Control
        {
            try
            {
                if (!source.InvokeRequired)
                    action(source);
                else
                    source.Invoke(new Action(() => action(source)));
            }
            catch (Exception ex)
            {
                Debug.Write("Error on 'InvokeIfRequired': {0}", ex.Message);
            }
            return source;
        }
    }
}