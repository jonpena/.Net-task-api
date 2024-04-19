using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;

namespace TaskApi.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class TareasController : ControllerBase
  {
    /** Setting Data Context EF **/

    private readonly DataContext context;

    public TareasController(DataContext context)
    {
      this.context = context;
    }

    /** Getting All Tasks [GET] **/

    [HttpGet]
    public async Task<ActionResult<List<Tarea>>> GetTasks()
    {
      return Ok(await this.context.Tareas.ToListAsync());
    }

    /** Getting A Task Through The ID [GET] **/

    [HttpGet("{id}")]
    public async Task<ActionResult<List<Tarea>>> GetTask(string id)
    {
      var tarea = this.context.Tareas.FindAsync(id);

      if (tarea.Result == null) return BadRequest("Task not Found");

      return Ok(tarea.Result);
    }

    /** Creating a New Task [POST] **/

    [HttpPost]
    [Route("CreateTask")]
    public async Task<ActionResult<List<Tarea>>> CreateTask(Tarea tarea)
    {
      tarea.Fecha = DateTime.Now;
      this.context.Tareas.Add(tarea);
      await this.context.SaveChangesAsync();
      return Ok(await this.context.Tareas.ToListAsync());
    }

    /** Updating A Task [PUT] **/

    [HttpPut]
    [Route("UpdateTask")]
    public async Task<ActionResult<List<Tarea>>> UpdateTask(Tarea newTarea)
    {
      var tarea = await this.context.Tareas.FindAsync(newTarea.Id);

      if (tarea == null) return BadRequest("Task Not Found And Not Updated");

      tarea.Titulo = newTarea.Titulo;
      tarea.Fecha = DateTime.Now;
      tarea.Descripcion = newTarea.Descripcion;

      await this.context.SaveChangesAsync();

      return Ok(await this.context.Tareas.ToListAsync());
    }

    /** Deleting A Task Through The ID [GET] **/

    [HttpDelete("DeleteTask/{id}")]
    public async Task<ActionResult<List<Tarea>>> DeleteTask(string id)
    {
      var tarea = this.context.Tareas.FindAsync(id);

      if (tarea.Result == null) return BadRequest("Task not Found and not Deleted");

      this.context.Tareas.Remove(tarea.Result);

      await this.context.SaveChangesAsync();
 
      return Ok(await this.context.Tareas.ToListAsync());
    }
  }
}
