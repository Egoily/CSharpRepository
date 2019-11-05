using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace EAlbums
{
    public partial class AlbumImageList : UserControl
    {
        public class ImagesLoadedEventArgs : EventArgs
        {
            public string Name;
            public List<string> ImagePaths = new List<string>();

            public ImagesLoadedEventArgs(string name, List<string> list)
            {
                Name = name;
                ImagePaths = list;
            }
        }

        public delegate void ImagesLoadedEventHandler(Object sender, ImagesLoadedEventArgs e);

        public event ImagesLoadedEventHandler ImagesLoaded;

        private void OnImagesLoaded(ImagesLoadedEventArgs e)
        {
            if (ImagesLoaded != null)
            {
                ImagesLoaded(this, e);
            }
        }

        private string currentAlbumName;

        private void AddImages(string[] paths)
        {
            foreach (string fileName in paths)
            {
                string safeFileName = Path.GetFileNameWithoutExtension(fileName);
                try
                {
                    var bitmap = new Bitmap(fileName);
                    var thumbnailImage = bitmap.GetThumbnailImage(40, 40, null, IntPtr.Zero);
                    bitmap.Dispose();
                    dataGridView.Rows.Add(thumbnailImage, safeFileName, fileName);
                }
                catch (Exception ex)
                {
                    continue;
                }
            }
            dataGridView.Refresh();
        }

        public void LoadImages(string name, List<string> imagePaths)
        {
            currentAlbumName = name;
            dataGridView.Rows.Clear();
            AddImages(imagePaths.ToArray());
        }

        public AlbumImageList()
        {
            InitializeComponent();
        }

        private void AlbumImageListLoad(object sender, EventArgs e)
        {
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

            AddImages(ofd.FileNames);
        }

        private void ToolStripItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == tsbInsertImage)
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    List<string> imagePaths = new List<string>();

                    for (int i = 0; i < dataGridView.RowCount; i++)
                    {
                        imagePaths.Add(dataGridView[2, i].Value.ToString());
                    }

                    OnImagesLoaded(new ImagesLoadedEventArgs(currentAlbumName, imagePaths));
                }
            }
        }

        private void EPanelAlbumImageListCloseClick(object sender, EventArgs e)
        {
            this.Visible = false;
        }


    }
}