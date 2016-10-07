<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="WfEmpleados.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.WfEmpleados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="registrar_empresa_1 empresa_mari box box-warning container visible animated bounce">
        <header>
            <h3>Listado de los empleados</h3>
        </header>
        <table id="lista_empleados" class="table table-bordered table-striped">
            <thead>
                <tr>
                <th>Nº</th>
                <th>Nombre</th>
                <th>Area</th>
                <th>Nº de documento</th>
                <th>Salario</th>
                <th colspan="2"><center>Opciones</center></th>
                    </tr>
            </thead>
            <tbody>
                 <%
                    System.Data.DataTable listar_personal_empresa = listar_personal();
                    int cnt = 0;
                    foreach (System.Data.DataRow row in listar_personal_empresa.Rows)
                    {
                        cnt++;
                        %>
                            <tr>
                                <!--Peronsal id-->
                                <td style="display:none;"><% Response.Write(row[0].ToString()+""); %></td>
                                <!--Apellido Paterno personal-->
                                <td style="display:none;"><% Response.Write(row[2].ToString()+""); %></td>
                                <!--Apellido materno personal-->
                                <td style="display:none;"><% Response.Write(row[1].ToString()+""); %></td>
                                <!--Solo nombre personal-->
                                <td style="display:none;"><% Response.Write(row[3].ToString()+""); %></td>
                                <td><% Response.Write(cnt); %></td>
                                <!--Nombres del personal-->
                                <td><% Response.Write(row[2].ToString()+" "+ row[1].ToString() + " " + row[3].ToString()); %></td>
                                <!--Sucursal nombre-->
                                <td style="display:none;"><% Response.Write(row[4].ToString()+""); %></td>
                                <!--Sucursal id-->
                                <td style="display:none;"><% Response.Write(row[5].ToString()+""); %></td>
                                <!--Area nombre-->
                                <td><% Response.Write(row[6].ToString()+""); %></td>
                                <!--Area id-->
                                <td style="display:none;"><% Response.Write(row[7].ToString()+""); %></td>
                                <!--Tipo de documento nombre-->
                                <td style="display:none"><% Response.Write(row[8].ToString()+""); %></td>
                                <!--Tipo de documento id-->
                                <td style="display:none"><% Response.Write(row[9].ToString()+""); %></td>
                                <!--Numero del docuemnto personal-->
                                <td><% Response.Write(row[10].ToString()+""); %></td>
                                <!--Persona direccion-->
                                <td style="display:none;"><% Response.Write(row[11].ToString()+""); %></td>
                                <!--Persona telefono-->
                                <td style="display:none;"><% Response.Write(row[12].ToString()+""); %></td>
                                <!--Fecha de nacimiento-->
                                <td style="display:none;"><% Response.Write(row[13].ToString()+""); %></td>
                                <!--Nivel de institucion id-->
                                <td style="display:none;"><% Response.Write(row[14].ToString()+""); %></td>
                                <!--Nivel de institucion nombre-->
                                <td style="display:none;"><% Response.Write(row[15].ToString()+""); %></td>
                                <!--Fecha de ingreso del personal-->
                                <td style="display:none;"><% Response.Write(row[16].ToString()+""); %></td>
                                <!--cargo id-->
                                <td style="display:none;"><% Response.Write(row[17].ToString()+""); %></td>
                                <!--cargo nombre-->
                                <td style="display:none;"><% Response.Write(row[18].ToString()+""); %></td>
                                <!--cargo monto-->
                                <td><% Response.Write(row[19].ToString()+""); %></td>
                                <!--Personal Distrito id -->
                                <td style="display:none;"><% Response.Write(row[20].ToString()+""); %></td>
                                <!--Personal distrito nombre-->
                                <td style="display:none;"><% Response.Write(row[21].ToString()+""); %></td>
                                <!--Personal estado civil-->
                                <td style="display:none;"><% Response.Write(row[22].ToString()+""); %></td>
                                <!--Personal sexo-->
                                <td style="display:none;"><% Response.Write(row[23].ToString()+""); %></td>
                                <!--Personal planilla-->
                                <td style="display:none;"><% Response.Write(row[24].ToString()+""); %></td>
                                <!--Provincia id-->
                                <td style="display:none;"><% Response.Write(row[25].ToString()+""); %></td>
                                <!--Provincia nombre-->
                                <td style="display:none;"><% Response.Write(row[26].ToString()+""); %></td>
                                <!--departamento id-->
                                <td style="display:none;"><% Response.Write(row[27].ToString()+""); %></td>
                                <!--departamento nombre-->
                                <td style="display:none;"><% Response.Write(row[28].ToString()+""); %></td>
                                <td> <a  href="javascript:;" class="editar_personal badge bg-blue" >Editar</a></td>
                                <td> <a  href="javascript:;" class="eliminar_personal badge bg-red">Eliminar</a></td>
                            </tr>
                        <%
                    }
                %>
            </tbody>
        </table>
        <section class="form-group col-sm-6 col-md-6">
            <input  type="button" class="btn btn-info btn-lg  siguiente" value="Desea ingresar un nuevo empleado"/>
        </section>
    </section>
    <section class="registrar_empresa_2 empresa_mari visible box box-info container ocultar animated">
        <header class="panel-heading">
            <h3>
                <center>Datos Personales</center>
            </h3>
        </header>
        <section class="form-group col-sm-6 col-md-6">
            
            <input type="hidden" id="personal_id"/>
            <label>Apellido Paterno</label>
            <input type="text" id="empleado_apellido_paterno" class="form-control" value="" />
            <div class="valida_ap_paterno"></div>
        </section>

        <section class="form-group col-sm-6 col-md-6">
            <label>Apellido Materno</label>
            <input type="text" id="empleado_apellido_materno" class="form-control" value="" />
            <div class="valida_ap_materno"></div>
        </section>

        <section class="form-group col-sm-6 col-md-6">
            <label>Nombre Completo</label>
            <input type="text" class="form-control" id="empleado_nombre" value="" />
            <div class="valida_pe_nombre"></div>
        </section>

       <section class="form-group col-sm-6 col-md-6">
            <label>Fecha de nacimiento</label>
            <input type="date" id="empleado_fecha_nacimiento"  value="<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd")); %>" class="form-control" min="<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd")); %>" />

        </section>
        <section  class="form-group col-sm-6 col-md-6">
                <label>Género</label>
                <section>
                    <input type="radio" value="1" id="radio1" name="radio_genero" checked/> 
                    <label for="radio1">Masculino</label>
                    <input type="radio" value="2" id="radio2" name="radio_genero" class="minimal-red" /> 
                    <label for="radio2">Femenino</label>
                </section>
                
            </section>

            <section  class="form-group col-sm-6 col-md-6" style="display:flex;  -webkit-flex-direction:row-reverse;"">
                
                <input type="button" class="btn btn-success btn-lg  siguiente siguiente1" value="Siguiente"style="margin:0 5px;" />
                <input type="button"  class="cancelar_registro btn btn-warning btn-lg" value="Cancelar"  />
                
            </section>

        </section>
  

    <!--Estrcutura de la segunda vista-->
    <section class="registrar_empresa_3 empresa_mari box box-danger container animated" >
           <header class="panel-heading">
            <h3>
                <center>Datos Adicionales</center>
            </h3>
        </header>
        <section class="form-group col-sm-6 col-md-6 ">
             <label>Nivel de Escolaridad</label>
                <select id="nivel_escolaridad" class=" form-control">
                        <option value="0">-- Seleccionar grado de estudio -- </option>
                        <%
                            List<Entidad.NivelInstruccion> list_escolaridad = listar_nivel_escolaridad();
                            foreach (var item in list_escolaridad)
                            {
                                 %>
                                     <option value="<%Response.Write(item.IdNivelInstruccion);%>"><%Response.Write(item.nombreNivelInstruccion); %></option>
                                 <%
                            }
                        %>
                    </select>
                <div class="valida_p_escolaridad"></div>
