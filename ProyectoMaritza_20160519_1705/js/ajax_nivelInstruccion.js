$(document).ready(function () {
    var IdNivelInstruccion = 0;
    desactivarTextBox();
    $(document).on("click", ".linkin_eliminar", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        if (confirm('¿Esta seguro de Eliminar este Registro?')) {
            IdNivelInstruccion = $(this).parent().parent().find('td:eq(0)').html();
            var parametros = new Object();
            var fila = $(this);
            parametros.IdNivelInstruccion = IdNivelInstruccion;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfNivelInstruccion.aspx/Delete_Nivel_Instruccion',
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
        IdNivelInstruccion = $(this).parent().parent().find('td:eq(0)').html();
        nombreNivelInstruccion = $(this).parent().parent().find('td:eq(1)').html();
        $("#ContentPlaceHolder1_txtNombreNivelInstruccion").val(nombreNivelInstruccion);
        $("#ContentPlaceHolder1_hidden_transac").val("E");
        $("#ContentPlaceHolder1_txtNombreNivelInstruccion").prop('disabled', false);
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
            if ($("#ContentPlaceHolder1_txtNombreNivelInstruccion").val() == "") acepto += "*Falta completar el campo nombre Nivel Instruccion</br>";
            if (!$(":text#ContentPlaceHolder1_txtNombreNivelInstruccion").attr("value").match(/^[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ\s]+$/)) acepto += "*El nombre nivel instrucción no tiene formato correcto</br>";
            if ($("#ContentPlaceHolder1_txtNombreNivelInstruccion").val().length > 40) acepto += "*Nivel Instruccion solo acepta como máximo 40 letras</br>";
            if (acepto == "") {
                var parametros = new Object();
                parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
                parametros.IdNivelInstruccion = IdNivelInstruccion;
                parametros.nombreNivelInstruccion = $("#ContentPlaceHolder1_txtNombreNivelInstruccion").val().trim();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'WfNivelInstruccion.aspx/Save_Datos_Nivel_Instruccion',
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
                            nuevafila += "<td> " + '<input type="submit" class="linkin btn btn-info" value="Editar"  class="linkin"' + "</td>";
                            nuevafila += "<td> " + '<input type="submit" class="linkin_eliminar btn btn-warning" value="Eliminar" class="linkin_eliminar"' + "</td>";
                            nuevafila += "</tr>";
                            $("#ContentPlaceHolder1_grd_nivelInstruccion tbody").append(nuevafila);
                            limpiartTextBox();
                            desactivarTextBox();
                            $("#ContentPlaceHolder1_lblErrores").html("Insertado con éxito");
                            if ($("#ContentPlaceHolder1_grd_nivelInstruccion tbody tr").length == 0) location.reload();
                        }
                        else if (objeto.msg_error == "Editado con éxito") {
                            $("#ContentPlaceHolder1_grd_nivelInstruccion tbody tr").find('td:eq(0)').each(function () {

                                //obtenemos el valor de la celda
                                valor = $(this).html();
                                if (parseInt(valor) == parseInt(IdNivelInstruccion)) {
                                    $(this).parent().find('td:eq(1)').html($("#ContentPlaceHolder1_txtNombreNivelInstruccion").val());
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
        $("#ContentPlaceHolder1_txtNombreNivelInstruccion").val("");
    }

    function desactivarTextBox() {
        $("#ContentPlaceHolder1_txtNombreNivelInstruccion").prop('disabled', true);
    }

    function activarTextBox() {
        $("#ContentPlaceHolder1_txtNombreNivelInstruccion").prop('disabled', false);
    }

});