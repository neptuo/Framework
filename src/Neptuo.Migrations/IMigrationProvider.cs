using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Migrations
{
    public interface IMigrationProvider
    {
        DateTime Timestamp { get; }

        IMigrationService FindUpMigrations();
        IMigrationService FindUpMigrations(DateTime target);

        IMigrationService FindDownMigrations(DateTime target);
    }
}
