$(document).on("ready", function () {
    var IdPermiso = 0, IdPersonal = 0;
    $("#ContentPlaceHolder1_btn_buscar").attr("disabled", true);
    $("#ContentPlaceHolder1_btn_grabar").attr("disabled", true);
    $("#ContentPlaceHolder1_btn_cancelar").attr("disabled", true);
    $("#ContentPlaceHolder1_txt_numDoc").attr("disabled", true);
    $("#ContentPlaceHolder1_txt_numDoc").val("");
    $("#ContentPlaceHolder1_txt_fechaInicio").attr("disabled", true);
    $("#ContentPlaceHolder1_txt_fechaFin").attr("disabled", true);
    $("#ContentPlaceHolder1_txt_justificacion").attr("disabled", true);
    $("#ContentPlaceHolder1_drp_tipoPermiso").attr("disabled", true);
    $(document).on("click", "#ContentPlaceHolder1_btn_buscar", function (e) {
        e.preventDefault();
        if ($("#ContentPlaceHolder1_txt_numDoc").val() == "") {
            agrega_error("#errorDNI", "Ingrese un dni");
            $("#ContentPlaceHolder1_txt_apePaterno").val("");
            $("#grd_permisos tbody tr").remove();
        } else {
            if ($("#ContentPlaceHolder1_txt_numDoc").val().length < 8) {
                agrega_error("#errorDNI", "Faltan carácteres...!!!");
                $("#ContentPlaceHolder1_txt_apePaterno").val("");
                $("#grd_permisos tbody tr").remove();
            } else {
                var parametros = new Object();
                parametros.num_doc = $("#ContentPlaceHolder1_txt_numDoc").val();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'WfPermiso.aspx/buscar_personal',
                    data: JSON.stringify(parametros),
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        var objeto = JSON.parse(data.d);
                        if (objeto.personal_nombre == null) {
                            agrega_error("#errorDNI", "Documento no encontrado...!!!");
                            $("#ContentPlaceHolder1_txt_apePaterno").val("");
                            $("#grd_permisos tbody tr").remove();
                        } else {
                            eliminar_error("#errorDNI");
                            $("#ContentPlaceHolder1_btn_grabar").attr("disabled", false);
                            $("#ContentPlaceHolder1_btn_cancelar").attr("disabled", false);
                            $("#ContentPlaceHolder1_txt_fechaInicio").attr("disabled", false);
                            $("#ContentPlaceHolder1_txt_fechaFin").attr("disabled", false);
                            $("#ContentPlaceHolder1_txt_justificacion").attr("disabled", false);
                            $("#ContentPlaceHolder1_drp_tipoPermiso").attr("disabled", false);
                            $("#ContentPlaceHolder1_txt_apePaterno").val(objeto.personal_apellido_paterno + " " + objeto.personal_apellido_materno + ", " + objeto.personal_nombre);
                            permisos(objeto.personal_id);
                        }
                    },
                    error: function (data) {
                        console.info('error');
                    }
                });
            }
        }
    });
    function permisos(personal_id) {
        var parametros = new Object();
        parametros.personal_id = personal_id;
        IdPersonal = personal_id;
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: "WfPermiso.aspx/listar_permisos",
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                var objeto = JSON.parse(data.d);
                $("#grd_permisos tbody tr").remove();
                $.each(objeto, function (index, value) {
                    var nuevafila = "";
                    nuevafila = "<tr>";
                    nuevafila += "<td>" + value.IdPermiso + "</td>";
                    nuevafila += "<td>" + value.fechaIniPermiso + "</td>";
                    nuevafila += "<td>" + value.fechaFinPermiso + "</td>";
                    nuevafila += "<td>" + value.justPermiso + "</td>";
                    nuevafila += '<td style="display:none;">' + value["tipoPermiso"].IdTipoPermiso + "</td>";
                    nuevafila += "<td>" + value["tipoPermiso"].nombreTipoPermiso + "</td>";
                    nuevafila += "<td>" + '<input type="submit" class="link-editar btn btn-info" value="Editar" />'
                    nuevafila += '<input type="submit" class="link-eliminar btn btn-warning" value="Eliminar" />' + "</td>";
                    nuevafila += "</tr>";
                    $("#grd_permisos tbody").append(nuevafila);
                });
            },
            error: function (data) {
                console.info('error');
            }
        });
    }
    $(document).on("click", "#ContentPlaceHolder1_btn_cancelar", function (e) {
        e.preventDefault();
        $("#ContentPlaceHolder1_txt_apePaterno").val("");
        $("#ContentPlaceHolder1_txt_numDoc").val("");
        $("#ContentPlaceHolder1_btn_buscar").attr("disabled", true);
        $("#ContentPlaceHolder1_btn_grabar").attr("disabled", true);
        $("#ContentPlaceHolder1_btn_cancelar").attr("disabled", true);
        $("#ContentPlaceHolder1_txt_numDoc").attr("disabled", true);
        $("#ContentPlaceHolder1_txt_fechaInicio").attr("disabled", true);
        $("#ContentPlaceHolder1_txt_fechaFin").attr("disabled", true);
        $("#ContentPlaceHolder1_txt_justificacion").attr("disabled", true);
        $("#ContentPlaceHolder1_drp_tipoPermiso").attr("disabled", true);
        $("#grd_permisos tbody tr").remove();
        limpiar();
    });
    $(document).on("click", ".link-editar", function (e) {
        e.preventDefault();
        IdPermiso = $(this).parent().parent().find('td:eq(0)').html();
        fechaIniPermiso = $(this).parent().parent().find('td:eq(1)').html();
        fechaFinPermiso = $(this).parent().parent().find('td:eq(2)').html();
        justPermiso = $(this).parent().parent().find('td:eq(3)').html();
        IdTipoPermiso = $(this).parent().parent().find('td:eq(4)').html();
        nombreTipoPermiso = $(this).parent().parent().find('td:eq(5)').html();
        $("#ContentPlaceHolder1_txt_fechaInicio").val(fechaIniPermiso.split('-').reverse().join('-'));
        $("#ContentPlaceHolder1_txt_fechaFin").val(fechaFinPermiso.split('-').reverse().join('-'));
        $("#ContentPlaceHolder1_txt_justificacion").val(justPermiso);
        $("#ContentPlaceHolder1_drp_tipoPermiso").val(IdTipoPermiso);
        $("#ContentPlaceHolder1_hidden_transac").val("E");

    });
    $(document).on("click", ".link-eliminar", function (e) {
        e.preventDefault();
        if (confirm('¿Esta seguro de Eliminar este Permiso?')) {
            IdPermiso = $(this).parent().parent().find('td:eq(0)').html();
            var parametros = new Object();
            var fila = $(this);
            parametros.IdPermiso = IdPermiso;

            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfPermiso.aspx/eliminar_permisos',
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
        }
    });

    $(document).on("click", "#ContentPlaceHolder1_btn_nuevo", function (e) {
        e.preventDefault();
        limpiar();
        $("#ContentPlaceHolder1_hidden_transac").val("N");
        $("#ContentPlaceHolder1_btn_buscar").attr("disabled", false);
        $("#ContentPlaceHolder1_txt_numDoc").attr("disabled", false);
        $("#ContentPlaceHolder1_txt_numDoc").attr("focus", true);
    });

    $(document).on("click", "#ContentPlaceHolder1_btn_grabar", function (e) {
        e.preventDefault();
        if (confirm('¿Esta seguro de Guardar este Permiso?')) {
            var can = 0;
            if ($("#ContentPlaceHolder1_drp_tipoPermiso").val() == 0) {
                can = 1;
                agrega_error("#errorTipo", "Ingrese el tipo de permiso");
            } else {
                eliminar_error("#errorTipo");
            }
            if ($("#ContentPlaceHolder1_txt_justificacion").val() == "") {
                can = 1;
                agrega_error("#errorJustificacion", "Ingrese la justificación");
            } else {
                eliminar_error("#errorJustificacion");
            }
            if ($("#ContentPlaceHolder1_txt_fechaFin").val() == "") {
                can = 1;
                agrega_error("#errorFechaFin", "Ingrese la fecha de culminación");
            } else {
                eliminar_error("#errorFechaFin");
            }
            if (can == 0) {
                var parametros = new Object();
                parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
                parametros.IdPermiso = IdPermiso;
                parametros.IdPersonal = IdPersonal;
                parametros.fechaIniPermiso = $("#ContentPlaceHolder1_txt_fechaInicio").val();
                parametros.fechaFinPermiso = $("#ContentPlaceHolder1_txt_fechaFin").val();
                parametros.justPermiso = $("#ContentPlaceHolder1_txt_justificacion").val();
                parametros.IdTipoPermiso = $("#ContentPlaceHolder1_drp_tipoPermiso").val();
                parametros.nombreTipoPermiso = $("#ContentPlaceHolder1_drp_tipoPermiso option:selected").text();
                parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: "WfPermiso.aspx/guardar_permiso",
                    data: JSON.stringify(parametros),
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        var objeto = JSON.parse(data.d);
                        if (objeto[0] == "E") {
                            $("#grd_permisos tbody tr").find('td:eq(0)').each(function () {
                                valor = $(this).html();
                                if (parseInt(valor) == parseInt(IdPermiso)) {
                                    $(this).parent().find('td:eq(1)').html($("#ContentPlaceHolder1_txt_fechaInicio").val().split('-').reverse().join('-'));
                                    $(this).parent().find('td:eq(2)').html($("#ContentPlaceHolder1_txt_fechaFin").val().split('-').reverse().join('-'));
                                    $(this).parent().find('td:eq(3)').html($("#ContentPlaceHolder1_txt_justificacion").val());
                                    $(this).parent().find('td:eq(4)').html($("#ContentPlaceHolder1_drp_tipoPermiso").val());
                                    $(this).parent().find('td:eq(5)').html($("#ContentPlaceHolder1_drp_tipoPermiso option:selected").text());
                                }
                            });
                            $("#ContentPlaceHolder1_hidden_transac").val("N");
                            limpiar();
                        }
                        else {
                            var nuevafila = "";
                            nuevafila = "<tr>";
                            nuevafila += "<td>" + objeto[1].IdPermiso + "</td>";
                            nuevafila += "<td>" + objeto[1].fechaIniPermiso + "</td>";
                            nuevafila += "<td>" + objeto[1].fechaFinPermiso + "</td>";
                            nuevafila += "<td>" + objeto[1].justPermiso + "</td>";
                            nuevafila += '<td style="display:none;">' + objeto[1]["tipoPermiso"].IdTipoPermiso + "</td>";
                            nuevafila += "<td>" + objeto[1]["tipoPermiso"].nombreTipoPermiso + "</td>";
                            nuevafila += "<td>" + '<input type="submit" class="link-editar btn btn-info" value="Editar" />'
                            nuevafila += '<input type="submit" class="link-eliminar btn btn-warning" value="Eliminar" />' + "</td>";
                            nuevafila += "</tr>";
                            $("#grd_permisos tbody").append(nuevafila);
                            $("#ContentPlaceHolder1_hidden_transac").val("N");
                            limpiar();
                        }
                        closeModal();
                    }
                });
            }
        }
    });
    function fechaActual() {
        var fullDate = new Date();
        var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : '0' + (fullDate.getMonth() + 1);
        var currentDate = fullDate.getFullYear() + "-" + twoDigitMonth + "-" + fullDate.getDate();
        return currentDate;
    }
    function limpiar() {
        var fecha = fechaActual();
        $("#ContentPlaceHolder1_txt_fechaInicio").val(fecha);
        $("#ContentPlaceHolder1_txt_fechaFin").val("");
        $("#ContentPlaceHolder1_txt_justificacion").val("");
        $("#ContentPlaceHolder1_drp_tipoPermiso").val(0);
        eliminar_error("#errorDNI");
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