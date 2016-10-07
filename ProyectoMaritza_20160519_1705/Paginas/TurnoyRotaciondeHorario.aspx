<%@ Page Title="" Language="C#" MasterPageFile="~/Plantilla.Master" AutoEventWireup="true" CodeBehind="TurnoyRotaciondeHorario.aspx.cs" Inherits="ProyectoMaritza_20160519_1705.Paginas.PersonalHorario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >

    <section action="" method="get" class="vista_1 form-horizontal visible">
	    <fieldset>
            <legend class="Personal">Programacion de Horarios, Turno y Local</legend>

	    <div class="form-group has-success">
                <input type="hidden" id="personal_id" />
	         <label class="control-label col-xs-2 " for="personal">Ingrese DNI :</label>
		     <div class="col-xs-6 ">
			     <input class="form-control" id="dniPersonal" type="text" maxlength="8" placeholder="ingrese el dni"/> 
	    	 </div>

            <div class="col-xs-4 ">
                <button class="btn btn-primary" id="buscar_dni">Buscar</button>
            </div>
            <label class="valida_campos"></label>
	    </div>
       <div class="table-responsive">
        <table id="tabla_horario_personal" class="table table-striped table-bordered table-hover table-condensed">
            <thead  class="danger">
			     <th>DNI</th>
                 <th>Sucursal</th>
                 <th>Horario Inicio</th>
                 <th>Horario Fin</th>
                 <th>Fecha Inicio</th>
                 <th>Fecha Fin</th>
                 <th>Descripción</th>
            </thead>
            <tbody>
            </tbody>
        </table>
     </div>
           <!-- <div class="col-xs-12 "><a href="Diseño.aspx" class=" btn btn-block btn-danger"> Salir</a></div>-->
   </fieldset>
 </section>

  <section action="" method="get" class="vista_2 form-horizontal oculta">
    <fieldset>
        <legend>MODIFICAR HORARIO DE TRABAJO</legend>       
          <div class="col-md-3">
              <input type="text" class="form-control" id="dni_oculto_personal" disabled />
          </div><br />
	     <div class="form-group has-success">
             <div class="col-xs-12">
                <div class="col-md-6">
                    <input  type="hidden" id="horario_id_actual"/>
                    <label class="">Horario inicial :</label>
                    <input type="time" id="horario_inicial" />
                 </div> 
                 <div class="col-md-6">
                     <label class="">Horario final :</label>
                    <input type="time" id="horario_final" />
                 </div>
                 <label class="valida_hora"></label>
             </div>
             <br />
             <br />
             <br />
             <div class="col-xs-12">
                 <section class="col-md-6">
                     <label>fecha inicio : </label>
                     <input type="date" id="fecha_inicio" value="<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd")); %>" min="<%Response.Write(DateTime.Now.ToString("yyyy-MM-dd")); %>" />
                 </section>
                 <section class="col-md-6">
                     <label>fecha Final : </label>
                     <input type="date" id="fecha_final" value="<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd")); %>" min="<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd")); %>" />
                 </section>
                 <label class="valida_fecha"></label>
             </div>
             <br />
             <br />
             <br />
             <div class="col-xs-12">
                <div class="col-md-6">
                    <label class="">Descipcion :</label>
                    <input class="form-control"type="text" id="descripcion_horario" placeholder="descripcion"/>
                 </div> 
             </div>
	     </div>

         <div class="form-group has-success">
            <div class="col-xs-4 col-sm-offset-2">
                <a href="TurnoyRotaciondeHorario.aspx" class="btn btn-primary" id="insertar"> Guardar</a>
            </div>
            <div class="col-xs-4 col-sm-offset-2">
                <a href="TurnoyRotaciondeHorario.aspx" class="btn btn-danger" id="cancelar"> Cancelar</a>
            </div>
	    </div>

	     </fieldset>
    </section>
</asp:Content>
