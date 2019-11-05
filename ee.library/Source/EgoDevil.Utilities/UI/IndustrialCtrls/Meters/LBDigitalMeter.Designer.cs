using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EgoDevil.Utilities.UI.IndustrialCtrls.Meters
{
    partial class LBDigitalMeter
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// Liberare le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LBDigitalMeter
            // 
            this.AutoScaleDimensions = new SizeF(6F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = "LBDigitalMeter";
            this.Size = new Size(131, 33);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
