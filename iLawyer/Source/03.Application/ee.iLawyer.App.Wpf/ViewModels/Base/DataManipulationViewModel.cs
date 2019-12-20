using ee.Core.Data;
using ee.Core.Framework.Schema;
using ee.Core.Wpf.Enums;
using ee.Core.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ee.iLawyer.App.Wpf.ViewModels.Base
{
    public class DataManipulationViewModel<T> : AbstractDataManipulationViewModel<T>
        where T : CloneableObject, new()
    {
        public DataManipulationViewModel() : base()
        {

        }


        public override string PermissionCodePrefix { get; }


        /// <summary>
        /// 设置默认按钮
        /// </summary>
        public override void SetDefaultButton()
        {


            ButtonsInToolBar.Add(new FuncButtonViewModel() { Name = "新增", Icon = "Plus", PermissionCode = PermissionCodeFormatter("new"), InModel = ActionMode.Main, Command = AddCommand });

            ButtonsInToolBar.Add(new FuncButtonViewModel() { Name = "保存", Icon = "Check", PermissionCode = PermissionCodeFormatter(""), InModel = ActionMode.Add | ActionMode.Edit, Command = SaveCommand });
            ButtonsInToolBar.Add(new FuncButtonViewModel() { Name = "取消", Icon = "Close", PermissionCode = PermissionCodeFormatter(""), InModel = ActionMode.Add | ActionMode.Edit, Command = CancelCommand });


            ButtonsInOpBar.Add(new FuncButtonViewModel() { Name = "查询", Icon = "Magnify", PermissionCode = PermissionCodeFormatter("query"), InModel = ActionMode.Main, Command = QueryCommand });
            ButtonsInOpBar.Add(new FuncButtonViewModel() { Name = "重置", Icon = "Refresh", PermissionCode = PermissionCodeFormatter("query"), InModel = ActionMode.Main, Command = ResetCommand });

            ButtonsInDetail.Add(new FuncButtonViewModel() { Name = "编辑", Icon = "Edit", PermissionCode = PermissionCodeFormatter("edit"), InModel = ActionMode.Main, Command = EditCommand });
            ButtonsInDetail.Add(new FuncButtonViewModel() { Name = "删除", Icon = "Delete", PermissionCode = PermissionCodeFormatter("delete"), InModel = ActionMode.Main, Command = RemoveCommand });
            base.SetDefaultButton();


        }
        public override IList<string> LoadPermissions()
        {
            return ee.iLawyer.ServiceProvider.Cacher.CurrentResources.Where(x => x.StartsWith(PermissionCodePrefix)).ToList();
        }

        public override void ExecuteRemoveCommand(object o)
        {
            var dr = System.Windows.Forms.MessageBox.Show("确定要删除?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr != DialogResult.OK)
            {
                return;
            }
            base.ExecuteRemoveCommand(o);
        }

        public override void DataManipulationCompleted(ref BaseResponse response)
        {
            base.DataManipulationCompleted(ref response);
            if (!response.IsOk())
            {
                System.Windows.Forms.MessageBox.Show($"错误码:{response.Code}. {response.Message}", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public override BaseResponse Create()
        {
            throw new NotImplementedException();
        }

        public override BaseQueryResponse<T> Query()
        {
            throw new NotImplementedException();
        }

        public override BaseResponse Remove()
        {
            throw new NotImplementedException();
        }

        public override BaseResponse Update()
        {
            throw new NotImplementedException();
        }
    }
}
