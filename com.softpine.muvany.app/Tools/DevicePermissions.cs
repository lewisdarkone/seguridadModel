using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.softpine.muvany.app.Tools
{
    public class DevicePermissions
    {
        /// <summary>
        /// Verificar y solicitar permiso para almacenamiento local
        /// </summary>
        /// <returns></returns>
        public async Task LocalStorateWritePermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageWrite>();

            if ( status == PermissionStatus.Unknown || status == PermissionStatus.Denied )
            {
                var statusRequest = await Permissions.RequestAsync<Permissions.StorageWrite>();
                if ( status != PermissionStatus.Unknown || status != PermissionStatus.Denied )
                {
                    await Console.Out.WriteLineAsync("Permisos asignados");
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("Permisos asignados");

            }
        }

        /// <summary>
        /// Verificar permisos de lectura local
        /// </summary>
        /// <returns></returns>
        public async Task LocalStorateReadPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if ( status == PermissionStatus.Unknown || status == PermissionStatus.Denied )
            {
                var statusRequest = await Permissions.RequestAsync<Permissions.StorageRead>();
                if ( status != PermissionStatus.Unknown || status != PermissionStatus.Denied )
                {
                    await Console.Out.WriteLineAsync("Permisos asignados");
                }
            }
            else
            {
                await Console.Out.WriteLineAsync("Permisos asignados");

            }
        }
    }
    
}
