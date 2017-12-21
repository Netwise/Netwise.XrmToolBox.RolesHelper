using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Netwise.XrmToolBox.RolesHelper.Exporters.Excels;
using Netwise.XrmToolBox.RolesHelper.Exporters.Excels.Configurations;
using Netwise.XrmToolBox.RolesHelper.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WB_Permissions;
using XrmToolBox.Extensibility;

namespace Netwise.XrmToolBox.RolesHelper
{
    /// <summary>
    /// Control which will be displayed in XrmToolBox.
    /// </summary>
    public class PluginControl : PluginControlBase
    {
        private delegate void SetAllControlsEnabled(bool value);
        private delegate CheckedListBox.CheckedItemCollection GetCheckedRolesDelegate();

        public System.Windows.Forms.GroupBox GB_Roles;
        private System.Windows.Forms.CheckedListBox CLB_Roles;
        public System.Windows.Forms.GroupBox GB_Permissions;
        public System.Windows.Forms.MenuStrip MainMenu;
        public System.Windows.Forms.ToolStripMenuItem B_Close;
        public System.Windows.Forms.ToolStripMenuItem B_LoadData;
        public System.Windows.Forms.CheckedListBox CLB_Users;
        public System.Windows.Forms.Integration.ElementHost elementHost1;
        public WB_Permissions.WB_Permissions wB_Permissions1;
        public ToolStripMenuItem TSMI_Export;
        public ToolStripMenuItem TSMI_SubItem_ExcelSingleFile;
        public System.Windows.Forms.GroupBox GB_Users;

        // Plugin-related properties

        public List<ModelSystemUser> Users { get; private set; }
        public List<ModelRole> Roles { get; private set; }
        public List<EntityMetadata> EntitiesMetadata { get; set; }
        public ModelSystemUser CurrentSelectedUser { get; set; }

        public PluginControl()
        {
            this.InitializeComponent();

            this.Users = new List<ModelSystemUser>();
            this.Roles = new List<ModelRole>();
            this.EntitiesMetadata = new List<EntityMetadata>();
        }

        #region Public Methods
        /// <summary>
        /// Returns multiple records from given Fetch XML Query.
        /// </summary>
        public EntityCollection RetrieveMultiple(string fetch)
        {
            // Prepare Query
            var query = new FetchExpression(fetch);
            return this.Service.RetrieveMultiple(query);
        }

        /// <summary>
        /// Returns single <see cref="Entity"/> record from given Fetch XML Query.
        /// </summary>
        public Entity Retrieve(string fetch)
        {
            // prepare Query
            var query = new FetchExpression(fetch);
            return this.Service.RetrieveMultiple(query).Entities.First();
        }

        /// <summary>
        /// Returns Role Privileges from given Role.
        /// </summary>
        public List<ModelRolePrivilege> GetRolePrivileges(ModelRole role)
        {
            // All downloaded Entities
            var rolePrivileges = this.RetrieveMultiple(FetchQueriesHelper.GetPrivilegesForRole(role));
            var list = new List<ModelRolePrivilege>();
            // Parse Entities
            foreach (var rolePrivilege in rolePrivileges.Entities)
            {
                var parsed = rolePrivilege.ToRolePrivilege();
                list.Add(parsed);
            }
            return list;
        }

        /// <summary>
        /// Cross-thread method to get list of checked Roles.
        /// </summary>
        public System.Windows.Forms.CheckedListBox.CheckedItemCollection GetCheckedRoles()
        {
            GetCheckedRolesDelegate del = new GetCheckedRolesDelegate(this.GetCheckedRolesDel);
            var ret = del.Invoke();
            return ret;
        }

        #endregion

        #region Designer Component Initialization

