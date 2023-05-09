using AlunosApi.Models;
using AlunosApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AlunosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunos()
        {
            try
            {
                var alunos = await _alunoService.GetAlunos();
                return Ok(alunos);
            }
            catch
            {

                //return BadRequest("request invalid");
                return StatusCode(StatusCodes.Status500InternalServerError, "Get alunos Error");
            }
        }

        [HttpGet]
        [Route("GetAlunoByName")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunoById([FromQuery] string name)
        {
            try
            {
                var alunos = await _alunoService.GetAlunoByName(name);
                if (alunos.Count() == 0)
                {
                    return NotFound($"there´s no alunos with name {name} in database");
                }
                else
                {
                    return Ok(alunos);
                }

            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, "invalid request");
            }
        }

        [HttpGet]
        [Route("GetAlunoById")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunoById([FromQuery] int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if (aluno == null)
                {
                    return NotFound($"there´s no aluno with id {id} in database");
                }
                else
                {
                    return Ok(aluno);
                }
            }
            catch
            {

                return StatusCode(StatusCodes.Status400BadRequest, "invalid request");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return StatusCode(StatusCodes.Status201Created, "Aluno created");
            }
            catch 
            {

                return StatusCode(StatusCodes.Status400BadRequest, "invalid request");
            }
        }

    }
}
