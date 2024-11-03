using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infrastructure.Security; 
public class JwtAuthorizeAttribute : AuthorizeAttribute
{

    private readonly string[] _roles;

    public JwtAuthorizeAttribute(params string[] roles)
    {
        _roles = roles;
    }
    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {       
        var token = httpContext.Session["JWToken"] as string;
       
        if (string.IsNullOrEmpty(token))
        {
            return false; 
        }
        
        JwtTokenService jwtService = new JwtTokenService();
        var payload = jwtService.GetPayload(token);        
        if (_roles.Length > 0 && !_roles.Contains(payload.role))
        {
            return false; 
        }

        return true; 
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {       
        filterContext.Result = new RedirectResult("/Account/Login");
    }
}
