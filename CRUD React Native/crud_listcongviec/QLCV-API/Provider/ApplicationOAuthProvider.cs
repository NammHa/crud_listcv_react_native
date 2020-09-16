using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using QLCV_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace QLCV_API.Provider
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;
        private QLCVEntities databaseManager = new QLCVEntities();


        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials
        (OAuthGrantResourceOwnerCredentialsContext context)
        {
            try
            {
                string usernameVal = context.UserName;
                string passwordVal = context.Password;

                if (usernameVal == "" || passwordVal == "")
                {
                    context.SetError("invalid_grant", "Tên đăng nhập và mật khẩu không được để trống");
                    return;

                }
                var user = this.databaseManager.TBL_NGUOI_DUNG.Where(x => x.UserName.Equals(usernameVal) && x.Password.Equals(passwordVal)).FirstOrDefault();

                if(user == null)
                {
                    context.SetError("invalid_grant", "Tài khoản không xác thực");
                    return;
                }


                ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                ClaimsIdentity cookiesIdentity = new ClaimsIdentity(context.Options.AuthenticationType);

                AuthenticationProperties properties = CreateProperties(user.Id.ToString(),user.UserName,user.Password,user.FullName);
                AuthenticationTicket ticket =
                new AuthenticationTicket(oAuthIdentity, properties);
                context.Validated(ticket);
                context.Request.Context.Authentication.SignIn(cookiesIdentity);
            }
            catch (Exception ex)
            {

                context.SetError("invalid_grant", ex.Message + ex.InnerException);
                return;
            }
        }
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string,
            string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication
        (OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri
        (OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string Id,string userName,string password,string Fullname)
        {
            IDictionary<string, string>
            data = new Dictionary<string, string>
            {
                { "UserId", Id },
                { "Username", userName },
                { "Userpass", password },
                { "UserFullname", Fullname }
            };
            return new AuthenticationProperties(data);
        }
    }
}