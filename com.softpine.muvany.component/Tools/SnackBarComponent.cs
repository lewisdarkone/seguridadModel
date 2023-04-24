using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace com.softpine.muvany.component.Tools;

public class SnackBarComponent
{
    private readonly ISnackbar SnackbarService;
    public SnackBarComponent(ISnackbar snackbarService)
    {
        SnackbarService = snackbarService;
    }



    /// <summary>
    /// Muestra un Snackbar informativo
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    public void ShowSnackBarInfo(string title, string body)
    {
        SnackbarService.Add
            (
               title +"\n"+body,
               Severity.Info

            );
    }

    /// <summary>
    /// Muestra un Snackbar Error
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    public void ShowSnackBarError(string title, string body)
    {
        SnackbarService.Add
            (
               title + "\n" + body,
               Severity.Error

            );
    }

    /// <summary>
    /// Muestra un Snackbar Error de campos requeridos por defecto
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    public void ShowSnackBarErrorCamposRequeridos()
    {
        SnackbarService.Add
            (
               "Campos requeridos" + "\n" + "Algunos campos son requeridos o no tienen el formato correcto",
               Severity.Error

            );
    }

    /// <summary>
    /// Muestra un Snackbar Error de campos requeridos por defecto
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    public void ShowSnackBarInfoOperacionExitosa()
    {
        SnackbarService.Add
            (
               "Operación Exitosa" + "\n" + "Los datos se han guardado correctamente",
               Severity.Success

            );
    }
}
