using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using System.Web.UI.WebControls;
using System.Web.UI;
using Telerik.Sitefinity.Web;
using System.Web;
using Telerik.Sitefinity;
using Telerik.Sitefinity.Pages.Model;
using System.IO;
using Telerik.Sitefinity.Modules.Pages;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;

namespace CleanNav
{
	[RequireScriptManager]
	[ControlDesigner(typeof(CleanNavDesigner))]
	public class CleanNav : SimpleView
	{
		protected override void InitializeControls(GenericContainer container)
		{

			StringWriter sWriter = new StringWriter();
			HtmlTextWriter writer = new HtmlTextWriter(sWriter);

			var pageManager = PageManager.GetManager();
			var currentNode = SiteMapBase.GetCurrentProvider().CurrentNode as PageSiteNode;
			var PageId = Guid.Empty;  // Default to top level memu

			if (SelectionMode == "SelectedPageChildren")
		{	
				if (!SelectedPageId.ToString().IsNullOrEmpty())
				{
					PageId = SelectedPageId;
				}
			}
			else if (SelectionMode == "CurrentPageSiblings")
			{
				if (currentNode.ParentNode != null)
				{
					var parentNode = currentNode.ParentNode as PageSiteNode;
					PageId = parentNode.Id;
				}
			}
			else if (SelectionMode == "CurrentPageChildren")
			{
				PageId = currentNode.Id;
			}

			SiteMapProvider provider = SiteMapBase.GetCurrentProvider();
			if (provider != null)
			{
				SiteMapNode siteMapNode;

				if (PageId == Guid.Empty)
				{
					siteMapNode = SiteMapBase.GetCurrentProvider().RootNode;
				}
				else
				{
					siteMapNode = provider.FindSiteMapNodeFromKey(PageId.ToString());
					
				}
				//Current page at top of list code
				if (ShowCurrent && (SelectionMode == "CurrentPageChildren" || SelectionMode == "SelectedPageChildren" || SelectionMode == "CurrentPageSiblings"))
				{
					RenderItem(writer, siteMapNode, 1);
				}
				else
				{
					foreach (SiteMapNode m in siteMapNode.ChildNodes)
					{
						RenderItem(writer, m, 1);
					}
				}
			}

			MenuContent.Text = WrapTemplate.Replace("[[nav]]", sWriter.ToString());

			//RUN WRITERS
		}

		protected void RenderItem(HtmlTextWriter writer, SiteMapNode node, int count)
		{
			var currentNode = SiteMapBase.GetCurrentProvider().CurrentNode as PageSiteNode;
			var pageNode = (PageSiteNode)node;

			if (pageNode.ShowInNavigation == true || ShowAll == true)
			{

				HyperLink hyp = null;

				/* Create and Render Link */
				hyp = new HyperLink();
				if ((pageNode.IsGroupPage) && pageNode.HasChildNodes && (OverrideGroup == true))
				{
					hyp.NavigateUrl = ResolveUrl(node.ChildNodes[0].Url);
				}
				else
				{
					hyp.NavigateUrl = ResolveUrl(node.Url);
				}
				hyp.Text = node.Title;

				string classes = "";
				if (!this.IsDesignMode() && node != null)
				{

					if (currentNode != null)
					{
						if (currentNode.PageId == pageNode.PageId)
						{
							classes += " active";
						}
					}

					if (node.ChildNodes.Count > 0)
					{
						classes += " parent";
					}

					if (node.PreviousSibling == null)
					{
						classes += " first";
					}

					if (node.NextSibling == null)
					{
						classes += " last";
					}

					if (UniqueClasses)
					{
						classes += " " + pageNode.UrlName.ToLower();
					}

					if (classes.Length > 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Class, classes.Trim());
					}
				}



				writer.RenderBeginTag(HtmlTextWriterTag.Li);

				hyp.Text = ItemTemplate.Replace("[[link]]", hyp.Text);
				hyp.RenderControl(writer);


				if (node.ChildNodes.Count > 0 && (count < LayerLimit || LayerLimit < 1))
				{

					writer.RenderBeginTag(HtmlTextWriterTag.Ul);

					foreach (SiteMapNode m in node.ChildNodes)
					{
						this.RenderItem(writer, m, count++);
					}

					writer.RenderEndTag();
				}
				writer.RenderEndTag();
			}
		}

		protected override string LayoutTemplateName
		{
			get { return "CleanNav.Resources.Views.CleanNav.ascx"; }
		}

		public Literal MenuContent
		{
			get { return Container.GetControl<Literal>("Menu", true); }
		}
		public override void RenderBeginTag(HtmlTextWriter writer)
		{

		}
		public override void RenderEndTag(HtmlTextWriter writer)
		{

		}

		#region Variables
		public Guid SelectedPageId
		{
			get;
			set;
		}

		public Boolean UniqueClasses
		{
			get;
			set;
		}

		public Boolean OverrideGroup
		{
			get;
			set;
		}

		public Boolean ShowAll
		{
			get;
			set;
		}

		public string SelectionMode
		{
			get;
			set;
		}
		public bool ShowCurrent
		{
			get;
			set;
		}
		protected string _wrapTemplate = "<ul>[[nav]]</ul>";
		public string WrapTemplate
		{
			get { return this._wrapTemplate; }
			set { this._wrapTemplate = value; }
		}

		protected string _itemTemplate = "[[link]]";
		public string ItemTemplate
		{
			get { return this._itemTemplate; }
			set { this._itemTemplate = value; }
		}

		protected int _layerLimit = 0;
		public int LayerLimit
		{
			get { return this._layerLimit; }
			set { this._layerLimit = value; }
		}
		#endregion
	}
}
