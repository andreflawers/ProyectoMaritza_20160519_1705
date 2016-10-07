$(document).on("ready", function () {
    var IdVacaciones = 0, IdPersonal = 0;
    $("#ContentPlaceHolder1_btnAdelanto").attr("disabled", true);
    $(document).on("click", "#ContentPlaceHolder1_btnBuscar", function (e) {
        e.preventDefault();
        if ($("#ContentPlaceHolder1_txtNumDoc").val().trim() == "") {
            agrega_error("#errorDNI", "Ingrese un dni");
        } else {
            eliminar_error("#errorDNI");
            var parametros = new Object();
            parametros.num_doc = $("#ContentPlaceHolder1_txtNumDoc").val();
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfVacaciones.aspx/buscar_personal',
                data: JSON.stringify(parametros),
                dataType: "json",
                async: true,
                success: function (data) {
                    var objeto = JSON.parse(data.d);
                    if (objeto.personal_nombre == null) {
                        $("#ContentPlaceHolder1_lblErrorId").val("Documento no encontrado !!!");
                        $("#ContentPlaceHolder1_txtApellidoPaterno").val("");
                        $("#ContentPlaceHolder1_btnAdelanto").attr("disabled", true);
                        $("#grd_vacaciones tbody tr").remove();
                    } else {
                        $("#ContentPlaceHolder1_btnAdelanto").attr("disabled", false);
                        $("#ContentPlaceHolder1_txtApellidoPaterno").val(objeto.personal_apellido_paterno + " " + objeto.personal_apellido_materno + ", " + objeto.personal_nombre);
                        vacaciones(objeto.personal_id);
                    }
                },
                error: function (data) {
                    console.info('error');
                }
            });
        }
    });
    function vacaciones(personal_id) {
        var parametros = new Object();
        parametros.personal_id = personal_id;
        IdPersonal = personal_id;
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: "WfVacaciones.aspx/listar_vacaciones",
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                var objeto = JSON.parse(data.d);
                $("#grd_vacaciones tbody tr").remove();
                $.each(objeto, function (index, value) {
                    var nuevafila = "";
                    nuevafila = "<tr>";
                    nuevafila += "<td>" + value.IdVacacion + "</td>";
                    nuevafila += "<td>" + value.fechaInicioVacacion + "</td>";
                    nuevafila += "<td>" + value.fechaFinVacacion + "</td>";
                    nuevafila += "<td>" + value.descripcionVacacion + "</td>";
                    nuevafila += '<td style="display:none;">' + value["encargado"].IdEncargado + "</td>";
                    nuevafila += "<td>" + value["encargado"].personal_apellido_paterno + " " + value["encargado"].personal_apellido_materno + ", " + value["encargado"].personal_nombre + "</td>";
                    nuevafila += "<td>" + '<input type="submit" class="link-editar btn btn-info" value="Editar" />'
                    nuevafila += '<input type="submit" class="link-eliminar btn btn-warning" value="Eliminar" />' + "</td>";
                    nuevafila += "</tr>";
                    $("#grd_vacaciones tbody").append(nuevafila);
                });
            },
            error: function (data) {
                console.info('error');
            }
        });
    }
    $(document).on("click", ".link-eliminar", function (e) {
        e.preventDefault();
        IdVacacion = $(this).parent().parent().find('td:eq(0)').html();
        var parametros = new Object();
        var fila = $(this);
        parametros.IdVacaciones = IdVacacion;
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: 'WfVacaciones.aspx/eliminar_vacaciones',
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                var objeto = JSON.parse(data.d);
                var parent = fila.parent().parent();
                $(parent).remove();
            },
            error: function (data) {
                console.info('error');
            }
        });
    });
    $(document).on("click", ".link-editar", function (e) {
        e.preventDefault();
        IdVacaciones = $(this).parent().parent().find('td:eq(0)').html();
        fechaInicioVacaciones = $(this).parent().parent().find('td:eq(1)').html();
        fechaFinVacaciones = $(this).parent().parent().find('td:eq(2)').html();
        descripcionVacaciones = $(this).parent().parent().find('td:eq(3)').html();
        IdEncargado = $(this).parent().parent().find('td:eq(4)').html();
        nombreEncargado = $(this).parent().parent().find('td:eq(5)').html();
        $("#ContentPlaceHolder1_txt_desde_popup").val(fechaInicioVacaciones.split('-').reverse().join('-'));
        $("#ContentPlaceHolder1_txt_hasta_popup").val(fechaFinVacaciones.split('-').reverse().join('-'));
        $("#ContentPlaceHolder1_txt_desc_popup").val(descripcionVacaciones);
        $("#encargado_id").val(IdEncargado);
        $("#filtrarEncargado").val(nombreEncargado);
        $("#ContentPlaceHolder1_hidden_transac").val("E");
        console.log($("#encargado_id").val());
        openModal();
    });
    $(document).on("click", "#ContentPlaceHolder1_btnAdelanto", function (e) {
        e.preventDefault();
        eliminar_error("#errorHasta");
        eliminar_error("#errorDesc");
        eliminar_error("#errorFiltrar");
        openModal();
        limpiar();
        $("#ContentPlaceHolder1_hidden_transac").val("N");
    });

    var parametro = new Object();
    parametro.valor = 1;
    var list_filtro = [];
    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        url: "WfVacaciones.aspx/filtrar_encargado",
        data: JSON.stringify(parametro),
        dataType: "json",
        async: true,
        success: function (data) {
            var objeto = JSON.parse(data.d);
            console.log(objeto);
            $("#filtrar_encargado").typeahead('destroy');
            list_filtro = objeto;
            var arreglo = Array();
            $.each(objeto, function (index, value) {
                arreglo[index] = value.personal_apellido_paterno + " " + value.personal_apellido_materno + ", " + value.personal_nombre;
            });
            $("#filtrarEncargado").typeahead({ source: arreglo });
        },
        error: function (data) {
            console.info('error');
        }
    });

    $(document).on("change", "#filtrarEncargado", function (e) {
        e.preventDefault();
        var termino = 0, encargado_id = 0;
        $.each($(list_filtro), function (index, value) {
            var personal_nombre = value.personal_apellido_paterno + " " + value.personal_apellido_materno + ", " + value.personal_nombre;
            if (personal_nombre == $("#filtrarEncargado").val().trim()) {
                termino = 1;
                encargado_id = value.IdEncargado;
            }
        });
        if (termino > 0) {
            $("#encargado_id").val(encargado_id);
        }
    });

    $(document).on("click", "#ContentPlaceHolder1_btn_guardar_popup", function (e) {
        e.preventDefault();
        var can = 0;
        if ($("#ContentPlaceHolder1_txt_hasta_popup").val() == "") {
            can = 1;
            agrega_error("#errorHasta", "Ingrese fecha de culminación");
        } else {
            eliminar_error("#errorHasta");
        }
        if ($("#ContentPlaceHolder1_txt_desc_popup").val() == "") {
            can = 1;
            agrega_error("#errorDesc", "Ingrese descripción");
        } else {
            eliminar_error("#errorDesc");
        }
        if ($("#filtrarEncargado").val() == "") {
            can = 1;
            agrega_error("#errorFiltrar", "Ingrese encargado");
        } else {
            eliminar_error("#errorFiltrar");
        }
        if (can == 0) {
            var parametros = new Object();
            parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
            parametros.IdVacaciones = IdVacaciones;
            parametros.IdPersonal = IdPersonal;
            parametros.fechaInicioVacaciones = $("#ContentPlaceHolder1_txt_desde_popup").val();
            parametros.fechaFinVacaciones = $("#ContentPlaceHolder1_txt_hasta_popup").val();
            parametros.descripcionVacaciones = $("#ContentPlaceHolder1_txt_desc_popup").val();
            parametros.IdEncargado = $("#encargado_id").val();
            parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: "WfVacaciones.aspx/guardar_vacaciones",
                data: JSON.stringify(parametros),
                dataType: "json",
                async: true,
                success: function (data) {
                    var objeto = JSON.parse(data.d);
                    if (objeto[0] == "E") {
                        $("#grd_vacaciones tbody tr").find('td:eq(0)').each(function () {
                            valor = $(this).html();
                            if (parseInt(valor) == parseInt(IdVacaciones)) {
                                $(this).parent().find('td:eq(1)').html($("#ContentPlaceHolder1_txt_desde_popup").val().split('-').reverse().join('-'));
                                $(this).parent().find('td:eq(2)').html($("#ContentPlaceHolder1_txt_hasta_popup").val().split('-').reverse().join('-'));
                                $(this).parent().find('td:eq(3)').html($("#ContentPlaceHolder1_txt_desc_popup").val());
                                $(this).parent().find('td:eq(4)').html($("#encargado_id").val());
                                $(this).parent().find('td:eq(5)').html($("#filtrarEncargado").val());
                            }
                        });
                    }
                    else {
                        var nuevafila = "";
                        nuevafila = "<tr>";
                        nuevafila += "<td>" + objeto[1].IdVacacion + "</td>";
                        nuevafila += "<td>" + objeto[1].fechaInicioVacacion + "</td>";
                        nuevafila += "<td>" + objeto[1].fechaFinVacacion + "</td>";
                        nuevafila += "<td>" + objeto[1].descripcionVacacion + "</td>";
                        nuevafila += '<td style="display:none;">' + objeto[1]["encargado"].IdEncargado + "</td>";
                        nuevafila += "<td>" + objeto[1]["encargado"].personal_apellido_paterno + " " + objeto[1]["encargado"].personal_apellido_materno + ", " + objeto[1]["encargado"].personal_nombre + "</td>";
                        nuevafila += "<td>" + '<input type="submit" class="link-editar btn btn-info" value="Editar" />'
                        nuevafila += '<input type="submit" class="link-eliminar btn btn-warning" value="Eliminar" />' + "</td>";
                        nuevafila += "</tr>";
                        $("#grd_vacaciones tbody").append(nuevafila);
                    }
                    closeModal();
                }
            });
        }
    });
    $(document).on("click", "#ContentPlaceHolder1_btn_salir_popup", function (e) {
        eliminar_error("#errorHasta");
        eliminar_error("#errorDesc");
        eliminar_error("#errorFiltrar");
    });
    function fechaActual() {
        var fullDate = new Date();
        var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : '0' + (fullDate.getMonth() + 1);
        var currentDate = fullDate.getFullYear() + "-" + twoDigitMonth + "-" + fullDate.getDate();
        return currentDate;
    }
    function limpiar() {
        var fecha = fechaActual();
        $("#ContentPlaceHolder1_txt_desde_popup").val(fecha);
        $("#ContentPlaceHolder1_txt_hasta_popup").val("");
        $("#ContentPlaceHolder1_txt_desc_popup").val("");
        $("#encargado_id").val(0);
        $("#filtrarEncargado").val("");
    }
    function openModal() {
        $("#MyModal").modal('show');
    }
    function closeModal() {
        $("#MyModal").modal('hide');
    }
    function eliminar_error(variable) {
        $(variable).removeClass("errores");
        $(variable).html("");
    }
    function agrega_error(variable, mensaje) {
        $(variable).addClass("errores");
        $(variable).html(mensaje);
    }
});