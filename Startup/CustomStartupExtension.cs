using Algorithms;
using InfrastructureCheckers;
using Microsoft.Extensions.Hosting;

namespace CoreSB.Startup
{
    public static class CustomStartupExtension
    {
        public static void CustomBeforeStartProcess(this IHost a)
        {
            System.Diagnostics.Trace.WriteLine($"Started");
            LINQcheck.GO();
            AlgCheck.GO();
            Patterns.StrategyPattern.StrategyPatternCheck.GO();
            System.Diagnostics.Trace.WriteLine($"Finished");
        }
    }
}
