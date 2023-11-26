using System.Threading.Tasks;

namespace Loggin
{
    public interface ILog
    {
        public string PathForLogs { get; set; }
        Task AddTrace(string logMessage);
        Task AddDebug(string logMessage);
        Task AddInfo(string logMessage);
        Task AddWarning(string logMessage);
        Task AddError(string logMessage);
        Task AddNone(string logMessage);
    }
}
