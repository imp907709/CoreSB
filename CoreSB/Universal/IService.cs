
namespace CoreSB.Universal
{


    public interface IService
    {
        IRepository GetRepositoryRead();
        IRepository GetRepositoryWrite();

        string actualStatus { get; }

        IServiceStatus _status { get; }

    }

    public interface IServiceStatus
    {
        public string Message { get; set; }
    }


}
