using System;
using System.Collections.Generic;
using System.Text;
using Realms;
namespace GalleryApp.Model
{
    public class UsuarioModel : RealmObject
    {
        public int Id { get; set; }
        public string Nombre  { get; set; }
        public string Usuario { get; set; }
        public string Contrasena { get; set; }
        public string FechaIngreso { get; set; }
    }
}
