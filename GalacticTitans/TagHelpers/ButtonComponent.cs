using Microsoft.AspNetCore.Razor.TagHelpers;
using static System.Net.Mime.MediaTypeNames;

namespace GalacticTitans.TagHelpers
{
    //Custom html komponendi klass, kus asuvad path-tüüpi nupu visuaalsed komponendid

    [HtmlTargetElement("cyberbutton")]
    public class ButtonTagHelper : TagHelper
    {
        public string Message { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "<button>";
            //string content = "< svg width = \"300\" height = \"130\" >" +
            //    "< rect width = \"200\" height = \"50\" fill = \"white\" />" +
            //    "< polygon points = \"0,0 0,20 20,0\" fill = \"red\" />" +
            //    "< polygon points = \"200,50 200,30 180,50\" fill = \"red\" />" +
            //    "< text > " +Message+ "</ text >  Sorry, your browser does not support inline SVG.</ svg >";
            string content = @$"< svg width = '300' height = '130' >" +
                "< rect width = '200' height = '50' fill = 'white' />" +
                "< polygon points = '0,0 0,20 20,0' fill = 'red' />" +
                "< polygon points = '200,50 200,30 180,50' fill = 'red' />" +
                "< text > " + Message + "</ text >  Sorry, your browser does not support inline SVG.</ svg >";
            output.Content.SetHtmlContent(content);
        }

        //public override void Process(TagHelperContext context, TagHelperOutput output)
        //{
        //    output.TagName = "div"; //button1 is aliasing svg
        //    //output.Attributes.SetAttribute("width", "300");
        //    //output.Attributes.SetAttribute("height", "130");
        //    //output.Attributes.SetAttribute("xmlns", "http://www.w3.org/2000/svg");
        //    output.Content.SetHtmlContent
        //        ($"    <svg width=\"300\" height=\"130\">\r\n        <rect width=\"200\" height=\"50\" fill=\"white\" />\r\n        <polygon points=\"0,0 0,20 20,0\" fill=\"red\" />\r\n        <polygon points=\"200,50 200,30 180,50\" fill=\"red\" />\r\n        <text fill=\"#dcef44\" x=\"150\" y=\"65\" text-anchor=\"middle\" alignment-baseline=\"middle\">pastesvg</text>\r\n        Sorry, your browser does not support inline SVG.\r\n    </svg>");
        //}
        //public override void Process(TagHelperContext context, TagHelperOutput output)
        //{
        //    Console.WriteLine("TagHelper triggered");
        //    // Set the tag to be <svg>
        //    output.TagName = "svg";

        //    // Add SVG attribui b v tes
        //    output.Attributes.SetAttribute("width", "100");
        //    output.Attributes.SetAttribute("height", "100");

        //    // Set the inner content of the SVG
        //    output.Content.SetHtmlContent(@"<circle cx='50' cy='50' r='40' stroke='green' stroke-width='4' fill='yellow' />beeboob");
        //}
    }
    [HtmlTargetElement("testbuttondiv")]
    public class TestButtonDiv : TagHelper
    {
        public string Message { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            
            output.Content.SetHtmlContent(Message);
        }
    }
    [HtmlTargetElement("custom-svg")]
    public class SvgButton : TagHelper
    {
        public string Message { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            output.Content.SetHtmlContent(Message);
        }
    }
}
