using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartSchool.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using SmartSchool.Dto;
using SmartSchool.Model;

namespace SmartSchool.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public AlunoController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        /// <summary>
        /// Método responsável para retornar todos os meus Aluno.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var aluno = _repo.GetAllAluno(true);
            return Ok(_mapper.Map<IEnumerable<AlunoDto>>(aluno));
        }
        /// <summary>
        /// Método responsável por retornar apenas um Aluno, por meio do Código ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet ("{Id}")]
        public IActionResult GetById(int id)
        {
            var aluno = _repo.GetAlunoById(id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");
            var alunoDto = _mapper.Map<AlunoDto>(aluno);
            return Ok(alunoDto);
        }
        /// <summary>
        /// Método responsávem para inserir um aluno no sistema.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post(AlunoDto model)
        {
            var aluno = _mapper.Map<Aluno>(model);
            _repo.Create(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não encontrado");
        }
        /// <summary>
        /// Método responsável para realizar atualizações por meio do codigo Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut ("{id}")]
        public IActionResult Put(int Id, AlunoDto model)
        {
            var aluno = _repo.GetAlunoById(Id, false);
            if (aluno == null) return BadRequest("Aluno não encontrado");
            _mapper.Map(aluno, model);
            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não encontrado");
        }
        /// <summary>
        /// Método responsável para realizar atualizações por meio do codigo Id.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch ("{id}")]
        public IActionResult Patch(int Id, AlunoDto model)
        {
            var aluno = _repo.GetAlunoById(Id, false);
            if (aluno == null) return BadRequest("Aluno não localizado");
            _mapper.Map(aluno, model);
            _repo.Update(aluno);
            if (_repo.SaveChanges())
            {
                return Created($"api/aluno/{model.Id}", _mapper.Map<AlunoDto>(aluno));
            }
            return BadRequest("Aluno não encontrado");
        }
        /// <summary>
        /// Método responsável para excluir uma aluno.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete ("{id}")]
        public IActionResult Delete(int Id)
        {
            var aluno = _repo.GetAlunoById(Id, false);
            if (aluno == null) return BadRequest("Aluno não localizado");
            _repo.Delete(aluno);
            if (_repo.SaveChanges())
            {
                return Ok("Aluno Deletado");
            }
            return BadRequest();
        }
    }
} 
