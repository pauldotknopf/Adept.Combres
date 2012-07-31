[assembly: WebActivator.PreApplicationStartMethod(typeof(Adept.Combres.Sample.App_Start.Combres), "PreStart")]
namespace Adept.Combres.Sample.App_Start {
	using System.Web.Routing;
	using global::Combres;
	
    public static class Combres {
        public static void PreStart() {
            RouteTable.Routes.AddCombresRoute("Combres");
        }
    }
}