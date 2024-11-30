using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace GalacticTitans.TagHelpers
{
    [HtmlTargetElement("simple")]
    public class SimpleTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetHtmlContent("Hello from Simple Tag Helper!");
        }
    }

    [HtmlTargetElement("albas", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class OmniTagHelper : TagHelper
    {
        // Property to define the type of SVG to be injected (e.g., icon name or ID)
        public string SvgId { get; set; }
        public string ContentType { get; set; }

        // Predefined SVGs - you can add more as needed
        private static readonly string HomeSvg = @"
            <svg xmlns='http://www.w3.org/2000/svg' width='100' height='100'>
                <rect width='100' height='100' fill='lightblue'/>
                <polygon points='50,15 90,40 90,70 50,95 10,70 10,40' fill='white'/>
            </svg>";

        private static readonly string SearchSvg = @"
            <svg xmlns='http://www.w3.org/2000/svg' width='100' height='100'>
                <circle cx='50' cy='50' r='40' stroke='black' stroke-width='3' fill='white'/>
                <line x1='70' y1='70' x2='90' y2='90' stroke='black' stroke-width='3'/>
            </svg>";

        private static readonly string SettingsSvg = @"
            <svg xmlns='http://www.w3.org/2000/svg' width='100' height='100'>
                <circle cx='50' cy='50' r='45' stroke='black' stroke-width='3' fill='white'/>
                <rect x='45' y='5' width='10' height='10' fill='black'/>
                <rect x='45' y='85' width='10' height='10' fill='black'/>
                <rect x='5' y='45' width='10' height='10' fill='black'/>
                <rect x='85' y='45' width='10' height='10' fill='black'/>
            </svg>";

        // Process method to handle different content types and SVG injection
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            Console.WriteLine("AliasTagHelper triggered!");
            var contentBuilder = new StringBuilder();

            if (string.IsNullOrEmpty(ContentType))
            {
                // Default behavior if no ContentType is specified
                contentBuilder.Append("Invalid ContentType specified.");
            }
            else
            {
                // Handle SVG content injection
                if (ContentType.ToLower() == "svg")
                {
                    string svgContent = GetSvgContent(SvgId);
                    contentBuilder.Append(svgContent ?? "<!-- SVG not found -->");
                }
                else
                {
                    // If not SVG, handle as raw HTML or text (you can extend as needed)
                    contentBuilder.Append($"ContentType '{ContentType}' is not supported in this helper.");
                }
            }

            // Set the final content to render
            output.Content.SetHtmlContent(contentBuilder.ToString());
        }

        // Method to return the SVG content based on SvgId
        private string GetSvgContent(string svgId)
        {
            switch (svgId?.ToLower())
            {
                case "home":
                    return HomeSvg;
                case "search":
                    return SearchSvg;
                case "settings":
                    return SettingsSvg;
                default:
                    return null; // Return null if SVG ID is not found
            }
        }
    }
}
