using NpgsqlTypes;
using Serilog.Events;
using Serilog.Sinks.PostgreSQL;

namespace E_CommerceAPI.API.Configurations.ColumnWriters
{
    public class UsernameColumnWriter : ColumnWriterBase
    {
        public UsernameColumnWriter() : base(NpgsqlDbType.Varchar)
        {
        }
        public override object GetValue(LogEvent logEvent, IFormatProvider formatProvider = null)
        {
            var (username, value) = logEvent.Properties.FirstOrDefault(p => p.Key == "user_name"); // burda usernmae catiram logEvent vasitesi ile ve ordaki propetini goturem qabaginda verdiyim deyerler ile (username, value) FirstOrDefault(p => p.Key == "user_name") ilede deyiremki bele bir deyer var 
            return value?.ToString() ?? null; // burda ise loglamda muleq sekilde bele vermeliyiki yaxalaya bilek username
        }
    }
}
