namespace com.softpine.muvany.models.Requests;

/// <summary>
///  Solicitud de Login por codigo.
/// </summary>
/// <param name="Email"></param>
/// <param name="Password"></param>
public record TokenByCodeRequest(string Email, string Code);


