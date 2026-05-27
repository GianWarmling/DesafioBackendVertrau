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

        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> Get()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
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

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Usuario usuario)
        {
            var emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email);

            if (emailExiste)
            {
                return BadRequest("Email já cadastrado!");
            }

            if (usuario.DataNascimento > DateTime.Now)
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

        // PUT api/<UsuarioController>/5
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

            if (usuario.DataNascimento > DateTime.Now)
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

        // DELETE api/<UsuarioController>/5
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
