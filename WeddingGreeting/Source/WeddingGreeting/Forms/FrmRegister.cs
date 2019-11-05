using Baidu.AI.Face;
using Baidu.AI.Face.Models;
using ee.Models;
using EgoDevil.Utilities.UI.MessageForm;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WeddingGreeting.Forms
{
    public partial class FrmRegister : Form
    {


        private string currentImagePath;

        public FrmRegister()
        {
            InitializeComponent();
        }

        private bool Validation()
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                txtName.Focus();
                MsgForm.Show("请输入姓名");
                return false;
            }
            if (string.IsNullOrEmpty(txtLabels.Text))
            {
                txtLabels.Focus();
                MsgForm.Show("请输入标签");
                return false;
            }
            if (string.IsNullOrEmpty(txtTableNo.Text))
            {
                txtTableNo.Focus();
                MsgForm.Show("请输入桌号");
                return false;
            }
            if (picbFacePicture.Image == null)
            {
                picbFacePicture.Focus();
                MsgForm.Show("请选择相片");
                return false;

            }
            return true;
        }

        private bool? SaveOrUpdate(GuestInfo info)
        {
            bool success = false;
            try
            {
                var name = info.Name;
                var gender = info.Gender;
                var labels = info.Labels;
                var tableNo = info.TableNo;
                var imagePath = info.ImagePath;
                var entourageText = info.Entourage;
                var guestType = info.GuestType;
                var userId = info.Id;
                if (string.IsNullOrEmpty(userId))
                {
                    userId = System.Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                }

                var imageFileName = Path.Combine($"GuestImages\\{userId}.jpg");


                var img = Bitmap.FromFile(imagePath);


                var option = new FaceOption()
                {
                    User_Info = $"姓名: {name} \n身份: {labels}\n桌号: {tableNo} ",
                };


                var guest = GlobalConfigs.Guests.FirstOrDefault(x => x.Name == name);
                BaseResponse<FaceRegisterResult> jObj;
                if (guest == null)//新增
                {

                    jObj = FaceApi.FaceSaveOrUpdate(new Bitmap(img), GlobalConfigs.Configurations.GroupId, userId, option);

                    success = (jObj != null && jObj.error_code == 0);

                    if (success)
                    {

                        var entourages = entourageText.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        var entourageNum = entourages.Count();

                        GlobalConfigs.Guests.Add(new ee.Models.GuestInfo()
                        {
                            Id = userId,
                            Name = name,
                            Gender = gender,
                            GuestType = guestType,
                            Entourage = entourageText,
                            EntourageNum = entourageNum,
                            Labels = labels,
                            TableNo = tableNo,
                            ImagePath = imageFileName,
                            CreateTime = DateTime.Now,
                        });
                        if (entourages != null)
                        {
                            foreach (var item in entourages)
                            {
                                GlobalConfigs.Guests.Add(new ee.Models.GuestInfo()
                                {
                                    Id = System.Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
                                    ParentId = userId,
                                    Name = $"{name}_{item}",
                                    Gender = 0,
                                    GuestType = guestType,
                                    Entourage = "",
                                    EntourageNum = 0,
                                    Labels = labels,
                                    TableNo = tableNo,
                                    ImagePath = null,
                                    CreateTime = DateTime.Now,
                                });
                            }
                        }

                        GlobalConfigMgr.SaveGuests();
                    }

                }
                else
                {
                    jObj = FaceApi.FaceSaveOrUpdate(new Bitmap(img), GlobalConfigs.Configurations.GroupId, guest.Id, option);
                    success = (jObj != null && jObj.error_code == 0);

                    if (success)
                    {

                        var entourages = entourageText.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                        var entourageNum = entourages.Count();

                        guest.Name = name;
                        guest.Gender = gender;
                        guest.GuestType = guestType;
                        guest.Entourage = entourageText;
                        guest.EntourageNum = entourageNum;
                        guest.Labels = labels;
                        guest.TableNo = tableNo;
                        guest.ImagePath = null;
                        guest.CreateTime = DateTime.Now;

                        for (int i = 0; i < GlobalConfigs.Guests.Count; i++)
                        {
                            if (GlobalConfigs.Guests[i].ParentId == userId)
                                GlobalConfigs.Guests.Remove(GlobalConfigs.Guests[i]);
                        }

                        if (entourages != null)
                        {
                            foreach (var item in entourages)
                            {
                                GlobalConfigs.Guests.Add(new ee.Models.GuestInfo()
                                {
                                    Id = System.Guid.NewGuid().ToString().ToUpper(),
                                    ParentId = userId,
                                    Name = $"{name}_{item}",
                                    Gender = 0,
                                    GuestType = guestType,
                                    Entourage = "",
                                    EntourageNum = 0,
                                    Labels = labels,
                                    TableNo = tableNo,
                                    ImagePath = null,
                                    IsAttend = guest.IsAttend,
                                    AttendTime = guest.AttendTime,
                                    CreateTime = DateTime.Now,
                                });
                            }
                        }



                        GlobalConfigMgr.SaveGuests();
                    }
                }
                var newImage = new Bitmap(img);
                img.Dispose();
                if (File.Exists(imageFileName))
                    File.Delete(imageFileName);

                newImage.Save(imageFileName, ImageFormat.Jpeg);
                newImage.Dispose();

                return success;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private object Register(object obj)
        {
            var success = SaveOrUpdate(obj as GuestInfo);
            if (success.HasValue)
            {
                var message = success.Value ? "录入成功" : "录入失败";
                lbOperationResult.InvokeIfRequired(c => c.ForeColor = success.Value ? Color.GreenYellow : Color.Red);
                lbOperationResult.InvokeIfRequired(c => c.Text = message);
            }
            return success;
        }
        private void FrmRegister_Load(object sender, EventArgs e)
        {
            cbbGender.SelectedIndex = 0;
            cbbGuestType.SelectedIndex = 0;
        }
        private void picbFacePicture_DoubleClick(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "(*.jpg,*.png,*.jpeg,*.bmp)|*.jpg;*.png;*.jpeg;*.bmp",
                Multiselect = false
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                currentImagePath = ofd.FileName;
                var image = Bitmap.FromFile(ofd.FileName);
                if (image != null)
                {
                    picbFacePicture.Image = new Bitmap(image);
                    image.Dispose();
                }
            }
        }


        private void btnRegister_Click(object sender, EventArgs e)
        {

            var val = Validation();
            if (val)
            {

                var info = new GuestInfo()
                {
                    Id = txtNamePinyin.Text.Trim(),
                    Name = txtName.Text,
                    Gender = cbbGender.SelectedIndex,
                    Labels = txtLabels.Text,
                    TableNo = txtTableNo.Text,
                    ImagePath = currentImagePath,
                    Entourage = txtEntourage.Text ?? "",
                    GuestType = cbbGuestType.SelectedIndex,
                };
                var thread = new EgoDevil.Utilities.BkWorker.BackgroundThread(Register);
                thread.Start(info);
            }

        }



        private void picbFacePicture_MouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void picbFacePicture_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }


    }
}
