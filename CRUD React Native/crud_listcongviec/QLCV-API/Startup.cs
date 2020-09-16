using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using QLCV_API.Provider;
using Owin;
using System;
using System.Web.Http;
using QLCV_API.Models;

[assembly: OwinStartup(typeof(QLCV_API.Startup))]
namespace QLCV_API
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }  
    }
}