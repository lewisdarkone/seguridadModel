namespace com.softpine.muvany.models.Requests;

/// <summary>
///  Clase para los parametros de solicitud de token
/// </summary>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record TokenRequest(string Email, string Password);


