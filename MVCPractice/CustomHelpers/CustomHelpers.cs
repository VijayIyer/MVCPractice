using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace MVCPractice.CustomHelpers
{
    public static class CustomHelpers
    {
        public static HtmlString Datalist(this HtmlHelper htmlHelper, string name,IEnumerable<SelectListItem> selectList)
        {
          
           //create the datalist tags
            TagBuilder dl = new TagBuilder("datalist");
            dl.Attributes.Add("id",name);
            //create options List
           
            List<string> options = new List<string>();

            foreach (var d in selectList)
            {
                TagBuilder opt = new TagBuilder("option");
                opt.Attributes.Add("value", d.Text);
               
              
                options.Add(opt.ToString(TagRenderMode.StartTag));
              
            }
            string joineditems = string.Join("", options.ToArray());
            joineditems += Environment.NewLine;
            dl.InnerHtml = joineditems;
            return new MvcHtmlString(dl.ToString());
        }
 }
}