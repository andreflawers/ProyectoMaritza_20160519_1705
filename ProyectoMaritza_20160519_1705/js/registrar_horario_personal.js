$(document).on("ready", function () {
    $(document).on("click", ".insertar_horario", function (e) {
        e.preventDefault();        
        $(".vista_2").removeClass("oculta");
        $(".vista_2").addClass("visible");
        $(".vista_1").removeClass("visible");
        $(".vista_1").addClass("oculta");

        var fila = $(this).parent().parent().index();
        fila++;
        
        var personal_id = $("#tabla_horario_personal tbody tr:nth-child(" + fila + ") td:nth-child(1)").html();
        
        var personal_dni = $("#tabla_horario_personal tbody tr:nth-child(" + fila + ") td:nth-child(13)").html();
        $("#dni_oculto_personal").val(personal_dni);
        $("#personal_id").val(personal_id);
    });
    
    $(document).on("click", "#insertar", function (e) {
        console.log('insertando');
        var descripcion = $("#descripcion_horario").val();
        var horario_inicial = $("#horario_inicial").val();
        var horario_final = $("#horario_final").val();
        var fecha_inicial = $("#fecha_inicio").val();
        var fecha_final = $("#fecha_final").val();
        var parametros = new Object();
        parametros.personal_id = $("#personal_id").val();
        parametros.horario_inicial = horario_inicial;
        parametros.horario_final = horario_final;
        parametros.fecha_inicial = fecha_inicial;
        parametros.fecha_final = fecha_final;
        parametros.descripcion = descripcion;

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "TurnoyRotaciondeHorario.aspx/insertarHorarioPersonal",
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {

                location.reload();
            },
            error: function (data) {
                console.info('error');
            }
        });
    });
    $(document).on("change", "#dniPersonal", function (e) {
        e.preventDefault();
        var ok = 0;
        $.each($(list_filtro), function (a, b) {
            if (b.personal_dni === $("#dniPersonal").val().trim()) {
                ok = 1;
            }
        });
        if (ok === 0) {
            $(".valida_campos").text("el documento no existe!!!");
        } else {

            $(".valida_campos").text("");
        }
    });
    $(document).on("click", "#actualizar", function (e) {
        console.log('actualizando');
        e.preventDefault();
        var descripcion = $("#descripcion_horario").val();
        var horario_inicial = $("#horario_inicial").val();
        var horario_final = $("#horario_final").val();
        var fecha_inicial = $("#fecha_inicio").val();
        var fecha_final = $("#fecha_final").val();
        var parametros = new Object();
        parametros.horario_id = $("#horario_id_actual").val();
        parametros.horario_inicial = horario_inicial;
        parametros.horario_final = horario_final;
        parametros.fecha_inicial = fecha_inicial;
        parametros.fecha_final = fecha_final;
        parametros.descripcion = descripcion;

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "TurnoyRotaciondeHorario.aspx/modificarHorarioPersonal",
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                location.reload();
            },
            error: function (data) {
                console.info('error');
            }
        });
    });
    $(document).on("click", ".modificar_horario", function (e) {
        e.preventDefault();
        alert('click modificar');
        $("#insertar").attr("id", "actualizar");
        $(".vista_2").removeClass("oculta");
        $(".vista_2").addClass("visible");
        $(".vista_1").removeClass("visible");
        $(".vista_1").addClass("oculta");

        var fila = $(this).parent().parent().index();
        console.log("cantidad de fila ? " + fila);
        fila++;
        
        var personal_id = $("#tabla_horario_personal tbody tr:nth-child(" + fila + ") td:nth-child(1)").html();
        var horario_id = $("#tabla_horario_personal tbody tr:nth-child(" + fila + ") td:nth-child(3)").html();
        var personal_dni = $("#tabla_horario_personal tbody tr:nth-child(" + fila + ") td:nth-child(13)").html();
        var horario_inicial = $("#tabla_horario_personal tbody tr:nth-child(" + fila + ") td:nth-child(6)").html();
        var horario_final = $("#tabla_horario_personal tbody tr:nth-child(" + fila + ") td:nth-child(7)").html();
        var fecha_inicial = ($("#tabla_horario_personal tbody tr:nth-child(" + fila + ") td:nth-child(8)").html());
        var fecha_final = ($("#tabla_horario_personal tbody tr:nth-child(" + fila + ") td:nth-child(9)").html());
        var descripcion = $("#tabla_horario_personal tbody tr:nth-child(" + fila + ") td:nth-child(10)").html();
        $("#fecha_inicio").val(fecha_inicial.trim());
        $("#fecha_final").val(fecha_final.trim());
        $("#horario_inicial").val(horario_inicial);
        $("#horario_final").val(horario_final);
        $("#descripcion_horario").val(descripcion);
        $("#dni_oculto_personal").val(personal_dni);
        $("#personal_id").val(personal_id);
        $("#horario_id_actual").val(horario_id);
    });
    function convertir_fecha(fecha_old) {
        var fecha_naci = fecha_old;
        var posi = fecha_naci.indexOf(" ");
        fecha_naci = fecha_naci.substring(0, posi);
        for (var i = 0; i < fecha_naci.length; i++) {
            if (fecha_naci.charAt(i) === '/') {
                var fh = fecha_naci.substring(0, i);
                var lo = fecha_naci.substring(i + 1, fecha_naci.length);
                fecha_naci = fh + "-" + lo;
            }
        }
        var mes = fecha_naci.substring(3, 5);
        var anho = fecha_naci.substring(6, fecha_naci.length);
        fecha_naci = anho + "-" + mes + "-" + fecha_naci.substring(0, 2);
        return fecha_naci;
    }
    function revierte_fecha(fecha_old) {
        fecha_naci = fecha_old;
        for (var i = 0; i < fecha_naci.length; i++) {
            if (fecha_naci.charAt(i) === '-') {
                var fh = fecha_naci.substring(0, i);
                var lo = fecha_naci.substring(i + 1, fecha_naci.length);
                fecha_naci = fh + "/" + lo;
            }
        }
        return fecha_naci;
    }
    $(document).on("click", ".eliminar_horario", function (e) {
        var fila = $(this).parent().parent().index();
        fila++;
        var parametros = new Object();
        var horario_id = $("#tabla_horario_personal tbody tr:nth-child(" + fila + ") td:nth-child(3)").html()
        parametros.horario_id = horario_id;
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "TurnoyRotaciondeHorario.aspx/eliminarHorario",
            data: JSON.stringify(parametros),
            dataType: "json",
            async: true,
            success: function (data) {
                $("#tabla_horario_personal tbody tr:nth-child(" + fila + ")").remove();
            },
            error: function (data) {
                console.info('error');
            }
        });
    });
    $(document).on("click", "#buscar_dni", function (e) {
        e.preventDefault();
        console.log("entreee");
        var personal_dni = $("#dniPersonal").val().trim();

        if (personal_dni.match(/^[0-9]+$/)) {
            var parametros = new Object();
            parametros.personal_dni = personal_dni;
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "TurnoyRotaciondeHorario.aspx/busquedaPorDNi",
                data: JSON.stringify(parametros),
                dataType: "json",
                async: true,
                success: function (data) {
                    var todo = "";
                    var cnt = 0;
                    console.log(data)
                    jQuery.each(data.d, function (index, value) {
                        cnt++;
                        var column1 = "<td style='display:none;'>" + value['personal_id'] + "</td>";
                        var column2 = "<td style='display:none;'>" + value['sucursal_id'] + "</td>";
                        var column3 = "<td style='display:none;'>" + value['horario_id'] + "</td>";
                        var column4 = "<td>" + value['personal_dni'] + "</td>";
                        var column5 = "<td>" + value['sucursal_nombre'] + "</td>";
                        var column6 = "<td>" + value['horario_inicial'] + "</td>";
                        var column7 = "<td>" + (value['horario_final']) + "</td>";
                        var varia8 = value['fecha_inicial'];
                        var varia9 = value['fecha_final'];

                        if (value['fecha_inicial'] !== "no existe") {
                            varia8 = convertir_fecha(value['fecha_inicial'])
                        }
                        if (value['fecha_final'] !== "no existe") {
                            varia9 = convertir_fecha(value['fecha_final']);
                        }
                        console.log(value['fecha_final']);
                        console.log(varia9);
                        var column8 = "<td>" + varia8 + "</td>";
                        var column9 = "<td>" + varia9 + "</td>";
                        var column10 = "<td>" + value['descripcion_horario'] + "</td>";
                        var column11 = "";
                        var column12 = "";
                        var column13 = "<td style='display:none;'> " + value['personal_nombre'] + "</td>";
                        if (value['horario_id'] === "0") {
                            column11 = "<td> <a href='javascript:;' class='insertar_horario btn btn-block btn-link'> Insertar horario</a> </td>";
                            column12 = "<td style='display:none;'> <a href='javascript:;' class='eliminar_horario btn btn-block btn-link'> Eliminar</a> </td>";
                        } else {
                            column11 = "<td> <a href='javascript:;' class='modificar_horario btn btn-block btn-link'> Modificar</a> </td>";
                            column12 = "<td> <a href='javascript:;' class='eliminar_horario btn btn-block btn-link'> Eliminar</a> </td>";
                        }
                        var fila = "<tr>" + column1 + column2 + column3 + column4 + column5 + column6 + column7 + column8 + column9 + column10 + column11 + column12 + column13 + "</tr>";
                        todo += fila;
                    });
                    console.log(cnt);
                    $("#tabla_horario_personal tbody").html(todo);
                },
                error: function (data) {
                    console.info('error');
                }
            });
        } else {
            $(".valida_campos").text("solo numeros");
        }
    });
    var parametros = new Object();
    parametros.pSC = 1
    var list_filtro = [];
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "TurnoyRotaciondeHorario.aspx/filtrarDniPersonal",
        data: JSON.stringify(parametros),
        dataType: "json",
        async: true,
        success: function (data) {
            $("#dniPersonal").typeahead('destroy');
            list_filtro = data.d;
            console.log(list_filtro);
            var arreglo = Array();
            $.each($(data.d), function (a, b) {
                arreglo[a] = b.personal_dni;
            });
            $("#dniPersonal").typeahead({ source: arreglo });
        },
        error: function (data) {
            console.info('error');
        }
    });
    $(document).on("change", "#dniPersonal", function (e) {
        e.preventDefault();
        var ok = 0;
        $.each($(list_filtro), function (a, b) {
            if (b.personal_dni === $("#dniPersonal").val().trim()) {
                ok = 1;
            }
        });
        if (ok === 0) {
            $(".valida_campos").text("el documento no existe!!!");
        } else {

            $(".valida_campos").text("");
        }
    });

});