using DesafioBackendVertrau.Data;
using DesafioBackendVertrau.Models;
using DesafioBackendVertrau.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DesafioBackendVertrau.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Listar todos os usuários cadastrados
        /// </summary>
        /// <returns>Lista de Usuários</returns>
        /// <response code="200">Retorna a lista de usuários</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Usuario>), 200)]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }


        /// <summary>
        /// Busca um usuário pelo ID
        /// </summary>
        /// <param name="id">ID do usuário</param>
        /// <returns>Usuário encontrado</returns>
        /// <response code="200">Retorna o usuário</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetById(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado!");
            }
            return Ok(usuario);
        }

        /// <summary>
        /// Cadastra um novo usuário
        /// </summary>
        /// <param name="usuario">Dados do usuário a ser criado</param>
        /// <returns code="200">Usuário criado com sucesso</returns>
        /// <returns code="400">Email cadastrado, data de nascimento não pode ser futura, ou gênero inválido</returns>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario usuario)
        {
            var emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email);

            if (emailExiste)
            {
                return BadRequest("Email já cadastrado!");
            }

            if (usuario.DataNascimento.HasValue && usuario.DataNascimento.Value.Date > DateTime.Now.Date)
            {
                return BadRequest("Data de nascimento não pode ser futura!");
            }

            if(!Enum.IsDefined(typeof(Genero), usuario.Genero))
            {
                return BadRequest("Gênero inválido!");
            }

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok(usuario);
        }

        /// <summary>
        /// Atualiza os dados de um usuário existente
        /// </summary>
        /// <param name="id">Id do usuário a ser atualizado</param>
        /// <param name="usuario">Novos dados do usuário</param>
        /// <response code="200">Usuário atualizado com sucesso</response>
        /// <response code="400">Email já cadastrado, data de nascimento futura ou gênero inválido</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Usuario usuario)
        {
            var usuarioBanco = await _context.Usuarios.FindAsync(id);

            if (usuarioBanco == null) 
            {
                return NotFound("Usuário não encontrado!");
            }

            var emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email && u.Id != id);

            if (emailExiste)
            {
                return BadRequest("Email já cadastrado!");
            }

            if (usuario.DataNascimento > DateTime.Now.Date)
            {
                return BadRequest("Data de nascimento não pode ser futura!");
            }

            if (!Enum.IsDefined(typeof(Genero), usuario.Genero))
            {
                return BadRequest("Gênero inválido!");
            }

            usuarioBanco.Nome = usuario.Nome;
            usuarioBanco.Sobrenome = usuario.Sobrenome;
            usuarioBanco.Email = usuario.Email;
            usuarioBanco.Genero = usuario.Genero;
            usuarioBanco.DataNascimento = usuario.DataNascimento;

            await _context.SaveChangesAsync();
            return Ok("Usuário atualizado com sucesso!");
        }

        /// <summary>Remove um usuário pelo ID</summary>
        /// <param name="id">ID do usuário a ser removido</param>
        /// <response code="200">Usuário removido com sucesso</response>
        /// <response code="404">Usuário não encontrado</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound("Usuário não encontrado!");
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return Ok("Usuário removido com sucesso!");
        }
    }
}
