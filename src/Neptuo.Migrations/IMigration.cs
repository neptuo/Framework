using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Migrations
{
    /// <summary>
    /// Single migration.
    /// </summary>
    public interface IMigration
    {
        void Execute();
    }

    /// <summary>
    /// Don't need <see cref="MigrationAttribute"/>.
    /// </summary>
    public interface IDateTimeMigration : IMigration
    {
        DateTime Timestamp { get; }
    }
}
