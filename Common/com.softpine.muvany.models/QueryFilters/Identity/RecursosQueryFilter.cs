#pragma warning disable 1591
namespace com.softpine.muvany.models.QueryFilters;

public class RecursosQueryFilter : BasePostQueryFilter
{

    public Int32? Id { get; set; }

    public String? Nombre { get; set; }

    public Int32? IdModulo { get; set; }

    public string? NombreModulo { get; set; }

    public int? EsMenuConfiguracion { get; set; }

    public string? DescripcionMenuConfiguracion { get; set; }
}




