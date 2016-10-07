$(document).ready(function () {
    var IdTipoDocumento = 0;
    desactivarTextBox();

    $(document).on("click", ".linkin_eliminar", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        if (confirm('¿Esta seguro de Eliminar este Registro?')) {
            IdTipoDocumento = $(this).parent().parent().find('td:eq(0)').html();
            var parametros = new Object();
            var fila = $(this);
            parametros.IdTipoDocumento = IdTipoDocumento;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfTipoDocumento.aspx/Delete_Tipo_Documento',
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
        IdTipoDocumento = $(this).parent().parent().find('td:eq(0)').html();
        nombreTipoDocumento = $(this).parent().parent().find('td:eq(1)').html();
        longitudTipoDocumento = $(this).parent().parent().find('td:eq(2)').html();
        $("#ContentPlaceHolder1_txtNombreTipoDocumento").val(nombreTipoDocumento);
        $("#ContentPlaceHolder1_txtLongitudTipoDocumento").val(longitudTipoDocumento);
        $("#ContentPlaceHolder1_hidden_transac").val("E");
        $("#ContentPlaceHolder1_txtNombreTipoDocumento").prop('disabled', false);
        $("#ContentPlaceHolder1_txtLongitudTipoDocumento").prop('disabled', false);
    });

    $(document).on("click", "#ContentPlaceHolder1_btnRegistraTipoDocumento", function (e) {
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
            if ($("#ContentPlaceHolder1_txtNombreTipoDocumento").val().trim() == "") acepto += "*Falta completar el campo nombre Tipo de Documento</br>";
            if (!$(":text#ContentPlaceHolder1_txtNombreTipoDocumento").attr("value").match(/^[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ\s]+$/)) acepto += "*El nombre tipo Documento no tiene formato correcto</br>";
            if ($("#ContentPlaceHolder1_txtNombreTipoDocumento").val().length > 50) acepto += "*Nombre Tipo Documento solo acepta como máximo 40 letras</br>";
            if ($("#ContentPlaceHolder1_txtLongitudTipoDocumento").val().trim() == "") acepto += "*Falta completar el campo longitud Tipo de Documento</br>";
            if (acepto == "") {
                var parametros = new Object();
                parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
                parametros.IdTipoDocumento = IdTipoDocumento;
                parametros.nombreTipoDocumento = $("#ContentPlaceHolder1_txtNombreTipoDocumento").val().trim();
                parametros.longitudTipoDocumento = $("#ContentPlaceHolder1_txtLongitudTipoDocumento").val().trim();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'WfTipoDocumento.aspx/Save_Datos_Tipo_Documento',
                    data: JSON.stringify(parametros),
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        var objeto = JSON.parse(data.d);
                        if (objeto.msg_error == "Insertado con éxito" && objeto.nro == 1) {
                            var nuevafila = "";
                            nuevafila = "<tr> ";
                            nuevafila += "<td >" + objeto.new_codigo + "</td>";
                            nuevafila += "<td>" + objeto.oList[1] + "</td>";
                            nuevafila += "<td>" + objeto.oList[2] + "</td>";
                            nuevafila += "<td> " + '<input type="submit" class="linkin btn btn-info" value="Editar"  class="linkin"' + "</td>";
                            nuevafila += "<td> " + '<input type="submit" class="linkin_eliminar btn btn-warning" value="Eliminar" class="linkin_eliminar"' + "</td>";
                            nuevafila += "</tr>";
                            $("#ContentPlaceHolder1_grd_tipo_documento tbody").append(nuevafila);
                            limpiartTextBox();
                            desactivarTextBox();
                            $("#ContentPlaceHolder1_lblErrores").html("Insertado con éxito");
                            if ($("#ContentPlaceHolder1_grd_tipo_documento tbody tr").length == 0) location.reload();
                        }
                        else if (objeto.msg_error == "Editado con éxito" && objeto.nro == 1) {
                            $("#ContentPlaceHolder1_grd_tipo_documento tbody tr").find('td:eq(0)').each(function () {

                                //obtenemos el valor de la celda
                                valor = $(this).html();
                                if (parseInt(valor) == parseInt(IdTipoDocumento)) {
                                    $(this).parent().find('td:eq(1)').html($("#ContentPlaceHolder1_txtNombreTipoDocumento").val());
                                    $(this).parent().find('td:eq(2)').html($("#ContentPlaceHolder1_txtLongitudTipoDocumento").val());
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
        $("#ContentPlaceHolder1_txtNombreTipoDocumento").val("");
        $("#ContentPlaceHolder1_txtLongitudTipoDocumento").val("");
    }

    function desactivarTextBox() {
        $("#ContentPlaceHolder1_txtNombreTipoDocumento").prop('disabled', true);
        $("#ContentPlaceHolder1_txtLongitudTipoDocumento").prop('disabled', true);
    }

    function activarTextBox() {
        $("#ContentPlaceHolder1_txtNombreTipoDocumento").prop('disabled', false);
        $("#ContentPlaceHolder1_txtLongitudTipoDocumento").prop('disabled', false);
    }

    $("#ContentPlaceHolder1_txtLongitudTipoDocumento").keydown(function (event) {
        if (event.shiftKey) {
            event.preventDefault();
        }

        if (event.keyCode == 46 || event.keyCode == 8) {
        }
        else {
            if (event.keyCode < 95) {
                if (event.keyCode < 48 || event.keyCode > 57) {
                    event.preventDefault();
                }
            }
            else {
                if (event.keyCode < 96 || event.keyCode > 105) {
                    event.preventDefault();
                }
            }
        }
    });

});