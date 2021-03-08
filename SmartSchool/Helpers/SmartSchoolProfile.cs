using AutoMapper;
using SmartSchool.Dto;
using SmartSchool.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.Helpers
{
    public class SmartSchoolProfile : Profile
    {
        public SmartSchoolProfile()
        {
            CreateMap<Aluno, AlunoDto>()
                .ForMember(des => des.Nome,
                           opt => opt.MapFrom(srp => $"{srp.Nome} {srp.Sobrenome}"))
                .ForMember(des => des.Idade,    
                opt => opt.MapFrom(src => src.DataNascimento.PegarIdade()))
                ;
            CreateMap<Aluno, Aluno>();

            CreateMap<Professor, ProfessorDto>().ReverseMap();
        }






    }
}
