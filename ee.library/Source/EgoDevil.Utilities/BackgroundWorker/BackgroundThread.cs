using System.ComponentModel;
using System.Drawing;

namespace EgoDevil.Utilities.BkWorker
{
    public class BackgroundThread
    {
        public delegate object RunFunction(object obj);

        public BackgroundWorker Bw;
        public RunFunction thisFunction;
        private BackgroundForm frmBackground;
        public event RunWorkerCompletedEventHandler RunWorkerCompleted;
        public BackgroundThread(RunFunction newFunction)
        {
            thisFunction = newFunction;
            Bw = new BackgroundWorker();
            Bw.DoWork += new DoWorkEventHandler(Bw_DoWork);
            Bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Bw_RunWorkerCompleted);
        }

        public void Start(object obj)
        {
            Bw.RunWorkerAsync(obj);
            frmBackground = new BackgroundForm();
            frmBackground.ShowDialog();
        }

        public void Start(object obj, Size formSize)
        {
            Bw.RunWorkerAsync(obj);
            frmBackground = new BackgroundForm(formSize);
            frmBackground.ShowDialog();
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            RunWorkerCompleted?.Invoke(sender, e);
            frmBackground.Dispose();
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = thisFunction?.Invoke(e.Argument);
        }
    }
}