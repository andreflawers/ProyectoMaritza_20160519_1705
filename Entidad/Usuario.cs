using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Usuario
    {
        public int NumUsuario { get; set; }
        public int IdUsuario { get; set; }
        public String nombreUsuario { get; set; }
        public String passwordUsuario { get; set; }
        public String estadoUsuario { get; set; }
        public int NumRol { get; set; }
        public int IdRol { get; set; }
        public String nombreRol { get; set; }
        public int NumPersonal { get; set; }
        public int IdPersonal { get; set; }
        public String nombrePersonal { get; set; }
        public String apellidoPaternoPersonal { get; set; }
        public String apellidoMaternoPersonal { get; set; }
        public String nombreTipoDocumento { get; set; }
        public String numeroDocumentoPersonal { get; set; }

        public Usuario()
        {
        }

        public Usuario(int u_num, int u_id, string u_nombre, string u_password, string u_estado, int u_r_num,
                        int u_r_id, String u_r_nombre, int u_p_num, int u_p_id, string u_p_nombre,
                        string u_p_apellidoPaterno, string u_p_apellidoMaterno, string u_p_nombreTipoDocumento, string u_p_numeroDocumento)
        {
            this.NumUsuario = u_num;
            this.IdUsuario = u_id;
            this.nombreUsuario = u_nombre;
            this.passwordUsuario = u_password;
            this.estadoUsuario = u_estado;
            this.NumRol = u_r_num;
            this.IdRol = u_r_id;
            this.nombreRol = u_r_nombre;
            this.NumPersonal = u_p_num;
            this.IdPersonal = u_p_id;
            this.nombrePersonal = u_p_nombre;
            this.apellidoPaternoPersonal = u_p_apellidoPaterno;
            this.apellidoMaternoPersonal = u_p_apellidoMaterno;
            this.nombreTipoDocumento = u_p_nombreTipoDocumento;
            this.numeroDocumentoPersonal = u_p_numeroDocumento;
        }
    }
}
