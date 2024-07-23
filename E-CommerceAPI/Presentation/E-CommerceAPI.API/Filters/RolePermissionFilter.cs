using E_CommerceAPI.Application.Abstractions.Services;
using E_CommerceAPI.Application.CustomAttributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace E_CommerceAPI.API.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        readonly IUserService _userService;

        public RolePermissionFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var name =   context.HttpContext.User.Identity?.Name;  // burdaki name Usernamedir mende bunu program cs de yazmisam NameClaimType = ClaimTypes.Name hemdeki token olsdurduqda Username essagin elemisem ClaimTypes.Name  

            if (!string.IsNullOrEmpty(name) && name!="aqsin") //name null deyilse
            {

            var descriptor = context.ActionDescriptor as ControllerActionDescriptor; //controllerin adini alir request geldikde bunu as ile cast edirikki controllerin adini ala bilsin

            var attribute = descriptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefinitionAttribute)) as AuthorizeDefinitionAttribute; //burda controllerin methodunun attribute alir custom attribute alir ve onu cast edir AuthorizeDefinitionAttribute tipine 

            var httpAttribute = descriptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute; //burda controllerin methodunun attribute alir custom attribute alir ve onu cast edir HttpMethodAttribute bunun vasitesi ile men htppmethodumun GET POST PUT DELETE oldugunu  ve hansi httpAttribuate olduqunu yoxlayiram 


                var code = $"{(httpAttribute != null ? httpAttribute.HttpMethods.First() : HttpMethods.Get)}.{attribute.ActionType}.{attribute.Definition.Replace(" ","")}"; // bu Code nedir bizim sqlde Code adinda bir sutun var bizde reflextion vasitesi ile bu sutuna butun melumatlarini birlesdirib qoymusduq indi onu yoxlayiriq hansidi   bu methodun icindeki bu setir GetAuthorizeDefinitionEndpoints    _action.Code = $"{_action.HttpType}.{_action.ActionType}.{_action.Definition.Replace(" ", "")}";  example bele gelir codenin icindeki data POST.Writing.CreateProduct

                var hasRole = await _userService.HasRolePermissionToEndpointAsync(name,code);

                if (!hasRole)
                    context.Result = new UnauthorizedResult();   // eger rola icazesi yoxdursa  geriye 401 status kodu qaytarir

                else
                 await next(); // bir sonraki middilawreyi tetikleyeceyiz
            }
            else
            await next();

        }




    }
}
