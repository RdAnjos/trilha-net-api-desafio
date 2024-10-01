using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // TODO: Buscar o Id no banco utilizando o EF
            var byId = _context.Tarefas.SingleOrDefault(x => x.Id == id);
            // TODO: Validar o tipo de retorno. Se não encontrar a tarefa, retornar NotFound,
            if (byId == null)
                return NotFound();

            // caso contrário retornar OK com a tarefa encontrada
            return Ok(byId);
        }

        //WORKING
        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // TODO: Buscar todas as tarefas no banco utilizando o EF
            var todasTarefas = _context.Tarefas.ToList();

            if (todasTarefas == null)
                return NotFound();

            return Ok(todasTarefas);
        }

        //WORKING
        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o titulo recebido por parâmetro
            var byTitulo = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));

            if (byTitulo == null)
                return NotFound();

            // Dica: Usar como exemplo o endpoint ObterPorData
            return Ok(byTitulo);
        }

        //WORKING
        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        //WORKING
        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // TODO: Buscar  as tarefas no banco utilizando o EF, que contenha o status recebido por parâmetro
            // Dica: Usar como exemplo o endpoint ObterPorData
            var tarefa = _context.Tarefas.Where(x => x.Status == status);

            return Ok(tarefa);
        }

        //WORKING
        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Adicionar a tarefa recebida no EF e salvar as mudanças (save changes)
            _context.Add(tarefa);
            _context.SaveChanges();

            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        //WORKING
        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.SingleOrDefault(x => x.Id == id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // TODO: Atualizar as informações da variável tarefaBanco com a tarefa recebida via parâmetro
            tarefaBanco.UpdateTask(tarefa.Titulo, tarefa.Descricao, tarefa.Data, tarefa.Status);

            // TODO: Atualizar a variável tarefaBanco no EF e salvar as mudanças (save changes)
            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();
            return Ok();
        }

        //WORKING
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // TODO: Remover a tarefa encontrada através do EF e salvar as mudanças (save changes)
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
