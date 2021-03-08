using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.Data;
using SmartSchool.Dto;
using SmartSchool.Model;
using System.Collections.Generic;

namespace SmartSchool.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public ProfessorController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        /// <summary>
        /// Método responsável para retornar todos meus professores
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var professor = _repo.GetAllProfessor(false);
            return Ok(_mapper.Map<IEnumerable<ProfessorDto>>(professor));
        }
        /// <summary>
        /// Método responsável por retornar apenas um professor por meio do código Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet ("{id}")]
        public IActionResult GetById(int Id)
        {
            var professor = _repo.GetProfessorById(Id, true);
            if (professor == null) return BadRequest("Aluno não encontrado");
            var alunoDto =(_mapper.Map<ProfessorDto>(professor));
            return Ok(alunoDto);
        }
        /// <summary>
        /// Método responsávem para inserir um aluno no sistema.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(ProfessorDto model)
        {
            var professo = _mapper.Map<Professor>(model);
            _repo.Create(professo);
            if (_repo.SaveChanges())
            {
                return Created($"api/professor/{model.Id}", _mapper.Map<ProfessorDto>(professo));
            }
            return BadRequest("Aluno não cadastrado");
        }
        /// <summary>
        /// Método responsável para realizar atualizações por meio do codigo Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut ("{id}")]
        public IActionResult Put(int id, ProfessorDto model)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");
            _mapper.Map(professor, model);
            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"api/professo/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não encontrado");
        }
        /// <summary>
        /// Método responsável para realizar atualizações por meio do codigo Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, ProfessorDto model)
        {
            var professor = _repo.GetProfessorById(id);
            if (professor == null) return BadRequest("Professor não encontrado");
            _mapper.Map(professor, model);
            _repo.Update(professor);
            if (_repo.SaveChanges())
            {
                return Created($"api/professo/{model.Id}", _mapper.Map<ProfessorDto>(professor));
            }
            return BadRequest("Professor não encontrado");
        }
        /// <summary>
        /// Método responsável para expluir uma Professor.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete ("{id}")]
        public IActionResult Delete(int Id)
        {
            var professor = _repo.GetProfessorById(Id, false);
            if (professor == null) return BadRequest();
            _repo.Delete(professor);
            if (_repo.SaveChanges())
            {
                return Ok("Professor foi deletado.");
            }
            return BadRequest();
        }
    }
}
