using SmartSchool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Data
{
   public  interface IRepository
    {
        void Create <T>(T entity) where T: class;
        void Update <T>(T entity) where T: class;
        void Delete <T>(T entity) where T: class;
        bool SaveChanges();

        Aluno[] GetAllAluno(bool IncluirProfessor = false);
        Aluno[] GetAllAlunoByDisciplinaId(int disciplinaId, bool IncluirProfessor = false);
        Aluno GetAlunoById(int alunoId, bool IncluirProfessor = false);

        Professor[] GetAllProfessor(bool IncluirAluno = false);
        Professor[] GetAllProfessorByDisciplinaId(int disciplinaId, bool IncluirAluno = false);
        Professor GetProfessorById(int professorId, bool IncluirAluno = false);
        
    }
}
