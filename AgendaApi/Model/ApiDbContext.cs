using Microsoft.EntityFrameworkCore;

namespace AgendaApi.Model
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Profissional> Profissionais { get; set; }
        public DbSet<Consulta> Consultas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Consulta>(entity =>
            {
                entity.HasKey(c => c.ConsultaId);

                entity.Property(c => c.NomeProfissional)
                      .HasMaxLength(100);

                entity.Property(c => c.RegistroProfissional)
                      .HasMaxLength(50);

                entity.Property(c => c.NomePaciente)
                      .HasMaxLength(100);

                entity.Property(c => c.CpfPaciente)
                      .HasMaxLength(14);

                entity.Property(c => c.DataConsulta)
                      .IsRequired();
            });
        }
    }
}