        private void InitializeComponent()
        {
            this.GB_Users = new System.Windows.Forms.GroupBox();
            this.CLB_Users = new System.Windows.Forms.CheckedListBox();
            this.GB_Roles = new System.Windows.Forms.GroupBox();
            this.CLB_Roles = new System.Windows.Forms.CheckedListBox();
            this.GB_Permissions = new System.Windows.Forms.GroupBox();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.B_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.B_LoadData = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_Export = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_SubItem_ExcelSingleFile = new System.Windows.Forms.ToolStripMenuItem();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.wB_Permissions1 = new WB_Permissions.WB_Permissions();
            this.GB_Users.SuspendLayout();
            this.GB_Roles.SuspendLayout();
            this.GB_Permissions.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // GB_Users
            // 
            this.GB_Users.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GB_Users.Controls.Add(this.CLB_Users);
            this.GB_Users.Location = new System.Drawing.Point(4, 27);
            this.GB_Users.Name = "GB_Users";
            this.GB_Users.Size = new System.Drawing.Size(261, 619);
            this.GB_Users.TabIndex = 0;
            this.GB_Users.TabStop = false;
            this.GB_Users.Text = "Users";
            // 
            // CLB_Users
            // 
            this.CLB_Users.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CLB_Users.FormattingEnabled = true;
            this.CLB_Users.Location = new System.Drawing.Point(7, 20);
            this.CLB_Users.Name = "CLB_Users";
            this.CLB_Users.Size = new System.Drawing.Size(244, 589);
            this.CLB_Users.TabIndex = 0;
            this.CLB_Users.SelectedIndexChanged += new System.EventHandler(this.CLB_Users_SelectedIndexChanged);
            // 
            // GB_Roles
            // 
            this.GB_Roles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.GB_Roles.Controls.Add(this.CLB_Roles);
            this.GB_Roles.Location = new System.Drawing.Point(271, 27);
            this.GB_Roles.Name = "GB_Roles";
            this.GB_Roles.Size = new System.Drawing.Size(261, 619);
            this.GB_Roles.TabIndex = 1;
            this.GB_Roles.TabStop = false;
            this.GB_Roles.Text = "Roles";
            // 
            // CLB_Roles
            // 
            this.CLB_Roles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)));
            this.CLB_Roles.Enabled = false;
            this.CLB_Roles.FormattingEnabled = true;
            this.CLB_Roles.Location = new System.Drawing.Point(6, 20);
            this.CLB_Roles.Name = "CLB_Roles";
            this.CLB_Roles.Size = new System.Drawing.Size(244, 589);
            this.CLB_Roles.TabIndex = 1;
            // 
            // GB_Permissions
            // 
            this.GB_Permissions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GB_Permissions.Controls.Add(this.elementHost1);
            this.GB_Permissions.Location = new System.Drawing.Point(538, 27);
            this.GB_Permissions.Name = "GB_Permissions";
            this.GB_Permissions.Size = new System.Drawing.Size(294, 619);
            this.GB_Permissions.TabIndex = 2;
            this.GB_Permissions.TabStop = false;
            this.GB_Permissions.Text = "Permissions";
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.B_Close,
            this.B_LoadData,
            this.TSMI_Export});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(835, 24);
            this.MainMenu.TabIndex = 3;
            this.MainMenu.Text = "MainMenu";
            // 
            // B_Close
            // 
            this.B_Close.Name = "B_Close";
            this.B_Close.Size = new System.Drawing.Size(94, 20);
            this.B_Close.Text = "Close this tool";
            this.B_Close.Click += new System.EventHandler(this.B_Close_Click);
            // 
            // B_LoadData
            // 
            this.B_LoadData.Name = "B_LoadData";
            this.B_LoadData.Size = new System.Drawing.Size(80, 20);
            this.B_LoadData.Text = "Load data...";
            this.B_LoadData.Click += new System.EventHandler(this.B_LoadData_Click);
            // 
            // TSMI_Export
            // 
            this.TSMI_Export.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_SubItem_ExcelSingleFile});
            this.TSMI_Export.Name = "TSMI_Export";
            this.TSMI_Export.Size = new System.Drawing.Size(69, 20);
            this.TSMI_Export.Text = "Export to:";
            // 
            // TSMI_SubItem_ExcelSingleFile
            // 
            this.TSMI_SubItem_ExcelSingleFile.Name = "TSMI_SubItem_ExcelSingleFile";
            this.TSMI_SubItem_ExcelSingleFile.Size = new System.Drawing.Size(164, 22);
            this.TSMI_SubItem_ExcelSingleFile.Text = "Excel - Single File";
            this.TSMI_SubItem_ExcelSingleFile.ToolTipText = "Worksheet #1 - All\r\nWorksheet #2...n - Worksheet per Role";
            this.TSMI_SubItem_ExcelSingleFile.Click += new System.EventHandler(this.TSMI_SubItem_ExcelSingleFile_Click);
            // 
            // elementHost1
            // 
            this.elementHost1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.elementHost1.Location = new System.Drawing.Point(6, 20);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(282, 589);
            this.elementHost1.TabIndex = 2;
            this.elementHost1.Text = "elementHost";
            this.elementHost1.Child = this.wB_Permissions1;
            // 
            // PluginControl
            // 
            this.Controls.Add(this.GB_Permissions);
            this.Controls.Add(this.GB_Roles);
            this.Controls.Add(this.GB_Users);
            this.Controls.Add(this.MainMenu);
            this.Name = "PluginControl";
            this.Size = new System.Drawing.Size(835, 649);
            this.GB_Users.ResumeLayout(false);
            this.GB_Roles.ResumeLayout(false);
            this.GB_Permissions.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Method used by the delegate.
        /// See this.GetCheckedRoles();
        /// </summary>
        private System.Windows.Forms.CheckedListBox.CheckedItemCollection GetCheckedRolesDel()
        {
            return this.CLB_Roles.CheckedItems;
        }

        /// <summary>
        /// Close tool window.
        /// </summary>
        private void B_Close_Click(object sender, System.EventArgs e)
        {
            this.CloseTool();
        }

        /// <summary>
        /// Download data from CRM.
        /// </summary>
        private void B_LoadData_Click(object sender, System.EventArgs e)
        {
            // Loading Screen + work in background
            this.WorkAsync(new WorkAsyncInfo()
            {
                Message = "Loading Users, Roles and Permissions from CRM...",
                Work = (backgroundWorker, args) =>
                {
                    this.WrapAsyncFunction(() =>
                    {
                        // Entity Metadata
                        this.EntitiesMetadata = this.Service.GetEntityMetadatas();
                    });
                },
                PostWorkCallBack = args =>
                {
                    // What should happened after Work
                    this.PrepareSection<ModelSystemUser>(this.Users, this.CLB_Users, this.DownloadUsers);
                    this.PrepareSection<ModelRole>(this.Roles, this.CLB_Roles, this.DownloadRoles);

                    // Clear WebBrowser
                    this.wB_Permissions1.RemoveAll();
                },
                AsyncArgument = null,
                IsCancelable = false,
                MessageWidth = 340,
                MessageHeight = 150
            });
        }

        /// <summary>
        /// Start export to Excel.
        /// </summary>
        private void TSMI_SubItem_ExcelSingleFile_Click(object sender, EventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Microsoft Excel Worksheet (*.xlsx)|*.xlsx|Microsoft Excel Worksheet Macro (*.xlsm)|*.xlsm";
            DialogResult dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var fileName = saveFileDialog.FileName;
                this.WorkAsync(new WorkAsyncInfo()
                {
                    Message = $"Exporting to Excel File ({ fileName })",
                    Work = (backgroundWorker, args) =>
                    {
                        this.WrapAsyncFunction(() =>
                        {
                            ExcelExporter exporter = new ExcelExporter();
                            FileInfo excelFile = exporter.Export(this, fileName, new ExcelSingleWorkbookConfiguration());

                            if (excelFile.Exists)
                            {
                                DialogResult result = MessageBox.Show("Do You want to open newly created file ?", "Open Excel", MessageBoxButtons.OKCancel);
                                if (result == DialogResult.OK)
                                {
                                    Process.Start(excelFile.FullName);
                                }
                            }
                        });
                    },
                    AsyncArgument = null,
                    IsCancelable = false,
                    MessageWidth = 340,
                    MessageHeight = 150
                });
            }
        }

        /// <summary>
        /// Wraps UI async functions.
        /// </summary>
        private void WrapAsyncFunction(Action action)
        {
            SetAllControlsEnabled del = new SetAllControlsEnabled(this.SetAllEnable);

            // Disable Controls
            this.Invoke(del, new object[] { false });

            action();

            // Enable Controls
            this.Invoke(del, new object[] { true });
        }

        /// <summary>
        /// Prepare single section.
        /// </summary>
        private void PrepareSection<TModel>(List<TModel> modelList, CheckedListBox listBox, Action action)
        {
            modelList?.Clear();
            listBox?.Items?.Clear();
            this.ExecuteMethod(action);
        }

        /// <summary>
        /// Download Users from CRM.
        /// </summary>
        private void DownloadUsers()
        {
            // Retrieved users
            var users = RetrieveMultiple(FetchQueries.GetUsers);
            // Add users to CheckedListBox
            var orderedUsers = users.Entities.OrderBy(selector => selector.Attributes[ModelSystemUser.Fields.FullName]);
            foreach (var user in orderedUsers)
            {
                var modelUser = user.ToSystemUser();
                // Add parsed User to List
                this.Users.Add(modelUser);
                // Add parsed User to CheckedListBox
                this.CLB_Users.Items.Add(modelUser.FullName, false);
            }
        }

        /// <summary>
        /// Download Roles from CRM.
        /// </summary>
        private void DownloadRoles()
        {
            // Retrieved Roles
            var roles = RetrieveMultiple(FetchQueries.GetRoles);
            // Add roles to CheckedListBox
            var orderedRoles = roles.Entities.OrderBy(selector => selector.Attributes[ModelRole.Fields.Name]);
            foreach (var role in orderedRoles)
            {
                var modelRole = role.ToRole();
                // Add parsed Role to List
                this.Roles.Add(modelRole);
                // Add parsed Role to CheckedListBox
                this.CLB_Roles.Items.Add(modelRole.Name, false);
            }
        }

        /// <summary>
        /// Allow for only one currently selected element in Users.
        /// </summary>
        private void CLB_Users_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox listBox = (CheckedListBox)sender;
            this.ExecuteMethod<CheckedListBox>(this.OnIndexChanged, listBox);
        }

        /// <summary>
        /// Set if Controls should be enabled for User to click them.
        /// </summary>
        private void SetAllEnable(bool isEnabled)
        {
            this.CLB_Users.Enabled = isEnabled;
            this.CLB_Roles.Enabled = isEnabled;
            this.wB_Permissions1.IsEnabled = isEnabled;
        }

        /// <summary>
        /// Run asynchroniously to not stop XrmToolBox.
        /// </summary>
        private void OnIndexChanged(CheckedListBox listBox)
        {
            // Prevent from further clicking
            this.SetAllEnable(false);

            // Uncheck everything
            listBox.ClearAllSelections();
            // Set as checked only checked item
            listBox.SetItemCheckState(listBox.SelectedIndex, CheckState.Checked);

            // Checked User
            var userIndex = listBox.SelectedIndex;
            // Checked Model connected with User
            CurrentSelectedUser = this.Users.ElementAt(userIndex);
            // Calculate permissions for selected User
            CalculatePermissions();

            // After everything enable - enable Roles for future plans
            this.SetAllEnable(true);
        }

        /// <summary>
        /// Main method used for calculating permissions and displaying them in the Permissions box.
        /// </summary>
        private void CalculatePermissions()
        {
            this.wB_Permissions1.RemoveAll();

            // Don't count anything if user is not selected
            if (this.CurrentSelectedUser == null)
            {
                return;
            }

            // Returns all Roles for specified User by SystemUserId
            var userRoles = this.RetrieveMultiple(FetchQueriesHelper.GetRolesForUser(this.CurrentSelectedUser)).ToSystemModelWithRoles();
            CheckRolesForSelectedUser(userRoles);
            //var html = HtmlHelper.PrepareHtml(userRoles, this, this.EntitiesMetadata);

            ICollection<DataRow> rows = GetDataRowCollection(userRoles, this.EntitiesMetadata);
            this.wB_Permissions1.AddRange(rows);
        }

        /// <summary>
        /// Pobierz dane dla tabeli Entity Permisiions z CRM.
        /// </summary>
        public ICollection<DataRow> GetDataRowCollection(ModelSystemUserWithRoles user, List<EntityMetadata> metadata)
        {
            List<DataRow> result = new List<DataRow>();
            Dictionary<ModelRole, List<ModelRolePrivilege>> LoadedPrivileges =
                new Dictionary<ModelRole, List<ModelRolePrivilege>>();

            foreach (var role in user.Roles)
            {
                var rolePrivileges = GetRolePrivileges(role);
                LoadedPrivileges.Add(role, rolePrivileges);
            }

            // For each Entity
            foreach (EntityMetadata entity in metadata)
            {
                if (user.Roles.Count == 0)
                {
                    continue;
                }

                foreach (var privilege in LoadedPrivileges)
                {
                    DataRow dataRow = new DataRow()
                    {
                        EntityName = entity.DisplayName.UserLocalizedLabel.Label,
                        EntityLogicalName = entity.LogicalName,
                        Role = privilege.Key.Name,
                        Read = GetPrivilegeRange(entity, privilege, PrivilegeType.Read),
                        Write = GetPrivilegeRange(entity, privilege, PrivilegeType.Write),
                        Append = GetPrivilegeRange(entity, privilege, PrivilegeType.Append),
                        AppendTo = GetPrivilegeRange(entity, privilege, PrivilegeType.AppendTo),
                        Assign = GetPrivilegeRange(entity, privilege, PrivilegeType.Assign),
                        Delete = GetPrivilegeRange(entity, privilege, PrivilegeType.Delete),
                        Share = GetPrivilegeRange(entity, privilege, PrivilegeType.Share),
                    };
                    result.Add(dataRow);
                }
            }

            return result;
        }
        public RoleRange GetPrivilegeRange(EntityMetadata entity, KeyValuePair<ModelRole, List<ModelRolePrivilege>> privilege, PrivilegeType type)
        {
            SecurityPrivilegeMetadata entityPrivigle = entity.Privileges.Where(p => p.PrivilegeType == type).FirstOrDefault();
            if (entityPrivigle == null)
            {
                return RoleRange.None;
            }
            ModelRolePrivilege rolePrivigle = privilege.Value.Where(model => model.PrivilegeId == entityPrivigle.PrivilegeId).FirstOrDefault();
            if (rolePrivigle == null)
            {
                return RoleRange.None;
            }
            ModelDepthMask depthMask = Dictionary.DepthMasks.Where(model => (int)model.Value == rolePrivigle.Mask).FirstOrDefault();
            return depthMask.RoleRange;
        }

        /// <summary>
        /// Check Roles in CheckedListBox from given User.
        /// </summary>
        private void CheckRolesForSelectedUser(ModelSystemUserWithRoles parsedUserRoles)
        {
            // Clear Roles CheckedListBox
            this.CLB_Roles.ClearAllSelections();
            // Check each Role
            foreach (var role in parsedUserRoles.Roles)
            {
                var roleIndex = this.Roles.GetIndexOfRole(role);
                // Check Role
                this.CLB_Roles.SetItemCheckState(roleIndex, CheckState.Checked);
            }
        }

        #endregion
    }
}