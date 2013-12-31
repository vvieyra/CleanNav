using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CleanNav
{
	class CleanNavDesigner : ControlDesignerBase
	{
		protected override void InitializeControls(Telerik.Sitefinity.Web.UI.GenericContainer container)
		{
			base.DesignerMode = ControlDesignerModes.Simple;
			PageSelector.WebServiceUrl = "~/Sitefinity/Services/Pages/PagesService.svc/";
			PageSelector.RootNodeID = Telerik.Sitefinity.Abstractions.SiteInitializer.FrontendRootNodeId;
			PageSelector.DisplayMode = FieldDisplayMode.Write;
		}

		protected override string LayoutTemplateName
		{
			get
			{
				return "CleanNav.Resources.Views.CleanNavDesigner.ascx";
			}
		}

		public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			var scriptDescriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
			var desc = (ScriptControlDescriptor)scriptDescriptors.Last();
			desc.AddComponentProperty("Selector", this.PageSelector.ClientID);
			return new[] { desc };
		}
		public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
		{
			var res = new List<System.Web.UI.ScriptReference>(base.GetScriptReferences());
			var assemblyName = this.GetType().Assembly.GetName().ToString();
			res.Add(new ScriptReference("CleanNav.Resources.JavaScript.CleanNav.js", assemblyName));
			return res.ToArray();
		}
		protected PageField PageSelector
		{
			get { return Container.GetControl<PageField>("Selector", true); }
			// This allows us to set/get properties of our PageField control
		}
	}
}
