using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace EAlbums
{
    public partial class TaskStrip : UserControl
    {
        private ImageViewer albumView = new ImageViewer();

        public ImageViewer AssociatedAlbumView
        {
            get { return albumView; }
            set { this.albumView = value; }
        }

        private AlbumImageList albumImageList = new AlbumImageList();

        public AlbumImageList AssociatedAlbumImageList
        {
            get
            {
                return albumImageList;
            }
            set
            {
                albumImageList = value;
            }
        }

        private const string AlbumNewName = "新建相册";

        private string currentAlbumName;

        public List<AlbumItem> AlbumList = new List<AlbumItem>();

        private string CalNewName()
        {
            int index = 1;
            while (true)
            {
                string newName;
                if (index == 1)
                {
                    newName = AlbumNewName;
                }
                else
                {
                    newName = AlbumNewName + "(" + index.ToString() + ")";
                }
                bool flat = false;
                foreach (ListViewItem item in listViewAlbums.Items)
                {
                    if (item.Text == newName)
                    {
                        flat = true;
                        break;
                    }
                }
                if (!flat)
                {
                    return newName;
                }

                index++;
            }
        }

        private void InsertAlbum()
        {
            string newName = CalNewName();
            ListViewItem item = new ListViewItem(newName, 0);

            listViewAlbums.Items.Add(item);
            item.BeginEdit();
            AlbumList.Add(new AlbumItem(newName));
        }

        private void DeleteAlbum()
        {
            if (listViewAlbums.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("确定要删除该相册?(Y/N)", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (AlbumItem item in AlbumList)
                    {
                        if (item.Name == listViewAlbums.SelectedItems[0].Text)
                        {
                            AlbumList.Remove(item);
                            break;
                        }
                    }
                    listViewAlbums.SelectedItems[0].Remove();
                }
            }
        }

        private void RepositionOptionPanel(Point pos, bool flt)
        {
            ePanelOption.Location = pos;
            if (flt)
            {
                btnInsertAlbum.Enabled = true;
                btnLookup.Enabled = btnDeleteAlbum.Enabled = false;
            }
            else
            {
                btnInsertAlbum.Enabled = false;
                btnLookup.Enabled = btnDeleteAlbum.Enabled = true;
            }
        }

        private void ShowAlbumImageList()
        {
            //ImageListForm frm = new ImageListForm(_albumView.ImagePaths);
            //frm.Loading += new ImageListForm.LoadingEventHandler(ImageListForm_Loading);
            //frm.ShowDialog();

            AssociatedAlbumImageList.Visible = true;
            AssociatedAlbumImageList.LoadImages(currentAlbumName, albumView.ImagePaths);
        }

        public void SetCurrentImagePaths(string name, List<string> imagePaths)
        {
            foreach (AlbumItem item in AlbumList)
            {
                if (item.Name == name)
                {
                    item.ImagePaths = imagePaths;
                    break;
                }
            }
        }

        private void ImageListFormLoading(object sender, ImageListForm.LoadingEventArgs e)
        {
            albumView.LoadThumbs(e.Images);
        }

        public TaskStrip()
        {
            InitializeComponent();
        }

        private void TaskStripLoad(object sender, EventArgs e)
        {
        }

        private void ListViewAlbumsMouseDown(object sender, MouseEventArgs e)
        {
            ListView lv = sender as ListView;

            ListViewItem item = lv.GetItemAt(e.X, e.Y);

            Point pos = new Point(lv.Location.X + e.Location.X, lv.Location.Y - ePanelOption.Height / 2);

            if (item != null)
            {
                RepositionOptionPanel(pos, false);
            }
            else
            {
                RepositionOptionPanel(pos, true);
            }
        }

        private void ApbtnDefaultAlbumClick(object sender, EventArgs e)
        {
            ShowAlbumImageList();
        }

        private void BtnInsertAlbumClick(object sender, EventArgs e)
        {
            InsertAlbum();
        }

        private void BtnDeleteAlbumClick(object sender, EventArgs e)
        {
            DeleteAlbum();
        }

        private void BtnLookupClick(object sender, EventArgs e)
        {
            ShowAlbumImageList();
        }

        private void ListViewAlbumsSelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewAlbums.SelectedItems.Count > 0)
            {
                foreach (AlbumItem item in AlbumList)
                {
                    currentAlbumName = listViewAlbums.SelectedItems[0].Text;
                    if (item.Name == currentAlbumName)
                    {
                        AssociatedAlbumView.ImagePaths = item.ImagePaths;
                        AssociatedAlbumView.LoadThumbs(item.ImagePaths);
                        break;
                    }
                }
            }
        }
    }

    public class AlbumItem
    {
        public List<string> ImagePaths = new List<string>();
        public string Name;

        public AlbumItem()
        {
        }

        public AlbumItem(string name)
        {
            Name = name;
        }

        public AlbumItem(string name, List<string> list)
        {
            Name = name;
            ImagePaths = list;
        }
    }
}