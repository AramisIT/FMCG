using Aramis;

namespace AtosFMCG.DatabaseObjects.Catalogs
{
    partial class UsersItemForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersItemForm));
            this.DepartmentControl = new Aramis.AramisSearchLookUpEdit();
            this.DepartmentLabel = new DevExpress.XtraEditors.LabelControl();
            this.PostControl = new Aramis.AramisSearchLookUpEdit();
            this.PostLabel = new DevExpress.XtraEditors.LabelControl();
            this.DescriptionControl = new DevExpress.XtraEditors.TextEdit();
            this.ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.OK = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.detach = new DevExpress.XtraBars.BarButtonItem();
            this.ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.RolesButtonsBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.MainPage = new DevExpress.XtraTab.XtraTabPage();
            this.Email = new DevExpress.XtraEditors.TextEdit();
            this.labelControl8 = new DevExpress.XtraEditors.LabelControl();
            this.DefaultInterfaceControl = new Aramis.AramisSearchLookUpEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.stringMobilePhone = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.MobilePhoneLabel = new DevExpress.XtraEditors.LabelControl();
            this.ParentId = new Aramis.AramisSearchLookUpEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Skin = new DevExpress.XtraEditors.ComboBoxEdit();
            this.EnteringToSystemPage = new DevExpress.XtraTab.XtraTabPage();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.RepeatedPassword = new DevExpress.XtraEditors.TextEdit();
            this.EnteredPassword = new DevExpress.XtraEditors.TextEdit();
            this.LoginControl = new DevExpress.XtraEditors.TextEdit();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.RolesControl = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem4 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem6 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem7 = new DevExpress.XtraBars.BarButtonItem();
            this.printBarcode = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.DepartmentControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DescriptionControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.MainPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Email.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefaultInterfaceControl.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.stringMobilePhone.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParentId.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Skin.Properties)).BeginInit();
            this.EnteringToSystemPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatedPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnteredPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoginControl.Properties)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RolesControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // DepartmentControl
            // 
            this.DepartmentControl.BaseFilter = null;
            this.DepartmentControl.Location = new System.Drawing.Point(157, 36);
            this.DepartmentControl.Name = "DepartmentControl";
            this.DepartmentControl.Properties.BaseFilter = null;
            this.DepartmentControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.DepartmentControl.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.DepartmentControl.Properties.NullText = "";
            this.DepartmentControl.Size = new System.Drawing.Size(312, 20);
            this.DepartmentControl.TabIndex = 1;
            // 
            // DepartmentLabel
            // 
            this.DepartmentLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DepartmentLabel.Location = new System.Drawing.Point(15, 39);
            this.DepartmentLabel.Name = "DepartmentLabel";
            this.DepartmentLabel.Size = new System.Drawing.Size(87, 14);
            this.DepartmentLabel.TabIndex = 28;
            this.DepartmentLabel.Text = "Подразделение";
            // 
            // PostControl
            // 
            this.PostControl.BaseFilter = null;
            this.PostControl.Location = new System.Drawing.Point(157, 63);
            this.PostControl.Name = "PostControl";
            this.PostControl.Properties.BaseFilter = null;
            this.PostControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.PostControl.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.PostControl.Properties.NullText = "";
            this.PostControl.Size = new System.Drawing.Size(312, 20);
            this.PostControl.TabIndex = 2;
            // 
            // PostLabel
            // 
            this.PostLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PostLabel.Location = new System.Drawing.Point(15, 66);
            this.PostLabel.Name = "PostLabel";
            this.PostLabel.Size = new System.Drawing.Size(64, 14);
            this.PostLabel.TabIndex = 30;
            this.PostLabel.Text = "Должность";
            // 
            // DescriptionControl
            // 
            this.DescriptionControl.Location = new System.Drawing.Point(157, 9);
            this.DescriptionControl.Name = "DescriptionControl";
            this.DescriptionControl.Properties.MaxLength = 66643;
            this.DescriptionControl.Size = new System.Drawing.Size(312, 20);
            this.DescriptionControl.TabIndex = 0;
            // 
            // ribbonStatusBar
            // 
            this.ribbonStatusBar.ItemLinks.Add(this.OK);
            this.ribbonStatusBar.ItemLinks.Add(this.barButtonItem2);
            this.ribbonStatusBar.ItemLinks.Add(this.barButtonItem3);
            this.ribbonStatusBar.ItemLinks.Add(this.detach);
            this.ribbonStatusBar.ItemLinks.Add(this.printBarcode);
            this.ribbonStatusBar.Location = new System.Drawing.Point(0, 294);
            this.ribbonStatusBar.Name = "ribbonStatusBar";
            this.ribbonStatusBar.Ribbon = this.ribbon;
            this.ribbonStatusBar.Size = new System.Drawing.Size(482, 31);
            // 
            // OK
            // 
            this.OK.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.OK.Caption = "OK";
            this.OK.Id = 0;
            this.OK.ImageIndex = 0;
            this.OK.Name = "OK";
            this.OK.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OK_ItemClick);
            // 
            // barButtonItem2
            // 
            this.barButtonItem2.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem2.Caption = "Записати";
            this.barButtonItem2.Id = 1;
            this.barButtonItem2.Name = "barButtonItem2";
            this.barButtonItem2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.Write_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem3.Caption = "Відміна";
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.ImageIndex = 1;
            this.barButtonItem3.Name = "barButtonItem3";
            this.barButtonItem3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem3_ItemClick);
            // 
            // detach
            // 
            this.detach.Caption = "Відкрепити";
            this.detach.Id = 6;
            this.detach.Name = "detach";
            this.detach.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.detach_ItemClick);
            // 
            // ribbon
            // 
            this.ribbon.ApplicationButtonText = null;
            this.ribbon.ExpandCollapseItem.Id = 0;
            this.ribbon.ExpandCollapseItem.Name = "";
            this.ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbon.ExpandCollapseItem,
            this.OK,
            this.barButtonItem2,
            this.barButtonItem3,
            this.detach,
            this.printBarcode});
            this.ribbon.Location = new System.Drawing.Point(0, 0);
            this.ribbon.MaxItemId = 9;
            this.ribbon.Name = "ribbon";
            this.ribbon.Size = new System.Drawing.Size(482, 49);
            this.ribbon.StatusBar = this.RolesButtonsBar;
            this.ribbon.Toolbar.ItemLinks.Add(this.OK, false, "", "", true);
            this.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Above;
            // 
            // RolesButtonsBar
            // 
            this.RolesButtonsBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.RolesButtonsBar.Location = new System.Drawing.Point(0, 0);
            this.RolesButtonsBar.Name = "RolesButtonsBar";
            this.RolesButtonsBar.Ribbon = this.ribbon;
            this.RolesButtonsBar.Size = new System.Drawing.Size(476, 27);
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl.Location = new System.Drawing.Point(0, 49);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.MainPage;
            this.xtraTabControl.Size = new System.Drawing.Size(482, 245);
            this.xtraTabControl.TabIndex = 0;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.MainPage,
            this.EnteringToSystemPage,
            this.xtraTabPage2});
            // 
            // MainPage
            // 
            this.MainPage.Controls.Add(this.Email);
            this.MainPage.Controls.Add(this.labelControl8);
            this.MainPage.Controls.Add(this.DefaultInterfaceControl);
            this.MainPage.Controls.Add(this.labelControl6);
            this.MainPage.Controls.Add(this.stringMobilePhone);
            this.MainPage.Controls.Add(this.labelControl5);
            this.MainPage.Controls.Add(this.MobilePhoneLabel);
            this.MainPage.Controls.Add(this.ParentId);
            this.MainPage.Controls.Add(this.labelControl7);
            this.MainPage.Controls.Add(this.labelControl4);
            this.MainPage.Controls.Add(this.DescriptionControl);
            this.MainPage.Controls.Add(this.Skin);
            this.MainPage.Controls.Add(this.DepartmentControl);
            this.MainPage.Controls.Add(this.DepartmentLabel);
            this.MainPage.Controls.Add(this.PostControl);
            this.MainPage.Controls.Add(this.PostLabel);
            this.MainPage.Name = "MainPage";
            this.MainPage.Size = new System.Drawing.Size(476, 217);
            this.MainPage.Text = "Настройка пользователя";
            // 
            // Email
            // 
            this.Email.Location = new System.Drawing.Point(157, 170);
            this.Email.Name = "Email";
            this.Email.Properties.MaxLength = 66643;
            this.Email.Size = new System.Drawing.Size(312, 20);
            this.Email.TabIndex = 6;
            // 
            // labelControl8
            // 
            this.labelControl8.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl8.Location = new System.Drawing.Point(15, 198);
            this.labelControl8.Name = "labelControl8";
            this.labelControl8.Size = new System.Drawing.Size(62, 14);
            this.labelControl8.TabIndex = 32;
            this.labelControl8.Text = "Интерфейс";
            // 
            // DefaultInterfaceControl
            // 
            this.DefaultInterfaceControl.BaseFilter = null;
            this.DefaultInterfaceControl.Location = new System.Drawing.Point(157, 195);
            this.DefaultInterfaceControl.MenuManager = this.ribbon;
            this.DefaultInterfaceControl.Name = "DefaultInterfaceControl";
            this.DefaultInterfaceControl.Properties.BaseFilter = null;
            this.DefaultInterfaceControl.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.DefaultInterfaceControl.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.DefaultInterfaceControl.Properties.NullText = "";
            this.DefaultInterfaceControl.Size = new System.Drawing.Size(311, 20);
            this.DefaultInterfaceControl.TabIndex = 7;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl6.Location = new System.Drawing.Point(15, 174);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(29, 14);
            this.labelControl6.TabIndex = 24;
            this.labelControl6.Text = "Email";
            // 
            // stringMobilePhone
            // 
            this.stringMobilePhone.Location = new System.Drawing.Point(157, 90);
            this.stringMobilePhone.Name = "stringMobilePhone";
            this.stringMobilePhone.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.stringMobilePhone.Properties.Appearance.BackColor2 = System.Drawing.Color.Transparent;
            this.stringMobilePhone.Properties.Appearance.BorderColor = System.Drawing.Color.Transparent;
            this.stringMobilePhone.Properties.Appearance.Options.UseBackColor = true;
            this.stringMobilePhone.Properties.Appearance.Options.UseBorderColor = true;
            this.stringMobilePhone.Properties.Mask.EditMask = "\\+38 \\(0\\d\\d\\) \\d\\d\\d-\\d\\d-\\d\\d";
            this.stringMobilePhone.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Regular;
            this.stringMobilePhone.Properties.MaxLength = 66643;
            this.stringMobilePhone.Size = new System.Drawing.Size(312, 20);
            this.stringMobilePhone.TabIndex = 3;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl5.Location = new System.Drawing.Point(15, 147);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(53, 14);
            this.labelControl5.TabIndex = 23;
            this.labelControl5.Text = "Родитель";
            // 
            // MobilePhoneLabel
            // 
            this.MobilePhoneLabel.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MobilePhoneLabel.Location = new System.Drawing.Point(15, 93);
            this.MobilePhoneLabel.Name = "MobilePhoneLabel";
            this.MobilePhoneLabel.Size = new System.Drawing.Size(117, 14);
            this.MobilePhoneLabel.TabIndex = 25;
            this.MobilePhoneLabel.Text = "Мобильный телефон";
            // 
            // ParentId
            // 
            this.ParentId.BaseFilter = null;
            this.ParentId.Location = new System.Drawing.Point(157, 144);
            this.ParentId.Name = "ParentId";
            this.ParentId.Properties.BaseFilter = null;
            this.ParentId.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.ParentId.Properties.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None);
            this.ParentId.Properties.NullText = "";
            this.ParentId.Size = new System.Drawing.Size(312, 20);
            this.ParentId.TabIndex = 5;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl7.Location = new System.Drawing.Point(15, 120);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(104, 14);
            this.labelControl7.TabIndex = 23;
            this.labelControl7.Text = "Стиль интерфейса";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl4.Location = new System.Drawing.Point(15, 12);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(121, 14);
            this.labelControl4.TabIndex = 22;
            this.labelControl4.Text = "Фамилия и инициалы";
            // 
            // Skin
            // 
            this.Skin.Location = new System.Drawing.Point(157, 117);
            this.Skin.Name = "Skin";
            this.Skin.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Skin.Size = new System.Drawing.Size(312, 20);
            this.Skin.TabIndex = 4;
            this.Skin.SelectedIndexChanged += new System.EventHandler(this.Skin_Modified);
            // 
            // EnteringToSystemPage
            // 
            this.EnteringToSystemPage.Controls.Add(this.labelControl3);
            this.EnteringToSystemPage.Controls.Add(this.labelControl2);
            this.EnteringToSystemPage.Controls.Add(this.labelControl1);
            this.EnteringToSystemPage.Controls.Add(this.RepeatedPassword);
            this.EnteringToSystemPage.Controls.Add(this.EnteredPassword);
            this.EnteringToSystemPage.Controls.Add(this.LoginControl);
            this.EnteringToSystemPage.Name = "EnteringToSystemPage";
            this.EnteringToSystemPage.Size = new System.Drawing.Size(476, 217);
            this.EnteringToSystemPage.Text = "Вход в систему";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl3.Location = new System.Drawing.Point(92, 128);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(104, 14);
            this.labelControl3.TabIndex = 16;
            this.labelControl3.Text = "Повторите пароль";
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl2.Location = new System.Drawing.Point(92, 102);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(80, 14);
            this.labelControl2.TabIndex = 15;
            this.labelControl2.Text = "Новый пароль";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelControl1.Location = new System.Drawing.Point(92, 62);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(34, 14);
            this.labelControl1.TabIndex = 14;
            this.labelControl1.Text = "Логин";
            // 
            // RepeatedPassword
            // 
            this.RepeatedPassword.Location = new System.Drawing.Point(209, 125);
            this.RepeatedPassword.Name = "RepeatedPassword";
            this.RepeatedPassword.Properties.PasswordChar = '*';
            this.RepeatedPassword.Size = new System.Drawing.Size(176, 20);
            this.RepeatedPassword.TabIndex = 2;
            this.RepeatedPassword.Enter += new System.EventHandler(this.Password_Enter);
            // 
            // EnteredPassword
            // 
            this.EnteredPassword.Location = new System.Drawing.Point(209, 99);
            this.EnteredPassword.Name = "EnteredPassword";
            this.EnteredPassword.Properties.PasswordChar = '*';
            this.EnteredPassword.Size = new System.Drawing.Size(176, 20);
            this.EnteredPassword.TabIndex = 1;
            this.EnteredPassword.Enter += new System.EventHandler(this.Password_Enter);
            // 
            // LoginControl
            // 
            this.LoginControl.Location = new System.Drawing.Point(209, 59);
            this.LoginControl.Name = "LoginControl";
            this.LoginControl.Properties.MaxLength = 25;
            this.LoginControl.Size = new System.Drawing.Size(176, 20);
            this.LoginControl.TabIndex = 0;
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.RolesControl);
            this.xtraTabPage2.Controls.Add(this.RolesButtonsBar);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(476, 217);
            this.xtraTabPage2.Text = "Роли";
            // 
            // RolesControl
            // 
            this.RolesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RolesControl.Location = new System.Drawing.Point(0, 27);
            this.RolesControl.MainView = this.gridView1;
            this.RolesControl.MenuManager = this.ribbon;
            this.RolesControl.Name = "RolesControl";
            this.RolesControl.Size = new System.Drawing.Size(476, 190);
            this.RolesControl.TabIndex = 1;
            this.RolesControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.RolesControl;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem1.Caption = "OK";
            this.barButtonItem1.Id = 0;
            this.barButtonItem1.ImageIndex = 0;
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItem4
            // 
            this.barButtonItem4.Caption = "Добавить новый элемент";
            this.barButtonItem4.Id = 2;
            this.barButtonItem4.LargeImageIndex = 2;
            this.barButtonItem4.Name = "barButtonItem4";
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem5.Caption = "Записать";
            this.barButtonItem5.Id = 1;
            this.barButtonItem5.Name = "barButtonItem5";
            // 
            // barButtonItem6
            // 
            this.barButtonItem6.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItem6.Caption = "Отмена";
            this.barButtonItem6.Id = 2;
            this.barButtonItem6.ImageIndex = 1;
            this.barButtonItem6.Name = "barButtonItem6";
            // 
            // barButtonItem7
            // 
            this.barButtonItem7.Caption = "Закрыть";
            this.barButtonItem7.Id = 5;
            this.barButtonItem7.LargeImageIndex = 4;
            this.barButtonItem7.Name = "barButtonItem7";
            // 
            // printBarcode
            // 
            this.printBarcode.Caption = "Друкувати штрих-код";
            this.printBarcode.Id = 8;
            this.printBarcode.Name = "printBarcode";
            this.printBarcode.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.printBarcode_ItemClick);
            // 
            // UsersItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 325);
            this.Controls.Add(this.xtraTabControl);
            this.Controls.Add(this.ribbonStatusBar);
            this.Controls.Add(this.ribbon);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "UsersItemForm";
            this.Ribbon = this.ribbon;
            this.StatusBar = this.ribbonStatusBar;
            this.Text = "Пользователи: ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UsersItemForm_FormClosed);
            this.Load += new System.EventHandler(this.Itemform_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Itemform_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DepartmentControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PostControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DescriptionControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.MainPage.ResumeLayout(false);
            this.MainPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Email.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DefaultInterfaceControl.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.stringMobilePhone.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ParentId.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Skin.Properties)).EndInit();
            this.EnteringToSystemPage.ResumeLayout(false);
            this.EnteringToSystemPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RepeatedPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EnteredPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LoginControl.Properties)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RolesControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

		private AramisSearchLookUpEdit DepartmentControl;
		private DevExpress.XtraEditors.LabelControl DepartmentLabel;
		private AramisSearchLookUpEdit PostControl;
		private DevExpress.XtraEditors.LabelControl PostLabel;



        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.BarButtonItem OK;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private AramisSearchLookUpEdit ParentId;
        private DevExpress.XtraEditors.ComboBoxEdit Skin;
        private DevExpress.XtraEditors.TextEdit LoginControl;
        private DevExpress.XtraEditors.TextEdit RepeatedPassword;
        private DevExpress.XtraEditors.TextEdit EnteredPassword;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit DescriptionControl;
        private DevExpress.XtraTab.XtraTabPage MainPage;
        private DevExpress.XtraTab.XtraTabPage EnteringToSystemPage;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private DevExpress.XtraBars.BarButtonItem detach;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar RolesButtonsBar;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItem4;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraBars.BarButtonItem barButtonItem6;
        private DevExpress.XtraBars.BarButtonItem barButtonItem7;
        private DevExpress.XtraEditors.LabelControl MobilePhoneLabel;
        private DevExpress.XtraEditors.TextEdit stringMobilePhone;
        private AramisSearchLookUpEdit DefaultInterfaceControl;
        private DevExpress.XtraEditors.LabelControl labelControl8;
        private DevExpress.XtraEditors.TextEdit Email;
        private DevExpress.XtraGrid.GridControl RolesControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraBars.BarButtonItem printBarcode;
    }
}
