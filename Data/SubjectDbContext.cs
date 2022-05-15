using Microsoft.EntityFrameworkCore;
using RestAPI.Models;

namespace RestAPI.Data
{
    public class SubjectDbContext : DbContext
    {
        public SubjectDbContext(DbContextOptions<SubjectDbContext> options) : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; }
    }
}
