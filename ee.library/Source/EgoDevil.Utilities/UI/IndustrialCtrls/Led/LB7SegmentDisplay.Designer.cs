﻿/*
 * Creato da SharpDevelop.
 * Utente: lucabonotto
 * Data: 26/03/2009
 * Ora: 10.42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EgoDevil.Utilities.UI.IndustrialCtrls.Leds

{
	partial class LB7SegmentDisplay
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// LB7SegmentDisplay
			// 
			this.AutoScaleDimensions = new SizeF(6F, 13F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.Name = "LB7SegmentDisplay";
			this.Size = new Size(44, 65);
			this.ResumeLayout(false);
		}
	}
}
