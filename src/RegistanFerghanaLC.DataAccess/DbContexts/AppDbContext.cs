using Microsoft.EntityFrameworkCore;
using RegistanFerghanaLC.Domain.Entities;
using RegistanFerghanaLC.Domain.Entities.ExtraLessons;
using RegistanFerghanaLC.Domain.Entities.Students;
using RegistanFerghanaLC.Domain.Entities.Teachers;
using RegistanFerghanaLC.Domain.Entities.Users;

namespace RegistanFerghanaLC.DataAccess.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
            : base(contextOptions)
        {

        }

        public virtual DbSet<Admin> Admins { get; set; } = default!;
        public virtual DbSet<Teacher> Teachers { get; set; } = default!;
        public virtual DbSet<Student> Students { get; set; } = default!;
        public virtual DbSet<Subject> Subjects { get; set; } = default!;
        public virtual DbSet<StudentSubject> StudentSubjects { get; set; } = default!;
        public virtual DbSet<ExtraLesson> ExtraLessons { get; set; } = default!;
        public virtual DbSet<ExtraLessonDetails> ExtraLessonDetails { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
