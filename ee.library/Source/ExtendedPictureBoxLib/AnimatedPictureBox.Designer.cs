using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using ExtendedPictureBoxLib.Animators;

namespace ExtendedPictureBoxLib
{
    partial class AnimatedPictureBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AnimatedPictureBox
            // 
            this.AutoScaleDimensions = new SizeF(6F, 12F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Name = "AnimatedPictureBox";
            this.Size = new Size(94, 78);
            this.ResumeLayout(false);

            this.components = new Container();
            this._stateAnimator = new ExtendedPictureBoxStateAnimator(this.components);
            ((ISupportInitialize)(this._stateAnimator)).BeginInit();
            // 
            // _stateAnimator
            // 
            this._stateAnimator.EndState = new PictureBoxState(
                ((Byte)(255)), 0F, 100F, 0F, 0F, 
                SystemColors.Control,
                SystemColors.Control, 
                SystemColors.ControlText,
                SystemColors.ControlText,
                0, 0F, 100F,
                Point.Empty,
                Point.Empty,
                Point.Empty);
            this._stateAnimator.ExtendedPictureBox = this;
            this._stateAnimator.StartState = new PictureBoxState(
                ((Byte)(255)), 0F, 100F, 0F, 0F,
                SystemColors.Control,
                SystemColors.Control,
                SystemColors.ControlText,
                SystemColors.ControlText,
                0, 0F, 500F,
                Point.Empty,
                Point.Empty,
                Point.Empty);
            this._stateAnimator.IntervallChanged += new EventHandler(this.OnAnimationIntervallChanged);
            this._stateAnimator.AnimationStopped += new EventHandler(this.OnAnimationStopped);
            this._stateAnimator.StepSizeChanged += new EventHandler(this.OnAnimationStepSizeChanged);
            this._stateAnimator.AnimationStarted += new EventHandler(this.OnAnimationStarted);
            this._stateAnimator.AnimationFinished += new EventHandler(this.OnAnimationFinished);
           


        }

        #endregion

        private ExtendedPictureBoxStateAnimator _stateAnimator;
    }
}
