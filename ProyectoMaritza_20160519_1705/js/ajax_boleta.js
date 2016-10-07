$(document).ready(function () {
    var objeto;
    var fechaActual = new Date();
    var n_mes = fechaActual.getMonth() + 1;
    var n_ano = fechaActual.getFullYear();
    var total, total_remuneracion, total_descuento_planilla, total_descuentos, total_movimientos;
    $("#ContentPlaceHolder1_btn_guardar").attr("disabled", true);
    $("#ContentPlaceHolder1_btn_imprimir").attr("disabled", true);
    $(document).on("click", "#ContentPlaceHolder1_bnt_buscar", function (e) {
        e.preventDefault();
        if ($("#ContentPlaceHolder1_txt_dni").val() == "") {
            $("#ContentPlaceHolder1_lbl_error").text("Ingrese número de documento");
            limpiar();

        }
        else {
            if ($("#ContentPlaceHolder1_txt_dni").val().length < 8) {
                $("#ContentPlaceHolder1_lbl_error").text("Faltan caracteres....");
                limpiar();
            } else {
                $("#ContentPlaceHolder1_lbl_error").text("");
                $("#ContentPlaceHolder1_btn_guardar").attr("disabled", false);
                listar_boleta($("#ContentPlaceHolder1_txt_dni").val(), n_mes, n_ano);

            }
        }
    });

    function listar_boleta(num_doc, mes, ano) {
        objeto = new Object();
        total = 0;
        total_descuento_planilla = 0;
        total_descuentos = 0;
        total_movimientos = 0;
        total_remuneracion = 0;

        var parametros = new Object();
        parametros.num_doc = num_doc;
        parametros.mes = mes;
        parametros.ano = ano;
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: 'WfBoleta.aspx/listar_boleta',
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                objeto = JSON.parse(data.d);
                $("#grd_remuneracion tbody tr").remove();
                $("#grd_descuento_planilla tbody tr").remove();
                if (objeto["personal"].personal_nombre == null) {
                    $("#ContentPlaceHolder1_lbl_error").text("Numero de documento no existe!!!");
                    limpiar();
                } else {
                    $("#ContentPlaceHolder1_txt_n_boleta").val((objeto["personal"].personal_id) + "" + ano + "" + mes);
                    $("#ContentPlaceHolder1_txt_apellidos_nombres").val(objeto["personal"].personal_apellido_paterno + " " + objeto["personal"].personal_apellido_materno + ", " + objeto["personal"].personal_nombre);
                    $("#ContentPlaceHolder1_txt_cargo").val(objeto["cargo"].nombreCargo);
                    $("#ContentPlaceHolder1_txt_modalidad").val("Mensual");
                    if ($("#ContentPlaceHolder1_txt_ano").val() == "") {
                        $("#ContentPlaceHolder1_txt_ano").val(ano);
                    }
                    if ($("#ContentPlaceHolder1_ddl_mes").val() == null) {
                        for (var i = 1; i <= n_mes; i++) {
                            var id = i;
                            var text = fechaMes(i);
                            $("#ContentPlaceHolder1_ddl_mes").get(0).options[$("#ContentPlaceHolder1_ddl_mes").get(0).options.length] = new Option(text, id);
                        }
                        $("select#ContentPlaceHolder1_ddl_mes").val(n_mes);
                    }
                    $("#ContentPlaceHolder1_txt_dias_trabajados").val(objeto.trabajados);
                    if (objeto["personal"].personal_planilla == 1) {
                        $("#ContentPlaceHolder1_txt_planilla_estado").val("Si");
                        aportaciones(objeto["cargo"].montoSalarioCargo);
                        if (mes != 7 && mes != 12) {
                            descuentos_planilla(objeto["cargo"].montoSalarioCargo);
                            //descuentoPlanilla(objeto.nombreDescuentoPlanilla, objeto.montoBoletaPlanilla);
                        }
                    } else {
                        $("#ContentPlaceHolder1_txt_planilla_estado").val("No");
                    }
                    $("#ContentPlaceHolder1_txt_dias_dominicales").val(objeto.dominicales);
                    $("#ContentPlaceHolder1_txt_dias_feriados").val(objeto.feriados);
                    $("#ContentPlaceHolder1_txt_tardanzas").val(objeto.tardanzas);
                    $("#ContentPlaceHolder1_txt_permiso").val(objeto.dias_p);
                    $("#ContentPlaceHolder1_txt_vacaciones").val(objeto.dias_v);

                    var monto_dias_trabajados = parseFloat(objeto["cargo"].montoSalarioCargo / daysInMonth(mes, ano) * objeto.trabajados);
                    var monto_dias_permiso = parseFloat(objeto.monto_p);
                    var monto_dias_vacaciones = parseFloat(objeto.dias_v * (objeto["cargo"].montoSalarioCargo / daysInMonth(mes, ano)));
                    var nuevafila = "";
                    nuevafila = "<tr>";
                    nuevafila += "<td>" + "Salario" + "</td>";
                    nuevafila += "<td>" + monto_dias_trabajados.toFixed(2) + "</td>";
                    nuevafila += "</tr>";
                    total_remuneracion += monto_dias_trabajados;
                    if (objeto.monto_p > 0) {
                        nuevafila += "<tr>";
                        nuevafila += "<td>" + "Permisos" + "</td>";
                        nuevafila += "<td>" + monto_dias_permiso.toFixed(2) + "</td>";
                        nuevafila += "</tr>";
                        total_remuneracion += monto_dias_permiso;
                    }
                    if (objeto.dias_v > 0) {
                        nuevafila += "<tr>";
                        nuevafila += "<td>" + "Vacaciones" + "</td>";
                        nuevafila += "<td>" + monto_dias_vacaciones.toFixed(2) + "</td>";
                        nuevafila += "</tr>";
                        total_remuneracion += monto_dias_vacaciones;
                    }
                    $("#grd_remuneracion").append(nuevafila);

                    remuneracion(objeto["cargo"].montoSalarioCargo, nuevafila, objeto.dominicales, objeto.feriados, mes, ano);

                    descuentos(objeto["cargo"].montoSalarioCargo, objeto.tardanzas, mes, ano);

                    movimientos();
                }

                console.log(objeto);
            },
            error: function (data) {
                console.info('error');
            }
        });

    }


    function remuneracion(salario, total, domingos, feriados, mes, ano) {
        var parametros = new Object();
        parametros.num_doc = $("#ContentPlaceHolder1_txt_dni").val();
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: 'WfBoleta.aspx/listar_boleta_remuneracion',
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                var objeto = JSON.parse(data.d);
                console.log(fechaMes(mes));
                $.each(objeto, function (index, value) {

                    if (value.nombreRemuneracion == "Feriados") {
                        value.montoRemuneracion = (value.montoRemuneracion * feriados);
                        var nuevafila = "";
                        nuevafila = "<tr>";
                        nuevafila += "<td>" + value.nombreRemuneracion + "</td>";
                        nuevafila += "<td>" + value.montoRemuneracion.toFixed(2) + "</td>";
                        nuevafila += "</tr>";
                        total_remuneracion += parseFloat(value.montoRemuneracion);
                    }
                    if (value.nombreRemuneracion == "Dominicales") {
                        value.montoRemuneracion = (value.montoRemuneracion * domingos);
                        var nuevafila = "";
                        nuevafila = "<tr>";
                        nuevafila += "<td>" + value.nombreRemuneracion + "</td>";
                        nuevafila += "<td>" + value.montoRemuneracion.toFixed(2) + "</td>";
                        nuevafila += "</tr>";
                        total_remuneracion += parseFloat(value.montoRemuneracion);
                    }
                    if (value.nombreRemuneracion == "Gratificacion" && (fechaMes(mes) == "Julio" || fechaMes(mes) == "Diciembre")) {
                        value.montoRemuneracion = ((value.montoRemuneracion * salario) / 100);
                        var nuevafila = "";
                        nuevafila = "<tr>";
                        nuevafila += "<td>" + value.nombreRemuneracion + "</td>";
                        nuevafila += "<td>" + value.montoRemuneracion.toFixed(2) + "</td>";
                        nuevafila += "</tr>";
                        total_remuneracion += parseFloat(value.montoRemuneracion);
                    }

                    $("#grd_remuneracion").append(nuevafila);
                });
            },
            error: function (data) {
                console.info('error');
            }
        });
    }
    function aportaciones(salario) {
        $("#grd_aportacion tbody tr").remove();
        var nuevafilaEssalud = "";
        nuevafilaEssalud = "<tr>";
        nuevafilaEssalud += "<td>" + "Essalud" + "</td>";
        nuevafilaEssalud += "<td>" + ((9 / 100) * salario).toFixed(2) + "</td>";
        nuevafilaEssalud += "</tr>";
        $("#grd_aportacion").append(nuevafilaEssalud);
    }

    /*function descuentoPlanilla(nombre, monto) {
        $("#grd_descuento_planilla tbody tr").remove();
        var nuevafila = "";
        nuevafila = "<tr>";
        nuevafila += "<td>" + nombre + "</td>";
        nuevafila += "<td>" + monto.toFixed(2) + "</td>";
        nuevafila += "</tr>";
        total_descuento_planilla = parseFloat(monto.toFixed(2));
        $("#grd_descuento_planilla").append(nuevafila);
    }*/

    function descuentos_planilla(salario) {
        var parametro = new Object();
        parametro.par = 1;
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: 'WfBoleta.aspx/listar_descuento_planilla',
            data: JSON.stringify(parametro),
            dataType: "json",
            async: true,
            success: function (data) {
                var desc = JSON.parse(data.d);
                $("#grd_descuento_planilla tbody tr").remove();
                $.each(desc, function (index, value) {
                    var nf = "";
                    nf = "<tr>";
                    nf += "<td>" + value.nombreDescuentoPlanilla + "</td>";
                    nf += "<td>" + parseFloat(value.montoDescuento * salario).toFixed(2) + "</td>";
                    nf += "</tr>";
                    total_descuento_planilla = parseFloat(value.montoDescuento * salario);
                    objeto["descuentoPlanilla"].IdDescuentoPlanilla = value.IdDescuentoPlanilla;
                    $("#grd_descuento_planilla").append(nf);
                });

            },
            error: function (data) {
                console.info('error');
            }
        });
    }

    function descuentos(salario, tardanzas, mes, ano) {
        var parametros = new Object();
        parametros.num_doc = $("#ContentPlaceHolder1_txt_dni").val();
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: 'WfBoleta.aspx/listar_boleta_descuento',
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                var objeto = JSON.parse(data.d);
                $("#grd_descuento tbody tr").remove();
                var descuen = 0;

                $.each(objeto, function (index, value) {

                    var nuevafila = "";
                    nuevafila += "<tr>";
                    nuevafila += "<td>" + value.nombreDescuento + "</td>";
                    if (value.nombreDescuento == "Tardanzas") {
                        if (tardanzas >= 3) {
                            total_descuentos += parseFloat(value.montoDescuento * tardanzas / 3);
                            nuevafila += "<td>" + (value.montoDescuento * tardanzas / 3).toFixed(2) + "</td>";
                        }
                    }
                    else {
                        total_descuentos += parseFloat(value.montoDescuento);
                        nuevafila += "<td>" + value.montoDescuento.toFixed(2) + "</td>";
                    }
                    nuevafila += "</tr>";

                    $("#grd_descuento").append(nuevafila);
                });
            },
            error: function (data) {
                console.info('error');
            }
        });
    }

    function movimientos() {
        var parametros = new Object();
        parametros.num_doc = $("#ContentPlaceHolder1_txt_dni").val();
        parametros.ano = fechaAno();
        parametros.mes = $("#ContentPlaceHolder1_ddl_mes option:Selected").index() + 1;
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: 'WfBoleta.aspx/listar_boleta_movimiento',
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                var objeto = JSON.parse(data.d);

                $("#grd_movimientos tbody tr").remove();
                $.each(objeto, function (index, value) {

                    var nuevafila = "";
                    nuevafila = "<tr>";
                    nuevafila += "<td>" + value["tipoMovimiento"].nombreTipoMovimiento + "</td>";
                    nuevafila += "<td>" + (value.montoMovimiento).toFixed(2) + "</td>";
                    nuevafila += "</tr>";
                    total_movimientos += parseFloat(value.montoMovimiento);

                    $("#grd_movimientos").append(nuevafila);
                });

            },
            error: function (data) {
                console.info('error');
            }
        });
    }

    function daysInMonth(humanMonth, year) {
        return new Date(year || new Date().getFullYear(), humanMonth, 0).getDate();
    }

    function fechaAno() {
        var fullDate = new Date();
        return fullDate.getFullYear();
    }
    function fechaMes(mes) {
        if (mes == 1) {
            return "Enero";
        } else if (mes == 2) {
            return "Febrero";
        } else if (mes == 3) {
            return "Marzo";
        } else if (mes == 4) {
            return "Abril";
        } else if (mes == 5) {
            return "Marzo";
        } else if (mes == 6) {
            return "Junio";
        } else if (mes == 7) {
            return "Julio";
        } else if (mes == 8) {
            return "Agosto";
        } else if (mes == 9) {
            return "Septiembre";
        } else if (mes == 10) {
            return "Octubre";
        } else if (mes == 11) {
            return "Noviembre";
        } else if (mes == 12) {
            return "Diciembre";
        }
    }
    $(document).on("change", "#ContentPlaceHolder1_ddl_mes", function (e) {
        e.preventDefault();
        listar_boleta($("#ContentPlaceHolder1_txt_dni").val(),
            $("#ContentPlaceHolder1_ddl_mes option:selected").index() + 1, $("#ContentPlaceHolder1_txt_ano").val());
    });
    $(document).on("change", "#ContentPlaceHolder1_txt_ano", function (e) {
        e.preventDefault();
        listar_boleta($("#ContentPlaceHolder1_txt_dni").val(),
            $("#ContentPlaceHolder1_ddl_mes option:selected").index() + 1, $("#ContentPlaceHolder1_txt_ano").val());
    });
    $(document).on("click", "#ContentPlaceHolder1_btn_guardar", function (e) {
        e.preventDefault();

        if (confirm('¿Esta seguro de Guardar este Registro?')) {

            var par = new Object();
            par.num_boleta = $("#ContentPlaceHolder1_txt_n_boleta").val();
            $.ajax({
                type: 'POST',
                contentType: "application/json; charset=utf-8",
                url: 'WfBoleta.aspx/consultar_num_boleta',
                data: JSON.stringify(par),
                dataType: "json",
                async: true,
                success: function (data) {
                    var num_bol = JSON.parse(data.d);
                    if (total <= 0) {
                        alert("El monto total es negativo!!!");
                        limpiar();
                    }
                    else {
                        if (num_bol == "") {
                            insertar_boleta($("#ContentPlaceHolder1_ddl_mes option:selected").index() + 1, $("#ContentPlaceHolder1_txt_ano").val());

                        }
                        else {

                            alert("Boleta ya existe!!!");
                            limpiar();
                        }
                    }
                    //console.log(data);
                },
                error: function (data) {
                    console.info('error');
                }
            });

        }
    });


    function insertar_boleta(mes, ano) {
        var twoDigitMonth = ((fechaActual.getMonth().length + 1) === 1) ? (fechaActual.getMonth() + 1) : '0' + (fechaActual.getMonth() + 1);
        var currentDate = fechaActual.getFullYear() + "-" + twoDigitMonth + "-" + fechaActual.getDate();
        var parametro = new Object();
        parametro.id_personal = objeto["personal"].personal_id;
        parametro.fecha_emision_boleta = currentDate;
        parametro.neto_a_cobrar = parseFloat(total.toFixed(0));
        parametro.id_boleta = $("#ContentPlaceHolder1_txt_n_boleta").val();
        parametro.id_descuento_planilla = objeto["descuentoPlanilla"].IdDescuentoPlanilla;
        parametro.monto_boleta_planilla = total_descuento_planilla.toFixed(0);
        parametro.mes = mes;
        parametro.ano = ano;
        console.log(parametro);
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: 'WfBoleta.aspx/insertar_boleta_final',
            data: JSON.stringify(parametro),
            dataType: "json",
            async: true,
            success: function (data) {
                var objeto = JSON.parse(data.d);
                //console.log(data);
                confirmar_insert();
                limpiar();
            },
            error: function (data) {
                console.info('error');
            }
        });
    }
    function confirmar_insert() {
        var par = new Object();
        par.num_boleta = $("#ContentPlaceHolder1_txt_n_boleta").val();
        $.ajax({
            type: 'POST',
            contentType: "application/json; charset=utf-8",
            url: 'WfBoleta.aspx/consultar_num_boleta',
            data: JSON.stringify(par),
            dataType: "json",
            async: true,
            success: function (data) {
                var num_bol = JSON.parse(data.d);
                if (num_bol == "") {

                }
                else {
                    alert("Se registro correctamente!!");

                }
                //console.log(data);
            },
            error: function (data) {
                console.info('error');
            }
        });
    }
    function limpiar() {
        $("#ContentPlaceHolder1_txt_n_boleta").val("");

        $("#ContentPlaceHolder1_txt_dni").val("");
        $("#ContentPlaceHolder1_txt_apellidos_nombres").val("");
        $("#ContentPlaceHolder1_txt_cargo").val("");
        $("#ContentPlaceHolder1_txt_modalidad").val("");
        $("#ContentPlaceHolder1_txt_ano").val("");
        $("#ContentPlaceHolder1_txt_dias_trabajados").val("");
        $("#ContentPlaceHolder1_txt_planilla_estado").val("");
        $("#ContentPlaceHolder1_txt_dias_feriados").val("");
        $("#ContentPlaceHolder1_txt_dias_dominicales").val("");
        $("#ContentPlaceHolder1_txt_tardanzas").val("");
        $("#ContentPlaceHolder1_txt_permiso").val("");
        $("#ContentPlaceHolder1_txt_vacaciones").val("");
        $("#ContentPlaceHolder1_txt_neto_a_pagar").val("");

        $("#grd_descuento tbody tr").remove();
        $("#grd_remuneracion tbody tr").remove();
        $("#grd_descuento_planilla tbody tr").remove();
        $("#grd_movimientos tbody tr").remove();
        $("#grd_aportacion tbody tr").remove();

        $("#ContentPlaceHolder1_btn_guardar").attr("disabled", true);
        $("#ContentPlaceHolder1_btn_imprimir").attr("disabled", true);

    }
    $(document).ajaxStop(function () {
        // 0 === $.active
        total = total_remuneracion - total_descuento_planilla - total_descuentos - total_movimientos;
        $("#ContentPlaceHolder1_txt_neto_a_pagar").val(total.toFixed(2));
    });
});