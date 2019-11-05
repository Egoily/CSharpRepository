using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace EgoDevil.Utilities.UI.EForm
{
    internal class EAntiAlias : IDisposable
    {
        private Graphics m_g;
        private SmoothingMode m_eMode;

        public EAntiAlias(Graphics g)
        {
            m_g = g;
            m_eMode = g.SmoothingMode;
            m_g.SmoothingMode = SmoothingMode.AntiAlias;
        }

        public void Dispose()
        {
            m_g.SmoothingMode = m_eMode;
        }
    }
}