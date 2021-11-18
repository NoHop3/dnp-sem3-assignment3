// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace LoginExample.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\bird\RiderProjects\Razor_intro_ex1\LoginExample\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\bird\RiderProjects\Razor_intro_ex1\LoginExample\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\bird\RiderProjects\Razor_intro_ex1\LoginExample\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\bird\RiderProjects\Razor_intro_ex1\LoginExample\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\bird\RiderProjects\Razor_intro_ex1\LoginExample\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\bird\RiderProjects\Razor_intro_ex1\LoginExample\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\bird\RiderProjects\Razor_intro_ex1\LoginExample\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\bird\RiderProjects\Razor_intro_ex1\LoginExample\_Imports.razor"
using LoginExample;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\bird\RiderProjects\Razor_intro_ex1\LoginExample\_Imports.razor"
using LoginExample.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\bird\RiderProjects\Razor_intro_ex1\LoginExample\Pages\Register.razor"
using LoginExample.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\bird\RiderProjects\Razor_intro_ex1\LoginExample\Pages\Register.razor"
using LoginExample.Data;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/Register")]
    public partial class Register : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#nullable restore
#line 51 "C:\Users\bird\RiderProjects\Razor_intro_ex1\LoginExample\Pages\Register.razor"
       
    private User newUser = new User();
    private string? birthDateString;
    private string? role = "Guest";

    private async void AddNewUser()
    {
        try
        {
            newUser.BirthYear = int.Parse(birthDateString.Substring(0, 4));
            await Data.AddNewUserAsync(newUser);
            NavigationManager.NavigateTo("/");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }


    private void ChosenBirthDate(ChangeEventArgs args)
    {
        birthDateString = args.Value.ToString().TrimStart('0');
    }

    private void ChosenRole(ChangeEventArgs args)
    {
        newUser.Role = args.Value.ToString();
    }

#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavigationManager { get; set; }
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IUserService Data { get; set; }
    }
}
#pragma warning restore 1591
