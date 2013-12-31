Type.registerNamespace("CleanNav");
CleanNav.CleanNavDesigner = function (element) {
	CleanNav.CleanNavDesigner.initializeBase(this, [element]);
	this._refreshing = false;
	this._Selector = null;
}
CleanNav.CleanNavDesigner.prototype = {
	initialize: function () {
		CleanNav.CleanNavDesigner.callBaseMethod(this, 'initialize');

		dialogBase.setWndHeight(460);
	},
	dispose: function () {
		CleanNav.CleanNavDesigner.callBaseMethod(this, 'dispose');
	},
	refreshUI: function () {
		var Data = this.getData();
		//TextBox Values
		jQuery('#txtSkin').val(Data.CssWrapper);
		jQuery('#layerLimit').val(Data.LayerLimit);
		jQuery('#txtTemplate').val(Data.WrapTemplate);
		jQuery('#txtItem').val(Data.ItemTemplate);

		//CheckBoxes
		jQuery('#ShowAll').attr('checked', Data.ShowAll);
		jQuery('#UniqueClasses').attr('checked', Data.UniqueClasses);
		jQuery('#OverrideGroup').attr('checked', Data.OverrideGroup);
		jQuery('#listCurrent').attr('checked', Data.ShowCurrent);

		//Page type
		jQuery('input[name="SelectionMode"]').each(function () {
			if (jQuery(this).val() == Data.SelectionMode) {
				jQuery(this).attr('checked', true);
			}
		});
		this._refreshing = true;
		var pageid = Data.SelectedPageId; // This gets the current value of PageId from our control (in PageSelector.cs)
		if (pageid) {
			this.get_Selector().set_value(pageid); // Here we set the current page for our PageField control (if its already set)
		}
		this._refreshing = false;
	},
	applyChanges: function () {
		var Data = this.getData();
		//TextBoxes
		Data.CssWrapper = jQuery('#txtSkin').val();
		Data.ItemTemplate = jQuery('#txtItem').val();
		Data.WrapTemplate = jQuery('#txtTemplate').val();
		Data.LayerLimit = jQuery('#layerLimit').val();

		//CheckBoxes
		if (jQuery('#UniqueClasses').is(':checked'))
			Data.UniqueClasses = true;
		else
			Data.UniqueClasses = false;

		if (jQuery('#ShowAll').is(':checked'))
			Data.ShowAll = true;
		else
			Data.ShowAll = false;

		if (jQuery('#OverrideGroup').is(':checked'))
			Data.OverrideGroup = true;
		else
			Data.OverrideGroup = false;

		if (jQuery('#listCurrent').is(':checked'))
			Data.ShowCurrent = true;
		else
			Data.ShowCurrent = false;

		//page type
		Data.SelectionMode = jQuery('input[name="SelectionMode"]:checked').val();
		if (jQuery('input[name="SelectionMode"]:checked').val() != "SelectedPageChildren")
			Data.SelectedPageId = null;
		else
			Data.SelectedPageId = this.get_Selector().get_value();
	},
	getData: function () {
		return this.getEditor().get_control();
	},
	getEditor: function () {
		return this._propertyEditor;
	},
	get_Selector: function () {
		return this._Selector;
	},
	set_Selector: function (value) {
		this._Selector = value;
	}
}

CleanNav.CleanNavDesigner.registerClass('CleanNav.CleanNavDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
if (typeof (Sys) !== 'undefined')
	Sys.Application.notifyScriptLoaded();