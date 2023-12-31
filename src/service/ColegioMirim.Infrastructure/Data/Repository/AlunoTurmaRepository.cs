﻿using ColegioMirim.Domain.AlunosTurma;
using Dapper;

namespace ColegioMirim.Infrastructure.Data.Repository
{
    public class AlunoTurmaRepository : IAlunoTurmaRepository
    {
        private readonly ColegioMirimContext _context;

        public AlunoTurmaRepository(ColegioMirimContext context)
        {
            _context = context;
        }

        public async Task<int> Count()
        {
            using var connection = _context.BuildConnection();
            var sql = "SELECT COUNT(*) FROM AlunoTurma";
            var count = await connection.QuerySingleAsync<int>(sql);

            return count;
        }

        public async Task<AlunoTurma> Create(AlunoTurma alunoTurma)
        {
            alunoTurma.CreatedAt = DateTimeOffset.Now;

            var sql = @"
                INSERT INTO 
                    AlunoTurma (Ativo, AlunoId, TurmaId, CreatedAt, UpdatedAt)
                OUTPUT INSERTED.Id
                VALUES (@Ativo, @AlunoId, @TurmaId, @CreatedAt, null)
            ";

            using var connection = _context.BuildConnection();
            alunoTurma.Id = await connection.QuerySingleAsync<int>(sql, alunoTurma);

            return alunoTurma;
        }

        public async Task<int> Delete(AlunoTurma alunoTurma)
        {
            using var connection = _context.BuildConnection();
            var sql = "DELETE FROM AlunoTurma WHERE Id = @Id";
            var result = await connection.ExecuteAsync(sql, alunoTurma);

            return result;
        }

        public async Task<List<AlunoTurma>> GetAll()
        {
            using var connection = _context.BuildConnection();
            var sql = "SELECT * FROM AlunoTurma";
            var alunosTurma = await connection.QueryAsync<AlunoTurma>(sql);

            return alunosTurma.ToList();
        }

        public async Task<AlunoTurma> GetById(int id)
        {
            using var connection = _context.BuildConnection();
            var sql = "SELECT * FROM AlunoTurma WHERE Id = @Id";
            var alunosTurma = await connection.QuerySingleOrDefaultAsync<AlunoTurma>(sql, new { Id = id });

            return alunosTurma;
        }

        public async Task<AlunoTurma> GetByAlunoIdTurmaId(int alunoId, int turmaId)
        {
            using var connection = _context.BuildConnection();
            var sql = "SELECT * FROM AlunoTurma WHERE AlunoId = @AlunoId AND TurmaId = @TurmaId";
            var alunosTurma = await connection.QuerySingleOrDefaultAsync<AlunoTurma>(sql, new { AlunoId = alunoId, TurmaId = turmaId });

            return alunosTurma;
        }

        public async Task<AlunoTurma> GetByUsuarioIdTurmaId(int usuarioId, int turmaId)
        {
            var sql = @"
                SELECT 
                    at.Id,
                    at.Ativo,
                    at.AlunoId,
                    at.TurmaId
                FROM 
                AlunoTurma AS at 
                INNER JOIN Aluno AS a 
                WHERE a.UsuarioId = @UsuarioId AND at.TurmaId = @TurmaId
            ";

            using var connection = _context.BuildConnection();
            var alunosTurma = await connection.QuerySingleOrDefaultAsync<AlunoTurma>(sql, new { UsuarioId = usuarioId, TurmaId = turmaId });

            return alunosTurma;
        }

        public async Task<int> Update(AlunoTurma alunoTurma)
        {
            alunoTurma.UpdatedAt = DateTimeOffset.Now;

            using var connection = _context.BuildConnection();
            var sql = "UPDATE AlunoTurma SET AlunoId = @AlunoId, TurmaId = @TurmaId, Ativo = @Ativo, CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt WHERE Id = @Id";
            var result = await connection.ExecuteAsync(sql, alunoTurma);

            return result;
        }
    }
}
