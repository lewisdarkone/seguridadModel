namespace com.softpine.muvany.models.Entities.Generals;

/// <summary>
/// Entidad a ser heredada por todas las demas entidades
/// </summary>
public class EntidadBase
{
    public Guid Id { get; set; }
    public DateTime FechaCreado { get; set; }
    public UserBasicInfo CreadoPor { get; set; }
    public DateTime FechaModificado { get; set; }
    public UserBasicInfo ModificadoPor { get; set; }


    public void SetCreateDates()
    {
        FechaCreado = DateTime.Now;
        FechaModificado = DateTime.Now;
    }

}
