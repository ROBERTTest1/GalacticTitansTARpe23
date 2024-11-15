using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GalacticTitans.TagHelpers
{
    [HtmlTargetElement("panel")]
    public class PanelTagHelper : TagHelper
    {
        public string Title { get; set; } //title of the panel

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "panel");

            var content = await output.GetChildContentAsync();

            var panelHtml = "test";

            output.Content.SetHtmlContent(panelHtml);
        }
    }
}
