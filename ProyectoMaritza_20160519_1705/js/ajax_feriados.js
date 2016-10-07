$(document).ready(function () {
    var idFeriado = 0;
    desactivarTextBox();

    $(document).on("click", ".linkin_eliminar", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        if (confirm('¿Esta seguro de Eliminar este Registro?')) {
            idFeriado = $(this).parent().parent().find('td:eq(0)').html();
            var parametros = new Object();
            var fila = $(this);
            parametros.idFeriado = idFeriado;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfFeriados.aspx/Delete_Feriados',
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
        idFeriado = $(this).parent().parent().find('td:eq(0)').html();
        fechaFeriado = $(this).parent().parent().find('td:eq(1)').html();
        var fecha = fechaFeriado.split('/');
        var fechaTotal = fecha[2].trim() + '-' + fecha[1].trim() + '-' + fecha[0].trim();
        $("#txtFechaFeriado").val(fechaTotal);
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
            if ($("#txtFechaFeriado").val().trim() == "") acepto += "*Falta completar el campo Fecha Feriado</br>";
            if (acepto == "") {
                var parametros = new Object();
                parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
                parametros.idFeriado = idFeriado;
                parametros.fechaFeriado = $("#txtFechaFeriado").val();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'WfFeriados.aspx/Save_Datos_Feriados',
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
                            nuevafila += "<td> " + '<input type="submit" class="linkin btn btn-info" value="Editar"  ' + "</td>";
                            nuevafila += "<td> " + '<input type="submit" class="linkin_eliminar btn btn-warning" value="Eliminar"' + "</td>";
                            nuevafila += "</tr>";
                            $("#ContentPlaceHolder1_grd_feriados tbody").append(nuevafila);
                            limpiartTextBox();
                            desactivarTextBox();
                            $("#ContentPlaceHolder1_lblErrores").html("Insertado con éxito");
                            if ($("#ContentPlaceHolder1_grd_feriados tbody tr").length == 0) location.reload();
                        }
                        else if (objeto.msg_error == "Editado con éxito") {
                            $("#ContentPlaceHolder1_grd_feriados tbody tr").find('td:eq(0)').each(function () {

                                //obtenemos el valor de la celda
                                valor = $(this).html();
                                if (parseInt(valor) == parseInt(idFeriado)) {
                                    var fechita = $("#txtFechaFeriado").val();
                                    var fecha = fechita.split('-');
                                    var fechaTotal = fecha[2].trim() + '/' + fecha[1].trim() + '/' + fecha[0].trim();
                                    $(this).parent().find('td:eq(1)').html(fechaTotal);
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
        $("#txtFechaFeriado").val("");
    }

    function desactivarTextBox() {
        $("#txtFechaFeriado").prop('disabled', true);
    }

    function activarTextBox() {
        $("#txtFechaFeriado").prop('disabled', false);
    }

});