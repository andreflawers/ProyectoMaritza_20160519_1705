$(document).ready(function () {
    var IdCargo = 0;
    desactivarTextBox();
    $(document).on("click", ".linkin_eliminar", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        if (confirm('¿Esta seguro de Eliminar este Registro?')) {
            IdCargo = $(this).parent().parent().find('td:eq(0)').html();
            var parametros = new Object();
            var fila = $(this);
            parametros.IdCargo = IdCargo;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfCargo.aspx/Delete_Cargo',
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
        IdCargo = $(this).parent().parent().find('td:eq(0)').html();
        nombreCargo = $(this).parent().parent().find('td:eq(1)').html();
        montoSalarioCargo = $(this).parent().parent().find('td:eq(2)').html();
        $("#ContentPlaceHolder1_txtNombreCargo").val(nombreCargo);
        $("#ContentPlaceHolder1_txtMontoSalario").val(montoSalarioCargo);
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
            if ($("#ContentPlaceHolder1_txtNombreCargo").val().trim() == "") acepto += "*Falta completar el campo nombre Cargo</br>";
            if ($("#ContentPlaceHolder1_txtNombreCargo").val().length > 30) acepto += "*Nombre Cargo solo acepta como máximo 50 letras</br>";
            if (!$(":text#ContentPlaceHolder1_txtNombreCargo").attr("value").match(/^[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ\s]+$/)) acepto += "*El nombre cargo no tiene formato correcto</br>";
            if ($("#ContentPlaceHolder1_txtMontoSalario").val().trim() == "") acepto += "*Falta completar el campo Monto salario</br>";
            if ($("#ContentPlaceHolder1_txtMontoSalario").val().length > 11) acepto += "*Monto Salario solo acepta como máximo 10 caracteres</br>";
            if (!$(":text#ContentPlaceHolder1_txtMontoSalario").attr("value").match(/^(?:[1-9]\d*|0)?(?:\.\d+)?$/)) acepto += "*El formato de monto salario no es el correcto </br>";
            if (parseInt($("#ContentPlaceHolder1_txtMontoSalario").val().trim()) > 9999999) acepto += "*El monto salario debe ser menor a 9999999.999</br>";
            if (acepto == "") {
                var parametros = new Object();
                parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
                parametros.IdCargo = IdCargo;
                parametros.nombreCargo = $("#ContentPlaceHolder1_txtNombreCargo").val().trim();
                parametros.montoSalarioCargo = $("#ContentPlaceHolder1_txtMontoSalario").val().trim();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'WfCargo.aspx/Save_Datos_Cargo',
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
                            $("#ContentPlaceHolder1_grd_cargo tbody").append(nuevafila);
                            limpiartTextBox();
                            desactivarTextBox();
                            $("#ContentPlaceHolder1_lblErrores").html("Insertado con éxito");
                            if ($("#ContentPlaceHolder1_grd_cargo tbody tr").length == 0) location.reload();
                        }
                        else if (objeto.msg_error == "Editado con éxito") {
                            $("#ContentPlaceHolder1_grd_cargo tbody tr").find('td:eq(0)').each(function () {

                                //obtenemos el valor de la celda
                                valor = $(this).html();
                                if (parseInt(valor) == parseInt(IdCargo)) {
                                    $(this).parent().find('td:eq(1)').html($("#ContentPlaceHolder1_txtNombreCargo").val());
                                    var remunera = parseFloat($("#ContentPlaceHolder1_txtMontoSalario").val());
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
        $("#ContentPlaceHolder1_txtNombreCargo").val("");
        $("#ContentPlaceHolder1_txtMontoSalario").val("");
    }

    function desactivarTextBox() {
        $("#ContentPlaceHolder1_txtNombreCargo").prop('disabled', true);
        $("#ContentPlaceHolder1_txtMontoSalario").prop('disabled', true);
    }

    function activarTextBox() {
        $("#ContentPlaceHolder1_txtNombreCargo").prop('disabled', false);
        $("#ContentPlaceHolder1_txtMontoSalario").prop('disabled', false);
    }


});