using Microsoft.EntityFrameworkCore;
using SmartSchool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Data
{
    public class Repository : IRepository
    {
        private readonly SmartContext _context;

        public Repository(SmartContext context)
        {
            _context = context;
        }


        public void Create<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        
        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 1 ? true : false;
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }


        public Aluno[] GetAllAluno(bool IncluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;
            if (IncluirProfessor)
            {
                query = query.Include(ad => ad.AlunosDisciplinas)
                    .ThenInclude(d => d.Disciplina)
                    .ThenInclude(p => p.Professor);
            }
            query = query.AsNoTracking().OrderBy(a => a.Id);
            return query.ToArray();
        }

        public Aluno[] GetAllAlunoByDisciplinaId(int AlunoId, bool IncluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (IncluirProfessor)
            {
                query = query.Include(ad => ad.AlunosDisciplinas)
                    .ThenInclude(d => d.Disciplina)
                    .ThenInclude(p => p.Professor);    
            }
            query = query.AsNoTracking().OrderBy(alu => alu.Id).Where(alu => alu.AlunosDisciplinas.Any(ad => ad.DisciplinaId == AlunoId));

            return query.ToArray();
        }

        public Aluno GetAlunoById(int alunoId, bool IncluirProfessor = false)
        {
            IQueryable<Aluno> query = _context.Alunos;

            if (IncluirProfessor)
            {
                query = query.Include(ad => ad.AlunosDisciplinas)
                    .ThenInclude(d => d.Disciplina)
                    .ThenInclude(p => p.Professor);
            }

            query = query.AsNoTracking().OrderBy(alu => alu.Id).Where(alu => alu.Id == alunoId);
            return query.FirstOrDefault();
        }

        public Professor [] GetAllProfessor(bool IncluirAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (IncluirAluno)
            {
                query = query.Include(d => d.Disciplinas)
                    .ThenInclude(ad => ad.AlunosDisciplinas)
                    .ThenInclude(a => a.Aluno);
            }
            query = query.AsNoTracking().OrderBy(pro => pro.Id);
            return query.ToArray();
        }

        public Professor[] GetAllProfessorByDisciplinaId(int disciplinaId, bool IncluirAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;
            if (IncluirAluno)
            {
                query = query.Include(d => d.Disciplinas)
                    .ThenInclude(ad => ad.AlunosDisciplinas)
                    .ThenInclude(a => a.Aluno);
            }
            query = query.AsNoTracking().OrderBy(pro => pro.Id).Where(d => d.Disciplinas.Any(ad => ad.AlunosDisciplinas.Any(d => d.DisciplinaId == disciplinaId)));
            return query.ToArray();
        }

        
        public Professor GetProfessorById(int ProfessorId, bool IncluirAluno = false)
        {
            IQueryable<Professor> query = _context.Professores;

            if (IncluirAluno)
            {
                query = query.Include(d => d.Disciplinas)
                    .ThenInclude(ad => ad.AlunosDisciplinas)
                    .ThenInclude(a => a.Aluno);
            }
            query = query.AsNoTracking().OrderBy(pro => pro.Id).Where(pro => pro.Id == ProfessorId);
            return query.FirstOrDefault();
        }

    }
}
