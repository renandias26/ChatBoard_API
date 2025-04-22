using ChatBoard.DataBase.Entity;
using Microsoft.EntityFrameworkCore;

namespace ChatBoard.DataBase.Context
{
    public class DataBaseContext(DbContextOptions<DataBaseContext> options) : DbContext(options)
    {
        public DbSet<Message> Message { get; set; }
        public DbSet<Group> Group { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Message>()
           .HasOne<Group>()
           .WithMany()
           .HasForeignKey(m => m.GroupId)
           .IsRequired();
        }
    }
}
