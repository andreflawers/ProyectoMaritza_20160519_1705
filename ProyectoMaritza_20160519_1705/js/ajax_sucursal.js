$(document).ready(function () {
    var IdSucursal = 0;
    $("#ContentPlaceHolder1_grd_sucursal td:nth-child(6),th:nth-child(6)").hide();
    desactivarTextBox();

    $(document).on("click", ".linkin_eliminar", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        if (confirm('¿Esta seguro de Eliminar este Registro?')) {
            IdSucursal = $(this).parent().parent().find('td:eq(0)').html();
            var parametros = new Object();
            var fila = $(this);
            parametros.IdSucursal = IdSucursal;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfSucursal.aspx/Delete_Sucursal',
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
        IdSucursal = $(this).parent().parent().find('td:eq(0)').html();
        nombreSucursal = $(this).parent().parent().find('td:eq(1)').html();
        direccionSucursal = $(this).parent().parent().find('td:eq(2)').html();
        telefonoSucursal = $(this).parent().parent().find('td:eq(3)').html();
        rucSucursal = $(this).parent().parent().find('td:eq(4)').html();
        IdDistrito = $(this).parent().parent().find('td:eq(5)').html();
        nombreDistrito = $(this).parent().parent().find('td:eq(6)').html();

        $("#ContentPlaceHolder1_txtNombreSucursal").val(nombreSucursal);
        $("#ContentPlaceHolder1_txtDireccionSucursal").val(direccionSucursal);
        $("#ContentPlaceHolder1_txtTelefonoSucursal").val(telefonoSucursal);
        $("#ContentPlaceHolder1_txtRucSucursal").val(rucSucursal);
        $("#ContentPlaceHolder1_cbn_distrito").val(IdDistrito);
        $("#ContentPlaceHolder1_hidden_transac").val("E");
        $("#ContentPlaceHolder1_txtNombreSucursal").prop('disabled', false);
        $("#ContentPlaceHolder1_txtDireccionSucursal").prop('disabled', false);
        $("#ContentPlaceHolder1_txtTelefonoSucursal").prop('disabled', false);
        $("#ContentPlaceHolder1_txtRucSucursal").prop('disabled', false);
        $("#ContentPlaceHolder1_cbn_distrito").prop('disabled', false);
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
            if ($("#ContentPlaceHolder1_txtNombreSucursal").val() == "") acepto += "*Falta completar el campo nombre sucursal</br>";
            if (!$(":text#ContentPlaceHolder1_txtNombreSucursal").attr("value").match(/^[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ\s]+$/)) acepto += "*El nombre sucursal no tiene formato correcto</br>";
            if ($("#ContentPlaceHolder1_txtDireccionSucursal").val() == "") acepto += "*Falta completar el campo direccion sucursal</br>";
            if (!$(":text#ContentPlaceHolder1_txtDireccionSucursal").attr("value").match(/^[0-9a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ/.\s]+$/)) acepto += "*La direccion sucursal no tiene formato correcto</br>";
            if ($("#ContentPlaceHolder1_txtTelefonoSucursal").val() == "") acepto += "*Falta completar el campo telefono sucursal</br>";
            if ($("#ContentPlaceHolder1_txtRucSucursal").val() == "") acepto += "*Falta completar el campo ruc sucursal</br>";
            if ($("#ContentPlaceHolder1_txtNombreSucursal").val().length > 50) acepto += "*Nombre Sucursal solo acepta como máximo 50 letras</br>";
            if ($("#ContentPlaceHolder1_txtDireccionSucursal").val().length > 50) acepto += "*Dirección sucursal solo acepta como máximo 50 letras</br>";
            if ($("#ContentPlaceHolder1_txtTelefonoSucursal").val().length > 10) acepto += "*Teléfono sucursal solo acepta como máximo 10 números</br>";
            if ($("#ContentPlaceHolder1_txtRucSucursal").val().trim().length != 11) acepto += "*Ruc sucursal debe tener 11 números</br>";
            if (isNaN($("#ContentPlaceHolder1_txtTelefonoSucursal").val().trim())) acepto += "*Teléfono sucursal no tiene el formato correcto</br>";
            if (isNaN($("#ContentPlaceHolder1_txtRucSucursal").val().trim())) acepto += "*Ruc sucursal no tiene el formato correcto</br>";

            if (acepto == "") {
                var parametros = new Object();
                parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
                parametros.IdSucursal = IdSucursal;
                parametros.nombreSucursal = $("#ContentPlaceHolder1_txtNombreSucursal").val().trim();
                parametros.direccionSucursal = $("#ContentPlaceHolder1_txtDireccionSucursal").val().trim();
                parametros.telefonoSucursal = $("#ContentPlaceHolder1_txtTelefonoSucursal").val().trim();
                parametros.rucSucursal = $("#ContentPlaceHolder1_txtRucSucursal").val().trim();
                parametros.IdDistrito = $("#ContentPlaceHolder1_cbn_distrito").val();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'WfSucursal.aspx/Save_Datos_Sucursal',
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
                            nuevafila += "<td>" + objeto.oList[3] + "</td>";
                            nuevafila += "<td>" + objeto.oList[4] + "</td>";
                            nuevafila += "<td style='display:none'>" + objeto.oList[5] + "</td>";
                            nuevafila += "<td>" + objeto.oList[6] + "</td>";
                            nuevafila += "<td> " + '<input type="submit" class="linkin btn btn-info" value="Editar"  class="linkin"' + "</td>";
                            nuevafila += "<td> " + '<input type="submit" class="linkin_eliminar btn btn-warning" value="Eliminar" class="linkin_eliminar"' + "</td>";
                            nuevafila += "</tr>";
                            $("#ContentPlaceHolder1_grd_sucursal tbody").append(nuevafila);
                            limpiartTextBox();
                            desactivarTextBox();
                            $("#ContentPlaceHolder1_lblErrores").html("Insertado con éxito");
                            if ($("#ContentPlaceHolder1_grd_sucursal tbody tr").length == 0) location.reload();
                        }
                        else if (objeto.msg_error == "Editado con éxito") {
                            $("#ContentPlaceHolder1_grd_sucursal tbody tr").find('td:eq(0)').each(function () {

                                //obtenemos el valor de la celda
                                valor = $(this).html();
                                if (parseInt(valor) == parseInt(IdSucursal)) {
                                    $(this).parent().find('td:eq(1)').html($("#ContentPlaceHolder1_txtNombreSucursal").val());
                                    $(this).parent().find('td:eq(2)').html($("#ContentPlaceHolder1_txtDireccionSucursal").val());
                                    $(this).parent().find('td:eq(3)').html($("#ContentPlaceHolder1_txtTelefonoSucursal").val());
                                    $(this).parent().find('td:eq(4)').html($("#ContentPlaceHolder1_txtRucSucursal").val());
                                    $(this).parent().find('td:eq(5)').html($("#ContentPlaceHolder1_cbn_distrito").val());
                                    $(this).parent().find('td:eq(6)').html($('#ContentPlaceHolder1_cbn_distrito option:selected').text());
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
        $("#ContentPlaceHolder1_txtNombreSucursal").val("");
        $("#ContentPlaceHolder1_txtDireccionSucursal").val("");
        $("#ContentPlaceHolder1_txtTelefonoSucursal").val("");
        $("#ContentPlaceHolder1_txtRucSucursal").val("");
    }

    function desactivarTextBox() {
        $("#ContentPlaceHolder1_txtNombreSucursal").prop('disabled', true);
        $("#ContentPlaceHolder1_txtDireccionSucursal").prop('disabled', true);
        $("#ContentPlaceHolder1_txtTelefonoSucursal").prop('disabled', true);
        $("#ContentPlaceHolder1_txtRucSucursal").prop('disabled', true);
    }

    function activarTextBox() {
        $("#ContentPlaceHolder1_txtNombreSucursal").prop('disabled', false);
        $("#ContentPlaceHolder1_txtDireccionSucursal").prop('disabled', false);
        $("#ContentPlaceHolder1_txtTelefonoSucursal").prop('disabled', false);
        $("#ContentPlaceHolder1_txtRucSucursal").prop('disabled', false);
    }

    $("#ContentPlaceHolder1_txtTelefonoSucursal").keydown(function (event) {
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

    $("#ContentPlaceHolder1_txtRucSucursal").keydown(function (event) {
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