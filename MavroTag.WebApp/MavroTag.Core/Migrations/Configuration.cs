namespace MavroTag.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<MavroTag.Core.Data.MavroTagDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MavroTag.Core.Data.MavroTagDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            context.Users.AddOrUpdate(c => c.Name, new Domain.User()
            {
                Name = "Administrator",
                Passphrase = "D35C178F-E027-431B-AB84-2119032BCB1B",
            });
        }
    }
}
