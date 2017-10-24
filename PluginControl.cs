using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using Netwise.XrmToolBox.RolesHelper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace Netwise.XrmToolBox.RolesHelper
{
    /// <summary>
    /// Control which will be displayed in XrmToolBox.
    /// </summary>
    public class PluginControl : PluginControlBase
    {
        private System.Windows.Forms.GroupBox GB_Roles;
        private System.Windows.Forms.CheckedListBox CLB_Roles;
        private System.Windows.Forms.GroupBox GB_Permissions;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem B_Close;
        private System.Windows.Forms.ToolStripMenuItem B_LoadData;
        private System.Windows.Forms.CheckedListBox CLB_Users;
        private WebBrowser WB_Permissions;
        private GroupBox GB_Sorting;
        public ComboBox CB_Sort;
        private System.Windows.Forms.GroupBox GB_Users;

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

            // Add Columns by which selection can be made
            this.CB_Sort.Text = string.Empty;
        }

        #region Public Methods

        /// <summary>
        /// Returns currently selected Sortable Header based on what is currently selected in ComboBox.
        /// </summary>
        public ModelSortableColumnHeader GetCurrentSelectedSortable()
        {
            var headerName = this.CB_Sort.Text;
            var selectedSortables = HtmlHelper.SortableColumnHeaders.Where(model => model.HeaderName.Equals(headerName));
            if (selectedSortables.Count() > 0)
            {
                var currentSortable = selectedSortables.First();
                return currentSortable;
            }
            else
            {
                return null;
            }
        }

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

        #endregion

        #region Designer Component Initialization

        private void InitializeComponent()
        {
            this.GB_Users = new System.Windows.Forms.GroupBox();
            this.CLB_Users = new System.Windows.Forms.CheckedListBox();
            this.GB_Roles = new System.Windows.Forms.GroupBox();
            this.CLB_Roles = new System.Windows.Forms.CheckedListBox();
            this.GB_Permissions = new System.Windows.Forms.GroupBox();
            this.GB_Sorting = new System.Windows.Forms.GroupBox();
            this.CB_Sort = new System.Windows.Forms.ComboBox();
            this.WB_Permissions = new System.Windows.Forms.WebBrowser();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.B_Close = new System.Windows.Forms.ToolStripMenuItem();
            this.B_LoadData = new System.Windows.Forms.ToolStripMenuItem();
            this.GB_Users.SuspendLayout();
            this.GB_Roles.SuspendLayout();
            this.GB_Permissions.SuspendLayout();
            this.GB_Sorting.SuspendLayout();
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
            this.GB_Permissions.Controls.Add(this.GB_Sorting);
            this.GB_Permissions.Controls.Add(this.WB_Permissions);
            this.GB_Permissions.Location = new System.Drawing.Point(538, 27);
            this.GB_Permissions.Name = "GB_Permissions";
            this.GB_Permissions.Size = new System.Drawing.Size(294, 619);
            this.GB_Permissions.TabIndex = 2;
            this.GB_Permissions.TabStop = false;
            this.GB_Permissions.Text = "Permissions";
            // 
            // GB_Sorting
            // 
            this.GB_Sorting.Controls.Add(this.CB_Sort);
            this.GB_Sorting.Location = new System.Drawing.Point(7, 20);
            this.GB_Sorting.Name = "GB_Sorting";
            this.GB_Sorting.Size = new System.Drawing.Size(200, 54);
            this.GB_Sorting.TabIndex = 1;
            this.GB_Sorting.TabStop = false;
            this.GB_Sorting.Text = "Sorting";
            // 
            // CB_Sort
            // 
            this.CB_Sort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Sort.FormattingEnabled = true;
            this.CB_Sort.Location = new System.Drawing.Point(7, 20);
            this.CB_Sort.Name = "CB_Sort";
            this.CB_Sort.Size = new System.Drawing.Size(187, 21);
            this.CB_Sort.TabIndex = 0;
            this.CB_Sort.SelectedIndexChanged += new System.EventHandler(this.CB_Sort_SelectedIndexChanged);
            // 
            // WB_Permissions
            // 
            this.WB_Permissions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WB_Permissions.Location = new System.Drawing.Point(7, 80);
            this.WB_Permissions.MinimumSize = new System.Drawing.Size(20, 20);
            this.WB_Permissions.Name = "WB_Permissions";
            this.WB_Permissions.ScriptErrorsSuppressed = true;
            this.WB_Permissions.Size = new System.Drawing.Size(288, 529);
            this.WB_Permissions.TabIndex = 0;
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.B_Close,
            this.B_LoadData});
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
            this.GB_Sorting.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Change table sorting based on selected Column.
        /// </summary>
        private void CB_Sort_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If selected item is the same as one currently displayed
            if ((sender as ComboBox).SelectedText.Equals(this.CB_Sort.Text))
            {
                return;
            }

            var selectedHeader = this.GetCurrentSelectedSortable();
            if (selectedHeader != null)
            {
                this.CalculatePermissions();
            }
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
                    // Entity Metadata
                    this.EntitiesMetadata = this.Service.GetEntityMetadatas();
                },
                PostWorkCallBack = args =>
                {
                    // What should happened after Work
                    this.PrepareSection<ModelSystemUser>(this.Users, this.CLB_Users, this.DownloadUsers);
                    this.PrepareSection<ModelRole>(this.Roles, this.CLB_Roles, this.DownloadRoles);

                    // Clear WebBrowser
                    this.WB_Permissions.DocumentText = string.Empty;
                },
                AsyncArgument = null,
                IsCancelable = false,
                MessageWidth = 340,
                MessageHeight = 150
            });
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
        }

        /// <summary>
        /// Run asynchroniously to not stop XrmToolBox.
        /// </summary>
        private void OnIndexChanged(CheckedListBox listBox)
        {
            // Prevent from further clicking
            SetAllEnable(false);

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

            // After everything enable Users
            this.CLB_Users.Enabled = true;
        }

        /// <summary>
        /// Main method used for calculating permissions and displaying them in the Permissions box.
        /// </summary>
        private void CalculatePermissions()
        {
            this.WB_Permissions.DocumentText = string.Empty;

            // Don't count anything if user is not selected
            if (this.CurrentSelectedUser == null)
            {
                return;
            }

            // Returns all Roles for specified User by SystemUserId
            var userRoles = this.RetrieveMultiple(FetchQueriesHelper.GetRolesForUser(this.CurrentSelectedUser));
            // User with all they Roles.
            var parsedUserRoles = userRoles.ToSystemModelWithRoles();
            // Check Roles in CheckedListBox
            CheckRolesForSelectedUser(parsedUserRoles);
            // Prepare HTML
            var html = HtmlHelper.PrepareHtml(parsedUserRoles, this, this.EntitiesMetadata);
            // Set HTML to WebBrowser
            this.WB_Permissions.DocumentText = html;
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