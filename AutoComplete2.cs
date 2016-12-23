using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.CustomControls
{

    /// <summary>
    /// AutoComplete control
    /// To make this control work, make sure these files are included in the web-project: jquery-ui.css, jquery-xxx.min.js, jquery-ui.min.js (, autocompletecontrolv2.js)
    /// </summary>
    public class AutoComplete2 : CompositeControl, INamingContainer
    {
        // Private vars
        private TextBox _TextBox;
        private HiddenField _HiddenField;


        public string Text
        {
            get
            {
                EnsureChildControls();
                return _TextBox.Text;
            }
            set
            {
                EnsureChildControls();
                _TextBox.Text = value;
            }
        }

        public string Value
        {
            get
            {
                EnsureChildControls();
                return _HiddenField.Value;
            }
            set
            {
                EnsureChildControls();
                _HiddenField.Value = value;
            }
        }

        /// <summary>
        /// E.g. "~/Scripts/autocompletecontrolv2.js"
        /// </summary>
        public string JavascriptFile
        {
            get
            {
                string result = string.Empty;

                if (ViewState["AutoCompleteV2_JavascriptFile"] != null)
                {
                    result = (string)ViewState["AutoCompleteV2_JavascriptFile"];
                }

                return result;
            }
            set
            {
                ViewState["AutoCompleteV2_JavascriptFile"] = value;
            }
        }

        /// <summary>
        /// The (relative) url to the (json REST) service to get the list item result data
        /// E.g. "~/Handlers/autocompletehandler.ashx?searchString="
        /// </summary>
        public string DataUrl
        {
            get
            {
                string result = string.Empty;
                if (ViewState[string.Format("AutoCompleteV2_DataUrl_{0}", this.UniqueID)] != null)
                {
                    result = (string)ViewState[string.Format("AutoCompleteV2_DataUrl_{0}", this.UniqueID)];
                }

                return result;
            }
            set
            {
                ViewState[string.Format("AutoCompleteV2_DataUrl_{0}", this.UniqueID)] = value;
            }
        }

        public string SearchParameterName
        {
            get
            {
                string result = "searchString"; //string.Empty;
                if (ViewState[string.Format("AutoCompleteV2_SearchParameterName_{0}", this.UniqueID)] != null)
                {
                    result = (string)ViewState[string.Format("AutoCompleteV2_SearchParameterName_{0}", this.UniqueID)];
                }

                return result;
            }
            set
            {
                ViewState[string.Format("AutoCompleteV2_SearchParameterName_{0}", this.UniqueID)] = value;
            }
        }

        public string ContextKey
        {
            get
            {
                string result = string.Empty;
                if (ViewState[string.Format("AutoCompleteV2_ContextKey_{0}", this.UniqueID)] != null)
                {
                    result = (string)ViewState[string.Format("AutoCompleteV2_ContextKey_{0}", this.UniqueID)];
                }

                return result;
            }
            set
            {
                ViewState[string.Format("AutoCompleteV2_ContextKey_{0}", this.UniqueID)] = value;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            //CreateChildControls();
        }

        protected override void OnLoad(EventArgs e)
        {
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (string.IsNullOrEmpty(DataUrl))
                throw new Exception(string.Format("No data url specified for autocomplete control '{0}'.", this.ID));

            // Render javascript include
            if (!Page.ClientScript.IsStartupScriptRegistered("AutoCompleteV2_JavascriptFileRef"))
            {
                string jsFile = "<script src=\"" + ResolveUrl(JavascriptFile) + "\" type=\"text/javascript\"></script>";
                Page.ClientScript.RegisterStartupScript(typeof(string), "AutoCompleteV2_JavascriptFileRef", jsFile, false);
            }

            // Init the control (execute for each autocomplete control on the page)
            if (!Page.ClientScript.IsStartupScriptRegistered(string.Format("AutoCompleteV2_Init_{0}", this.UniqueID)))
            {
                string url = ResolveUrl(string.Format("{0}?ContextKey={1}&{2}=", DataUrl, ContextKey, SearchParameterName));
                string jsInitBlock = string.Format("initAutoCompleteControl('{0}', '{1}', '{2}');", _TextBox.ClientID, _HiddenField.ClientID, url);
                Page.ClientScript.RegisterStartupScript(typeof(string), string.Format("AutoCompleteV2_Init_{0}", this.UniqueID), jsInitBlock, true);
            }

        }

        protected override void Render(HtmlTextWriter writer)
        {
            // Container div
            writer.AddAttribute(HtmlTextWriterAttribute.Class, "fm");
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Panel");
            writer.RenderBeginTag(HtmlTextWriterTag.Div);

            // Label left from control
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_Label");
            writer.RenderBeginTag(HtmlTextWriterTag.Label);
            writer.Write("Label...");
            writer.RenderEndTag();

            // Containing Span
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_ContainerSpan");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:inline-block;");


            _HiddenField.RenderControl(writer);
            _HiddenField.ID = this.ClientID + "_hfAutoComplete";

            _TextBox.CssClass = "TextboxWatermark";
            _HiddenField.ID = this.ClientID + "_tbAutoComplete";
            _TextBox.RenderControl(writer);

            // CustomVal Span
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_customVal");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:none;visibility:hidden;");
            writer.RenderEndTag();

            // Placeholder Span
            writer.RenderBeginTag(HtmlTextWriterTag.Span);
            writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_placeholder");
            writer.AddAttribute(HtmlTextWriterAttribute.Style, "display:none;");
            writer.RenderEndTag();

            //... validator


            // End Containing Span
            writer.RenderEndTag();

            // End Containing Div / Panel
            writer.RenderEndTag();
            
        }


        protected override void CreateChildControls()
        {
            _TextBox = new TextBox();
            _TextBox.ID = "TextBox";
            Controls.Add(_TextBox);

            _HiddenField = new HiddenField();
            _HiddenField.ID = "HiddenField";
            Controls.Add(_HiddenField);

            base.CreateChildControls();
        }


    }

}
