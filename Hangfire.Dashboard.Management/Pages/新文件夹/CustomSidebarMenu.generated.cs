﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hangfire.Dashboard.Management.Pages
{
    
    #line 2 "..\..\Pages\CustomSidebarMenu.cshtml"
    using System;
    
    #line default
    #line hidden
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    #line 4 "..\..\Pages\CustomSidebarMenu.cshtml"
    using Hangfire.Annotations;
    
    #line default
    #line hidden
    
    #line 3 "..\..\Pages\CustomSidebarMenu.cshtml"
    using Hangfire.Dashboard;
    
    #line default
    #line hidden
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    internal partial class CustomSidebarMenu : RazorPage
    {
#line hidden

        #line 6 "..\..\Pages\CustomSidebarMenu.cshtml"

        public CustomSidebarMenu([NotNull] IEnumerable<Func<RazorPage, MenuItem>> items)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            Items = items;
        }
        public IEnumerable<Func<RazorPage, MenuItem>> Items { get; }

        #line default
        #line hidden

        public override void Execute()
        {


WriteLiteral("\r\n");






WriteLiteral("\r\n");


            
            #line 14 "..\..\Pages\CustomSidebarMenu.cshtml"
 if (!Items.Any()) return;

            
            #line default
            #line hidden
WriteLiteral("<div id=\"stats\" class=\"list-group\">\r\n");


            
            #line 16 "..\..\Pages\CustomSidebarMenu.cshtml"
     foreach (var item in Items)
    {
        var itemValue = item(this);

            
            #line default
            #line hidden
WriteLiteral("        <a href=\"");


            
            #line 19 "..\..\Pages\CustomSidebarMenu.cshtml"
            Write(Url.To(itemValue.Url));

            
            #line default
            #line hidden
WriteLiteral("\" class=\"list-group-item");


            
            #line 19 "..\..\Pages\CustomSidebarMenu.cshtml"
                                                           Write(itemValue.Active ? " active" : null);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n            ");


            
            #line 20 "..\..\Pages\CustomSidebarMenu.cshtml"
       Write(itemValue.Text);

            
            #line default
            #line hidden
WriteLiteral("\r\n            <span class=\"pull-right\">\r\n");


            
            #line 22 "..\..\Pages\CustomSidebarMenu.cshtml"
                 foreach (var metric in itemValue.GetAllMetrics())
                {
                    
            
            #line default
            #line hidden
            
            #line 24 "..\..\Pages\CustomSidebarMenu.cshtml"
                Write(Html.InlineMetric(metric));

            
            #line default
            #line hidden
            
            #line 24 "..\..\Pages\CustomSidebarMenu.cshtml"
                                                
                }

            
            #line default
            #line hidden
WriteLiteral("            </span>\r\n        </a>\r\n");


            
            #line 28 "..\..\Pages\CustomSidebarMenu.cshtml"
    }

            
            #line default
            #line hidden
WriteLiteral("</div>");


        }
    }
}
#pragma warning restore 1591
