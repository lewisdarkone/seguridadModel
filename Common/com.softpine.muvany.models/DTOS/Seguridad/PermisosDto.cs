namespace com.softpine.muvany.models.DTOS;

/// <summary>
/// 
/// </summary>
public class PermisosDto
{
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? Descripcion { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int IdAccion { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? AccionNombre { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int IdRecurso { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? RecursoNombre { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int? EsBasico { get; set; }

    public override string ToString()
    {
        if ( this == null || string.IsNullOrEmpty(Descripcion))        
            return "No Asignado";
        else        
            return Descripcion;
    }

}
