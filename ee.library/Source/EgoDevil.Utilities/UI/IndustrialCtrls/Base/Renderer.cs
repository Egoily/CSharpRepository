using System;
using System.Drawing;

namespace EgoDevil.Utilities.UI.IndustrialCtrls.Base
{
    /// <summary>
    /// Renderer interface for all EgoDevil.Utilities.UI.IndustrialCtrls renderer
    /// </summary>
    public interface ILBRenderer : IDisposable
    {
        object Control
        {
            set;
            get;
        }

        bool Update();

        void Draw(Graphics Gr);
    }
}