using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace WebApplication1.Handlers
{
    /// <summary>
    /// Summary description for AutoCompleteHandler
    /// </summary>
    public class AutoCompleteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            GetAutoCompleteTerms(context);

            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private void GetAutoCompleteTerms(HttpContext context)
        {
            context.Response.Clear();
            context.Response.ContentType = "application/json";

            // Populate List of AutocompleteEntry here accordingly
            string searchString = (context.Request["searchString"] != null) ? context.Request["searchString"] : string.Empty;
            List<AutoCompleteItem> totalDataList = GetAllData();
            List<AutoCompleteItem> resultList = totalDataList.Where(i => i.Text.ToLower().Contains(searchString.ToLower())).ToList();

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            string json = jsSerializer.Serialize(resultList);
            context.Response.Write(json);
            context.Response.End();
        }


        private List<AutoCompleteItem> GetAllData()
        {
            List<AutoCompleteItem> totalDataList = new List<AutoCompleteItem>();
            totalDataList.Add(new Handlers.AutoCompleteItem("Avans", "2"));
            totalDataList.Add(new Handlers.AutoCompleteItem("Aachjes", "7"));
            totalDataList.Add(new Handlers.AutoCompleteItem("Bill", "9"));
            totalDataList.Add(new Handlers.AutoCompleteItem("Events", "25"));
            totalDataList.Add(new Handlers.AutoCompleteItem("Events 2", "27"));

            return totalDataList;
        }


    }

    public class AutoCompleteItem
    {
        public string Text { get; set; }    // label
        public string Value { get; set; }   // value

        public AutoCompleteItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

    }

}
