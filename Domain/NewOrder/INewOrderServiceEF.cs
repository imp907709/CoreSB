
using System.Threading.Tasks;
using CoreSB.Universal;

namespace CoreSB.Domain.NewOrder
{
    public interface INewOrderServiceEF : IService
    {
        Task ReInitialize();
    }
}
