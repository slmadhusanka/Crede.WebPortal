using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;

[assembly: OwinStartup("ProgramConfiguration", typeof(WebPortal.ProgramConfiguration))]
namespace WebPortal
{
    public class ProgramConfiguration
    {
        static void Main(string[] args)
        {
            //...  
        }
    }
}