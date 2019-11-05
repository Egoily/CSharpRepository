using Baidu.AI.Face;
using Baidu.AI.Face.Models;
using ee.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeddingGreeting
{
    public class GuestMgr
    {

        public static bool SaveOrUpdateFace(GuestInfo info)
        {
            bool success = false;
            try
            {
                if (string.IsNullOrEmpty(info.Id))
                {
                    info.Id = System.Guid.NewGuid().ToString().Replace("-", "").ToUpper();
                }

                var guest = GlobalConfigs.Guests.FirstOrDefault(x => x.Id == info.Id);
                string imageFileName = null;
                if (!string.IsNullOrEmpty(info.ImagePath) && (info.ImagePath != guest?.ImagePath || guest == null))
                {
                    imageFileName = Path.Combine($"GuestImages\\{info.Id}.jpg");
                    var img = Bitmap.FromFile(info.ImagePath);

                    var option = new FaceOption()
                    {
                        User_Info = $"姓名: {info.Name} \n身份: {info.Labels}\n桌号: {info.TableNo} ",
                    };

                    var jObj = FaceApi.FaceSaveOrUpdate(new Bitmap(img), GlobalConfigs.Configurations.GroupId, guest != null ? guest.Id : info.Id, option);

                    success = (jObj != null && jObj.error_code == 0);

                    if (success)
                    {
                        var newImage = new Bitmap(img);
                        img.Dispose();
                        if (File.Exists(imageFileName))
                            File.Delete(imageFileName);

                        newImage.Save(imageFileName, ImageFormat.Jpeg);
                        newImage.Dispose();
                    }
                }
                else
                {
                    success = true;
                }

                if (success)
                {
                    var entourageText = info.Entourage ?? "";
                    var entourages = entourageText.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                    var entourageNum = entourages.Count();

                    if (guest == null)//新增
                    {
                        GlobalConfigs.Guests.Add(new ee.Models.GuestInfo()
                        {
                            Id = info.Id,
                            Name = info.Name,
                            FullName = info.Name,
                            Gender = info.Gender,
                            GuestType = info.GuestType,
                            Entourage = entourageText,
                            EntourageNum = entourageNum,
                            Labels = info.Labels,
                            TableNo = info.TableNo,
                            ImagePath = imageFileName,
                            CashGift = info.CashGift,
                            CreateTime = DateTime.Now,
                        });
                        if (entourages != null)
                        {
                            foreach (var item in entourages)
                            {
                                GlobalConfigs.Guests.Add(new ee.Models.GuestInfo()
                                {
                                    Id = System.Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
                                    ParentId = info.Id,
                                    Name = $"{item}",
                                    FullName = $"{info.Name}_{item}",
                                    Gender = 0,
                                    GuestType = info.GuestType,
                                    Entourage = "",
                                    EntourageNum = 0,
                                    Labels = $"{info.Name} 随行人员",
                                    TableNo = info.TableNo,
                                    ImagePath = null,
                                    CreateTime = DateTime.Now,
                                });
                            }
                        }
                    }
                    else
                    {

                        var oldName = guest.Name;
                        guest.Name = info.Name;
                        guest.Gender = info.Gender;
                        guest.GuestType = info.GuestType;
                        guest.Labels = info.Labels;
                        guest.TableNo = info.TableNo;
                        guest.ImagePath = imageFileName;
                        guest.CashGift = info.CashGift;
                        guest.CreateTime = DateTime.Now;



                        if (guest.Entourage != info.Entourage)
                        {
                            GlobalConfigs.Guests.RemoveAll(x => x.ParentId == info.Id);
                            guest.Entourage = entourageText;
                            guest.EntourageNum = entourageNum;
                            if (entourages != null)
                            {
                                foreach (var item in entourages)
                                {
                                    GlobalConfigs.Guests.Add(new ee.Models.GuestInfo()
                                    {
                                        Id = System.Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
                                        ParentId = info.Id,
                                        Name = $"{item}",
                                        FullName = $"{info.Name}_{item}",
                                        Gender = 0,
                                        GuestType = info.GuestType,
                                        Entourage = "",
                                        EntourageNum = 0,
                                        Labels = $"{info.Name} 随行人员",
                                        TableNo = info.TableNo,
                                        ImagePath = null,
                                        IsAttend = guest.IsAttend,
                                        AttendTime = guest.AttendTime,
                                        CreateTime = DateTime.Now,
                                    });
                                }
                            }
                        }
                        #region 关联更新
                        var parent = GlobalConfigs.Guests.FirstOrDefault(x => x.Id == guest.ParentId);
                        if (parent != null && !string.IsNullOrEmpty(parent.Entourage))
                        {
                            var pEntourages = parent.Entourage.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                            for (var i = 0; i < pEntourages.Length; i++)
                            {
                                if (pEntourages[i] == oldName)
                                {
                                    pEntourages[i] = guest.Name;
                                }
                            }
                            parent.Entourage = string.Join(",", pEntourages);
                            parent.EntourageNum = pEntourages.Count();
                            guest.FullName = $"{parent.Name}_{guest.Name}";
                        }
                        #endregion

                    }
                    GlobalConfigMgr.SaveGuests();
                }


                return success;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool UpdateGuestInfo(GuestInfo info)
        {
            try
            {
                var guest = GlobalConfigs.Guests.FirstOrDefault(x => x.Name == info.Name);
                var oldName = guest.Name;
                guest.Name = info.Name;
                guest.Gender = info.Gender;
                guest.GuestType = info.GuestType;


                guest.Labels = info.Labels;
                guest.TableNo = info.TableNo;
                guest.ImagePath = info.ImagePath;
                guest.CashGift = info.CashGift;
                guest.CreateTime = DateTime.Now;
                guest.IsAttend = info.IsAttend;
                guest.AttendTime = info.AttendTime;



                var entourages = info.Entourage.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
                var entourageNum = entourages.Count();

                if (guest.Entourage != info.Entourage)
                {
                    GlobalConfigs.Guests.RemoveAll(x => x.ParentId == info.Id);
                    guest.Entourage = info.Entourage;
                    if (entourages != null)
                    {
                        foreach (var item in entourages)
                        {
                            GlobalConfigs.Guests.Add(new ee.Models.GuestInfo()
                            {
                                Id = System.Guid.NewGuid().ToString().Replace("-", "").ToUpper(),
                                ParentId = info.Id,
                                Name = $"{item}",
                                FullName = $"{info.Name}_{item}",
                                Gender = 0,
                                GuestType = info.GuestType,
                                Entourage = "",
                                EntourageNum = 0,
                                Labels = info.Labels,
                                TableNo = info.TableNo,
                                ImagePath = null,
                                IsAttend = guest.IsAttend,
                                AttendTime = guest.AttendTime,
                                CreateTime = DateTime.Now,
                            });
                        }
                    }
                }

                #region 关联更新
                var parent = GlobalConfigs.Guests.FirstOrDefault(x => x.Id == guest.ParentId);
                if (parent != null && !string.IsNullOrEmpty(parent.Entourage))
                {
                    var pEntourages = parent.Entourage.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                    for (var i = 0; i < pEntourages.Length; i++)
                    {
                        if (pEntourages[i] == oldName)
                        {
                            pEntourages[i] = guest.Name;
                        }
                    }

                    parent.Entourage = string.Join(",", pEntourages);
                    guest.EntourageNum = pEntourages.Count();
                    guest.FullName = $"{parent.Name}_{guest.Name}";
                }
                #endregion

                GlobalConfigMgr.SaveGuests();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void RemoveGuest(GuestInfo guest)
        {
            var id = guest.Id;
            RemoveGuest(id);
            if (guest.ParentId != null)
            {
                var parent = GlobalConfigs.Guests.FirstOrDefault(x => x.Id == guest.ParentId);
                if (parent != null && !string.IsNullOrEmpty(parent.Entourage))
                {
                    var pEntourages = parent.Entourage.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

                    for (var i = 0; i < pEntourages.Length; i++)
                    {
                        if (pEntourages[i] == guest.Name)
                        {
                            pEntourages[i] = null;
                            break;
                        }
                    }
                    parent.Entourage = string.Join(",", pEntourages.Where(x => x != null));
                    var num = pEntourages.Count(x => x != null);
                    parent.EntourageNum = num;
                }
            }
            GlobalConfigs.Guests.RemoveAll(x => x.ParentId == id || x.Id == id);

            GlobalConfigMgr.SaveGuests();
        }

        public static bool RemoveGuest(string userId)
        {
            var jObj = FaceApi.DeleteUser(userId, GlobalConfigs.Configurations.GroupId);

            return (jObj != null && jObj.error_code == 0);
        }

    }
}
