$(document).ready(function () {
    var IdRemuneracion = 0;
    desactivarTextBox();

    $(document).on("click", ".linkin_eliminar", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        if (confirm('¿Esta seguro de Eliminar este Registro?')) {
            IdRemuneracion = $(this).parent().parent().find('td:eq(0)').html();
            var parametros = new Object();
            var fila = $(this);
            parametros.IdRemuneracion = IdRemuneracion;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfRemuneracion.aspx/Delete_Remuneracion',
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
        IdRemuneracion = $(this).parent().parent().find('td:eq(0)').html();
        nombreRemuneracion = $(this).parent().parent().find('td:eq(1)').html();
        montoRemuneracion = $(this).parent().parent().find('td:eq(2)').html();
        $("#ContentPlaceHolder1_txtNombreRemuneracion").val(nombreRemuneracion);
        $("#ContentPlaceHolder1_txtMontoRemuneracion").val(montoRemuneracion);
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
            if ($("#ContentPlaceHolder1_txtNombreRemuneracion").val().trim() == "") acepto += "*Falta completar el campo nombre Remuneracion</br>";
            if (!$(":text#ContentPlaceHolder1_txtNombreRemuneracion").attr("value").match(/^[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ\s]+$/)) acepto += "*El nombre remuneracion no tiene formato correcto</br>";
            if ($("#ContentPlaceHolder1_txtNombreRemuneracion").val().length > 30) acepto += "*Nombre Remuneracion solo acepta como máximo 30 letras</br>";
            if ($("#ContentPlaceHolder1_txtMontoRemuneracion").val().trim() == "") acepto += "*Falta completar el campo monto remuneración</br>";
            if ($("#ContentPlaceHolder1_txtMontoRemuneracion").val().length > 10) acepto += "*Monto Remuneracion solo acepta como máximo 10 caracteres</br>";
            if (!$(":text#ContentPlaceHolder1_txtMontoRemuneracion").attr("value").match(/^(?:[1-9]\d*|0)?(?:\.\d+)?$/)) acepto += "*El formato de monto remuneracion no es el correcto </br>"
            if (parseInt($("#ContentPlaceHolder1_txtMontoRemuneracion").val().trim()) > 9999999) acepto += "*El monto remuneracion debe ser menor a 9999999.999</br>";
            if (acepto == "") {
                var parametros = new Object();
                parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
                parametros.IdRemuneracion = IdRemuneracion;
                parametros.nombreRemuneracion = $("#ContentPlaceHolder1_txtNombreRemuneracion").val().trim();
                parametros.montoRemuneracion = $("#ContentPlaceHolder1_txtMontoRemuneracion").val().trim();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'WfRemuneracion.aspx/Save_Datos_Remuneracion',
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
                            $("#ContentPlaceHolder1_grd_remuneracion tbody").append(nuevafila);
                            limpiartTextBox();
                            desactivarTextBox();
                            $("#ContentPlaceHolder1_lblErrores").html("Insertado con éxito");
                            if ($("#ContentPlaceHolder1_grd_remuneracion tbody tr").length == 0) location.reload();
                        }
                        else if (objeto.msg_error == "Editado con éxito") {
                            $("#ContentPlaceHolder1_grd_remuneracion tbody tr").find('td:eq(0)').each(function () {

                                //obtenemos el valor de la celda
                                valor = $(this).html();
                                if (parseInt(valor) == parseInt(IdRemuneracion)) {
                                    $(this).parent().find('td:eq(1)').html($("#ContentPlaceHolder1_txtNombreRemuneracion").val());
                                    var remunera = parseFloat($("#ContentPlaceHolder1_txtMontoRemuneracion").val());
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
        $("#ContentPlaceHolder1_txtNombreRemuneracion").val("");
        $("#ContentPlaceHolder1_txtMontoRemuneracion").val("");
    }

    function desactivarTextBox() {
        $("#ContentPlaceHolder1_txtNombreRemuneracion").prop('disabled', true);
        $("#ContentPlaceHolder1_txtMontoRemuneracion").prop('disabled', true);
    }

    function activarTextBox() {
        $("#ContentPlaceHolder1_txtNombreRemuneracion").prop('disabled', false);
        $("#ContentPlaceHolder1_txtMontoRemuneracion").prop('disabled', false);
    }

});