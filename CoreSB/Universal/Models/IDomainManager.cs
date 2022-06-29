
namespace CoreSB.Universal
{
    using System.Runtime.CompilerServices;
    public interface IDomainManager
    {

        void CleanUp([CallerMemberName] string CallerMemberName = "");
        string GetDbName([CallerMemberName] string CallerMemberName = "");
        void ReInitialize([CallerMemberName] string CallerMemberName = "");
    }
}
