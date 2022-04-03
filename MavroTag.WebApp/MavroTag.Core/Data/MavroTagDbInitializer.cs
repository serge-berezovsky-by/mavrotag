using MavroTag.Core.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MavroTag.Core.Data
{
    public class MavroTagDbInitializer : MigrateDatabaseToLatestVersion<MavroTagDbContext, Configuration>
    {
    }
}