</section>
                <section class="form-group col-sm-6 col-md-6">
                    <label>Teléfono</label>
                    <input type="text" id="telefono_personal" class="form-control" />
                    <div class="valida_p_telefono_personal"></div>
                </section>
         
            <section  class="form-group col-sm-6 col-md-6">
                <label>Estado Civil</label>
                <select class="form-control" id="estado_civil">
                    <option value="Soltero">Soltero</option>
                    <option value="Casado">Casado</option>
                    <option value="Viudo">Viudo</option>
                </select>
                </section>
        <section></section>
                <section class="form-group col-sm-6 col-md-6">
                    <label>Tipo de Documento</label>
                    <select id="tipo_de_documento" class=" form-control">
                        <option value="0" longitud="0">-- Seleccionar tipo de documento -- </option>
                        <%
                            List<Entidad.TipoDocumento> list_tipo_d_d = listar_tipo_de_documento();
                            foreach (var item in list_tipo_d_d)
                            {
                                %>
                                <option value="<%Response.Write(item.IdTipoDocumento);%>" longitud="<%Response.Write(item.longitudTipoDocumento); %>"><%Response.Write(item.nombreTipoDocumento); %></option>
                                <%
                            }
                        %>
                    </select>
                     <div class="valida_p_tipo_documento"></div>
                </section>
                      <section class="form-group col-sm-6 col-md-6">

                        <label>Nº de documento</label>
                        <input type="text" id="numero_documento" class=" form-control" disabled />
                          <div class="valida_p_n_documento"></div>
                    </section>
        <section class="form-group col-sm-6 col-md-6 " style="display:flex;  -webkit-flex-direction:row-reverse; ">
            
            <input type="button" class="siguiente btn btn-success btn-lg" value="Siguiente"  style="margin:0 5px;" />
            <input type="button" class="anterior btn btn-danger btn-lg" value="Anterior" style="margin:0 5px;"/>
            <input type="button"  class="cancelar_registro btn btn-warning btn-lg" value="Cancelar" style="margin:0 5px;" />
        </section>
    </section>
    <!--Estructura de la tercera vista-->
    <section class="registrar_empresa_4 empresa_mari box box-success container animated">
         <header class="panel-heading">
            <h3>
                <center>Ubicación y lugar de Nacimiento del Personal</center>
            </h3>
        </header>
            <section class="form-group col-sm-6 col-md-6">
                <select id="list_departamento" name="" class=" form-control">
                        <option value="0">--Seleccione La empresa--</option>
                        <%
                            List<Entidad.Departamento> lis_departamentos = listar_departamentos();
                            foreach (var item in lis_departamentos)
                            {
                                %> 
                                    <option value="<%Response.Write(item.IdDepartamento); %>"><%Response.Write(item.nombreDepartamento); %></option>
                                <% 
                            }
                        %>
                </select>
                <div class="valida_departamento_personal"></div>
             </section>
            <section class="form-group col-sm-6 col-md-6">
                
                <select id="provincia" name="" class=" form-control">
                        <option value="0">--Seleccione Provincia--</option>
                </select>
                <div class="valida_provincia_personal"></div>
            </section>
            <section class="form-group col-sm-6 col-md-6">
                <input  type="hidden" id="distrito_id" value="0"/>
                <label>Buscar el distrito</label>
                <input type="text" id="filtrar_distrito" class=" form-control" placeholder="ingrese el distrito" />
                <div class="valida_p_distrito"></div>
            </section>
  
      
            <section class="form-group col-sm-6 col-md-6" >
                <label>Ingrese la dirección</label>
                <input type="text" id="direccion_personal" class=" form-control" /> 
                <div class="valida_p_direc_personal"></div>
            </section>

        <section class="form-group col-sm-6 col-md-6  col-md-offset-6"  style="display:flex;  -webkit-flex-direction:row-reverse; "" >
           
            <input type="button" class="siguiente btn btn-success btn-lg" value="Siguiente" style="margin:0 5px;" />
             <input type="button" class="anterior btn btn-danger btn-lg" value="Anterior" style="margin:0 5px;"  />
            <input type="button"  class="cancelar_registro btn btn-warning btn-lg" value="Cancelar" style="margin:0 5px;" />
        </section>

    </section>

    <!--Estructura de la cuarta vista-->


    <!--Estructura de la quinta vista-->
    <section class="registrar_empresa_5 empresa_mari box box-warning container animated">
        <header class="panel-heading">
            <h3>
                <center>Empleado dispocisión de la empresa</center>
            </h3>
        </header>
        <section class="form-group col-sm-6 col-md-6">
            <label>Seleccione el Cargo</label>
            <select id="cargo_empresa" name="" class=" form-control">
                    <option value="0">--Seleccione el Cargo--</option>
                    <%
                        List<Entidad.Cargo> lis_cargos = Listar_cargos();
                        foreach (var item in lis_cargos)
                        {
                            %> 
                                <option valorCargo="<%Response.Write(item.montoSalarioCargo); %>" value="<%Response.Write(item.IdCargo); %>"><%Response.Write(item.nombreCargo); %></option>
                            <% 
                        }
                    %>
            </select>
            <div class="valida_cargo"></div>
        </section>
        <!--<section  class="form-group col-sm-6 col-md-6">
            <input type="hidden" id="salario_id"/>
            <label>Salario : </label>
            <input type="text" id="salario_personal" class=" form-control" />
             <div class="valida_p_sal_personal"></div>
        </section>-->
        <section class="form-group col-sm-6 col-md-6">
            <label>Precio por cargo : </label>
            <input type="text" id="precio_cargo" class="form-control" disabled/>
            <div class="valida_p_precio_cargo"></div>
        </section>
        <!--<section class="form-group col-sm-6 col-md-6">
            <label>Remuneración extra : </label>
            <input type="text" id="remuneraciones_extras" class="form-control" disabled/>
            <div class="valida_p_remune_extras"></div>
        </section>-->
        <section class="form-group col-sm-6 col-md-6">
            <label>Seleccione el área</label>
            <select id="area_empresa" name="" class=" form-control">
                    <option value="0">--Seleccione el área--</option>
                    <%
                        List<Entidad.Area> list_area_vista = listar_area();
                        foreach (var item in list_area_vista)
                        {
                            %> 
                                <option value="<%Response.Write(item.IdArea); %>"><%Response.Write(item.nombreArea); %></option>
                            <% 
                        }
                    %>
            </select>
            <div class="valida_p_ar_empresa"></div>
        </section>
        <section class="form-group col-sm-6 col-md-6">
            <label>fecha de ingreso a la empresa : </label>
            <input type="date" id="fecha_ingreso_empresa" class="form-control" value="<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd")); %>"/>
        </section>

          <section class="form-group col-sm-6 col-md-6">
                <label>Nombre de la sucursal</label>
                <select id="listar_sucursales" name="" class=" form-control">

                    <option value="0">--Seleccione la sucursal --</option>
                    <%
                        List<Entidad.Sucursal> listar_sucursal = listar_sucursal_empresa();
                        foreach (var item in listar_sucursal)
                        {
                             %>
                                <option value="<%Response.Write(item.IdSucursal); %>"><%Response.Write(item.nombreSucursal); %></option>
                            <%
                        }
                    %>
                </select>
            <div class="valida_p_sucursales"></div>
            <div class="valida_si_existe_dni"></div>
        </section>
         <section class="form-group col-sm-6 col-md-6">
                    <label>Planilla</label>
              <section>
                    <input type="radio" value="1" name="radio_planilla" class="minimal-red" checked /> SI
                    <input type="radio" value="2" name="radio_planilla" class="minimal-red" />  NO
              </section>
         </section>
        
            <section class="form-group col-sm-6 col-md-6"  style="display:flex;  -webkit-flex-direction:row-reverse;" >
                <input type="button" class="anterior btn btn-danger btn-lg" value="Anterior" />
            </section>
        <section>
        <section class="box-body pad table-responsive">
            <table class="table table-bordered text-center">
                <tr class="">

                    <td>
                        <input type="button" id="insertar" class="btn btn-primary btn-lg" value="Guardar" />
                    </td>

                    <td>
                        <input type="button" class="cancelar_registro btn btn-warning btn-lg" value="Cancelar" />
                    </td>
                </tr>
            </table>
        </section>
    </section>
    
    <!--Estrucrura vista 6-->
</asp:Content>
