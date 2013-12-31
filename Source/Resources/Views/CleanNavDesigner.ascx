<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Publishing.Web.UI.Designers" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI"
	TagPrefix="sitefinity" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers" %>
<sitefinity:FormManager id="formManager" runat="server" />
<div class="sfColWrapper sfModeSelector sfClearFix">
	<h2 class="sfStep1">
		Customize Your Navigation</h2>
	<div class="sfStep2Options">
		<div id="groupSettingPageSelect" style="display: block;">
			<h3 id="SelectionHeading">
				Links to which pages to display?</h3>
			<p id="SelectionNotes" class="sfSecondStepExample">
			</p>
			<ul class="sfRadioList">
				<li id="liTopLevelPages">
					<input type="radio" id="rbTopLevelPages" name="SelectionMode" value="TopLevelPages" checked="checked" />
					<label for="rbTopLevelPages">
						Top level pages</label>
				</li>
				<li id="liSelectedPageChildren">
					<input type="radio" id="rbSelectedPageChildren" name="SelectionMode" value="SelectedPageChildren" />
					<label for="rbSelectedPageChildren">
						All pages under particular page</label>
					<sfFields:PageField ID="Selector" CssClass="pageSelector" runat="server" />
				</li>
				<li id="liCurrentPageChildren">
					<input type="radio" id="rbCurrentPageChildren" name="SelectionMode" value="CurrentPageChildren" />
					<label for="rbCurrentPageChildren">
						All pages under currently opened page</label>
				</li>
				<li id="liCurrentPageSiblings">
					<input type="radio" id="rbCurrentPageSiblings" name="SelectionMode" value="CurrentPageSiblings" />
					<label for="rbCurrentPageSiblings">
						All sibling pages of currently opened page</label>
				</li>
				
			</ul>
		</div>
		<div id="groupDesignSettings">
			<h3 class="sfMoreDetails">
				Design settings</h3>
			<ul class="sfTargetList">
                <li>
                    <label for="txtItem" class="sfTxtLbl">Link Template <em class="sfNote">(inside &lt;A&gt;)</em></label>
                    <input type="text" id="txtItem" class="sfTxt" />
                </li>
                <li>
                    <label for="txtTemplate" class="sfTxtLbl">Wrapper Template <em class="sfNote">(wraps around navigation)</em></label>
                    <input type="text" id="txtTemplate" class="sfTxt" />
                </li>
			</ul>
		</div>
		<div>
			<h3>Misc. Data</h3>
			<ul class="sfTargetList">
				<li>
					<label class="sfTxtLabel">
						Layer Limit<em class="sfNote"> - Limits the number of navigation levels to show</em>
					</label>
					<input type="text" id="layerLimit" class="sfTxt" />
					<p class="sfExample">
						Use a negative value or 0 to set no limit.
					</p>
				</li>
				<li>
                    <input type="checkbox" id="UniqueClasses" />
					<label class="sfTxtLabel" for="UniqueClasses">
						Generate unique classes for each link
					</label>
				</li>
				<li>
                    <input type="checkbox" id="ShowAll" />
					<label class="sfTxtLabel" for="ShowAll">
						Show all regardless of "Show in Navigation"
					</label>
				</li>
                <li>
                    <input type="checkbox" id="OverrideGroup" />
                    <label class="sfTxtLabel" for="OverrideGroup">
                        Override default redirect for Group pages.
                    </label>
                    <p class="sfExample">
						Group pages will use URL from first child for link. No redirection.
					</p>
                </li>
				<li class="hidden">
					<input type="checkbox" id="listCurrent" />
					<label for="listCurrent">Show parent node</label>
					<p class="sfExample">Adds a link to the parent page at the top </p>
				</li>
			</ul>
		</div>
	</div>
</div>
<script type="text/javascript">
	$(window).load(function () {
		if ($('input[name="SelectionMode"]:checked').val() != "SelectedPageChildren") {
			$('.pageSelector').hide();
		}

		$('input[name="SelectionMode"]').click(function () {
			if ($('input[name="SelectionMode"]:checked').val() != "SelectedPageChildren") {
				$('.pageSelector').hide();
			}
			else {
				$('.pageSelector').show();
			}
		});

		if ($('#rbCurrentPageChildren').is(':checked') || $('#rbSelectedPageChildren').is(':checked') || $('#rbCurrentPageSiblings').is(':checked')) {
			$('.hidden').show();
		}
		else {
			$('.hidden').hide();
		}
		$('input[name="SelectionMode"]').click(function () {
			if ($('#rbCurrentPageChildren').is(':checked') || $('#rbSelectedPageChildren').is(':checked') || $('#rbCurrentPageSiblings').is(':checked')) {
				$('.hidden').show();
			}
			else {
				$('.hidden').hide();
			}
		});
	});
</script>
