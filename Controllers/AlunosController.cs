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
        [Route("CreateAluno")]
        public async Task<ActionResult> Create(Aluno aluno)
        {
            try
            {
                await _alunoService.CreateAluno(aluno);
                return StatusCode(StatusCodes.Status201Created, "Aluno has been created successfully");
            }
            catch
            {

                return StatusCode(StatusCodes.Status400BadRequest, "invalid request");
            }
        }

        [HttpPut]
        [Route("UpdateAluno/{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] Aluno aluno)
        {
            try
            {
                if (aluno.Id == id)
                {
                    await _alunoService.UpdateAluno(aluno);
                    return StatusCode(StatusCodes.Status200OK, "Aluno has been Updated successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid request");
                }


            }
            catch
            {
                return StatusCode(StatusCodes.Status400BadRequest, "invalid request");
            }
        }

        [HttpDelete]
        [Route("DeleteAluno")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if (aluno != null)
                {
                    await _alunoService.DeleteAluno(aluno);
                    return StatusCode(StatusCodes.Status200OK, "Aluno has been deleted successfully");
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "invalid request");
                }

            }
            catch 
            {
                return StatusCode(StatusCodes.Status400BadRequest, "invalid request");
            }
         
        }
    }
}
