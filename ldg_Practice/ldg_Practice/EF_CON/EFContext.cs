using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ldg_Practice.EF_CON.Model;

namespace ldg_Practice.EF_CON
{
    internal class EFContext : DbContext //Db연결 구문
    {
        public DbSet<User>? User { get; set; } //Dbset<Table 이름>

        string connection = "server=localhost;port=3306;database=LDG;user=root;password=dlehdrlf1;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                connection,
                new MySqlServerVersion(new Version(8, 0, 21))
                );
        }

    }
}
