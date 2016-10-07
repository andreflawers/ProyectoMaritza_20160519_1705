$(document).ready(function () {
    var IdTipoPermiso = 0;
    desactivarTextBox();

    $(document).on("click", ".linkin_eliminar", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        if (confirm('¿Esta seguro de Eliminar este Registro?')) {
            IdTipoPermiso = $(this).parent().parent().find('td:eq(0)').html();
            var parametros = new Object();
            var fila = $(this);
            parametros.IdTipoPermiso = IdTipoPermiso;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfTipoPermiso.aspx/Delete_TipoPermiso',
                data: JSON.stringify(parametros),
                dataType: "json",
                async: true,
                success: function (data) {
                    var objeto = JSON.parse(data.d);
                    if (objeto.msg_error == "Eliminado con éxito") {
                        //alert(objeto.msg_error);
                        var parent = fila.parent().parent();
                        $(parent).remove();
                        $("#ContentPlaceHolder1_lblErrores").html("Eliminado con éxito");
                    }
                    else $("#ContentPlaceHolder1_lblErrores").html(objeto.msg_error);

                },
                error: function (data) {
                    console.info('error');
                }
            });
        }
    });

    $(document).on("click", ".linkin", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        IdTipoPermiso = $(this).parent().parent().find('td:eq(0)').html();
        nombreTipoPermiso = $(this).parent().parent().find('td:eq(1)').html();
        remuneracionTipoPermiso = $(this).parent().parent().find('td:eq(2)').html();
        $("#ContentPlaceHolder1_txtNombreTipoPermiso").val(nombreTipoPermiso);
        $("#ContentPlaceHolder1_txtRemuneracion").val(remuneracionTipoPermiso);
        $("#ContentPlaceHolder1_hidden_transac").val("E");
        activarTextBox();
    });

    $(document).on("click", "#ContentPlaceHolder1_btnRegistra", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        activarTextBox();
        $("#ContentPlaceHolder1_hidden_transac").val("N");
    });

    $(document).on("click", "#ContentPlaceHolder1_btnCancelar", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        $("#ContentPlaceHolder1_hidden_transac").val("N");
        limpiartTextBox();
        desactivarTextBox();
    });


    $(document).on("click", "#ContentPlaceHolder1_btnGuardar", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        if (confirm('¿Esta seguro desea guardar/editar?')) {
            var acepto = "";
            if ($("#ContentPlaceHolder1_txtNombreTipoPermiso").val().trim() == "") acepto += "*Falta completar el campo nombre Tipo Permiso</br>";
            if (!$(":text#ContentPlaceHolder1_txtNombreTipoPermiso").attr("value").match(/^[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ\s]+$/)) acepto += "*El nombre tipo permiso no tiene formato correcto</br>";
            if ($("#ContentPlaceHolder1_txtNombreTipoPermiso").val().length > 50) acepto += "*Nombre Tipo Permiso solo acepta como máximo 50 letras</br>";
            if ($("#ContentPlaceHolder1_txtRemuneracion").val().trim() == "") acepto += "*Falta completar el campo remuneración</br>";
            if ($("#ContentPlaceHolder1_txtRemuneracion").val().length > 11) acepto += "*Remuneracion solo acepta como máximo 10 caracteres</br>";
            if (!$(":text#ContentPlaceHolder1_txtRemuneracion").attr("value").match(/^(?:[1-9]\d*|0)?(?:\.\d+)?$/)) acepto += "*El formato de remuneracion no es el correcto </br>";
            if (parseInt($("#ContentPlaceHolder1_txtRemuneracion").val().trim()) > 9999999) acepto += "*El monto remuneracion debe ser menor a 9999999.999</br>";
            //alert($(":text#ContentPlaceHolder1_txtRemuneracion").attr("value"));
            if (acepto == "") {
                var parametros = new Object();
                parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
                parametros.IdTipoPermiso = IdTipoPermiso;
                parametros.nombreTipoPermiso = $("#ContentPlaceHolder1_txtNombreTipoPermiso").val().trim();
                parametros.remuneracionTipoPermiso = $("#ContentPlaceHolder1_txtRemuneracion").val().trim();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'WfTipoPermiso.aspx/Save_Datos_TipoPermiso',
                    data: JSON.stringify(parametros),
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        var objeto = JSON.parse(data.d);
                        if (objeto.msg_error == "Insertado con éxito") {
                            var nuevafila = "";
                            nuevafila = "<tr> ";
                            nuevafila += "<td >" + objeto.new_codigo + "</td>";
                            nuevafila += "<td>" + objeto.oList[1] + "</td>";
                            nuevafila += "<td>" + objeto.oList[2] + "</td>";
                            nuevafila += "<td> " + '<input type="submit" class="linkin btn btn-info" value="Editar"  ' + "</td>";
                            nuevafila += "<td> " + '<input type="submit" class="linkin_eliminar btn btn-warning" value="Eliminar"' + "</td>";
                            nuevafila += "</tr>";
                            $("#ContentPlaceHolder1_grd_tipoPermiso tbody").append(nuevafila);
                            limpiartTextBox();
                            desactivarTextBox();
                            $("#ContentPlaceHolder1_lblErrores").html("Insertado con éxito");
                            if ($("#ContentPlaceHolder1_grd_tipoPermiso tbody tr").length == 0) location.reload();
                        }
                        else if (objeto.msg_error == "Editado con éxito") {
                            $("#ContentPlaceHolder1_grd_tipoPermiso tbody tr").find('td:eq(0)').each(function () {

                                //obtenemos el valor de la celda
                                valor = $(this).html();
                                if (parseInt(valor) == parseInt(IdTipoPermiso)) {
                                    $(this).parent().find('td:eq(1)').html($("#ContentPlaceHolder1_txtNombreTipoPermiso").val());
                                    var remunera = parseFloat($("#ContentPlaceHolder1_txtRemuneracion").val());
                                    //$(this).parent().find('td:eq(2)').html($("#ContentPlaceHolder1_txtRemuneracion").val());
                                    $(this).parent().find('td:eq(2)').html(remunera.toFixed(3));
                                }

                            })
                            limpiartTextBox();
                            desactivarTextBox();
                            $("#ContentPlaceHolder1_lblErrores").html("Editado con éxito");
                        }
                        else $("#ContentPlaceHolder1_lblErrores").html("Error!!!!.... No se pudo completar la acción");

                    },
                    error: function (data) {
                        console.info('error');
                    }
                });
            }
            else {
                $("#ContentPlaceHolder1_lblErrores").html(acepto);
            }
        }
    });

    function limpiartTextBox() {
        $("#ContentPlaceHolder1_txtNombreTipoPermiso").val("");
        $("#ContentPlaceHolder1_txtRemuneracion").val("");
    }

    function desactivarTextBox() {
        $("#ContentPlaceHolder1_txtNombreTipoPermiso").prop('disabled', true);
        $("#ContentPlaceHolder1_txtRemuneracion").prop('disabled', true);
    }

    function activarTextBox() {
        $("#ContentPlaceHolder1_txtNombreTipoPermiso").prop('disabled', false);
        $("#ContentPlaceHolder1_txtRemuneracion").prop('disabled', false);
    }

});