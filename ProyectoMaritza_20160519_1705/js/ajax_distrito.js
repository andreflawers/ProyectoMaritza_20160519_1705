$(document).ready(function () {
    var IdDistrito = 0;
    desactivarTextBox();
    $("#ContentPlaceHolder1_grd_distrito td:nth-child(3),th:nth-child(3)").hide();

    $(document).on("click", ".linkin_eliminar", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_lblErrores").html("");
        if (confirm('¿Esta seguro de Eliminar este Registro?')) {
            IdDistrito = $(this).parent().parent().find('td:eq(0)').html();
            var parametros = new Object();
            var fila = $(this);
            parametros.IdDistrito = IdDistrito;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfDistrito.aspx/Delete_Distrito',
                data: JSON.stringify(parametros),
                dataType: "json",
                async: true,
                success: function (data) {
                    var objeto = JSON.parse(data.d);
                    if (objeto.msg_error == "Eliminado con éxito") {
                        var parent = fila.parent().parent();
                        $(parent).remove(); $("#ContentPlaceHolder1_lblErrores").html("Eliminado con éxito");
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
        IdDistrito = $(this).parent().parent().find('td:eq(0)').html();
        nombreDistrito = $(this).parent().parent().find('td:eq(1)').html();
        IdProvincia = $(this).parent().parent().find('td:eq(2)').html();
        nombreProvincia = $(this).parent().parent().find('td:eq(3)').html();

        $("#ContentPlaceHolder1_txtNombreDistrito").val(nombreDistrito);
        $("#ContentPlaceHolder1_cbn_provincia").val(IdProvincia);
        $("#ContentPlaceHolder1_hidden_transac").val("E");
        $("#ContentPlaceHolder1_txtNombreDistrito").prop('disabled', false);
        $("#ContentPlaceHolder1_cbn_provincia").prop('disabled', false);
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
            if ($("#ContentPlaceHolder1_txtNombreDistrito").val().trim() == "") acepto += "*Falta completar el campo nombre Distrito";
            if (!$(":text#ContentPlaceHolder1_txtNombreDistrito").attr("value").match(/^[a-zA-ZáéíóúàèìòùÀÈÌÒÙÁÉÍÓÚñÑüÜ\s]+$/)) acepto += "*El nombre distrito no tiene formato correcto</br>";
            if ($("#ContentPlaceHolder1_txtNombreDistrito").val().length > 50) acepto += "*Nombre Distrito solo acepta como máximo 50 letras</br>";
            if (acepto == "") {
                var parametros = new Object();
                parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
                parametros.IdDistrito = IdDistrito;
                parametros.nombreDistrito = $("#ContentPlaceHolder1_txtNombreDistrito").val().trim();
                parametros.IdProvincia = $("#ContentPlaceHolder1_cbn_provincia").val();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'WfDistrito.aspx/Save_Datos_Distrito',
                    data: JSON.stringify(parametros),
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        var objeto = JSON.parse(data.d);
                        console.log(objeto.nro);
                        if (objeto.msg_error == "Insertado con éxito" && objeto.nro == 1) {
                            var nuevafila = "";
                            nuevafila = "<tr> ";
                            nuevafila += "<td >" + objeto.new_codigo + "</td>";
                            nuevafila += "<td>" + objeto.oList[1] + "</td>";
                            nuevafila += "<td style='display:none'>" + objeto.oList[2] + "</td>";
                            nuevafila += "<td>" + objeto.oList[3] + "</td>";
                            nuevafila += "<td> " + '<input type="submit" class="linkin btn btn-info" value="Editar"  class="linkin"' + "</td>";
                            nuevafila += "<td> " + '<input type="submit"  class="linkin_eliminar btn btn-warning" value="Eliminar" class="linkin_eliminar"' + "</td>";
                            nuevafila += "</tr>";
                            $("#ContentPlaceHolder1_grd_distrito tbody").append(nuevafila);
                            limpiartTextBox();
                            desactivarTextBox();
                            $("#ContentPlaceHolder1_lblErrores").html("Insertado con éxito");
                            if ($("#ContentPlaceHolder1_grd_distrito tbody tr").length == 0) location.reload();
                        }
                        else if (objeto.msg_error == "Editado con éxito" && objeto.nro == 1) {
                            $("#ContentPlaceHolder1_grd_distrito tbody tr").find('td:eq(0)').each(function () {

                                //obtenemos el valor de la celda
                                valor = $(this).html();
                                if (parseInt(valor) == parseInt(IdDistrito)) {
                                    $(this).parent().find('td:eq(1)').html($("#ContentPlaceHolder1_txtNombreDistrito").val());
                                    $(this).parent().find('td:eq(2)').html($("#ContentPlaceHolder1_cbn_provincia").val());
                                    $(this).parent().find('td:eq(3)').html($('#ContentPlaceHolder1_cbn_provincia option:selected').text());
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
        $("#ContentPlaceHolder1_txtNombreDistrito").val("");
    }

    function desactivarTextBox() {
        $("#ContentPlaceHolder1_txtNombreDistrito").prop('disabled', true);
    }

    function activarTextBox() {
        $("#ContentPlaceHolder1_txtNombreDistrito").prop('disabled', false);
    }

});