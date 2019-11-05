using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EAlbums
{
    public partial class ImageListForm : Form
    {
        public List<string> Images = new List<string>();

        public class LoadingEventArgs : EventArgs
        {
            public List<string> Images = new List<string>();

            public LoadingEventArgs(List<string> list)
            {
                Images = list;
            }
        }

        public delegate void LoadingEventHandler(Object sender, LoadingEventArgs e);

        public event LoadingEventHandler Loading;

        private void OnLoading(LoadingEventArgs e)
        {
            if (Loading != null)
            {
                Loading(this, e);
            }
        }

        public ImageListForm()
        {
            InitializeComponent();
        }

        public ImageListForm(List<string> list)
        {
            InitializeComponent();
            this.Images = list;
        }

        private void ImageListFormLoad(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
            foreach (string fileName in Images)
            {
                string safeFileName = Path.GetFileNameWithoutExtension(fileName);
                try
                {
                    dataGridView.Rows.Add((new Bitmap(fileName)).GetThumbnailImage(40, 40, null, IntPtr.Zero), safeFileName, fileName);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dataGridView.Refresh();
        }

        private void DataGridViewRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //======================================================
            //标识行号
            try
            {
                DataGridView dgv = sender as DataGridView;
                Rectangle rectangle = new Rectangle(e.RowBounds.Location.X,
                                                    Convert.ToInt32(e.RowBounds.Location.Y + (e.RowBounds.Height - dgv.RowHeadersDefaultCellStyle.Font.Size) / 2),
                                                    dgv.RowHeadersWidth - 4,
                                                    e.RowBounds.Height);
                TextRenderer.DrawText(e.Graphics,
                                     (e.RowIndex + 1).ToString(),
                                     dgv.RowHeadersDefaultCellStyle.Font,
                                     rectangle,
                                     dgv.RowHeadersDefaultCellStyle.ForeColor,
                                      TextFormatFlags.Right);
            }
            catch (Exception ex)
            {
                Console.Write("RowPostPaint:" + ex.Message);
            }
        }

        private void OpenFileDialogFileOk(object sender, CancelEventArgs e)
        {
            OpenFileDialog ofd = sender as OpenFileDialog;
            foreach (string fileName in ofd.FileNames)
            {
                string safeFileName = Path.GetFileNameWithoutExtension(fileName);
                try
                {
                    dataGridView.Rows.Add((new Bitmap(fileName)).GetThumbnailImage(40, 40, null, IntPtr.Zero), safeFileName, fileName);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dataGridView.Refresh();

            Images.Clear();
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                Images.Add(dataGridView[2, i].Value.ToString());
            }

            OnLoading(new LoadingEventArgs(Images));
        }

        private void BtnInsertImageClick(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }
    }
}