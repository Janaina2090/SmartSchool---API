using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Model
{
    public class Curso
    {

        public Curso()
        {}

        public Curso(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public IEnumerable<Disciplina> Disciplina { get; set; }
        public IEnumerable<AlunoCurso> AlunosCursos { get; set; }
    }
}
