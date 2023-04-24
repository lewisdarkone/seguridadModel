#pragma warning disable 1591

namespace com.softpine.muvany.core.Contracts
{
    public class Menu
    {
        public Int32 Id { get; set; }

        public String Nombre { get; set; }
        public int? ModuloPadre { get; set; }
        public string? URL { get; set; }
        public string? Cssicon { get; set; }

        public ICollection<Menu1>? Recursos { get; set; }
    }
    public class Menu1
    {
        public Int32 Id { get; set; }

        public String Nombre { get; set; }
        public int? ModuloPadre { get; set; }
        public string? URL { get; set; }
        public string? Cssicon { get; set; }

        public ICollection<Menu2>? Recursos { get; set; }
    }
    public class Menu2
    {
        public Int32 Id { get; set; }

        public String Nombre { get; set; }
        public int? ModuloPadre { get; set; }
        public string? URL { get; set; }
        public string? Cssicon { get; set; }

        public ICollection<Menu3>? Recursos { get; set; }
    }
    public class Menu3
    {
        public Int32 Id { get; set; }

        public String Nombre { get; set; }
        public int? ModuloPadre { get; set; }
        public string? URL { get; set; }
    }

}
