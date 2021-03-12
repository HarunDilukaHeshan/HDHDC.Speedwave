using System.Threading.Tasks;

namespace HDHDC.Speedwave.Data
{
    public interface ISpeedwaveDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
