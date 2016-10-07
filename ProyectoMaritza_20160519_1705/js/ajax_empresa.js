$(document).ready(function () {
    desactivarTextBox();
    $(document).on("click", ".linkin_eliminar", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        if (confirm('¿Esta seguro de Eliminar este Registro?')) {
            rucEmpresa = $(this).parent().parent().find('td:eq(0)').html();
            var parametros = new Object();
            var fila = $(this);
            parametros.rucEmpresa = rucEmpresa;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfEmpresa.aspx/Delete_Empresa',
                data: JSON.stringify(parametros),
                dataType: "json",
                async: true,
                success: function (data) {
                    var objeto = JSON.parse(data.d);
                    if (objeto.msg_error == "Eliminado con éxito") {
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
        rucEmpresa = $(this).parent().parent().find('td:eq(0)').html();
        nombreEmpresa= $(this).parent().parent().find('td:eq(1)').html();
        direccionEmpresa = $(this).parent().parent().find('td:eq(2)').html();
        descripcionEmpresa = $(this).parent().parent().find('td:eq(3)').html();

        $("#ContentPlaceHolder1_txtRucEmpresa").val(rucEmpresa);
        $("#ContentPlaceHolder1_txtNombreEmpresa").val(nombreEmpresa);
        $("#ContentPlaceHolder1_txtDireccionEmpresa").val(direccionEmpresa);
        $("#ContentPlaceHolder1_txtDescripcionEmpresa").val(descripcionEmpresa);
        $("#ContentPlaceHolder1_hidden_transac").val("E");
        $("#ContentPlaceHolder1_txtRucEmpresa").prop('disabled', true);
        $("#ContentPlaceHolder1_txtNombreEmpresa").prop('disabled', false);
        $("#ContentPlaceHolder1_txtDireccionEmpresa").prop('disabled', false);
        $("#ContentPlaceHolder1_txtDescripcionEmpresa").prop('disabled', false);

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
            if ($("#ContentPlaceHolder1_txtRucEmpresa").val() == "") acepto += "Falta completar el campo ruc Empresa</br>";
            if ($("#ContentPlaceHolder1_txtNombreEmpresa").val() == "") acepto += "Falta completar el campo nombre Empresa</br>";
            if (!$(":text#ContentPlaceHolder1_txtNombreEmpresa").attr("value").match(/^[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ\s]+$/)) acepto += "*El nombre empresa no tiene formato correcto</br>";
            if ($("#ContentPlaceHolder1_txtDireccionEmpresa").val() == "") acepto += "Falta completar el campo direccion Empresa</br>";
            if (!$(":text#ContentPlaceHolder1_txtDireccionEmpresa").attr("value").match(/^[0-9a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ.\s]+$/)) acepto += "*El direccion empresa no tiene formato correcto</br>";
            if ($("#ContentPlaceHolder1_txtDescripcionEmpresa").val() == "") acepto += "Falta completar el campo descripcion Empresa</br>";
            if (!$(":text#ContentPlaceHolder1_txtDescripcionEmpresa").attr("value").match(/^[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ\s]+$/)) acepto += "*El descripcion empresa no tiene formato correcto</br>";
            if ($("#ContentPlaceHolder1_txtRucEmpresa").val().length != 11) acepto += "*Ruc Empresa solo debe tener 11 números</br>";
            if ($("#ContentPlaceHolder1_txtNombreEmpresa").val().length > 40) acepto += "*Nombre Empresa solo acepta como máximo 40 letras</br>";
            if ($("#ContentPlaceHolder1_txtDireccionEmpresa").val().length > 40) acepto += "*Dirección Empresa solo acepta como máximo 40 letras</br>";
            if ($("#ContentPlaceHolder1_txtDescripcionEmpresa").val().length > 40) acepto += "*Descripción Distrito solo acepta como máximo 40 letras</br>";
            if (isNaN($("#ContentPlaceHolder1_txtRucEmpresa").val().trim())) acepto += "*Ruc Empresa no tiene el formato correcto</br>";
            if (acepto == "") {
                var parametros = new Object();
                parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
                parametros.rucEmpresa = $("#ContentPlaceHolder1_txtRucEmpresa").val().trim();
                parametros.nombreEmpresa = $("#ContentPlaceHolder1_txtNombreEmpresa").val().trim();
                parametros.direccionEmpresa = $("#ContentPlaceHolder1_txtDireccionEmpresa").val().trim();
                parametros.descripcionEmpresa = $("#ContentPlaceHolder1_txtDescripcionEmpresa").val().trim();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'WfEmpresa.aspx/Save_Datos_Empresa',
                    data: JSON.stringify(parametros),
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        var objeto = JSON.parse(data.d);
                        if (objeto.msg_error == "Insertado con éxito"&&objeto.nro==1) {
                            //console.log(objeto.oList);
                            //alert(objeto.oList[0]);
                            //alert(objeto.oList[1]);
                            var nuevafila = "";
                            nuevafila = "<tr> ";
                            nuevafila += "<td >" + objeto.new_codigo + "</td>";
                            nuevafila += "<td>" + objeto.oList[1] + "</td>";
                            nuevafila += "<td>" + objeto.oList[2] + "</td>";
                            nuevafila += "<td>" + objeto.oList[3] + "</td>";
                            nuevafila += "<td> " + '<input type="submit" class="linkin btn btn-info" value="Editar"  class="linkin"' + "</td>";
                            nuevafila += "<td> " + '<input type="submit" class="linkin_eliminar btn btn-warning" value="Eliminar" class="linkin_eliminar"' + "</td>";
                            nuevafila += "</tr>";
                            $("#ContentPlaceHolder1_grd_empresa tbody").append(nuevafila);
                            limpiartTextBox();
                            desactivarTextBox();
                            $("#ContentPlaceHolder1_lblErrores").html("Insertado con éxito");
                            if ($("#ContentPlaceHolder1_grd_empresa tbody tr").length == 0) location.reload();
                        }
                        else if (objeto.msg_error == "Editado con éxito") {
                            $("#ContentPlaceHolder1_grd_empresa tbody tr").find('td:eq(0)').each(function () {

                                //obtenemos el valor de la celda
                                valor = $(this).html();
                                if (parseInt(valor) == parseInt($("#ContentPlaceHolder1_txtRucEmpresa").val())) {
                                    $(this).parent().find('td:eq(1)').html($("#ContentPlaceHolder1_txtNombreEmpresa").val());
                                    $(this).parent().find('td:eq(2)').html($("#ContentPlaceHolder1_txtDireccionEmpresa").val());
                                    $(this).parent().find('td:eq(3)').html($("#ContentPlaceHolder1_txtDescripcionEmpresa").val());
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
        $("#ContentPlaceHolder1_txtRucEmpresa").val("");
        $("#ContentPlaceHolder1_txtNombreEmpresa").val("");
        $("#ContentPlaceHolder1_txtDireccionEmpresa").val("");
        $("#ContentPlaceHolder1_txtDescripcionEmpresa").val("");
    }

    function desactivarTextBox() {
        $("#ContentPlaceHolder1_txtRucEmpresa").prop('disabled', true);
        $("#ContentPlaceHolder1_txtNombreEmpresa").prop('disabled', true);
        $("#ContentPlaceHolder1_txtDireccionEmpresa").prop('disabled', true);
        $("#ContentPlaceHolder1_txtDescripcionEmpresa").prop('disabled', true);
    }

    function activarTextBox() {
        $("#ContentPlaceHolder1_txtRucEmpresa").prop('disabled', false);
        $("#ContentPlaceHolder1_txtNombreEmpresa").prop('disabled', false);
        $("#ContentPlaceHolder1_txtDireccionEmpresa").prop('disabled', false);
        $("#ContentPlaceHolder1_txtDescripcionEmpresa").prop('disabled', false);
    }


    $("#ContentPlaceHolder1_txtRucEmpresa").keydown(function (event) {
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