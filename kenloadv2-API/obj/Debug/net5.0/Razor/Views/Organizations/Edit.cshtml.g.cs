#pragma checksum "C:\Users\Masterspace\Documents\projects\kenloadv2\kenloadv2-API\Views\Organizations\Edit.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7454cddbe27667c8d29c121a740245d6bb1b9016"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Organizations_Edit), @"mvc.1.0.view", @"/Views/Organizations/Edit.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7454cddbe27667c8d29c121a740245d6bb1b9016", @"/Views/Organizations/Edit.cshtml")]
    public class Views_Organizations_Edit : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<KenloadV2API.Models.Organization>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Masterspace\Documents\projects\kenloadv2\kenloadv2-API\Views\Organizations\Edit.cshtml"
  
    ViewData["Title"] = "Edit";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Edit</h1>

<h4>Organization</h4>
<hr />
<div class=""row"">
    <div class=""col-md-4"">
        <form asp-action=""Edit"">
            <div asp-validation-summary=""ModelOnly"" class=""text-danger""></div>
            <input type=""hidden"" asp-for=""id"" />
            <div class=""form-group"">
                <label asp-for=""code"" class=""control-label""></label>
                <input asp-for=""code"" class=""form-control"" />
                <span asp-validation-for=""code"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""name"" class=""control-label""></label>
                <input asp-for=""name"" class=""form-control"" />
                <span asp-validation-for=""name"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""deletedstatus"" class=""control-label""></label>
                <input asp-for=""deletedstatus"" class=""form-control"" />
                <span asp-validation-f");
            WriteLiteral(@"or=""deletedstatus"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""regdate"" class=""control-label""></label>
                <input asp-for=""regdate"" class=""form-control"" />
                <span asp-validation-for=""regdate"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <input type=""submit"" value=""Save"" class=""btn btn-primary"" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action=""Index"">Back to List</a>
</div>

");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 48 "C:\Users\Masterspace\Documents\projects\kenloadv2\kenloadv2-API\Views\Organizations\Edit.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<KenloadV2API.Models.Organization> Html { get; private set; }
    }
}
#pragma warning restore 1591
