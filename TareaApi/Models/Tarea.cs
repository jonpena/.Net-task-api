using System.ComponentModel.DataAnnotations;

namespace TaskApi.Models
{
  public class Tarea
  {
    [StringLength (50)]
    public string Id { get; set; } = string.Empty;
    
    [StringLength (50)]
    public string Titulo { get; set; } = string.Empty;
    
    public DateTime Fecha { get; set; }

    [StringLength(250)]
    public string Descripcion { get; set; } = string.Empty;
  }
}
