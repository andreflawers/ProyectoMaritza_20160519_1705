$(document).ready(function (e) {
    var IdMovimiento = 0, IdPersonal = 0;
    $("#ContentPlaceHolder1_btnMovimiento").attr("disabled", true);
    $(document).on("click", "#ContentPlaceHolder1_btnBuscar", function (e) {
        e.preventDefault();
        if ($("#ContentPlaceHolder1_txtNumDoc").val().trim() == "") {
            agrega_error("#errorDNI", "Ingrese un dni");
            $("#grd_movimientos tbody tr").remove();
            $("#ContentPlaceHolder1_txtApellidoPaterno").val("");
            $("#ContentPlaceHolder1_btnMovimiento").attr("disabled", true);
        } else {
            if ($("#ContentPlaceHolder1_txtNumDoc").val().length < 8) {
                agrega_error("#errorDNI", "Faltan carácteres...!!!");
                $("#grd_movimientos tbody tr").remove();
                $("#ContentPlaceHolder1_txtApellidoPaterno").val("");
                $("#ContentPlaceHolder1_btnMovimiento").attr("disabled", true);
            } else {
                var parametros = new Object();
                parametros.num_doc = $("#ContentPlaceHolder1_txtNumDoc").val();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: 'WfMovimientos.aspx/buscar_personal',
                    data: JSON.stringify(parametros),
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        var objeto = JSON.parse(data.d);
                        if (objeto.personal_nombre == null) {
                            agrega_error("#errorDNI", "Documento no encontrado...!!!");
                            $("#ContentPlaceHolder1_txtApellidoPaterno").val("");
                            $("#ContentPlaceHolder1_btnMovimiento").attr("disabled", true);
                            $("#grd_movimientos tbody tr").remove();
                        } else {
                            eliminar_error("#errorDNI");
                            $("#ContentPlaceHolder1_btnMovimiento").attr("disabled", false);
                            $("#ContentPlaceHolder1_txtApellidoPaterno").val(objeto.personal_apellido_paterno + " " + objeto.personal_apellido_materno + ", " + objeto.personal_nombre);
                            movimientos(objeto.personal_id);
                        }
                    },
                    error: function (data) {
                        console.info('error');
                    }
                });
            }
        }
    });
    $(document).on("click", ".link-editar", function (e) {
        e.preventDefault();
        IdMovimiento = $(this).parent().parent().find('td:eq(0)').html();
        descripcionMovimiento = $(this).parent().parent().find('td:eq(1)').html();
        fechaMovimiento = $(this).parent().parent().find('td:eq(2)').html();
        montoMovimiento = $(this).parent().parent().find('td:eq(3)').html();
        IdTipoMovimiento = $(this).parent().parent().find('td:eq(4)').html();
        nombreTipoMovimiento = $(this).parent().parent().find('td:eq(5)').html();
        $("#ContentPlaceHolder1_txt_descr_popup").val(descripcionMovimiento);
        $("#ContentPlaceHolder1_txt_fecha_popup").val(fechaMovimiento.split('-').reverse().join('-'));
        $("#ContentPlaceHolder1_txt_monto_popup").val(montoMovimiento);
        $("#ContentPlaceHolder1_ddl_tipo_mov_popup").val(IdTipoMovimiento);
        $("#ContentPlaceHolder1_hidden_transac").val("E");
        openModal();
    });
    $(document).on("click", ".link-eliminar", function (e) {
        e.preventDefault();
        if (confirm('¿Esta seguro de Eliminar este Registro?')) {
            IdMovimiento = $(this).parent().parent().find('td:eq(0)').html();
            var parametros = new Object();
            var fila = $(this);
            parametros.IdMovimiento = IdMovimiento;
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfMovimientos.aspx/eliminar_movimiento',
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
    $(document).on("click", "#ContentPlaceHolder1_btn_guardar_popup", function (e) {
        e.preventDefault();
        if (confirm('¿Esta seguro de Guardar este Registro?')) {
            var can = 0;
            if ($("#ContentPlaceHolder1_ddl_tipo_mov_popup").val() == 1000) {
                can = 1;
                agrega_error("#errorTipo", "Ingrese el tipo de movimiento");
            } else {
                eliminar_error("#errorTipo");
            }
            if ($("#ContentPlaceHolder1_txt_fecha_popup").val() == "") {
                can = 1;
                agrega_error("#errorFecha", "Ingrese la fecha");
            } else {
                eliminar_error("#errorFecha");
            }
            if ($("#ContentPlaceHolder1_txt_descr_popup").val() == "") {
                can = 1;
                agrega_error("#errorDescr", "Ingrese la descripción");
            } else {
                eliminar_error("#errorDescr");
            }
            if ($("#ContentPlaceHolder1_txt_monto_popup").val() == "") {
                can = 1;
                agrega_error("#errorMonto", "Ingrese el monto");
            } else {
                eliminar_error("#errorMonto");
            }
            if (can == 0) {
                var parametros = new Object();
                parametros.modo_edicion = $("#ContentPlaceHolder1_hidden_transac").val();
                parametros.IdPersonal = IdPersonal;
                parametros.IdMovimiento = IdMovimiento;
                parametros.descripcionMovimiento = $("#ContentPlaceHolder1_txt_descr_popup").val();
                parametros.fechaMovimiento = $("#ContentPlaceHolder1_txt_fecha_popup").val();
                parametros.montoMovimiento = $("#ContentPlaceHolder1_txt_monto_popup").val();
                parametros.IdTipoMovimiento = $("#ContentPlaceHolder1_ddl_tipo_mov_popup").val();
                $.ajax({
                    type: 'POST',
                    contentType: "application/json; charset=utf-8",
                    url: "WfMovimientos.aspx/guardar_movimiento",
                    data: JSON.stringify(parametros),
                    dataType: "json",
                    async: true,
                    success: function (data) {
                        var objeto = JSON.parse(data.d);
                        if (objeto[0] == "E") {
                            $("#grd_movimientos tbody tr").find('td:eq(0)').each(function () {
                                valor = $(this).html();
                                if (parseInt(valor) == parseInt(IdMovimiento)) {
                                    $(this).parent().find('td:eq(1)').html($("#ContentPlaceHolder1_txt_descr_popup").val());
                                    $(this).parent().find('td:eq(2)').html($("#ContentPlaceHolder1_txt_fecha_popup").val().split('-').reverse().join('-'));
                                    $(this).parent().find('td:eq(3)').html($("#ContentPlaceHolder1_txt_monto_popup").val());
                                    $(this).parent().find('td:eq(4)').html($("#ContentPlaceHolder1_ddl_tipo_mov_popup").val());
                                    $(this).parent().find('td:eq(5)').html($("#ContentPlaceHolder1_ddl_tipo_mov_popup option:selected").text());
                                    console.log($("#ContentPlaceHolder1_ddl_tipo_mov_popup").val());
                                }
                            })
                        } else {
                            var nuevafila = "";
                            nuevafila = "<tr>";
                            nuevafila += "<td>" + objeto[1].IdMovimiento + "</td>";
                            nuevafila += "<td>" + objeto[1].descripcionMovimiento + "</td>";
                            nuevafila += "<td>" + objeto[1].fechaMovimiento + "</td>";
                            nuevafila += "<td>" + objeto[1].montoMovimiento + "</td>";
                            nuevafila += '<td style="display:none;">' + objeto[1]["tipoMovimiento"].IdTipoMovimiento + "</td>";
                            nuevafila += "<td>" + objeto[1]["tipoMovimiento"].nombreTipoMovimiento + "</td>";
                            nuevafila += "<td>" + '<input type="submit" class="link-editar btn btn-info" value="Editar" />'
                            nuevafila += '<input type="submit" class="link-eliminar btn btn-warning" value="Eliminar" />' + "</td>";
                            nuevafila += "</tr>";
                            $("#grd_movimientos tbody").append(nuevafila);
                        }
                        closeModal();
                    }
                });
            }
        }
    });
    $(document).on("click", "#ContentPlaceHolder1_btnMovimiento", function (e) {
        e.preventDefault();
        openModal();
        limpiar();
        $("#ContentPlaceHolder1_hidden_transac").val("N");
    });
    function openModal() {
        $("#MyModal").modal('show');
    }
    function closeModal() {
        $("#MyModal").modal('hide');
    }
    function fechaActual() {
        var fullDate = new Date();
        var twoDigitMonth = ((fullDate.getMonth().length + 1) === 1) ? (fullDate.getMonth() + 1) : '0' + (fullDate.getMonth() + 1);
        var currentDate = fullDate.getFullYear() + "-" + twoDigitMonth + "-" + fullDate.getDate();
        return currentDate;
    }
    function limpiar() {
        var fecha = fechaActual();
        $("#ContentPlaceHolder1_hidden_transac").val("N");
        $("#ContentPlaceHolder1_ddl_tipo_mov_popup").val(1000);
        $("#ContentPlaceHolder1_txt_descr_popup").val("");
        $("#ContentPlaceHolder1_txt_monto_popup").val("");
        $("#ContentPlaceHolder1_txt_fecha_popup").val(fecha);
        eliminar_error("#errorTipo");
        eliminar_error("#errorerrorFecha");
        eliminar_error("#errorDescr");
        eliminar_error("#errorMonto");
    }
    function movimientos(personal_id) {
        var parametros = new Object();
        parametros.personal_id = personal_id;
        IdPersonal = personal_id;
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: "WfMovimientos.aspx/listar_movimiento",
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                var objeto = JSON.parse(data.d);
                $("#grd_movimientos tbody tr").remove();
                $.each(objeto, function (index, value) {
                    var nuevafila = "";
                    nuevafila = "<tr>";
                    nuevafila += "<td>" + value.IdMovimiento + "</td>";
                    nuevafila += "<td>" + value.descripcionMovimiento + "</td>";
                    nuevafila += "<td>" + value.fechaMovimiento + "</td>";
                    nuevafila += "<td>" + value.montoMovimiento + "</td>";
                    nuevafila += '<td style="display:none;">' + value["tipoMovimiento"].IdTipoMovimiento + "</td>";
                    nuevafila += "<td>" + value["tipoMovimiento"].nombreTipoMovimiento + "</td>";
                    nuevafila += "<td>" + '<input type="submit" class="link-editar btn btn-info" value="Editar" />'
                    nuevafila += '<input type="submit" class="link-eliminar btn btn-warning" value="Eliminar" />' + "</td>";
                    nuevafila += "</tr>";
                    $("#grd_movimientos tbody").append(nuevafila);
                });
            },
            error: function (data) {
                console.info('error');
            }
        });
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