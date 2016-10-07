using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Logueo
    {
        public string usuario { get; set; }
        public string password { get; set; }
        public String respuesta { get; set; }
        public int IdUsuario { get; set; }

        public Logueo()
        {
        }

        public Logueo(string l_usuario, string l_password, string l_respuesta, int l_IdUsuario)
        {
            this.usuario = l_usuario;
            this.password = l_password;
            this.respuesta = l_respuesta;
            this.IdUsuario = l_IdUsuario;
        }
    }
}
