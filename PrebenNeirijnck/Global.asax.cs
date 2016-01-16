using PrebenNeirijnck.App_Start;
using System;
using Umbraco.Web;

namespace PrebenNeirijnck
{
    public class Global : UmbracoApplication
    {
        protected override void OnApplicationStarted(object sender, EventArgs e)
        {
            UnityConfig.RegisterComponents();
        }
    }
}