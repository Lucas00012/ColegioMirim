using ColegioMirim.Domain.Usuarios;
using FluentMigrator;

namespace ColegioMirim.Infrastructure.Data.Migrations
{
    [Migration(1)]
    public class InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table("Aluno")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Nome").AsAnsiString(60).NotNullable()
                .WithColumn("RA").AsAnsiString(8).NotNullable()
                .WithColumn("UsuarioId").AsInt32().NotNullable()
                .WithColumn("Ativo").AsBoolean().WithDefaultValue(true)
                .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
                .WithColumn("UpdatedAt").AsDateTimeOffset().Nullable();

            Create.Table("AlunoTurma")
                .WithColumn("Ativo").AsBoolean().WithDefaultValue(true)
                .WithColumn("AlunoId").AsInt32().NotNullable()
                .WithColumn("TurmaId").AsInt32().NotNullable()
                .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
                .WithColumn("UpdatedAt").AsDateTimeOffset().Nullable();

            Create.Table("Turma")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Ativo").AsBoolean().WithDefaultValue(true)
                .WithColumn("Nome").AsAnsiString(60).NotNullable()
                .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
                .WithColumn("UpdatedAt").AsDateTimeOffset().Nullable()
                .WithColumn("Ano").AsInt32().NotNullable();

            Create.Table("Usuario")
                .WithColumn("Id").AsInt32().NotNullable().PrimaryKey().Identity()
                .WithColumn("Email").AsAnsiString(60).NotNullable()
                .WithColumn("SenhaHash").AsAnsiString(120).NotNullable()
                .WithColumn("CreatedAt").AsDateTimeOffset().NotNullable()
                .WithColumn("UpdatedAt").AsDateTimeOffset().Nullable()
                .WithColumn("TipoUsuario").AsAnsiString(60).NotNullable();

            Create.PrimaryKey("PK_AlunoTurma")
                .OnTable("AlunoTurma").Columns("AlunoId", "TurmaId");

            Create.ForeignKey("FK_Aluno_Usuario")
                .FromTable("Aluno").ForeignColumn("UsuarioId")
                .ToTable("Usuario").PrimaryColumn("Id");

            Create.ForeignKey("FK_AlunoTurma_Aluno")
                .FromTable("AlunoTurma").ForeignColumn("AlunoId")
                .ToTable("Aluno").PrimaryColumn("Id");

            Create.ForeignKey("FK_AlunoTurma_Turma")
                .FromTable("AlunoTurma").ForeignColumn("TurmaId")
                .ToTable("Turma").PrimaryColumn("Id");

            Insert.IntoTable("Usuario")
                .Row(new
                {
                    Email = "admin@colegiomirim.com",
                    CreatedAt = DateTimeOffset.Now,
                    SenhaHash = "85c58f1677c957fcd40bc923235cf06e", //@Aa123456
                    TipoUsuario = TipoUsuario.Administrador
                });
        }

        public override void Down()
        {
            Delete.ForeignKey("FK_AlunoTurma_Aluno").OnTable("AlunoTurma");
            Delete.ForeignKey("FK_AlunoTurma_Turma").OnTable("AlunoTurma");

            Delete.Table("Aluno");
            Delete.Table("AlunoTurma");
        }
    }
}
