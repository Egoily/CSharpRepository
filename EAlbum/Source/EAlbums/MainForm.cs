using System;
using System.Windows.Forms;

namespace EAlbums
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void AlbumImageListImagesLoaded(object sender, AlbumImageList.ImagesLoadedEventArgs e)
        {
            this.albumView.ImagePaths = e.ImagePaths;
            this.albumView.LoadThumbs(e.ImagePaths);

            this.taskStrip.SetCurrentImagePaths(e.Name, e.ImagePaths);
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            this.Visible = true;
            this.albumView.Loading();
        }
    }
}