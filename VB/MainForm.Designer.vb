Namespace DXHtmlEditor

    Partial Class MainForm

        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.IContainer = Nothing

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing AndAlso (Me.components IsNot Nothing) Then
                Me.components.Dispose()
            End If

            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DXHtmlEditor.MainForm))
            Me.ribbonControl1 = New DevExpress.XtraBars.Ribbon.RibbonControl()
            Me.skinDropDownButtonItem1 = New DevExpress.XtraBars.SkinDropDownButtonItem()
            Me.skinPaletteRibbonGalleryBarItem1 = New DevExpress.XtraBars.SkinPaletteRibbonGalleryBarItem()
            Me.bbiUndo = New DevExpress.XtraBars.BarButtonItem()
            Me.bbiRedo = New DevExpress.XtraBars.BarButtonItem()
            Me.ribbonPage1 = New DevExpress.XtraBars.Ribbon.RibbonPage()
            Me.ribbonPageGroup2 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
            Me.ribbonPageGroup1 = New DevExpress.XtraBars.Ribbon.RibbonPageGroup()
            Me.ribbonPage2 = New DevExpress.XtraBars.Ribbon.RibbonPage()
            Me.layoutControl = New DevExpress.XtraLayout.LayoutControl()
            Me.panelControl1 = New DevExpress.XtraEditors.PanelControl()
            Me.dxHtmlEditorWebView = New DXHtmlEditor.DXHtmlEditorWebView()
            Me.targetMemoEdit = New DevExpress.XtraEditors.MemoEdit()
            Me.sourceMemoEdit = New DevExpress.XtraEditors.MemoEdit()
            Me.layoutControlGroup1 = New DevExpress.XtraLayout.LayoutControlGroup()
            Me.tabbedControlGroup1 = New DevExpress.XtraLayout.TabbedControlGroup()
            Me.layoutControlGroup2 = New DevExpress.XtraLayout.LayoutControlGroup()
            Me.layoutControlItem4 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.layoutControlGroup3 = New DevExpress.XtraLayout.LayoutControlGroup()
            Me.layoutControlItem1 = New DevExpress.XtraLayout.LayoutControlItem()
            Me.layoutControlGroup4 = New DevExpress.XtraLayout.LayoutControlGroup()
            Me.layoutControlItem2 = New DevExpress.XtraLayout.LayoutControlItem()
            CType((Me.ribbonControl1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.layoutControl), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.layoutControl.SuspendLayout()
            CType((Me.panelControl1), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panelControl1.SuspendLayout()
            CType((Me.dxHtmlEditorWebView), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.targetMemoEdit.Properties), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.sourceMemoEdit.Properties), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.layoutControlGroup1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.tabbedControlGroup1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.layoutControlGroup2), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.layoutControlItem4), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.layoutControlGroup3), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.layoutControlItem1), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.layoutControlGroup4), System.ComponentModel.ISupportInitialize).BeginInit()
            CType((Me.layoutControlItem2), System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' ribbonControl1
            ' 
            Me.ribbonControl1.ExpandCollapseItem.Id = 0
            Me.ribbonControl1.Items.AddRange(New DevExpress.XtraBars.BarItem() {Me.ribbonControl1.ExpandCollapseItem, Me.skinDropDownButtonItem1, Me.skinPaletteRibbonGalleryBarItem1, Me.bbiUndo, Me.bbiRedo})
            Me.ribbonControl1.Location = New System.Drawing.Point(0, 0)
            Me.ribbonControl1.MaxItemId = 6
            Me.ribbonControl1.Name = "ribbonControl1"
            Me.ribbonControl1.Pages.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPage() {Me.ribbonPage1})
            Me.ribbonControl1.Size = New System.Drawing.Size(848, 201)
            ' 
            ' skinDropDownButtonItem1
            ' 
            Me.skinDropDownButtonItem1.Id = 1
            Me.skinDropDownButtonItem1.Name = "skinDropDownButtonItem1"
            ' 
            ' skinPaletteRibbonGalleryBarItem1
            ' 
            Me.skinPaletteRibbonGalleryBarItem1.Caption = "skinPaletteRibbonGalleryBarItem1"
            Me.skinPaletteRibbonGalleryBarItem1.Id = 3
            Me.skinPaletteRibbonGalleryBarItem1.Name = "skinPaletteRibbonGalleryBarItem1"
            ' 
            ' bbiUndo
            ' 
            Me.bbiUndo.Caption = "Undo"
            Me.bbiUndo.Id = 4
            Me.bbiUndo.ImageOptions.SvgImage = CType((resources.GetObject("bbiUndo.ImageOptions.SvgImage")), DevExpress.Utils.Svg.SvgImage)
            Me.bbiUndo.Name = "bbiUndo"
            AddHandler Me.bbiUndo.ItemClick, New DevExpress.XtraBars.ItemClickEventHandler(AddressOf Me.OnUndo)
            ' 
            ' bbiRedo
            ' 
            Me.bbiRedo.Caption = "Redo"
            Me.bbiRedo.Id = 5
            Me.bbiRedo.ImageOptions.SvgImage = CType((resources.GetObject("bbiRedo.ImageOptions.SvgImage")), DevExpress.Utils.Svg.SvgImage)
            Me.bbiRedo.Name = "bbiRedo"
            AddHandler Me.bbiRedo.ItemClick, New DevExpress.XtraBars.ItemClickEventHandler(AddressOf Me.OnRedo)
            ' 
            ' ribbonPage1
            ' 
            Me.ribbonPage1.Groups.AddRange(New DevExpress.XtraBars.Ribbon.RibbonPageGroup() {Me.ribbonPageGroup2, Me.ribbonPageGroup1})
            Me.ribbonPage1.Name = "ribbonPage1"
            Me.ribbonPage1.Text = "View"
            ' 
            ' ribbonPageGroup2
            ' 
            Me.ribbonPageGroup2.ItemLinks.Add(Me.bbiUndo)
            Me.ribbonPageGroup2.ItemLinks.Add(Me.bbiRedo)
            Me.ribbonPageGroup2.Name = "ribbonPageGroup2"
            Me.ribbonPageGroup2.Text = "Edit"
            ' 
            ' ribbonPageGroup1
            ' 
            Me.ribbonPageGroup1.ItemLinks.Add(Me.skinDropDownButtonItem1)
            Me.ribbonPageGroup1.ItemLinks.Add(Me.skinPaletteRibbonGalleryBarItem1)
            Me.ribbonPageGroup1.Name = "ribbonPageGroup1"
            Me.ribbonPageGroup1.Text = "Appearance"
            ' 
            ' ribbonPage2
            ' 
            Me.ribbonPage2.Name = "ribbonPage2"
            Me.ribbonPage2.Text = "ribbonPage2"
            ' 
            ' layoutControl
            ' 
            Me.layoutControl.AllowCustomization = False
            Me.layoutControl.Controls.Add(Me.panelControl1)
            Me.layoutControl.Controls.Add(Me.targetMemoEdit)
            Me.layoutControl.Controls.Add(Me.sourceMemoEdit)
            Me.layoutControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.layoutControl.Location = New System.Drawing.Point(0, 201)
            Me.layoutControl.Name = "layoutControl"
            Me.layoutControl.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = New System.Drawing.Rectangle(2455, 1009, 975, 877)
            Me.layoutControl.Root = Me.layoutControlGroup1
            Me.layoutControl.Size = New System.Drawing.Size(848, 448)
            Me.layoutControl.TabIndex = 2
            ' 
            ' panelControl1
            ' 
            Me.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
            Me.panelControl1.Controls.Add(Me.dxHtmlEditorWebView)
            Me.panelControl1.Location = New System.Drawing.Point(32, 61)
            Me.panelControl1.Name = "panelControl1"
            Me.panelControl1.Size = New System.Drawing.Size(784, 355)
            Me.panelControl1.TabIndex = 7
            ' 
            ' dxHtmlEditorWebView
            ' 
            Me.dxHtmlEditorWebView.AllowExternalDrop = True
            Me.dxHtmlEditorWebView.CreationProperties = Nothing
            Me.dxHtmlEditorWebView.DefaultBackgroundColor = System.Drawing.Color.White
            Me.dxHtmlEditorWebView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.dxHtmlEditorWebView.Location = New System.Drawing.Point(2, 2)
            Me.dxHtmlEditorWebView.Name = "dxHtmlEditorWebView"
            Me.dxHtmlEditorWebView.Size = New System.Drawing.Size(780, 351)
            Me.dxHtmlEditorWebView.TabIndex = 6
            Me.dxHtmlEditorWebView.ZoomFactor = 1R
            ' 
            ' targetMemoEdit
            ' 
            Me.targetMemoEdit.Location = New System.Drawing.Point(32, 61)
            Me.targetMemoEdit.MenuManager = Me.ribbonControl1
            Me.targetMemoEdit.Name = "targetMemoEdit"
            Me.targetMemoEdit.Properties.[ReadOnly] = True
            Me.targetMemoEdit.Size = New System.Drawing.Size(784, 355)
            Me.targetMemoEdit.StyleController = Me.layoutControl
            Me.targetMemoEdit.TabIndex = 5
            ' 
            ' sourceMemoEdit
            ' 
            Me.sourceMemoEdit.Location = New System.Drawing.Point(32, 61)
            Me.sourceMemoEdit.MenuManager = Me.ribbonControl1
            Me.sourceMemoEdit.Name = "sourceMemoEdit"
            Me.sourceMemoEdit.Size = New System.Drawing.Size(784, 355)
            Me.sourceMemoEdit.StyleController = Me.layoutControl
            Me.sourceMemoEdit.TabIndex = 4
            AddHandler Me.sourceMemoEdit.TextChanged, New System.EventHandler(AddressOf Me.OnSourceMemoEditTextChanged)
            ' 
            ' layoutControlGroup1
            ' 
            Me.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.[True]
            Me.layoutControlGroup1.GroupBordersVisible = False
            Me.layoutControlGroup1.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.tabbedControlGroup1})
            Me.layoutControlGroup1.Name = "Root"
            Me.layoutControlGroup1.Size = New System.Drawing.Size(848, 448)
            Me.layoutControlGroup1.TextVisible = False
            ' 
            ' tabbedControlGroup1
            ' 
            Me.tabbedControlGroup1.Location = New System.Drawing.Point(0, 0)
            Me.tabbedControlGroup1.Name = "tabbedControlGroup1"
            Me.tabbedControlGroup1.SelectedTabPage = Me.layoutControlGroup2
            Me.tabbedControlGroup1.Size = New System.Drawing.Size(822, 422)
            Me.tabbedControlGroup1.TabPages.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutControlGroup2, Me.layoutControlGroup3, Me.layoutControlGroup4})
            ' 
            ' layoutControlGroup2
            ' 
            Me.layoutControlGroup2.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutControlItem4})
            Me.layoutControlGroup2.Location = New System.Drawing.Point(0, 0)
            Me.layoutControlGroup2.Name = "layoutControlGroup2"
            Me.layoutControlGroup2.Size = New System.Drawing.Size(790, 361)
            Me.layoutControlGroup2.Text = "HTML Editor"
            ' 
            ' layoutControlItem4
            ' 
            Me.layoutControlItem4.Control = Me.panelControl1
            Me.layoutControlItem4.Location = New System.Drawing.Point(0, 0)
            Me.layoutControlItem4.Name = "layoutControlItem4"
            Me.layoutControlItem4.Size = New System.Drawing.Size(790, 361)
            Me.layoutControlItem4.Text = "Edit Html"
            Me.layoutControlItem4.TextSize = New System.Drawing.Size(0, 0)
            Me.layoutControlItem4.TextVisible = False
            ' 
            ' layoutControlGroup3
            ' 
            Me.layoutControlGroup3.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutControlItem1})
            Me.layoutControlGroup3.Location = New System.Drawing.Point(0, 0)
            Me.layoutControlGroup3.Name = "layoutControlGroup3"
            Me.layoutControlGroup3.Size = New System.Drawing.Size(790, 361)
            Me.layoutControlGroup3.Text = "Plain Text Editor"
            ' 
            ' layoutControlItem1
            ' 
            Me.layoutControlItem1.Control = Me.sourceMemoEdit
            Me.layoutControlItem1.Location = New System.Drawing.Point(0, 0)
            Me.layoutControlItem1.Name = "layoutControlItem1"
            Me.layoutControlItem1.Size = New System.Drawing.Size(790, 361)
            Me.layoutControlItem1.Text = "Set Html Text"
            Me.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Top
            Me.layoutControlItem1.TextSize = New System.Drawing.Size(0, 0)
            Me.layoutControlItem1.TextVisible = False
            ' 
            ' layoutControlGroup4
            ' 
            Me.layoutControlGroup4.Items.AddRange(New DevExpress.XtraLayout.BaseLayoutItem() {Me.layoutControlItem2})
            Me.layoutControlGroup4.Location = New System.Drawing.Point(0, 0)
            Me.layoutControlGroup4.Name = "layoutControlGroup4"
            Me.layoutControlGroup4.Size = New System.Drawing.Size(790, 361)
            Me.layoutControlGroup4.Text = "Html Text"
            ' 
            ' layoutControlItem2
            ' 
            Me.layoutControlItem2.Control = Me.targetMemoEdit
            Me.layoutControlItem2.Location = New System.Drawing.Point(0, 0)
            Me.layoutControlItem2.Name = "layoutControlItem2"
            Me.layoutControlItem2.Size = New System.Drawing.Size(790, 361)
            Me.layoutControlItem2.Text = "Get Html Text"
            Me.layoutControlItem2.TextLocation = DevExpress.Utils.Locations.Top
            Me.layoutControlItem2.TextSize = New System.Drawing.Size(0, 0)
            Me.layoutControlItem2.TextVisible = False
            ' 
            ' MainForm
            ' 
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(848, 649)
            Me.Controls.Add(Me.layoutControl)
            Me.Controls.Add(Me.ribbonControl1)
            Me.IconOptions.SvgImage = CType((resources.GetObject("MainForm.IconOptions.SvgImage")), DevExpress.Utils.Svg.SvgImage)
            Me.Name = "MainForm"
            Me.Ribbon = Me.ribbonControl1
            Me.Text = "DXHtmlEditor Sample"
            CType((Me.ribbonControl1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.layoutControl), System.ComponentModel.ISupportInitialize).EndInit()
            Me.layoutControl.ResumeLayout(False)
            CType((Me.panelControl1), System.ComponentModel.ISupportInitialize).EndInit()
            Me.panelControl1.ResumeLayout(False)
            CType((Me.dxHtmlEditorWebView), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.targetMemoEdit.Properties), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.sourceMemoEdit.Properties), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.layoutControlGroup1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.tabbedControlGroup1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.layoutControlGroup2), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.layoutControlItem4), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.layoutControlGroup3), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.layoutControlItem1), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.layoutControlGroup4), System.ComponentModel.ISupportInitialize).EndInit()
            CType((Me.layoutControlItem2), System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()
        End Sub

#End Region
        Private ribbonControl1 As DevExpress.XtraBars.Ribbon.RibbonControl

        Private ribbonPage1 As DevExpress.XtraBars.Ribbon.RibbonPage

        Private ribbonPageGroup1 As DevExpress.XtraBars.Ribbon.RibbonPageGroup

        Private ribbonPage2 As DevExpress.XtraBars.Ribbon.RibbonPage

        Private layoutControl As DevExpress.XtraLayout.LayoutControl

        Private targetMemoEdit As DevExpress.XtraEditors.MemoEdit

        Private sourceMemoEdit As DevExpress.XtraEditors.MemoEdit

        Private layoutControlGroup1 As DevExpress.XtraLayout.LayoutControlGroup

        Private dxHtmlEditorWebView As DXHtmlEditor.DXHtmlEditorWebView

        Private skinDropDownButtonItem1 As DevExpress.XtraBars.SkinDropDownButtonItem

        Private panelControl1 As DevExpress.XtraEditors.PanelControl

        Private tabbedControlGroup1 As DevExpress.XtraLayout.TabbedControlGroup

        Private layoutControlGroup3 As DevExpress.XtraLayout.LayoutControlGroup

        Private layoutControlItem1 As DevExpress.XtraLayout.LayoutControlItem

        Private layoutControlGroup2 As DevExpress.XtraLayout.LayoutControlGroup

        Private layoutControlItem4 As DevExpress.XtraLayout.LayoutControlItem

        Private layoutControlGroup4 As DevExpress.XtraLayout.LayoutControlGroup

        Private layoutControlItem2 As DevExpress.XtraLayout.LayoutControlItem

        Private skinPaletteRibbonGalleryBarItem1 As DevExpress.XtraBars.SkinPaletteRibbonGalleryBarItem

        Private bbiUndo As DevExpress.XtraBars.BarButtonItem

        Private ribbonPageGroup2 As DevExpress.XtraBars.Ribbon.RibbonPageGroup

        Private bbiRedo As DevExpress.XtraBars.BarButtonItem
    End Class
End Namespace
