using Aproxima.Models;
using Microsoft.EntityFrameworkCore;

namespace Aproxima.Data
{
    public partial class AproximaContext : DbContext
    {
        public AproximaContext()
        {
        }

        public AproximaContext(DbContextOptions<AproximaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Agendamento> Agendamento { get; set; }
        public virtual DbSet<Classificacao> Classificacao { get; set; }
        public virtual DbSet<ClassificacaoFornecedor> ClassificacaoFornecedor { get; set; }
        public virtual DbSet<Documentacao> Documentacao { get; set; }
        public virtual DbSet<Fornecedor> Fornecedor { get; set; }
        public virtual DbSet<Solicitacao> Solicitacao { get; set; }
        public virtual DbSet<SolicitacaoDocumentacao> SolicitacaoDocumentacao { get; set; }
        public virtual DbSet<SolicitacaoFornecedor> SolicitacaoFornecedor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=Aproxima;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agendamento>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Data).HasColumnType("date");

                entity.Property(e => e.Hora).HasColumnType("time(4)");

                entity.HasOne(d => d.Solicitacao)
                    .WithMany(p => p.Agendamento)
                    .HasForeignKey(d => d.SolicitacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agendamento_Solicitacao");
            });

            modelBuilder.Entity<Classificacao>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Orientacao).IsUnicode(false);
            });

            modelBuilder.Entity<ClassificacaoFornecedor>(entity =>
            {
                entity.HasOne(d => d.Classificacao)
                    .WithMany(p => p.ClassificacaoFornecedor)
                    .HasForeignKey(d => d.ClassificacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassificacaoFornecedor_Classificacao");

                entity.HasOne(d => d.Fornecedor)
                    .WithMany(p => p.ClassificacaoFornecedor)
                    .HasForeignKey(d => d.FornecedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClassificacaoFornecedor_Fornecedor");
            });

            modelBuilder.Entity<Documentacao>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Classificacao)
                    .WithMany(p => p.Documentacao)
                    .HasForeignKey(d => d.ClassificacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Documentacao_Classificacao");
            });

            modelBuilder.Entity<Fornecedor>(entity =>
            {
                entity.Property(e => e.Descricao)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Solicitacao>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AdultosRenda).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Detalhamento).IsUnicode(false);

                entity.Property(e => e.RendaMensal).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Classificacao)
                    .WithMany(p => p.Solicitacao)
                    .HasForeignKey(d => d.ClassificacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Solicitacao_Classificacao");
            });

            modelBuilder.Entity<SolicitacaoDocumentacao>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Documentacao)
                    .WithMany(p => p.SolicitacaoDocumentacao)
                    .HasForeignKey(d => d.DocumentacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SolicitacaoDocumentacao_Documentacao");

                entity.HasOne(d => d.Solicitacao)
                    .WithMany(p => p.SolicitacaoDocumentacao)
                    .HasForeignKey(d => d.SolicitacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SolicitacaoDocumentacao_Solicitacao");
            });

            modelBuilder.Entity<SolicitacaoFornecedor>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Fornecedor)
                    .WithMany(p => p.SolicitacaoFornecedor)
                    .HasForeignKey(d => d.FornecedorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SolicitacaoFornecedor_Fornecedor");

                entity.HasOne(d => d.Solicitacao)
                    .WithMany(p => p.SolicitacaoFornecedor)
                    .HasForeignKey(d => d.SolicitacaoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SolicitacaoFornecedor_Solicitacao");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
