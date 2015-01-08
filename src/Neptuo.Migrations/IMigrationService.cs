using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neptuo.Migrations
{
    public interface IMigrationService
    {
        bool IsExecuted { get; }
        bool HasMigrations { get; }

        void Execute();
    }
}
