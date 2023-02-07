using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NetServerDemo.Model;

namespace NetServerDemo
{
    internal class EFContext : DbContext //Db연결 구문
    {
        public DbSet<User_TBL>? User_TBLs { get; set; } //Dbset<Table 이름>
        public DbSet<ChatRoom_TBL>? ChatRoom_TBLs { get; set; }

        public DbSet<Chat_TBL>? Chat_TBLs { get; set; }

        string connection = "server=localhost;port=3306;database=LDG;user=root;password=dlehdrlf1;Charset=win1252"; 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                connection,
                new MySqlServerVersion(new Version(8, 0, 21))
                );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_TBL>()
                .Property(x => x.User_Num)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<User_TBL>()
                .HasKey(p => new { p.User_Num });

            modelBuilder.Entity<ChatRoom_TBL>()
                .Property(x => x.ChatRoom_Num)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ChatRoom_TBL>()
                .HasKey(p => new { p.ChatRoom_Num });

            modelBuilder.Entity<Chat_TBL>()
                .Property(x => x.Chat_Num)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Chat_TBL>()
                .HasKey(p => new { p.Chat_Num });
        }

    }
}
